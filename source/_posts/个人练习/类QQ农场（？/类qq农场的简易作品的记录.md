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
    //创建新的字典类，清空数据
    fruitConfigs = new Dictionary<string, FruitConfig>();
    fruitLevelConfigs = new Dictionary<string, List<FruitLevelConfig>>();
    //读取表格内容
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
        //表格头部
        Dictionary<string, int> headers = new Dictionary<string, int>();

        //两个可能为空的数据列，所以需要单独提取出来做判断
        float[] rotTime = null;
        float saleTime = 0;

        //主要读取逻辑
        FileInfo file = new FileInfo(EXCEL_PATH + fileName);
        using (ExcelPackage package = new ExcelPackage(file))
        {
            //获取第一张表格
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];

            //读取表格头部名字对应的列号
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

            //读取表格数据
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
                //如果表格中不是空的就读取，如果是，则复用上一个
                if (!string.IsNullOrEmpty(rotTimeStr))
                    rotTime = GetRotTimes(rotTimeStr);
                if (rotTime != null)
                    fruit.rotTime = rotTime;

                string saleTimeStr = sheet.Cells[row, headers["单个水果的销售时间，单位秒"]].Value?.ToString();
                //同上
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

在创建之前我们需要先思考一下，一个田地管理类都需要什么。  
首先是每块田的田地数据，我们需要新建一个类`FieldObject`来挂载到GameObject上，但是我们要怎么区分它呢？答案是用ID来区分管理，我们可以通过在每个田地上挂载脚本，然后为它们附上ID。  
因为是一个练习作品，所以不太需要自动生成田地。  
然后我们来分析一下田地的状态属性：它需要有ID、种植的水果名、田地的状态（要浇水、要施肥、可收获、可种植）、种植的水果的等级、

``` csharp
//FieldObject.cs
public class FieldObject : MonoBehaviour
{
    public int fieldId;
    private FieldData data;

    public void Init(FieldData data)
    {
        this.data = data;
    }
}

//FieldData.cs
public class FieldData
{
    public int fieldId;
    public string fruitName;
    public ENUM_FIELD_STATE state;
    public int fruitLevel;
}

/// <summary>
/// 地块状态
/// </summary>
public enum ENUM_FIELD_STATE
{
    /// <summary>
    /// 空地
    /// </summary>
    Empty,
    /// <summary>
    /// 需要水
    /// </summary>
    NeedWater,
    /// <summary>
    /// 生长中（可施肥）
    /// </summary>
    Growing,
    /// <summary>
    /// 成熟
    /// </summary>
    Mature
}
```

这样一个简单的田地数据就完成了，我们接下来需要一个玩家数据类`PlayerData`，来读取一些玩家的信息，才能进行一些行为：

``` csharp

public class PlayerData
{
    public int id;
    public int nowLevel;
    public int waterCount;
    public int coins;
    public int gems;
    public InventoryData inventory;
    public List<FieldData> fields;
    public Dictionary<string, int> fruitLevels;
}

public class InventoryData
{
    /// <summary>
    /// Key的命名规则为：fruitName_fruitLevel
    /// </summary>
    public Dictionary<string, int> fruits;
    /// <summary>
    /// Key的命名规则为：itemType_itemId
    /// </summary>
    public Dictionary<string, int> items;
}
```

然后我们就可以来写田地管理类啦：

