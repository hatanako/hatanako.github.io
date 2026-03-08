---
title: Unity复习学习笔记（八）：动画、模型与寻路
date: 2026-01-18
categories: 编程笔记
tags:
  - Unity
---

## 2d相关

### 一、图片导入相关设置

#### 1.Unity支持的图片格式

Unity支持的图片格式有很多

BMP：是Windows操作系统的标准图像文件格式，特点是几乎不进行压缩，占磁盘空间大  
TIF：基本不损失图片信息的图片格式，缺点是体积大

JPG：一般指JPEG格式，属于有损压缩格式，能够让图像压缩在很小的存储空间，一定程度上会损失图片数据，无透明通道  
PNG：无损压缩算法的位图格式，压缩比高，生成文件小，有透明通道

TGA：支持压缩，使用不失真的压缩算法，还支持编码压缩。体积小，效果清晰，兼备BMP的图像质量和JPG的体积优势，有透明通道  
PSD：是PS软件专用的格式，通过一些第三方工具或自制工具可以直接将PSD界面转为UI界面

其他还支持：EXR、GIF、HDR、IFF、PICT等等

其中Unity最常用的图片格式是JPG、PNG、TGA三种格式

#### 2.图片设置的六大部分

##### 1.纹理类型

纹理类型主要设置什么？

设置纹理类型主要是为了让纹理图片有不同的主要用途，指明其用于哪项工作的纹理

参数：

Default：默认纹理，大部分导入的模型贴图都是该类型

{% asset_img 1.png %}

补充：

{% asset_img 2.png %}

Normal Map：法线贴图格式

{% asset_img 3.png %}

Editor GUI and Legacy GUI：一般在编辑器中或者GUI上使用的纹理

Sprite（2D and UI）： 2D游戏或者UGUI中使用的格式

{% asset_img 4.png %}

Cursor：自定义光标

Cookie：光源剪影格式

{% asset_img 5.png %}

Lightmap：光照贴图格式

Single Channel：纹理只需要单通道的格式

{% asset_img 6.png %}

##### 2.纹理形状

纹理不仅可以用于模型贴图，还可以用于制作天空盒和反射探针  
纹理形状设置主要就是用于在两种模式之间进行切换

参数：

{% asset_img 7.png %}

##### 3.高级设置

高级设置主要是纹理的一些尺寸规则、读写规则、以及MipMap相关设置

参数：

{% asset_img 8.png %}

补充：MipMap是什么？

在三位计算机图形的贴图渲染中有一个常用的技术被称为Mipmapping。  
为了加快渲染速度和减少图像锯齿，贴图被处理成由一系列被预先计算和优化过的图片组成的文件  
这种贴图被称为mipmap  
Mipmap需要占用一定的内存空间

Mipmap中没一个层级的小图都是主图的一个特定比例的缩小细节的复制品  
虽然在某些必要的视角，主图仍然会被使用，来渲染完整的喜姐  
但是当贴图被缩小或者只需要从远距离观看时，mipmap就会转换到适当的层级

因为mipmap贴图需要被读取的像素远少于普通贴图，所以渲染的速度得到了提升。  
而且操作的时间减少了，因为mipmap的图片已经是做过抗锯齿处理的，从而减少了实时渲染的负担。  
放大和缩小也因为mipmap而变得更有效率

如果贴图的基本尺寸是256x256像素的话，它的mipmap就会有八个层级  
每个层级是上一层级的四分之一大小  
依次层级大小为：128x128，64x64，32x32，16x16，8x8....

开启mipmap功能后，Unity会帮助我们根据图片信息生成n张不同分辨率的图片，在场景中会根据我们离该模型的距离选择合适尺寸的图片用于渲染，提升渲染效率

##### 4.平铺拉伸

参数：

{% asset_img 9.png %}

##### 5.平台设置

平台设置主要设置纹理最终打包时在不同平台的尺寸、格式、压缩方式  
它非常的重要，因为它决定了打包时包的大小和读取性能方面的问题

参数：

{% asset_img 10.png %}

补充：

{% asset_img 11.png %}

##### 6.预览窗口

### 二、Sprite

#### 1.SpriteEditor

SpriteEditor就是一个精灵图片编辑器  
它主要用于编辑2D游戏开发中使用的Sprite精灵图片  
它可以用于编辑图集中提取元素，设置精灵边框，设置九宫格，设置轴心（中心）点等等功能

如果是3D工程，需要安装2D Sprite包才能使用SpriteEditor

##### **Single图片编辑功能讲解**

Single图片编辑主要讲解的就是在设置图片时  
将精灵图片模式（Sprite Mode）设置为Single的精灵图片在Sprite Editor窗口中如何编辑

1.Sprite Editor  
基础图片设置（右下角窗口）  
主要用于设置单张图片的基础属性

2.Custom Outline（决定渲染区域）  
自定义边缘线设置，可以自定义精灵网格的轮廓形状  
默认情况下不修改都是在矩形网格上渲染，边缘外部透明区域会被渲染，浪费性能  
使用自定义轮廓，可以调小透明区域，提高性能

3.Custom Physics Shape（决定碰撞判断区域）  
自定义精灵图片的物理形状，主要用于设置需要物理碰撞判断的2D图形  
它决定了之后产生碰撞检测的区域

4.Secondary Textures（为图片添加特殊效果）  
次要纹理设置，可以将其他纹理和该精灵图片关联  
着色器可以的到这些辅助纹理然后用于做一些效果处理  
让精灵应用其他效果

