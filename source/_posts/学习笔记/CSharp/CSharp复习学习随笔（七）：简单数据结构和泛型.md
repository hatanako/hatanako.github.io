---
title: CSharp复习学习随笔（七）：简单数据结构和泛型
date: 2025-11-16
categories: 编程笔记
tags:
  - C#
---


## 简单的数据结构类

### 1.Arraylist

ArrayList是一个C#为我们封装好的类，它的本质是一个object类型的数组。同时它帮助我们实现了很多方法，比如数组的增删改查（crud）。

**如何声明**：

```cs
using System.Collections;
ArrayList array = new ArrayList();
```

#### **CRUD**

##### **增**

Add 方法  
它的语法是：array.Add(value);

> array.Add("Hello");

AddRange方法  
它的语法是：array.AddRange(collection);  
例如，下面的代码将一个字符串数组添加到ArrayList中：

> array.AddRange(new string\[\] { "World", "!" });

Insert 方法  
它的语法是：array.Insert(index, value);  
例如，下面的代码将在索引1的位置插入一个新的元素"C#"：

> array.Insert(1, "C#");

InsertRange方法  
它的语法是：array.InsertRange(index, collection);  
例如，下面的代码将在索引2的位置插入一个字符串数组：

> array.Insert(2,new string\[\]{ "Welcome", "to" });

##### 删

Remove方法  
它的语法是：array.Remove(value);

> array.Remove("!");

RemoveAt方法  
它的语法是：array.RemoveAt(index);

> array.RemoveAt(0); // 移除索引0的元素 "Hello"

RemoveRange方法  
它的语法是：array.RemoveRange(index, count);

> array.RemoveRange(1, 2); // 从索引1开始移除2个元素 "to", "C#"

Clear方法  
它的语法是：array.Clear();

> array.Clear(); // 清空ArrayList中的所有元素

##### 查

Contains方法  
它的语法是：array.Contains(value);  
例如，下面的代码检查ArrayList中是否包含元素"Hello"：

> array.Add("Hello");  
> array.Add("World");  
> bool containsHello = array.Contains("Hello"); // 返回true

IndexOf方法  
参数：value - 要查找的元素，返回该元素在ArrayList中的索引，如果不存在则返回-1。

> int indexOfWorld = array.IndexOf("World"); // 返回1

LastIndexOf方法  
同上，但从ArrayList的末尾开始搜索。

直接通过索引访问元素  
可以 使用 索引直接访问ArrayList中的元素，例如：

> object firstElement = array\[0\]; // 访问第一个元素 "Hello"

##### 改

通过索引设置元素  
可以使用索引直接设置ArrayList中的元素，例如：

> array\[0\] = "Hi"; // 将第一个元素更改为 "Hi"

Sort方法  
它的语法是：array.Sort();  
例如，下面的代码对ArrayList中的元素进行排序：

> array.Sort(); // 对ArrayList中的元素进行排序

#####  遍历

Count属性  
获取ArrayList长度  
它的语法是：array.Count;

> int count = array.Count; // 返回ArrayList中的元素数量

Capacity属性  
获取容量  
它的语法是：array.Capacity;

> int count = array.Capacity;// 返回ArrayList的容量

**可以使用for循环或者foreach进行遍历，下面不再过多描述。**

#### 装箱拆箱

ArrayList本质上是一个可以自动扩容的object数组，由于用万物之父来存储数据，自然存在装箱拆箱。  
当往其中进行值类型存储时就是在装箱，将值类型对象取出来转换使用时，就存在拆箱。  
**所以尽量少用，对性能的影响较大。**

**练习：**

{% asset_img 1.png %}

练习1：  
ArrayList本质上是一个object数组的封装  
1.ArrayList可以不用一开始就定长，单独使用数组是定长的  
2.数组可以指定存储类型，ArrayList默认为object类型  
3.数组的增删需要我们自己去实现，ArrayList帮我们封装了方便的API来使用  
4.ArrayList使用时可能存在装箱拆箱，数组使用时只要不是object数组那就不存在这个问题  
5.数组长度为Length，ArrayList长度为Count

### 2.Stack

Stack（栈）是一个C#为我们封装好的类，它的本质也是object\[\]数组，只是封装了特殊的存储规则。

stack是栈存储容器，栈是一种先进后出的数据结构，先存入的数据后获取，后存入的数据先获取  
遵循**先进后出**的理念。

**如何声明：**

```cs
using System.Collections;
ArrayList array = new ArrayList();
```

#### CRUD

##### 增

使用Push方法将元素添加到堆栈的顶部

> stack.Push(1);

##### 取

使用Pop方法删除并返回堆栈顶部的元素

> var topElement = stack.Pop();

##### 查

