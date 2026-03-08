---
title: Unity复习学习随笔（三）：GUI
date: 2025-12-05
categories: 编程笔记
tags:
  - Unity
---

## 一、工作原理和主要作用

### 1.GUI是什么

GUI：全称即时模式游戏用户交互界面（IMGUI），在Unity中一般被简称为GUI，它是一个代码驱动的UI系统

### 2.GUI的主要作用

1.作为程序员的调试工具，创建游戏内调试工具  
2.为脚本组件创建自定义检视面板  
3.创建新的编辑器窗口和工具以拓展Unity本身（一般用作内置游戏工具）

注意：不要用它为玩家制作UI功能

### 3.GUI的工作原理

在继承MonoBehaviour的脚本中的特殊函数里，调用GUI提供的方法  
类似于生命周期函数

```cs
private void OnGUI()
{
}
```

**注意**：  
1.它每帧执行，相当于是用于专门绘制GUI界面的函数  
2.一般只在其中执行GUI相关界面绘制和操作逻辑  
3.该函数在OnDisable之前，LateUpdate之后执行  
4.只要是继承Mono的脚本，都可以在OnGUI中绘制GUI

## 二、基本控件

### 1.GUI控件绘制的共同点

-   它们都是GUI公共类中提供的静态函数，直接调用即可
-   它们的参数都大同小异
-   位置参数：Rect参数   x y位置 w h尺寸
-   显示文本：string参数
-   图片信息：Texture参数
-   综合信息：GUIContent参数
-   自定义样式：GUIStyle参数
-   每一种控件都有多种重载，都是各个参数的排列组合
-   必备的参数内容是位置信息和显示信息

### 2.文本控件

**基本使用**：

```cs
GUI.Label(new Rect(0,0,200,20),"这是一个文本");
public Texture tex;
GUI.Label(new Rect(0,0,200,200),tex);
```

**综合使用**：

```cs
public GUIContent content;
public Rect rect;
GUI.Label(rect,content);
```

tooltip：可以获取当前鼠标或者键盘选中的GUI控件对应的信息

**自定义样式**：

```cs
public GUIStyle style;
GUI.Label(new Rect(0,0,200,20),"这是一个文本",style);
```

具体内容可以去Unity的官网API了解学习

### 3.按钮控件

Button：在按钮范围内，按下鼠标再抬起鼠标才算一次点击，才会返回true

RepeatButton：只要在长按按钮范围内，按下鼠标就会一直返回true

```cs
public Rect buttonRect;
public GUIContent buttonContent;
public GUIStyle buttonStyle;
if(GUI.Button(buttonRect,buttonContent,buttonStyle))
  Debug.Log("按钮被点击");
if(GUI.RepeatButton(buttonRect,buttonContent))
  Debug.Log("长按按钮被点击");
```

**练习**：

{% asset_img 1.png %}

### 4.多选框

**普通样式**：

```cs
private bool isSel;
isSel = GUI.Toggle(new Rect(0,0,100,40),isSel,"多选框");
```

**自定义样式，显示问题**：

修改固定宽高：fixedWidth和fixedHeight  
修改从GUIStyle边缘到内容起始处的空间 padding

```cs
private bool isSel;
public GUIStyle style;
isSel = GUI.Toggle(new Rect(0,0,100,40),isSel,"多选框",style);
```

### 5.单选框

单选框是基于多选框来实现的

```cs
private int nowSelIndex = 1;
if(GUI.Toggle(new Rect(0,0,100,30),nowSelIndex == 1,"选项1"))
  nowSelIndex = 1;
if(GUI.Toggle(new Rect(0,30,100,30),nowSelIndex == 2,"选项2"))
  nowSelIndex = 2;
if(GUI.Toggle(new Rect(0,60,100,30),nowSelIndex == 3,"选项3"))
  nowSelIndex = 3;
```

**练习**：

{% asset_img 2.png %}

**要完成面板之间相互控制显示，有三种方法**

