---
title: CSharp复习学习随笔（四）：类型和ref、out关键字
date: 2025-11-04
categories: 编程笔记
tags:
  - C#
---


## 值类型和引用类型的区别

### 1.使用上的区别

```cs
int a = 10;
int[] arr = { 1, 2, 3, 4, 5 };
int b = a;
int[] arr2 = arr;
Console.WriteLine($"修改前: a = {a}, b = {b}");
Console.WriteLine($"修改前: arr[0] = {arr[0]},arr2[0] = {arr2[0]} ");
b = 20;
arr2[0] = 100;
Console.WriteLine("***************************");
Console.WriteLine($"修改后: a = {a}, b = {b}");
Console.WriteLine($"修改后: arr[0] = {arr[0]},arr2[0] = {arr2[0]} ");
```

打印出来的结果为：

> 修改前: a = 10, b = 10  
> 修改前: arr\[0\] = 1,arr2\[0\] = 1  
> \*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*\*  
> 修改后: a = 10, b = 20  
> 修改后: arr\[0\] = 100,arr2\[0\] = 100

值类型在相互赋值时，把内容拷贝给了对方，所以b和a会有区别，而引用类型赋值时让两者指向相同的值，所以内容会一起改变。

那么为什么会有以上的差别？

因为值类型和引用类型存储在的内存区域是不同的，存储方式是不同的。所以造成了他们在使用上的区别。

本质上讲，值类型存储在栈空间内，这个空间是由系统分配的，小而快，并且系统会自动回收。

而引用类型存储在堆空间内，这个空间是用户手动申请和释放的，大而慢。

#### 练习

{% asset_img 1.png %}

### 引用类型的特殊例：string

```cs
string str1 = "123";
string str2 = str1;
str2 = "321";
Console.WriteLine(str1);
Console.WriteLine(str2);
```

为什么会出现这样的结果？因为C#对string进行了特殊的处理，使得它具备值类型的特征，在给新的值str2重新赋值时，就会在堆中重新分配空间，因此才会出现这样的结果。

string 虽然方便 但是有一个小缺点，如果频繁的改变string对它进行重新赋值，会产生内存垃圾。

## ref 和 out 关键字

ref 和 out 关键字作为函数参数的修饰符，他们的作用是 当传入的值类型参数在内部修改时/引用类型参数在内部重新声明时，外部的值会发生变化。

#### ref 和 out 的使用

```cs
static void ChangeValueRef(ref int _value)
{
  _value = 3;
}
static void ChangeValueOut(out int _value)
{
  _value = 5;
}
static void Main(string[] args)
{
  int a = 1;
  ChangeValueRef(ref a);
  Console.WriteLine(a);
  ChangeValueOut(out a);
}
```

#### ref 和 out 的区别

ref 传入的变量必须初始化 但是在函数内部 可改可不改

out传入的变量必须在内部赋值 但是在函数内部 必须修改该值（必须赋值）

#### 练习

{% asset_img 2.png %}

## 变长参数

举例：如果一个函数，它想要计算n个参数的和，那么函数参数的个数就不确定了。

这时候我们就需要用到变长参数params

```cs
static int Sum(params int[] arr)
{
}
```

params int\[\] 意味着可以传入n个int参数   n 可以等于0，传入的参数会存在arr数组中。

> 注意：
> 
> 1.params关键字后面必为数组
> 
> 2.数组的类型可以是任意的类型
> 
> 3.函数参数可以有别的参数和 params 关键字修饰的参数
> 
> 4.函数参数中只能最多出现一个params关键字，并且一定是在最后一组参数，前面可以有n个其他参数。

## 参数默认值

有参数默认值的参数，一般被称为可选参数，作用是当调用函数时可以不传入参数，不传就会使用默认值作为参数的值。

```cs
static void IsTrue(bool flag = false)
{
}
```

注意：

1.支持多参数默认值，每个参数都可以有默认值。

2.如果要混用，可选参数必须写在普通参数后面。

#### 练习

{% asset_img 3.png %}