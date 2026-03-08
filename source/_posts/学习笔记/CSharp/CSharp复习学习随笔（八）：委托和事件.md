---
title: CSharp复习学习随笔（八）：委托和事件
date: 2025-11-18
categories: 编程笔记
tags:
  - C#
---

## 一、委托

**委托是什么？**

委托是函数（方法）的容器  
可以理解为表示函数（方法）的变量类型，用来存储、传递函数（方法）  
委托本质是一个类，用来定义函数（方法）的类型（返回值和参数的类型）  
**不同的函数（方法）必须对应和各自”格式“一致的委托**

**基本语法**

关键字：delegate  
语法：访问修饰符 delegate 返回值 委托名(参数列表);

**写在哪里？**

可以声明再namespace和class语句块中，更多的写在namespace中

**如何定义自定义委托**

访问修饰符默认不写为public，在别的命名空间中也能使用，一般使用public

注：委托之间不能够和函数一样重载

**如何使用定义好的委托**

委托的变量是函数的容器

```cs
delegate void Function(){}
static void Func(){}
Function f = new Function(Func);
Function f2 = Func;
f.Invoke();
f2();
```

委托常用在：1.作为类的成员；2.作为函数的参数。

```cs
delegate void MyDelegate();
delegate void MyDelegate2();
class Test
{
  public MyDelegate myDelegate { get; set; }
  public MyDelegate2 myDelegate2 { get; set; }
  public void TestFunc(MyDelegate d1,MyDelegate2 d2)
  {
    d1();
    d2();
  }
}
```

**多播委托**

委托变量可以存储多个函数。

**增**：

```cs
MyDelegate func = Func1;
func += Func1;
```

**删**：

多减不会报错。

```cs
MyDelegate func = Func1;
func -= Func1;
```

**系统定义好的委托**：

1.Action（无参无返回值）

2.Func<>（可以指定返回值类型的泛型委托）

3.Action<>  (可以传入n个参数的委托，最多16个)

4.Func<> (可以传n个参数，并且有返回值的，系统也提供了16个)  
注：参数在前，返回值在最后。

**练习**：

{% asset_img 1.png %}

## 二、事件

**事件是什么？**

事件是基于委托的存在，事件是委托的安全包裹，让委托的使用更具有安全类型，是一种特殊的变量类型。

**事件的使用**

声明语法：访问修饰符 event 委托类型 事件名;

1.事件是作为成员变量存在于类中  
2.委托怎么用 事件就怎么用

**事件和委托的区别**

1.不能在类内部赋值  
2.不能在类外部调用

注意：它只能作为成员存在于类和接口以及结构体中

**为什么有事件？**

1.防止外部随意制空委托  
2.防止外部随意调用委托  
3.事件相当于对委托进行了一次封装，让其更加安全

**练习**

{% asset_img 2.png %}

## 三、匿名函数

**什么是匿名函数**？

匿名函数就是没有名字的函数  
它的使用主要是配合委托和事件进行使用  
脱离委托和事件是不会使用匿名函数的

**基本语法**

delegate (参数列表){函数逻辑};        注意：结尾有分号！！

**使用**

何时使用？  
1.函数中传递委托参数时  
2.委托或事件赋值时

1.无参无返回

```cs
Action a = delegate () { Console.WriteLine("匿名函数逻辑"); };
```

2.有参

容器内的泛型格式要与匿名函数参数相同

```cs
Action<int> b = delegate (int i) { Console.WriteLine("匿名函数容器" + i );};
```

3.有返回值

```cs
Func<int, int> c = delegate (int i) { return i * i; };
```

4.一般情况会作为函数参数传递或者作为函数返回值

参数传递

```cs
class Test
{
  public MyDelegate myDelegate { get; set; }
  public MyDelegate2 myDelegate2 { get; set; }
  public void TestFunc(MyDelegate d1,MyDelegate2 d2)
  {
    d1();
    d2();
  }
}
static void Main(string[] args)
{
  Test test = new Test();
  test.myDelegate = delegate () { Console.WriteLine("这是第一个委托的匿名函数"); };
  test.TestFunc(test.myDelegate,
  delegate () { Console.WriteLine("这是第二个委托的匿名函数"); }
  );
}
```

返回值

```cs
static MyDelegate GetDelegate()
{
  return delegate () { Console.WriteLine("作为返回值的匿名函数"); };
}
```

**匿名函数的缺点**

添加到委托或事件容器中后不记录，无法单独移除  
因为匿名函数没有名字，所以没有办法指定移除某一个匿名函数

**练习**

{% asset_img 3.png %}

## 四、Lambda表达式

**什么是lambda表达式？**

可以讲lambda表达式理解为匿名函数的简写，它出了写法不同外，使用上和匿名函数一模一样，都是和委托或者事件配合使用的。

**lambda表达式的语法**

> （参数列表）=> {函数逻辑};

**如何使用**

**1.无参无返回**

```cs
Action a = () => { Debug.WriteLine("无参无返回"); };
```

**2.有参**

```cs
Action<int> b = (int x) => { Debug.WriteLine("有参无返回" + x); };
```

**3.参数类型可省略**

```cs
Action<int> c = (x) => { Debug.WriteLine("有参无返回" + x); };
```

**4.有返回值**

```cs
Func<int, int> d = (x) => { return x * x; };
```

其他传参使用等和匿名函数一样，缺点也和匿名函数一样

**闭包**

内层的函数可以引用包含在它外层的函数的变量，即使外层函数的执行已经终止  
注意：该变量提供的值并非变量创建时的值，而是在父函数范围内的最终值。

**练习**：

{% asset_img 4.png %}