参数：

{% asset_img 12.png %}

##### Multiple图集元素分割

当图片资源是图集时，需要在设置时将模式设置为Multiple  
这时我们可以使用Sprite Editor自带的功能进行图集元素分割

**参数**：

{% asset_img 13.png %}

##### Polygon多边形编辑

如果我们使用的资源时多边形资源  
我们可以在设置时将模式设置为Polygon  
然后可以在Sprite Editor中进行快捷设置

但是一般这种模式在实际开发中使用较少

#### 2.Sprite Renderer

Sprite Renderer就是精灵渲染器  
所有的2D游戏中游戏资源（除UI外）都是通过Sprite Renderer让我们看到的  
它是2D游戏开发中的一个极为重要的组件

如何创建2D对象？

1.直接拖入Sprite图片  
2.右键创建  
3.空物体添加脚本

参数：

{% asset_img 14.png %}

代码设置：只需得到SpriteRenderer脚本然后设置即可。

练习：

-   写一个工具类，让我们可以更加方便的加载Multiple类型的图集资源
    

#### 3.Sprite Creator

Sprite Creator是精灵创造者，我们可以利用Sprite Editor的多边形工具创造出各种多边形  
Unity也为我们提供了线程的一些多边形

它的主要作用是2D游戏的替代资源  
在等待美术出资源时我们可以用他们作为替代品，有点类似Unity提供的自带几何体

替代资源是做demo和学习时的必备品

#### 4.Sprite Mask

Sprite Mask是精灵遮罩的意思  
它的主要作用就是对精灵图片产生遮罩  
我们可以通过它来制作一些特殊的功能，比如只显示图片的一部分让玩家看到

参数：

{% asset_img 15.png %}

#### 5.Sorting Group

Sorting Group是排序分组的意思  
它的主要作用就是对多个精灵图片进行分组排序  
Unity会将同一个排序组中的精灵图片一起排序，就好像他们是单个游戏对象一样  
主要作用是对于需要分层的2D游戏用于整体排序

注意：子排序组，先排子对象，再按父对象和别人一起排（同层和同层比）  
<u><strong>多个挂载排序分组组件的预设体</strong></u>之间通过<u><strong>修改排序索引</strong></u>来决定前后顺序

#### 6.Sprite Atlas

为什么要打图集？

打图集的目的就是减少DrawCall 提高性能，在2D游戏开发，以及UI开发中是会频繁使用的功能

如何在Unity中打开自带的打图集的功能？

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

参数：

{% asset_img 16.png %}

{% asset_img 17.png %}

{% asset_img 18.png %}

### 三、2D物理系统

#### 1.刚体

刚体是物理系统中帮助我们进行模拟物理碰撞中力的效果的

2D物理系统中的刚体何3D中的刚体基本是一样的  
最大的区别是对象只会在XY平面上移动，并且只在垂直于该平面的轴上旋转 

**参数**：

{% asset_img 19.png %}

{% asset_img 20.png %}

{% asset_img 21.png %}

如何选择不同类型的刚体：

Dynamic动态刚体：受力的作用，要动要碰撞的对象  
Kinematic运动学刚体：通过刚体API移动的对象，不受力的作用，但是想要进行碰撞检测  
Static静态刚体：不动不受力作用的静态物体，但是想要进行碰撞检测

刚体API：和3D刚体API基本一致，由Vector3变成了Vector2

#### 2.碰撞器

碰撞器是用于在物理系统中，表示物体体积的（形状或范围）  
刚体通过得到碰撞器的范围信息进行计算，判断两个物体范围是否接触  
如果接触，刚体就会模拟力的效果产生速度和旋转

2D碰撞器一共被分为了6种：

1.圆形碰撞器

{% asset_img 22.png %}

2.盒装碰撞器

{% asset_img 23.png %}

3.多边形碰撞器

{% asset_img 24.png %}

4.边界碰撞器

{% asset_img 25.png %}

5.胶囊碰撞器

{% asset_img 26.png %}

6.复合碰撞器

{% asset_img 27.png %}

练习：

{% asset_img 28.png %}

#### 3.物理材质

物理材质是用于决定在物体产生碰撞时这些物体之间的摩擦和弹性表现的  
通过物理材质我们可以做出类似斜坡不花落，小球反弹等效果

{% asset_img 29.png %}

#### 4.恒定力

恒定力是一个特殊的脚本，它可以给一个2D刚体持续添加一个力，在做一些随着时间推移而加速的对象时很适用，比如类似火箭发射等效果  
恒定力脚本会线性的为对象添加力和扭矩力，让其移动和旋转

参数：

{% asset_img 30.png %}

#### 5.效应器

2D效应器是配合2D碰撞器一起使用的，可以让游戏对象在相互接触时产生一些特殊的物理作用力  
可以通过2D效应器快捷的实现一些，传送带、互斥、吸引、漂浮、单向碰撞等等效果

不同种类的2D效应器：

1.区域效应器

{% asset_img 31.png %}

2.浮力效应器

{% asset_img 32.png %}

3.点效应器

{% asset_img 33.png %}

4.平台效应器

{% asset_img 34.png %}

5.表面效应器

{% asset_img 35.png %}

### 四、SpriteShape

SpriteShape是精灵形状的意思，它主要是方便我们以节约美术资源为前提，制作2D游戏场景地形或者背景

#### 1.Sprite Shape Profile

如何导入SpriteShape工具

> 1.在Package Manager中导入相关工具  
> 2.可以选择性导入事例和拓展资源

