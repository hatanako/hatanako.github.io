---
title: Unity复习学习笔记（七）：NGUI
date: 2025-12-19
categories: 编程笔记
tags:
  - Unity
---

## NGUI是什么

NGUI全称：下一代用户界面（Next-Gen UI）

它是第三方提供的Unity付费插件，专门用于执着Unity中游戏UI的第三方工具  
相对于GUI它更适用于制作游戏UI功能，更方便使用，性能和效率更高

Unity 插件：是一种基于Unity规范编写出来的程序，主要用于拓展功能，简单理解就是别人基于Unity写好的某种功能代码，我们可以直接用来处理特定的游戏逻辑

## NGUI基础组件

### 一、Root组件

控制分辨率模式自适应的基础

#### root有什么用？

Root是用于分辨率自适应的根对象，可以设置基本分辨率，相当于设置UI显示区域，并且管理所有UI控件的分辨率自适应

可以简单理解为：它管理一个UI画布，所有的UI都是显示在这个画布上的，它会管理UI画布和不同屏幕分辨率的适应关系

#### Root相关参数

Flexible：灵活模式

在该模式下，UI都是以像素为基础，100像素的物体无论在多少分辨率上都是100像素  
这就意味着，100像素在分辨率低的屏幕上可能显示正常，但是在高分辨率上就会显得很小

>         Minimum Height：屏幕高小于该值时开始按比例缩放  
>         Maximum Height：屏幕高大于该值时开始按比例缩放  
>         Shrink Portrait UI：竖屏时，按宽度来适配  
>         Adjust by DPI：使用dpi做适配计算，建议勾选

Constrained：约束

该模式下，屏幕按尺寸比例来适配，不管实际屏幕又多大，NGUI都会通过合适的缩放来适配屏幕  
这样在高分辨率上显示的UI就会被放大保持原有大小，但有可能会模糊，好处是各设备看到的UI和屏幕比例是一样的

>         Content Width：按照该宽度值适配屏幕  
>         Content Height：按照该高度值适配屏幕  
>         Fit表示以哪个值做适配  
>                 勾选Width：屏幕比例变化时，按照宽度来适配（宽度始终不变）  
>                 勾选Height：屏幕比例变化时，按照高度来适配（高度始终不变）  
>                 两个都勾选：不会被裁剪，但是有黑边  
>                                         当适配宽高比大于实际宽高比时，就会按照宽度适配，反之则高度  
>                 两个都不勾选：始终保证屏幕被UI填充满，不会有黑边，可能会被裁剪

Constrained On Mobiles：上两种模式的综合体，在PC和Mac等桌面设备上用Flexible模式，在移动设备上用Constrained模式

### 二、Panel组件

panel的用处：

管理一个UI面板的渲染顺序  
管理一个UI面板的所有子控件

参数：

{% asset_img 1.png %}

{% asset_img 2.png %}

### 三、Event System

EventSystem的作用：  
主要作用是让摄像机渲染出来的物体，能够接收到NGUI的输入事件，大部分设置不需要我们去修改  
有了它就可以来响应玩家的各种输入

参数：

{% asset_img 3.png %}

### 四、图集

**图集的作用**：

NGUI中的最小图片控件Sprite要使用图集中的图片进行显示  
图集就是把很多单独的小图合并为一张大图，合并后的大图就是图集  
目的：提高渲染性能

**如何打开图集制作工具**：

一：Project右键打开  
二：上方工具栏NGUI——Open ——Atlas Maker

**如何新建图集**：

在图集工具中创建

图集的关键文件有三：  
1.图集文件  
2.图集材质  
3.图集图片

{% asset_img 4.png %}

**如何修改删除图集元素**：

在图集工具中操作增删改。

### 五、Sprite精灵图片

Sprite的作用：

NGUI中所有中小尺寸的图片显示都是使用的Sprite  
使用它来显示图集中的单个图片资源

sprite参数：

{% asset_img 5.png %}

{% asset_img 6.png %}

公共资源参数：

{% asset_img 7.png %}

**代码设置图片**：

```css
public UISprite sprite;
void Start()
{
sprite.width = 300;
//其余都直接点出修改就行
}
```

### 六、Label控件

**Label的作用**：

NGUI所有文本显示都使用Label来显示

**Label参数**：

{% asset_img 8.png %}

{% asset_img 9.png %}

**富文本**：

{% asset_img 10.png %}

**代码设置Label**：

如何声明：  
只需声明UILabel类即可，其他同Sprite设置参数

### 七、Texture大图控件

**Texture的作用**：

Sprite只能显示图集中图片，一般用于显示中小图片  
如果使用大尺寸图片，没有必要打图集，直接使用Texture组件进行显示

**Texture参数**：

{% asset_img 11.png %}

**Texture代码设置**：

```cs
public UITexture tex;
Texture texture = Resource.Load<Texture>("");
if(texture != null)
tex.mainTexture = texture;
```

## NGUI组合控件

### 一、Button按钮

**所有组合控件的特点**：

1.在三个基础组件对象上添加对应组件

2.如果希望响应点击等事件，需要添加碰撞器

**Button的作用**：

