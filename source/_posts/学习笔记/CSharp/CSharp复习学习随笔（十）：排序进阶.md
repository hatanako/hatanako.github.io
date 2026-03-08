---
title: CSharp复习学习随笔（十）：排序进阶
date: 2025-11-21
categories: 编程笔记
tags:
  - C#
---


## 插入排序

### 1.插入排序的基本原理

通过把一串数组分为排序区和未排序区  
用一个索引值作为分水岭  
用未排序区元素与排序区元素比较直到未排序区清空

{% asset_img 1.png %}

{% asset_img 2.png %}

### 2.代码实现

前提规则：  
排序开始前，首先认为第一个元素在排序区中，其他所有元素在未排序区中  
排序开始后，每次将未排序区第一个元素取出用于和排序区中的元素进行比较（从后往前）  
满足条件（较大或者较小），则排序区中的元素往后移动一个位置

注意：所有数字都在一个数组中，所谓的两个区域是一个分水岭索引

___

首先先创建一个数组：

```cs
Random r = new Random();
int[] arr = new int[10];
for (int i = 0; i < arr.Length; i++)
{
  arr[i] = r.Next(1, 20);
}
```

第一步：  
取出未排序区的所有元素，i=1的原因：默认第一个元素在排序区

```cs
for(int i = 1; i < arr.Length; i++)
{
}
```

第二步：  
每一轮取出排序区的最后一个元素和未排序区第一个元素的索引

```cs
for(int i = 1; i < arr.Length; i++)
{
  int index = i - 1;
  int current = arr[i];
}
```

第三步：在未排序区进行比较，移动位置，确定插入索引后插入数字

```cs
for(int i = 1; i < arr.Length; i++)
{
  int index = i - 1;
  int current = arr[i];
  while (index >= 0)
  {
    if (arr[index] < current)
    break;
    arr[index + 1] = arr[index];
    index--;
  }
  arr[index+1] = current;
}
```

### 3.总结

**为什么有两层循环？**

第一层循环：一次取出未排序区的元素进行排序  
第二层循环：找到想要插入的位置

**为什么第一层循环是从1开始遍历？**

插入排序的关键是分两个区域，已排序区和未排序区，默认第一个元素在已排序区

**为什么使用while循环？**

满足条件才比较，否则证明插入的位置已确定，不需要继续循环

**为什么可以直接往后移位置？**

每轮未排序数已经被记录下来了，不会存在有空出来的位置

**为什么确定位置后，是放在index+1的位置？**

当循环停止时，插入位置应该时停止循环的索引加1处

**基本原理**：  
两个区域用索引值来区分  
未排序区和排序区的元素不停比较，找到合适位置然后插入当前元素

**套路写法**：  
两层循环，一层获取未排序区元素，一层找到合适插入位置

**注意事项**：  
默认开头已排序，第二层循环外插入

## 希尔排序

### 1.希尔排序的基本原理

**希尔排序**是插入排序的升级版，必须先掌握插入排序

**希尔排序的原理**  
将整个待排序序列分割成为若干个子序列，分别进行插入排序

总而言之，希尔排序对插入排序的升级主要就是加入了一个步长的概念  
通过步长每次可以把原序列分为多个子序列，再对子序列进行插入排序  
在极限情况下可以有效降低普通插入排序的时间复杂度，从而提升算法效率

### 2.代码实现

第一步：先实现插入排序

```cs
for(int i = 1; i < arr.Length; i++)
{
  int index = i - 1;
  int current = arr[i];
  while (index >= 0)
  {
    if (arr[index] < current)
    break;
    arr[index + 1] = arr[index];
    index--;
  }
  arr[index+1] = current;
}
```

第二步：确认步长  
基本规则：  
每次步长变化都是/2，一开始步长就是数组长度/2，之后每一次都是在上一次的步长基础上/2  
结束条件是步长<=0

```cs
for(int step = arr.Length / 2;step > 0; step/=2)
{
}
```

第三步：执行插入排序  
注：每次得到步长后，会把该步长下所有序列都进行插入排序

```cs
for(int step = arr.Length / 2;step > 0; step/=2)
{
  for (int i = step; i < arr.Length; i++)
  {
    int index = i - step;
    int current = arr[i];
    while (index >= 0)
    {
      if (arr[index] < current)
      break;
      arr[index + step] = arr[index];
      index-=step;
    }
    arr[index + step] = current;
  }
}
```

