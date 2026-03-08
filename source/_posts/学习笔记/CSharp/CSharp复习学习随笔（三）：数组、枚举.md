---
title: CSharp复习学习随笔（三）：数组、枚举
date: 2025-11-04
categories: 编程笔记
tags:
  - C#
---


## 一、枚举

### 基本概念

枚举是一个比较特别的存在，它是一个被命名的整型常量的集合，一般用它来表示状态、类型等等。

> 声明枚举和声明枚举变量是两个概念：
> 
> 声明枚举：相当于是创建一个自定义的枚举类型。
> 
> 声明枚举变量：使用声明的自定义枚举类型来创建一个枚举变量。

### 声明枚举的语法：

```cs
enum E_自定义枚举名
{
  自定义枚举项1,
  自定义枚举项2,
  自定义枚举项3,
  自定义枚举项4
}
enum E_Entity
{
  Player = 3,
  Skeleton,
  Slime = 100,
  Boss,
}
```

枚举一般声明在 namespace 语句块中，也可以声明在 class 语句块和 struct 语句块中。

注意：枚举不能在函数语句块中声明。

### 枚举的使用

自定义的枚举类型 变量名 = 默认值;（自定义的枚举类型.枚举项）

```cs
E_Entity entityType = E_Entity.Player;
```

一般常用于判断和switch函数。

### 枚举的类型转换

```cs
int i = (int)entityType;
Console.WriteLine(i);
entityType = 3;
string str = entityType.ToString();
Console.WriteLine(str);
entityType = (E_Entity)Enum.Parse(typeof(E_Entity),str);
Console.WriteLine(entityType);
```

> 打印结果为：
> 
> 3
> 
> Player
> 
> Player

### 枚举的作用

> 在开发中，对象很多时候会有很多状态
> 
> 比如玩家有一个动作状态，我们需要用一个变量或者标识来表示当前玩家处于的是哪种状态
> 
> 枚举可以帮助我们清晰的分清楚状态的含义。

#### 练习

{% asset_img 1.png %}

## 二、数组

数组是存储一组相同类型数据的集合，分为一维、多维、交错数组，一般情况下一维数组被简称为数组。

### 1.一维数组

#### 数组的声明

```cs
变量类型[] 数组名;
int[] arr1;
int[] arr2 = new int[5];
int[] arr3 = new int[5]{1,2,3,4,5};
int[] arr4 = new int[]{1,2,3,4};
```

#### 数组的使用

```cs
int[] arr = { 1, 2, 3, 4, 5 };
Console.WriteLine($"数组的长度: {arr.Length}");
Console.WriteLine($"数组的第一个元素: {arr[0]}");
Console.WriteLine($"数组的第三个元素: {arr[2]}");
arr[1] = 20;
Console.WriteLine($"修改后的第二个元素: {arr[1]}");
for (int i = 0; i < arr.Length; i++)
{
  Console.Write(arr[i] + ",");
}
Console.WriteLine();
foreach (var item in arr)
{
  Console.Write(item + ",");
}
Console.WriteLine();
Array.Resize(ref arr, arr.Length + 1);
arr[arr.Length - 1] = 6;
Console.WriteLine("增加元素后的数组:");
foreach (var item in arr)
{
  Console.Write(item + ",");
}
arr.ToList().Add(6);
arr[arr.Length - 1] = 7;
Console.WriteLine();
Console.WriteLine("使用LINQ增加元素后的数组:");
foreach (var item in arr)
{
  Console.Write(item + ",");
}
Console.WriteLine();
int[] newArr = new int[arr.Length - 1];
Array.Copy(arr, 0, newArr, 0, newArr.Length);
Console.WriteLine("删除元素后的数组:");
foreach (var item in newArr)
{
  Console.Write(item + ",");
}
Console.WriteLine();
arr = arr.Where((value, index) => index != arr.Length - 1).ToArray();
Console.WriteLine("使用LINQ删除元素后的数组:");
foreach (var item in arr)
{
  Console.Write(item + ",");
}
```

> **运行后的结果：**
> 
> 数组的长度: 5  
> 数组的第一个元素: 1  
> 数组的第三个元素: 3  
> 修改后的第二个元素: 20  
> 1,20,3,4,5,  
> 1,20,3,4,5,  
> 增加元素后的数组:  
> 1,20,3,4,5,6,  
> 使用LINQ增加元素后的数组:  
> 1,20,3,4,5,7,  
> 删除元素后的数组:  
> 1,20,3,4,5,  
> 使用LINQ删除元素后的数组:  
> 1,20,3,4,5,