第一种：都写在一个OnGUI中，通过bool标识去控制显隐  
第二种：挂载在同一个对象上，通过控制脚本的失活激活enable去控制代码是否执行，达到显隐  
第三种：挂载在不同对象上，通过控制对象的失活激活来达到面板的显隐

**如何在多个面板之间互相调用显隐**：  
通过静态变量和静态方法的形式，在Awake中初始化静态变量，如果要用这个方法，一开始对象不能失活

### 6.输入框

**普通输入**：

第二个参数为显示内容的参数string  
第三个参数为控制最大输入参数长度

```cs
private string inputStr = "请输入内容";
inputStr = GUI.TextField(new Rect(0,0,100,30),inputStr,16);
```

**密码输入**：

```cs
private string pwdStr = "请输入内容";
pwdStr = GUI.PasswordField(new Rect(0,0,100,30),pwdStr,'*');
```

### 7.拖动条

**水平拖动条**：

第三个参数是当前值，第四个参数是最小值，第五个参数是最大值

```cs
private float nowValue = 0.5f;
nowValue = GUI.HorizontalSlider(new Rect(0,0,400,50),nowValue,0f,1f);
Debug.Log(nowValue);
```

**竖直拖动条**：

```cs
nowValue = GUI.VerticalSlider(new Rect(100,100,50,100),nowValue,0f,1f);
```

**练习**：

{% asset_img 3.png %}

### 8.图片绘制

ScaleMode：  
ScaleAndCrop：也会通过宽高比来计算图片，但是会进行裁剪  
ScaleToFit：会自动根据宽高比进行计算，不会拉变形，会一直保持图片完全显示的状态  
StretchToFill：始终填充满你传入的Rect范围

alpha是用来控制图片是否开启透明通道的

imageAspect：自定义宽高比，不填默认为0，会使用原始宽高比

```cs
public Rect rect;
public Texture texture;
public ScaleMode mode = ScaleMode.StretchToFill;
public bool alpha = true;
public float wh = 0;
GUI.DrawTexture(rect,texture,mode,alpha,wh);
```

### 9.框的绘制

```cs
GUI.Box(rect,"123");
```

**练习**：

{% asset_img 4.png %}

## 三、复合控件

### 1.工具栏

```cs
private index = 1;
private string[] toolbarInfos = new string[]{"选项1","选项2","选项3"};
index = GUI.Toolbar(new Rect(0,0,100,30),index,toolbarInfos);
```

工具栏可以帮助我们根据不同的返回索引，来处理不同的逻辑

### 2.选择网格

相对Toolbar多了一个参数：xCount，代表水平方向最多显示嗯抖按钮数量

```cs
private int gridIndex = 0;
private string[] toolbarInfos = new string[]{"选项1","选项2","选项3"};
gridIndex = GUI.SelectionGrid(new Rect(0,50,200,30),gridIndex,toolbarInfos，10);
```

**练习**：

{% asset_img 5.png %}

### 3.分组

用于批量控制控件位置，可以理解为包裹者的控件加了一个父对象  
可以通过控制分组来控制包裹控件的位置

```cs
public Rect groupPos;
GUI.BeginGroup(groupPos);
GUI.Button(new Rect(0,0,100,30),"测试按钮");
GUI.EndGroup();
```

### 4.滚动列表

```cs
public Rect scPos;
public Rect showPos;
private Vector2 nowPos;
nowPos = GUI.BeginScrollView(scPos,nowPos,showPos);
```

**练习**：

{% asset_img 6.png %}

### 5.窗口

第一个参数是窗口的唯一id，不能跟其他窗口重复  
委托参数是用于绘制窗口用的函数，传入即可

id对于我们来说有一个重要作用，除了区分不同窗口，还可以在一个函数中去处理多个窗口的逻辑，通过id去区分它们

