---
title: Unity复习学习随笔（五）：Unity基础
date: 2025-12-11
categories: 编程笔记
tags:
  - Unity
---


## 3D数学

### 一、Mathf

#### 1.Mathf和Math

Math是C#中封装好的用于 数学计算 的工具类 —— 位于System命名空间中  
Mathf是Unity中封装好的用于数学计算的工具类 —— 位于UnityEngine命名空间中

它们都是提供来用于进行数学计算的

#### 2.它们的区别

Mathf 和 Math 中的相关方法几乎一样  
Math 是C#自带的工具类，主要提供一些数学相关计算的方法  
Mathf 是Unity专门封装的，不仅包含Math中的方法，还多了一些适用于游戏开发的方法  
所以在进行Unity游戏开发时， 使用 Mathf中的方法进行数学计算即可 

#### 3.Mathf中的常用方法 —— 一般只计算一次

(1):Π —— PI

```cs
print(Mathf.PI);
```

(2):取绝对值 —— Abs

```cs
print(Mathf.Abs(-11));
```

(3):向上取整 —— CeilToInt

```cs
print(Mathf.CeilToInt(1.1f));
```

(4):向下取整 —— FloorToInt

```cs
print(Mathf.FloorToInt(1.9f));
```

(5):钳制函数 —— Clamp

```cs
print(Mathf.Clamp(10,11,20));
print(Mathf.Clamp(22,11,20));
print(Mathf.Clamp(15,11,20));
```

(6):获取最大值 —— Max

```cs
print(Mathf.Max(1,23,3,4,5));
```

(7):获取最小值 —— Min

```cs
print(Mathf.Min(1,23,3,4,5));
```

(8):一个数的n次幂 —— Pow

```cs
print(Mathf.Pow(2,3));
```

(9):四舍五入 —— RoundToInt

```cs
print(Mathf.RoundToInt(1.4f));
```

(10):返回一个数的平方根 —— Sqrt

```cs
print(Mathf.Sqrt(4));
```

(11):判断一个数是否是2的n次方 —— IsPowerOfTwo

返回值：True/False

```cs
print(Mathf.IsPowerOfTwo(8));
```

(12):判断正负数 —— Sign

返回值：正数为1，负数为-1

```cs
print(Mathf.Sign(-50));
```

#### 4.Mathf中的常用方法 —— 一般不停计算

(1).插值运算 —— Lerp

Lerp函数公式：result= Mathf.Lerp(start,end,t);

t作为插值系数，取值范围为1，即：result = start + (end - start) \* t

插值运算用法一：  
每帧改变start的值——变化速度先快后慢，位置无限接近，但是不会得到end位置

```cs
start = Mathf.Lerp(start,10,Time.deltaTime);
```

插值运算用法二：  
每帧改变t的值 —— 变化速度匀速，位置每帧接近，当t >=1时，得到结果

```cs
float time=0;
time += Time.deltaTime;
result = Mathf.Lerp(start, 10, time);
```

**练习**：

{% asset_img 1.png %}

### 二、三角函数

#### 1.弧度和角度相互转换

```cs
float rad = 1;
float anger = rad * Mathf.Rad2Deg;
anger = 1;
rad = anger * Mathf.Deg2Rad;
```

#### 2.三角函数

注意：Mathf中的三角函数相关函数，传入的参数需要是弧度值

```cs
print(Mathf.Sin(30 * Mathf.Deg2Rad));
```

#### 3.反三角函数

注意：反三角函数得到的结果是正弦或者余弦值对应的弧度

```cs
rad = Mathf.Asin(0.5f);
print(rad * Mathf.Rad2Deg);
```

**练习**：

{% asset_img 2.png %}

### 三、坐标系

#### 1.世界坐标系

原点：世界中心点  
轴向：世界坐标系的三个轴向都是固定的

> this.transform.position;  
> this.transform.rotation;  
> this.transform.eulerAngles;  
> this.transform.lossyScale;

#### **2.物体坐标系**

原点：物体的中心点（建模时决定）  
轴向：  
物体右方为x轴正方向  
物体上方为y轴正方向  
物体前方为z轴正方向

> this.transform.localPosition;  
> this.transform.localRotation;  
> this.transform.localEulerAngles;  
> this.transform.localScale;

#### **3.屏幕坐标系**

原点：屏幕左下角  
轴向：  
向右为x轴正方向  
向上为y轴正方向  
最大宽高：  
Screen.width  
Screen.height

> Input.mousePosition;  
> Screen.width;  
> Screen.height;

#### **4.视口坐标系**

原点：屏幕左下角  
轴向：  
向右为x轴正方向  
向上为y轴正方向  
特点：  
左下角为(0,0)  
右上角为(1,1)  
和屏幕坐标类似，将坐标单位化

摄像机上的视口范围。

#### 5.坐标转换

世界转本地：

> this.transform.InverseTransformDirection  
> this.transform.InverseTransformPoint  
> this.transform.InverseTransformVector

本地转世界：

> this.transform.TransformDirection  
> this.transform.TransformPoint  
> this.transform.TransformVector

世界转屏幕：

> Camera.main.WorldToScreenPoint

屏幕转世界：

> Camera.main.ScreenToWorldPoint

世界转视口：

> Camera.main.WorldToViewportPoint

