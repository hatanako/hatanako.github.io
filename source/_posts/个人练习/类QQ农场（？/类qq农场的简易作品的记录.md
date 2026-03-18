---
title: 类QQ农场的简易作品记录和复盘
date: 2026-03-18
categories: 个人练习
tags:
  - 农场
---

这两天写了一个类似于QQ农场的玩意，因为有些地方不太会，问了AI，所以写在这里，从开头开始复盘一下。

## 新建项目

在新建项目之前，我们需要分析一下这个项目需要实现什么功能，达到什么样的效果：

1. 每块田需要种种子，浇水，施肥，收获，空地五个状态，那么也就对应了四个功能：种种子、浇水、施肥、收获
2. 我们需要去比较好的读取/存储数据，来记录玩家当前的状态（数据持久化）
3. 我们需要通过Excel表配置水果种类、各个种类水果的等级、和各个道具的数据（如狗粮、装饰品）

那么这样一个简单的QQ农场，我们这样分析下来只需要把它分成三大块功能：

1. UI面板逻辑（Script/UI）
2. 各种数据（Script/Data）
3. 各种数据的管理类，以及一些行为提供给外部，用来操作数据（Script/Manager）

如此项目结构就很清晰了，那么首先我们应该先配置Excel表中的数据，把Excel表中的数据以代码的形式表现出来：  
总共有三个数据大类：水果种类配置`FruitConfig`、水果等级配置`FruitLevelConfig`、道具配置`ItemConfig`

### 配置数据

``` csharp
/// <summary>
/// 获取途径
/// </summary>
public enum ENUM_GET_WAY
{
    /// <summary>
    /// 开局就有
    /// </summary>
    Start,
    /// <summary>
    /// 农场等级提升后解锁
    /// </summary>
    LevelUp,
    /// <summary>
    /// 种子培育后获得
    /// </summary>
    Grow,
    /// <summary>
    /// 合成游戏解锁
    /// </summary>
    Games
}

/// <summary>
/// 道具种类
/// </summary>
public enum ENUM_ITEM_TYPE 
{ 
    /// <summary>
    /// 肥料
    /// </summary>
    Fertilizer, 
    /// <summary>
    /// 装饰
    /// </summary>
    Decoration,
    /// <summary>
    /// 狗粮
    /// </summary>
    DogFood, 
    /// <summary>
    /// 培育材料
    /// </summary>
    Material 
}

/// <summary>
/// 植物信息配置
/// </summary>
[System.Serializable]
public class FruitConfig
{
    public string fruitName;
    public string cropType;
    public int unlockLevel; //解锁等级，-1代表特殊
    public string description;
    public List<ENUM_GET_WAY> getWays; //获取途径
    public int price;
    public float[] rotTime;
    public float saleTime;
}

/// <summary>
/// 植物升级配置
/// </summary>
[System.Serializable]
public class FruitLevelConfig
{
    public int level;
    public int upgradeCost; //升级醇浆
    public int goldCost; //升级金币
    public int gemCost; //升级钻石
    public float matureTime; //成熟时间，单位为秒
    public int harvestCount; //每次收获的数量
    public int upgradeGain; //掉落醇浆的数量，单位为个
    public float upgradeGainMultiplier; //掉落琼浆的概率，单位为百分比
    public int stealCount; //每次被偷的数量
    public int canGrowCount; //可再成长多少次
}

/// <summary>
/// 道具基类和培育材料
/// </summary>
public class ItemConfig
{
    public int itemId;
    public string itemName;
    public string description;
    public ENUM_ITEM_TYPE itemType;
    public List<ENUM_GET_WAY> getWays;
}
```

然后就会有问题了，每种道具的用处都不太一样呀，装饰会提升繁华度，肥料会减少植物成长时间，狗粮可以给狗升级和提升好感度，那么应该怎么做呢？  
继承就好啦（点头

``` csharp
/// <summary>
/// 肥料
/// </summary>
public class FertilizerConfig : ItemConfig
{
    public int lessWaitTime;
}

/// <summary>
/// 装饰
/// </summary>
public class DecorationConfig : ItemConfig
{
    public int beautyValue;
}

/// <summary>
/// 狗粮
/// </summary>
public class DogFoodConfig : ItemConfig
{
    public int petExp;
    public int petHeart;
}
```