``` csharp
public class FarmManager : MonoBehaviour
{
    private static FarmManager instance;
    public static FarmManager Instance => instance;

    private PlayerData playerData;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        playerData = DataManager.Instance.LoadPlayerData();
        InitFields();
    }

    #region 田地的行为

    /// <summary>
    /// 种植
    /// </summary>
    /// <param name="fieldId">田地ID</param>
    /// <param name="fruitName">水果名字</param>
    public void Plant(int fieldId, string fruitName)
    {
        FieldData field = playerData.fields.Find(f => f.fieldId == fieldId);

        if (field == null || field.state != ENUM_FIELD_STATE.Empty)
            return;
        if (!playerData.fruitLevels.ContainsKey(fruitName)) return;

        field.state = ENUM_FIELD_STATE.NeedWater;
        field.fruitName = fruitName;
        field.fruitLevel = playerData.fruitLevels[fruitName];
    }

    /// <summary>
    /// 一键种植
    /// </summary>
    /// <param name="fruitName"></param>
    public void PlantAll(string fruitName)
    {
        foreach (FieldData field in playerData.fields)
        {
            if (field.state != ENUM_FIELD_STATE.Empty) continue;
            Plant(field.fieldId, fruitName);
        }
    }

    /// <summary>
    /// 浇水
    /// </summary>
    /// <param name="fieldId">田地ID</param>
    public void Water(int fieldId)
    {
        FieldData field = playerData.fields.Find(f => f.fieldId == fieldId);
        if (playerData.waterCount <= 0) return;
        if (field == null || field.state != ENUM_FIELD_STATE.NeedWater) return;

        field.state = ENUM_FIELD_STATE.Growing;
        playerData.waterCount--;
    }

    /// <summary>
    /// 一键浇水
    /// </summary>
    public void WaterAll()
    {
        foreach (FieldData field in playerData.fields)
        {
            if (field.state != ENUM_FIELD_STATE.NeedWater) continue;
            if (playerData.waterCount <= 0) break; // 水用完了就停
            field.state = ENUM_FIELD_STATE.Growing;
            playerData.waterCount--;
        }
    }

    /// <summary>
    /// 施肥
    /// </summary>
    /// <param name="fieldId">田地ID</param>
    /// <param name="fertilizerId">肥料ID</param>
    public void Fertilize(int fieldId, int fertilizerId)
    {
        FieldData field = playerData.fields.Find(f => f.fieldId == fieldId);
        if (field == null || field.state != ENUM_FIELD_STATE.Growing)
            return;
        string key = "Fertilizer_" + fertilizerId;
        if (!playerData.inventory.items.ContainsKey(key) ||
             playerData.inventory.items[key] <= 0) return;

        playerData.inventory.items["Fertilizer_" + fertilizerId]--;
        field.state = ENUM_FIELD_STATE.Mature;

    }

    /// <summary>
    /// 一键施肥
    /// </summary>
    public void FertilizeAll(int fertilizerId)
    {
        string key = "Fertilizer_" + fertilizerId;
        foreach (FieldData field in playerData.fields)
        {
            if (field.state != ENUM_FIELD_STATE.Growing) continue;
            if (!playerData.inventory.items.ContainsKey(key) ||
            playerData.inventory.items[key] <= 0) return;
            playerData.inventory.items["Fertilizer_" + fertilizerId]--;
            field.state = ENUM_FIELD_STATE.Mature;
        }
    }

    /// <summary>
    /// 收获
    /// </summary>
    /// <param name="fieldId">田地ID</param>
    public void Harvest(int fieldId)
    {
        FieldData field = playerData.fields.Find(f => f.fieldId == fieldId);
        if (field == null || field.state != ENUM_FIELD_STATE.Mature)
            return;

        FruitLevelConfig fruit = DataManager.Instance.GetFruitLevelConfig(field.fruitName, field.fruitLevel);
        if (fruit == null)
            return;

        string keyName = field.fruitName + "_" + field.fruitLevel;
        if (playerData.inventory.fruits.ContainsKey(keyName))
            playerData.inventory.fruits[keyName] += fruit.harvestCount;
        else
            playerData.inventory.fruits.Add(keyName, fruit.harvestCount);

        field.state = ENUM_FIELD_STATE.Empty;
        field.fruitName = "";
        field.fruitLevel = 0;
    }

    /// <summary>
    /// 一键收获
    /// </summary>
    public void HarvestAll()
    {
        foreach (FieldData field in playerData.fields)
        {
            if (field.state == ENUM_FIELD_STATE.Mature)
                Harvest(field.fieldId);
        }
    }

    #endregion

    /// <summary>
    /// 初始化田地
    /// </summary>
    private void InitFields()
    {
        // 找到场景里所有 FieldObject
        FieldObject[] fieldObjects = FindObjectsOfType<FieldObject>();

        foreach (FieldObject fieldObj in fieldObjects)
        {
            // 检查 playerData.fields 里有没有这个 fieldId 的数据
            FieldData data = playerData.fields.Find(f => f.fieldId == fieldObj.fieldId);

            // 没有就创建一条默认数据（空地）
            if (data == null)
            {
                data = new FieldData
                {
                    fieldId = fieldObj.fieldId,
                    fruitName = "",
                    state = ENUM_FIELD_STATE.Empty,
                    fruitLevel = 0
                };
                playerData.fields.Add(data);
            }

            // 让 FieldObject 持有自己的数据引用
            fieldObj.Init(data);
        }
    }

    /// <summary>
    /// 获取田地信息
    /// </summary>
    /// <param name="fieldId">田地ID</param>
    /// <returns></returns>
    public FieldData GetFieldData(int fieldId)
    {
        return playerData.fields.Find(f => f.fieldId == fieldId);
    }

    public List<FieldData> GetAllField()
    {
        return playerData.fields;
    }

    /// <summary>
    /// 获取已解锁的水果列表
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int> GetUnlockedFruits()
    {
        return playerData.fruitLevels;
    }

    /// <summary>
    /// 获取仓库里的道具列表
    /// </summary>
    /// <returns></returns>
    public Dictionary<string, int> GetItems()
    {
        return playerData.inventory.items;
    }

    /// <summary>
    /// 存档
    /// </summary>
    public void Save()
    {
        DataManager.Instance.SavePlayerData(playerData);
    }
}
```

这样一个田地管理类就完成的差不多啦，我们还需要在DataManager中加几个方法，提供给FarmManager：

``` csharp
/// <summary>
/// 获得水果参数
/// </summary>
/// <param name="fruitName">水果名</param>
/// <returns>水果参数</returns>
public FruitConfig GetFruitConfig(string fruitName)
{
    fruitConfigs.TryGetValue(fruitName, out FruitConfig config);
    return config;
}

/// <summary>
/// 获得水果等级参数
/// </summary>
/// <param name="fruitName">水果名</param>
/// <param name="level">水果等级</param>
/// <returns>水果等级参数</returns>
public FruitLevelConfig GetFruitLevelConfig(string fruitName, int level)
{
    if (fruitLevelConfigs.TryGetValue(fruitName, out var levels))
        return levels[level - 1]; // level从1开始，下标从0开始
    return null;
}

/// <summary>
/// 获得道具参数
/// </summary>
/// <param name="type">道具类别</param>
/// <param name="id">道具id</param>
/// <returns>道具参数</returns>
public ItemConfig GetItemConfig(ENUM_ITEM_TYPE type, int id)
{
    if (itemConfigs.TryGetValue(type, out var items))
        if (items.TryGetValue(id, out var item))
            return item;
    return null;
}

public void SavePlayerData(PlayerData data)
{
    JsonManager.Instance.SaveData(data, "PlayerData");
}

public PlayerData LoadPlayerData()
{
    PlayerData data = JsonManager.Instance.LoadData<PlayerData>("PlayerData");

    // 初始化默认值，防止空引用
    if (data.inventory == null)
        data.inventory = new InventoryData
        {
            fruits = new Dictionary<string, int>(),
            items = new Dictionary<string, int>()

        };

    if (data.fields == null)
        data.fields = new List<FieldData>();

    if(data.fruitLevels == null)
        data.fruitLevels = new Dictionary<string, int>();

    return data;
}
```

