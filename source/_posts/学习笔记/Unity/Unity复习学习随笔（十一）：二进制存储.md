---
title: Unity复习学习随笔（十一）：二进制存储
date: 2026-02-02
categories: 编程笔记
tags:
  - Unity
---

## 什么是数据持久化？

**数据持久化**就是将 内存 中的数据模型转换为存储模型，以及将存储模型转换为内存中的数据模型的统称

简单来说就是将游戏数据存储到硬盘，硬盘中数据读取到游戏中，也就是传统意义上的**存盘**

## 二进制是什么？

**二进制**是计算技术中广泛采用的一种数制。二进制数据是由0和1两个数码来表示的数。  
它的基数为2，进位规则是“逢二进一”。

计算机中存储的数据本质上都是二进制的存储，在计算机中，位（bit）是最小的存储单位。  
1位就是一个0或者一个1

也就是说，一个文件的数据本质上都是由n个0和1组合而成的， 通过不同的解析规则最终呈现在我们的眼前。

### 学习二进制读写数据的原因

前面学习的**xml和json**，都是用特定的字符串组合规则来读写数据的。

**清晰易懂**是他们的共同好处，但是也是一把双刃剑，如果我们用xml和json存储数据，如果不进行加密，那么只要玩家找到对应的存储 信息 ，就能够快速修改其中的内容。

而且由于他们把数据转换成了对应的xml或者json字符串，我们最终在存储数据时存储的都是字符串数据，在读写时效率较低，内存和硬盘空间占用较大

即：xml和json的安全性和效率较低

二进制的好处：

1.安全性较高  
2.效率较高  
3.为**网络通信**做铺垫

## 各类型数据转字节数据

### 回顾

#### 不同变量类型

-   有符号：sbyte int short long
-   无符号：byte uint ushort ulong
-   浮点：float double decimal
-   特殊：bool char string

#### 变量的本质

变量的本质就是二进制，它们在内存中都以字节的形式存储着  
1byte = 8bit        1bit不是0就是1        通过sizeof方法可以看到常用的变量类型占用的字节空间长度

### 二进制文件读写的本质

通过将各类型变量转换为字节数组，将字节数组直接存储到文件中  
不仅可以节约存储空间，提升效率，还可以提升安全性  
在网络通信中也是 使用 字节数据进行传输的

### 各类型数据和字节数据相互转换

C#提供了一个公共类帮助我们进行转换：BitConverter  
我们只需记住API即可  
命名空间：System

#### 1.将各类型转换为字节

注意：decimal类型和string类型不能直接进行转换

```cs
byte[] bytes = BitConverter.GetBytes(99);
```

#### 2.将字节数组转换为各个类型

注：第二个参数为该字节的第几个索引开始

```cs
int i = BitConverter.ToInt32(bytes, 0);
```

### 标准编码格式

编码是用预先规定的方法将文字、数字或其他对象编成数码，或将信息、数据转换成规定的电脉冲信号。  
为保证编码的正确性，编码要规范化、标准化，即需有标准的编码格式。  
常见的编码格式有ASCII、ANSI、GBK、GB2312、UTF-8、Unicode等等

如果在读取字符时采用了不统一的编码格式，可能会出现乱码

游戏开发中常见编码格式：UTF-8  
中文相关编码格式：GBK  
英文相关编码格式：ASCII

在C#中有一个专门的编码格式类，来帮助我们将字符串和字节数组进行转换

类名：Encoding  
命名空间：System.Text

#### 1.将字符串以指定编码格式转为字节

```cs
byte[] bytes1 = Encoding.UTF8.GetBytes("123123123");
```

#### 2.字节数组以指定编码格式转为字符串

```cs
string s = Encoding.UTF8.GetString(bytes1);
```

## 文件操作相关

### 文件相关

#### 代码中的文件操作是什么

在电脑上我们可以在操作系统中创建删除修改文件  
可以增删查改各种各样的文件类型

代码中的文件操作就是通过代码来做这些事情

#### 文件相关操作公共类

C#提供了一个名为File（文件）的公共类  
让我们可以快捷的通过代码操作文件相关  
类名：File  
命名空间：System.IO

#### 文件操作File类的常用内容

##### 1.判断文件是否存在

