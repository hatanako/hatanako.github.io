---
title: Unity复习学习随笔（四）：数据持久化PlayerPrefs
date: 2025-12-06
categories: 编程笔记
tags:
  - Unity
---

## 什么是数据持久化？

**数据持久化**就是将内存中的数据 模型 转换为存储模型，以及将存储模型转换为内存中的数据模型的统称

简单来说就是将游戏数据存储到硬盘，硬盘中数据读取到游戏中，也就是传统意义上的存盘

## PlayerPrefs的基本方法

### 1.PlayerPrefs是什么

**PlayerPrefs** 是Unity提供的可以用于存储读取玩家数据的公共 类

### 2.存储相关

**PlayerPrefs**的数据存储类似于键值对存储，一个键对应一个值  
提供了存储3种数据的方法：int、float、 string  
键：string类型  
值：int、float、string对应3种API

```cs
PlayerPrefs.SetInt("Age",18);
PlayerPrefs.SetFloat("Height",175.5f);
PlayerPrefs.SetString("Name","123");
```

直接调用Set相关方法，只会把数据存到内存里，当游戏结束时，Unity会自动把数据存到硬盘中  
如果游戏不是正常结束的，而是崩溃，数据是不会存到硬盘中的  
只要调用下面的方法就会马上存储到硬盘中

```cs
PlayerPrefs.Save();
```

PlayerPrefs是有局限性的，它只能存3种类型的数据  
如果想要存储别的类型的数据，只能降低精度，或者上升精度来进行存储

如果不同类型 使用 同一键名进行存储，会进行覆盖

### 3.读取相关

注意：运行时，只要你Set了对应键值对，即使没有存储到本地，也能够读取出 信息

get中还存在有默认值作为第二个参数，如果没有找到对应的值，就会返回函数的第二个参数  
可以用它来进行基础数据的初始化处理

```cs
int age = PlayerPrefs.GetInt("Age");
float height = PlayerPrefs.GetFloat("Height");
string name = PlayerPrefs.GetString("Name");
```

**判断数据是否存在**：

```cs
if(PlayerPrefs.HasKey("myName"))
{
  print("存在对应的键值对数据");
}
```

### 4.删除数据

删除指定键值对

```cs
PlayerPrefs.DeleteKey("Age");
```

删除所有存储信息

```cs
PlayerPrefs.DeleteAll();
```

**练习**：

{% asset_img 1.png %}

### 不同平台的存储位置

### Windows

PlayerPrefs存储在 HKCU\\Software\\\[公司名称\]\\\[产品名称\] 项下的注册表中  
其中公司和产品名称是在“Project Settings”中设置的名称

查看流程：  
运行 regedit ——>HKEY\_CURRENT\_USER——>SOFTWARE——>Unity——>UnityEditor——>公司名称——>产品名称

### Android

/data/data/包名/shared\_Prefs/pkg\_game.xml

### IOS

/Library/Preference/\[应用ID\].plist

## PlayerPrefs数据的唯一性

PlayerPrefs中不同数据的唯一性是由Key决定的，不同的Key决定了不同的数据，同一个项目中  
如果不同数据Key相同，就会造成数据丢失，要保证数据不丢失就要建立一个保证Key唯一的规则

**练习**：

{% asset_img 2.png %}

## 如何制作一个自己的PlayerPrefs包

### 1.反射的补充

知识回顾：

> Type：用于获取类的所有信息、字段、属性、方法等等  
> Assembly：用于获取程序集，通过程序集获取Type  
> Activator：用于快速实例化对象

判断一个类型的对象是否可以让另一个类型为自己分配空间：父类装子类

```cs
Type fatherType = typeof(Father);
Type childType = typeof(Child);
if(fatherType.IsAssignableFrom(sonType))
{
  print("可以装");
  Father f = Activator.CreateInstance(sonType) as Father;
}
```

通过反射获取泛型类型：

```cs
List<string> list = new List<string>();
Type listType = list.GetType();
Type[] types = listType.GetGenericArguments();
```

### 2.需求分析

对于每一个数据类，都存在有两个方法，Save和Load，并且数据类中的所有字段都是通过PlayerPrefs进行存取的。  
那么我们完全可以把它们提取出来，通过一个类来进行整体的Save和Load，即一个**数据管理类**

### 3.搭建基础架构