视口转世界：

> Camera.main.ViewportToWorldPoint

**练习**：

回顾和坐标系转换的Unity知识点，进行总结。

## Vector3向量

Vector3有两种几何意义  
1.位置——代表一个点  
2.方向——代表一个方向

向量的模长 = 根号下的x² + y² + z²

在Vector3中提供了一个成员属性，用来表示向量的模长：magnitude。

```cs
Vector3 a = new Vector3(1,2,3);
print(a.magnitude);
```

得到两个点之间的距离：

```cs
Vector3 a = new Vector3(1,2,3);
Vector3 b = new Vector3(3,2,1);
print(Vector3.Distance(a,b));
```

**单位向量**

模长为1的向量为单位向量，任意一个向量经过归一化就是单位向量  
只需要方向，不想让模长影响计算结果时使用单位向量

归一化公式：  
A向量（x，y，z）得出模长|d|，转换为单位向量 = （x/|d| , y/|d| , z/|d|）

同样的，Vector3中提供了获取单位向量的成员属性：normalized

```cs
Vector3 a = new Vector3(3,3,3);
print(a.normalized);
```

**总结**

1.Vector3可以表示一个点，也可以表示一个向量，具体表示什么，是根据我们的具体需求和逻辑决定  
2.如何在Unity里面得到向量：终点减起点就可以得到向量，点A也可以代表向量，代表的就是OA向量，O就是坐标系原点  
3.得到了向量，就可以利用Vector3中提供的成员属性来得到模长和单位向量  
4.**模长**相当于可以得到两点之间的距离，**单位向量**主要是用来进行移动计算的，它不会影响我们想要的移动效果

### 向量的点乘

点乘可以得到一个向量在自己向量上投影的长度  
点乘结果＞0，两个向量夹角为锐角  
点乘结果 = 0 ，两个向量夹角为直角  
点乘结果＜ 0 ，两个向量夹角为钝角

我们可以中这个规律判断敌人的大致方位

**补充**：调试画线

```cs
public Transform target;
Debug.DrawLine(this.transform.position,
this.transform.forward+this.transform.position,
Color.red);
Debug.DrawRay(this.transform.position,
this.transform.forward,
Color.white);
```

#### 1.通过点乘判断对象方位

Vector3提供了计算点乘的方法

```cs
Debug.DrawRay(this.transform.position,this.transform.forward,Color.red);
Debug.DrawRay(this.transform.position,
(target.position - this.transform.position).normalized,
Color.red);
float dotResult = Vector3.Dot(this.transform.forward,
target.position - this.transform.position);
if(dotResult >= 0)
{
  print("它在我前面");
}
else
  print("它在我后面");
```

#### 2.通过点乘推导公式算出夹角

{% asset_img 3.png %}

```cs
float dotResult = Vector3.Dot(this.transform.forward,
(target.position - this.transform.position).normalized);
print("角度："+ Mathf.Acos(dotResult) * Mathf.Rad2Deg);
```

同样存在有API：

```cs
print("角度："+Vector3.Angle(this.transform.forward,
(target.position - this.transform.position)));
```

**练习**：

{% asset_img 4.png %}

### 向量的叉乘

#### 1.叉乘计算

```cs
public Transform A;
public Transform B;
print(Vector3.Cross(A.position,B.position));
```

#### 2.叉乘的几何意义

假如向量A和B都在XZ平面上  
向量A叉乘向量B，如果y大于0，那么B在A的右侧  
如果y小于0，那么B在A的左侧

我们也可以用叉乘得到一个平面的法向量

### 向量插值运算

#### 1.线性插值

Vector3.Lerp(start,end,t);

对两个点进行插值计算，t的取值范围为0~1

公式：result = start + (end - start) \* t

如何运用等同于上面的Mathf。

注：匀速移动，当time >= 1 时，改变了目标位置后，就会直接瞬移到新的目标位置

#### 2.球形插值

线性插值和球形插值的区别：

线性：直接平移，直线轨迹  
球形：直接旋转，弧形轨迹

写法和Lerp一模一样

```cs
time += Time.DeltaTime;
A.position = Vector3.Slerp(Vector3.right * 10,Vector3.forward * 10,time);
```

**练习**：

{% asset_img 5.png %}

## 四元数

### 1.为何使用四元数

**欧拉角**：

由三个角度(x,y,z)组成，在特定坐标系下用于描述物体的旋转量  
空间中任意旋转都可以分解成绕三个互相垂直轴的三个旋转角组成的序列

**欧拉角旋转约定**：

heading-pitch-bank  
是一种最常用的旋转序列约定  
y-x-z约定

heading：物体绕自身的对象坐标系的y轴旋转的角度  
pitch：物体绕自身的对象坐标系的x轴旋转的角度  
bank：物体绕自身的对象坐标系的z轴旋转的角度

**Unity中的欧拉角**：

Inspector窗口中调节的Rotation就是欧拉角  
this.transform.eulerAngles得到的就是欧拉角角度

**欧拉角的优缺点**：

**优点**：  
直观，易理解  
存储空间小（三个数表示）  
可以进行从一个方向到另一个方向旋转大于180度的角度

**缺点**：  
同一旋转的表示不唯一  
万向节死锁

**万向节死锁：**

