---
title: Unity复习学习随笔（六）：XML数据
date: 2025-12-14
categories: 编程笔记
tags:
  - Unity
---

## XML是什么

全称：**可拓展标记语言（Extensible Markup Language）**  
XML是国际通用的  
它是被设计来用于传输和存储数据的一种文本特殊格式，文件后缀一般为.xml

## XML基本语法

### 固定内容

固定语法：

```undefined
<?xml version="1.0" encoding="UTF-8"?>
```

version代表版本        encoding代表编码格式  
所谓编码格式，就是读取文件时，解析字符串使用的编码是什么

**编码格式**：

不同的字符，在内存中的二进制是不一样的，每一个字符对应一个数字  
不同的编码格式，字符对应的二进制是不一样的

xml的基本语法就是**<元素标签></元素标签>**配对出现的

注：一定必须有一个根节点  
节点之间可以填写数据，或者在其中包裹别的节点

### 基本规则

每个元素都必须有关闭标签  
元素命名规则基本遵照c#中变量名命名规则  
XML标签对大小写敏感  
XML文件必须有根元素  
特殊的符号应该用实体引用：  
        < —— <小于  
        > —— >大于  
        & —— &和号  
        &apos —— ' 单引号  
        " —— “ 引号

### XML属性

属性就是在元素标签后面空格 添加的内容  
注：属性必须用引号包裹，单引号双引号都可以

如果使用属性记录信息，不想使用元素记录，可以像下面一样写

```undefined
<People name = "father" age = '50' />
```

**属性和元素节点的区别**：

属性和元素节点只是写法上的区别而已，我们可以选择自己喜欢的方式来记录数据

**XML文件存放位置**

1.只读不写的XML文件，可以放在Resources或者StreamingAssets文件夹下  
2.动态存储的XML文件，可以放在Application.persistentDataPath路径下

### XML读取

C#读取XML的方法有几种  
1：XmlDocument（把数据加载到内存中，方便读取）  
2：XmlTextReader（以流形式加载，内存占用更少，但是是单向只读，使用不是特别方便，除非有特殊需求，否则不会使用）  
3：Linq（等以后涉及到Linq时再详细说明）

使用XmlDocument类读取是比较方便容易理解何操作的方法

#### 读取Xml信息

1.直接根据xml字符串内容，来加载xml文件  
存放在Resources文件夹下的xml文件加载处理

```cs
XmlDocument xml = new XmlDocument();
TextAsset asset = Resources.Load<TextAsset>("TestXml");
xml.LoadXml(asset.text);
```

2.通过xml文件的路径去进行加载

```cs
xml.Load(Application.streamingAssetsPath + "/TestXml.xml");
```

#### 读取元素和属性信息

节点信息类：XmlNode 单个节点信息类  
节点列表信息：XmlNodeList 多个节点信息类

获取xml当中的根节点：

```cs
XmlNode root = xml.SelectSingleNode("Root");
```

再通过根节点，去获取下面的子节点：

```cs
XmlNode nodeName = root.SelectSingleNode("name");
```

如果想要获取节点包裹的元素信息，直接 .InnerText

```cs
print(nodeName.InnerText);
```

获取属性：

```cs
XmlNode nodeImte = root.SelectSingleNode("Item");
print(nodeItem.Attributes["id"].Value);
print(nodeItem.Attributes["num"].Value);
```

第二种获取属性方式：

```cs
print(nodeItem.Attributes.GetNamedItem("id").Value);
```

如果一个节点下有同名节点：

```cs
XmlNodeList friendList = root.SelectNodes("Friend");
```

遍历方式：

迭代器：

```cs
foreach(XmlNode item in friendList)
{
  print(item.selectSingleNode("name").InnerText);
}
```

通过for循环：

```cs
for(int i = 0; i< friendList.Count;i++)
{
  print(friendList[i].SelectSingleNode("name").InnerText);
}
```

**练习**：

{% asset_img 1.png %}

#### C#存储修改Xml文件

##### 1.决定存储在哪个文件夹下

注意：存储xml文件，在Unity中一定时使用各平台都可读可写可找到的路径  
1.Resources：可读不可写，打包后找不到  
2.Application.streamingAssetsPath 可读，pc端可写，找得到  
3.Application.dataPath 打包后找不到  
4.Application.persistentDataPath 可读可写找的到

```cs
string path = Application.persistentDatapath + "/PlayerInfo.xml";
print(Application.persistentDatapath);
```

##### 2.存储xml文件

关键类 XmlDocument 用于创建节点，存储文件  
关键类 XmlDeclaration 用于添加版本信息  
关键类 XmlElement 节点类

存储有5步：  
1.创建文本对象：

```cs
XmlDocument xml = new XmlDocument();
```

2.添加固定版本信息：

```cs
XmlDeclaration xmlDec = xml.CreateXmlDeclaration("1.0","UTF-8","");
xml.AppendChild(xmlDec);
```

