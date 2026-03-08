---
title: Unity复习学习随笔（二）：Unity重要组件和API
date: 2025-11-29
categories: 编程笔记
tags:
  - Unity
---

## 一、最小单位：GameObject

### 1.GameObject中的成员变量

名字：name         可更改       

```cs
print(this.gameObject.name);
this.gameObject.name = "111";
```

是否激活：activeSelf

```cs
print(this.gameObject.activeSelf);
```

是否是静态：isStatic

```cs
print(this.gameObject.isStatic);
```

层级：layer         注：为int类型

```cs
GameObject obj2 = GameObject.FindWithTag("Test");
```

```cs
print(this.gameObject.layer);
```

标签：tag

```cs
print(this.gameObject.tag);
```

transform：和 this.transform 是一样的

```cs
print(this.gameObject.transform.position);
```

### 2.GameObject中的静态方法

**创建自带几何体**

只要得到了一个GameObject对象，就能够得到它身上挂载的任何 脚本 信息  
通过GetComponent方法来获取

```cs
GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
obj.name = "CUBE";
```

#### **查找对象的方法**

**1.查找单个对象**

注：只能找到激活的对象，不能找到失活的。

如果场景中存在多个满足条件的对象，无法准确确定找到的是谁

通过对象名查找：  
这个查找效率比较低下，因为它会在场景中的所有对象中去查找  
没有找到就会返回null

```cs
GameObject obj1 = GameObject.Find("Test");
if(obj1 != null)
{
  print(obj1.name);
}
```

通过tag查找：  
该方法和上面这个方法效果一样，只是名字不一样而已

```cs
GameObject obj2 = GameObject.FindWithTag("Test");
if(obj2 != null)
{
  print(obj2.name);
}
```

得到某一个单个对象，目前有两种方式：  
1.从外部面板进行关联  
2.通过API去找

**2.查找多个对象**  
找多个对象的API，只能是通过tag去找多个，通过名字是没有找多个方法的  
同样也是只能找到激活对象，不能找到失活对象

```cs
GameObject[] objs = GameObject.FindGameObjectsWithTag("Player");
```

还有几个查找对象相关是用的比较少的方法，它是由GameObject的父类 Object 提供的方法  
Unity里面的Object 不是指的万物之父 Object  
Unity里面的Object 命名空间在UnityEngine中的 Object 类，也是继承万物之父的一个自定义类  
C#中的 Object 命名空间是在 System 中的

它可以找到场景中挂载的某一个脚本对象：

```cs
Test3 test = GameObject.FindObjectByType<Test3>();
```

它的效率更低，上面的GameObject. Find 和FindWithTag找，只是遍历对象  
这个方法不仅要遍历对象，还要遍历对象上挂载的脚本

#### **实例化（克隆）对象的方法**

实例化（ 克隆 ）对象，它的作用是根据一个GameObject对象，创建出一个和它一模一样的对象

准备用来克隆的对象：  
1.直接是场景上的某个对象  
2.可以是一个预制体对象

```cs
Public GameObject obj;
void Start()
{
  GameObject.Instantiate(obj);
}
```

因为这个方法是Unity里面的Object基类提供给我们的，所以可以直接 使用

#### **删除对象的方法**

```cs
GameObject.Destroy(obj);
GameObject.Destroy(obj,5);
GameObject.Destroy(this);
```

第二个参数代表的是延迟几秒后删除，并且Destroy不仅能删除对象，还能删除脚本

删除对象有两种作用：  
1.是删除指定的一个游戏对象  
2.是删除一个指定的脚本对象  
注意：这个Destroy方法，不会马上移除对象，只是给对象加了一个移除标识  
一般情况下，它会在下一帧时把这个对象移除并从内存中移除

如果有特殊需求，一定想要马上删除，需要使用以下方法，不然建议使用上面的Destroy方法  
因为是异步的，可以降低卡顿的概率

```cs
GameObject.DestroyImmediate(obj);
```

**注：如果继承了MonoBehaviour，可以不用写GameObject就可以直接调用**

#### 过场景不移除

默认情况下，在切换场景时，场景中的对象都会被自动删除掉  
如果希望某个对象过场景不被移除  
就可以使用下面的方法：  
不想谁被移除就传谁，一般都是传依附的GameObject对象  
比如下面这句代码的意思，就是自己依附的GameObject对象过场景不被删除

```cs
GameObject.DontDestroyOnLoad(this.gameObject);
```

### 3.GameObject中的成员方法

#### 创建空物体

new一个GameObject就是在创建一个空物体，可以直接输入名字、添加脚本

```cs
GameObject obj1 = new GameObject();
GameObject obj2 = new GameObject("123123123");
GameObject obj3 = new GameObject("123123123",typeof(Test2),typeof(Test3));
```

**为对象添加脚本：**  
继承Mono的脚本是不能够被new的  
如果想要在某一个对象上动态的添加继承Mono的脚本  
直接是使用GameObject提供的方法即可

```cs
Test2 test2 = obj1.AddComponent<Test2>();
```

通过返回值，我们可以得到加入的脚本信息，用来进行一些处理

**得到脚本的成员方法和继承Mono的类得到脚本的方法一模一样**

#### 标签比较

下面两种方法是一样的

```cs
bool returnValue = this.gameObject.CompareTag("Test");
bool returnValue2 = this.gameObject.tag == "Test";
```

#### 设置激活失活

失活false，激活true

```cs
obj1.SetActive(false);
```

#### 次要的成员方法：了解即可，不建议使用

下面的方法不建议使用，效率比较低  
通过广播或者发送消息的形式，让自己或者别人执行某些行为方法

**通知自己执行什么行为**

命令自己去执行这个函数，会在自己身上挂载的所有脚本去找这个名字的函数

```cs
this.gameObject.SendMessage("TestFunction");
public void TestFunction()
{
}
```

它会去找到自己身上所有的脚本有这个名字的函数去执行  
同样的后面可以添加参数，作为函数的参数使用