好了，现在我们有表的一些基础的数据类了，接下来要怎么读表呢？  
首先，我们需要去下载一下[ELLPlus](https://www.nuget.org/packages/EPPlus/4.5.3.3)，下载下来后，我们把nupkg后缀改为zip后解压  
进入到`epplus.4.5.3.3\lib\net40`中将`ELLPlus.dll`文件复制到Unity中`Assets/Plugin`文件夹下，然后我们就可以使用它了。  
（当然也可以用别的第三方dll文件来读取表格，我因为有用过ELLPlus比较熟悉所以这里拿ELLPlus举例）

### 读取数据

如果想要读取数据的话，我们需要一个数据管理类`DataManager`，专门用来存储读取出来的数据。  
那么首先我们需要思考一下，对于水果种类配置、水果等级配置、道具配置最好应该用什么数据结构去存储呢？

1. 水果种类配置：对于水果种类配置我们完全可以用一个字典来存储，因为它不会重复，也不会更改，只有在需要的时候查找就行了：  
    `Dictionary<string, FruitConfig>`
2. 水果等级配置：对于水果等级的配置表，一个水果会有多个等级，只是用来查找的，也不会重复，所以我们可以和水果种类配置基本相同：  
    `Dictionary<string, List<FruitLevelConfig>>`在这里有一个补充，其实这里完全可以用HashSet来作为key，因为只涉及到读写，完全不需要增删改的功能
3. 道具配置：对于道具配置这一块，稍微比较复杂，我们需要去区分一个道具它是什么**种类**的，然后再通过id去查找它的数据，所以应该是这样的：
    `Dictionary<ENUM_ITEM_TYPE,Dictionary<int,ItemConfig>>`

然后对于DataManager，因为它在整个游戏中在不停的提供数据，为了避免多次创建，所以我们需要把它设置为单例模式，并挂载到场景上，而且要保证它不会因为场景切换而销毁。

``` csharp
public class DataManager : MonoBehaviour
{
    private static DataManager instance;
    public static DataManager Instance => instance;

    private Dictionary<string, FruitConfig> fruitConfigs;

    private Dictionary<string, List<FruitLevelConfig>> fruitLevelConfigs;

    private Dictionary<ENUM_ITEM_TYPE,Dictionary<int,ItemConfig>> itemConfigs;


    private void Awake()
    {
        if (instance != null && instance != this) { Destroy(gameObject); return; }
        instance = this;
        DontDestroyOnLoad(gameObject); // 场景切换不销毁
    }
}
```

然后我们就可以开始写读取数据的逻辑了：二者都可以放在`Awake`函数中，也可以放在`Start`函数中。

#### 先来看FruitConfigs和FruitLevelConfigs的读取

``` csharp
//DataManager.cs

/// <summary>
/// 读取水果表
/// </summary>
private void ReadFruitConfig()
{
    fruitConfigs = new Dictionary<string, FruitConfig>();
    fruitLevelConfigs = new Dictionary<string, List<FruitLevelConfig>>();
    List<FruitConfig> fruits = ExcelReader.ReadFruitConfigs("水果配置表.xlsx");
    List<FruitLevelConfig> fruitLevels = new List<FruitLevelConfig>();
    foreach (FruitConfig fruit in fruits)
    {
        fruitLevels = ExcelReader.ReadFruitLevelConfigs("水果配置表.xlsx", fruit.fruitName);
        fruitLevelConfigs.Add(fruit.fruitName,fruitLevels);
        fruitConfigs.Add(fruit.fruitName, fruit);
    }
}
```

我在这里又封装了一个类`ExcelReader`，专门用来读取Excel表格，只需要把Excel表放到指定的位置即可，但是在这之前需要先稍微补充一下ELLPlus的读取数据的方法：

``` csharp
FileInfo file = new FileInfo(EXCEL_PATH + fileName);
    using (ExcelPackage package = new ExcelPackage(file))
    {
        //读取第一张表
        ExcelWorksheet sheet = package.Workbook.Worksheets[1];
        //根据名字读取表
        ExcelWorksheet sheet1 = package.Workbook.Worksheets["水果配置表"];

        //得到表中的行数/列数
        // sheet.Dimension.Columns;
        // sheet.Dimension.Rows;


        //得到表中第几行第几列的数据
        sheet.Cells[row,column].Value;
    }
```

注意：读取表都是从1开始的，并不是从0下标开始！

``` csharp
//ExcelReader.cs

public static class ExcelReader
{
    private static string EXCEL_PATH = Application.dataPath + "/ArtRes/Excel/";

    /// <summary>
    /// 读取水果总表
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public static List<FruitConfig> ReadFruitConfigs(string fileName)
    {
        List<FruitConfig> fruits = new List<FruitConfig>();
        FruitConfig fruit = new FruitConfig();
        Dictionary<string, int> headers = new Dictionary<string, int>();

        float[] rotTime = null;
        float saleTime = 0;

        FileInfo file = new FileInfo(EXCEL_PATH + fileName);
        using (ExcelPackage package = new ExcelPackage(file))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];
            for (int col = 1; col <= sheet.Dimension.Columns; col++)
            {
                string header = sheet.Cells[1, col].Value?.ToString();
                if (string.IsNullOrEmpty(header)) continue; // 先判空再操作

                if (header.Contains("("))
                    header = header.Substring(0, header.IndexOf("(")).Trim();
                else if (header.Contains("（"))
                    header = header.Substring(0, header.IndexOf("（")).Trim();

                headers[header] = col;
            }

            for (int row = 2; row <= sheet.Dimension.Rows; row++)
            {
                fruit = new FruitConfig();
                fruit.fruitName = sheet.Cells[row, headers["水果名称"]].Value.ToString();
                fruit.cropType = sheet.Cells[row, headers["作物类型"]].Value.ToString();
                fruit.unlockLevel = int.Parse(sheet.Cells[row, headers["农场解锁等级"]].Value.ToString());
                fruit.description = sheet.Cells[row, headers["水果的描述"]].Value.ToString();
                fruit.getWays = GetWays(sheet.Cells[row, headers["获取途径"]].Value.ToString());
                fruit.price = int.Parse(sheet.Cells[row, headers["这个水果的单价"]].Value.ToString());

                string rotTimeStr = sheet.Cells[row, headers["腐烂等级时间，单位秒"]].Value?.ToString();
                if (!string.IsNullOrEmpty(rotTimeStr))
                    rotTime = GetRotTimes(rotTimeStr);
                if (rotTime != null)
                    fruit.rotTime = rotTime;

                string saleTimeStr = sheet.Cells[row, headers["单个水果的销售时间，单位秒"]].Value?.ToString();
                if (!string.IsNullOrEmpty(saleTimeStr))
                    saleTime = float.Parse(saleTimeStr);
                if (saleTime != 0)
                    fruit.saleTime = saleTime;
                fruits.Add(fruit);
            }
        }

        return fruits;
    }

    /// <summary>
    /// 获取水果等级配置列表
    /// </summary>
    /// <param name="fileName">Excel文件名</param>
    /// <param name="fruitName">水果名</param>
    /// <returns></returns>
    public static List<FruitLevelConfig> ReadFruitLevelConfigs(string fileName, string fruitName)
    {
        List<FruitLevelConfig> fruitLevels = new List<FruitLevelConfig>();
        FruitLevelConfig fruitLevel;
        Dictionary<string, int> headers = new Dictionary<string, int>();

        FileInfo file = new FileInfo(EXCEL_PATH + fileName);
        using (ExcelPackage package = new ExcelPackage(file))
        {
            ExcelWorksheet sheet = package.Workbook.Worksheets[fruitName + "的等级配置"];

            for (int col = 1; col <= sheet.Dimension.Columns; col++)
            {
                string header = sheet.Cells[1, col].Value?.ToString();
                if (string.IsNullOrEmpty(header)) continue; // 先判空再操作

                if (header.Contains("("))
                    header = header.Substring(0, header.IndexOf("(")).Trim();
                else if (header.Contains("（"))
                    header = header.Substring(0, header.IndexOf("（")).Trim();

                headers[header] = col;
            }
            for (int row = 2; row <= sheet.Dimension.Rows; row++)
            {
                fruitLevel = new FruitLevelConfig();

                fruitLevel.level = int.Parse(sheet.Cells[row, headers["等级"]].Value.ToString());
                fruitLevel.upgradeCost = int.Parse(sheet.Cells[row, headers["升级至此等级需要花费的升级醇浆数量"]].Value.ToString());
                fruitLevel.goldCost = int.Parse(sheet.Cells[row, headers["升级至此级需花费的金币"]].Value.ToString());
                fruitLevel.gemCost = int.Parse(sheet.Cells[row, headers["升至此级需花费的宝石"]].Value.ToString());
                if(headers.ContainsKey("成熟时间"))
                    fruitLevel.matureTime = float.Parse(sheet.Cells[row, headers["成熟时间"]].Value.ToString());
                else if(headers.ContainsKey("成熟时间，单位秒"))
                    fruitLevel.matureTime = float.Parse(sheet.Cells[row, headers["成熟时间，单位秒"]].Value.ToString());
                fruitLevel.harvestCount = int.Parse(sheet.Cells[row, headers["单次收获数量"]].Value.ToString());
                fruitLevel.upgradeGain = int.Parse(sheet.Cells[row, headers["单次掉落升级醇浆数量"]].Value.ToString());
                fruitLevel.upgradeGainMultiplier = float.Parse(sheet.Cells[row, headers["收获时升级醇浆的掉落概率"]].Value.ToString());
                fruitLevel.stealCount = int.Parse(sheet.Cells[row, headers["单次偷取成功的数量"]].Value.ToString());
                fruitLevel.canGrowCount = int.Parse(sheet.Cells[row, headers["可二次生长的次数"]].Value.ToString());

                fruitLevels.Add(fruitLevel);
            }

        }
        return fruitLevels;
    }

    /// <summary>
    /// 转换字符串为枚举类型：获取途径
    /// </summary>
    /// <param name="str">表头字符串</param>
    /// <returns>获取类型的List（或许可以改成HashSet？）</returns>
    private static List<ENUM_GET_WAY> GetWays(string str)
    {
        string[] newStr = str.Split('；');
        List<ENUM_GET_WAY> enumType = new List<ENUM_GET_WAY>();
        for (int i = 0; i < newStr.Length; i++)
        {
            switch (newStr[i])
            {
                case "开局就有":
                    enumType.Add(ENUM_GET_WAY.Start);
                    break;
                case "农场等级解锁":
                    enumType.Add(ENUM_GET_WAY.LevelUp);
                    break;
                case "种子培育":
                    enumType.Add(ENUM_GET_WAY.Grow);
                    break;
                case "合成游戏解锁":
                    enumType.Add(ENUM_GET_WAY.Games);
                    break;
            }
        }
        return enumType;
    }

    /// <summary>
    /// 字符串转为float类型：腐烂时间
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    private static float[] GetRotTimes(string str)
    {
        str = str.Replace("[", "");
        str = str.Replace("]", "");
        string[] newStr = str.Split(new char[] { ',' });
        float[] rotTimes = new float[newStr.Length];
        for (int i = 0; i < newStr.Length; i++)
        {
            rotTimes[i] = float.Parse(newStr[i]);
        }
        return rotTimes;
    }
}
```

来看代码：你会发现我在两个获取列表的里面都加入了一个`headers`字典，为什么要这么做呢？  
因为如果我在表格中表头乱序排序，~~那你不就炸了嘛~~，保险起见，做了这个字典，也让代码更加直观了一点：我们可以直接输入文字，来告诉我们这一行是什么东西

#### 再来看道具表的读取

``` csharp
//DataManager.cs

/// <summary>
/// 读取道具表
/// </summary>
private void ReadItemConfig()
{
    itemConfigs = new Dictionary<ENUM_ITEM_TYPE, Dictionary<int, ItemConfig>>();
    string fileName = "道具配置表.xlsx";
    itemConfigs.Add(ENUM_ITEM_TYPE.Fertilizer, ExcelReader.ReadItemConfigs(fileName,ENUM_ITEM_TYPE.Fertilizer));
    itemConfigs.Add(ENUM_ITEM_TYPE.Decoration, ExcelReader.ReadItemConfigs(fileName,ENUM_ITEM_TYPE.Decoration));
    itemConfigs.Add(ENUM_ITEM_TYPE.Material, ExcelReader.ReadItemConfigs(fileName,ENUM_ITEM_TYPE.Material));
    itemConfigs.Add(ENUM_ITEM_TYPE.DogFood, ExcelReader.ReadItemConfigs(fileName,ENUM_ITEM_TYPE.DogFood));
    
}
```

``` csharp
//ExcelReader.cs

/// <summary>
/// 获取道具配置列表
/// </summary>
/// <param name="fileName">文件名</param>
/// <param name="type">道具类型</param>
/// <returns>一个字典，key为道具ID，value为道具内容</returns>
public static Dictionary<int, ItemConfig> ReadItemConfigs(string fileName, ENUM_ITEM_TYPE type)
{
    Dictionary<int, ItemConfig> items = new Dictionary<int, ItemConfig>();
    Dictionary<string, int> headers = new Dictionary<string, int>();

    FileInfo file = new FileInfo(EXCEL_PATH + fileName);
    using (ExcelPackage package = new ExcelPackage(file))
    {
        ExcelWorksheet sheet = package.Workbook.Worksheets[GetSheetName(type)];
        for (int col = 1; col <= sheet.Dimension.Columns; col++)
        {
            string header = sheet.Cells[1, col].Value?.ToString();
            if (string.IsNullOrEmpty(header)) continue; // 先判空再操作

            if (header.Contains("("))
                header = header.Substring(0, header.IndexOf("(")).Trim();
            else if (header.Contains("（"))
                header = header.Substring(0, header.IndexOf("（")).Trim();

            headers[header] = col;
        }

        for (int row = 2; row <= sheet.Dimension.Rows; row++)
        {
            if (sheet.Cells[row, headers["物品的ID"]].Value == null) continue;
            ItemConfig item = new ItemConfig();
            switch (type)
            {
                case ENUM_ITEM_TYPE.Fertilizer:
                    FertilizerConfig fertilizer = new FertilizerConfig();
                    fertilizer.lessWaitTime = int.Parse(sheet.Cells[row, headers["减少成熟等待时间的量"]].Value?.ToString());
                    item = fertilizer;
                    break;
                case ENUM_ITEM_TYPE.Decoration:
                    DecorationConfig decoration = new DecorationConfig();
                    decoration.beautyValue = int.Parse(sheet.Cells[row, headers["繁荣度的加成值"]].Value?.ToString());
                    item = decoration;
                    break;
                case ENUM_ITEM_TYPE.DogFood:
                    DogFoodConfig dogFood = new DogFoodConfig();
                    dogFood.petHeart = int.Parse(sheet.Cells[row, headers["单次喂食增加的宠物心情值"]].Value?.ToString());
                    dogFood.petExp = int.Parse(sheet.Cells[row, headers["单次喂食增加的宠物经验"]].Value?.ToString());
                    item = dogFood;
                    break;
                case ENUM_ITEM_TYPE.Material:
                    break;
            }
            item.itemId = int.Parse(sheet.Cells[row, headers["物品的ID"]].Value?.ToString());
            item.itemName = sheet.Cells[row, headers["物品名"]].Value?.ToString();
            item.description = sheet.Cells[row, headers["物品的描述"]].Value?.ToString();
            item.getWays = GetWays(sheet.Cells[row, headers["获取途径"]].Value?.ToString());

            items.Add(item.itemId, item);
        }
    }

    return items;
}

/// <summary>
/// 获取道具配置表的表名
/// </summary>
/// <param name="type">道具种类</param>
/// <returns>表名</returns>
private static string GetSheetName(ENUM_ITEM_TYPE type)
{
    switch (type)
    {
        case ENUM_ITEM_TYPE.Fertilizer: return "肥料的配置";
        case ENUM_ITEM_TYPE.Decoration: return "装饰的配置";
        case ENUM_ITEM_TYPE.DogFood: return "狗粮的配置";
        case ENUM_ITEM_TYPE.Material: return "培育材料的配置";
        default: return "";
    }
}
```

到这里我们就获取到所有的配表数据了，~~这一块花了我好长时间，累死了~~，然后接下来我们可以先创建一个田地管理类`FarmManager`了！

## 田地逻辑

## UI相关

## 拖拽相关

## 仓库相关