使用Peek方法返回堆栈顶部的元素但不删除它

> var peekElement = stack.Peek();

使用Contains方法检查堆栈中是否存在特定元素

> bool containsElement = stack.Contains(1);

##### 改

堆栈不支持直接修改元素，通常需要弹出元素，修改后再推入堆栈。实在要改只能去清空它。

> stack.Clear();

##### 遍历

Count属性  
获取栈的长度

> stack.Count;

基本上用foreach来遍历  
遍历出来的顺序也是从栈顶到栈底

还有一种遍历方式是将栈转换为object数组  
遍历出来的顺序也是从栈顶到栈底

> object\[\] array = stack.ToArray();

循环弹栈

```cs
while(stack.Count > 0)
{
  var element = stack.Pop();
}
```

**装箱拆箱**：同ArrayList，不再过多赘述。

**练习**：

{% asset_img 2.png %}

### 3.Queue

Queue是一个C#为我们封装好的类，它的本质也是object\[\]数组，只是封装了特殊的存储规则

Queue是队列存储容器，队列是一种先进先出的数据结构，遵循**先进先出**的规则，即：先存入的数据先获取，后存入的数据后获取。

**队列的声明**：

同栈和ArrayList。

#### **CRUD**

##### **增**

 使用 Enqueue 方法将元素添加到队列的末尾

>  queue.Enqueue("元素1");  
>  queue.Enqueue("元素2");

##### **取**

使用 Dequeue 方法从队列的开头移除并返回元素

> var item1 = queue.Dequeue();

##### **查**

使用 Peek 方法查看队列开头的元素而不移除它

> var item2 = queue.Peek();

使用 Contains 方法查看元素是否存在于队列中

> queue.Contains(1);

##### **改**

队列不支持直接修改元素，但可以通过出队和入队的方式实现  
实在需要只能进行清空后重新添加，或者使用其他数据结构如 List 或 LinkedList

**遍历和装箱拆箱**：同ArrayList和栈（Stack）。

**练习**：

{% asset_img 3.png %}

### 4.Hashtable

哈希表（Hashtable）是基于键的哈希代码组织起来的键值对  
它的主要作用是**提高数据查询的效率**  
使用键来访问集合中的元素

**声明**方式同上。

#### CRUD

##### 增

使用 Add 方法添加元素  
第一个参数是键，第二个参数是值

> table.Add("001", "张三");

##### 删

使用 Remove 方法删除元素

> table.Remove("001");

##### 查

使用键获取值

> Console.WriteLine(table\["001"\]); //输出 张三

使用 ContainsKey/Contains 方法检查键是否存在

> table.ContainsKey("003");

使用 ContainsValue 检查值是否存在

> hashtable.ContainsValue("张三");

##### 改

使用键直接赋值修改值，无法修改键

> table\["003"\] = "王五";

**遍历**

使用 Count 属性获取长度

遍历所有键：

>  foreach (var key in table.Keys)  
>  {  
>      Console.WriteLine($"键: {key}, 值: {table\[key\]}");  
>  }

遍历所有值：

> foreach (var value in table.Values)  
> {  
>     Console.WriteLine($"值: {value}");  
> }

键值对一起遍历：

>  foreach (DictionaryEntry entry in table)  
>  {  
>      Console.WriteLine($"键: {entry.Key}, 值: {entry.Value}");  
>  }

迭代器遍历：此处暂时只做了解，后面再详细整理迭代器。

> IDictionaryEnumerator enumerator = table.GetEnumerator();  
> while (enumerator.MoveNext())  
> {  
>     Console.WriteLine($"键: {enumerator.Key}, 值: {enumerator.Value}");  
> }

**装箱拆箱：不过多描述。**

**练习：**

{% asset_img 4.png %}

## 泛型

### 1.什么是泛型？

泛型实现了类型参数化，达到代码重用的目的，通过类型参数化来实现同一份代码上操作多种类型

泛型相当于类型占位符，定义类或方法时使用替代符代表变量类型，当真正使用类或者方法时再具体指定类型。

### 2.泛型分类

泛型可以分成三类：泛型类，泛型接口和泛型函数。

### 3.泛型类和接口

**基本语法**：

> class 类名<泛型占位字母>  
> interface 接口名<泛型占位字母>

可以用泛型字母来实现泛型变量、泛型成员等等。

### 4.泛型方法（函数）

可以使用泛型类型在里面做一些逻辑处理或当作参数列表,也可以作为返回值。

**基本语法**：

> 函数名<泛型占位字母>（参数列表）{
> 
>         T t = default(T);
> 
> }

注意：泛型站为字母可以有多个，用逗号分开

在泛型类中：

```cs
class Test<T>
{
  public T value;
  public void Run(T t){}
}
```