#### **广播行为**

让自己和自己的子对象执行

```cs
this.gameObject.BroadcastMessage("函数名");
```

向父对象和自己发送消息并执行

```cs
this.gameObject.SendMessageUpwards("函数名");
```

### 总结

**GameObject的常用内容：**

**基本成员变量：**名字、失活激活状态、标签、层级等等

**静态方法相关：**  
创建自带几何体  
查找场景中对象  
实例化对象  
删除对象  
过场景不移除

**成员方法：**  
为对象动态添加指定脚本  
设置失活激活状态  
和Mono中相同的，得到脚本的方法

**练习**：

{% asset_img 1.png %}

## 二、时间相关：Time

时间相关内容主要用于游戏中参与位移、计时、时间暂停等等操作

### 1.时间缩放比例

**时间停止**

```cs
Time.timeScale = 0;
```

**恢复正常**

```cs
Time.timeScale = 1;
```

**2倍速**

```cs
Time.timeScale = 2;
```

### 2.帧间隔时间

帧间隔时间：最近的一帧用了多长时间，主要用来计算位移：路程=时间\*速度  
根据需求，选择参与计算的间隔时间  
如果希望游戏暂停就不动的，就使用deltaTime  
如果希望不受暂停影响，就使用unscaledDeltaTime

**受scale影响**

```cs
print(Time.deltaTime);
```

**不受scale影响的帧间隔时间**

```cs
print(Time.unscaledDeltaTime);
```

### 3.游戏开始到现在的时间

主要用来计时，一般用于单机游戏

受scale影响：

```cs
print(Time.time);
```

不受scale影响：

```cs
print(Time.unscaledTime);
```

### 4.物理帧间隔时间 FixedUpdate

```cs
print(Time.fixedDeltaTime);
print(Time.fixedUnscaledDeltaTime);
```

### 5.帧数

从开始到现在游戏跑了多少帧

```cs
print(Time.frameCount);
```

**练习**：

{% asset_img 2.png %}

## 三：Transform

#### transform主要用来干嘛？

游戏对象（GameObject）位移、旋转、缩放、父子关系、坐标转换等相关操作都由它处理  
它是Unity提供的极其重要的类

#### 位置和位移

##### 1.Vector3基础

Vector3主要是用来表示三维坐标系中的一个点，或者一个向量

**如何声明**：

```cs
Vector3 v = new Vector3();
v.x = 10;
v.y = 20;
v.z = 30;
Vector3 v2 = new Vector3(10,20);
Vector3 v3 = new Vector3(10,20,30);
Vector3 v4;
v4.y = 50;
v4.x = 100;
v4.z = 100;
```

**Vector的基本计算**：

对应坐标相加减乘除，即xyz1+xyz2，xyz1-xyz2，xyz \* 一个数，xyz / 一个数

```cs
print(v + v2);
print(v2 - v3);
print(v * 3);
print(v / 2);
```

**常用的向量**：

```cs
print(Vector3.zero);
print(Vector3.right);
print(Vector3.left);
print(Vector3.up);
print(Vector3.down);
print(Vector3.forward);
print(Vector3.back);
```

**常用的方法**：

**计算两个点之间的距离的方法**

Distance(Vector3 a,Vector3 b)

```cs
print(Vector3.Distance(v, v2));
```

##### 2.位置

**相对世界坐标系**

通过Position得到的位置，是相对于世界坐标系的原点的位置  
可能和面板上显示的是不一样的  
因为如果对象有父子关系，并且父对象位置，不在原点，那么和面板上肯定就是不一样的

```cs
print(this.transform.position);
```

**相对父对象**

这两个坐标对我们来说都很重要，如果想以面板坐标为准来进行位置设置  
那么一定是通过localPositon来进行设置的

```cs
print(this.transform.localPosition);
```

**他们两个可能出现是一样的情况：**  
1.父对象的坐标就是世界坐标系原点0，0，0  
2.对象没有父对象

注意：位置的赋值不能直接改变 x,y,z 只能整体改变（即只能更改position/localPosition整体）

**如果只想更改一个值x，y和z要保持原有坐标一致，有两种方法：**

1.直接赋值

```cs
this.transform.position = new Vector3(10, this.transform.position.y, this.transform.position.z);
```

2.先取出来再赋值

虽然不能直接更改transform的xyz，但是Vector3是可以直接改 xyz 的  
所以可以线取出来更改Vector3，再重新进行赋值

```cs
Vector3 vPos = this.transform.position;
vPos.y = 10;
this.transform.position = vPos;
```

**对象当前的各朝向**

对象当前的面朝向：

```cs
print(this.transform.forward);
```

其他用法都同上面常用的向量，但是都是实时变化的。

##### 3.位移

坐标系下的位移计算公式：  
路程 = 方向 \* 速度 \* 时间 

方式一：自己计算

```cs
this.transform.position += this.transform.forward * 1 * Time.deltaTime;
```

方式二：API

使用Translate方法：  
参数一：表示位移多少        路程 = 方向 \* 速度 \* 时间   
参数二：表示相对坐标系        默认该参数是相对于自己坐标系的

相对于世界坐标系的z轴动        始终朝世界z轴移动

```cs
this.transform.Translate(Vector3.forward * 1 * Time.deltaTime, Space.World);
```

相对于世界坐标的，自己的面朝向动        始终朝面朝向移动

```cs
this.transform.Translate(this.transform.forward * 1 * Time.deltaTime, Space.World);
```

相对于自己坐标系下的自己的面朝向向量移动（一定不会让物体这样移动）

```cs
this.transform.Translate(this.transform.forward * 1 * Time.deltaTime, Space.Self);
```

相对于自己的坐标系下的z轴正方向移动        始终朝面朝向移动

```cs
this.transform.Translate(Vector3.forward * 1 * Time.deltaTime, Space.Self);
```

注：一般使用API来进行位移

**练习**：