```cs
GUI.Window(1,new Rect(100,100,200,150),DrawWindow,"窗口标题");
private void DrawWindow(int id)
{
  GUI.Button(new Rect(0,0,30,20),"123123123");
}
```

### 6.模态窗口

模态窗口可以让其他控件不再有用，可以理解为在最上层，其他按钮都点不到了，只能点击该窗口上的控件

```cs
GUI.ModalWindow(3,new Rect(300,100,200,150),DrawWindow,"模态窗口");
```

### 7.拖动窗口

```cs
private Rect dragWinPos = new Rect(400,400,200,150);
dragWinPos = GUI.Window(4,dragWinPos,DrawWindow,"拖动窗口");
private void DrawWindow(int id)
{
  GUI.Button(new Rect(0,0,30,20),"123123123");
  GUI.DragWindow();
}
```

DragWindow();  
该API 写在窗口函数中调用，可以让窗口被拖动  
传入Rect参数的重载的作用，是决定窗口中哪一部分位置可以被拖动  
默认不填，就是无参重载，默认窗口所有位置都能被拖动

**练习**：

{% asset_img 7.png %}

## 四、自定义整体样式

### 1.全局颜色

```cs
GUI.color = Color.red;
GUI.contentColor = Color.yellow;
GUI.backgroundColor = Color.red;
GUI.Button(new Rect(0,0,100,30),"测试按钮");
GUI.Label(new Rect(0,50,100,300),"测试文本");
```

### 2.整体皮肤样式

```cs
public GUISkin skin;
GUI.skin = skin;
```

虽然设置了皮肤，但是绘制时如果使用GUIStyle参数，皮肤就没有用了

它可以帮助我们整套的设置，自定义样式  
相对于单个控件设置Style要方便一些

**练习**：

{% asset_img 8.png %}

### 3.GUILayout自动布局

```cs
GUI.BeginGroup(new Rect(100,100,200,300));
GUILayout.BeginHorizontal();
GUILayout.Button("123123123");
GUILayout.Button("123123123");
GUILayout.Button("123123123");
GUILayout.EndHorizontal();
GUI.EndGroup();
```

主要用于进行编辑器开发，如果用它来做游戏UI不太合适

### 4.GUILayoutOption布局选项

**控件的固定宽高**

```cs
GUILayout.Width(300);
GUILayout.Height(200);
```

**允许控件的最小宽高**

```cs
GUILayout.MinWidth(50);
GUILayout.MinHeight(50);
```

**允许控件的最大宽高**

```cs
GUILayout.MaxWidth(100);
GUILayout.MaxHeight(100);
```

**允许或禁止水平拓展**

```cs
GUILayout.ExpandWidth(true);
GUILayout.ExpandHeight(false);
GUILayout.ExpandHeight(true);
GUILayout.ExpandHeight(false);
```

**如何在编辑模式下运行代码**

需要在对象上添加特性：**_\[ExecuteAlways\]_**

## 五、九宫格概念

九宫格就是将屏幕分成9个部分：  
左、右、上、下、中、左上、右上、左下、右下  
每个部分都有一个相对UI屏幕原点的原点——是通过屏幕的宽高计算出来的

分辨率自适应就是当分辨率改变时重新计算UI控件的位置  
_**控件坐标计算公式 = 相对屏幕位置 + 中心点偏移位置 + 偏移位置**_

## 创建一个自己的GUI

 想要创建一个自己的GUI，首先需要对基础结构进行分析

每一个GUI都拥有着**位置信息，以及内容信息，自定义样式**，那么我们就可以把它们**封装成一个基类**，让各个GUI去继承它，而位置信息又涉及到九宫格位置，因此最好再创建一个位置信息类，专门用来记录位置信息：

### 位置信息类

