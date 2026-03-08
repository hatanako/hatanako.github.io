---
title: Unity复习学习笔记（九）：UGUI
date: 2026-01-20
categories: 编程笔记
tags:
  - Unity
---

UGUI 是Unity引擎内自带的UI系统，官方称之为：Unity UI  
是目前Unity商业游戏开发中使用最广泛的UI系统开发解决方案  
它是基于Unity游戏对象的UI系统，只能用来做游戏UI功能  
不能用于开发Unity编辑器中内置的用户界面

## 六大基础组件

### 如何使用？

在Hierarchy窗口中右键选择UI，UI中所有内容都是UGUI相关控件

Canvas对象上依附的：  
Canvas：画布组件，主要用于渲染UI控件  
Canvas Scaler：画布分辨率自适应组件，主要用于分辨率自适应  
Graphic Raycaster：射线事件交互组件，主要用于控制射线响应相关  
RectTransform：UI对象位置锚点控制组件，主要用于控制位置和对齐方式

EventSystem对象上依附的：  
EventSystem  
Standalone Input Module  
玩家输入事件响应系统和独立输入模块组件，主要用于监听玩家操作

### Canvas

#### canvas的作用

Canvas的意思是画布，它是UGUI中所有UI元素能够被显示的根本  
它主要负责渲染自己的所有UI子对象

如果UI控件对象不是Canvas的子对象，那么控件将不能被渲染  
我们可以通过修改Canvas组件上的参数修改渲染方式

场景中允许有多个Canvas对象  
可以分别管理不同画布的渲染方式，分辨率适应方式等等参数

如果没有特殊需求，一般只需要一个Canvas对象

#### Canvas组件的三种渲染方式

Screen Space - Overlay  
屏幕空间，覆盖模式，UI始终在前  
Pixel Perfect：是否开启无锯齿精确渲染（性能换效果）  
SortOrder：排序层编号（用于控制多个Canvas时的渲染先后顺序）  
TargetDisplay：目标设备（在哪个显示设备上显示）  
Additional Shader Channels：其他着色器通道，决定着色器可以读取哪些数据

Screen Space - Camera  
屏幕空间，摄像机模式，3D物体可以显示在UI之前  
RenderCamera：用于渲染UI的摄像机（如果不设置将 类 似于覆盖模式）  
Plane Distance：UI平面在摄像机前方的距离，类似整体Z轴的感觉  
Sorting Layer：所在排序层  
Order in Layer：排序层的序号

World Space  
世界空间，3D模式  
可以把UI对象像3D物体一样处理，常用于VR或者AR  
Event Camera：用于处理UI 事件 的摄像机（如果不设置，不能正常注册UI事件）

#### CanvasScaler

CanvasScaler是画布缩放控制器，它是用于分辨率自适应的组件  
主要负责在不同分辨率下UI控件大小自适应，并不负责位置，位置由RectTransform负责  
它主要提供了三种用于分辨率自适应的模式  
可以选择符合项目需求的方式进行分辨率自适应

画布大小和缩放系数：  
选中Canvas对象后再RectTransform组件中看到的宽高和缩放  
宽高\*缩放系数 = 屏幕分辨率

Reference Resolution：参考分辨率  
在缩放模式的宽高模式中出现的参数，参与分辨率自适应的计算

分辨率大小自适应主要是通过不同的算法计算出一个缩放系数，用该系数去缩放所有UI控件  
让其在不同分辨率下达到一个较为理想的显示效果

##### Constant Pixel Size（恒定像素模式）

无论屏幕大小如何，UI始终保持相同像素大小

Scale Factor：缩放系数，按此系数缩放画布中的所有UI元素  
Reference Pixels Per Unit：单位参考像素，多少像素对应Unity中的一个单位（默认一个单位为100像素）图片设置中的Pixel Per Unit设置，会和该参数一起参与计算

恒定像素模式计算公式：

**UI原始尺寸 = 图片大小（像素）/(Pixels Per Unit / Reference Pixels Per Unit)**