3.添加根节点：

```cs
XmlElement root = xml.CreateElement("Root");
xml.AppendChild(root);
```

4.为根节点添加子节点：

```cs
XmlElement name = xml.CreateElement("name");
name.InnerText = "name";
root.AppendChild(name);
XmlElement atk = xml.CreateElement("atk");
atk.InnerText = "10";
root.AppendChild(atk);
XmlElement listInt = xml.CreateElement("listInt");
for(int i = 1;i<=3;i++)
{
  XmlElement childNode = xml.CreateElement("int");
  childNode.InnetText = i.ToString();
  listInt.AppendChild(childNode);
}
root.AppendChild(listInt);
XmlElement itemList = xml.CreateElement("itemList");
for(int i = 1;i<=3;i++)
{
  XmlElement childNode = xml.CreateElement("Item");
  childNode.SetAttribute("id",i.ToString);
  childNode.SetAttribute("num",(i * 10).ToString);
  itemList.AppendChild(childNode);
}
root.AppendChild(itemList);
```

5.保存

```cs
xml.Save(path);
```

##### 3.修改xml文件

1.判断是否存在文件

```cs
if(File.Exists(path))
{
}
```

2.加载后，直接添加节点，移除节点即可

```cs
if(File.Exists(path))
{
  XmlDocument newXml = new XmlDocument();
  newXml.Load(path);
  XmlNode node = newXml.SelectSingleNode("Root").SelectSingleNode("atk");
  node = newXml.SelectSingleNode("Root/atk");
  XmlNode root2 = newXml.SelectSingleNode("Root");
  root2.RemoveChild(node);
  XmlElement speed = newXml.CreateElement("speed");
  speed.InnerText = "20";
  root2.AppendChild(speed);
  newXml.Save(path);
}
```

**练习**：

{% asset_img 2.png %}

### XML主要用处

网络游戏：可以用于存储一些客户端的简单不重要数据  
可以用于传输信息（基本不会大范围使用，因为比较耗流量）

单机游戏：用于存储游戏相关数据  
用于配置游戏数据

### XML序列化

#### 什么是序列化和反序列化

序列化：把对象转化为可传输的字节序列过程称为序列化

反序列化：把字节序列还原为对象的过程称为反序列化

#### xml序列化流程

1.准备一个数据结构类

```cs
public class Test1
{
  public int testPublic = 10;
  private int testPrivate = 11;
  protected int testProtected = 12;
  internal int testInternal = 13;
  public string testPublicStr = "123";
  public int testPro{get;set;}
  public Test2 testClass = new Test2();
}
public class Test2
{
  public int test1 = 1;
  public float test2 = 1.1f;
  public bool test3 = true;
}
public class Test
{
  void Start()
  {
    Test1 t = new Test1();
  }
}
```

2.进行序列化

第一步：确定存储路径

```cs
public class Test
{
  void Start()
  {
    string path = Application.persistentDataPath + "/Test.xml";
  }
}
```

第二步：结合 using知识点 和 StreamWriter这个流对象 来写入文件

括号内的代码：写入一个文件流，如果有该文件，直接打开并修改，如果没有该文件，直接新建一个文件  
using的新用法：括号当中包裹的声明对象，会在大括号语句块结束后自动释放掉  
当语句块结束，会自动帮助我们调用对象的Dispose这个方法，让其进行销毁  
using一般都是配合内存占用比较大，或者有读写操作时，进行使用的

```cs
public class Test
{
  void Start()
  {
    using(StreamWriter stream = new StreamWriter(path))
    {
    }
  }
}
```

第三步：进行xml文件序列化

第一个参数：文件流对象  
第二个参数：想要被翻译的对象  
注意：翻译机器的类型 一定要和传入的对象是一致的，不然会报错

```cs
public class Test
{
  void Start()
  {
    using(StreamWriter stream = new StreamWriter(path))
    {
      XmlSerializer s = new XmlSerializer(typeof(Test));
      s.Serialize(stream,t);
    }
  }
}
```

关键知识点：  
        XmlSerializer 用于序列化对象为xml的关键类  
        StreamWriter 用于存储文件  
        using 用于方便流对象释放和销毁

**这种序列化方式不支持字典！！并且只能序列化公共成员**

#### 自定义节点名或设置属性

XmlAttribute特性：变成属性格式，可以传入参数：别名

XmlElement特性：自定义节点名

XmlArrayItem特性：更改子节点名字

```cs
public class Test2
{
  [XmlAttribute(Test1)]
  public int test1 = 1;
  [XmlElement("Test222222")]
  public float test2 = 1.1f;
  [XmlAttribute()]
  public bool test3 = true;
}
```

### XML反序列化

1.判断文件是否存在

```cs
string path = Application.persistentDataPath + "/Test.xml";
if(File.Exists(path))
{
}
```

2.反序列化