噢，存读档这里有一个JsonManager，是我之前学的时候封装的一个管理类，在这里贴一下代码：

``` csharp
using LitJson;
using System.IO;
using UnityEngine;

/// <summary>
/// 序列化和反序列化Json数据的类型
/// </summary>
public enum JsonType
{
    JsonUtility,
    LitJson
    //其他Json库
}

/// <summary>
/// Json数据管理类，主要用于进行 Json数据的读取与写入
/// </summary>
public class JsonManager
{
    private static JsonManager instance = new JsonManager();
    public static JsonManager Instance { get { return instance; } }
    private JsonManager()
    { }

    //存储Json数据 序列化
    public void SaveData(object data, string fileName, JsonType type = JsonType.LitJson)
    {
        //确定存储路径
        string path = Application.persistentDataPath + "/" + fileName + ".json";
        string jsonData = "";
        //选择Json库进行序列化
        switch (type)
        {
            case JsonType.JsonUtility:
                jsonData = JsonUtility.ToJson(data, true);
                break;
            case JsonType.LitJson:
                jsonData = JsonMapper.ToJson(data);
                break;
        }

        //写入文件
        File.WriteAllText(path, jsonData);
    }

    public T LoadData<T>(string fileName, JsonType type = JsonType.LitJson) where T : new()
    {
        //确定存储路径
        //首先先判断默认数据文件夹是否存在数据文件
        string path = Application.streamingAssetsPath + "/" + fileName + ".json";
        if (!File.Exists(path))
            path = Application.persistentDataPath + "/" + fileName + ".json";
        //如果读写路径都没有数据文件，则返回默认数据
        if (!File.Exists(path))
            return new T();
        string jsonData = File.ReadAllText(path);

        T data = default;
        //选择Json库进行反序列化
        switch (type)
        {
            case JsonType.JsonUtility:
                data = JsonUtility.FromJson<T>(jsonData);
                break;
            case JsonType.LitJson:
                data = JsonMapper.ToObject<T>(jsonData);
                break;
        }
        return data;
    }

}
```

然后我们就来到了UI部分！~~鼓掌👏👏👏👏~~

## UI相关

在开始做UI之前，我们需要分析一下，我们需要做一个什么样的UI：

+ 我希望它可以淡入淡出
+ 我希望它可以方便调用，动态创建

那么需求确立了，我们就可以去做一个基类来实现这样的功能：

``` csharp
using UnityEngine;
using UnityEngine.Events;

public abstract class BasePanel : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private float alphaSpeed = 10;

    public bool isShow = false;
    private UnityAction hideCallBack = null;

    protected virtual void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    protected virtual void Start()
    {
        Init();
    }

    public abstract void Init();

    public virtual void ShowMe()
    {
        canvasGroup.alpha = 0;
        isShow = true;
        // 显示时开启交互和射线，让按钮可以点击
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void HideMe(UnityAction callBack)
    {
        canvasGroup.alpha = 1;
        isShow = false;
        hideCallBack = callBack;
        // 开始淡出时立刻关闭射线，防止淡出过程中还能点击
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
    }

    protected virtual void Update()
    {
        if (isShow && canvasGroup.alpha != 1)
        {
            canvasGroup.alpha += alphaSpeed * Time.unscaledDeltaTime;
            if (canvasGroup.alpha >= 1)
                canvasGroup.alpha = 1;
        }
        else if (!isShow && canvasGroup.alpha != 0)
        {
            canvasGroup.alpha -= alphaSpeed * Time.unscaledDeltaTime;
            if (canvasGroup.alpha <= 0)
            {
                canvasGroup.alpha = 0;
                hideCallBack?.Invoke();
            }
        }
    }
}
```

然后我们需要一个UI管理类

``` csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    private static UIManager instance = new UIManager();
    public static UIManager Instance => instance;

    //用于存储显示着的面板的 每显示一个面板 就会存入这个字典
    //隐藏面板时 直接获取字典中的对应面板 进行隐藏
    private Dictionary<string, BasePanel> panelDic = new Dictionary<string, BasePanel>();

    //场景中的 Canvas对象 用于设置为面板的父对象
    private Transform canvasTrans;
    public Transform CanvasTrans => canvasTrans; 

    private UIManager()
    {
        //得到场景中的Canvas对象
        GameObject canvas = GameObject.Instantiate(Resources.Load<GameObject>("UI/Canvas"));
        canvasTrans = canvas.transform;
        //通过过场景不移除该对象 保证这个游戏过程中 只有一个 canvas对象
        GameObject.DontDestroyOnLoad(canvas);
    }

    //显示面板
    public T ShowPanel<T>() where T:BasePanel
    {
        //我们只需要保证 泛型T的类型 和面板预设体名字 一样 定一个这样的规则 就可以非常方便的让我们使用了
        string panelName = typeof(T).Name;

        //判断 字典中 是否已经显示了这个面板
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;

        //显示面板 根据面板名字 动态的创建预设体 设置父对象
        GameObject panelObj = GameObject.Instantiate(Resources.Load<GameObject>("UI/" + panelName));
        //把这个对象 放到场景中的 Canvas下面
        panelObj.transform.SetParent(canvasTrans, false);

        //指向面板上 显示逻辑 并且应该把它保存起来
        T panel = panelObj.GetComponent<T>();
        //把这个面板脚本 存储到字典中 方便之后的 获取 和 隐藏
        panelDic.Add(panelName, panel);
        //调用自己的显示逻辑
        panel.ShowMe();

        return panel;
    }

    /// <summary>
    /// 隐藏面板
    /// </summary>
    /// <typeparam name="T">面板类名</typeparam>
    /// <param name="isFade">是否淡出完毕过后才删除面板 默认是ture</param>
    public void HidePanel<T>(bool isFade = true) where T:BasePanel
    {
        //根据泛型得名字
        string panelName = typeof(T).Name;
        //判断当前显示的面板 有没有你想要隐藏的
        if( panelDic.ContainsKey(panelName) )
        {
            if( isFade )
            {
                //就是让面板 淡出完毕过后 再删除它 
                panelDic[panelName].HideMe(() =>
                {
                    //删除对象
                    GameObject.Destroy(panelDic[panelName].gameObject);
                    //删除字典里面存储的面板脚本
                    panelDic.Remove(panelName);
                });
            }
            else
            {
                //删除对象
                GameObject.Destroy(panelDic[panelName].gameObject);
                //删除字典里面存储的面板脚本
                panelDic.Remove(panelName);
            }
        }
    }

    //得到面板
    public T GetPanel<T>() where T:BasePanel
    {
        string panelName = typeof(T).Name;
        if (panelDic.ContainsKey(panelName))
            return panelDic[panelName] as T;
        //如果没有对应面板显示 就返回空
        return null;
    }

}

```

然后后续我们就只需要去继承这个基类来制作面板就好了，下面直接贴代码和图片了：

{% asset_img 1.png %}

``` csharp
public class FieldPanel : BasePanel
{
    private FieldData currentField;

    public Text txtTitle;
    public Text txtState;
    public Text txtFruitName;
    public Text txtLevel;
    public Button btnOne;
    public Text btnOneText;
    public Button btnAll;
    public Text btnAllText;
    public Button btnClose;

    public override void Init()
    {
        txtTitle.text = "田地" + currentField.fieldId;
        txtState.text = CheckState(currentField.state);
        txtFruitName.text = currentField.fruitName;
        txtLevel.text = "Lv" + currentField.fruitLevel;
        btnClose.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<FieldPanel>();
        });
        btnOne.onClick.AddListener(() =>
        {
            if (DragManager.Instance.isDragging) return;
            OnClickOne();
        });
        btnAll.onClick.AddListener(() =>
        {
            OnClickAll();
        });
    }

    /// <summary>
    /// 检查当前状态
    /// </summary>
    /// <param name="state"></param>
    /// <returns></returns>
    private string CheckState(ENUM_FIELD_STATE state)
    {
        string txt = "";
        switch (state)
        {
            case ENUM_FIELD_STATE.Empty:
                txt = "空的";
                break;
            case ENUM_FIELD_STATE.NeedWater:
                txt = "需要水";
                break;
            case ENUM_FIELD_STATE.Growing:
                txt = "需要施肥";
                break;
            case ENUM_FIELD_STATE.Mature:
                txt = "可收获";
                break;
        }
        return txt;
    }

    /// <summary>
    /// 外部调用，在ShowPanel之后刷新显示
    /// </summary>
    /// <param name="data">田地Data</param>
    public void SetField(FieldData data)
    {
        currentField = data;
        RefreshUI(); // 有了数据再刷新显示
    }

    /// <summary>
    /// 刷新UI
    /// </summary>
    private void RefreshUI()
    {
        txtTitle.text = "田地" + currentField.fieldId;
        txtState.text = CheckState(currentField.state);
        txtFruitName.text = currentField.fruitName;
        txtLevel.text = currentField.fruitLevel > 0 ? "Lv" + currentField.fruitLevel : "";

        // 根据状态设置按钮文字和行为
        switch (currentField.state)
        {
            case ENUM_FIELD_STATE.Empty:
                btnOne.gameObject.SetActive(true);
                btnAll.gameObject.SetActive(true);
                btnOneText.text = "种植";
                btnAllText.text = "一键种植";
                break;
            case ENUM_FIELD_STATE.NeedWater:
                btnOne.gameObject.SetActive(true);
                btnAll.gameObject.SetActive(true);
                btnOneText.text = "浇水";
                btnAllText.text = "一键浇水";
                break;
            case ENUM_FIELD_STATE.Growing:
                btnOne.gameObject.SetActive(true);
                btnAll.gameObject.SetActive(true);
                btnOneText.text = "施肥";
                btnAllText.text = "一键施肥";
                break;
            case ENUM_FIELD_STATE.Mature:
                btnOne.gameObject.SetActive(true);
                btnAll.gameObject.SetActive(true);
                btnOneText.text = "收获";
                btnAllText.text = "一键收获";
                break;
        }
    }

    /// <summary>
    /// 点击单个按钮的方法
    /// </summary>
    private void OnClickOne()
    {
        switch (currentField.state)
        {
            case ENUM_FIELD_STATE.Empty:
                // TODO: 弹出水果选择界面
                UIManager.Instance.ShowPanel<ChoosePanel>().SetData(currentField,ENUM_SELECT_TYPE.Fruit,false);
                break;
            case ENUM_FIELD_STATE.NeedWater:
                FarmManager.Instance.Water(currentField.fieldId);
                break;
            case ENUM_FIELD_STATE.Growing:
                // TODO: 弹出肥料选择界面
                UIManager.Instance.ShowPanel<ChoosePanel>().SetData(currentField, ENUM_SELECT_TYPE.Fertilizer,false);
                break;
            case ENUM_FIELD_STATE.Mature:
                FarmManager.Instance.Harvest(currentField.fieldId);
                break;
        }
        UIManager.Instance.HidePanel<FieldPanel>();
        RefreshUI();
    }

    /// <summary>
    /// 点击All按钮的方法
    /// </summary>
    private void OnClickAll()
    {
        switch (currentField.state)
        {
            case ENUM_FIELD_STATE.Empty:
                // TODO: 弹出水果选择界面，选完后 PlantAll
                UIManager.Instance.ShowPanel<ChoosePanel>().SetData(currentField, ENUM_SELECT_TYPE.Fruit, true);
                break;
            case ENUM_FIELD_STATE.NeedWater:
                FarmManager.Instance.WaterAll();
                break;
            case ENUM_FIELD_STATE.Growing:
                // TODO: 弹出肥料选择界面，选完后 FertilizeAll
                UIManager.Instance.ShowPanel<ChoosePanel>().SetData(currentField, ENUM_SELECT_TYPE.Fertilizer, true);
                break;
            case ENUM_FIELD_STATE.Mature:
                FarmManager.Instance.HarvestAll();
                break;
        }
        UIManager.Instance.HidePanel<FieldPanel>();
        RefreshUI();
    }
}
```