想要节约美术资源的创建地形或其他类似资源，首先我们要准备精灵形状概括资源，之后我们就会使用它来创建地形等资源

> 1.开放不封闭的图形  
> 2.封闭的图形

参数：

{% asset_img 36.png %}

#### 2.Sprite Shape Renderer/Controller

参数：

{% asset_img 37.png %}

{% asset_img 38.png %}

### 五、Tilemap

Tilemap一般称之为瓦片地图或者平铺地图  
是Unity2017中新增的功能  
主要用于快速编辑2D游戏中的场景，通过复用资源的形式提升地图多样性

工作原理就是用一张张小图排列组合为一张大地图

它和SpriteShape的异同：

共同点：他们都是用于制作2D游戏场景或地图的

不同点：  
1.SpriteShape可以让地形有弧度，TileMap不行  
2.TileMap可以快捷制作有伪z轴的地图，SpriteShape不行

如何创建瓦片资源：

1.Assets——> Create——>Tile  
2.在Tile Palette瓦片调色板窗口创建  
        1.首先新建一个瓦片地图编辑文件  
        2.将资源拖入窗口中选择要保存的路径

瓦片资源参数：

{% asset_img 39.png %}

#### 瓦片调色板的使用

参数：

{% asset_img 40.png %}

操作技巧

{% asset_img 41.png %}

面板相关：

{% asset_img 42.png %}

如何编辑瓦片地图：

方法一：通过瓦片调色板文件创建  
方法二：直接在场景中进行创建

矩形瓦片地图用于做横板游戏地图  
六边形瓦片地图用于做策略游戏地图  
等距瓦片地图用于做有“Z”轴的2D游戏

注意：

在编辑等距瓦片地图时  
1.需要修改工程的自定义轴排序，以Y轴决定渲染顺序（0，1，-0.26）  
2.如果地图存在前后关系需要修改TileRenderer的渲染模式  
3.可以通过z轴偏移来控制绘制单个瓦片时的高度  
4.精灵纹理的中心点会影响最终的显示效果

#### 瓦片地图关键脚本和碰撞器

参数：

{% asset_img 43.png %}

{% asset_img 44.png %}

{% asset_img 45.png %}

碰撞器：

为挂载TilemapRenderer脚本的对象添加 Tilemap Collider2D脚本  
会自动添加碰撞器  
注意：想要生成碰撞器的瓦片Collider Type类型要进行设置

#### 瓦片地图扩展包

下载地址：github.com/Unity-Technologies/2d-extras

拓展包为Tilemap添加了新的瓦片类型和笔刷类型，帮助我们更好的编辑2D地图

##### 新增瓦片类型

规则瓦片：RuleTile

{% asset_img 46.png %}

动画瓦片：AnimatedTile

{% asset_img 47.png %}

管道瓦片：PipelineTile

{% asset_img 48.png %}

随机瓦片：RandomTile

{% asset_img 49.png %}

地形瓦片：TerrainTile

{% asset_img 50.png %}

权重随机瓦片：WeightedRandomTile

（高级）规则覆盖瓦片：(Advanced)Rule Override Tile

##### 新增笔刷类型

新建自定义笔刷：

1.预设体笔刷——用于快捷刷出想要创建的精灵  
2.预设体随机笔刷——用于快捷随机刷出先要创建的精灵  
3.随机笔刷——可以指定瓦片进行关联，随机刷出对应瓦片

拓展笔刷：

1.Coordinate Brush 坐标笔刷 ——可以实时看到格子坐标  
2.GameObject Brush 游戏对象笔刷——可以再场景中选择和擦除游戏对象，仅限于选定的游戏对象的子级  
3.Group Brush 组合笔刷 —— 可以设置参数，当点击一个瓦片样式时，会自动取出一个范围内的瓦片  
4.Line Brush 线性笔刷——决定起点和终点，画一条线  
5.Random Brush 随机笔刷——和之前的自定义随机画笔类似  
6.Tint Brush 着色笔刷 ——可以给瓦片着色，瓦片的颜色锁要开启（Inspector窗口切换Debug模式，修改Flags）  
7.Tine Brush（Smooth）光滑着色笔刷——可以给瓦片进行渐变着色，需要按要求改变材质

#### 代码绘制瓦片地图

Tilemap组件：管理瓦片地图  
TileBase组件：瓦片资源对象基类  
Grid组件：用于坐标转换

```cs
public class Test : MonoBehaviour
{
  public Tilemap map;
  public Grid grid;
  public TileBase tileBase;
  private void Start()
  {
    map.ClearAllTiles();
    TileBase tmp = map.GetTile(new Vector3Int(0, 0, 0));
    map.SetTile(new Vector3Int(0, 0, 0), tileBase);
    map.SetTile(new Vector3Int(1, 0, 0),null);
    map.SwapTile(tmp, tileBase);
    grid.WorldToCell(Vector3.zero);
    Vector3 worldPos = Camera.main.ScreenToWorldPoint(Vector3.zero);
    grid.WorldToCell(worldPos);
  }
}
```

### 六、动画基础

#### Animation动画窗口

Animation窗口主要用于再Unity内部创建和修改动画  
所有在场景中的对象都可以通过Animation窗口为其制作动画

原理：  
制作动画时：记录在固定时间点对象挂载的脚本的变量变化  
播放动画时：将制作动画时记录的数据在固定时间点进行改变，产生动画效果

关键词说明