它不会让UI控件进行分辨率大小自适应，会让UI控件始终保持设置的尺寸大小显示  
一般在进行游戏开发极少使用这种模式，除非通过代码计算来设置缩放系数

##### Scale With Screen Size（缩放模式）

根据屏幕尺寸进行缩放，随着屏幕尺寸放大缩小

Reference Resolution：参考分辨率（美术出图的标准分辨率）  
缩放模式下所有匹配模式都会基于参考分辨率进行自适应计算

Screen Match Mode：屏幕匹配模式  
当前屏幕分辨率宽高比不适应参考分辨率时  
用于分辨率大小自适应的匹配模式

Expand：水平或垂直拓展画布区域，会根据宽高比的变化来放大缩小画布，可能有黑边  
Shrink：水平或垂直裁剪画布区域，会根据宽高比的变化来放大缩小画布，可能会裁剪  
Match Width Or Height：以宽高或者二者的平均值作为参考来缩放画布区域

###### Expand

拓展匹配：将Canvas Size进行宽或者高扩大，让他高于参考分辨率

计算公式：  
**缩放系数 = Mathf.Min(屏幕宽/参考分辨率宽, 屏幕高/参考分辨率高);  
画布尺寸 = 屏幕尺寸/缩放系数**

表现效果：最大程度的缩小UI元素，保留UI控件所有细节，可能留黑边

###### Shrink

收缩匹配：将Canvas Size进行宽或高收缩，让他低于参考分辨率

计算公式：  
**缩放系数 = Mathf.Max(屏幕宽/参考分辨率宽, 屏幕高/参考分辨率高);  
画布尺寸 = 屏幕尺寸/缩放系数**

表现效果：最大程度的放大UI元素，让UI元素能够填满画面，可能会出现裁剪

###### Match Width Or Height

宽高匹配：以宽高或者二者的某种平均值作为参考来缩放画布

Match：确定用于计算的宽高匹配值

计算公式：  
在取平均值之前，先取相对宽度和高度的对数

> float logWidth = Mathf.Log(屏幕宽/参考分辨率宽，2);  
> float logHeight = Mathf.Log(屏幕高/参考分辨率高，2);

在对数空间中变换是为了获得更好的性能以及更准确的结果

> float logWeightedAverage = Mathf.Lerp(logWidth,logHeight,m\_MatchWidthOrHeight);  
> scaleFactor = Mathf.Pow(2,logWeightedAverage);

表现效果：主要用于只有横屏模式或者竖屏模式的游戏

竖屏游戏：Match = 0  
将画布宽度设置为参考分辨率的宽度并保持比例不变，屏幕变高可能会有黑边

横屏游戏：Match = 1  
将画布高度设置为参考分辨率的高度，并保持比例不变，屏幕变长可能会有黑边

###### 使用建议

**游戏开发一般使用Scale With Screen Size 缩放模式**  
存在横竖屏切换选择Expend和Shrink  
不存在选择Match Width or Height

##### Constant Physical Size（恒定物理模式）

无论屏幕大小和分辨率如何，UI元素始终保持相同物理大小

DPI：（Dots Per Inch，每英寸点数）图像每英寸长度内的像素点数  
Physical Unit：物理单位，使用的物理单位种类  
Fallback Screen DPI：备用DPI，当找不到设备DPI时，使用此值  
Default Sprite DPI：默认图片DPI

计算公式：  
根据DPI算出新的Reference Pixels Per Unit （单位参考像素）  
新单位参考像素 = 单位参考像素 \* Physical Unit /Default Sprite DPI

再使用模式一：恒定像素模式的公式进行计算  
即：**UI原始尺寸 = 图片大小（像素）/(Pixels Per Unit / Reference Pixels Per Unit)**

**恒定像素模式和恒定物理模式区别：**

相同点：他们都不会进行缩放，图片多大显示多大，使用他们不会进行分辨率大小自适应  
不同点：相同尺寸不懂DPI设备像素点区别，像素点越多细节越多  
同为5像素，DPI较低的设备上看起来的尺寸可能会大于DPI较高的设备