{% asset_img 2.png %}

``` csharp
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ENUM_SELECT_TYPE
{
    Fruit,
    Fertilizer
}

public class ChoosePanel : BasePanel
{
    public Text txtTitle;
    public Transform content;
    public Button btnClose;

    private FieldData currentField;

    public GameObject selectItemPrefab;

    private bool isAllButton;
    public override void Init()
    {
        btnClose.onClick.AddListener(() =>
        {
            UIManager.Instance.HidePanel<ChoosePanel>();
        });
    }

    /// <summary>
    /// 设置数据，注意：直接调用了Destroy，可优化
    /// </summary>
    /// <param name="field"></param>
    /// <param name="type"></param>
    public void SetData(FieldData field, ENUM_SELECT_TYPE type, bool isAll)
    {
        isAllButton = isAll;
        currentField = field;
        foreach (Transform child in content)
            Destroy(child.gameObject);
        //清空旧列表，生成新列表
        if (type == ENUM_SELECT_TYPE.Fruit)
            BuildFruitList();
        else
            BuildFertilizerList();
    }

    /// <summary>
    /// 生成可种植列表，注意：直接调用了Instantiate，可优化
    /// </summary>
    private void BuildFruitList()
    {
        //获取已经解锁的水果列表
        Dictionary<string, int> fruits = FarmManager.Instance.GetUnlockedFruits();
        txtTitle.text = "请选择种子";
        foreach (var fruit in fruits)
        {
            //生成槽位
            GameObject selectItem = Instantiate(selectItemPrefab, content);
            SelectItem script = selectItem.GetComponent<SelectItem>();

            string fruitName = fruit.Key;
            int level = fruit.Value;
            script.Init(fruitName, "Lv" + level, "", () =>
            {
                if (!isAllButton)
                {
                    FarmManager.Instance.Plant(currentField.fieldId, fruitName);
                }
                else
                {
                    FarmManager.Instance.PlantAll(fruitName);
                }
                UIManager.Instance.HidePanel<ChoosePanel>();
            }, ENUM_DRAG_TYPE.Plant, fruitName);
        }
    }

    /// <summary>
    /// 生成肥料列表，同上，可优化
    /// </summary>
    private void BuildFertilizerList()
    {
        Dictionary<string, int> items = FarmManager.Instance.GetItems();
        txtTitle.text = "请选择肥料";
        foreach (var item in items)
        {
            string key = item.Key;
            string itemType = key.Split('_')[0];
            if (itemType != ENUM_ITEM_TYPE.Fertilizer.ToString())
                continue;
            int itemId = int.Parse(key.Split("_")[1]);
            int count = item.Value;

            ItemConfig config = DataManager.Instance.GetItemConfig(ENUM_ITEM_TYPE.Fertilizer, itemId);

            GameObject selectItem = Instantiate(selectItemPrefab, content);
            SelectItem script = selectItem.GetComponent<SelectItem>();
            script.Init(config.itemName, itemType, count.ToString(), () =>
            {
                if (!isAllButton)
                {
                    FarmManager.Instance.Fertilize(currentField.fieldId, itemId);
                }
                else
                {
                    FarmManager.Instance.FertilizeAll(itemId);
                }
                UIManager.Instance.HidePanel<ChoosePanel>();
            }, ENUM_DRAG_TYPE.Fertilize, "", itemId);
        }
    }
}

```

然后在ChoosePanel中有一个预制体需要根据数量生成，它上面也有挂载脚本：

{% asset_img 3.png %}

``` csharp
public class SelectItem : MonoBehaviour
{
    public Text txtName;
    public Text txtDescription;
    public Text txtCount;
    private Button btn;

    private ENUM_DRAG_TYPE dragType;
    private string fruitName;
    private int fertilizerId;

    private void Awake()
    {
        btn = this.gameObject.AddComponent<Button>();
    }

    public void Init(string name, string desc, string count, UnityAction onSelect, ENUM_DRAG_TYPE type,
                     string fruit = "", int fertId = -1)
    {
        txtName.text = name;
        txtDescription.text = desc;
        txtCount.text = count;
        btn.onClick.RemoveAllListeners(); // 防止重复添加
        btn.onClick.AddListener(onSelect);
        dragType = type;
        fruitName = fruit;
        fertilizerId = fertId;
    }
}
```