动画时间轴：  
每一个动画文件都有自己的一个生命周期，从动画开始到结束/  
我们可以在动画时间轴上编辑每一个动画生命周期中的变化

动画中的帧：  
假设某个动画的帧率为60帧每秒，意味着该动画1秒钟最多会有60次改变机会  
每一帧的间隔时间是1s/60≈16.67ms  
也就是说，我们最快可以每16.67ms改变一次对象状态

关键帧：动画在时间轴上的某一个时间节点上处于的状态

**面板**：

{% asset_img 51.png %}

创建动画后：

{% asset_img 52.png %}

代码控制：

Unity中存在两套动画系统：  
1.Mecanim动画系统——主要用Animator组件控制动画  
2.Animation动画系统——主要用Animation组件控制动画（Unity4之前的版本可能用的到）

目前大部分动画都采用的是新动画系统，只有特殊需求或者针对一些简易动画，才会使用老动画系统

老动画系统参数：

{% asset_img 53.png %}

```cs
public class Test : MonoBehaviour
{
  private Animation animation;
  private void Start()
  {
    animation = this.GetComponent<Animation>();
    animation.Play("1");
    animation.CrossFade("2");
    animation.PlayQueued("3");
    animation.CrossFadeQueued("2");
    animation.Stop();
    if (animation.IsPlaying("1"))
    {
    }
    animation.wrapMode = WrapMode.Loop;
    animation["1"].layer = 1;
    animation["1"].weight = 0.5f;
    animation["1"].blendMode = AnimationBlendMode.Additive;
  }
}
```

动画事件：

动画事件主要用于处理当动画播放到某一时刻想要触发某些逻辑  
比如进行伤害检测、发射子弹、特效播放等等

#### Animator动画状态机

**有限状态机概念：**

有限状态机（Finite - state machine，FSM）  
又称有限状态自动机，简称状态机  
是表现有限个状态以及在这些状态之间的转移和动作等行为的数学模型

有限：表示是有限度的不是无限的  
状态：指所拥有的所有状态

例：  
人会很多个动作，也就是多种状态，这些状态包括站立、走路、跑步、攻击、防守、睡觉等等  
我们每天都会在这些状态中切换，而且这些状态虽然多但是是有限的  
当达到某种条件时，我们就会在这些状态中进行切换  
而且这种切换是随时可能发生的

**有限状态机对我们的意义：**

游戏开发中有很多功能系统都是有限状态机  
最典型的状态机系统：动作系统——当满足某个条件切换一个动作，且动作是有限的  
AI系统——当满足某个条件切换一个状态，且状态是有限的

所以状态机是游戏开发中一个必不可少的概念

最简单的状态机实现：

定义一个变量，通过switch来处理不同的逻辑。

#### Animator Controller

面板：

{% asset_img 54.png %}

##### 关键组件Animator

参数：

{% asset_img 55.png %}

**相关API**

我们用代码控制状态机切换主要使用的就是Animator提供给我们的API  
一共有四种切换条件 int、float、bool、trigger  
所以对应的API也是和这四种类型有关系的

```cs
public class Test : MonoBehaviour
{
  private Animator anim;
  private void Start()
  {
    anim = GetComponent<Animator>();
    anim.SetFloat("floatPara", 1.0f);
    anim.SetInteger("intPara", 2);
    anim.SetBool("boolPara", true);
    anim.SetTrigger("triggerPara");
    anim.GetFloat("floatPara");
    anim.GetInteger("intPara");
    anim.Play("Run");
  }
}
```

#### 2D动画

##### 序列帧动画

最常见的序列帧动画就是动画片  
以固定的时间间隔按序列切换图片，就是序列帧动画的本质  
当固定时间间隔足够短时，我们肉眼就会认为图片是连续动态的，进而形成动画（会动的画面）

它的本质和游戏的帧率概念有点类似

原理就是在一个循环中按一定时间间隔不停的切换显示的图片

**如何根据原理利用代码实现序列帧动画？**

```cs
public class Test : MonoBehaviour
{
  public Sprite[] sprs;
  private SpriteRenderer sr;
  private float time = 0;
  private int nowIndex = 0;
  private void Start()
  {
    sr = GetComponent<SpriteRenderer>();
    sr.sprite = sprs[0];
  }
  private void Update()
  {
    time += Time.deltaTime;
    if (time >= 0.1f)
    {
      time = 0;
      nowIndex++;
      if (nowIndex >= sprs.Length) nowIndex = 0;
      sr.sprite = sprs[nowIndex];
    }
  }
}
```

**如何在Animation窗口制作序列帧动画**

方法一：  
1.创建一个空物体  
2.创建一个动画  
3.直接将某一个动作的序列帧拖入窗口中

方法二：  
直接将图片拖入Hierarchy层级窗口中

注意：需要修改动画帧率，来控制动画的播放速度

##### 2D骨骼动画

传统的序列帧动画，为了达成好的动画效果，理论上来说，图片越多，动作越流畅  
往往需要较多的美术资源，虽然效果好但是资源占用较多

而2D骨骼动画是利用3D骨骼动画的制作原理进行制作的  
将一张2D图片分割成n个部位，为每个部位绑上骨骼，控制骨骼旋转移动  
达到用最少的2D美术资源做出流畅的2D动画效果

**Unity如何制作2D骨骼动画？**

1.使用Unity2018新加的功能2D Animation工具制作  
2.使用跨平台骨骼动画制作工具 Spine

参数：

{% asset_img 56.png %}

{% asset_img 57.png %}

{% asset_img 58.png %}