```cs
if(File.Exists(Application.dataPath + "/Test.txt"))
{
  print("文件存在");
}
else
{
  print("不存在");
}
```

##### 2.创建文件

```cs
FileStream fs = File.Create(Application.dataPath + "/test.txt");
```

##### 3.写入文件

###### 将指定字节数组写入到指定路径的文件中

```cs
byte[] bytes = BitConverter.GetBytes(999);
File.WriteAllBytes(Application.dataPath + "/test.txt", bytes);
```

###### 将指定的string数组内容一行行写入到指定路径中

```cs
string[] strs = new string[] { "123", "456", "123123123" };
File.WriteAllLines(Application.dataPath + "/test2.txt",strs);
```

###### 将指定字符串写入指定路径

```cs
File.WriteAllText(Application.dataPath + "/test3.txt", "123123123\n123123123");
```

##### 4.读取文件

###### 读取字节数据

```cs
byte[] bytes1 = File.ReadAllBytes(Application.dataPath + "/test.txt");
print(BitConverter.ToInt32(bytes1));
```

###### 读取所有行信息

```cs
string[] strs1 = File.ReadAllLines(Application.dataPath + "/test2.txt");
for (int i = 0; i < strs1.Length; i++)
{
  print(strs1[i]);
}
```

###### 读取所有文本信息

```cs
string str = File.ReadAllText(Application.dataPath + "/test3.txt");
print(str);
```

##### 5.删除文件

注意：如果删除打开着的文件会报错

```cs
File.Delete(Application.dataPath + "/test.txt");
```

##### 6.复制文件

参数一：现有文件，需要是流关闭状态  
参数二：目标文件

```cs
File.Copy(Application.dataPath + "/test2.txt", Application.dataPath + "/test4.txt");
```

##### 7.文件替换

参数一：用来替换的路径  
参数二：被替换的路径  
参数三：备份路径

```cs
File.Replace(Application.dataPath + "/test2.txt", Application.dataPath + "/test4.txt", Application.dataPath + "/test4(old).txt");
```

##### 8.以流的形式，打开文件并写入或读取

参数一：路径  
参数二：打开模式  
参数三：访问模式

```cs
FileStream fs = File.Open(Application.dataPath + "/test2.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
```

### 文件流相关

#### 什么是文件流

在C#中提供了一个文件流类：FileStream类  
它的主要作用是用于读写文件的细节，  
File只能整体读写文件，而FileStream可以以读写字节的形式进行处理文件

简单来说：文件内的数据类比于一条数据流，可以通过FileStream一部分一部分的去读写这一串数据流。  
比如：先存储一个int类型的，再存储一个 bool类型，最后再存一个char类型  
我们就可以利用FileStream进行逐个的读写操作

#### FileStream文件流常用方法

类名：FileStream  
命名空间：System.IO

##### 1.打开或创建指定文件

###### 方法一：new FileStream

参数一：路径  
参数二：打开模式  
        CreateNew：创建新文件，如果文件存在则报错  
        Create：创建文件，如果文件存在，则覆盖  
        Open：打开文件：如果文件不存在则报错  
        OpenOrCreate：打开或者创建文件，根据实际情况操作  
        Append：弱存在文件，则打开并查找文件尾，或者创建一个新文件  
        Truncate：打开并清空文件内容  
参数三：访问模式  
参数四：共享权限  
        None：谢绝共享  
        Read：允许别的程序读取当前文件  
        Write：允许别的程序写入该文件  
        ReadWrite：允许别的程序读写该文件

```cs
FileStream fs = new FileStream(Application.dataPath + "/test1.txt",
FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.ReadWrite);
```

###### 方法二：File.Create

参数一：路径  
参数二：缓存大小  
参数三：描述如何创建或覆盖该文件（不常用）  
        Asynchronous：可用于异步读写  
        DleteOnClose：不在使用时，自动删除  
        Encrypted：加密  
        None：不应用其他选项  
        RandomAccess：随机访问文件  
        SequentialScan：从头到尾顺序访问文件  
        WriteThrough：通过中间缓存直接写入磁盘

```cs
FileStream fs2 = File.Create(Application.dataPath + "/test2.txt",1024,FileOptions.None);
```

###### 方法三：File.Open

参数一：路径  
参数二：打开模式