### 3.总结

**基本原理**：  
设置步长，步长不断缩小，到1排序后结束

**具体排序方式**：插入排序原理

**套路写法**：  
三层循环，一层获取步长，一层获取未排序区元素，一层找到合适位置插入

**注意事项**：  
步长确定后，会将所有子序列进行插入排序

## 归并排序

### 1.归并排序基本原理

归并 = 递归 + 合并

归并排序分为两部分：1.基本排序规则；2.递归平分数组

递归平分数组：不停进行分割，直到长度小于2停止，然后开始一层一层向上比较

基本排序规则：左右元素进行比较，依次放入新数组中，一侧没有了另一侧直接放入新数组

### 2.代码实现

第一步：  
基本排序规则

```cs
public static int[] CompareSort(int[] left, int[] right)
{
  int[] result = new int[left.Length + right.Length];
  int leftIndex = 0;
  int rightIndex = 0;
  for (int i = 0; i < result.Length; i++)
  {
    if (leftIndex >= left.Length)
    {
      result[i] = right[rightIndex++];
    }
    else if (rightIndex >= right.Length)
    {
      result[i] = left[leftIndex++];
    }
    else if (left[leftIndex] >= right[rightIndex])
    {
      result[i] = left[leftIndex];
      leftIndex++;
    }
    else
    {
      result[i] = right[rightIndex];
      rightIndex++;
    }
  }
  return result;
}
```

第二步：  
递归平分数组  
结束条件为长度小于2

```cs
public static int[] Merge(int[] array)
{
  if(array.Length<2)
  return array;
  int mid = array.Length / 2;
  int[] left = new int[mid];
  int[] right = new int[array.Length - mid];
  for(int i = 0;i<array.Length; i++)
  {
    if (i < mid)
    left[i] = array[i];
    else right[i-mid] = array[i];
  }
  return CompareSort(Merge(left), Merge(right));
}
```

### 3.总结

**理解递归逻辑**：  
一开始不会执行CompareSort函数，要先找到最小容量数组后，才会回头调用进行排序

**套路写法**：  
两个函数，一个基本排序规则，一个递归平分数组

**注意事项**：  
排序规则函数再评分数组函数的内部 return 调用

## 快速排序

### 1.快速排序基本原理

先选取一个基准值，再产生左右标准，左右比基准，如果满足条件则换位  
排完一次后，重新对基准进行定位，再对左右两遍进行递归直到有序

### 2.代码实现

第一步：声明用于快速排序的函数

```cs
public static void QuickSort(int[] array,int left,int right)
{
}
```

第二步：记录基准值，左游标和右游标

```cs
public static void QuickSort(int[] array,int left,int right)
{
  int tempLeft, tempRight, temp;
  temp = array[left];
  tempLeft = left;
  tempRight = right;
}
```

第三步：  
核心交换逻辑：左右游标会不同变化，要不相同时才能继续变化

```cs
public static void QuickSort(int[] array,int left,int right)
{
  int tempLeft, tempRight, temp;
  temp = array[left];
  tempLeft = left;
  tempRight = right;
  while(tempLeft != tempRight )
  {
    while (tempLeft < tempRight && array[tempRight] > temp)
    {
      tempRight--;
    }
    array[tempLeft] = array[tempRight];
    while(tempLeft < tempRight && array[tempLeft - 1] =< temp)
    {
      tempLeft++;
    }
    array[tempRight] = array[tempLeft];
  }
}
```

第四步：  
放置基准值，跳出循环后，把基准值放在中间位置  
此时tempRight和tempLeft一定是相等的

```cs
public static void QuickSort(int[] array,int left,int right)
{
  int tempLeft, tempRight, temp;
  temp = array[left];
  tempLeft = left;
  tempRight = right;
  while(tempLeft != tempRight )
  {
    while (tempLeft < tempRight && array[tempRight] > temp)
    {
      tempRight--;
    }
    array[tempLeft] = array[tempRight];
    while(tempLeft < tempRight && array[tempLeft - 1] =< temp)
    {
      tempLeft++;
    }
    array[tempRight] = array[tempLeft];
  }
  array[tempRight] = temp;
}
```

第五步：  
递归