{% asset_img 59.png %}

{% asset_img 60.png %}

图集骨骼怎么编辑：

注意：需先将Sprite设置为图集样式，并进行切片

再进入编辑双击想要编辑的图片进行创建骨骼

##### psb图集编辑

psd和psb两种格式，都是ps这款软件用于保存图像处理数据的文件格式  
psd和psb两种格式并没有太大的区别  
最大的区别是psd格式兼容除了ps以外的一些软件  
而psb只能用ps打开

在Unity中官方建议使用psb格式

参数：

{% asset_img 61.png %}

##### 反向动力学IK

在骨骼动画中，构建骨骼的方法被称为正向动力学  
它的表现形式是，子骨骼（关节）的位置根据父骨骼（关节）的旋转而改变，即父带动子

而IK全称是Inverse Kinematics，翻译过来就是反向动力学的疑似

它的表现形式是，子骨骼的末端位置会带动自己以及父骨骼旋转，即子带动父

如何引入2D IK包？

在Package Manager窗口中引入2D IK工具包  
需要在Advanced高级选项中选中Show preview packages  
这样才能看到2D IK的相关内容

如果在引入包时报错，需要在Windows防火墙中添加入站规则

参数：

{% asset_img 62.png %}

ik的意义：

1.瞄准功能；2.头部朝向功能；3.拾取物品功能  
等等又指向性的功能时，我们都可以通过IK来达到目的

最大的作用，可以方便我们进行动画制作

##### 换装

**资源在同一文件夹：**

如何在同一个psb文件中制作换装资源：

1.在ps中制作美术资源时，将一个游戏对象的所有换装资源都摆放好位置  
2.当我们导入该资源时，要注意是否导入隐藏图层

关键组件：  
SpriteLibrary，精灵资料库，确定类别分组信息  
SpriteResolver，精灵解算器，确定部位类别和使用的图片

数据文件：  
SpriteLibraryAsset，精灵资料库资源，具体记录类别分组信息的文件

**如何通过代码获取？**

1.获取各部位的SpriteResolver（需要引用命名空间）  
2.使用SpriteResolver的API进行装备切换  
GetCategory()获取当前部位默认的类别名  
SetCategoryAndLabel 设置当前部位想要切换的图片信息

**资源不在同一文件夹：**

如何在不同psb文件中制作换装资源：

1.保证各部位在ps文件中的统一  
2.基础部位可选择性隐藏

编辑换装资源的骨骼信息：

注意事项：不同文件的骨骼信息必须统一，所以我们直接使用复制的方式

手动添加关键组件和数据文件：

1.首先创建SpriteLibraryAsset数据文件  
2.为根对象添加SpriteLibrary并关联数据文件  
3.为换装部位关联SpriteResolver

如何选择同一文件和不同文件制作换装资源两种方案：

> 换装较少的游戏，比如只有面部表情更换，可以使用同一psb文件方案  
> 换装较多的游戏，比如各部位有n种装备，可以使用不同psb文件方案  
> 不同psb文件，拓展性更强

一切根据需求而定

##### Spine

Spine导出的资源有三个文件：  
.json        存储了骨骼信息  
.png        使用的图片图集  
.atlas.txt        图片在图集中的位置信息

当我们把三个资源导入到已经引入了Spine运行库的Unity工程后  
会自动为我们生成：  
\_Atlas        材质和.atlas.txt文件的引用配置文件  
\_Material        材质文件  
\_SkeletonData         json和\_Atlas资源的引用配置文件

使用Spine导出的骨骼动画：

1.直接将\_SkeletonData文件，拖入到场景中  
选择创建SkeletonAnimation对象

2.创建空对象，然后手动添加脚本进行关联

{% asset_img 63.png %}

{% asset_img 64.png %}

{% asset_img 65.png %}

{% asset_img 66.png %}

{% asset_img 67.png %}

**Spine骨骼动画代码控制相关**：

```cs
using Spine;
using Spine.Unity;
using UnityEngine;
public class Test : MonoBehaviour
{
  private SkeletonAnimation sa;
  [SpineAnimation]
  public string jumpName;
  [SpineBone]
  public string jumpBone;
  private void Start()
  {
    sa = this.GetComponent<SkeletonAnimation>();
    sa.loop = false;
    sa.AnimationName = "run";
    sa.AnimationState.SetAnimation(0, "jump", false);
    sa.AnimationState.SetAnimation(0, jumpName, false);
    sa.AnimationState.AddAnimation(0, "run", true, 0);
    sa.skeleton.ScaleX = 1;
    sa.AnimationState.Start += (t)=>
    {
      print( sa.AnimationName + " 动画开始播放");
    };
    sa.AnimationState.End += (t) =>
    {
      print(sa.AnimationName + " 动画被中断或者清除");
    };
    sa.AnimationState.Complete += (t) =>
    {
      print(sa.AnimationName + " 动画播放完成");
    };
    sa.AnimationState.Event += (t, e) =>
    {
      print(sa.AnimationName + " 自定义事件：" + e.Data.Name);
    };
    Bone bone = sa.skeleton.FindBone(jumpBone);
    sa.skeleton.SetAttachment(slotName: "weapon", attachmentName: "gun");
  }
}
```

## 模型导入相关

Unity中使用的模型：

Unity支持很多模型格式，比如：.fbx，.dae  .3ds  .dxf  .obj等等  
99%的模型都不是在Unity中制作的，都是美术人员在建模软件中制作，如3DMax，Maya等等  
当他们制作完模型后，虽然Unity支持很多模型格式，但是官方建议是将模型在建模软件中导出为**FBX格式**后再使用