### 5.泛型的作用

不同类型对象的相同逻辑处理就可以选择泛型，使用泛型可以一定程度上避免装箱拆箱。

例：优化ArrayList

**练习**：

{% asset_img 5.png %}

## 泛型约束

**什么是泛型约束**

让泛型的类型有一定的限制。

关键词：**where**

**泛型约束一共有六种**

> 1.值类型                                                                where 泛型字母:struct  
> 2.引用类型                                                             where 泛型字母:class  
> 3.存在无参公共构造函数                                        where 泛型字母:new()  
> 4.某个类本身或者其派生类                                   where 泛型字母:类名  
> 5.某个接口的派生类型                                           where 泛型字母:接口名  
> 6.另一个泛型类型本身或者派生类型                     where 泛型字母:另一个泛型字母

例：

```cs
class Test<T> where T:struct
```

**约束的组合使用**：使用逗号分隔，如果报错即不能组合使用，new()一般放在结尾。

**多个泛型约束**：在每个泛型前面加where即可，使用空格分隔。

练习：

{% asset_img 6.png %}

练习1：

```cs
public class singleBase<T> where T: new()
{
  private static T instance = new T();
  public static T Instance
  {
    get { return instance; }
  }
}
```

练习2：

```cs
public class NewArrayList<T>
{
  T[] array;
  int Length;
  public NewArrayList(int size)
  {
    array = new T[size];
    Length = size;
  }
  public T this[int index]
  {
    get { return array[index]; }
    set { array[index] = value; }
  }
  public void Clear()
  {
    for (int i = 0; i < array.Length; i++)
    {
      array[i] = default(T);
    }
  }
  public void Add(T item, int index)
  {
    if (index >= 0 && index < array.Length)
    {
      array[index] = item;
    }
    else
    {
      Length *= 2;
      T[] newArray = new T[Length];
      for (int i = 0; i < array.Length; i++)
      {
        newArray[i] = array[i];
      }
      newArray[index] = item;
    }
  }
  public void RemoveAt(int index)
  {
    if (index >= 0 && index < array.Length)
    {
      array[index] = default(T);
    }
  }
  public void PrintAll()
  {
    for (int i = 0; i < array.Length; i++)
    {
      System.Console.WriteLine(array[i]);
    }
  }
  public void FindItem(T item)
  {
    for (int i = 0; i < array.Length; i++)
    {
      if (array[i].Equals(item))
      {
        System.Console.WriteLine("Item found at index: " + i);
        return;
      }
    }
    System.Console.WriteLine("Item not found");
  }
}
```

## 常用泛型数据结构类

### 1.List

本质为一个可变类型的泛型数组，List类帮助我们实现了很多方法，比如泛型数组的增删查改。

**声明**：

```cs
using System.Collections;
List<int> list = new() { 1, 2, 3, 4, 5 };
```

**CRUD**：

**增**

使用Add方法

> list.Add(1);

使用AddRange方法  
可以使用数组，List等

> list.AddRange(newList);

**删**

移除指定位置的元素  
使用RemoveAt方法

> list.RemoveAt(2); //移除索引为2的元素，即数字3

移除指定值的元素  
使用Remove方法

> list.Remove(1);//移除值为1的元素

清空  
使用Clear方法

> list.Clear();

**查**

得到指定位置的元素  
直接通过索引器下标查找

正向查找元素位置  
找到返回位置，找不到返回-1  
使用IndexOf方法

> int index = list.IndexOf(4); //查找值为4的元素位置

反向查找元素位置  
使用LastIndexOf方法  
使用方法同上

查看元素是否存在  
使用Contains方法  
同ArrayList，不多讲解

**改**

使用索引器直接修改

**遍历**

获取长度：使用Count变量  
获取容量：使用Capacity变量

使用for循环或者foreach循环遍历

**练习**：

{% asset_img 7.png %}

### 2.Dictionary

可以将Dictionary理解为拥有泛型的Hashtable，它也是基于键的哈希代码组织起来的键值对  
键值对类型从Hashtable的object变为了可以自己制定的泛型。

其余不过多描述，等同于Hashtable。

**练习**：

{% asset_img 8.png %}

### 3.顺式存储和链式存储

这二者是数据结构中的两种存储结构。

#### 顺式存储

顺式存储指的是用一组地址连续的存储单元依次存储 线性 表的各个元素。  
顺式存储包含：数组、栈（Stack）、队列（Queue）、列表（List）、ArrayList  
只是他们其中部分的组织规则有所不同。

#### 链式存储

链式存储（链接存储）指的是用一组任意的存储单元存储线性表中的各个元素  
包含：单向链表、双向链表、循环链表（环形链表）