{% asset_img 4.png %}

``` csharp
using UnityEngine;
using UnityEngine.UI;

public class GamePanel : BasePanel
{
    public Button btnAddWater;
    public Button btnUnlockFruit;
    public Button btnAddItem;
    public Button btnUpgradeFruit;
    public Button btnInventory;

    [Header("测试参数")]
    [InspectorName("一次获得多少水")]
    public int addWaterAmount = 10;
    [InspectorName("需要解锁的水果名")]
    public string unlockFruitName = "苹果";
    [InspectorName("添加的道具种类")]
    public ENUM_ITEM_TYPE addItemType = ENUM_ITEM_TYPE.Fertilizer;
    [InspectorName("添加的道具ID")]
    public int addItemId = 1;
    [InspectorName("添加的道具数量")]
    public int addItemAmount = 5;
    [InspectorName("想要升级的种子名")]
    public string upgradeFruitName = "苹果";

    public override void Init()
    {
        btnAddWater.onClick.AddListener(() =>
        {
            FarmManager.Instance.AddWater(addWaterAmount);
            Debug.Log($"水量+{addWaterAmount}");
        });

        btnUnlockFruit.onClick.AddListener(() =>
        {
            FarmManager.Instance.UnlockFruit(unlockFruitName);
            Debug.Log($"解锁{unlockFruitName}");
        });

        btnAddItem.onClick.AddListener(() =>
        {
            FarmManager.Instance.AddItem(addItemType, addItemId, addItemAmount);
            Debug.Log($"获得{addItemType}_{addItemId} x{addItemAmount}");
        });

        btnUpgradeFruit.onClick.AddListener(() =>
        {
            FarmManager.Instance.UpgradeFruit(upgradeFruitName);
            Debug.Log($"{upgradeFruitName}升级");
        });
        btnInventory.onClick.AddListener(() =>
        {
            UIManager.Instance.ShowPanel<InventoryPanel>();
        });
    }

}
```

然后我们补充一下测试功能的代码：

``` csharp
//FarmManager

    #region 测试功能
    /// <summary>
    /// 测试功能：添加水
    /// </summary>
    /// <param name="amount">数量</param>
    public void AddWater(int amount)
    {
        playerData.waterCount += amount;
    }
    /// <summary>
    /// 测试功能：解锁水果
    /// </summary>
    /// <param name="fruitName">水果名</param>
    public void UnlockFruit(string fruitName)
    {
        if (playerData.fruitLevels.ContainsKey(fruitName)) return; // 已解锁不重复添加
        playerData.fruitLevels.Add(fruitName, 1);
    }
    /// <summary>
    /// 测试功能：添加道具
    /// </summary>
    /// <param name="type">道具种类</param>
    /// <param name="itemId">道具ID</param>
    /// <param name="amount">道具数量</param>
    public void AddItem(ENUM_ITEM_TYPE type, int itemId, int amount)
    {
        string key = type.ToString() + "_" + itemId;
        if (playerData.inventory.items.ContainsKey(key))
            playerData.inventory.items[key] += amount;
        else
            playerData.inventory.items.Add(key, amount);
    }
    /// <summary>
    /// 测试功能：水果升级
    /// </summary>
    /// <param name="fruitName">水果名</param>
    public void UpgradeFruit(string fruitName)
    {
        if (!playerData.fruitLevels.ContainsKey(fruitName)) return;

        int currentLevel = playerData.fruitLevels[fruitName];
        // 下一级配置存在才能升级
        FruitLevelConfig nextLevel = DataManager.Instance.GetFruitLevelConfig(fruitName, currentLevel + 1);
        if (nextLevel == null) return; // 已经满级

        playerData.fruitLevels[fruitName]++;
    }

    #endregion
```

到这里，我们已经做的差不多齐全了，可以测试一下功能是不是都做的差不多，哪里出现了问题等等。  
然后这里我们会发现一个问题，就是田地的状态没有反馈，你直接看过去的话，是不知道它是什么样的状态的，这样不太好，我们需要加一点视觉效果：

``` csharp
//FieldObject.cs

public void Init(FieldData data)
{
    this.data = data;
    RefreshView();
}

public void RefreshView()
{
    Renderer renderer = GetComponent<Renderer>();
    switch (data.state)
    {
        case ENUM_FIELD_STATE.Empty:
            renderer.material.color = Color.gray;
            break;
        case ENUM_FIELD_STATE.NeedWater:
            renderer.material.color = Color.yellow;
            break;
        case ENUM_FIELD_STATE.Growing:
            renderer.material.color = Color.green;
            break;
        case ENUM_FIELD_STATE.Mature:
            renderer.material.color = Color.red;
            break;
    }
}
```

那么，我们应该怎么在改变状态的时候去触发这个方法呢？答案很简单：事件  
我们创建一个事件系统`EventCenter`，专门用来注册、调用事件

``` csharp
using System.Collections.Generic;
using UnityEngine.Events;

public class EventCenter
{
    private static EventCenter instance = new EventCenter();
    public static EventCenter Instance => instance;

    private Dictionary<string, UnityAction> eventDic = new Dictionary<string, UnityAction>();

    public void AddListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
            eventDic[eventName] += action;
        else
            eventDic[eventName] = action;
    }

    public void RemoveListener(string eventName, UnityAction action)
    {
        if (eventDic.ContainsKey(eventName))
            eventDic[eventName] -= action;
    }

    public void TriggerEvent(string eventName)
    {
        if (eventDic.ContainsKey(eventName))
            eventDic[eventName]?.Invoke();
    }
}
```