**使用FBX模型格式的优势**：

> 1.减少不必要数据，提升导入效率  
> 2.不需要在每台计算机上安装建模软件的授权副本  
> 3.对Unity版本无要求，使用原始3D模型格式可能会因为版本不同导致错误或意外

**导入模型的基本流程**：

> 1.将模型从建模软件中以FBX文件格式导出  
> 2.将模型导入到Unity的资源文件夹中  
> 3.在Unity内部进行基础设置——模型、骨骼、动作、材质

**如何在Unity中设置模型的内容**：

在Project窗口选中导入的模型  
在Inspector窗口进行相关设置，四个页签分别是：  
1.Model 模型页签  
2.Rig 操纵（骨骼）页签  
3.Animation 动画页签  
4.Materials 材质纹理页签  
通过这四个页签对模型动作相关信息设置完成后，之后我们才能在场景中更好的使用这些模型资源

### 1.model 页签

该页签主要是用于设置诸如：模型比例、是否导入模型中摄像机和光源、网格压缩方式等等相关信息  
修改模型中存储的各个元素的属性，最终会影响在Unity中使用模型时的一些表现

{% asset_img 68.png %}

{% asset_img 69.png %}

{% asset_img 70.png %}

{% asset_img 71.png %}

### 2.Rig 页签

该页签主要是用于设置如何将骨骼映射到导入模型中的网格，以便能够将其动画化  
对于人形角色模型，需要分配或创建Avatar（替身信息）  
对于非人形角色模型，需要在骨骼中确定根骨骼

简单来说Rig页签主要是设置骨骼和替身系统相关信息的  
设置了他们，动画才能正常播放

Rig面板的基础信息：

{% asset_img 72.png %}

Avatar面板参数：

{% asset_img 73.png %}

### 3.Animation 页签

当我们选中包括动画剪辑的模型时，该页签将显示动画设置相关的内容

动画剪辑时Unity动画的最小构成元素  
代表一个单独的动作

当美术做好动画导出时建议将模型和动画文件分别导出  
1.导出包含网格信息不包含动作信息模型  
2.导出不包含网格信息包含动作信息的动作（模型）文件  
具体的导出规则可以参考Unity官方文档

Animation动画页签的四大部分：

#### 1.基础信息设置

{% asset_img 74.png %}

#### 2.动画剪辑属性基本设置

{% asset_img 75.png %}

{% asset_img 76.png %}

#### 3.动画剪辑属性其他设置

{% asset_img 77.png %}

{% asset_img 78.png %}

{% asset_img 79.png %}

{% asset_img 80.png %}

#### 4.预览窗口 

{% asset_img 81.png %}

### 4.Materials 页签

{% asset_img 82.png %}

## 3d动画相关

### 如何使用

1.将模型拖入场景中  
2.为模型对象添加Animator脚本  
3.为其创建Animator Controller动画控制器（状态机）  
4.将想要使用的相关动作，拖入Animator Controller动画控制器（状态机）窗口  
5.在Animator Controller动画控制器（状态机）窗口编辑动画关系  
6.代码控制状态切换

#### 状态设置相关参数

{% asset_img 83.png %}

#### 连线设置相关参数

{% asset_img 84.png %}

{% asset_img 85.png %}

#### 动画分层和遮罩

##### 动画分层的目的

游戏中有时会有这样的需求：

人物健康状态时播放正常动画，人物非健康状态时播放特殊动画  
比如血量低于一定界限，人物的大部分动作将表现为虚弱状态  
我们可以利用动画分层来快速实现这样的功能

动画分层和动画遮罩结合使用  
3D游戏中我们常常会面对这样的需求：

人物站立时会有开枪动作；人物跑动时会有开枪动作；人物蹲下时会有开枪动作；  
从表现上来看光是开枪动作可能就有三种，如果让美术做三种动作就费时又费资源

我们是否可以这样：  
开枪动画只影响上半身  
下半身根据实际情况播放站立、跑动、蹲下动作  
通过上下半身播放不同的动画就可以达到动画的组合播放

动画分层主要就是达到两个目的：

1.两套不同层动作的切换  
2.结合动画遮罩让两个动画叠加在一起播放，提升动画多样性，节约资源

##### 如何使用分层 

1.新建一个动画层  
2.设置动画层参数  
3.在该层中设置状态机（注意：结合遮罩使用时默认状态一般为Null状态）  
4.根据需求创建动画遮罩

**分层参数**

{% asset_img 86.png %}

### 1D混合

游戏动画中常见的功能就是在两个或者多个相似运动之间进行混合  
比如  
1.根据角色的速度来混合行走和奔跑动画  
2.根据角色的转向来混合向左或向右倾斜的动作

可以理解为高级版的动画过渡

上面的动画过渡都是处理两个不同类型动作之间切换的过渡效果  
而动画混合是允许合并多个动画来使动画平滑混合

**如何在状态机窗口创建动画混合状态**

在Animator Controller窗口 右键->Create State -> From New Blend Tree

参数

{% asset_img 87.png %}

### 2D混合

**1D混合**是用一个参数控制动画的混合，之所以叫1D是因为一个参数可以看作是**一维线性**的  
**2D混合**你可以简单理解是用两个参数控制动画的混合，之所以叫2D是因为两个参数可以看作是**二位平面xy轴**的感觉

**2D混合的种类**：