当某个特定周达到某个特殊值时，绕一个轴旋转可能会覆盖住另一个轴的旋转，从而失去一维自由度  
Unity中x轴达到90度时，会产生万向节死锁

**总结**：

因为欧拉角存在一些缺点，而四元数旋转不存在万向节死锁问题  
因此在计算机中我们往往使用四元数来表示三位空间中的旋转信息

### 2.四元数是什么

四元数是简单的超复数，由实数加上三个虚数单位组成，主要用于在三维空间中表示旋转

四元数的原理包含了大量数学相关知识，较为复杂  
因此此处只对其基本构成和基本公式进行讲解，如果想要深入了解请自行查阅资料

#### 四元数的构成

一个四元数包含了一个标量和一个3D向量  
\[w,v\],w为标量，v为3D向量  
\[w,(x,y,z)\]  
对于给定的任意一个四元数：表示3D空间中的一个旋转量

##### 轴-角对

在3D空间中，任意旋转都可以表示成绕着某一个轴旋转一个角得到

注：该轴并不是空间中的xyz轴，而是任意一个轴

对于给定旋转，假设为绕着n轴，旋转β度，n轴为（x,y,z）  
那么可以构成四元数为  
四元数Q = \[cos(β/2),sin(β/2)n\]  
四元数Q = \[cos(β/2),sin(β/2)x,sin(β/2)y,sin(β/2)z\]

四元数Q则表示绕着轴n，旋转β度的旋转量

#### Unity中的四元数

Quaternion，是Unity中的四元数结构体

##### 四元数初始化方法

轴角对公式初始化

四元数Q = \[cos(β/2),sin(β/2)x,sin(β/2)y,sin(β/2)z\]  
Quaternion q = new Quaternion(sin(β/2)x,sin(β/2)y,sin(β/2)z,cos(β/2))

轴角对方法初始化

四元数Q = Quaternion.AngleAxis(角度,轴);  
Quaternion q = Quaternion.AngleAxis(60,Vector3.right);

例：绕x轴旋转60度

```cs
Quaternion q = new Quaternion(1 * Mathf.Sin(30 * Mathf.Deg2Rad),0,0,
Mathf.Cos(30 * Mathf.Deg2Rad));
Quaternion q1 = Quaternion.AngleAxis(60,Vector3.right);
```

##### 四元数和欧拉角转换

欧拉角转四元数：Quaternion.Euler(x,y,z);

四元数转欧拉角：

```cs
Quaternion q;
print(q.eulerAngles);
```

### 3.四元数的常用方法

#### 单位四元数

单位四元数表示没有旋转量（角位移）  
当角度为0或者360度时，对于给定轴都会得到单位四元数

```cs
print(Quaternion.identity);
```

使用案例：

```cs
Instantiate(testObj,Vector3.zero,Quaternion.identity);
```

#### 四元数插值运算

四元数中同样提供了如同Vector3的插值运算  
Lerp和Slerp

在四元数中Lerp和Slerp只有一些细微差别，由于算法不同，Slerp的效果会好一些  
Lerp的效果相比Slerp更快但是如果旋转范围较大效果会比较差  
所以建议使用Slerp进行插值运算

```cs
public Transform target;
public Transform A;
public Transform B;
private Quaternion start;
private float time;
private void Start()
{
  start = B.transform.rotation;
}
private void Update()
{
  A.transform.rotation =
  Quaternion.Slerp(A.transform.rotation,target.rotation,Time.deltaTime);
  time += Time.deltaTime;
  B.transform.rotation = Quaternion.Slerp(start,target.rotation,time);
}
```

#### 向量指向四元数

Quaternion.LookRotation(面朝向量);  
LookRotation方法可以将传入的面朝向量转换为对应的四元数角度 信息

例：当任务面朝向想要改变时，只需要把目标面朝向传入该函数，便可以得到目标四元数角度信息，之后将人物四元数角度信息改为得到的信息即可达到转向

**练习**：

{% asset_img 6.png %}

### 4.四元数计算

#### 四元数相乘

q3 = q1 \* q2  
两个四元数相乘得到一个新的四元数  
代表两个旋转量的叠加，相当于旋转

注意：旋转相对的坐标系，是物体自身坐标系

```cs
Quaternion q = Quaternion.AngleAxis(20,Vector3.up);
this.transform.rotation *= q;
```

#### 四元数乘向量

v2 = q1 \* v1  
四元数乘向量返回一个新的向量  
可以将指定向量旋转对应四元数的旋转量，相当于旋转向量

```cs
Vector3 v = Vector3.forward;
print(v);
v = Quaternion.AngleAxis(45,Vector3.up) * v;
print(v);
```

**练习**：

{% asset_img 7.png %}

## mono中的重要内容

### 一、延迟（延时）函数

#### 1.什么是延迟函数

延迟函数顾名思义，就是会延时执行的函数  
我们可以自己设定延时要执行的函数和具体延时的时间，是MonoBehavior基类中实现好的方法

#### 2.延迟函数的使用

##### 延迟函数Invoke：

参数一：函数名，字符串格式  
参数二：延迟时间：秒为单位

```cs
public Test1 t;
void Start()
{
  Invoke("DelayDo",1);
}
private void DelayDo()
{
  print("11111");
  Test(2);
  t.Test();
}
private void Test(int i)
{
  print(i);
}
public void Test()
{
  print("1111");
}
```