恒定物理模式不会让UI控件进行分辨率大小自适应  
会让UI始终保持设置的尺寸大小，而且会更具设备DPI进行计算  
让在不同设备上的显示大小更加准确

一般游戏开发中极少使用这种模式

#### CanvasScaler（3D模式）

当Canvas的渲染模式设置为**世界空间3D渲染模式**时，会被强制变为**World 3D世界模式**

##### World

世界模式：简称3D模式

Dynamic Pixels Per Unit：UI中动态创建的位图（例如文本）中，单位像素数（类似密度）  
Reference Pixels Per Unit：单位参考像素，多少像素对应Unity中的一个单位（默认为100像素）

总结：

只有在3D渲染下才会启用的模式，主要用于控制该模式下的像素密度

#### Graphic Raycaster

图形射线投射器：它是用于检测UI输入事件的射线发射器

它主要负责通过射线检测玩家和UI元素的交互，判断是否点击到了UI元素

Ignore Reversed Graphics：是否忽略反转图形  
Blocking Objects：射线被哪些类型的碰撞器阻挡（在覆盖渲染模式下无效）  
Blocking Mask：射线被哪些层级的碰撞器阻挡（在覆盖渲染模式下无效）

### EventSystem

事件系统：用于管理玩家的输入事件并分发给各UI控件  
它是事件逻辑处理模块  
所有的UI事件都通过EventSystem组件中轮询检测并做相应的执行  
它类似一个中转站，和许多模块一起共同协作

如果没有它，所有点击、拖曳等等行为都不会被响应

参数：  
First Selected：首先选择的游戏对象，可以设置游戏一开始的默认选择  
Send Navigation Events：是否允许导航事件（移动/按下/取消）  
Drag Threshold：拖拽操作的阈值（移动多少像素算拖拽）

#### Standalone Input Module

独立输入模块：它主要针对处理鼠标、键盘、控制器、触屏（新版Unity）的输入  
输入的事件通过EventSystem进行分发，它依赖于EventSystem组件，他们缺一不可

参数：

Horizontal Axis：水平轴按钮对应的热键名  
Vertical Axis：垂直轴按钮对应的热键名  
Submit Button：提交（确定）按钮对应的热键名  
Cancel Button：取消按钮对应的热键名  
（以上都对应Input管理器）  
Input Actions Per Second：每秒允许键盘/控制器输入的数量  
Repeat Delay：每秒输入操作重复率生效前的延迟时间  
ForceModule Active：是否强制模块处于激活状态

### RectTransform

矩形变换：继承于Transform，是专门用于处理UI元素位置大小相关的组件

Transform组件只处理位置、角度、缩放  
RectTransform在此基础上加入了矩形相关，将UI元素当作一个矩形来处理  
加入了中心点、锚点、长宽等属性  
其目的是更加方便的控制其大小以及分辨率自适应中的位置适应

参数：  
Pivot：轴心（中心）点，取值范围0-1  
Anchors（相对父矩形锚点）：  
Min是矩形锚点范围X和Y的最小值  
Max是矩形锚点范围X和Y的最大值  
取值范围是0-1  
Pos(X,Y,Z)：轴心点（中心点）相对锚点的位置  
Width/Height：矩形的宽高  
Left/Top/Right/Bottom：矩形边缘相对于锚点的位置；当锚点分离时会出现这些内容  
Rotation：围绕轴心点旋转的角度  
{% asset_img 1.png %}：缩放大小  
{% asset_img 2.png %}：Blueprint Mode（蓝图模式），启用后，编辑旋转和缩放不会影响矩形，只会影响显示内容  
：Raw Edit Mode（原始编辑模式），启用后，改变轴心和锚点值不会改变矩形位置

{% asset_img 3.png %}：会出现锚点中心点快捷设置面板  
鼠标左键点击其中的选项，可以快捷设置锚点（九宫格布局）  
按住shift点击鼠标左键可以同时设置轴心点（相对自身矩形）  
按住Alt点击鼠标左键可以同时设置位置

