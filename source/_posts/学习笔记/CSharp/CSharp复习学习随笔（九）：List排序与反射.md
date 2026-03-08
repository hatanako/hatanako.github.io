---
title: CSharp复习学习随笔（九）：List排序与反射
date: 2025-11-21
categories: 编程笔记
tags:
  - C#
---

## List排序

### 1.List自带排序方法

```cs
List<int> list = new List<int>() { 1, 3, 5, 7, 9, 2, 4, 6, 8, 0 };
list.Sort();
```

ArrayList中也有Sort排序方法，默认为升序。

### 2.自定义 类 的排序

我们需要去继承IComparable接口，来实现排序

```cs
public class Item : IComparable<Item>
{
  public int id { get; set; }
  public int price { get; set; }
  public Item(int _id, int _price)
  {
    id = _id;
    price = _price;
  }
  public int CompareTo(Item? other)
  {
    if (this.price > other.price)
    {
      return 1;
    }
    else if (this.price < other.price)
    {
      return -1;
    }
    else
    {
      return 0;
    }
  }
}
public class Program
{
  static void Main(string[] args)
  {
    List<Item> items = new List<Item>()
    {
      new Item(1,20),
      new Item(2,10),
      new Item(3,30),
      new Item(4,25)
    };
    items.Sort();
  }
}
```

补充说明：CompareTo返回值的含义

小于0：放在传入对象的前面  
等于0：保持当前位置不变  
大于0：放在传入对象的后面

可以简单理解为传入对象的位置就是0，如果返回值为负数，就放在左边，如果是正数就放在右边

### 3.通过委托函数进行排序

```cs
static void Main(string[] args)
{
  List<Item> items = new List<Item>()
  {
    new Item(1,20),
    new Item(2,10),
    new Item(3,30),
    new Item(4,25)
  };
  items.Sort();
  items.Sort((a, b) => a.price - b.price);
}
```

### **练习**

{% asset_img 1.png %}

## 协变逆变

### 1.什么是协变逆变

**协变：**  
和谐的变化，自然的变化  
因为里氏替换原则，父类可以装子类，所以**子类变父类**的感受是和谐的  
比如： string 变成 object

**逆变：**  
逆常规的变化，不正常的变化  
因为里氏替换原则，父类可以装子类，但是子类不能装父类，所以**父类变子类**的感受是不和谐的  
比如： object 变成 string

协变和逆变是用来修饰泛型的  
**协变：out  
逆变：in**  
用于在泛型中修饰泛型字母的，**只有泛型接口和泛型委托能使用**  
**如何记忆？**协变是子类变父类，所以是out，逆变是父类变子类，所以是in

### 2.作用

返回值和参数  
用out修饰的泛型，只能作为返回值

```cs
delegate T MyFunc<out T>();
```

用in修饰的泛型，只能作为参数

```cs
delegate void MyFunc<in T>(T v);
```

注：同样遵循里氏替换原则，用out和in修饰的泛型委托，如果类型是父子关系，可以相互装载

**练习**

{% asset_img 2.png %}

## 多线程

### 1.进程

进程（Process）是计算机中的程序关于某数据集合上的一次运行活动，是系统进行资源分配的基本单位，是操作系统的基础  
打开一个应用程序就是在操作系统上 开启 了一个进程  
进程之间可以相互独立运行，互不干扰，同时也可以相互访问、操作。

### 2.什么是线程

操作系统能够进行运算调度的最小单位，它被包含在进程之中，是进程中的实际运作单位  
一条线程指的是进程中一个单一顺序的控制流，一个进程中可以并发多个线程  
在学习练习中，我们写的程序大部分都是在主线程中

简单理解：线程就是代码从上到下运行的一条“管道”

### 3.什么是多线程

我们可以通过代码来开启新的线程，可以同时运行代码的多条“管道”，就叫多线程

### 4.语法

线程类 Thread 需要引用命名空间 System.Threading