```cs
FileStream fs3 = File.Open(Application.dataPath + "/test1.txt", FileMode.OpenOrCreate);
```

##### 2.重要属性和方法

###### 文本字节长度

```cs
print(fs.Length);
```

###### 是否可写

```cs
print(fs.CanWrite);
```

###### 是否可读

```cs
print(fs.CanRead);
```

###### 将字节写入文件，当写入后一定执行一次

```cs
fs.Flush();
```

###### 关闭流，当文件读写完毕后一定执行

```cs
fs.Close();
```

###### 缓存资源销毁回收

```cs
fs.Dispose();
```

##### 3.写入字节

方法：Write  
参数一：写入的字节数组  
参数二：数组中的开始索引  
参数三：写入多少个字节

```cs
fs.Write(bytes, 0, bytes.Length);
```

写入字符串时，先写入长度，再写入字符串具体内容

```cs
bytes = Encoding.UTF8.GetBytes("123123123");
int length = bytes.Length;
fs.Write(BitConverter.GetBytes(length), 0, BitConverter.GetBytes(length).Length);
fs.Write(bytes,0, length);
fs.Flush();
fs.Dispose();
```

##### 4.读取字节

###### 方法一：挨个读取字节数组

读取第一个整形

参数一：用于存储读取的字节数组的容器  
参数二：容器中开始的位置  
参数三：读取多少个字节装入容器  
返回值：当前流索引前进了几个位置

```cs
byte[] bytes2 = new byte[4];
fs.Read(bytes2, 0, 4);
int i = BitConverter.ToInt32(bytes2);
print(i);
```

读取第二个字符串

```cs
int index = fs2.Read(bytes2, 0, 4);
int length = BitConverter.ToInt32(bytes2);
bytes2 = new byte[length];
fs.Read(bytes2 , 0, length);
string s = Encoding.UTF8.GetString(bytes2);
```

###### 方法二：一次性读取再挨个读取

```cs
byte[] bytes1 = new byte[fs1.Length];
fs1.Read(bytes1,0, (int)fs1.Length);
fs1.Dispose();
print(BitConverter.ToInt32(bytes1, 0));
int length = BitConverter.ToInt32(bytes1, 4);
string s = Encoding.UTF8.GetString(bytes1, 8, length);
```

#### 如何更加安全的使用

using关键字重要用法：  
using(声明一个引用对象)  
{  
        使用对象  
}  
无论发生什么情况，当using语句块结束后  
会自动调用该对象的销毁方法，避免忘记销毁或关闭流  
using是一种更安全的使用方法

注意：为了文件操作安全，建议都使用using来进行处理

通过FileStream读写时一定要注意，读的规则一定要和写一致  
我们存储数据的先后顺序时我们自己定义的规则  
只要按照规则读写就能保证数据的正确性

### 文件夹相关

#### 文件夹操作是什么？

平时我们可以再操作系统的文件管理系统中，通过一些操作增删查改文件夹

#### C#提供给我们的文件夹操作公共类

类名：Directory  
命名空间：System.IO

##### 1.判断文件夹是否存在

```cs
if(Directory.Exists(Application.persistentDataPath + "/test"))
{
  print("存在");
}
else
{
  print("不存在");
}
```

##### 2.创建文件夹

```cs
DirectoryInfo info = Directory.CreateDirectory(Application.persistentDataPath + "/test");
```

##### 3.删除文件夹

参数一：路径  
参数二：是否删除非空目录，如果为true，将删除整个目录，  
                如果是false，仅当该目录为空才可删除

```cs
Directory.Delete(Application.persistentDataPath + "/test", true);
```

##### 4.查找文件夹和文件

###### 得到指定路径下所有文件夹名

```cs
string[] strings = Directory.GetDirectories(Application.dataPath);
for (int i = 0; i < strings.Length; i++)
{
  print(strings[i]);
}
```

###### 得到指定路径下所有文件名

```cs
string[] strings = Directory.GetFiles(Application.dataPath);
for (int i = 0; i < strings.Length; i++)
{
  print(strings[i]);
}
```

##### 5.移动文件夹

如果第二个参数所在的路径已经存在了一个文件夹，那么会报错  
移动会把文件夹中的所有内容一起移到新的路径

```cs
Directory.Move(Application.persistentDataPath + "test",Application.dataPath+"/111");
```