{% asset_img 3.png %}

#### 角度和旋转

##### **1.角度**

**相对世界坐标角度**

```cs
print(this.transform.eulerAngles);
```

**相对父对象角度**

```cs
print(this.transform.localEulerAngles);
```

**注意：设置角度和设置位置一样，不能单独设置xyz，要一起设置**

如果我们希望改变的角度是面板上显示的内容，那么改变的其实是相对父对象的角度

```cs
this.transform.eulerAngles = Vector3.zero;
```

##### **2.旋转**

**自己计算：**省略不讲，和位置一样，不停的改变角度即可

**API计算：**

**自转：**

第一个参数相当于是每一帧旋转的角度，第二个参数默认不填，就是相对于自己坐标系进行的旋转

```cs
this.transform.Rotate(new Vector3(0, 5, 0) * Time.deltaTime);
```

下面的代码是相对于世界坐标系旋转

```cs
this.transform.Rotate(new Vector3(0, 5, 0) * Time.deltaTime,Space.World);
```

**相对某个轴**：

参数一：相对哪个轴进行转动  
参数二：转动的角度是多少  
参数三：默认不填，就是相对于自己的坐标系进行旋转

下面的代码是**相对于自己**的坐标系 绕着 **y轴** 转动，angle角度为10

```cs
this.transform.Rotate(Vector3.up , 10 * Time.deltaTime);
```

**相对于某一个点转：**

参数一：相对于哪一个点转圈  
参数二：相对于那一个点的哪个轴转圈  
参数三：旋转的度数        旋转速度\*时间

```cs
this.transform.RotateAround(Vector3.zero,Vector3.up,10 * Time.deltaTime);
```

通过eulerAngles得到的角度，不会出现负数的情况  
虽然界面上显示出了负数，但是通过代码获取始终只能得到0-360度之间的数

练习：

{% asset_img 4.png %}

#### 缩放和面向

##### 1.缩放

**相对于世界坐标系**：

```cs
print(this.transform.lossyScale);
```

**相对于本地坐标系（父对象）**：

```cs
print(this.transform.localScale);
```

**注意：**  
1.同样缩放不能只改xyz，只能一起改（相对于世界坐标系的缩放大小只能得，不能改）  
所以我们一般要修改缩放大小，都是改的相对于父对象的缩放大小：localScale  
2.Unity没有提供关于缩放的API，之前的旋转位移都提供了对应的API，但是缩放并没有  
如果想要让缩放发生变化，只能自己去写（自己算）

例：

```cs
this.transform.localScale += Vector3.one * Time.deltaTime;
```

##### 2.面向

让一个对象的面朝向，可以一只看向某一个点或者某一个对象  
**看向一个点**        相对于世界坐标系

```cs
this.transform.LookAt(Vector3.zero);
```

**看向一个对象**        需要传入一个对象的Transform信息

```cs
public Transform obj1;
void Update()
{
  this.transform.LookAt(obj1);
}
```

**练习**：

{% asset_img 5.png %}

#### 父子关系 

**获取父对象**

```cs
print(this.transform.parent.name);
```

**设置父对象**

```cs
this.transform.parent = null;
this.transform.parent = GameObject.Find("Father").transform;
this.transform.SetParent(GameObject.Find("Father").transform);
```

在SetParent方法中存在重载，另一种写法：  
第一个参数：要设置为父亲的对象  
第二个：是否保留世界坐标的位置、角度、缩放等信息  
如果为true，那么会保留世界坐标下的状态，和**父对象进行计算**后得到本地坐标系的信息  
如果为false，那么不会保留，会**直接把世界坐标系下的位置状态赋值**到本地坐标系下

**和子类断绝关系**

和自己所有的儿子断绝关系，没有父子关系（不会影响到子类的子类）

```cs
this.transform.DetachChildren();
```

**获取子对象**

**按名字查找儿子**：  
找到儿子的 transform信息  
Find方法 是能过找到失活的对象的，GameObject相关的查找是不能找到失活对象的  
它只能找到自己的子类，找不到子类的子类

```cs
this.transform.Find("Child");
```

虽然它的效率比 GameObject.Find相关要高一点，但是前提是必须知道父亲是谁才能找

**遍历儿子**：

如何得到有多少子类：  
失活的子类也会算数量  
找不到孙子，所以孙子不会算数量

```cs
print(this.transform.childCount);
```

通过索引号去得到自己的子类  
如果编号溢出，会直接报错  
返回值是transform 可以得到儿子的位置相关信息

```cs
this.transform.GetChild(1);
```

```cs
for (int i = 0; i < this.transform.childCount; i++)
{
  print(this.transform.GetChild(i).name);
}
```

**子类的操作**

判断父类是谁：  
判断自己是否是参数内对象的子类

```cs
son.IsChildOf(this.transform);
```

得到自己作为子类的编号：

```cs
print(son.GetSiblingIndex());
```

把自己设为第一个子类

```cs
son.SetAsFirstSibling();
```

把自己设为最后一个子类

```cs
son.SetAsLastSibling();
```

把自己设置为指定位置的子类  
如果填的数比最大编号还大，不会报错，会直接设置成最后一个编号

```cs
son.SetSiblingIndex(3);
```

**练习**：

{% asset_img 6.png %}

练习2答案：

```cs
public static Transform CustomFind(this Transform father, string childName)
{
  Transform target = null;
  target = father.Find(childName);
  if (target != null)
  return target;
  for (int i = 0; i < father.transform.childCount; i++)
  {
    father.GetChild(i).CustomFind(childName);
    if (target != null)
    return target;
  }
  return target;
}
```

#### 坐标转换

##### 1.世界坐标转本地坐标

世界坐标系的点转换为相对本地坐标系的点，会受到缩放影响：

```cs
print(this.transform.InverseTransformPoint(Vector3.forward));
```

世界坐标系的方向转换为相对本地坐标系的方向  
不受到缩放影响：