关键知识：  
1.using 和StreamReader  
2.XmlSerializer 的 Deserialize反序列化方法

读取文件：

```cs
string path = Application.persistentDataPath + "/Test.xml";
if(File.Exists(path))
{
  using (StreamReader reader = new StreamReader(path))
  {
    XmlSerializer s = new XmlSerializer(typeof(Test));
    Test t = s.Deserialize(render) as Test;
  }
}
```

注意：List对象如果有默认值，反序列化时不会清空，会往后面添加

### IXmlSerializable接口

#### IXmlSerializable是什么？

C#的XmlSerializer 提供了可拓展内容，可以让一些不能被序列化和反序列化的特殊类能被处理  
让特殊类继承 IXmleSerializable 接口，实现其中的方法即可

自定义类实践：

```cs
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using UnityEngine;
public class Test1 : IXmlSerializable
{
  public int test1;
  public string test2;
  public XmlSchema GetSchema()
  {
    return null;
  }
  public void ReadXml(XmlReader reader)
  {
    XmlSerializer s = new XmlSerializer(typeof(int));
    reader.ReadStartElement("test1");
    this.test1 = (int)s.Deserialize(reader);
    reader.ReadEndElement();
    s = new XmlSerializer(typeof(string));
    reader.ReadStartElement("test2");
    this.test2 = (string)s.Deserialize(reader);
    reader.ReadEndElement();
  }
  public void WriteXml(XmlWriter writer)
  {
    XmlSerializer s = new XmlSerializer(typeof(int));
    writer.WriteStartElement("test1");
    s.Serialize(writer, this.test1);
    writer.WriteEndElement();
    writer.WriteStartElement("test2");
    s = new XmlSerializer(typeof(string));
    s.Serialize(writer, this.test2);
    writer.WriteEndElement();
  }
}
public class Test : MonoBehaviour
{
  private void Start()
  {
    Test1 t = new Test1();
    t.test2 = "123";
    string path = Application.persistentDataPath + "/test1.xml";
    using (StreamWriter writer = new StreamWriter(path))
    {
      XmlSerializer s = new XmlSerializer(typeof(Test));
      s.Serialize(writer, t);
    }
  }
}
```

在序列化时，如果对象中的引用成员为空，那么xml里面是看不到该字段的

### 如何让Dictionary支持序列化反序列化

我们没有办法修改C#自带的类，所以我们可以**重写一个类，用来继承Dictionary**，然后让这个类**继承序列化拓展接口IXmlSerializable**，最后实现里面的序列化和反序列化方法即可

```cs
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
public class SerializerDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
{
  public XmlSchema GetSchema()
  {
    return null;
  }
  public void ReadXml(XmlReader reader)
  {
    XmlSerializer keySer = new XmlSerializer(typeof(TKey));
    XmlSerializer valueSer = new XmlSerializer(typeof(TValue));
    reader.Read();
    while (reader.NodeType != XmlNodeType.EndElement)
    {
      TKey key = (TKey)keySer.Deserialize(reader);
      TValue value = (TValue)valueSer.Deserialize(reader);
      this.Add(key, value);
    }
    reader.Read();
  }
  public void WriteXml(XmlWriter writer)
  {
    XmlSerializer keySer = new XmlSerializer(typeof(TKey));
    XmlSerializer valueSer = new XmlSerializer(typeof(TValue));
    foreach (KeyValuePair<TKey, TValue> kv in this)
    {
      keySer.Serialize(writer, kv.Key);
      valueSer.Serialize(writer, kv.Value);
    }
  }
}
```

## 创建一个属于自己的XML文件管理类

目标：提供公共的序列化和反序列化方法给外部，方便外部存储和获取数据

数据管理类的准备：

```cs
using System;
public class XmlDataManager
{
  private static XmlDataManager instance;
  public static XmlDataManager Instance { get; } = new XmlDataManager();
  private XmlDataManager() { }
  public void SaveData(object data,string fileName)
  {
  }
  public object LoadData(Type type,string fileName)
  {
    return null;
  }
}
```

SaveData：

```cs
public void SaveData(object data, string fileName)
{
  string path = Application.persistentDataPath + "/" + fileName + ".xml";
  using (StreamWriter writer = new StreamWriter(path))
  {
    XmlSerializer s = new XmlSerializer(data.GetType());
    s.Serialize(writer, data);
  }
}
```

LoadData：

```cs
public object LoadData(Type type, string fileName)
{
  string path = Application.persistentDataPath + "/" + fileName + ".xml";
  if (!File.Exists(path))
  {
    path = Application.streamingAssetsPath + "/" + fileName + ".xml";
    if(!File.Exists(path))
      return Activator.CreateInstance(type);
  }
  using (StreamReader reader = new StreamReader(path))
  {
    XmlSerializer s = new XmlSerializer(type);
    return s.Deserialize(reader);
  }
}
```