UI界面中的按钮，当点击按钮后我们可以进行一些处理

**制作Button**：

1.一个Sprite（需要文字再加一个Label子对象）

2.为Sprite添加Button脚本

3.添加碰撞器

**Button参数**：

{% asset_img 12.png %}

**监听事件的方式**：

1.拖脚本（方法不能是私有的）

2.代码获取按钮对象监听

```cs
public UIButton button;
button.onClick.Add(new EventDelegate(ClickDo));
public void ClickDo()
{
  print("按钮点击");
}
```

**练习**：

{% asset_img 13.png %}

### 二、Toggle组件

toggle组件有什么用：单选框、多选框都可以使用它来制作

**制作Toggle**：  
1.两个Sprite 一父一子  
2.为父对象添加Toggle脚本  
3.添加碰撞器

**Toggle的参数**：

{% asset_img 14.png %}

**监听事件的两种方式**：

1.拖代码  
2.代码进行监听添加  
（同button）

**练习**：

{% asset_img 15.png %}

### 三、Input组件

input就是一个输入框，可以用来制作账号密码聊天输入框

**如何制作一个Input**：  
1.一个Sprite做背景，一个Label显示文字  
2.为Sprite添加Input脚本  
3.添加碰撞器

**Input参数**：

{% asset_img 16.png %}

{% asset_img 17.png %}

**监听事件的两种方式**：

1.拖曳脚本  
2.通过代码关联

**练习**：

{% asset_img 18.png %}

PopupList是一个下拉列表

**制作PopupList**：

1.一个sprite做背景，一个label做显示内容  
2.添加PopupList脚本  
3.添加碰撞器  
4.关联Label做信息更新，选择Label中的SetCurrentSelection函数

**参数相关**：

{% asset_img 19.png %}

{% asset_img 20.png %}

{% asset_img 21.png %}

{% asset_img 22.png %}

**监听事件的两种方式**：

1.拖曳代码  
2.通过代码添加

**练习**：

{% asset_img 23.png %}

### 五、Slider

Slider是一个滑动条控件，主要用于设置音乐音效大小等内容

**制作Slider**：

1.三个sprite，一个做根对象为背景，两个子对象，一个进度条，一个滑动块  
2.设置层级  
3.为根背景添加Slider脚本  
4.添加碰撞器（父对象或者滑块）  
5.关联三个对象

**参数相关**：

{% asset_img 24.png %}

**监听事件的两种方式**：

1.拖曳代码  
2.通过代码添加

**练习**：

{% asset_img 25.png %}

### 六、Scrollbar和Progressbar

ScrollBar滚动条——一般不单独使用，都是配合滚动视图使用，类似VS右侧的滚动条  
ProgressBar进度条——一般不咋使用，一般直接用Sprite的Filed填充模式即可

他们的参数和之前的知识很类似，所以只需了解即可

**制作ScrollBar**：

1.两个Sprite，一个背景一个滚动条  
2.背景父对象添加脚本  
3.添加碰撞器  
4.关联对象

**制作ProgressBar**：

1.两个Sprite，一个背景一个滚动条  
2.背景父对象添加脚本  
3.关联对象

**练习**：

{% asset_img 26.png %}

### 七、Scrollview

Scrollview：滚动视图，用于编程的VS代码窗口就是典型的滚动视图  
游戏中主要用于背包、商店、排行榜等功能

**制作ScrollView**：

1.直接在工具栏创建即可，NGUI——Create——ScrollView  
2.如果需要ScrollBar，自行添加水平和竖直  
3.添加子对象，为子对象添加Drag Scroll View 和碰撞器

**参数相关**：

{% asset_img 27.png %}

{% asset_img 28.png %}

{% asset_img 29.png %}

**自动对齐脚本Grid，参数相关**：

{% asset_img 30.png %}

{% asset_img 31.png %}

**练习**：

{% asset_img 32.png %}

## Anchor

Anchor是用于九宫格布局的锚点  
它有两个关键知识点  
1.老版本——锚点组件——用于控制对象的对齐方式  
2.新版本——3大基础控件自带        锚点内容——用于控制对象相对父对象布局

### **老版本**

主要用于设置面板相对屏幕的九宫格位置  
用于控制对象的对齐方式

**参数**：

{% asset_img 33.png %}

**新版本**：

用于控制对象相对父对象布局

{% asset_img 34.png %}

**练习**：

{% asset_img 35.png %}

## EventListener和EventTrigger——特殊事件监听

**控件自带事件的局限性**：

目前复合控件只提供了一些常用的事件监听方式，比如：button点击，toggle值变化等等  
如果想要制作按下、抬起、长按等功能，利用前面的知识是无法完成的

**NGUI事件响应函数**：

添加了碰撞器的对象，NGUI提供了一些利用反射调用的函数：  
经过 OnHover(bool isOver)  
按下 OnPress(bool pressed)  
点击 OnClick()  
双击 OnDoubleClick()  
拖曳开始 OnDragStart()  
拖曳中 OnDrag(Vector2 delta)  
拖曳结束 OnDragEnd()  
注：下面两个函数的参数为正在拖曳的对象  
拖曳经过某对象 OnDragOver(GameObject go)  
拖曳离开某对象 OnDragOut(GameObject go)  
等等