注：  
延时函数第一个参数传入的是函数名字符串  
延时函数没办法传入参数，只有包裹一层  
函数名必须是该脚本上声明的函数

##### 延迟重复执行函数：InvokeRepeating

参数一：函数名字符串  
参数二：第一次执行的延迟时间  
参数三：之后每次执行的间隔时间

```cs
void Start()
{
  InvokeRepeating("DelayRe",3,1);
}
private void DelayRe()
{
  print("重复执行");
}
```

注：注意事项和延迟函数一致

##### 取消延迟函数

取消该脚本上的所有延时函数执行

```cs
CancelInvoke();
```

指定函数名取消

只要取消了指定延迟，不管之前该函数开启了多少次延迟执行，都会统一取消

```cs
CancelInvoke("DelayDo");
```

##### 判断是否有延迟函数

```cs
if( IsInvoking() )
{
  print("存在延迟函数");
}
if( IsInvoking("DelayRe") )
{
  print("存在延迟函数DelayRe");
}
```

#### 延迟函数受对象失活销毁影响

脚本依附对象失活或者脚本自己失活  
延迟函数可以继续执行，不会受到影响

脚本依附对象销毁或者脚本移除，延迟函数无法继续执行

**练习**：

{% asset_img 8.png %}

### 二、协同程序

#### 1.Unity是否支持多线程

Unity 是支持多线程的，知识新开线程无法访问Unity相关对象的内容

注：Unity中的多线程要记得关闭

```cs
Thread t;
void Start()
{
  t = new Thread(Test);
}
private void Test()
{
  while(true)
  {
  Thread.Sleep(100);
  print("123");
  }
}
private void OnDestroy()
{
  t.Abort();
  t = null;
}
```

#### 2.协同程序是什么

协同程序简称协程，它是“假”的多线程，它不是多线程

它的主要作用是将代码分时执行，不卡主线程  
简单理解，是吧可能让主线程卡顿的耗时的逻辑分时分步执行

主要使用场景：  
异步加载文件、异步下载文件、场景异步加载、批量创建时防止卡顿

#### 3.协同程序和线程的区别

新开一个线程是独立的一个管道，和主线程并行执行  
新开一个协程是再原线程上开启，进行逻辑分时分步执行

#### 4.协程的使用

继承MonoBehavior的类，都可以开启协程函数

协程函数两个关键点：  
1.返回值为IEnumerator类型及其子类  
2.函数中通过yield return 返回值; 进行返回

```cs
void Start()
{
  Coroutine c1 = StartCoroutine(MyCoroutine(1,"123"));
}
IEnumerator MyCoroutine(int i, string str)
{
  print(i);
  yield return new WaitForSecond(1f);
  print(str);
}
```

**关闭协程**：

关闭所有：

```cs
StopAllCoroutines();
```

关闭指定协程：

```cs
StopCoroutine(c1);
```

#### 5.yield return 不同内容的含义

##### 1.下一帧执行

yield return 数字;  
yield return null;  
在Update 和LateUpdate之间执行

##### 2.等待指定秒后执行

yield return new WaitForSeconds(秒);  
在Update 和 LateUpdate 之间执行

##### 3.等待下一个固定物理帧更新时执行

yield return new WaitForFixedUpdate();  
在 FixedUpdate 和碰撞检测相关函数之后执行

##### 4.等待摄像机何GUI渲染完成后执行

yield return new WaitForEndOfFrame();  
在 LateUpdate 之后的渲染相关处理完毕后之后  
主要用来截图

##### 5.一些特殊类型的对象，比如异步加载相关函数返回的对象

在涉及到 异步加载资源、异步加载场景、网络加载时 再讲解  
一般在 Update 和 LateUpdate 之间执行

##### 6.跳出协程

yield break;

#### 6.协程受对象和组件失活销毁的影响

协程开启后，组件和物体被销毁时，协程不执行  
物体失活协程不执行，组件失活协程执行

#### 总结

Unity支持多线程，只是新开线程无法访问主线程中Unity相关内容  
一般主要用于复杂逻辑运算或者网络消息接收等等  
注：Unity中的多线程一定要记得关闭

协同程序不是多线程，它是将线程中逻辑进行分时执行，避免卡顿

继承MonoBehaviour的累都可以使用协程

协程只有当组件单独失活时不受影响，其他情况协程会停止

**练习**：

{% asset_img 9.png %}

### 三、协同程序原理

#### 1.协程的本质

协程可以分成两部分  
1.协程函数本体  
2.协程调度器

协程本体就是一个能够中间暂停返回的函数  
协程调度器是Unity内部实现的，会在对应的时机帮助我们继续执行协程函数

Unity只实现了协程调度部分  
协程的本体本质上就是一个C#的迭代器方法

#### 2.协程本体是迭代器方法的实现

1.协程函数本体：  
如果我们不通过开启协程方法执行协程  
Unity的协程调度器是不会帮助我们管理协程函数的

但是我们可以自己执行迭代器函数内容