```cs
print(this.transform.InverseTransformDirection(Vector3.forward));
```

会受到影响：

```cs
print(this.transform.InverseTransformVector(Vector3.forward));
```

世界坐标转本地坐标可以帮助我们大概判断一个相对位置

##### 2.本地坐标转世界坐标

 本地坐标系的点转换为相对世界坐标系的点，会受到缩放影响：

```cs
print(this.transform.TransformPoint(Vector3.forward));
```

本地坐标系的方向转换为相对世界坐标系的方向  
不受到缩放影响：

```cs
print(this.transform.TransformDirection(Vector3.forward));
```

会受到影响：

```cs
print(this.transform.TransformVector(Vector3.forward));
```

##### 总结

最重要的是本地坐标系的点转换为世界坐标系的店  
比如：现在玩家要在自己面前的n歌单位前，放一团火，这个时候，我不用关心世界坐标系  
通过相对于本地坐标系的位置来转换为世界坐标系的点，进行特效的创建或者攻击范围的判断

练习：

{% asset_img 7.png %}

## 四：Input和Screen

### 1.Input

注意：输入的相关内容肯定是写在Update中的

#### **鼠标在屏幕的位置**

屏幕坐标的原点是在屏幕的左下角，往右是x轴正方向，往上是y轴正方向  
返回值是Vector3，但是只有x和y有值，z一直是0，因为屏幕本来就是2d的，不存在z轴

```cs
print(Input.mousePosition);
```

#### **检测鼠标输入**

鼠标按下一瞬间就进入：  
（0左键，1右键，2中键）  
只有按下的一瞬间会触发，进入一次

```cs
if (Input.GetMouseButtonDown(0))
{
  print("左键被按下");
}
if (Input.GetMouseButtonDown(1))
{
  print("右键被按下");
}
if (Input.GetMouseButtonDown(2))
{
  print("中键被按下");
}
```

鼠标抬起一瞬间进入：

```cs
if (Input.GetMouseButtonUp(0))
{
  print("左键被松开");
}
```

鼠标长按按下抬起都会进入：

```cs
if (Input.GetMouseButton(0))
{
  print("左键被触发");
}
```

中键滚动：  
返回值y：-1下 0没有操作 1上  
它的返回值是Vector的值，我们鼠标中键滚动，会改变其中的Y值

```cs
print(Input.mouseScrollDelta);
```

#### **检测键盘输入**

键盘按下：

```cs
if (Input.GetKeyDown(KeyCode.Space))
{
  print("空格被按下");
}
```

传入字符串的重载：  
这里传入的字符串不能是大写的，不然会报错  
这里只能传入小写的字符串

```cs
if (Input.GetKeyDown("a"))
{
  print("a被按下");
}
```

键盘抬起：

```cs
if (Input.GetKeyUp(KeyCode.Space))
{
  print("空格被松开");
}
```

键盘长按：

```cs
if (Input.GetKey(KeyCode.Space))
{
  print("空格被触发");
}
```

#### **检测默认轴输入**

我们可以在Edit——Project Settings——Input Manager来更改甚至自定义轴

鼠标键盘输入主要是用来控制玩家，比如：旋转、位移等等  
所以Unity提供了更方便的方法来帮助我们控制对象的位移和旋转

键盘AD按下时，返回-1到1之间的变换  
得到的这个值就是我们的左右方向，我们可以通过它来控制对象左右移动或者旋转

```cs
Input.GetAxis("Horizontal");
```

键盘SW按下时，返回-1到1之间的变换

```cs
Input.GetAxis("Vertical");
```

鼠标横向移动时，-1 到 1 左 右

```cs
Input.GetAxis("Mouse X");
```

鼠标竖向移动时，-1 到 1 下 上

```cs
Input.GetAxis("Mouse Y");
```

GetAxisRaw方法和GetAxis使用方法相同  
只不过它的返回值只会是 -1，0，1 不会有中间值

#### **其他**

是否有任意键或者鼠标长按：

```cs
if (Input.anyKey)
print("有一个键长按");
```

是否有任意键或者鼠标按下：

```cs
if (Input.anyKeyDown)
print("有一个键按下");
```

这一帧的键盘输入：

```cs
print(Input.inputString);
```

#### **手柄输入相关**

得到连接的手柄的所有按钮名字：

```cs
string[] strs = Input.GetJoystickNames();
```

按下：

```cs
if (Input.GetButtonDown("Attack"))
{
  print("攻击键被按下");
}
```

抬起：

```cs
if (Input.GetButtonUp("Attack"))
{
  print("攻击键被松开");
}
```

长按：

```cs
if (Input.GetButton("Attack"))
{
  print("攻击键被触发");
}
```

#### **移动设备触摸相关**

```cs
if(Input.touchCount > 0)
{
  Touch t1 = Input.touches[0];
  print(t1.position);
  print(t1.deltaPosition);
}
```

是否启用多点触控：

```cs
Input.multiTouchEnabled = false;
```

陀螺仪（重力感应）  
是否开启陀螺仪，必须开启才能正常使用：

```cs
Input.gyro.enabled = true;
```

重力加速度向量：

```cs
print(Input.gyro.gravity);
```

旋转速度：

```cs
print(Input.gyro.rotationRate);
```

陀螺仪当前的旋转四元数：  
比如用这个角度信息来控制场景上的一个3d物体受到重力影响，手机怎么动它怎么动

```cs
print(Input.gyro.attitude);
```

**练习**：

{% asset_img 8.png %}

### 2.Screen

#### 静态属性

**常用**

**当前屏幕分辨率**

```cs
Resolution r = Screen.currentResolution;
```

**屏幕窗口当前宽高**  
这得到的是当前窗口的宽高，不是设备分辨率的宽高  
一般写代码要用窗口宽高，做计算时就用它们

```cs
print(Screen.width);
print(Screen.height);
```

**屏幕休眠模式**

```cs
Screen.sleepTimeout = SleepTimeout.NeverSleep;
```