```cs
public class PlayerPrefsDataManager
{
  private static PlayerPrefsDataManager instance = new PlayerPrefsDataManager();
  public static PlayerPrefsDataManager Instance { get { return instance; } }
  private PlayerPrefsDataManager() { }
  public void SaveData( object data, string keyName )
  {
  }
  public object LoadData( Type type, string keyName )
  {
    return null;
  }
}
```

### 4.存储

#### 1.存储常用成员

```cs
public void SaveData( object data, string keyName )
{
  Type type = data.GetType();
  FieldInfo[] fields = type.GetFields();
  string saveKeyName = "";
  foreach (FieldInfo field in fields)
  {
    saveKeyName = keyName + type.Name + "_" + field.FieldType.Name + "_" + field.Name;
    SaveValue(field.GetValue(data), saveKeyName);
  }
}
private void SaveValue(object value,string key)
{
  Type type = value.GetType();
  if (type == typeof(int))
  {
    PlayerPrefs.SetInt(key, (int)value);
  }
  else if (type == typeof(float))
  {
    PlayerPrefs.SetFloat(key, (float)value);
  }
  else if (type == typeof(string))
  {
    PlayerPrefs.SetString(key, (string)value);
  }
  else if (type == typeof(bool))
  {
    PlayerPrefs.SetInt(key, (bool)value ? 1 : 0);
  }
  else
  {
    Debug.LogError("Unsupported data type: " + type.Name);
  }
}
```

#### 2.存储List

```cs
private void SaveValue(object value,string key)
{
  else if (typeof(IList).IsAssignableFrom(type))
  {
    IList list = value as IList;
    PlayerPrefs.SetInt(key + "_Count", list.Count);
    for (int i = 0; i < list.Count; i++)
    {
      SaveValue(list[i], key + "_" + i);
    }
  }
}
```

#### 3.存储Dictionary

和List存储非常相似

```cs
private void SaveValue(object value,string key)
{
  else if (typeof(IDictionary).IsAssignableFrom(type))
  {
    IDictionary dict = value as IDictionary;
    PlayerPrefs.SetInt(key + "_Count", dict.Count);
    int index = 0;
    foreach (var k in dict.Keys)
    {
      SaveValue(k, key + "_Key_" + index);
      SaveValue(dict[k], key + "_Value_" + index);
      index++;
    }
  }
}
```

#### 4.存储自定义类

其余类型不再描述，如果需要自行拓展

存储的完整代码：

```cs
private void SaveValue(object value,string key)
{
  Type type = value.GetType();
  if (type == typeof(int))
  {
    PlayerPrefs.SetInt(key, (int)value);
  }
  else if (type == typeof(float))
  {
    PlayerPrefs.SetFloat(key, (float)value);
  }
  else if (type == typeof(string))
  {
    PlayerPrefs.SetString(key, (string)value);
  }
  else if (type == typeof(bool))
  {
    PlayerPrefs.SetInt(key, (bool)value ? 1 : 0);
  }
  else if (typeof(IList).IsAssignableFrom(type))
  {
    IList list = value as IList;
    PlayerPrefs.SetInt(key + "_Count", list.Count);
    for (int i = 0; i < list.Count; i++)
    {
      SaveValue(list[i], key + "_" + i);
    }
  }
  else if (typeof(IDictionary).IsAssignableFrom(type))
  {
    IDictionary dict = value as IDictionary;
    PlayerPrefs.SetInt(key + "_Count", dict.Count);
    int index = 0;
    foreach (var k in dict.Keys)
    {
      SaveValue(k, key + "_Key_" + index);
      SaveValue(dict[k], key + "_Value_" + index);
      index++;
    }
  }
  else
  {
    SaveData(value, key);
  }
}
```

### 5.读取

#### 1.读取常用成员

```cs
public object LoadData( Type type, string keyName )
{
  object data = Activator.CreateInstance(type);
  FieldInfo[] fields = type.GetFields();
  string loadKeyName = "";
  foreach(FieldInfo field in fields)
  {
    loadKeyName = keyName + type.Name + "_" + field.FieldType.Name + "_" + field.Name;
    field.SetValue(data, LoadValue(field.FieldType, loadKeyName));
  }
  return data;
}
private object LoadValue(Type fieldType, string loadKeyName)
{
  if (fieldType == typeof(int))
  {
    return PlayerPrefs.GetInt(loadKeyName);
  }
  else if (fieldType == typeof(float))
  {
    return PlayerPrefs.GetFloat(loadKeyName);
  }
  else if (fieldType == typeof(string))
  {
    return PlayerPrefs.GetString(loadKeyName);
  }
  else if (fieldType == typeof(bool))
  {
    return PlayerPrefs.GetInt(loadKeyName) == 1 ? true : false;
  }
  else
  {
    return LoadData(fieldType, loadKeyName);
  }
}
```