## 三大基础控件

### Image图片

Image是图像组件，是UGUI中用于显示精灵图的关键组件  
除了背景图等大图，一般都使用Image来显示UI中的图片元素

参数：

{% asset_img 4.png %}

代码相关：

```cs
Image img = GetComponent<Image>();
img.sprite = Resources.Load<Sprite>("example_sprite");
(transform as RectTransform).sizeDelta = new Vector2(100, 100);
img.raycastTarget = false;
img.color = Color.red;
```

### Text文本控件

Text是文本组件，是UGUI中用于显示文本的关键组件

参数：

{% asset_img 5.png %}

富文本：

{% asset_img 6.png %}

边缘线和阴影：

边缘线组件：outline  
阴影组件：Shadow

代码控制详情到Unity官网查看或者直接查看源码。

### RawImage大图控件

RawImage是原始图像组件，是UGUI中用于显示任何纹理图片的关键组件  
它和Image的区别是，一般RawImage用于显示大图（背景图，不需要打入图集的图片等等）

参数：

{% asset_img 7.png %}

## 组合控件

### Button按钮

按钮控件，是UGUI中用于处理玩家按钮相关交互的关键组件

默认创建的Button由两个对象组成：  
父对象：Button组件的依附对象，同时挂载了一个Image组件作为背景  
子对象：按钮文本（可选）

参数：

{% asset_img 8.png %}

{% asset_img 9.png %}

监听点击事件的两种方式：  
1.拖脚本  
2.代码添加  
通过AddListener函数来调用

### Toggle单选多选框

Toggle是开关组件，是UGUI钟用于处理玩家单选框多选框相关交互的关键组件

开关组件默认是多选框  
可以通过配合ToggleGroup组件制作为单选框

默认创建的Toggle由4个对象组成  
父对象——Toggle组件依附  
子对象——背景图（必备）、选中图（必备）、说明文字（可选）

参数：

{% asset_img 10.png %}

代码较多使用的为isOn参数，来获取是否勾选了此选项

监听事件：拖脚本/代码添加

### Inputfield文本输入控件

InputField是输入字段组件，是UGUI钟用于处理玩家文本输入相关交互的关键组件

默认创建的InputField由三个对象组成  
父对象——InputField组件依附对象以及同时在其上挂载了一个Image作为背景吐  
子对象——文本显示组件（必备）、默认显示文本组件（必备）

参数：

{% asset_img 11.png %}

{% asset_img 12.png %}

{% asset_img 13.png %}

{% asset_img 14.png %}

代码控制不详细讲述。

监听事件：同上

### Slider滑动条控件

Slider是滑动条组件，是UGUI中用于处理滑动条相关交互的关键组件

默认创建的Slider由四组对象组成  
父对象——Slider组件依附的对象  
子对象——背景图、进度图、滑动块三组对象

参数：

{% asset_img 15.png %}

代码控制和监听事件不再过多描写，同上

### Scrollbar滚动条

ScrollBar是滚动条组件，是UGUI中用于处理滚动条相关交互的关键组件

默认创建的ScrollBar由两组对象组成  
父对象——ScrollBar组件依附的对象  
子对象——滚动块对象

一般情况下我们不会单独使用滚动条，都是配合ScrollView滚动视图来使用

参数：

{% asset_img 16.png %}

### ScrollView滚动视图

ScrollRect是滚动视图组件，是UGUI中用于处理滚动视图相关交互的关键组件

默认创建的ScrollRect由4组对象组成  
父对象——ScrollRect组件依附的对象，还有一个Image组件作为背景图  
子对象：  
Viewport控制滚动视图可视范围和内容显示  
Scrollbar Horizontal 水平滚动条  
Scrollbar Vertical 垂直滚动条

参数：

{% asset_img 17.png %}

### Dropdown下拉列表

DropDown是下拉列表（下拉选单）组件，是UGUI中用于处理下拉列表相关交互的关键组件