然后我们在田地创建时，添加变色的事件，然后在关闭游戏后，删除变色的事件，然后在各个地方调用：

``` csharp
//FieldObject.cs
public void Init(FieldData data)
{
    this.data = data;
    EventCenter.Instance.AddListener("RefreshField_" + data.fieldId, RefreshView);
    RefreshView();
}

private void OnDestroy()
{
    if (data == null) return; 
    EventCenter.Instance.RemoveListener("RefreshField_" + data.fieldId, RefreshView);
}

//调用位置一般为更改状态后
```

然后现在我们就可以来做一个拓展的功能：拖拽功能

## 拖拽相关

对于拖拽操作，实际上是我第一次做，所以这里可能会说的比较详细一些

如果想要实现拖拽，我们需要先继承两个接口：`IBeginDragHandler`, `IDragHandler`  
我们会得到两个方法：其中，OnDrag里面可以不写任何逻辑，只需要实现了就可以了
我们需要写的逻辑都会写在OnBeginDrag里面。

那么首先，我们需要一个拖拽数据类，来存储当前拖拽的是什么，需要携带什么参数传递出去，这是比较重要的。

``` csharp
public enum ENUM_DRAG_TYPE
{
    Plant,      // 种植
    Fertilize,  // 施肥
    Water,      // 浇水
    Harvest     // 收获
}

public class DragData
{
    public string fruitName;      // 种植用
    public int fertilizerId;      // 施肥用
    public ENUM_FIELD_STATE targetState; // 目标地块需要是什么状态
    public ENUM_DRAG_TYPE dragType;
}
```

然后我们在FarmManager中加一行代码：`public DragData currentDragData`来管理当前拖拽的状态（因为拖拽只对田地有效，所以放在了FarmManager内）  
同样，我们需要一个拖拽管理类，来管理拖拽时的逻辑：

``` csharp
using UnityEngine;
using UnityEngine.UI;
public class DragManager : MonoBehaviour
{
    private static DragManager instance;
    public static DragManager Instance => instance;

    // 当前拖拽类型
    public ENUM_DRAG_TYPE dragType;
    public string dragFruitName;
    public int dragFertilizerId;
    public bool isDragging = false;

    public Sprite iconSeed;      // 种子
    public Sprite iconWater;     // 水壶
    public Sprite iconFertilizer; // 肥料
    public Sprite iconHarvest;   // 镰刀

    private Image dragIcon;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        GameObject iconObj = new GameObject("DragIcon");
        iconObj.transform.SetParent(UIManager.Instance.CanvasTrans);
        dragIcon = iconObj.AddComponent<Image>();
        dragIcon.rectTransform.sizeDelta = new Vector2(64, 32); // 设置大小，后续可以改为自定义
        dragIcon.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 图标跟随鼠标
        if (isDragging)
            dragIcon.transform.position = Input.mousePosition;

        // 松手结束拖拽
        if (isDragging && Input.GetMouseButtonUp(0))
            EndDrag();
    }

    public void StartDrag(ENUM_DRAG_TYPE type, string fruitName = "", int fertilizerId = -1)
    {
        isDragging = true;
        dragType = type;
        dragFruitName = fruitName;
        dragFertilizerId = fertilizerId;
        dragIcon.sprite = GetDragIcon(type);
        dragIcon.gameObject.SetActive(true);
    }

    private Sprite GetDragIcon(ENUM_DRAG_TYPE type)
    {
        switch (type)
        {
            case ENUM_DRAG_TYPE.Plant:
                return iconSeed;
            case ENUM_DRAG_TYPE.Fertilize:
                return iconFertilizer;
            case ENUM_DRAG_TYPE.Water:
                return iconWater;
            case ENUM_DRAG_TYPE.Harvest:
                return iconHarvest;
        }
        return null;
    }

    public void EndDrag()
    {
        isDragging = false;
        dragIcon.gameObject.SetActive(false);
    }
}
```

然后，我们需要在可以拖拽的物体的脚本上挂载上面的两个接口，并在其中实现逻辑：

``` csharp
//SelectItem.cs
private ENUM_DRAG_TYPE dragType;
private string fruitName;
private int fertilizerId;

public void OnBeginDrag(PointerEventData eventData)
{
    DragManager.Instance.StartDrag(dragType, fruitName, fertilizerId);
    UIManager.Instance.HidePanel<ChoosePanel>();
    UIManager.Instance.HidePanel<FieldPanel>();
}
```

其他地方类似，只需要在OnBeginDrag里面写StartDrag和其他的逻辑就行  
然后我们需要在`FieldObject`里多添加一些判断：

``` csharp
 private void OnMouseDown()
 {
     if (UIManager.Instance.GetPanel<FieldPanel>() != null) return;
     if (UIManager.Instance.GetPanel<ChoosePanel>() != null) return;
     if (UIManager.Instance.GetPanel<InventoryPanel>() != null) return;
     //TODO:拿到数据,根据状态显示操作
     FieldPanel panel = UIManager.Instance.ShowPanel<FieldPanel>();
     panel.SetField(data);
 }

 private void OnMouseEnter()
 {
     if (!DragManager.Instance.isDragging) return;

     switch (DragManager.Instance.dragType)
     {
         case ENUM_DRAG_TYPE.Plant:
             FarmManager.Instance.Plant(fieldId, DragManager.Instance.dragFruitName);
             break;
         case ENUM_DRAG_TYPE.Water:
             FarmManager.Instance.Water(fieldId);
             break;
         case ENUM_DRAG_TYPE.Fertilize:
             FarmManager.Instance.Fertilize(fieldId, DragManager.Instance.dragFertilizerId);
             break;
         case ENUM_DRAG_TYPE.Harvest:
             FarmManager.Instance.Harvest(fieldId);
             break;
     }
     EventCenter.Instance.TriggerEvent("RefreshField_" + fieldId);
 }
 //其他方法
```