**如何声明一个新线程**

注意：线程执行的代码，需要封装到一个函数中  
新线程将要执行的代码逻辑被封装到了一个函数语句块中

```cs
static void Main(string[] args)
{
  Thread t = new Thread(NewThreadLogic);
}
static void NewThreadLogic()
{
}
```

**如何启动线程**

通过调用Start方法来启动线程

```cs
t.Start();
```

**如何设置为后台线程**

当前台线程都结束了的时候，整个程序也就结束了，即使还有后台线程正在运行  
后台线程不会防止应用程序的进程被终止掉  
如果不设置为后台线程，可能会导致进程无法正常关闭

```cs
t.IsBackground = true;
```

**如何关闭并释放一个线程**

如果开启的线程中不是死循环，是能够结束的逻辑，那么不用刻意的去关闭它  
如果是死循环，想要终止这个线程，有两种方式

（1）死循环中bool标识

```cs
static bool stopThread = false;
static void Main(string[] args)
{
  Thread t = new Thread(NewThreadLogic);
  t.Start();
  t.IsBackground = true;
  stopThread = true;
}
static void NewThreadLogic()
{
  while (!stopThread)
  {
  }
}
```

（2）通过线程提供的方法（.net core版本中无法终止，会报错）

注意查看代码说明，大概率已过时，建议 使用 try catch语句块

```cs
t.Abort();
```

**如何使线程休眠**

```cs
Thread.Sleep();
```

在哪个线程里执行，就休眠哪个线程

### 5.线程之间共享数据

多个线程使用的内存是共享的，都属于该应用程序（进程）  
所以要注意当多线程同时操作同一片内存区域时可能会出问题  
可以通过加锁的形式避免

lock：当我们在多个线程当中想要访问同样的东西，进行逻辑处理时，为了避免不必要的逻辑顺序执行的差错  
lock（引用类型对象）{要执行完毕的代码}

```cs
static object obj = new object();
static void Main(string[] args)
{
  Thread t = new Thread(NewThreadLogic);
  t.Start();
  lock (obj)
  {
    Console.SetCursorPosition(0, 0);
    Console.WriteLine("主线程运行中...");
  }
}
static void NewThreadLogic()
{
  lock (obj)
  {
    Console.SetCursorPosition(10, 10);
    Console.WriteLine("新线程运行中...");
  }
}
```

### 6.多线程的意义

可以使用多线程专门处理一些复杂耗时的逻辑，例如寻路、网络通信等等。

**练习**：

{% asset_img 3.png %}

## 预处理器指令

### 1.什么是编译器

编译器是一种翻译程序，它用于将**源语言程序**翻译为**目标语言程序**

> **源语言程序**：某种程序设计语言写成的，比如C#、C、CPP等语言写的程序  
> **目标语言程序**：二进制数表示的伪机器代码写的程序

### 2.什么是预处理器指令

预处理器指令是用于指导编译器在实际编译开始之前对信息进行预处理的指令  
预处理器指令都是以#开始，它并不是语句，所以不以分号；结束  
经常用到的整理代码逻辑的 折叠代码块（#region）就是预处理器指令

### 3.常见的预处理器指令

1). #define        定义一个符号，类似一个没有值的变量  
     #undef         取消define定义的符号，让其失效  
两者都是写在脚本文件最前面，一般配合 if 指令使用，或配合特性

2). #if        #elif        #else        #endif  
和if语句规则一样，一般配合#define定义的符号使用，用于高速编译器进行编译代码的流程控制

3).#warning        #error  
告诉编译器是报警告还是报错，一般还是配合if使用

```cs
#define UnityEditor
public class Program
{
  static void Main(string[] args)
  {
    #if UnityEditor
    System.Console.WriteLine("Unity Editor is defined.");
    #else
    #warning "Unity Editor is not defined"
    #endif
  }
}
```

例如上方代码，如果存在定义的UnityEditor符号，则执行代码WriteLine，没有该符号则发出警告。