#### 2.读取List

```cs
private object LoadValue(Type fieldType, string loadKeyName)
{
  else if (typeof(IList).IsAssignableFrom(fieldType))
  {
    IList list = Activator.CreateInstance(fieldType) as IList;
    int count = PlayerPrefs.GetInt(loadKeyName + "_Count",0);
    Type elementType = fieldType.IsGenericType ? fieldType.GetGenericArguments()[0] : typeof(object);
    for (int i = 0; i < count; i++)
    {
      list.Add(LoadValue(elementType, loadKeyName + "_" + i));
    }
    return list;
  }
}
```

#### 3.读取字典

```cs
private object LoadValue(Type fieldType, string loadKeyName)
{
  else if (typeof(IDictionary).IsAssignableFrom(fieldType))
  {
    IDictionary dict = Activator.CreateInstance(fieldType) as IDictionary;
    int count = PlayerPrefs.GetInt(loadKeyName + "_Count");
    Type[] genericArgs = fieldType.GetGenericArguments();
    Type keyType = genericArgs[0];
    Type valueType = genericArgs[1];
    for (int i = 0; i < count; i++)
    {
      object key = LoadValue(keyType, loadKeyName + "_Key_" + i);
      object value = LoadValue(valueType, loadKeyName + "_Value_" + i);
      dict.Add(key, value);
    }
    return dict;
  }
}
```

#### 4.读取自定义类

同上存储，直接提供完整代码：

```cs
private object LoadValue(Type fieldType, string loadKeyName)
{
  if (fieldType == typeof(int))
  {
    return PlayerPrefs.GetInt(loadKeyName);
  }
  else if (fieldType == typeof(float))
  {
    return PlayerPrefs.GetFloat(loadKeyName);
  }
  else if (fieldType == typeof(string))
  {
    return PlayerPrefs.GetString(loadKeyName);
  }
  else if (fieldType == typeof(bool))
  {
    return PlayerPrefs.GetInt(loadKeyName) == 1 ? true : false;
  }
  else if (typeof(IList).IsAssignableFrom(fieldType))
  {
    IList list = Activator.CreateInstance(fieldType) as IList;
    int count = PlayerPrefs.GetInt(loadKeyName + "_Count",0);
    Type elementType = fieldType.IsGenericType ? fieldType.GetGenericArguments()[0] : typeof(object);
    for (int i = 0; i < count; i++)
    {
      list.Add(LoadValue(elementType, loadKeyName + "_" + i));
    }
    return list;
  }
  else if (typeof(IDictionary).IsAssignableFrom(fieldType))
  {
    IDictionary dict = Activator.CreateInstance(fieldType) as IDictionary;
    int count = PlayerPrefs.GetInt(loadKeyName + "_Count");
    Type[] genericArgs = fieldType.GetGenericArguments();
    Type keyType = genericArgs[0];
    Type valueType = genericArgs[1];
    for (int i = 0; i < count; i++)
    {
      object key = LoadValue(keyType, loadKeyName + "_Key_" + i);
      object value = LoadValue(valueType, loadKeyName + "_Value_" + i);
      dict.Add(key, value);
    }
    return dict;
  }
  else
  {
    return LoadData(fieldType, loadKeyName);
  }
}
```

## 加密思路

1.**找不到**：  
把存在硬盘上的内容放在一个不容易找到的地方，多层文件夹包裹，名字辨识度低  
但是对于PlayerPrefs不太适用，因为位置已经固定了，更改不了

2.**看不懂**：让数据的Key和Value让别人看不懂，俗称加密，为Key和Value加密

3.**解不出**：不让别人获取到加密规则，就解不出来了

注：游戏加密只是提高了修改数据的门槛，只要别人获取到了源代码，知道了加密规则就没有任何意义了。但是对于一般玩家来说是几乎不可能的事情。