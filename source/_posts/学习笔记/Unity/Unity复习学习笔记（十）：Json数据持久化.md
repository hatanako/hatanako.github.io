---
title: Unity复习学习笔记（十）：Json数据持久化
date: 2026-01-22
categories: 编程笔记
tags:
  - Unity
---

Json：JavaScript对象简谱（JavaScript Object Notation）  
json是国际通用的一种轻量级的数据交换格式  
主要在网络通讯中用于传输数据，或本地数据存储和读取  
易于人阅读和编写，同时也易于机器解析和生成，并有效的提升网络传输效率

Json文档就是 使用 Json格式配置填写的文档  
后缀一般为.json  
在游戏中可以把游戏数据按照Json格式标准存储在Json文档中  
再将Json文档存储在硬盘上或者传输给远端  
达到数据持久化或者数据传输的目的

## Json和Xml的异同

### 共同点

1.都是纯文本  
2.都有层级结构  
3.都具有描述性

#### 不同点

1.Json配置更简单  
2.Json在某些情况下读写更加迅速

## Json文件格式

### Json基本语法

#### 用什么编辑？

只要能打开文档的软件都能打开Json文件  
常用的一些编辑Json文件的方式  
1.系统自带——记事本、写字板  
2.通用文本编辑器——Sublime Text等  
3.网页上的Json编辑器

#### 注释

和C#中注释方式一致  
1.双斜杠：//注释内容  
2.斜杠加星号 /\*注释内容\*/

#### 语法结构

Json格式是一种键值对结构

{% asset_img 1.png %}

符号含义：

大括号{} —— 对象  
中括号\[\] —— 数组  
冒号:     —— 键值对对应关系  
逗号， —— 数据 分割  
双引号 “” —— 键名/字符串

键值对表示：  
“键名”：值内容

值类型：  
数字（整数或浮点）、字符串、true/false、数组、对象、null

#### 配置Json文档时的注意事项

1.如果数据表示对象那么最外层有大括号  
2.一定是键值对形式  
3.键一定是字符串格式  
4.键值对用逗号分开  
5.数组用\[\]包裹  
6.对象用{}包裹

### Excel转Json

1.在网页上搜索Excel转Json  
2.选择在线转换的网站  
3.进行转换  
4.保存Json格式的数据

## C#读取存储Json文件

### Jsonutility

JsonUtility是Unity自带的用于解析Json的公共类  
它可以：  
1.将内存中对象序列化为Json格式的字符串  
2.将Json字符串反序列化为类对象

#### 在文件中存读字符串

##### 存储

第一个参数必须是存在的文件路径，如果没有对应文件夹会报错

```cs
File.WriteAllText(Application.persistentDataPath + "/Test.json","存储内容");
```

##### 读取

```cs
string str = File.ReadAllText(Applicaton.persistentDataPath + "/Test.json");
```

#### 序列化

把内存中的数据存储到硬盘中  
方法：JsonUtility.ToJson(对象);

注意：  
1.float序列化时看起来会有一些误差  
2.自定义类需要加上序列化特性\[System.Serializable\]  
3.想要序列化私有变量，需要加上特性\[SerializeField\]  
4.JsonUtility不支持字典  
5.JsonUtility存储null对象不会是null，而是默认值的数据

#### 反序列化

把硬盘上的数据读取到内存中

方法：JsonUtility.FromJson(字符串)

注意：如果Json中数据少了，读取到内存中类对象中时不会报错

注意事项  
1.JsonUtility无法直接读取数据集合  
2.文本编码格式需要UTF-8，不然无法加载

#### 总结

File存读字符串的方法 ReadAllText 和 WriteAllText  
JsonUtility提供的序列化和反序列化的方法 ToJson 和 FromJson  
自定义类需要加上序列化特性\[System.Serializable\]  
私有保护成员需要加上\[SerializeField\]  
JsonUtility不支持字典  
JsonUtility不能直接把数据反序列化为数据集合  
Json文档编码格式必须是UTF-8