```cs
IEnumerator Test()
{
  print("11");
  yield return 1;
  print("22");
  yield return 2;
  print("33");
  yield return 3;
  print("44");
}
void Start()
{
  IEnumerator ie = Test();
  ie.MoveNext();
  print(ie.Current);
  ie.MoveNext();
  print(ie.Current);
  ie.MoveNext();
  print(ie.Current);
  while(ie.MoveNext())
  print(ie.Current);
}
```

MoveNext 返回值代表着是否到了结尾（这个迭代器函数是否执行完毕）

2.协程调度器  
继承MonoBehaviour后，开启协程  
相当于是把一个协程函数（迭代器）放入Unity的协程调度器中帮助我们管理进行执行  
具体的yield return 后面的规则，也是Unity定义的一些规则

总结：

可以简化理解迭代器函数  
C#看到迭代器函数和yield return 语法糖，就会把原本是一个的函数变成“几部分”  
我们可以通过迭代器从上到下遍历这“几部分”进行执行  
就达到了将一个函数中的逻辑分时执行的目的

而协程调度器就是利用迭代器函数返回的内容来进行之后的处理  
比如Unity中的协程调度器  
根据yield return 返回的内容 决定了下一次在什么时候执行下一部分

理论上来说，我们可以利用迭代器函数的特点，自己实现协程调度器来取代Unity自带的调度器

**练习**：

{% asset_img 10.png %}

## Resources资源动态加载

### 一.Unity中特殊文件夹

#### 1.工程路径获取

该方式获取到的路径一般情况下只在编辑模式下使用  
我们不会在实际发布游戏后还使用该路径  
游戏发布过后，该路径就不存在了

```cs
print(Application.dataPath);
```

#### 2.Resources 资源文件夹

路径获取：一般不获取  
只能使用Resources相关API进行假爱  
如果硬要获取，可以用工程路径拼接

注意：  
需要我们自己进行创建  
作用：  
资源文件夹  
1.需要通过Resources相关API动态加载的资源需要放在其中  
2.该文件夹下所有文件都会被打包出去  
3.打包时Unity会对其压缩加密  
4.该文件夹打包后只读，只能通过Resources相关API加载

#### 3.StreamingAssets 流动资源文件夹

路径获取：

```cs
print(Application.streamingAssetsPath);
```

注意：需要自己去创建  
作用：  
流文件夹：  
1.打包出去不会被压缩加密，可以随意更改  
2.移动平台只读，PC平台可读可写  
3.可以放入一些需要自定义动态加载的初始资源

#### 4.persistentDataPath 持久数据文件夹

路径获取：

```cs
print(Application.persistentDataPath);
```

注意：不需要我们自己创建  
作用：固定数据文件夹  
1.所有平台可读可写  
2.一般用于防止动态下载或者动态创建的文件，游戏中创建或者获取的文件都放在其中

#### 5.Plugins 插件文件夹

路径获取：一般不获取

注意：需要自己创建  
作用：插件文件夹  
不同平台的插件相关文件放在其中  
比如IOS和Android平台

#### 6.Editor 编辑器文件夹

路径获取：一般不获取  
如果硬要获取可以用工程路径拼接

注意：需要我们自己创建  
作用：编辑器文件夹  
1.开发Unity编辑器时，编辑器相关脚本放在该文件夹中  
2.该文件夹的内容不会被打包出去

#### 7.Standard Assets 默认资源文件夹 

路径获取：一般不获取

注意：需要我们自己创建

作用：默认资源文件夹  
一般Unity自带资源都放在这个文件夹下，代码和资源优先被编译

**练习**：

{% asset_img 11.png %}

### 二.Resources同步加载

#### 1.Resources资源动态加载的作用

通过代码动态加载Resources文件夹下指定路径资源  
避免繁琐的拖拽操作

#### 2.常用资源类型

1.预设体对象——GameObject  
2.音效文件——AudioClip  
3.文本文件——TextAsset  
4.图片文件——Texture  
5.其他类型——需要什么用什么类型

注意：预设体对象加载需要实例化，其他资源加载一般直接用

#### 3.资源同步加载 普通方法

在一个工程当中，Resources文件夹可以有多个，通过API加载时，它会自己去这些同名的Resources文件夹中找资源  
打包时，Unity会把所有的Resources资源打包在一起。

**1.预设体对象想要创建在场景上，需要记得实例化**

第一步：要去加载预设体的资源文件(本质上就是加载配置数据到内存中)  
第二步：如果想要创建到场景上，一定是加载过配置文件过后，然后实例化

```cs
Object obj = Resources.Load("Cube");
Instantiate(obj);
```

**2.音效资源**

```cs
public AudioSource audio;
Object obj1 = Resources.Load("Music/TestMusic");
audio.clip = obj1 as AudioClip;
audio.Play();
```

**3.文本资源**  
文本资源支持的格式:.txt/.xml/.bytes/.json/.html/.csv.....

```cs
TextAsset text = Resources.Load("Txt/Test") as TextAsset;
print(text.text);
print(text.bytes);
```

**4.图片**

```cs
private Texture tex;
tex = Resources.Load("Tex/image") as Texture;
private void OnGUI()
{
  GUI.DrawTexture(new Rect(0,0,100,100),tex);
}
```

其他类型需要什么类型，就用什么类型

**5.问：资源同名怎么办**：Resources.Load加载同名资源时，无法准确加载出想要的内容，因此可以使用另外的API

**加载指定类型的资源**：