#### DirectoryInfo和FileInfo

DirectoryInfo目录信息类  
我们可以通过它获取文件夹的更多信息  
它主要出现在两个地方：

##### 1.创建文件夹方法的返回值

```cs
DirectoryInfo info1 = Directory.CreateDirectory(Application.dataPath + "/111");
```

###### 全路径

```cs
print(info1.FullName);
```

###### 文件名

```cs
print(info1.Name);
```

##### 2.查找上级文件夹信息

```cs
DirectoryInfo info2 = Directory.GetParent(Application.dataPath + "/111");
```

###### 全路径

```cs
print(info2.FullName);
```

###### 文件名

```cs
print(info2.Name);
```

##### 重要方法

###### 得到所有子文件夹的目录信息

```cs
DirectoryInfo[] infos = info2.GetDirectories();
```

##### FileInfo文件信息类

我们可以通过DirectoryInfo得到该文件下的所有文件信息

```cs
FileInfo[] fileInfos = info2.GetFiles();
```

该两个类一般再多文件夹或者多文件操作时会用到

用法和Directory和File类大同小异

## C#类对象的序列化和反序列化

### 序列化

#### 声明类对象

如果要使用C#自带的序列化二进制方法  
需要先添加\[System.Serializable\]特性

#### 将对象进行二进制序列化

##### 方法一：使用内存流得到二进制字节数组

主要用于得到字节数组，可以用于网络传输

1.内存流对象：MemoryStream  
命名空间：System.IO

2.二进制格式化对象：BinaryFormatter  
命名空间：System.Runtime.Serialization.Formatters.Binary  
主要方法：序列化方法Serialize

```cs
public class Test : MonoBehaviour
{
  private void Start()
  {
    Student s = new Student();
    using(MemoryStream ms = new MemoryStream())
    {
      BinaryFormatter bf = new BinaryFormatter();
      bf.Serialize(ms, s);
      byte[] bytes = ms.GetBuffer();
      File.WriteAllBytes(Application.dataPath+"/123.111", bytes);
      ms.Close();
    }
  }
}
[System.Serializable]
public class Student
{
  public int id =1;
  public string name="123"; public string description="111";
  public int age=16;
  public bool sex=false;
}
```

##### 方法二：使用文件流进行存储

```cs
Student s = new Student();
using(FileStream fs = new FileStream(Application.dataPath + "/test1.test", FileMode.OpenOrCreate, FileAccess.Write))
{
  BinaryFormatter bf = new BinaryFormatter();
  bf.Serialize(fs, s);
  fs.Flush();
  fs.Close();
}
```

### 反序列化

#### 反序列化文件中的数据

主要类：  
FileStream、BinaryFormatter

主要方法：Deserialize

```cs
Student s = new Student();
using(FileStream fs = new FileStream(Application.dataPath + "/test1.test", FileMode.OpenOrCreate, FileAccess.Write))
{
  BinaryFormatter bf = new BinaryFormatter();
  s = (Student)bf.Deserialize(fs);
  fs.Close();
}
```

#### 反序列化网络传输过来的二进制数据

主要类：  
MemoryStream内存流类  
BinaryFormatter 二进制格式化类

主要方法：Deserialize

目前没有网络传输，所以依旧从文件中读取

```cs
Student s = new Student();
byte[] bytes = File.ReadAllBytes(Application.dataPath + "/test1.test");
using(MemoryStream ms = new MemoryStream(bytes))
{
  BinaryFormatter formatter = new BinaryFormatter();
  s = formatter.Deserialize(ms) as Student;
  ms.Close();
}
```

### 加密

#### 何时加密？何时解密？

当我们将类对象转换为二进制数据时进行加密  
当我们将二进制数据转换为类对象时进行解密

这样如果第三方获取到我们的二进制数据，当他们不知道加密规则和解密密钥时就无法获取正确的数据，起到保证数据安全的作用

#### 加密是否一定安全？

加密只是提高了破解门槛，没有100%保密的数据  
通过各种尝试始终是可以破解加密规则的，只是时间问题，加密只能起到提升一定安全性的作用

#### 常见加密算法

MD5、SHA1、HMAC、AES/DES/3DES算法等等