**不常用**

**窗口模式**

> 独占全屏：FullScreenMode.ExclusiveFullScreen  
> 全屏窗口：FullScreenMode.FullScreenWindow  
> 最大化窗口：FullScreenMode.MaximizedWindow  
> 窗口模式：FullScreenMode.Windowed

```cs
Screen.fullScreenMode = FullScreenMode.Windowed;
```

**移动设备屏幕转向相关**

允许自动旋转为左横向，Home键在左

```cs
Screen.autorotateToLandscapeLeft = true;
```

允许自动旋转为右横向，Home键在右

```cs
Screen.autorotateToLandscapeRight = true;
```

允许自动旋转到纵向，Home键在下

```cs
Screen.autorotateToPortrait = true;
```

允许自动旋转到纵向（反），Home键在上

```cs
Screen.autorotateToPortraitUpsideDown = true;
```

**指定屏幕显示方向**

```cs
Screen.orientation = ScreenOrientation.LandscapeLeft;
```

{% asset_img 9.png %}

以上基本上都可以在发布程序的时候进行更改。

#### 静态方法

设置分辨率：一般移动设备不使用  
第三个参数为是否全屏

```cs
Screen.SetResolution(1920, 1080, false);
```

**练习**：

{% asset_img 10.png %}

## 五：Camera

### 1.Camera可编辑参数

Camera组件：

Clear Flags

> 如何清除背景：  
> skybox：天空盒  
> Solid Color：颜色填充  
> Depth only：只画该层，背景透明（一般作用于多个摄像机）  
> Don't Clear：不移除，覆盖渲染

Culling Mask

> 选择性渲染部分层级：可以指定只渲染对应层级的对象

Projection

> Perspective        透视模式  
> FOV Axis ： 视场角轴：决定了光学仪器的视野范围  
> Field of view ：视口大小  
> Physical Camera：  
> 物理摄像机：勾选后可以模拟真实世界中的摄影机、焦距、传感器尺寸等等  
>         Focal Length：焦距  
>         Sensor Type：传感器类型  
>         Sensor Size：传感器尺寸  
>         Lens Shift：透镜移位  
>         Gate Fit：闸门配合  
> orthographic：正交摄像机（一般用于2d游戏制作）：Size：摄制范围

Clipping Planes：裁剪平面距离

Viewport Rect：

视口范围，屏幕上将绘制该摄像机视图的位置（主要用于双摄像机游戏，0-1相当于宽高百分比）

Depth：渲染顺序上的深度（数字越小越先渲染）

Rendering Path：渲染路径（默认不用修改）

Target Texture：

> 渲染纹理：  
> 可以把摄像机画面渲染到一张图上：主要用于制作小地图  
> 在Project右键创建 Render Texture

Occlusion Culling：是否启用删除遮挡（可以提升一些性能）

Allow HDR：是否允许高动态范围渲染

Allow MSAA：是否允许抗锯齿

Allow Dynamic Resolution：是否允许动态分辨率呈现

Target Display：用于哪个显示器：主要用来开发有多个屏幕的平台游戏

**练习**：

{% asset_img 11.png %}

### 2.Camera代码相关

#### 重要静态成员

**1.获取摄像机**

主摄像机的获取：  
如果想通过这个方式快速获取摄像机，那么场景上必须有一个tag为MainCamera的摄像机

```cs
print(Camera.main);
```

获取摄像机的数量：

```cs
print(Camera.allCamerasCount);
```

得到所有摄像机：

```cs
Camera[] allCamera = Camera.allCameras;
print(allCamera.Length);
```

**2.渲染相关委托**

摄像机剔除前处理的委托函数

```cs
Camera.onPreCull += (c) =>
{
};
```

摄像机渲染前处理的委托

```cs
Camera.onPreRender += (c) => { };
```

摄像机渲染后处理的委托

```cs
Camera.onPostRender += (c) => { };
```

#### 重要成员

1.界面上的参数，都可以在Camera中获取到，例：可以更改主摄像机的深度

```cs
Camera.main.depth = 10;
```

2.世界坐标转屏幕坐标  
转换过后，x和y就是屏幕坐标，z对应的时这个3d物体离摄像机有多远  
我们会用这个来做的功能，最多的就是头顶血条相关的功能

```cs
Vector3 v = Camera.main.WorldToScreenPoint(this.transform.position);
```

3.屏幕坐标转世界坐标

如果不改变z，那么z默认为0，转换过去的世界坐标系的点，永远都是一个点，可以理解为视口相交的焦点  
如果改变了z，那么转换过去的世界坐标的点，就是相对于摄像机前方多少单位的横截面上的世界坐标点

```cs
print(Camera.main.ScreenToWorldPoint(Input.mousePosition));
```

**练习**：

{% asset_img 12.png %}

## 六、 光 源系统

### 1.光源组件

#### 面板参数

Type：光源类型  
        Spot：聚光灯  
                Range：发光范围距离  
                Spot Angle：光锥角度  
        Directional：方向光（环境光）  
        Point：点光源  
        Area：面光源

Color：颜色

Mode：光源模式  
        Realtime：实时光源：每帧实时计算，效果好，性能消耗大  
        Baked：烘焙光源：事先计算好，无法动态变化  
        Mixed：混合光源：预先计算+实时运算

Intensity：光源亮度

Indirect Multiplier：改变间接光的强度  
        低于1，每次反弹会使光更暗  
        大于1，每次反弹会使光更亮

Shadow Type：  
        NoShadows：关闭阴影  
        HardShadows：生硬阴影  
        SoftShadows：柔和阴影

RealtimeShadows：  
        Strength：阴影暗度0-1之间，越大越黑  
        Resolution：阴影贴图渲染分辨率，越高越逼真，消耗越高  
        Bias：阴影推离光源的距离  
        Normal Bias：阴影投射面沿法线收缩距离  
        Near Panel：渲染阴影的近裁剪面

Cookie：投影遮罩