```cs
public enum E_Alignment_Type
{
  Up,
  Down,
  Left,
  Right,
  Center,
  Left_Top,
  Right_Top,
  Left_Down,
  Right_Down
}
public class CustomGUIPos
{
  private Rect rectPos = new Rect(0, 0, 100, 100);
  public E_Alignment_Type screenAlignType;
  public E_Alignment_Type controlAlignType;
  public Vector2 offsetPos = Vector2.zero;
  public float width = 100f;
  public float height = 50f;
  private Vector2 centerPos;
  private Vector2 screenPos;
  public Rect Pos
  {
    get
    {
      rectPos.width = width;
      rectPos.height = height;
      CalcScreenPos();
      CalcControlPos();
      rectPos.x = screenPos.x + centerPos.x + offsetPos.x;
      rectPos.y = screenPos.y + centerPos.y + offsetPos.y;
      return rectPos;
    }
  }
  private void CalcControlPos()
  {
    switch (controlAlignType)
    {
      case E_Alignment_Type.Up:
        SetPos(centerPos, -width / 2, 0);
        break;
      case E_Alignment_Type.Down:
        SetPos(centerPos, -width / 2, -height);
        break;
      case E_Alignment_Type.Left:
        SetPos(centerPos, 0, -height / 2);
        break;
      case E_Alignment_Type.Right:
        SetPos(centerPos, -width, -height / 2);
        break;
      case E_Alignment_Type.Center:
        SetPos(centerPos, -width / 2, -height / 2);
        break;
      case E_Alignment_Type.Left_Top:
        SetPos(centerPos, 0, 0);
        break;
      case E_Alignment_Type.Right_Top:
        SetPos(centerPos, -width, 0);
        break;
      case E_Alignment_Type.Left_Down:
        SetPos(centerPos, 0, -height);
        break;
      case E_Alignment_Type.Right_Down:
        SetPos(centerPos, -width, -height);
        break;
    }
  }
  private void CalcScreenPos()
  {
    switch (screenAlignType)
    {
      case E_Alignment_Type.Up:
        SetPos(screenPos, Screen.width / 2, 0);
        break;
      case E_Alignment_Type.Down:
        SetPos(screenPos,Screen.width / 2, Screen.height);
        break;
      case E_Alignment_Type.Left:
        SetPos(screenPos, 0, Screen.height / 2);
        break;
      case E_Alignment_Type.Right:
        SetPos(screenPos, Screen.width, Screen.height / 2);
        break;
      case E_Alignment_Type.Center:
        SetPos(screenPos,screenPos.x / 2, screenPos.y / 2);
        break;
      case E_Alignment_Type.Left_Top:
        SetPos(screenPos, 0, 0);
        break;
      case E_Alignment_Type.Right_Top:
        SetPos(screenPos, Screen.width, 0);
        break;
      case E_Alignment_Type.Left_Down:
        SetPos(screenPos, 0, Screen.height);
        break;
      case E_Alignment_Type.Right_Down:
        SetPos(screenPos, Screen.width, Screen.height);
        break;
    }
  }
  private void SetPos(Vector2 position, float x, float y)
  {
    position.x = x;
    position.y = y;
  }
}
```

### 控件基类

```cs
using UnityEngine;
public class CustomGUIBase : MonoBehaviour
{
  public CustomGUIPos position;
  public GUIContent content;
  public GUIStyle style;
  public bool useCustomStyle = false;
  private void OnGUI()
  {
    if (useCustomStyle)
    {
      StyleDraw();
    }
    else
    {
      NotStyleDraw();
    }
  }
  protected virtual void StyleDraw()
  {
    GUI.Button(position.Pos, content, style);
  }
  protected virtual void NotStyleDraw()
  {
    GUI.Button(position.Pos, content);
  }
}
```

目前简单的基类已经写完了，但是会发现两个问题  
1.没有办法很好的去决定绘制顺序，在绘制多个GUI的时候会出现混乱的情况  
2.不能在编辑模式下看到GUI，降低了开发效率

**那么能不能通过创建一个根节点，来获取它底下的所有GUI，通过遍历绘制呢？**  
这样可以同时将两个问题全部解决