默认创建的Drop Down由四组对象组成  
父对象：Drop Down组件依附的对象，还有个Image组件作为背景图  
子对象：Label是当前选项描述  
                Arrow右侧小箭头  
                Template下拉列表选单

参数：

{% asset_img 18.png %}

## 图集制作

为什么要打图集？

UGUI和NGUI使用上最大的不同是，NGUI使用前就要打图集，UGUI可以之后再打图集

打图集的目的就是为了减少DrawCall，提高性能  
DrawCall就是CPU通知GPU进行一次渲染的命令，如果次数较多就会导致游戏卡顿  
我们可以通过打图集的方式，将小图合并成大图，将本来n次的DrawCall变为一次DrawCall

Unity中的打图集功能：

在工程设置面板中打开功能  
Edit →Project Setting → Editor  
Sprite Packer（精灵包装器，可以通过Unity自带图集工具生成图集）  
Disabled：默认设置，不会打包图集

Enabled For Builds（Legacy Sprite Packer）：Unity仅在构建时打包图集，在编辑模式下不会打包图集  
Always Enabled（Legacy Sprite Packer）：Unity在构建时打包图集，在编辑模式下运行前会打包图集

Legacy Sprite Packer传统打包模式，相对下面两种模式来说，多了一个设置图片之间的间隔距离  
Padding Power：选择打包算法在计算打包的精灵之间以及精灵与生成的图集边缘之间的间隔距离，这里的数字代表2的n次方

Enabled For Build：Unity在构建时打包图集，在编辑器模式下不会打包  
Always Enabled：Unity在构建时打包图集，在编辑模式下运行前会打包图集

## UI事件监听窗口

目前所有控件都只提供了常用的事件监听列表  
如果想做一些类似长按、双击、拖拽的功能是无法制作的  
或者想让Image和Text，RawImage三大基础控件能够响应玩家输入也是无法制作的

而事件接口就是用来处理类似问题，让所有控件都能够添加更多的事件监听来处理对应逻辑

### 常用事件接口

IPointerEnterHandler - OnPointerEnter - 当指针进入对象时调用（鼠标进入）  
IPointerExitHandler - OnPointerExit - 当指针退出对象时调用（鼠标离开）  
注：上面两种在移动设备上不存在，因为不存在进入和离开的概念

IPointerDownHandler - OnPointerUp - 在对象上按下指针时调用（按下）  
IPointerUpHandler - OnPointerClick - 在同一对象上按下再松开指针时调用（点击）

IBeginDragHandler - OnBeginDrag - 即将开始拖动时在拖动对象上调用（开始拖拽）  
IDragHandler - OnDrag - 发生拖动时在拖动对象上调用（拖拽中）  
IEndDragHandler - OnEndDrag - 拖动完成时在拖动对象上调用（结束拖拽）

### 不常用（了解）

IInitializePotentialDragHandler - OnInitializePotentialDrag - 在找到拖动目标时调用，可用于初始化  
IDropHandler - OnDrop - 在拖动目标对象上调用  
IScrollHandler - OnScroll - 当鼠标滚轮滚动时调用  
IUpdateSelectedHandler - OnUpdateSelected - 每次勾选时在选定对象上调用

ISelectHandler - OnSelect - 当对象成为选定对象时调用  
IDeselectHandler - OnDeselect - 取消选择选定对象时调用

#### 导航相关

IMoveHandler - OnMove - 发生移动事件（上下左右等）时调用  
ISubmitHandler - OnSubmit - 按下Submit按钮时调用  
ICancelHandler - OnCancel - 按下Cancel按钮时调用

### 如何使用

1.继承MonoBehaviour的脚本继承对应的事件接口，引用命名控件  
2.实现接口中的内容  
3.将该脚本挂载到想要监听自定义事件的UI控件上

### PointerEventData参数

父类：BaseEventData

PointerId：鼠标左右中键点击鼠标的ID，通过它可以判断右键点击  
position：当前指针位置（屏幕坐标系）  
pressPosition：按下时指针的位置  
delta：指针移动增量  
clickCount：连击次数  
clickTime：点击时间