**练习**：

{% asset_img 4.png %}

## 反射和特性

### 反射

#### 1.什么是程序集

程序集是经由编译器编译得到的，供进一步编译执行的额那个中间产物  
在Windows系统中，它一般表现为后缀为.dll(库文件)或者是.exe(可执行文件)的格式

说的简单点就是我们写的代码的集合，最终被编译器编译成了一个程序集给别人使用，例如dll文件或者exe文件

#### 2.元数据

元数据就是用来描述数据的数据，这个概念不仅仅用于程序上，在别的领域也有元数据  
例如，程序中的类，类中的函数、变量等等信息就是程序的元数据  
有关程序以及类型的数据被称为元数据，它们保存在程序集中

#### 3.反射的概念

程序正在运行时，可以查看其他程序集或者自身的元数据。  
一个运行的程序查看本身或者其他程序的元数据的行为就叫做反射

在程序运行时，通过反射可以得到其他程序集或者自己程序集代码的各种信息  
类，函数，变量，对象等等，实例化它们，执行、操作它们

#### 4.反射的作用

因为反射可以在程序编译后获得信息，所以它提高了程序的拓展性和灵活性  
1.程序运行时得到所有的元数据，包括元数据的特性  
2.程序运行时，实例化对象，操作对象  
3.程序运行时创建新对象，用这些对象执行任务

#### 5.语法相关

##### 1）Type

type（类的信息类），它是反射功能的基础。它是访问元数据的主要方式  
使用 Type 的成员获取有关类型声明的信息  
有关类型的成员（如构造函数、方法、字段、属性和类的事件）

###### **如何获取Type**

①万物之父object中的 GetType()方法可以获取对象的Type

```cs
int a = 10;
Type type = a.GetType();
```

                type打印出的结果为int所在的命名空间

②通过 typeof 关键字 传入类名，也可以得到对象的Type

```cs
Type type2 = typeof(int);
```

③通过类的名字，也可以获取类型（注：类名必须包含命名空间，不然找不到）

```cs
Type type3 = Type.GetType("System.Int32");
```

注：以上三个type它们指向的堆内存地址时一样的

###### **得到类的程序集信息**

```cs
Console.WriteLine(type.Assembly);
```

###### **获取类中的所有公共成员**

```cs
Test test = new Test(1, 2, "hello");
Type testType = test.GetType();
MemberInfo[] members = testType.GetMembers();
```

###### **获取类的公共构造函数并调用**

1.获取所有的构造函数

```cs
ConstructorInfo[] constructors = testType.GetConstructors();
```

2.获取其中一个构造函数，并执行

得到构造函数需要传入 Type 数组，数组中的内容按照顺序为参数类型  
执行构造函数需要传入 object 数组，表示按顺序传入的参数

2.1.得到无参构造，并执行

```cs
ConstructorInfo ctor = testType.GetConstructor(Type.EmptyTypes);
Test test1 = (Test)ctor.Invoke(null);
```

2.2.得到有参构造

```cs
ConstructorInfo ctor2 = testType.GetConstructor(new Type[]
{ typeof(int), typeof(int), typeof(string) }
);
Test test2 = (Test)ctor2.Invoke(new object[] { 3, 4, "world" });
```

###### **获取类的公共成员变量**

1.得到所有成员变量

```cs
FieldInfo[] fieldInfos = testType.GetFields();
foreach (FieldInfo fieldInfo in fieldInfos)
{
  Console.WriteLine($"Field: {fieldInfo.Name}, Type: {fieldInfo.FieldType}");
}
```

2.得到指定名称的公共成员变量

```cs
FieldInfo fieldInfo1 = testType.GetField("j");
```

3.通过反射获取和设置对象的值

3.1通过反射获取对象的某个变量的值

```cs
Console.WriteLine(fieldInfo1.GetValue(testType));
```

3.2通过反射设置对象的某个变量的值