Cookie Size：

Draw Halo：球形光环开关

Flare：耀斑

Render Mode：渲染优先级  
        Auto：运行时确定  
        Inportant：以像素质量为单位进行渲染，效果逼真，消耗大  
        Not Important：以快速方式进行渲染

Culling Mask：剔除遮罩层，决定哪些层的对象受到该光源影响

#### 代码控制

可以通过Light来点出成员变量，但是目前暂时用不到。

**练习**：

{% asset_img 13.png %}

#### 光相关面板

它需要通过Windows——Rendering——Lighting打开

Environment：环境相关设置  
        Skybox Material：天空盒材质，可以改变天空盒  
        Sun Source：太阳来源，不设置会默认使用场景中最亮的方向光代表太阳  
        Environment Lighting：环境光设置  
                Source：环境光光源颜色  
                        Skybox：天空和材质作为环境光颜色  
                        Gradient：可以为天空、地平线、地面单独选择颜色和它们之间混合  
                Intensity Multiplier：环境光亮度  
                Ambient Mode：全局光照模式，只有启用了实时全局和全局烘焙时才有用

Other：其他设置  
        Fog：雾开关  
                Color：雾颜色  
                Mode：雾计算模式  
                        Linear：随距离线性增加  
                                Start：离摄像机多远开始有雾  
                                End：离摄像机多远完全遮挡  
                        Exponential：随距离指数增加  
                                Density：强度  
                        Exponential Quare：随距离比指数更快的增加  
                                Density：强度  
        Halo Texture：光源周围挥着光环的纹理  
        Halo Strength：光环可见性  
        Flare Fade Speed：耀斑淡出时间，最初出现之后淡出的时间  
        Flare Strength：耀斑可见性  
        Spot Cookie：聚光灯剪影纹理

## 七、物理系统的碰撞检测

### 1.刚体

碰撞产生的必要条件，两个物体都有碰撞器，至少一个物体有刚体

#### RigidBody组件信息

Mass：质量（默认为千克）质量越大惯性越大  
Drag：空气阻力，根据力移动对象时影响对象的空气阻力大小，0表示没有空气阻力  
Angular Drag：根据扭矩旋转对象时影响对象的空气阻力大小。0表示没有空气阻力  
Use Gravity：是否受重力影响  
Is KineMatic：如果启用此选项，则对象将不会被物理引擎驱动，只能通过（Transform）进行操作  
对于移动平台，或者如果要动画化附加了HingeJoint的刚体，此属性将非常有用  
Interprolate：插值运算，让刚体物体移动更平滑  
        None：不应用插值运算  
        Interpolate：根据前一帧的变换来平滑变换  
        ExtraPolate：差值运算，根据下一帧的估计变换来平滑变换

Collision Detection：碰撞检测模式，用于防止快速移动的对象穿过其它对象而不检测碰撞

> Discrete：离散检测  
> 对场景中所有其他碰撞体使用离散碰撞检测。其他碰撞体在测试碰撞时会使用离散碰撞检测。用于正常碰撞（默认值）  
> Continuous：连续检测  
> 对动态碰撞体（具有刚体）使用离散碰撞检测  
> 并对静态碰撞体（没有刚体）使用连续碰撞检测  
> 设置为连续动态（Continuous Dynamic）的刚体将在测试与该刚体的碰撞时使用连续碰撞检测。（此属性对物理性能有很大影响，如果没有快速对象的碰撞问题，将其保留为Discrete设置）  
> Continuous Dynamic：连续动态检测        性能消耗高  
> 对设置为连续（Continuous）和连续动态（Continuous Dynamic）碰撞的游戏对象  使用连续的碰撞检测。还将对静态碰撞体（没有刚体）使用连续碰撞检测。  
> 对于所有其他的碰撞体，使用离散碰撞检测。用于快速移动的对象  
> Continuous Speculative：连续推测检测  
> 对刚体和碰撞体使用推测性连续碰撞检测。该方法通常比连续碰撞检测的成本更低  
> {% asset_img 14.png %}  
>            性能消耗关系：Continuous Dynamic>Continuous Speculative>Continuous>Discrete

Constraints：约束，对刚体运动的限制  
        Freeze Position：有选择的停止刚体沿世界X、Y、Z轴的移动  
        Freeze Rotation：有选择的停止刚体围绕局部XYZ轴的旋转

### 2.碰撞器

#### 3D碰撞器种类

1.盒状  
2.球状  
3.胶囊  
4.网格  
5.轮胎  
6.地形

#### 共同参数

Is Trigger：是否是触发器，如果启用此属性，则该碰撞体将用于触发事件，并被物理引擎忽略  
主要用于进行没有物理效果的碰撞检测  
Material：物理材质，可以确定碰撞体和其他对象碰撞时的交互（表现）方式  
Center：碰撞体在对象局部空间的中心点位置

#### 常用碰撞器

Box Collider：盒状碰撞器  
        Size：碰撞体在XYZ方向上的大小  
Sphere Collider：球状碰撞器  
         Radius：球形碰撞器的半径大小  
Capsule Collider：胶囊碰撞器  
        Radius：胶囊体半径  
        Height：胶囊体高度  
        Direction：胶囊体在对象局部空间中的轴向

#### 异形物体使用多种碰撞器组合

刚体对象的子对象碰撞器信息参与碰撞检测

#### 不常用碰撞器（稍微了解一下即可）

Mesh Collider（性能消耗较高）：网格碰撞器  
Wheel Collider：环状碰撞器  
Terrain Collider：地形碰撞器

### 3.物理材质

#### 物理材质参数