pressEventCamera：最后一个OnPointerPress按下事件关联的摄像机  
enterEventCamera：最后一个OnPointerEnter进入时间关联的摄像机

**总结：**

好处：需要监听自定义事件的控件挂载继承实现了接口的脚本就可以监听到一些特殊事件  
可以通过它实现一些长按，双击拖拽的功能

坏处：不方便管理，需要自己写脚本继承接口挂载倒对应控件上，比较麻烦

## EventTrigger事件触发器

它是一个继承了上面所描述的所有事件接口的脚本，它可以让我们更方便的为控件添加事件监听

**如何使用？**

1.拖拽脚本进行关联  
2.通过代码添加

```cs
EventTrigger.Entry entry = new EventTrigger.Entry();
entry.eventID = EventTriggerType.PointerUp;
entry.callback.AddListener((data)=>{print("123123")});
EventTrigger et = new EventTrigger();
et.triggers.Add(entry);
```

### 如何使用

## 屏幕转UI相对坐标

RectTransformUtility 公共类是一个RectTransform的辅助类  
主要用于进行一些坐标的转换等等操作  
其中目前最重要的函数就是将屏幕上的点，转换为UI本地坐标下的点

方法：  
RectTransformUtility.ScreenPointToLocalPointInRectangle  
参数一：相对父对象  
参数二：屏幕点  
参数三：摄像机  
参数四：最终得到的点  
一般配合拖拽事件使用

## Mask遮罩

遮罩是在不改变图片的情况下，让图片在游戏中只显示其中的一部分

实现遮罩效果的关键组件是Mask组件  
通过在父对象上添加Mask组件即可遮罩其子对象

注意：  
1.想要被遮罩的Image需要勾选Maskable  
2.只要父对象添加了Mask组件，那么所有的UI子对象都会被遮罩  
3.遮罩父对象图片的制作，不透明的地方显示，透明的地方被遮罩

## 模型显示在UI前

### 直接使用摄像机渲染

Canvas的渲染模式只要不是渲染模式  
摄像机模式和3D模式都可以让模型显示在UI之前（Z轴在UI元素之前即可）

注意：  
1.摄像机模式时建议使用专门的摄像机渲染UI相关  
2.面板上的3D物体建议也用UI摄像机进行渲染

### 将3D物体渲染在图片上，通过图片显示

专门使用一个摄像机渲染3D模型，将其渲染内容输出到Render Texture上  
类似小地图的制作方式，再将渲染的图显示在UI上

该方式不管Canvas的渲染模式是哪种都可以使用

## 粒子显示在UI前

和3D物体相似

注意：在摄像机模式下，可以在粒子组件的Renderer相关参数中改变排序层，让粒子特效始终显示在其之前不受z轴影响

## 异形按钮

顾名思义，就是不是传统矩形的按钮

### 如何让它被准确的点击？

#### 方法一：通过添加子对象的形式

按钮之所以能够响应点击，主要是根据图片矩形范围进行判断的  
它的范围判断是自下而上的，意思是如果有子对象图片，子对象图片的范围也会算为可点击范围  
那么我们就可以用多个透明图拼凑不规则图形作为按钮子对象用于射线检测

#### 方法二：通过代码改变图片的透明度响应阈值

第一步：修改图片参数，开启Read/Write Enabled开关  
第二步：通过代码修改图片的响应阈值

图片的响应阈值参数：alphaHitTestMinimumThreshold

该参数含义，指定一个像素必须具有的最小alpha值，来认为射线是否命中图片  
即当像素点alpha值小于了那个值，就不会被检测了

## 自动布局组件

### 自动布局是什么？

虽然UGUI的RectTransform已经非常方便的帮助我们快速布局  
但是UGUI中还提供了很多可以帮助我们对UI控件进行自动布局的组件  
他们可以帮助我们自动的设置UI控件的位置和大小等

自动布局的工作方式一般是：自动布局控制组件 + 布局元素 = 自动布局