> 1.2D Simple Directional        2D简单定向模式，运动表示不同方向时使用，比如向前后左右走  
> 2.2D Freeform Directional    2D自由形式定向模式，同上，运动表示不同方向时使用，但是可以在不同方向上有多个运动  
> 3.2D Freeform Cartesian        2D自由形式笛卡尔坐标模式，运动不表示不同方向时使用，比如向前走不拐弯，向前跑不拐弯，向前走右转等等  
> 4.Direct                                 直接模式，自由控制每个节点权重，一般做表情动作等

前三种方式只是针对动作的不同采用不同的算法来进行混合的  
第四种可以用多个参数进行融合

混合树种还可以再嵌入混合树，使用上是一致的，根据实际情况选择性使用

### 动画子状态机

子状态机顾名思义就是在状态机里还有一个状态机  
它的主要作用就是某一个状态是由多个动作状态组合而成的复杂状态  
比如某一个技能它是由三段动作组合而成的，跳起，攻击，落下  
当我们释放这个技能时会连续播放这三个动作  
那么我们完全可以把他们放到一个子状态机中

如何创建子状态机：

同混合树一样，只是选择了Sub-State Machine

### 动画IK控制

在上面2D动画已经讲解了什么是IK，此处不做记录。

**如何进行IK控制？**

1.在状态机的层级设置中，开启IK通道  
2.继承MonoBehaviour 的类中，Unity定义了一个IK回调函数：OnAnimatorIK  
        我们可以在该函数中调用Unity提供的IK相关API来控制IK  
3.Animator中的IK相关API  
SetLookAtWeight                设置头部IK权重  
SetLookAtPosition              设置头部IK看向位置  
SetIKPositionWeight           设置IK位置权重  
SetIKRotationWeight           设置IK旋转权重  
SetIKPosition                        设置IK对应的位置  
SetIKRotation                        设置IK对应的角度  
AvatarIKGoal枚举                四肢末端IK枚举

IK在游戏开发的应用：

1.拾取某一件物品  
2.持枪或持弓瞄准某一个对象  
3.附近有物品时角色看向物品  
等等

**关于OnAnimatorIK和OnAnimatorMove两个函数的理解**

我们可以简单理解这两个函数是两个和动画相关的特殊生命周期函数  
他们会在每帧的状态机和动画处理完后调用  
OnAnimatorIK在OnAnimatorMove之前调用  
OnAnimatorIK主要处理IK运动相关逻辑  
OnAnimatorMove主要处理动画移动以修改根运动的回调逻辑  
他们存在的目的只是多了一个调用时机，当每帧的动画和状态机逻辑处理完后再调用

### 动画目标匹配

动画目标匹配主要指的是当游戏中角色要以某种动作移动，该动作播放完毕后，人物的手或者脚必须落在某一个地方  
比如：角色需要跳过垫脚石或者跳跃并抓住放量，那么我们就需要动作目标匹配来达到想要的效果

**如何实现？**

Unity中的Animator提供了对应的函数来完成该功能  
使用步骤是：  
1.找到动作关键点位置信息（比如起跳点，落地点，简单理解就是真正可能产生位移的动画表现部分  
2.将关键信息传入MatchTargetAPI中

参数一：目标位置  
参数二：目标角度  
参数三：匹配的骨骼位置  
参数四：位置角度参数  
参数五：开始位移动作的百分比  
参数六：结束位移动作的百分比

注意：  
调用匹配动画的时机有一些限制  
1.必须保证动画已经切换到了目标动画上  
2.必须保证调用时动画并不是处于过渡截断而真正在播放目标动画  
如果发型匹配不正确，往往都是这两个原因造成的  
3.需要开启Apply Root Motion

### 状态机行为脚本（State Machine Behaviour）

状态机脚本是一类特殊的脚本，继承指定的基类  
它主要用于关联到状态机中的状态矩形上  
我们可以按照一定规则编写脚本  
当进入、退出、保持在某一个特定状态时我们可以进行一些逻辑处理  
简单解释就是为Animator Controller状态机窗口中的某一个状态添加一个脚本  
利用这个脚本我们可以做一些特殊功能

比如：  
1.进入或退出某一状态时播放声音  
2.仅在某些状态下检测一些逻辑，比如是否接触地面等等  
3.激活和控制某些状态相关的特效

**如何使用状态机脚本：**

1.新建一个脚本继承StateMachineBehaviour基类  
2.实现其中的特定方法进行状态行为监听

> OnStateEnter        进入状态时，第一个Update中调用  
> OnStateExit        退出状态时，最后一个Update中调用  
> OnStateIK        OnAnimatorIK后调用  
> OnStateMove        OnAnimatorMove后调用  
> OnStateUpdate        除第一帧和最后一帧，每个Update上调用  
> OnStateMachineEnter        子状态机进入时调用，第一个Update中调用  
> OnStateMachineExit        子状态机退出时调用，最后一个Update中调用

3.处理对应逻辑

**状态机行为脚本**相对**动画事件**来说更准确  
但是使用起来稍微麻烦一些，需要根据实际需求选择使用

### 状态机复用

游戏开发时经常遇到这样的情况：  
有N个玩家和N个怪物，他们的动画状态机行为都是一致的，只是对应的动作不同而已  
这时如果我们为他们每一个对象都创建一个状态机进行状态设置和过渡设置无疑是浪费时间的  
所以状态机复用就是解决这一问题的方案  
主要用于为不同对象使用共同的状态机行为  
减少工作量，提升开发效率

**如何复用状态机？**