好了，现在所有的东西都基本跑通了，最后我们来做仓库。

## 仓库相关

仓库实际上就是一个UI界面，然后我们按照xxx顺序排序生成不同的格子就可以了。但是这样生成会比较损耗性能，最好的做法还是只在最开始生成一次，后面只读和修改对应格子就可以了。

``` csharp
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPanel : BasePanel
{
    public Button btnFruit;
    public Button btnItem;
    public Button btnXiyouSort;
    public Button btnCountSort;

    public Transform content;
    public GameObject itemSlotPrefab;

    public Text txtName;
    public Text txtLevel;
    public Text txtDesc;

    public Button btnSure;
    public Button btnClose;

    /// <summary>
    /// 0为稀有度，1为数量
    /// </summary>
    private int sortType = 0;

    /// <summary>
    /// 0是水果，1是道具
    /// </summary>
    private int showType = 0;
    public override void Init()
    {
        btnFruit.onClick.AddListener(() => { showType = 0; RefreshList(); });
        btnItem.onClick.AddListener(() => { showType = 1; RefreshList(); });
        btnXiyouSort.onClick.AddListener(() => { sortType = 0; RefreshList(); });
        btnCountSort.onClick.AddListener(() => { sortType = 1; RefreshList(); });
        btnClose.onClick.AddListener(() => UIManager.Instance.HidePanel<InventoryPanel>());
        RefreshList(); // 默认显示水果列表
    }

    /// <summary>
    /// 获取玩家拥有的水果列表并展示在content里
    /// </summary>
    private void ShowFruitList()
    {
        Dictionary<string, int> fruits = FarmManager.Instance.GetFruits(); // 返回 Dictionary<string,int>

        // 转成列表方便排序
        var list = new List<KeyValuePair<string, int>>(fruits);

        if (sortType == 0) // 按稀有度（单价）
            list.Sort((a, b) =>
            {
                // key格式是 "苹果_1"，取名字部分查配置
                string nameA = a.Key.Split('_')[0];
                string nameB = b.Key.Split('_')[0];
                int aLevel = int.Parse(a.Key.Split('_')[1]);
                int bLevel = int.Parse(b.Key.Split('_')[1]);
                int priceA = DataManager.Instance.GetFruitConfig(nameA)?.price ?? 0;
                int priceB = DataManager.Instance.GetFruitConfig(nameB)?.price ?? 0;
                //如果二者名字相同，则按照水果等级降序
                if (nameA == nameB) return bLevel.CompareTo(aLevel);
                return priceB.CompareTo(priceA); // 降序
            });
        else // 按数量
            list.Sort((a, b) => b.Value.CompareTo(a.Value));

        foreach (var item in list)
        {
            string fruitName = item.Key.Split('_')[0];
            int level = int.Parse(item.Key.Split('_')[1]);
            int count = item.Value;

            GameObject slot = Instantiate(itemSlotPrefab, content);
            ItemSlot script = slot.GetComponent<ItemSlot>();
            script.Init(fruitName, count, () =>
            {
                // 点击显示详情
                FruitConfig config = DataManager.Instance.GetFruitConfig(fruitName);
                txtName.text = fruitName;
                txtLevel.text = "Lv" + level;
                txtDesc.text = config?.description ?? "";
            });
        }
    }

    /// <summary>
    /// 同上，展示道具
    /// </summary>
    private void ShowItemList()
    {
        var items = FarmManager.Instance.GetItems();
        var list = new List<KeyValuePair<string, int>>(items);

        if (sortType == 0) // 按稀有度（道具类型顺序）
            list.Sort((a, b) => a.Key.CompareTo(b.Key));
        else // 按数量
            list.Sort((a, b) => b.Value.CompareTo(a.Value));

        foreach (var item in list)
        {
            string[] parts = item.Key.Split('_');
            ENUM_ITEM_TYPE type = (ENUM_ITEM_TYPE)System.Enum.Parse(
                typeof(ENUM_ITEM_TYPE), parts[0]);
            int id = int.Parse(parts[1]);
            int count = item.Value;

            ItemConfig config = DataManager.Instance.GetItemConfig(type, id);
            if (config == null) continue;

            GameObject slot = Instantiate(itemSlotPrefab, content);
            ItemSlot script = slot.GetComponent<ItemSlot>();
            script.Init(config.itemName, count, () =>
            {
                // 点击显示详情
                txtName.text = config.itemName;
                txtLevel.text = config.itemType.ToString();
                txtDesc.text = config.description;
            });
        }
    }

    /// <summary>
    /// 刷新列表
    /// </summary>
    private void RefreshList()
    {
        // 清空旧列表
        foreach (Transform child in content)
            Destroy(child.gameObject);

        // 清空详情
        txtName.text = "";
        txtLevel.text = "";
        txtDesc.text = "";

        if (showType == 0)
            ShowFruitList();
        else
            ShowItemList();
    }
}
```

完结。