Dynamic Friction：已在移动时使用的摩擦力。通常为0到1之间的值，值为零就像冰一样，值为1将使对象迅速静止（除非用很大的力或重力推动对象）  
Static Friction：当对象静止在表面上时使用的摩擦力。通常为0到1之间的值。值为0就像冰一样，值为1将导致很难让对象移动。  
Bounciness：表面的弹性如何？值为0将不会反弹。值为1将在反弹时不产生任何能量损失，预计会有一些近似值，但可能只会给模拟增加少量能量  
Friction Combine：两个碰撞对象的摩擦力组合方式  
        Average：对两个摩擦值求平均值  
        Minimum：使用两个值中的最小值  
        Maximum：使用两个值中的最大值  
        Multiply：两个摩擦值相乘  
unce Combine：两个碰撞对象的弹性的组合方式。其模式与Friction Combine模式相同

### 4.碰撞检测函数

注意：**碰撞和触发响应函数**属于**特殊的生命周期函数**，也是通过反射调用的

#### 物理碰撞检测响应函数

```cs
private void OnCollisionEnter(Collision collision)
{
}
private void OnCollisionExit(Collision collision)
{
}
private void OnCollisionStay(Collision collision)
{
}
```

Collision类型的参数包含了碰到自己的对象的相关信息

关键参数：  
1.碰撞得到的对象碰撞器的信息        collision.collider  
2.碰撞对象的依附对象（GameObject）        collision.gameObject  
3.碰撞对象的依附对象的位置信息                collision.transform  
4.触碰点数相关        collision.contactCount  
5.接触点具体的坐标        ContactPoint\[\] points = collision.contacts

只要得到了碰撞到的对象的任意一个信息就可以得到它的所有信息

#### 触发器检测响应函数

```cs
private void OnTriggerEnter(Collider other)
{
}
private void OnTriggerExit(Collider other)
{
}
private void OnTriggerStay(Collider other)
{
}
```

#### 明确什么时候会响应函数

1.只要挂载的对象能和别的物体产生碰撞或者触发，那么对应的这六个函数就能够被响应  
2.六个函数都不是必写的，只需要根据需求来选择  
3.如果一个异形物体，刚体在父对象上，如果想通过子对象上挂脚本检测碰撞是不行的，必须挂载到这个刚体父对象上  
4.要明确物理碰撞和触发器响应的区别

#### 碰撞和触发器函数都可以写成虚函数，在子类重写逻辑

一般会把想要重写的碰撞和触发函数写成保护类型的，没有必要写成public，因为不会自己手动调用，因为都是Unity通过反射帮助我们调用的

**练习**：

{% asset_img 15.png %}

### 5.刚体加力

#### 刚体自带添加力的方法

给刚体加力的目标就是让其有一个速度，朝向某一个方向移动

**1.首先应该获取刚体组件**

```cs
private Rigidbody rb;
void Start()
{
rb = GetComponent<Rigidbody>();
}
```

**2.添加力**

相对世界坐标，世界坐标系 Z轴正方向加了一个力  
加力过后，对象是否停止移动，由阻力决定  
如果阻力为0，那给了一个力过后始终不会停止运动  
如果即使有阻力，也希望对象一直动，就在update中写该语句。

```cs
rb.AddForce(Vector3.forward * 10);
```

相对本地坐标

```cs
rb.AddRelativeForce(Vector3.forward * 10);
```

如果想要在世界坐标系方法中让对象相对于自己的面朝向动

```cs
rb.AddForce(this.transform.forward * 10);
```

**3.添加扭矩力，让其旋转**

相对世界坐标

```cs
rb.AddTorque(Vector3.up * 10);
```

相对本地坐标

```cs
rb.AddRelativeTorque(Vector3.up * 10);
```

**4.直接改变速度**

这个速度方向是相对于世界坐标系的，如果要直接通过改变速度让其移动，一定要注意

```cs
rb.velocity = Vector3.forward * 10;
```

**5.模拟爆炸效果**

模拟爆炸的力，一定是所有希望产生爆炸效果影响的对象  
都需要得到他们的刚体来执行这个方法，才能都有效果

```cs
rb.AddExplosionForce(10,Vector3.zero, 10);
```

#### 力的几种模式

第二个参数是力的模式，主要作用就是计算方式不同而已  
由于四种计算方式的不同，最终的移动速度就会不同

```cs
rb.AddForce(Vector3.forward * 10,ForceMode.Acceleration);
```

**动量定理**：Ft = mv        v = Ft/m  
F：力        t：时间        m：质量        v：速度

**1.Acceleration**  
给物体增加一个持续的加速度，忽略其质量  
v = Ft/m        F：（0，0，10）        t：0.02s        m：默认为1       
得出每物理帧移动0.2m/s \* 0.02 = 0.004m

**2.Force**  
给物体添加一个持续的力，与质量有关  
其他相同，m为2kg，得出v = 0.1m/s        每物理帧移动0.002m

**3.Impulse**  
给物体添加一个瞬间的力，与物体的质量有关，忽略时间，默认为1  
F：（0，0，10）        t：默认为1       m：2kg           v = 5m/s  
每物理帧移动0.1m

**4.VelocityChange**  
给物体添加一个瞬时速度，忽略质量  
F：（0，0，10）        t：默认为1       m：默认为1        v = 10m/s  
每物理帧移动0.2m

#### 力场脚本：Constant Force

#### 补充：刚体的休眠

获取刚体是否处于休眠状态，如果是，就唤醒

```cs
if (rb.IsSleeping())
{
  rb.WakeUp();
}
```

**练习**：

{% asset_img 16.png %}

## 八、音效系统

### 1.音频文件导入

**常用格式**：.wav、.mp3、.ogg、.aiff

#### 音频文件属性设置

Force To Mono：多声道转单声道  
        Normalize：强制为单声道时，混合过程中被标准化  
Load In Background：在后台加载，不阻塞主线程  
Ambisonic：立体混响声，非常适合360度视频和XR应用程序，如果音频文件包含立体混响声编码的音频，请启用此选项  
LoadType：加载类型  
        Decompress On Load：不压缩形式存在内存，加载快，但是内存占用高（适用小音效）  
        Compress in memory：压缩形式存在内存，加载慢，内存小（适用于较大音效文件）  
        Streaming：以流形式存在，使用时解码。内存占用最小，cpu消耗高（性能换内存）  