自动布局控制组件：Unity提供了很多用于自动布局的管理性质的组件用于布局  
布局元素：具备布局属性的对象们，这里主要是指具备RectTransform的UI组件

### 布局元素的布局属性

要参与自动布局的布局元素必须包含布局属性

#### 布局属性

Minimum Width：该布局元素应具有的最小宽度  
Minimum Height：该布局元素应该具有的最小高度

Preferred Width：在分配额外可用宽度之前，此布局元素应该具有的宽度  
Preferred Height：在分配额外可用高度之前，此布局元素应该具有的高度

Flexible Width：此布局元素应该相对于其同级而填充的额外可用宽度的相对量  
Flexible Height：此布局元素应该相对于其同级而填充的额外可用高度的相对量

在进行自动布局时，都会通过计算布局元素中的这六个属性得到控件的大小位置

在布局时，布局元素大小设置的基本规则是：  
1.首先分配最小大小Minimum width和Minimum Height  
2.如果父类容器中有足够的可用空间，则分配Preferred Width和Preferred Height  
3.如果上面两条分配完成后还有额外控件，则分配Flexible Width和Flexible Height

一般情况下布局元素的这些属性都是0  
但是特定的UI组件依附的对象布局属性会被改变，比如Image和Text

一般情况下我们不会去手动修改它们，但是如果有需求，可以手动添加一个**LayoutElement组件**  
来修改这些布局属性

### 水平垂直布局组件

将子对象并排或者数值的放在一起

组件名：Horizontal/Vertical Layout Group

参数相关：  
Padding：左右上下边缘偏移位置  
Spacing：子对象之间的间距

ChildAlignment：九宫格对齐方式  
Control Child Size：是否控制子对象的宽高  
Use Child Scale：在设置子对象大小和布局时，是否考虑子对象的缩放  
Child Force Expand：是否强制子对象拓展以填充额外可用控件

### 网格布局组件

将子对象当成一个个格子设置他们的大小和位置

组件名：Grid Layout Group

参数相关：  
Padding：左右上下边缘偏移位置  
Cell Size：每个格子的大小  
Spacing：格子间隔  
Start Corner：第一个元素所在的位置（四个角）  
Start Axis：沿哪个轴防止元素；Horizontal水平放置，Vertical竖直  
Child Alignment：格子对齐方式（九宫格）  
Constraint：行列约束  
        Flexible：灵活模式，根据容器大小自动适应  
        Fixed Column Count：固定列数  
        Fixed Row Count：固定行数

### 内容大小适配器

它可以自动的调整RectTransform的长宽来让组件自动设置大小  
一般在Text上使用，或者配合其他布局组件一起使用

组件名：Content Size Fitter  
参数相关  
Horizontal Fit：如何控制宽度  
Vertical Fit：如何控制高度

Unconstrained：不根据布局元素伸展  
MIn Size：根据布局元素的最小宽高度来伸展  
Preferred Size：根据布局元素的偏好宽度来伸展宽度

### 宽高比适配器

1.让布局元素按照一定比例来调整自己的大小  
2.使布局元素在父对象内部根据父对象大小进行适配

组件名：Aspect Ratio Fitter

参数相关：  
Aspect Mode：适配模式，通过调整矩形大小来实施宽高比  
        None：不让矩形适应宽高比  
        Width Controls Height：根据宽度自动调整高度  
        Height Controls Width：根据高度自动调整宽度  
        Fit In Parent：自动调整宽高、位置、锚点，使矩形适应父项的矩形，同时保持宽高比，但是会出现黑边  
        Envelope Parent：同上，但是裁剪  
Aspect Ratio：宽高比

## CanvasGroup整体控制

如果我们想要整体控制一个面板的淡入淡出，或者整体禁用，就需要添加该组件

参数相关：  
Alpha：整体透明度控制  
Interactable：整体启用禁用设置  
Blocks Raycasts：整体射线检测设置  
Ignore Parent Groups：是否忽略父级CanvasGroup的作用