```cs
tex = Resources.Load("Tex/Test",typepf(Texture)) as Texture;
```

**加载指定名字的所有资源**：

```cs
Object[] objs = Resources.LoadAll("Tex/Test");
foreach (Object item in objs)
{
}
```

#### 4.资源同步加载 泛型方法

```cs
TextAsset text1 = Resources.Load<TextAsset>("Tex/Test");
```

**练习**

{% asset_img 12.png %}

注：多次加载不会浪费内存，但是会消耗性能

### 三.Resources异步加载

#### 1.异步加载是什么？

如果加载过大的资源可能会造成程序卡顿  
卡顿的原因就是从硬盘上吧数据读取到内存中，是需要进行计算的  
越大的资源耗时越长，就会造成掉帧卡顿

Resources异步加载，就是内部新开一个线程进行资源加载，不会造成主线程卡顿

#### 2.异步加载方法

注意：异步加载不能马上得到加载的资源，至少要等一帧

1.通过异步加载中的完成事件，监听使用加载的资源

注意：一定要等加载结束后才能使用！！

```cs
private Texture tex;
private Start()
{
  ResourceRequest request = Resources.LoadAsync<Texture>("Tex/Test");
  request.completed += LoadOver;
}
private void LoadOver(AsyncOperation rq)
{
  print("加载结束");
  tex = (rq as ResourceRequest).asset as Texture;
}
private void OnGUI()
{
  if(tex != null)
  GUI.DrawTexture(new Rect(0,0,100,100),tex);
}
```

2.通过协程使用加载的资源

```cs
void Start()
{
  StartCoroutine(Load());
}
IEnumerator Load()
{
  ResourceRequest request = Resources.LoadAsync<Texture>("Tex/Test");
  while(!request.isDone)
  {
    print(request.priority);
    yield return null;
  }
  tex = request.asset as Texture;
}
```

#### 总结

1.完成事件监听异步加载：  
好处：写法简单  
坏处：只能在资源加载结束后进行处理  
“线性加载”

2.协程异步加载：  
好处：可以在协程中处理复杂逻辑，比如同时加载多个资源，比如进度条更新  
坏处：写法稍麻烦  
“并行加载”

注意：  
理解为什么异步加载不能马上结束，为什么至少要等一帧  
理解协程异步加载的原理

**练习**：

{% asset_img 13.png %}

```cs
public class ResourceManager
{
  private static ResourceManager instance;
  public static ResourceManager Instance
  {
    get
    {
      if (instance == null)
      {
        instance = new ResourceManager();
      }
      return instance;
    }
  }
  private ResourceManager() { }
  public void LoadResource<T>(string name,UnityAction<T> callBack) where T : Object
  {
    ResourceRequest request = Resources.LoadAsync<T>(name);
    request.completed += (asyncOperation) =>
    {
      callBack((asyncOperation as ResourceRequest).asset as T);
    };
  }
}
```

### 四.Resources卸载资源

#### 1.Resources重复加载资源会浪费内存吗

其实Resources加载一次资源过后，该资源就一直存放在内存中作为缓存  
第二次加载时发现缓存中存在该资源，会直接取出来进行使用  
所以多次重复加载不会浪费内存，但是会浪费性能  
（每次加载都会去查找取出，始终伴随一些性能消耗）

#### 2.如何手动释放掉缓存中的资源

1.卸载指定资源：  
Resources.UnloadAsset 方法

注意：该方法不能释放GameObject对象，因为它会用于实例化对象  
它只能用于一些不需要实例化的内容，比如：图片、音效、文本等  
一般情况下我们很少单独使用它

```cs
GameObject obj = Resources.Load<GameObject>("Cube");
Resources.UnloadAsset(obj);
```

2.卸载未使用的资源  
注意：一般在过场景和GC一起使用

```cs
Resources.UnloadUnusedAssets();
GC.Collect();
```

## 场景异步 切换

### 一.场景同步切换的缺点

在切换场景时，Unity会删除当前场景上的所有对象，并且去加载下一个场景的相关信息  
如果当前场景对象过多，或者下一个场景对象过多  
这个过程会非常的耗时，会让玩家感受到卡顿  
所以异步切换就是来解决该问题的

### 二.场景异步切换

场景异步加载和资源异步加载几乎一致，有两种方式

#### 1.通过事件回调函数，异步加载

```cs
AsyncOperation ao = SceneManager.LoadSceneAsync("Test");
ao.completed += (a) =>
{
  print("加载结束");
};
```

#### 2.通过协程异步加载

需要注意的是，加载场景会把当前场景上没有特殊处理的对象都删除了  
所以，协程中的部分逻辑可能是执行不了的  
解决思路：让处理场景价值的脚本依附的对象，过场景时不被移除

```cs
void Start()
{
  StartCoroutine(LoadScene("Test"));
  DontDestroyOnLoad(this.gameObject);
}
IEnumerator LoadScene(string name)
{
  AsyncOperation ao = SceneManager.LoadSceneAsync(name);
  print("异步加载过程中打印的信息");
  yield return ao;
  print("异步加载结束后打印的信息");
}
```

我们可以在异步加载过程中去更新进度条  
第一种就是利用场景异步加载的进度去更新，但是不是特别准确，一般也不会直接用