单向链表：每个元素都有一个指向后一个元素的箭头  
双向链表：每个元素都有指向前后元素的箭头  
循环链表：头尾相连

#### 如何实现一个简单的单向链表

```cs
class LinkedNode<T>
{
  public T data;
  public LinkedNode<T> nextNode;
  public LinkedNode(T value)
  {
    data = value;
    nextNode = null;
  }
}
class LinkedList<T>
{
  public LinkedNode<T> headNode;
  public LinkedNode<T> lastNode;
  public void Add(T value)
  {
    LinkedNode<T> newNode = new LinkedNode<T>(value);
    if(headNode == null)
    {
      headNode = newNode;
      lastNode = newNode;
    }
    else
    {
      lastNode.nextNode = newNode;
      lastNode = newNode;
    }
  }
  public void Remove(T value)
  {
    if (headNode == null)
    return;
    if (headNode.data.Equals(value))
    {
      headNode = headNode.nextNode;
      if(headNode == null)
      lastNode = null;
      return;
    }
    LinkedNode<T> currentNode = headNode;
    if (lastNode.data.Equals(value))
    {
      while(currentNode.nextNode != lastNode)
      {
        currentNode = currentNode.nextNode;
      }
      lastNode = currentNode;
      return;
    }
    currentNode = headNode;
    while (currentNode.nextNode != null)
    {
      if (currentNode.nextNode.data.Equals(value))
      {
        currentNode.nextNode = currentNode.nextNode.nextNode;
        return;
      }
      currentNode = currentNode.nextNode;
    }
  }
}
```

#### 二者的优缺点

从增删改查的角度思考：

> 增：链式存储计算上优于顺序存储（中间插入时链式不需要像顺序一样去移动位置）  
> 删：链式存储计算上优于顺序存储（中间删除时链式不需要像顺序一样去移动位置）  
> 查：顺序存储使用上由于链式存储（数组可以直接通过下标得到元素，链式需要遍历）  
> 改：顺序存储使用上优于链式存储（数组可以直接通过下标得到元素，链式需要遍历）

练习：

{% asset_img 9.png %}

### 4.Linkedlist

它的本质是一个可变类型的泛型双向链表。

**如何声明**：

```cs
using System.Collections;
LinkedList<int> linkedList = new LinkedList<int>();
```

链表对象需要掌握两个类：一是链表本身，二是链表节点LinkedListNode

**增**

**1.在链表尾部增加元素**

> linkedList.AddLast(10);

**2.在链表头部增加元素**

> linkedList.AddFirst(20);

**3.在某一节点后添加一个节点**

> LinkedListNode<int> n = linkedList.Find(20);  
> linkedList.AddAfter(n,11);

**4.在某一节点前添加一个节点(同上，方法名为AddBefore)**

**删**

**1.移除头节点**

> linkedList.RemoveFirst();

**2.移除尾节点**

> linkedList.RemoveLast();

**3.移除指定节点**

> linkedList.Remove(10);//变量为数值，不是位置

**4.清空**

> linkedList.Clear();

**查**

**1.头节点**

> LinkedListNode<int> first = linkedList.First;

**2.尾节点**

> LinkedListNode<int> last = linkedList.Last;

**3.找到指定值的节点（只能通过遍历查找）**

LinkedListNode<int> node = linkedList.Find(10);

**4.判断是否存在**

> bool flag = linkedList.Contain(10);

**改**

> 需要先获得节点再改变值，例：linkedList.First.Value = 30;

**遍历**

**1.foreach遍历（此处不多描述）**

**2.通过节点遍历**

> LinkedListNode<int> nowNode = linkedList.First;//或者改成Last，然后从后往前遍历  
> while(nowNode != null)  
> {  
>         //其他操作  
>         nowNode = nowNode.Next;//若为从后往前，则为Previous  
> }

**练习**：

{% asset_img 10.png %}

### 5.泛型栈和队列

使用上与Stack（栈）和Queue（队列）一模一样，此处不多描述，只是添加了一个泛型

**练习**：

{% asset_img 11.png %}

**答：**

普通线性表：  
数组，List，LinkedList  
数组：固定不变的一组数据  
List：经常改变，经常通过下标查找  
LinkedList：不确定长度的，经常临时插入改变，查找较少

先进后出：  
栈（Stack）  
对于一些可以利用先进后出存储特点的逻辑  
例：UI面板显隐规则

先进先出：  
队列（Queue）  
对于一些可以利用先进先出存储特点的逻辑  
例：消息队列，有了就往里放，然后慢慢依次处理

键值对：  
字典（Dictionary）  
需要频繁查找的，又对应关系的数据  
比如一些数据存储，id对应数据内容  
例：道具ID对应道具 信息