我们将 CustomGUIBase的OnGUI改名为DrawGUI，让对象自己调用进行绘制

```cs
[ExecuteAlways]
public class CustomGUIRoot : MonoBehaviour
{
  private CustomGUIBase[] guiControls;
  private void OnGUI()
  {
    guiControls = this.GetComponentsInChildren<CustomGUIBase>();
    foreach (CustomGUIBase control in guiControls)
    {
      control.DrawGUI();
    }
  }
}
```

基础功能是实现了，但是对于每次绘制都要遍历子对象，很明显是浪费性能的，那么应该怎么做去把它优化好呢？

我们可以通过判断当前是否是编辑状态，来进行优化，避免游戏运行时性能开销过大  
代码：

```cs
private void Start()
{
  guiControls = this.GetComponentsInChildren<CustomGUIBase>();
}
private void OnGUI()
{
  if(!Application.isPlaying)
  guiControls = this.GetComponentsInChildren<CustomGUIBase>();
}
```

**自定义Label**：

```cs
public class CustomGUILabel : CustomGUIBase
{
  protected override void NotStyleDraw()
  {
    GUI.Label(position.Pos, content);
  }
  protected override void StyleDraw()
  {
    GUI.Label(position.Pos, content, style);
  }
}
```

**自定义Button**：

```cs
public class CustomGUIButton : CustomGUIBase
{
  public event UnityAction clickEvent;
  protected override void NotStyleDraw()
  {
    if (GUI.Button(position.Pos, content))
    {
      clickEvent?.Invoke();
    }
  }
  protected override void StyleDraw()
  {
    if (GUI.Button(position.Pos, content, style))
    {
      clickEvent?.Invoke();
    }
  }
}
```

**自定义多选框**：

```cs
public class CustomGUIToggle : CustomGUIBase
{
  public bool isOn = false;
  public bool lastOn = false;
  public event UnityAction<bool> changeValue;
  protected override void NotStyleDraw()
  {
    isOn = GUI.Toggle(position.Pos, isOn, content);
    if(isOn != lastOn)
    {
      changeValue?.Invoke(isOn);
      lastOn = isOn;
    }
  }
  protected override void StyleDraw()
  {
    isOn = GUI.Toggle(position.Pos, isOn, content, style);
    if (isOn != lastOn)
    {
      changeValue?.Invoke(isOn);
      lastOn = isOn;
    }
  }
}
```

**自定义单选框**：

```cs
public class CustomGUIToggleGroup : MonoBehaviour
{
  public CustomGUIToggle[] toggles;
  private CustomGUIToggle frontTurTog;
  void Start()
  {
    if (toggles.Length == 0)
    return;
    for (int i = 0; i < toggles.Length; i++)
    {
      CustomGUIToggle toggle = toggles[i];
      toggle.changeValue += (value) =>
      {
        if (value)
        {
          for (int j = 0; j < toggles.Length; j++)
          {
            if (toggles[j] != toggle)
            {
              toggles[j].isOn = false;
            }
          }
          frontTurTog = toggle;
        }
        else if(frontTurTog == toggle)
        {
          toggle.isOn = true;
        }
      };
    }
  }
}
```

**自定义输入框**：

```cs
public class CustomGUIInput : CustomGUIBase
{
  protected override void NotStyleDraw()
  {
    content.text = GUI.TextField(position.Pos, content.text);
  }
  protected override void StyleDraw()
  {
    content.text = GUI.TextField(position.Pos, content.text,style);
  }
}
```

**自定义图片**：

```cs
public class CustomGUITexture : CustomGUIBase
{
  public ScaleMode scaleMode = ScaleMode.StretchToFill;
  protected override void NotStyleDraw()
  {
    GUI.DrawTexture(position.Pos, (Texture2D)content.image, scaleMode);
  }
  protected override void StyleDraw()
  {
    GUI.DrawTexture(position.Pos, (Texture2D)content.image, ScaleMode.StretchToFill);
  }
}
```