第二种就是根据游戏的规则，自己定义进度条变化的条件

例如：场景加载结束更新20%，动态加载怪物更新20%，动态加载场景模型，进度条顶满，隐藏进度条。

**总结**：

场景异步加载和资源异步加载一样，有两种方式：  
1.通过事件回调函数  
2.协程异步加载

他们的优缺点表现和资源异步加载也是一样的  
1.事件回调函数  
优点：写法简单，逻辑清晰  
缺点：只能加载完场景做一些事情，不能再加载过程中处理逻辑  
2.协程异步加载  
优点：可以在加载过程中处理逻辑，比如进度条更新等  
缺点：写法较为麻烦，要通过协程

**练习**：

{% asset_img 14.png %}

```cs
public class CustomSceneManager
{
  private static CustomSceneManager instance;
  public static CustomSceneManager Instance
  {
    get
    {
      if (instance == null)
      {
        instance = new CustomSceneManager();
      }
      return instance;
    }
  }
  private CustomSceneManager() { }
  public void LoadScene(string sceneName,UnityAction action)
  {
    AsyncOperation ao = SceneManager.LoadSceneAsync(sceneName);
    ao.completed += (asyncOperation) =>
    {
      action();
    };
  }
}
```

## LineRenderer

### 一、LineRenderer是什么

LineRenderer是Unity提供的一个用于画线的组件，使用它可以在场景中绘制线段  
一般可以用于：

1.绘制攻击范围；2.武器红外线；3.辅助功能；4.其他画线功能

### 二、LineRenderer参数相关

{% asset_img 15.png %}

{% asset_img 16.png %}

{% asset_img 17.png %}

{% asset_img 18.png %}

### 三、LineRenderer代码相关

```cs
public class Test : MonoBehaviour
{
  private Material m;
  private void Start()
  {
    GameObject line = new GameObject();
    line.name = "Line";
    LineRenderer lineRenderer = line.AddComponent<LineRenderer>();
    lineRenderer.loop = true;
    lineRenderer.startWidth = 0.1f;
    lineRenderer.endWidth = 1f;
    lineRenderer.startColor = Color.red;
    lineRenderer.endColor = Color.green;
    m = Resources.Load<Material>("TestMaterial");
    lineRenderer.material = m;
    lineRenderer.positionCount = 4;
    Vector3[] positions = new Vector3[4]
    {
      new Vector3(0,0,0),
      new Vector3(0,0,5),
      new Vector3(5,0,5),
      new Vector3(5,0,0)
    };
    lineRenderer.SetPositions(positions);
    lineRenderer.useWorldSpace = false;
    lineRenderer.generateLightingData = true;
  }
}
```

**练习**：

{% asset_img 19.png %}

## 核心系统

### 一、范围检测

#### 1.回顾：物理系统之碰撞检测

碰撞产生的必要条件：  
1.至少有一个物体有刚体  
2.两个物体都必须有碰撞器

碰撞和触发  
碰撞会产生实际的物理效果  
触发看起来不会产生碰撞但是可以通过函数监听触发

碰撞检测主要用于实体物体之间产生物理效果时使用

#### 2.什么是范围检测

游戏中的瞬时攻击范围判断一般会使用范围检测  
例：  
1.玩家在前方5米处释放一个地刺魔法，在此处范围内的对象将受到地刺伤害  
2.玩家攻击，在前方1米圆形范围内对象都受到伤害等等  
类似这种并没有实体物体，只想要检测在指定某一范围内是否让敌方受到伤害时，便可以使用范围判断  
简而言之，在指定位置进行范围判断，我们就可以得到处于指定范围内的对象  
目的时对对象进行处理，比如受伤、减血等等

#### 3.如何进行范围检测

必备条件：想要被范围检测到的对象，必须具备碰撞器  
注意点：  
1.范围检测相关api，只有当执行该句代码时，进行一次范围检测，它是瞬时的  
2.范围检测相关api，并不会真正产生一个碰撞器，只是碰撞判断计算而已

**范围检测API**：  
**1.盒状范围检测**  
参数一：立方体中心点  
参数二：立方体三边大小  
参数三：立方体角度  
参数四：检测指定层级（不填检测所有层）  
参数五：是否忽略触发器   UseGlobal-使用全局设置(默认) Collide-检测触发器 Ignore-忽略触发器  
返回值：在该范围内的触发器（得到了对象触发器就可以得到对象的所有信息）

```cs
Collider[] colliders = Physics.OverlapBox( Vector3.zero,
new Vector3(1,2,3),
Quaternion.AngleAxis(45,Vector3.up),
1 << LayerMask.NameToLayer("UI") |
1 << LayerMask.NmaeToLayer("Default"),
QueryTriggerInteraction.UseGlobal);
```

**重要知识点**：

关于层级：  
通过名字得到层级编号：LayerMask.NameToLayer  
我们需要通过编号左移构建二进制数  
这样每一个编号的层级都是对应位为1的2进制数  
我们通过位运算可以选择想要检测的层级  
好处：一个int就可以表示所有想要检测的层级信息

层级编号是0-31，刚好32位，是一个int数  
每一个编号代表的都是二进制的一位

另一个API  
返回值：碰撞到的碰撞器数量  
参数：传入一个数组进行存储

