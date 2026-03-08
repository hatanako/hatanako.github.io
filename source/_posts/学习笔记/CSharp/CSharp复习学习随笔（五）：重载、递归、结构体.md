---
title: CSharp复习学习随笔（五）：重载、递归、结构体
date: 2025-11-05
categories: 编程笔记
tags:
  - C#
---

## 函数 重载

函数重载的概念：

> 在同一语句块中，函数（方法）名相同，但是参数的数量不同。
> 
> 或者参数的数量相同，但是参数的类型或顺序不同。
> 
> 这样我们就称其为函数重载。

函数重载的作用：

> 1.命名一组功能相似的函数，减少函数名的数量，避免命名空间的污染。
> 
> 2.提升程序的可读性。

注意：

> 重载和返回值类型无关，只和参数类型、个数、顺序有关。
> 
> 调用时，程序会自己根据传入的参数类型来判断使用哪一个重载。

用法举例：

```cs
static int CalcSum(int a, int b)
{
  return a + b;
}
static int CalcSum(int a, int b, int c)
{
  return a + b + c;
}
static double CalcSum(double a, double b)
{
  return a + b;
}
static double CalcSum(float a, double b)
{
  return a + b;
}
static double CalcSum(double a, float b)
{
  return a + b;
}
static void CalcSum(int a, int b, out int sum)
{
  sum = a + b;
}
static void CalcSum(float a, float b, ref float sum)
{
  sum = a + b;
}
static void Main(string[] args)
{
  CalcSum(1, 2);
  CalcSum(1, 2, 3);
  CalcSum(1.5, 2.5);
  CalcSum(1.5f, 2.5);
  CalcSum(2.5, 1.5f);
  int result = 1;
  CalcSum(3, 4, out result);
  float resultF = 1.0f;
  CalcSum(5, 6, ref resultF);
}
```

### 练习

{% asset_img 1.png %}

## 递归函数

递归函数的概念：递归函数就是让自己调用自己。

一个正确的递归函数必须要有结束调用的条件，并且用于条件判断的这个条件，必须能够达到停止的目的。

### 例

```cs
static void Print(int _num,int max)
{
  if (_num <= max)
  {
    System.Console.WriteLine(_num);
    Print(_num + 1, max);
  }
}
static void Main(string[] args)
{
  Print(5, 10);
}
```

### 练习

{% asset_img 2.png %}

```cs
static void Print(int _num,int max)
{
  if (_num <= max)
  {
    System.Console.WriteLine(_num);
    Print(_num + 1, max);
  }
  else
  {
    return;
  }
}
static int JieChen(int n)
{
  if (n == 1)
  {
    return 1;
  }
  else
  {
    return n * JieChen(n - 1);
  }
}
static int AddJieChen(int n)
{
  if(n == 1)
  {
    return 1;
  }
  else
  {
    return JieChen(n) + AddJieChen(n - 1);
  }
}
static float KanZhuGan(float height, int day)
{
  if (day < 10)
  {
    return KanZhuGan(height / 2, day + 1);
  }
  else
  {
    return height / 2;
  }
}
static bool Print(int _num)
{
  Console.WriteLine(_num);
  return _num == 200 || Print(_num + 1);
}
static void Main(string[] args)
{
  Print(0, 10);
  Console.WriteLine(JieChen(5));
  Console.WriteLine(AddJieChen(10));
  Console.WriteLine(KanZhuGan(100, 1));
  Print(1);
}
```

## 结构体

> 结构体是一种自定义变量类型，类似枚举，需要自己定义。
> 
> 它是数据和函数的集合。在结构体中，可以声明各种变量和方法。
> 
> 作用：用来表现存在关系的数据集合，比如用结构体表现学生、动物等等。

### 基本语法

结构体一般写在 namespace 语句块中。

结构体的关键字：struct

> struct 自定义结构体名
> 
> {
> 
>         第一部分：变量
> 
>         第二部分：构造函数（可选）
> 
>         第三部分：函数
> 
> }
> 
> 注意：结构体名字的规范是帕斯卡命名法。

### 例

```cs
struct Student
{
  public string name;
  public int age;
  public float score;
  public int birthday;
  public Student(string _name, int _age, float _score, int _birthday)
  {
    name = _name;
    age = _age;
    score = _score;
    birthday = _birthday;
  }
  public void ShowInfo()
  {
    System.Console.WriteLine("姓名：" + name);
    System.Console.WriteLine("年龄：" + age);
    System.Console.WriteLine("成绩：" + score);
    System.Console.WriteLine("生日：" + birthday);
  }
}
static void Main(string[] args)
{
}
```

### 补充：访问修饰符

用于修饰结构体中的变量和方法，是否能够被外部 使用 。

public 公共的 可以被外部访问

private 私有的 只能在内部使用

在结构体中，不写默认为private 类 型。

注意：

> 在结构体中，构造函数必须满足四个条件：
> 
> 1.没有返回值
> 
> 2.函数名必须和结构体名相同
> 
> 3.必须有参数
> 
> 4.如果声明了构造函数，那么必须在其中对所有变量数据初始化处理

### 练习

{% asset_img 3.png %}