1.在Project窗口右键Create→Animator Override Controller  
2.为此文件在Inspector窗口关联基础的Animator Controller文件  
3.关联需要的动画

## 角色控制器

角色控制器是让角色可以受制于碰撞，但是不会被刚体所牵制  
如果我们对角色使用刚体判断碰撞，可能会出现一些奇怪的表现  
比如：  
1.在斜坡上往下滑动  
2.不加约束的情况下碰撞可能让自己被撞飞等等  
而角色控制器会让角色的表现更加稳定  
Unity提供了角色控制器脚本专门用于控制角色

注意：  
添加角色控制器后，不用再添加刚体  
能检测碰撞函数、能检测触发器函数、能被射线检测

参数：

{% asset_img 88.png %}

代码相关：

```cs
using UnityEngine;
public class Test : MonoBehaviour
{
  public CharacterController cc;
  public Animator anim;
  private void Start()
  {
    cc = GetComponent<CharacterController>();
    anim = GetComponent<Animator>();
    if (cc.isGrounded)
    {
      print("接触地面了");
    }
  }
  private void Update()
  {
    anim.SetInteger("Speed", (int)Input.GetAxisRaw("Vertical"));
    cc.SimpleMove(this.transform.forward * 10 * Input.GetAxisRaw("Vertical") * Time.deltaTime);
  }
  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    print(hit.gameObject.name);
  }
  private void OnTriggerEnter(Collider other)
  {
    print("触发器触发" + other.gameObject.name);
  }
}
```

## 导航寻路系统

概述：

Unity中的导航寻路系统是能够让我们在游戏世界中  
让角色能够从一个起点准确的到达另一个终点  
并且能够自动避开两个点之间的障碍物选择最近最合理的路径进行前往

Unity中的导航寻路系统的本质  
就是在A\*算法的基础上进行了拓展和优化

寻路算法补充：[知乎文章](https://zhuanlan.zhihu.com/p/620172389 "知乎文章")

### 1.导航网格（NavMesh）

如何打开导航网格窗口：

Window→AI→Navigation        打开Unity内置的导航网格生成窗口

参数相关：

1.Object页签——设置参与寻路烘焙的对象

{% asset_img 89.png %}

2.Bake页签——导航数据烘焙页签，设置寻路网格的具体信息

{% asset_img 90.png %}

3.Areas页签——导航地区页签，设置对象的寻路消耗

{% asset_img 91.png %}

4.Agents页签——代理页签，设置寻路代理信息

{% asset_img 92.png %}

### 2.导航网格寻路组件（NavMesh Agent）

寻路组件的作用就是帮助我们让角色可以在地形上准确的移动起来

本质就是根据烘焙出的寻路网格信息，通过基于A\*寻路的算法计算出行进路径让我们在该路径上移动

参数相关：

{% asset_img 93.png %}

代码相关：

一个鼠标点击后自动寻路前往的例子：

```cs
using UnityEngine;
using UnityEngine.AI;
public class Test : MonoBehaviour
{
  public NavMeshAgent agent;
  private void Start()
  {
  }
  private void Update()
  {
    if (Input.GetMouseButtonDown(0))
    {
      RaycastHit hit;
      if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit))
      {
        agent.SetDestination(hit.point);
      }
    }
    if (Input.GetKeyDown(KeyCode.Space))
    {
      agent.isStopped = !agent.isStopped;
    }
  }
}
```

**需要注意**：如果两边平台中有跳跃连接点，但是无法前往，需要检查两边是否都勾选了**生成网格连接点**。

不常用内容：

```cs
print(agent.speed);
print(agent.angularSpeed);
print(agent.acceleration);
print(agent.hasPath);
print(agent.destination);
print(agent.isStopped);
print(agent.path);
print(agent.pathPending);
print(agent.remainingDistance <= agent.stoppingDistance);
print(agent.pathStatus);
print(agent.updatePosition);
print(agent.updateRotation);
print(agent.velocity);
NavMeshPath nav = new NavMeshPath();
if (agent.CalculatePath(new Vector3(10,0,10),nav))
{
  print("Path calculated successfully");
}
else
{
  print("Path calculation failed");
}
agent.SetPath(nav);
agent.ResetPath();
agent.Warp(new Vector3(5,0,5));
```

### 3.导航网格链接组件（Off-Mesh Link）

我们在烘焙地形数据的时候，可以生成网格外链接  
但是它是满足条件的都会生成，而且是要在编辑模式下生成  
如果我们只希望两个未连接的平面之间只有有限条连接路径可以跳跃过去  
并且运行时可以动态添加，就可以使用**网格外连接组件**  
达到“指哪打哪”的效果

网格外链接组件的使用：

1.使用两个对象作为两个平面之间的连接点（起点和重点）  
2.添加Off Mesh Link脚本进行关联  
3.设置参数

参数：

{% asset_img 94.png %}

### 4.导航网格动态障碍物组件（NavMesh Obstacle）

在游戏中常常会有这样的功能：  
场景中有一道门，如果这道门没有被破坏是不能自动导航到门后场景的  
只有当这道门被破坏了，才可以通过此处前往下一个场景  
而类似这样的物体本身是不需要进行寻路的，所以没有必要为它添加NavMeshAgent脚本  
这时就会使用动态障碍组件实现该功能

如何使用：

1.为需要进行动态阻挡得对象添加NavMeshObstacle组件  
2.设置相关参数  
3.代码逻辑控制它的移动或者显隐

参数：

{% asset_img 95.png %}