例：

```cs
void OnPress(bool pressed)
{
  if(pressed)
    print("按下");
}
```

**更方便的UIEventListener和UIEventTrigger**：

他们帮助我们封装了所有的特殊响应函数，可以通过它进行管理添加

1.UIEventListener：适合代码添加

```cs
public UISprite A;
public UISprite B;
void Start()
{
  UIEventListener listener = UIEventListener.Get(A.gameObject);
  listener.onPress += (obj,isPress) =>{
    print(obj.name + "被按下或者抬起，"+isPress);
  };
}
```

2.UIEventTrigger：适合Inspector面板的关联脚本添加

Listener更适合代码添加监听，Trigger更适合拖曳对象添加监听  
Listener传入的对象更具体，Trigger不会传入参数，我们需要在函数中判断处理逻辑

**练习**：

{% asset_img 36.png %}

## DrawCall

字面理解DrawCall，就是**绘制呼叫**的意思，表示CPU（中央处理器）通知GPU（图形处理器-显卡）

DrawCall的概念：

就是CPU（处理器）准备号渲染数据（顶点、纹理、发现、Shader等等）后，告知GPU（图形处理器-显卡）开始渲染（将命令放入命令缓冲区）的命令

简单来说：一次DrawCall就是CPU准备好渲染数据通知GPU渲染的这个过程

如果游戏中的DrawCall数量较高就会影响CPU的效率，最直接的感受就是游戏会卡顿

例：以拷贝文件来进行对比  
假设我们创建1w个小文件，每个文件大小为1kb，然后把这些文件拷贝到另一个文件夹中，你会发现，即使这些文件加起来不超过10mb，但是拷贝花费的时间是很长的，如果我们单独创建1个10mb的文件拷贝到另一个文件夹，基本可以瞬间拷贝完毕  
为什么会这样呢？

因为每一个文件赋值动作都需要很多额外操作，比如分配内存，创建数据等等  
这些操作就会带来一些额外的性能开销  
简单理解，文件越多额外开销就越大

渲染过程和上面的例子很类似，每次DrawCall，CPU都需要准备很多数据发送给GPU  
那么如果DrawCall越多，额外开销就越大，其实GPU的渲染效率是很强大的，往往影响渲染效率的，都是因为CPU提交命令的速度  
如果DrawCall太多，CPU就会把大量时间花在提交DrawCall上，造成CPU过载，游戏卡顿

### 如何降低DrawCall数量

在UI层面上：小图合大图→即多个小DrawCall变一次大DrawCall

#### 制作UI时降低DrawCall的技巧

通过NGUI Panel上提供的DrawCall查看工具  
注意不同图集之间的层级关系  
注意Label的层级关系

## NGUI字体

**NGUI字体的作用**：

1.降低DrawCall  
2.自定义美术字体

**制作NGUI字体**：

NGUI内部提供了字体制作工具  
1.根据字体文件，生成指定内容文字，达到降低DrawCall的目的  
2.使用第三方工具BitmapFont生成字体信息和图集，通过NGUI字体工具使用第三方工具生成的内容制作字体，达到自定义美术字体

**Unity动态字体和NGUI字体如何选择**

1.文字变化较多使用Unity动态字体，变化较少用NGUI字体  
2.想要减少DrawCall用NGUI字体  
3.美术字用NGUI字体

**练习**：

{% asset_img 37.png %}

## NGUI缓动

**什么是NGUI缓动**：

NGUI缓动就是让控件交互时，进行缩放变化、透明变化、位置变化、角度变化等等行为  
我们可以通过使用NGUI自带的Tween功能来实现这些缓动效果

**使用NGUI缓动**：

1.关键组件 Tween缓动相关组件  
2.关键组件 Play Tween可以通过它让该对象和输入事件关联

**Tween参数**：

{% asset_img 38.png %}

**Play Tween参数**：

{% asset_img 39.png %}

{% asset_img 40.png %}

**练习**：

{% asset_img 41.png %}

## **模型和粒子显示在UI前**

### **NGUI中显示模型**

**方法一：**

使用UI摄像机渲染3D模型  
1.改变NGUI的整体层级为UI层  
2.改变主摄像机和NGUI摄像机的渲染层级  
3.将想要被UI摄像机渲染的对象层级改为UI层  
4.调整模型和UI控件的Z轴距离

**方法二**：

使用多摄像机渲染 Render Texture

### **NGUI中显示粒子特效**

1.让Panel和例子特效处于一个排序层  
2.再粒子特效的Render参数中设置自己的层级

## 其他

### NGUI 事件响应、播放音效

PlaySound脚本

### NGUI控件和键盘按键绑定

KeyBinding脚本

### PC端 tab键快捷切换选中

KeyNavigation脚本

### 语言本地化

Localization脚本  
1.在Resources下创建一个txt文件，命名必须为Localization  
2.配置文件  
3.给想要切换文字的Label对象挂载Localize 关联key  
4.给用于切换语言的下拉列表下添加脚本LanguageSelection

**练习**：

{% asset_img 42.png %}