有很多的第三方加密算法库，可以直接获取用于在程序中对数据进行加密

## 实现一个简单的自定义读写类

```cs
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
public class BinaryDataManager
{
  private static BinaryDataManager instance = new BinaryDataManager();
  public static BinaryDataManager Instance => instance;
  private static string SAVE_PATH = Application.persistentDataPath + "/Data/";
  private BinaryDataManager() { }
  public void Save(object obj, string fileName)
  {
    if (!Directory.Exists(SAVE_PATH))
      Directory.CreateDirectory(SAVE_PATH);
    using (FileStream fs = new FileStream(SAVE_PATH + fileName + ".data", FileMode.OpenOrCreate, FileAccess.Write))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      formatter.Serialize(fs, obj);
      fs.Flush();
      fs.Close();
    }
  }
  public T Load<T>(string fileName) where T : class
  {
    if (!File.Exists(SAVE_PATH + fileName + ".data"))
      return default(T);
    T obj = null;
    using (FileStream fs = File.Open(SAVE_PATH + fileName + ".data", FileMode.Open, FileAccess.Read))
    {
      BinaryFormatter binaryFormatter = new BinaryFormatter();
      obj = binaryFormatter.Deserialize(fs) as T;
      fs.Close();
    }
    return obj;
  }
}
```

## 主要用处

网络游戏：用于存储客户端数据、用于传输信息

单机游戏：用于存储游戏相关数据、用于配置游戏数据

注：可以自行实现一个通过excel表格配置数据，然后直接自动生成数据类和数据文件

## 补充：如何在Unity中添加菜单栏功能

### 为编辑器菜单栏添加新的选项入口

可以通过Unity提供我们的MenuItem特性在菜单栏添加选项按钮  
特性名：MenuItem  
命名空间：UnityEditor

例：

```cs
[MenuItem("GameTool/Test")]
private static void TestFunc()
{
  Debug.Log("Test");
}
```

注意：  
必须是一个静态方法  
菜单栏必须要有两级或以上的层级，不然会报错  
可以不继承Monobehaviour

### 刷新Project窗口内容

类名：AssetDatabase  
命名空间：UnityEditor  
方法：Refresh

```cs
AssetDatabase.Refresh();
```

### Editor文件夹

editor文件夹可以放在项目的任何文件夹下，可以有多个  
放在其中的内容，项目打包时不会被打包到项目中  
一般编辑器相关代码都可以放在该文件夹中

## 补充：Excel数据读取

### Excel表的本质

Excel表本质上也是一堆数据，只不过它有自己的存储读取规则  
如果我们想要通过代码读取它，那么必须知道它的存储规则

官网专门提供了对应的dll文件用来解析Excel文件的

对于此类dll，我们一般只在编辑器中使用，所以最好放置在Editor文件夹内

### 打开Excel表

```cs
[MenuItem("GameTool/打开Excel表")]
private static void OpenExcel()
{
  using(FileStream fs = File.Open(Application.dataPath + "/ArtRes/Excel/PlayerInfo.xlsx", FileMode.Open, FileAccess.Read))
  {
    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
    DataSet result = excelReader.AsDataSet();
    for (int i = 0;i<result.Tables.Count;i++)
    {
      print("表名"+result.Tables[i].TableName);
    }
    fs.Close();
  }
}
```

### 获取Excel表中单元格信息

```cs
[MenuItem("GameTool/读取Excel信息")]
private static void ReadExcel()
{
  using (FileStream fs = File.Open(Application.dataPath + "/ArtRes/Excel/PlayerInfo.xlsx", FileMode.Open, FileAccess.Read))
  {
    IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(fs);
    DataSet result = excelReader.AsDataSet();
    for (int i = 0; i < result.Tables.Count; i++)
    {
      DataTable table = result.Tables[i];
      for (int j = 0; j < table.Columns.Count; j++)
      {
        DataRow row = table.Rows[j];
        for(int k = 0; k < table.Columns.Count; k++)
        {
          Debug.Log(row[k].ToString());
        }
      }
    }
    fs.Close();
  }
}
```

### 获取Excel表中信息的意义

既然我们能获取到Excel表中的所有数据，那么我们就可以通过表中的数据来动态生成相关数据

为什么不直接读取Excel表？

1.读取效率较低  
2.数据安全性低