```cs
fieldInfo1.SetValue(test2, 20);
Console.WriteLine(fieldInfo1.GetValue(test2));
```

###### 获取类的公共成员方法

通过Type类中的 GetMethod 方法，得到类中的方法  
MethodInfo 是方法的反射信息

```cs
Type strType = typeof(string);
MethodInfo[] methodInfos = strType.GetMethods();
foreach (MethodInfo methodInfo in methodInfos)
{
  Console.WriteLine($"Method: {methodInfo.Name}");
}
```

1.如果存在方法重载，用Type数组表示参数类型

```cs
MethodInfo methodInfo2 = strType.GetMethod("Substring",
new Type[] {typeof(int), typeof(int) }
);
```

2.调用方法        注：如果是静态方法，Invoke中的第一个参数传入null即可

```cs
string str = "Hello, World!";
object result = methodInfo2.Invoke(str, new object[] {7, 5});
```

第一个参数相当于是哪一个对象要执行这个成员方法

###### 其他

> 得枚举：GetEnumName,GetEnumNames  
> 得事件：GetEvent,GetEvents  
> 得接口：GetInterface(s)  
> 得属性：GetProperty(s)  
> 等等

___

##### 2）Assembly

Assembly是一个程序集类  
主要用来加载其他程序集，加载后才能用Type来使用其他程序集中的信息  
如果想要使用不是自己程序集中的内容，需要先加载程序集，比如 dll文件（库文件）  
简单的把库文件堪称一种代码仓库，它提供给使用者一些可以直接拿来用的变量、函数或者类

三种加载程序集的函数

1.一般用来加载在同一文件下的其他程序集

```cs
Assembly assembly = Assembly.Load("程序集名称");
```

2.一般用来加载不在同一文件下的其他程序集

```cs
Assembly assembly2 = Assembly.LoadFrom("程序集路径");
Assembly assembly1 = Assembly.LoadFile("程序集完整路径");
```

**使用方法**：

先加载一个指定程序集，再加载程序集中的一个类对象，之后才能使用反射

```cs
Assembly assembly = Assembly.LoadFrom(@"D:\learn\算法\ConsoleApp1\ConsoleApp1\bin\Debug\net8.0\ConsoleApp1");
Type[] types = assembly.GetTypes();
foreach (Type t in types)
{
  Console.WriteLine($"Type: {t.FullName}");
}
Type playerType = assembly.GetType("ConsoleApp1.Player");
foreach (MemberInfo member in playerType.GetMembers())
{
  Console.WriteLine($"Member: {member.Name}, Type: {member.MemberType}");
}
object playerInstance = Activator.CreateInstance(playerType);
```

3.类库工程创建

类库工程主要用于编写dll文件，是纯粹的实现算法逻辑等等操作的工程

___

##### 3）Activator

Activator是用于将Type对象快速实例化为对象的类

1.无参构造

```cs
Type type1 = typeof(Test);
Test test3 = (Test)Activator.CreateInstance(type1);
```

2.有参构造

```cs
Test test4 = (Test)Activator.CreateInstance(type1,
new object[] { 5, 6, "Activator" }
);
Test test5 = (Test)Activator.CreateInstance(type1, 7, 8, "Direct Params");
```

### 反射总结

在程序运行时，通过反射可以得到其他程序集或者自己的程序集代码的各种信息  
类、函数、变量、对象等等，实例化它们，执行他们，操作他们

关键类：Type，Assembly，Activator

在开发学习前期，基本不会使用反射，所以暂时只需要了解反射可以做什么即可

为什么要学习反射

Unity的基本工作机制就是建立在反射的基础上的，为后面学习Unity的工作打下基础

**练习**：

{% asset_img 5.png %}

___

### C#特性

#### 1.特性是什么

特性是一个允许我们向程序的程序集添加元数据的语言结构  
它是用于保存程序结构信息的某种特殊类型的类