### Litjson

它是一个第三方库，用于处理Json的序列化和反序列化  
LitJson是C#编写，体积小、速度快、易于使用  
它可以很容易的嵌入到我们的代码中  
只需要将LitJson代码拷贝到工程中即可

#### 如何获取LitJson

1.前往LitJson官网  
2.通过官网前往GitHub获取最新版本代码  
3.将代码拷贝到Unity工程中，即可开始使用LitJson

#### 如何序列化？

方法：JsonMapper.ToJson(对象);

注意：  
1.相对JsonUtility不需要加特性  
2.不能序列化私有变量  
3.支持字典类型，字典的键建议都是字符串，因为Json的特点，Json中的键会加上双引号  
4.需要引用LitJson命名空间  
5.LitJson可以准确的保存null类型

#### 反序列化

方法：JsonMapper.ToObject(字符串)  
可以使用ToObject的泛型转换可以更加的方便快捷：  
JsonMapper.ToObject<T>(字符串)

注意：  
1.类结构需要无参构造函数，否则反序列化时报错  
2.字典虽然支持，但是键在使用为数值时会有问题，需要使用字符串类型

#### 注意事项

1.LitJson可以直接读取数据集合  
2.文本编码格式需要是UTF-8，不然无法加载

#### 总结

1.LitJson提供的序列化反序列化方法，JsonMapper.ToJson和ToObject<>  
2.LitJson无需加特性  
3.LitJson不支持私有变量  
4.LitJson支持字典序列化反序列化  
5.LitJson可以直接将数据反序列化为数据集合  
6.LitJson反序列化时，自定义类型需要无参构造  
7.Json文档编码格式必须是utf-8

### 二者对比

#### 相同点

1.他们都是用于Json的序列化反序列化  
2.Json文档编码格式必须是UTF-8  
3.都是通过静态类精选方法调用

#### 不同点

1.JsonUtility是Unity自带，LitJson是第三方需要引用命名空间  
2.JsonUtility使用自定义类需要加特性，LitJson不需要  
3.JsonUtility支持私有变量（加特性），LitJson不支持  
4.JsonUtility不支持字典，LitJson支持（但是键只能是字符串）  
5.JsonUtility不能直接将数据反序列化为数据集合（数组字典），LitJson可以  
6.JsonUtility对自定义类不要求有无参构造，LitJson需要  
7.JsonUtility存储空对象时会存储默认值，LitJson会存null

#### 如何选择

根据实际需求，建议使用LitJson  
原因：LitJson不用加特性，支持字典，支持直接反序列化为数据集合，存储null更准确

## 自己封装一个简单的Json存储工具类

先创建一个类：

```cs
public class JsonManager
{
  private static JsonManager instance = new JsonManager();
  public static JsonManager Instance { get { return instance; } }
  private JsonManager()
  { }
}
```

因为实际上有多种工具类来提供序列化和反序列化选择，所以我们需要创建一个枚举类型来供给选择，提升代码的泛用性

```cs
public enum JsonType
{
  JsonUtility,
  LitJson
}
```

序列化：

```cs
public void SaveData(object data,string fileName,JsonType type = JsonType.LitJson)
{
  string path = Application.persistentDataPath + "/" + fileName + ".json";
  string jsonData = "";
  switch (type)
  {
    case JsonType.JsonUtility:
      jsonData = JsonUtility.ToJson(data, true);
      break;
    case JsonType.LitJson:
      jsonData = JsonMapper.ToJson(data);
      break;
  }
  File.WriteAllText(path, jsonData);
}
```

反序列化：

```cs
public T LoadData<T>(string fileName, JsonType type = JsonType.LitJson) where T : new()
{
  string path = Application.streamingAssetsPath + "/" + fileName + ".json";
  if(!File.Exists(path))
    path = Application.persistentDataPath + "/" + fileName + ".json";
  if (!File.Exists(path))
    return new T();
  string jsonData = File.ReadAllText(path);
  T data = default;
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
```