```cs
Collider[] colliders1 = new Collider[];
if(Physics.OverlapBoxNonAlloc(Vector3.zero,Vector3.one,colliders1) != 0)
{
}
```

**2.球形范围检测**  
参数一：中心点  
参数二：球半径  
参数三：检测指定层级（不填检测所有层）  
参数四：是否忽略触发器（同上盒状检测）  
返回值：范围内触发器

```cs
colliders = Physics.OverlapSphere(Vector3.zero,
5,
1 << LayerMask.NameToLayer("UI") |
1 << LayerMask.NmaeToLayer("Default"),
QueryTriggerInteraction.UseGlobal);
```

另一个api  
返回值：碰撞到的碰撞器数量  
参数：传入一个数组进行存储  
Physics.OverlapSphereNonAlloc();  
使用同上

**3.胶囊检测**  
参数一：半圆一中心点  
参数二：半圆二中心点  
参数三：半圆半径  
参数四：检测指定层级  
参数五：是否忽略触发器  
返回值：在该范围内的触发器

```cs
colliders = Physics.OverlapCapsule(Vector3.zero,Vector3.up,1,1<<LayerMask.NameToLayer("Default"));
```

另一个API：Physics.OverlapCapsuleNonAlloc();

**练习**：

{% asset_img 20.png %}

### 二、射线检测

#### 1.什么是射线检测

物理系统中，目前我们学习的物体相交判断  
1.碰撞检测——必备条件：1.刚体；2.碰撞器  
2.范围检测——必备条件：碰撞器

如果想要做这样的碰撞检测呢？  
1.鼠标选择场景上一物体  
2.FPS射击游戏（无弹道，不产生实际的子弹对象进行移动）  
等等，需要判断一条线和物体的碰撞情况

射线检测就是来解决这些问题的  
它可以在指定点发射一个指定方向的射线，判断该射线与哪些碰撞器相交，得到对应对象

#### 2.射线对象

**1.3d世界中的射线**

假设有一条起点坐标（1，0，0），方向为世界坐标z轴正方向的射线

注意：  
理解参数含义  
参数一：起点  
参数二：方向（不是两点决定射线方向，第二个参数直接就代表方向向量)

Ray中的参数

```cs
Ray r = new Ray(Vector3.right,Vector3.forward);
print(r.origin);
print(r.direction);
```

**2.摄像机发出的射线**

得到一条从屏幕位置作为起点，摄像机视口方向为方向的射线

```cs
Ray r2 = Camera.main.ScreenPointToRay(Input.mousePosition);
```

注：单独的射线对于我们来说没有实际意义，需要用它结合物理系统进行射线判断

#### 3.碰撞检测函数

Physics类中提供了很多进行射线检测的静态函数  
他们有很多种重载类型，我们只需要掌握核心的几个函数，其他函数自然就明白是什么意思了  
注：射线检测也是瞬时的，执行代码时进行一次射线检测

**1.最原始的射线检测**：

参数一：射线  
参数二：检测的最大距离，超出范围不检测  
参数三：检测指定层级（不填检测所有层）  
参数四：是否忽略触发器  
返回值：bool，当碰撞到对象时，返回true，没有返回false

```cs
Ray r3 = new Ray(Vector3.zero,Vector3.forward);
if(Physics.Raycast(r3,1000,1<<LayerMask.NameToLayer("Enemy"),
QueryTriggerInteraction.UseGlobal))
print("碰撞到了对象");
```

还有一种重载，**不用传入射线**，直接传入**起点和方向**也可以用于判断

```cs
if(Physics.Raycast(Vector3.zero,Vector3.forward,1000,1<<LayerMask.NameToLayer("Enemy"),
QueryTriggerInteraction.UseGlobal))
print("碰撞到了对象");
```

**2.获取相交的单个物体信息**

物体信息类：RaycastHit  
参数一：射线  
参数二：RaycastHit时结构体，是值类型。Unity会通过out关键字在函数内部处理后，得到碰撞数据后返回到该参数中。  
参数三：距离  
参数四：检测指定层级  
参数五：是否忽略触发器

```cs
RaycastHit hitInfo;
if(Physics.Raycast(r3, out hitInfo,1000,1<<LayerMask.NameToLayer("Enemy")))
{
  print(hitInfo.collider.gameObject.name);
  print(hitInfo.point);
  print(hitInfo.normal);
  print(hitInfo.transform.position);
  print(hitInfo.distance);
}
```

RaycastHit不仅可以得到我们碰撞到的对象信息  
还可以得到一些碰撞的点、距离、法线等等的信息

还有一种重载，不用传入射线，直接传入起点和方向也可以进行判断

**3.获取相交的多个物体**

可以得到碰撞的多个对象，如果没有，就是容量为0的数组  
参数一：射线  
参数二：距离  
参数三：指定检测层级  
参数四：是否忽略触发器

```cs
RaycastHit[] hits = Physics.RaycastAll(r3,1000);
```

同样存在重载

还有一种函数，返回碰撞的数量，通过out得到数据

```cs
Physics.RaycastNonAlloc(r3,hits,1000);
```

#### 4.使用时注意的问题

距离、层级两个参数都是int类型  
当我们传入参数时，一定要明确传入的参数代表的是距离还是层级

**练习**：

{% asset_img 21.png %}