Preload Audio Data：预加载音频，勾选后进入场景就加载，不勾选，第一次使用时才加载  
Compression Format：压缩方式  
        PCM：音频以最高质量存储  
        Vorbis：相对PCM压缩的更小，根据质量决定  
        ADPCM：包含噪音，会被多次播放的声音，如碰撞声  
Quality：音频质量，确定要应用于压缩剪辑的压缩量。  
                不适用于PCM/ADPCM/HEVAG格式  
Sample Rate Setting：PCM和ADPCM压缩格式允许自动优化或者手动降低采样率  
        Preserve Sample Rate：此设置可保持采样率不变（默认）  
        Optimize Sample Rate：此设置根据分析的最高频率内容自动优化采样率  
        Override Sample Rate：此设置允许手动覆盖采样率，因此可有效地将其用于丢弃频率内容

### 2.音频源和音频监听器脚本

#### AudioSource：音频源

AudioClip：声音剪辑文件（音频文件）  
Output：默认将直接输出到场景中的音频监听器，可以更改为输出到混音器  
Mute：静音开关  
Bypass Effect：开关滤波器效果  
Bypass Listener Effects：快速开关所有监听器  
Bypass Reverb Zones：快速开关所有混响区  
Play On Awake：对象创建时就播放音乐，也就是开关启动游戏就播放  
Loop：循环  
Priority：优先级  
Volume：音量大小  
Pitch：音高  
Stereo Pan：2D声音立体声位置，相当于左右声道  
Spatial Blend：音频受3d空间的影响程度  
Reverb Zone Mix：到混响区的输出信号量  
3D Sound Settings：和Spatial Blend参数成正比应用  
        Doppler Level：多普勒效果等级  
        Spread：扩散角度设置为3D立体声还是多声道  
        Volume Rolloff：声音衰减速度  
                Logarithmic Rolloff：靠近音频源时，声音很大，但离开对象时，声音降低非常快  
                Linear Rolloff：与音频源的距离越远，听到的声音越小  
                Custom Rolloff：音频源的音频效果是根据曲线图的设置变化的  
        Min/Max Distance:最小距离内，声音保持最大响度；最大距离外，声音开始减弱

**AudioListener**：监听器  
场景中只能存在一个监听器

### 3.代码控制音频源

#### 代码控制播放停止

Play播放，Stop停止，Pause暂停，UnPause取消暂停，PlayDelayed（秒数）延迟多少秒播放

```cs
AudioSource audioSource;
void Start()
{
  audioSource = GetComponent<AudioSource>();
}
private void Update()
{
  if(Input.GetKeyDown(KeyCode.P))
    audioSource.Play();
  if(Input.GetKeyUp(KeyCode.S))
    audioSource.Stop();
}
```

#### 如何检测音效播放完毕

```cs
if (audioSource.isPlaying)
{
  print("播放中");
}
else
print("播放结束");
```

#### 如何动态控制音效播放

1.直接在要播放音效的对象上挂载脚本，控制播放  
2.实例化挂载了音效源脚本的对象（用的比较少）  
3.用一个AudioSource来控制播放不同的音效

一个GameObject可以挂载多个音效源AudioSource  
使用时要注意，如果要挂载多个，，那一定要自己管理它们，控制它们的播放，停止  
不然我们没有办法准确的获取谁是谁

### 4.麦克风输入相关

#### 获取设备麦克风信息

```cs
string[] strs = Microphone.devices;
for (int i = 0; i < strs.Length; i++)
{
  print(strs[i]);
}
```

#### 开始录制

Microphone.Start();  
参数一：设备名，传空使用默认设备  
参数二：超过录制长度后，是否重头录制  
参数三：录制时长  
参数四：采样率

```cs
AudioClip clip = Microphone.Start(null, false, 10, 44100);
```

#### 结束录制

参数：设备名，传空使用默认设备

```cs
Microphone.End(null);
```

#### 获取音频数据用于存储或者传输

用于存储数组数据的长度，是用 **声道数 \* 剪辑长度** 来决定的

```cs
float[] f = new float[clip.channels * clip.samples];
clip.GetData(f, 0);
```

## 九、场景切换和游戏退出

**场景切换**：

直接写代码切换场景可能会报错，可能原因是没有把场景加入场景列表中

```cs
if (Input.GetKeyUp(KeyCode.Space))
{
  SceneManager.LoadScene("场景名");
}
```

退出游戏：

一定是发布游戏过后才有用

```cs
if (Input.GetKeyDown(KeyCode.Escape))
{
  Application.Quit();
}
```

## 十、鼠标隐藏锁定相关

**隐藏鼠标：**

```cs
Cursor.visible = false;
```

**锁定鼠标：**

None是不锁定  
Locked锁定，鼠标会被限制在屏幕中心点，不仅会被锁定，还会被隐藏，可以通过Esc摆脱锁定  
Confined限制在窗口范围内，按ESC摆脱锁定

```cs
Cursor.lockState = CursorLockMode.Locked;
```

**设置鼠标图片：**

参数一：光标图片  
参数二：偏移位置，相对图片左上角  
参数三：平台支持的光标模式（硬件或软件）

```cs
public Texture2D pic;
Cursor.SetCursor(pic, Vector2.zero,CursorMode.Auto);
```

## 十一、随机数和Unity自带委托相关

**随机数**：

Unity中的随机数：

        使用随机数int重载，规则是左包含右不包含

```cs
Random.Range(0, 100);
```

        float重载：左包含右包含

```cs
Random.Range(0.0f, 100.0f);
```

C#中的随机数：

```cs
System.Random rand = new System.Random();
rand.Next(0,100);
```

**委托**：

C#的自带委托：

```cs
System.Action ac = () =>
{
  print("123");
};
```

Unity的自带委托：

```cs
UnityAction action = () => {
  print("123");
};
```