#### 练习

{% asset_img 2.png %}

### 二维数组

> 二维数组是使用两个下标（索引）来确定元素的数组，两个下标可以理解成行标和列标
> 
> 比如矩阵
> 
> 1，2，3
> 
> 4，5，6
> 
> 就可以用二维数组 int\[2,3\]来表示

#### 二维数组的声明

```cs
int[,] arr;
int[,] arr2 = new int[3, 4];
int[,] arr3 = { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
int[,] arr4 = new int[3,3] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 } };
```

#### 二维数组的使用

```cs
int[,] arr = new int[3, 4]
{
  {1, 2, 3, 4},
  {5, 6, 7, 8},
  {9, 10, 11, 12}
};
Console.WriteLine("二维数组的长度: " + arr.Length);
Console.WriteLine("二维数组的行数: " + arr.GetLength(0));
Console.WriteLine("二维数组的列数: " + arr.GetLength(1));
Console.WriteLine("二维数组中第2行第3列的元素: " + arr[1, 2]);
for(int i = 0;i < arr.GetLength(0); i++)
{
  for(int j = 0;j < arr.GetLength(1); j++)
  {
    Console.Write(arr[i, j] + " ");
  }
}
Console.WriteLine();
int[,] newArr = new int[4, 4];
for(int i = 0;i < arr.GetLength(0); i++)
{
  for(int j = 0;j < arr.GetLength(1); j++)
  {
    newArr[i, j] = arr[i, j];
  }
}
newArr[3, 0] = 13;
Console.WriteLine("增加元素后的新数组:");
for(int i = 0;i < newArr.GetLength(0); i++)
{
  for(int j = 0;j < newArr.GetLength(1); j++)
  {
    Console.Write(newArr[i, j] + " ");
  }
}
Console.WriteLine();
int[,] smallerArr = new int[2, 4];
for(int i = 0;i < 2; i++)
{
  for(int j = 0;j < arr.GetLength(1); j++)
  {
    smallerArr[i, j] = arr[i, j];
  }
}
Console.WriteLine("删除元素后的新数组:");
for(int i = 0;i < smallerArr.GetLength(0); i++)
{
  for(int j = 0;j < smallerArr.GetLength(1); j++)
  {
    Console.Write(smallerArr[i, j] + " ");
  }
}
```

最后输出的结果如下：

> 二维数组的长度: 12  
> 二维数组的行数: 3  
> 二维数组的列数: 4  
> 二维数组中第2行第3列的元素: 7  
> 1 2 3 4 5 6 7 8 9 10 11 12  
> 增加元素后的新数组:  
> 1 2 3 4 5 6 7 8 9 10 11 12 13 0 0 0  
> 删除元素后的新数组:  
> 1 2 3 4 5 6 7 8

#### 练习

{% asset_img 3.png %}

### 交错数组

交错数组是数组的数组，每个维度的数量可以不同

注意：二维数组的每行列数相同，交错数组可以不同。

#### 交错数组的声明

```cs
int[][] arr1;
arr1 = new int[3][];
int[][] arr2 = new int[3][]
{
  new int[]{1,2,3},
  new int[]{4,5},
  new int[]{6,7,8,9}
};
int[][] arr3 = new int[][]
{
  new int[]{1,2,3},
  new int[]{4,5},
  new int[]{6,7,8,9}
};
int[][] arr4 =
{
  new int[]{1,2,3},
  new int[]{4,5},
  new int[]{6,7,8,9}
};
```

#### 交错数组的使用

```cs
int[][] arr = new int[][]
{
  new int[] { 1, 2, 3 },
  new int[] { 4, 5 },
  new int[] { 7, 8, 9 }
};
Console.WriteLine(arr.Length);
Console.WriteLine(arr[0][1]);
Console.WriteLine(arr[2][0]);
arr[1][1] = 50;
Console.WriteLine(arr[1][1]);
foreach(var subArray in arr)
{
  foreach(var item in subArray)
  {
    Console.Write(item + " ");
  }
  Console.WriteLine();
}
Console.WriteLine();
Console.WriteLine(arr[0].Length);
Console.WriteLine(arr[1].Length);
```