```cs
public static void QuickSort(int[] array,int left,int right)
{
  int tempLeft, tempRight, temp;
  temp = array[left];
  tempLeft = left;
  tempRight = right;
  while(tempLeft != tempRight )
  {
    while (tempLeft < tempRight && array[tempRight] > temp)
    {
      tempRight--;
    }
    array[tempLeft] = array[tempRight];
    while(tempLeft < tempRight && array[tempLeft - 1] =< temp)
    {
      tempLeft++;
    }
    array[tempRight] = array[tempLeft];
  }
  array[tempRight] = temp;
  QuickSort(array, left, tempRight-1);
  QuickSort(array, tempLeft+1, right);
}
```

第六步：结束递归

```cs
public static void QuickSort(int[] array,int left,int right)
{
  if (left >= right)
  return;
  int tempLeft, tempRight, temp;
  temp = array[left];
  tempLeft = left;
  tempRight = right;
  while(tempLeft != tempRight)
  {
    while (tempLeft < tempRight && array[tempRight] > temp)
    {
      tempRight--;
    }
    array[tempLeft] = array[tempRight];
    while(tempLeft < tempRight && array[tempLeft] <= temp)
    {
      tempLeft++;
    }
    array[tempRight] = array[tempLeft];
  }
  array[tempRight] = temp;
  QuickSort(array, left, tempRight-1);
  QuickSort(array, tempLeft+1, right);
}
```

### 3.总结

归并排序和快速排序都会用到递归  
**两者的区别**：

相同点：  
1.他们都会用到递归  
2.他们都会把数组分成几部分  
不同点：  
1.归并排序递归过程中会不停产生新数组用于合并；快速排序不会产生新数组  
2.归并排序时拆分数组完毕后再进行排序；快速排序时边排序边拆分

**套路写法**：  
基准值变量；左右游标记录

3个while循环：判断是否排序结束，内部进行左右调换，重合了就结束进行下一轮排序

## 堆排序

### 1.堆排序基本原理

堆排序是一种基于**完全二叉树**结构的排序方法，它可以分为**大根堆**和**小根堆**。在堆排序中，我们首先将待排序的数组构造成一个大根堆，这样最大的元素就会位于堆的顶部。接着，我们将顶部的元素与数组末尾的元素交换，这样最大的元素就被放置在了数组的最后。然后，我们将剩余的元素再次构造成大根堆，并重复这个过程，直到整个数组变得有序。

**关键规则**：最大非叶子节点：数组长度/2 - 1

**父节点和叶子节点**：父节点为i，左节点2i + 1，右节点2i + 2

### 2.代码实现

第一步：实现父节点和左右节点比较

```cs
public static void HeapCompare(int[] a, int nowIndex,int arrayLength)
{
  int left = 2 * nowIndex + 1;
  int right = 2 * nowIndex + 2;
  int current = nowIndex;
  if(left < arrayLength && a[left] >= a[current])
  {
    current = left;
  }
  if(right < arrayLength && a[right] >= a[current])
  {
    current = right;
  }
  if(current != nowIndex)
  {
    int temp = a[nowIndex];
    a[nowIndex] = a[current];
    a[current] =temp;
    HeapCompare(a, current, arrayLength);
  }
}
```

第二步：构建大堆顶

```cs
static void BuildBigHeap(int[] a)
{
  for(int i= a.Length / 2 - 1; i >=0 ; i--)
  {
    HeapCompare(a, i, a.Length);
  }
}
```

第三步：结合大堆顶和节点比较，实现堆排序，把堆顶不停往后移动

```cs
static void HeapSort(int[] a)
{
  BuildBigHeap(a);
  for(int i= a.Length -1; i > 0; i--)
  {
    int temp = a[0];
    a[0] = a[i];
    a[i] = temp;
    HeapCompare(a, 0, i);
  }
}
```

### 3.总结

**套路写法**：  
堆顶比较；构建大堆顶；堆排序

**重要规则**：最大非叶子节点索引：数组长度 / 2 - 1

**父节点和叶子节点索引关系**：  
父节点为i，左节点2i+1，右节点2i+2

**注意**：  
堆是一类特殊的树  
堆的通用特点就是父节点会大于或者小于所有子节点  
我们并没有真正的把数组变成堆，只是利用了堆的特点来解决排序问题