特性提供功能强大的方法以将声明信息与 C# 代码（类型、方法、属性等）相关联。  
特性与程序实体关联后，即可在运行时使用反射查询特性信息

特性的目的是高速编译器把程序结构的某组元数据嵌入程序集中  
它可以放置在几乎所有的声明中（类、变量、函数等等声明）

特性的本质是一个类，我们可以利用特性类为元数据添加额外信息  
比如一个类、成员变量、成员方法等等为他们添加更多的额外信息  
之后可以通过反射来获取这些额外信息

#### 2.自定义特性

自定义特性只需继承特性基类 Attribute

```cs
class MyCustomAttribute : Attribute
{
  public string info { get; }
  public MyCustomAttribute(string _info)
  {
    info = _info;
  }
}
```

#### 3.特性的使用

基本语法：\[特性名(参数列表)\]  
本质上就是在调用特性类的构造函数

写在哪里？类、函数、变量的上一行，表示他们具有该特性信息

判断是否使用了某个特性可以使用IsDefined()函数

> IsDefined的参数：  
> 参数1：特性的类型  
> 参数2：代表是否搜索继承链（属性和事件忽略此参数）

```cs
static void Main(string[] args)
{
  MyClass myClass = new MyClass();
  Type type = myClass.GetType();
  if (type.IsDefined(typeof(MyCustomAttribute), false))
  {
    Console.WriteLine("using Attribute");
  }
}
```

获取所有特性：

```cs
object[] attrs = type.GetCustomAttributes(true);
foreach (var attr in attrs)
{
  if(attr is MyCustomAttribute myAttr)
  {
    Console.WriteLine("类特性信息：" + myAttr.info);
    myAttr.PlusFunc();
  }
}
```

#### 4.限制自定义特性的使用范围

通过为特性类添加特性来限制其适用范围  
关键字：AttributeUsage  
它拥有三个参数  
参数1：AttributeTargets —— 特性能够用在哪些地方  
参数2：AllowMultiple —— 是否允许多个特性实例用在同一个目标上  
参数3：Inherited —— 特性是否能被派生类和重写成员继承

例：

```cs
[AttributeUsage(AttributeTargets.Class| AttributeTargets.Struct,
AllowMultiple = true,Inherited = true)]
class MyCuntom2 : Attribute
{
}
```

#### 5.系统自带特性——过时特性

关键字：Obsolete  
用于提示用户使用的方法等成员已经过时，建议使用新方法  
一般加在函数前的特性

参数1：调用过时方法时提示的内容  
参数2：true——使用该方法会报错，false——使用该方法会警告

#### 6.系统自带特性——调用者信息特性

哪个文件调用？  
CallerFilePath特性  
哪一行调用？  
CallerLineNumber特性  
哪个函数调用  
CallerMemberName特性

需要引用命名空间 using System.Runtime.CompilerServices;  
一般作为函数参数的特性

```cs
public void SpeakCaller(string str, [CallerFilePath] string fileName = "",
[CallerLineNumber] int line = 0,
[CallerMemberName]string target = "")
{
}
```

#### 7.系统自带特性——条件编译特性

关键字：Conditional  
它会和预处理指令 #define 配合使用  
给它传入的参数就是 define 定义的符号，定义了之后才能编译执行

需要引用命名空间 using System.Diagnostics;  
主要可以用在一些调试代码上，有时想执行有时不想执行的代码

#### 8.系统自带特性——外部dll包函数特性

关键字：DllImport  
用来标记非.Net(C#)的函数，表明该函数在一个外部的DLL中定义。  
一般用来调用 C 或者 CPP 的dll包写好的代码  
需要引用命名空间 using System.Runtime.InteropServices;

传入的参数为dll包的文件名或路径名

例：

```cs
[DllImport("Test.dll")]
public static extern void TestDllFunc();
```

**练习**：

{% asset_img 6.png %}

## 迭代器

### 1.迭代器是什么

迭代器（iterator）有时又称光标（cursor），是程序设计的软件设计模式  
迭代器模式提供一个方法顺序访问一个聚合对象中的各个元素，而又不暴露其内部的标识

在表现效果上看，是可以在容器对象（例如链表或数组）上遍历访问的接口  
设计人员无需关心容器对象的内存分配的实现细节  
可以用foreach遍历的类，都是实现了迭代器的

### 2.标准迭代器的实现方法

关键接口：IEnumerator，IEnumerable  
命名空间：using System.Collections  
可以通过同时继承IEnumerable和IEnumerator实现其中的方法

IEnumerable

继承后实现的函数功能为使迭代器可行

```cs
public IEnumerator GetEnumerator()
{
  throw new NotImplementedException();
}
```

IEnumerator

继承后实现的各类方法、变量为具体迭代的逻辑

```cs
public object Current => throw new NotImplementedException();
public bool MoveNext()
{
  throw new NotImplementedException();
}
public void Reset()
{
  throw new NotImplementedException();
}
```

**foreach本质**

> 1.先获取in后面这个对象的 IEnumerator，会调用对象其中的GetEnumerator方法来获取  
> 2.执行得到这个IEnumerator对象中的 MoveNext 方法  
> 3.只要MoveNext方法的返回值是true 就会去得到Current，然后赋值给item

```cs
class CustomList : IEnumerable, IEnumerator
{
  private int[] list;
  private int index = -1;
  public CustomList(int[] initialValues)
  {
    list = initialValues;
  }
  public object Current
  {
    get
    {
      return list[index];
    }
  }
  public IEnumerator GetEnumerator()
  {
    Reset();
    return this;
  }
  public bool MoveNext()
  {
    index++;
    return index < list.Length;
  }
  public void Reset()
  {
    index = -1;
  }
}
```

### 3.用yield return 语法糖实现迭代器

yield return 是C#提供给我们的语法糖，所谓语法糖，也称糖衣语法  
主要作用就是将复杂逻辑简单化，可以增加程序的可读性，从而减少程序代码出错的机会

关键接口：IEnumerable  
命名空间：using System.Collections  
让想要通过foreach遍历的自定义类实现接口中的方法GetEnumerator即可

yield关键字，配合迭代器使用，可以理解为暂时返回，保留当前的状态，一会还会回来

```cs
class CustomList2 : IEnumerable
{
  private int[] list;
  public CustomList2(int[] initialValues)
  {
    list = initialValues;
  }
  public IEnumerator GetEnumerator()
  {
    for (int i = 0; i < list.Length; i++)
    {
      yield return list[i];
    }
  }
}
```

### 4.用yield return 语法糖为泛型类实现迭代器

```cs
class CustomList2<T> : IEnumerable
{
  private T[] list;
  public CustomList2(T[] initialValues)
  {
    list = initialValues;
  }
  public IEnumerator GetEnumerator()
  {
    for (int i = 0; i < list.Length; i++)
    {
      yield return list[i];
    }
  }
}
```

**练习：**

{% asset_img 7.png %}

## 特殊语法

### 1.var隐式类型

var是一种特殊的变量类型，它可以用来表示任意类型的变量

注意：var不能作为类的成员，只能用于临时变量声明时，也就是一般写在函数语句块中  
          var必须初始化

```cs
var a = 5;
var s = "Hello World";
var array = new int[] {1,2,3, 4, 5 };
var customList = new CustomList(array);
```

### 2.设置对象初始值

声明对象时，可以通过直接写大括号的形式初始化公共成员变量和属性  
注：需要无参构造函数

```cs
CustomList list = new CustomList { list =new int[] { 1, 2, 3, 4, 5 } };
```

### 3.设置集合初始值

声明集合对象时，也可以通过大括号直接初始化内部属性

```cs
int[] array = { 1, 2, 3, 4, 5 };
```

### 4.匿名类型

var变量可以声明为自定义的匿名类型，只能有成员变量，不能有函数

```cs
var people = new { age = 10, money = 111, name = "11" };
```

### 5.可空类型

**1.值类型时不能赋值为空的**

**2.声明时，在值类型后面加？ 可以赋值为空**

```cs
int? i = null;
```

**3.判断是否为空**

```cs
if (i.HasValue)
{
  Console.WriteLine(i);
}
```

**4.安全获取可空类型值**

**4-1.如果为空，默认返回值类型的默认值**

```cs
Console.WriteLine(i.GetValueOrDefault());
```

**4-2.也可以指定一个默认值**

```cs
Console.WriteLine(i.GetValueOrDefault(1));
```

**另一种使用方法：**

相当于一种语法糖，可以帮助我们自动去判断是否为空

```cs
List<int> ints =null;
ints?.Sort();
if (ints != null)
{
  ints.Sort();
}
```

### 6.空合并操作符

空合并操作符 ??  
语法：左边值 ?? 右边值  
如果左边值为null，就返回右边值，否则返回左边值  
只要是可以为null的对象都可以用

```cs
int? a = null;
int b = a == null ? 100 : a.Value;
int c = a ?? 100;
```

### 7.内插字符串

关键符号：用来构造字符串，让字符串中可以拼接变量

```cs
Console.WriteLine($"123123123,,,{c},,");
```

### 8.单句逻辑简略写法

当语句块中只有一行代码时，可以省略大括号

```cs
if (true)
Console.WriteLine("111");
```

```cs
public CustomList(int[] initialValues) => list = initialValues;
```

## 值和引用类型的补充

**复习：**

**值类型：**  
无符号：byte,ushort,uint,ulong  
有符号：sbyte,short,int,long  
浮点数：float,double,decimal  
特殊：char,bool  
枚举：enum  
结构体：struct

**引用类型：**  
string，数组，class，interface，委托

值类型和引用类型的本质区别：  
值类型的具体内容存在栈内存上，引用的具体内容存在堆内存上

### 1.如何判断值类型和引用类型

可以按f12进到类的内部查看，如果是class就是引用，struct就是值类型

### 2.语句块

各个语句块的关系：

> 命名空间  
> （包含）  
> 类、接口、结构体  
> （包含）  
> 函数、属性、索引器、运算符重载等（类、接口、结构体）  
> （包含）  
> 条件分支、循环

上层语句块：类、结构体  
中层语句块：函数  
底层语句块：条件分支，循环等

**逻辑代码写在哪里？**  
函数、条件分支、循环——中底层语句块

**变量声明在哪里？**  
上、中、底层都能声明变量  
上层语句块中：成员变量  
中、底层语句块中：临时变量

### 3.变量的生命周期

编程时大部分都是临时变量  
在中底层声明的临时变量（函数、条件分支、循环语句块等）  
语句块执行结束  
没有被记录的对象将被回收或编程垃圾  
值类型：被系统自动回收  
引用类型：栈上用于存地址的房间被系统自动回收，堆中具体内容变成垃圾，待下一次GC才会被回收

想要不被回收或者不变垃圾，必须要将其记录下来  
如何记录？  
在更高层级记录或者使用静态全局变量记录

### 4.结构体中的值和引用

结构体本身时值类型  
前提：该结构体没有作为其他类的成员  
在结构体中的值，栈中存储值具体的内容  
在结构体中的引用，堆中存储引用的具体内容

引用类型始终存储在堆中，真正通过结构体使用其中的引用类型只是在顺藤摸瓜

### 5.类中的值和引用

类本身时引用类型  
在类中的值，堆中存储具体的值  
在类中的引用，堆中存储具体的值

### 6.数组中的存储规则

数组本身是引用类型  
值类型数组，堆中存具体内容  
引用类型数组，堆中存地址

### 7.结构体继承接口

利用里氏替换原则，用接口容器装载结构体存在装箱拆箱