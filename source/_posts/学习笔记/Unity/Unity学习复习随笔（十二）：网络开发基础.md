---
title: Unity学习复习随笔（十二）：网络开发基础
date: 2026-02-20
categories: 编程笔记
tags:
  - Unity
---

## 网络开发理论

### 网络基本概念

#### 1.网络的作用

在没有网络之前，每个人的设备（电脑等）都是彼此孤立的  
网络的出现让设备之间可以相互通信

**网络是由若干设备和链接这些设备的链路构成，各种设备间接或者直接通过介质相连**  
设备之间想要进行 信息 传递时，将想要传递的数据编码为二进制数值便可以被有效的传输  
这些数据是以电脉冲的形式进行传输的  
线缆中的电压是在高低状态之间进行变化的，因而：  
**二进制中1是通过产生一个正电压来传输的  
二进制中0是通过产生一个负电压来传输的**

#### 2.局域网

**局域网（Local Area Network，简称LAN）**是按照范围划分而来的名称  
是指**某一个小区域内由多台设备互联成的计算机组**  
可以是家里的两台设备组成，也可以是学校、公司里的上千台设备组成  
特点是分布地区范围有限，覆盖范围一般是房源几千米内

#### 3.以太网

**以太网是一种计算机局域网技术，**是目前应用最普遍的局域网技术  
IEEE组织（电气与电子工程师协会）的IEEE 802.3标准制定了以太网的技术标准  
它规定了包括物理层的连线、电子信号和介质访问层协议的内容

**简单理解：以太网就是网络连接的一种规则（协议）**

##### 以太网——网络拓扑结构

概念：用传输媒体把计算机等各种设备互相连接起来的物理布局  
是指设备互连过程中构成的几何形状

#### 4.城域网

**城域网（Metropolitan Area Network，简称MAN）**是在一个城市范围内所建立的网络  
通常覆盖一个城市，从几十公里到一百公里不等，可能会有多种介质  
用户的数量也比局域网更多

#### 5.广域网

**广域网（Wide Area Network，简称WAN），又称外网、公网**  
是连接不同地区局域网或城域网设备通信的远程网，通常跨接很大的物理范围  
所覆盖的范围从几十公里到几千公里  
它能链接多个地区、城市和国家，形成国际性的远程网络

**注意：广域网并不等同于互联网**

#### 6.互联网（因特网）

互联网，如果作为名词理解的话，就是互相联网，让各种设备处于同一网络环境下  
只要设备互相连接网络了，那么设备之间就可以进行通信  
它一般泛指彼此能够通信的设备组成的网络，但是目前往往我们提到的互联网，大部分是指代的因特网

可以把互联网作为名词理解，也可以将互联网理解为因特网的代称

> **互联网（internet，音译为因特网）**，又称国际网络，指的是网络与网络之间所串联成的庞大网络，这些网络以一组通用的协议（规则）相连，形成逻辑上的单一巨大国际网络。  
> 互联网目前已经把200多个国家和地区的大部分设备连接了起来，形成了一个遍布全世界的网络。  
> **所以在一定程度上可以说，互联网等同于广域网，广域网包含了互联网。  
> 互联网使用的技术，在广域网上一定有，但是某些广域网的技术，互联网上不一定有。**比如军用的广域网，普通人是不会使用的。

互联网（因特网）的本质就是人为定义的一系列协议（规则），**总称为“互联网协议”。  
主要功能：定义计算机如何接入互联网，以及接入互联网的计算机的通信标准；也就是为我们的设备定义连入标准，并且为传输的二进制数据定义一些传输规则**  
只要遵守这些规则来进行网络连接和数据传输，我们的各种设备就可以通过网络进行通讯，进行信息的交换

简单理解因特网，它是国际上最大的互联网，所以当我们提到互联网时一般都代指因特网  
**它是指当前全球最大的、开放的、有众多网络互相连接而成的特定的计算机网络，它采用TCP/IP协议簇作为通信的规则，提供了包括万维网（WWW）、文件传输（FTP）、电子邮件（E-mail）、远程登录（Telnet）等等服务**

只要我们的设备和应用程序遵守这套因特网的互联网规则，那么我们就可以在这个庞大的网络体系当中畅游

#### 7.万维网

**万维网（World Wide Web，简称WWW，也称Web、3W等）**  
它是存储在因特网的计算机当中，数量巨大的文档（页面）的集合  
它是无数个网络站点和网页的集合，是构成因特网的主要部分  
我们平时用浏览器看到的内容就属于万维网，他们本质上就是一个个的文档（页面）

如果把因特网看作是网络的基础，那么万维网就可以被看作是对因特网的应用  
是利用因特网规则的一种信息传递和呈现的手段，可以认为万维网就是网站和页面的统称

##### 总结

> 1.  网络：由若干设备和连接这些设备的链路构成，设备间可以相互通信
> 2.  局域网：指某一个小区域内由堕胎设备互联成的计算机组
> 3.  以太网：网络连接的一种规则，定义了连接传输规范
> 4.  城域网：是在一个城市范围内所建立的网络，几十到一百公里
> 5.  广域网：是连接不同地区、城市、国家的远程网络，几十到几千公里
> 6.  互联网（因特网）：是目前国际上最大的广域网，定义了通信规则等
> 7.  万维网：是基于因特网的网站和网页的统称

练习：

1. 常见的网络拓扑结构有哪些？（请至少写出5种拓扑结构）

答：星型、总线型、环型、网状、树状

2\. 网络是如何让设备之间可以通信的？

答：

物理连接：设备可以通过物理连接（如网线）或无线连接（如Wi-Fi或蓝牙）进行通信。

协议：设备之间需要遵循共同的通信规则，这些规则称为协议。协议定义了设备如何处理信息。

网络设备：网络中的设备（如路由器、交换机）负责将数据包从一个设备传输到另一个设备，确保信息能够正确到达。

地址系统：每个设备在网络中都有唯一的地址（如IP地址和MAC地址），用于识别和定位设备。

### IP、端口、Mac地址

#### 1.IP地址

##### 基本概念

**IP地址（Internet Protocol Address）**是指互联网协议地址，又译为网际协议地址  
IP地址是IP协议提供的一种统一的地址格式，IP地址是设备在网络中的具体地址

IP地址就像是设备的家庭住址一样，被用来给互联网上的电脑一个编号，用于定位  
我们可以用电话来举例子，IP地址就像是一个电话号码，我们需要对方的电话，才能够联系到对方

##### IP地址分 类

**按协议分类：**

1.  **IPv4：**互联网协议第四版，由四个数组成，每个数的取值范围是0~255  
    每个数用.来分割，它的数量是有限的 0.0.0.0~255.255.255.255  
    相当于是由4个字节表示，一个字节八位，255的二进制数则是 1111 1111  
    A类：0.x.x.x~127.x.x.x(32位二进制最高位为0，适用于网内主机数达1600w台的大型网络)  
    B类：128.x.x.x~191.x.x.x(32位二进制最高2位为10，适用于中等规模，6w台设备)  
    C类：192.x.x.x~223.x.x.x(32位二进制最高3位为110，适用于小规模，254台设备)  
    D类：224.x.x.x~239.x.x.x(32位二进制最高4位为1110，属于特殊IP，一般为广播地址)  
    E类：240.x.x.x~255.x.x.x(32位二进制最高5位为11110，作为特殊使用)
2.  **IPv6：**互联网协议第六版，由八个数组成，每个数的取值范围是0~65535  
    每个数用：来分割，它是萎了解决IPv4的有限性而设计的（几乎无限）  
    0:0:0:0:0:0:0:0~65535:65535:65535:65535:65535:65535:65535:65535

**按使用范围分类：**

1.  **公网IP：**用于连接外网，想要和远程设备进行通信时使用的IP地址  
    查看方式：网页搜索IP地址查询，即可查找到公网IP
2.  **私网IP：**也称局域网IP，私网IP不能上网，只能用于局域网内通信  
    查看方式：  
    1.在windows操作系统冲打开命令提示符窗口，然后输入指令ipconfig查看本机的IP地址信息  
    快捷方式：win+R → cmd → ipconfig  
    2.在mac操作系统中打开终端窗口，然后输入指令ifconfig查看本机的IP地址信息  
    也可以在网络设置窗口上直接查看IP地址

#### 2.端口号

通过IP地址我们可以在网络上找到一台设备，但是我们想要和设备通信，本质上是和运行在设备上的某一个应用程序进行通信  
一台设备上可能允许n个应用程序，而端口号就是用来区分这些应用程序的，让我们可以明确到底是和哪一个应用程序进行通信

**基本概念：**IP地址决定了设备在网络中的具体地址，而端口是不同应用程序在该设备上的门牌号码，一台设备上不同的应用程序想要进行通信就必须对应一个唯一的端口号

**使用规则：**端口号的取值范围是**0~65535**  
**我们在进行网络程序开发时，需要自己为应用程序设置端口号，端口号不能和其他应用程序相同，避免产生冲突**  
一般选择1024以上的端口进行 使用 ，1024以下的一般由IANA互联网数字分配机构管理

#### 3.mac地址

**基本概念：Mac地址（Media Access Control Address）直译为媒体存取控制地址，也称局域网地址、Mac地址、以太网地址、物理地址**

它是用来确认网络设备的地址，在OSI 模型 中，第三层网络层负责IP地址，第二层数据链路层则负责Mac地址，**Mac地址是用于在网络中唯一标识一个网卡的，一台设备可以有多个网卡，每个网卡都会有一个唯一的Mac地址**

在早期的网络中，只用Mac地址便可以实现两台设备间的通信，但随着设备的增多，Mac地址虽然具备唯一性但是并不携带位置信息，如果通过广播方式查找设备，会给网络造成巨大负担。所以才有了IP地址来定位网络中的设备

**基本构成：**Mac地址的长度为48位（6个字节），通常表示位12个16进制数  
如：_00-1A-2B-3C-4D-5E_就是一个Mac地址  
前三个字节，16进制数00-1A-2B代表网络硬件制造商的编号，它由IEEE（电气与电子工程师协会）分配  
后三个字节，16进制数3C-4D-5E代表该制造商所制造的某个网络产品（如网卡）的系列号

查看方式和查看IP地址相同

Mac地址就像是身份证号，IP地址就好像是住址。  
Mac地址是物理层面上通信的基础，IP地址是逻辑层面上通信的基础

##### 总结

在互联网中寻找一台指定设备就好像在现实世界中找朋友串门  
必须要知道朋友的地址：  
IP地址 = 朋友的住址  
端口号 = 门牌号

在网络通信中，我们通过IP地址以及端口号定位想要通信的远端计算机中的某一个应用程序  
IP地址 = 设备在外网的位置  
端口 = 运行在该设备上的应用程序位置  
Mac地址 = 设备进行网络通信的唯一标识，设备真正进行物理信息传输用来定位的标识

练习：

1\. IP地址和端口的作用？

答：在网络通信中，我们通过IP地址以及端口号定位想要通信的远端计算机中的某一个应用程序

### 客户端和服务端

#### 1.客户端

**名词角度解释：**

客户端：用户使用的设备（计算机、手机、平板等等）

客户端应用程序：用户使用的设备上安装的应用程序，用户会直接使用操作的内容  
比如各种游戏、软件等

**基本概念：**

**客户端（Client，或称为用户端、前端）**，**是指与服务端相对应，为客户提供本地服务的应用程序**  
我们在设备上（计算机、手机等）使用的所有软件和应用极狐都是客户端应用程序  
比如：各种浏览器，游戏，软件等等

用户在设备上（计算机、手机、平板等）运行使用的应用程序就是客户端应用程序（简称客户端）

#### 2.服务端

**名词角度解释：**

**服务端：**为客户端提供服务的设备，一般是一台性能较好的计算机

**服务端应用程序：**为客户端提供的应用程序，该应用程序是运行在服务端设备上的

往往在软件开发中提到的服务端或服务器都是泛指服务端应用程序

**基本概念：**

**服务端（Server，或称为服务器、后端）**，是为客户端服务的，服务的内容诸如向客户端提供资源，保存客户端数据等等  
它是一种有针对性的服务程序，往往一个服务端都是针对性的为某类客户端提供服务  
它往往是一台运行在远端的计算机，客户端和服务端通过网络进行通信

比如：  
某游戏的服务端只为该游戏的客户端提供服务（消息转发、信息保存、逻辑处理等等）  
某软件的服务端只为该软件的客户端提供服务（消息转发、信息保存、逻辑处理等等）

服务端应用程序运行在远端的一台计算机上，客户端通过网络和服务端进行通讯，服务端为客户端提供各种服务。

#### 3.网络游戏开发中的客户端和服务端

**单机游戏：**只有客户端，没有服务端，不存在玩家之间的交互，数据存储在本地

**网络游戏：**有客户端和服务端，玩家之间可以进行交互（信息同步，信息交换）  
静态（不变的）数据存储在客户端，动态（要变的）数据存储在服务端

**网络游戏开发中的客户端：**

Unity、UE、Cocos、Egret、Laya、Flash等等游戏引擎开发的游戏都属于客户端应用程序  
他们都是被用户直接操作的，主要功能就是游戏玩法、UI交互、美术表现、本地数据保存等

**网络游戏开发中的服务端：**

C++、Java、C#、Go等等语言开发的运行在远端计算机上为游戏客户端提供服务的软件，  
都属于服务端应用程序，它的主要功能就是消息转发、数据保存、逻辑处理等等

##### 总结

**1.客户端：**用户在设备上运行使用的应用程序就是客户端应用程序（简称客户端）  
**2.服务端：**服务端应用程序运行在远端的一台计算机上，客户端通过网络和服务端进行通讯，服务端为客户端提供各种服务  
**3.网络游戏开发中的客户端和服务端**  
我们用Unity开发的应用程序就是游戏客户端应用程序  
后端程序可以使用C++、C#、Java、Go等语言进行服务端程序开发，为游戏客户端提供服务  
客户端和服务端之间通过互联网进行信息交换

练习：

1\. 什么是服务端（服务器）？游戏服务器一般处理什么？

答：服务端应用程序运行在远端的一台计算机上，客户端通过网络和服务端进行通讯，服务端为客户端提供各种服务  
主要功能就是消息转发、数据保存、逻辑处理等等

### 数据通信模型

#### 1.数据通信模型

在早期的计算机网络中，为了有效的利用计算机，一般将数据通信模型分为：  
**分散式（Decentralized）、集中式（Centralized）、分布式（Distributed）**  
这三种方式决定了数据在网络环境中的管理方式

##### 分散式

**在分散式系统中，用户只负责管理自己的计算机系统，各自独立的系统之间没有资源或信息的交换和共享。就类似一台台没有联网的设备。**

这种模式由于存在大量共享数据的重复存储，除了引起数据冗余之外，也容易导致一个组织内各部门数据的不一致性。同时还会造成硬件、支持和运营维护等成本的大量增加，因此早被淘汰

##### 集中式

在集中式环境中，用一台主计算机保存一个组织的全部数据，而用户则通过设备连接到这台计算机系统并和他通信，从而达到访问数据的目的

优点：方便数据共享、消除了数据的冗余和不一致性  
缺点：可靠性不如分散式，主机出现故障所有系统全部瘫痪

##### 分布式

分布式是分散式和集中式的混合，类似我们学习过的计算机网络，是分散式的水平交互和集中式的垂直控制相结合的一种模式

它间距了分散式和集中式的优点：方便数据共享、消除了数据的冗余和不一致性，同样也加强了容错性

例：所有数据用专用的数据库集中存储，属于集中式  
        对数据的处理则由各个部门的软件分别控制，属于分布式

#### 2.C/S模型

C/S(Client/Server)模型也叫C/S模式，也就是上面的客户端和服务端的模式  
它是目前大多数网络通信采用的模型

#### 3.B/S模型

B/S(Browse/Server)模型也叫B/S模式，它是一种基于Web的通信模型，使用HTTP超文本传输协议通信  
B/S是一种特殊的C/S模型，特殊之处就是这种模型的客户端不需要我们自己开发，一般是某种浏览器，比如IE、Chrome等等

B/S模型的优点就是澹台计算机可以访问任何一个Web服务器，简单来说就是Web服务端可以随意变，但是客户端是通用的，我们不需要针对不同的服务端专门提供客户端应用程序了

#### 4.P2P模型

**P2P（Peer-to-Peer）模型**也叫对等互联，每个联网的设备同时运行一个应用程序的**客户端（Client）和服务端（Server）**部分，也就是说一个应用程序同时作为客户端和服务端

优点：通信方便，成本低  
缺点：可靠性不如C/S模型

##### 总结

对于网络游戏开发来说，我们采用**C/S模型**来进行前后端开发。  
在服务端的布局上往往使用的是**分布式**的形式进行管理。  
比如服务端的用户数据使用集中式进行管理，玩家的数据都存储在数据库应用（SQL Server、MySQL）中；

服务端应用程序使用分布式进行管理，账号服务器、游戏服务器、聊天服务器等等分布式管理，这些服务端应用程序都使用数据库中的数据分别进行逻辑处理。

**练习**：

1.游戏服务器的布局架构一般采用什么方式？

答：集中式和分布式

2.游戏中的通信模型一般采用什么方式？

答：C/S模型

## 网络协议

### 网络协议概述

**协议的字面意思**：经过谈判、协商而制定的共同承认、共同遵守的文件

**网络协议的基本概念**：  
网络协议是计算机网络中进行数据交换而建立的规则、标准或约定的集合  
指的是计算机网络中互相通信的对等实体之间交换信息时所遵守的规则的集合

**简单来说**：如果想要在网络环境中进行通信，那么网络协议就是你必须遵守的规则

OSI模型是网络通信的基本规则，TCP/IP协议是基于OSI模型的工业实现

简单来说：OSI模型是国际组织定义的一套理论基础，主要用于定义网络通信的规则  
TCP/IP协议是基于这套理论基础真正实现的通信规则

之后的网络通信API底层都是基于TCP/IP协议的

**练习**：

1. 一句话概括网络协议的作用

答：网络协议是计算机和设备之间进行通信的规则和标准，确保数据能够准确、可靠地传输和处理。

### OSI模型

#### OSI模型是什么

**OSI（Open System Interconnection Reference Model）开放式系统互联通信参考模型**，简称**OSI模型**，它是一种概念模型。  
由ISO（International Organization for Standardization）国际标准化组织提出，是一个试图使各种设备在世界范围内互联为网络的标准框架  
不同公司都按照统一的标准来控制网络互联通信，那么各设备之间就能够达到真正的互联通信了。

**简单来说：**  
OSI模型是人为定义的一个标准（规范），它制定了设备之间相互连接相互通信的标准（规范），各公司按照这个标准设计的规则（协议），就可以让不同设备利用互联网进行互联通信

#### OSI模型的规则

由于互联网协议（规则）很庞大，很复杂，所以OSI模型采用了分而治之的设计方法，把网络的功能划分为不同的多个模块，以分层的形式有机地组合在了一起

OSI模型将复杂的互联网实现分成了好几层（部分）  
每一层都要靠下一层的支持，用户接触到的都只是最上面的一层，感受不到下面层级的复杂性

**OSI模型**把互联通信的过程抽象的分成了七个层级：

> 1.  应用层
> 2.  表示层
> 3.  会话层
> 4.  传输层
> 5.  网络层
> 6.  数据链路层
> 7.  物理层

不同层级的职能各有不同。

#### OSI模型每层的职能

OSI七层模型相对来说是比较抽象的概念和一些硬件知识，其中每一层的内容深入后都有很多知识点。  
比如：交换机、路由器、网线等等硬件设备的工作原理，一些协议（规则）的具体内容  
这些知识对于软件开发来说，目前没有必要过于深入的去学习它们，因为这些知识体量较大且不能较快的提升我们的业务能力

所以我们的学习重点应该放在了解每一层大概做了什么上。

##### 1.物理层

**意义**：物理层就是把电脑连接起来的物理手段，它主要规定了网络的一些电气特性的作用是负责传送0和1的电信号。  
物理层将二进制数据利用电脉冲在物理没接上实现比特流的传输  
**它的主要功能是**：定义传输模式、定义传输速率、比特同步、比特编码等等

##### 2.数据链路层

**意义**：物理层用物理硬件来传输0和1，但是单纯的0和1没有任何意义  
必须规定解读方式，比如多少个信号算一组？每个信号位的意义？  
这就是数据链路层的意义，它在物理层上方确定0和1的分组方式，并且明确信息是发送给哪台计算机的网卡（Mac地址）。  
**功能**：将想要发送的信息构成一个数据包，叫做“帧”，每一个帧分为两个部分：标头Head+数据Data  
标头包含数据包的一些说明项，比如：发送方和接收方的Mac地址，数据类型等等

##### 3.网络层

**意义**：通过Mac地址定位一台计算机，理论上是可行的，但是效率非常低下  
我们之前学习过IP地址、端口、Mac地址，我们知道我们是通过IP地址快速的定位网络上的设备的而网络层的主要功能就和IP地址有关系

**功能**：**IP选址，路由选择**  
在网络环境中，两台设备之间可能会经过很多个数据链路，也可能还要经过很多通信子网  
网络层的主要作用就是选择一条合适的路径用于通信。它会在上一层的数据基础上添加标头  
**包含信息：IP地址、版本、长度等等信息**

##### 4.传输层

有了Mac地址和IP地址，我们已经可以在互联网上任意两台主机上建立通信了，但是如果没有端口号，我们无法准确的在应用程序之间通信。  
**传输层的功能，就是建立端口到端口的通信**

**功能**：**建立、管理和维护端到端的连接**  
传输层也称运输层，传输层负责主机中两个进程之间的通信，功能是为端到端连接提供可靠的传输服务。**它也会在上一层的数据基础上添加标头  
包含信息：发送方接收方的端口信息、协议信息等等**

##### 5.应用层

应用层为最上层，和用户直接打交道的可以联网的应用程序就属于这一层，比如浏览器、游戏等

**功能**：  
为应用程序提供服务，我们可以根据自己要传递的信息，决定使用哪一种协议（规则）来处理数据进行传输  
程序员主要开发的也是这一层，**它会在原始数据的基础上添加标头  
包含信息：协议信息等等**

常见的协议（规则）：FTP， HTTP ，SMTP等等

##### 6.表示层

不同操作系统的应用层代码和数据可能规范都不一样，为了让信息可以在各个操作系统和设备通用，表示层做的事情就很重要了

**功能**：  
数据格式转化、代码转换、数据加密  
为了让不同设备之间能够有统一的规则，表示层会把数据转换为能与各系统格式，兼容并且适合传输的格式。  
表示层就像是一个翻译，会把数据相关信息翻译成国际通用的规则。

##### 7.会话层

有了准备好的上层数据，那么这些数据最终就是希望被传递的内容，那么在信息传递时我们需要对其进行管理，比如消息是否发送完毕，对方是否收到，是否断开连接等等  
会话层的主要工作就是完成这些内容。

**功能**：**建立、管理和维护会话**  
它主要负责数据传输中设置和维护网络中两台设备之间的通信连接  
它不参与具体的传输，主要提供包括**访问验证和会话管理在内的建立和维护应用之间通信的机制**

**练习：**

1. OSI七层模型分别有哪些层？并简要说明每一层的作用

> 答：  
> **物理层**：负责比特流的传输，将数据转换为电信号或光信号，通过物理介质进行传输。  
> **数据链路层**：提供可靠的数据传输，负责将数据分帧，进行差错检测和纠正，确保数据在相邻节点间无误传输。  
> **网络层**：负责路径选择和数据包的转发，确保数据能够跨网络传输到目标地址。  
> **传输层**：提供端到端的可靠数据传输服务，支持流量控制、错误恢复以及数据分段与重组。  
> **会话层**：管理通信会话，负责建立、维护和终止应用程序之间的对话。  
> **表示层**：处理数据的格式转换、加密解密和压缩解压，确保不同系统之间的数据兼容性。  
> **应用层**：直接面向用户，提供网络服务接口，如HTTP、FTP、SMTP等协议，用于实现具体的网络应用。

### TCP/IP协议

#### 1.TCP/IP是什么

**TCP/IP（Transmission Control Protocol/Internet Protocol），TCP/IP传输协议，即传输控制/网络协议，也叫做网络通讯协议**  
是指能够在多个不同网络之间实现信息传输的协议簇，它是一个工业标准（即实际会使用的标准）  
**TCP/IP协议不仅仅指的是TCP和IP两个协议，而是指一个由FTP、SMTP、UDP、IP等等协议构成的协议簇**，只是因为TCP/IP协议中TCP和IP最具有代表性，所以被称为TCP/IP协议  
**它是正儿八经的用于互联网的通信协议**

简单来说：**TCP/IP是一系列规则的统称，他们定义了消息在网络间进行传输的规则，是供已连接互联网的设备进行通信的通信规则**

#### 2.TCP/IP的规则

> **TCP/IP网络结构体系实际上是基于OSI七层模型设计出来的。**  
> OSI七层模型只是一个概念模型，它主要用于描述、讨论和理解单个网络功能  
> 而TCP/IP协议是为了解决一组特定的问题而设计的，它是基于互联网开发的标准协议  
> 简单来说：**OSI只是一个概念模型，而TCP/IP协议是基于这个概念的具体实现**

TCP/IP协议把互联通信的过程抽象成了四个层级：  
1.应用层；2.传输层；3.网络层；4.网络接口层（数据链路层）。这四层是基于OSI模型进行设计的。

OSI是国际组织指定的适用于全世界计算机网络的统一标准，是一套基本规则和概念

TCP/IP是基于OSI根据目前的实际情况制定的一套规则，它主要用于对当前互联网结构体系提供一组规则，所有形式的网络传输都遵循这套规则，它是OSI概念的具体实现。  
在进行网络开发时，我们就是基于TCP/IP协议来进行网络通信的。

#### 3.TCP/IP每层的职能

##### 1.应用层

为应用程序提供服务：  
1.根据需求选择传输协议  
2.格式化数据、加密解密  
3.建立、管理和维护会话

添加协议头（FTP、HTTP等协议），一般决定传输信息的类型

##### 2.传输层

建立、管理和维护端到端的连接

添加协议端口头（TCP或UDP协议），一般决定传输信息的规则以及端口

##### 3.网络层

IP选址及路由选择

添加IP头，决定传输路线

##### 4.网络接口层（数据链路层）

1.提供一条准确无误的传输线路  
2.传输数据的物理媒介

产生帧（消息分段），决定最终路线

#### **4.TCP/IP的意义**

TCP/IP协议是基于OSI模型的具体实现是互联网中设备间通信的基本规则我们之后学习的网络通信的各种方式都是基于TCP/P协议的  
我们不用把它想得太过抽象和复杂了，只需要记住**想要进行网络通信，我们就遵循TCP/IP协议的规则进行消息传递就行了  
网络通信API都是基于TCP/IP协议的封装各种语言(C#、C++、Java、Go等等)都有对应的网络通信类对TCP/IP协议进行了封装  
我们只需要使用对应的类和方法进行网络连接、网络通信就可以完成对应的功能**

练习：

1. 分别说出TCP/IP协议的4层，并且说出上两层对应的部分协议

答：应用层、传输层、网络层、网络接口层；  
SMTP、UDP、TCP、HTTP等

### TCP和UDP

#### TCP/IP中的重要协议

应用层：  
HTTP：超文本传输协议  
HTTPS：加密的超文本传输协议  
FTP：文件传输协议  
DNS：域名系统

传输层：  
TCP：传输控制协议  
UDP：用户数据报 协议

网络层：  
IP

#### TCP和UDP的区别

连接方面：  
TCP：面向连接（例如打电话要先拨号建立连接）  
UDP：无连接，发送数据前不需要建立连接

安全方面：  
TCP：无差错、不丢失、不重复、按序到达  
UDP：只会尽力交付，不保证可靠性

传输效率：  
TCP：相对较低  
UDP：相对较高

连接对象：  
TCP：一对一  
UDP：一对一、一对多、多对一、多对多

#### TCP协议

TCP (Transmission Control Protocol, 传输控制协议)是面向连接的协议，  
也就是说，在收发数据前，必须和对方建立可靠的连接并且在消息传送过程中是有顺序的，并且是不会丢包(丢弃消息)的。如果某一条消息在传送过程中失败了，会重新发送消息，直到成功  
它的特点是:  
1.面向连接——两者之间必须建立可靠的连接  
2.一对一——只能是1对1的建立连接  
3.可靠性高——消息传送失败会重新发送，不允许丢包  
4.有序的——是按照顺序进行消息发送的

TCP协议中有一个重要概念：**三次握手，四次挥手**

三次握手：TCP连接请求（C to S），TCP授予连接（S to C），TCP确认连接（C to S）

四次挥手：数据发送完毕，等待服务器发送（C to S）;发送消息（S to C）;发送完毕（S to C）；等待，如果无回复则断开（C to S）

因为有了这个规则，所以可以提供可靠的服务，通过TCP连接传送的数据，可以做到：  
无差错、不丢失、不重复、按顺序到达

它让服务器和客户端之间的数据传递变得更加可靠

#### UDP协议

UDP (User Datagram Protocol,用户数据协议)是一种无需建立连接就可以发送封装的IP数据包的方法，提供面向事务的简单不可靠信息传送服务  
它的特点是:  
1.无连接—— 两者之间无需建立连接  
2.可靠性低——消息可能在传送过程中丢失，丢失后不会重发  
3.传输效率高——由于它的可靠性低并且也无需建立连接，所以传输效率上更高一些  
4.n对n——TCP只能1对1连接进行消息传递，而UDP由于无连接所以可以n对n

UDP协议不像TCP协议需要建立连接有三次握手和四次挥手  
所以当使用UDP协议发送信息时，会直接把信息数据扔到网络上，所以也就造成了UDP的不可靠性，信息在这个传递过程中是有可能丢失的  
虽然UDP是一个不靠谱的协议，但是由于它不需要建立连接，也不会像TCP协议那样携带更多的信息，所以它具有更好的传输效率它具有资源消耗小，处理速度快的特点

#### 总结

TCP：  
更可靠，保证数据的正确性和有序性（三次握手四次挥手）  
适合对信息准确性要求高，效率要求较低的使用场景

UDP：  
更效率，传输更快，资源消耗更少，适合对实时性要求高的使用场景

练习：

1. 请说明TCP协议和UDP协议的区别

答：

连接方面：  
TCP：面向连接（例如打电话要先拨号建立连接）  
UDP：无连接，发送数据前不需要建立连接

安全方面：  
TCP：无差错、不丢失、不重复、按序到达  
UDP：只会尽力交付，不保证可靠性

传输效率：  
TCP：相对较低  
UDP：相对较高

连接对象：  
TCP：一对一  
UDP：一对一、一对多、多对一、多对多

2. 请简述TCP协议 三次握手，四次挥手在做什么？

答：

三次握手：TCP连接请求（C to S），TCP授予连接（S to C），TCP确认连接（C to S）

四次挥手：数据发送完毕，等待服务器发送（C to S）;发送消息（S to C）;发送完毕（S to C）；等待，如果无回复则断开（C to S）

## 网络通信

### 网络游戏通信方案概述

#### 1.弱联网和强联网游戏

**弱联网游戏**：  
这种游戏不会频繁的进行数据通信，客户端和服务端之间每次连接只处理一次请求，服务端处理完客户端的请求后返回数据后就断开连接了

例：一般的三消游戏，卡牌游戏等都是弱联网游戏，这些游戏的核心玩法都由客户端完成，客户端处理完成后告诉服务端一个结果，服务端验证结果即可，不需要随时通信  
如：开心消消乐、刀塔传奇等

**强联网游戏**：  
这种游戏会频繁的和服务端进行通信，会一直和服务端保持连接状态，不停的和服务器之间交换数据

例：MMORPG、MOBA、ACT等都是强联网游戏，这些游戏的部分核心逻辑是由服务端进行处理，客户端和服务端之间不停的在同步信息  
如：刀塔2，最终幻想14、英雄联盟等等

#### 2.长连接和短连接游戏

长连接和短链接游戏是按照网络游戏通信特点来划分的，我们可以认为：  
弱联网→短链接，强联网→长连接

**短连接游戏**：  
需要传输数据时，建立连接，传输数据，获得响应，断开连接  
通信特点：需要通信时再连接，通信完毕后断开连接  
通信方式：HTTP超文本传输协议、HTTPS安全的超文本传输协议（本质为TCP协议）

**长连接游戏**：  
不管是否需要传输数据，客户端与服务器一直处于连接状态，除非一端主动断开，或者出现意外情况（客户端关闭或服务端崩溃等）  
通信特点：连接一直建立，可以实时的传输数据  
通信方式：TCP传输控制协议或UDP用户数据报协议

#### 3.Socket、HTTP、FTP

**Socket**：**网络套接字**，是对网络中不同主机上的应用进程之间进行双向通信的端点的抽象，一个套接字就是网络上进程通信的一段，提供了应用层进程利用网络协议交换数据的机制  
网络通信主要学习的就是Socket网络套接字当中的各种API来进行网络通信  
**主要用于制作长连接游戏（强联网游戏）**

**Http/Https**：**（安全的）超文本传输协议**，是一个简单的请求-响应协议，它通常运行在TCP协议之上，它指定了客户端可能发送给服务端什么样的信息以及得到什么样的响应。  
**主要用于制作短连接游戏（弱联网游戏），也可以用来进行资源下载**

**FTP**：**文件传输协议**，是用于再网络上进行文件传输的一套标准协议，可以利用它来进行网络上资源的下载和上传。它也是基于TCP的传输，是面向连接的，为文件传输提供了可靠的保证

##### 总结

网络游戏的通信方案大体上可以根据游戏的实际情况分为两种：  
长连接（强联网）游戏和短连接（弱联网）游戏

1.Socket网络套接字，只要用来完成长连接网络游戏需求  
2.Http超文本传输协议，主要用来完成短连接网络游戏需求（或资源下载相关）  
3.Ftp文件传输协议，主要用来完成资源的下载和上传等需求

练习：

1. 请简述长连接和短连接游戏的区别

答：

**短连接游戏**：  
需要传输数据时，建立连接，传输数据，获得响应，断开连接  
通信特点：需要通信时再连接，通信完毕后断开连接  
通信方式：HTTP超文本传输协议、HTTPS安全的超文本传输协议（本质为TCP协议）

**长连接游戏**：  
不管是否需要传输数据，客户端与服务器一直处于连接状态，除非一端主动断开，或者出现意外情况（客户端关闭或服务端崩溃等）  
通信特点：连接一直建立，可以实时的传输数据  
通信方式：TCP传输控制协议或UDP用户数据报协议

#### IP地址和端口类

想要进行网络通信，进行网络连接，首先我们需要找到对应的设备、IP和端口号是定位网络中设备必不可少的关键元素  
C#中提供了对应的IP和端口相关的类来声明对应信息

##### IPAddress类

命名空间：System.Net  
类名：IPAddress

初始化IP信息的方式：

```cs
byte[] ipAddress = new byte[] {123,123,123,11 };
IPAddress ip1 = new IPAddress(ipAddress);
IPAddress ip2 = new IPAddress(0x77665544);
IPAddress ip3 = IPAddress.Parse("111.111.11.11");
```

特殊的IP地址：127.0.0.1代表的是本机地址

获取可用的IPv6地址：IPAddress.IPv6Any

##### IPEndPoint类

命名空间：System.Net  
类名：IPEndPoint

IPEndPoint类将网络端点表示为IP地址和端口号，表现为IP地址和端口号的组合

初始化方式：

```cs
IPEndPoint ipPoint = new IPEndPoint(ip3, 8080);
IPEndPoint ipPoint2 = new IPEndPoint(IPAddress.Parse("192.168.2.10"), 8080);
```

练习：

1. 请声明一个IP地址为本机IP，端口号为8080的IPEndPoint对象

#### 域名解析

##### 什么是域名解析

域名解析也叫域名指向、服务器设置、域名配置以及反向IP登记等等  
说的简单点就是将好记得域名解析成IP  
IP地址是网络上标识站点的数字地址，但是IP地址相对来说记忆困难  
所以为了方便记忆，采用域名来代替IP地址标识站点地址

域名解析就是域名到IP地址的转换过程。域名的解析工作由DNS服务器完成  
我们在进行通信时有时会有需求通过域名获取IP

域名系统（Domain Name System，缩写DNS）是互联网的一项服务  
它作为将域名和IP地址相互映射的一个分布式数据库，能够使人更方便地访问互联网  
使因特网上解决网上机器命名的一种系统，因为IP地址记忆不方便，就采用了域名系统来管理名字和IP的对应关系

##### IPHostEntry类

命名空间：System.Net  
类名：IPHostEntry  
主要作用：域名解析后的返回值可以通过该对象获取IP地址、主机名等信息  
该类不会自己声明，都是作为某些方法的返回值信息，我们主要通过该类对象获取返回的信息

获取关键IP：成员变量：AddressList  
获取主机别名列表：成员变量：Aliases  
获取DNS鸣潮：成员变量：HostName

##### DNS类

命名空间：System.Net  
类名：Dns  
主要作用：Dns是一个静态类，提供了很多静态方法，可以使用它来根据域名获取IP地址

常用方法：

```cs
void Start()
{
    print(Dns.GetHostName());
    IPHostEntry entry = Dns.GetHostEntry("www.baidu.com");
    for (int i = 0; i < entry.AddressList.Length; i++)
    {
        print(entry.AddressList[i]);
        print(entry.Aliases[i]);
    }
    print(entry.HostName);
    GetHostEntry();
}
private async void GetHostEntry()
{
    Task<IPHostEntry> task = Dns.GetHostEntryAsync("www.baidu.com");
    await task;
    for (int i = 0; i < task.Result.AddressList.Length; i++)
    {
        print(task.Result.AddressList[i]);
        print(task.Result.Aliases[i]);
    }
    print(task.Result.HostName);
}
```

练习：

1. 什么是域名解析？

#### 序列化和反序列化二进制

在网络通信中，我们把想要传递的类对象信息序列化为二进制数据（一般为byte字节数组）  
再将该二进制数据通过网络传输给远端设备  
远端设备获取到该二进制数据后再将其反序列化为对应的类对象

对于序列化和反序列化，可以查看上一篇文章：
注意：  
在网络开发时，不会使用BinaryFormatter类来进行数据的序列化和反序列化  
因为客户端和服务端的开发语言大多数情况下是不同的  
BinaryFormatter类序列化的数据无法兼容其他语言

##### 如何将一个类对象转换为二进制

```cs
public class Test : MonoBehaviour
{
    void Start()
    {
        PlayerInfo info = new PlayerInfo();
        int indexNum = sizeof(int) +
        sizeof(int) +
        Encoding.UTF8.GetBytes(info.name).Length +
        sizeof(int) +
        sizeof(bool);
        byte[] playerBytes = new byte[indexNum];
        int index = 0;
        BitConverter.GetBytes(info.id).CopyTo(playerBytes, index);
        index += sizeof(int);
        byte[] nameBytes = Encoding.UTF8.GetBytes(info.name);
        BitConverter.GetBytes(nameBytes.Length).CopyTo(playerBytes, index);
        index += sizeof(int);
        nameBytes.CopyTo(playerBytes, index);
        index += nameBytes.Length;
        BitConverter.GetBytes(info.age).CopyTo(playerBytes, index);
        index += sizeof(int);
        BitConverter.GetBytes(info.sex).CopyTo(playerBytes, index);
        index += sizeof(bool);
    }
}
public class PlayerInfo
{
    public int id = 1;
    public string name = "123";
    public int age = 11;
    public bool sex = true;
}
```

##### 如何将二进制数据转为一个类对象

```cs
PlayerInfo player = new PlayerInfo();
byte[] bytes = playerBytes;
index = 0;
player.id = BitConverter.ToInt32(bytes, index);
index += sizeof(int);
int nameLength = BitConverter.ToInt32(bytes, index);
index += sizeof(int);
player.name = Encoding.UTF8.GetString(bytes, index, nameLength);
index += nameLength;
player.age = BitConverter.ToInt32(bytes, index);
index += sizeof(int);
player.sex = BitConverter.ToBoolean(bytes, index);
index += sizeof(bool);
```

### 套接字Socket

#### 1.Socket套接字的作用

它是C#提供给我们用于网络通信的一个类（在其它语言当中也有对应的Socket类）  
类名：Socket  
命名空间：System.Net.Sockets

Socket套接字是支持TCP/IP网络通信的基本操作单位  
一个套接字对象包含以下关键信息  
1.本机的IP地址和端口  
2.对方主机的IP地址和端口  
3.双方通信的协议信息

一个Socket对象标识一个本地或者远程套接字信息  
它可以被视为一个数据通道  
这个通道连接于客户端和服务器之间  
数据的发送和接受均通过这个通道进行

一般在制作长连接游戏时，我们会使用Socket套接字作为我们的通信方案  
我们通过它连接客户端和服务端，通过它来收发信息  
你可以把它抽象的想象成一根管子，插在客户端和服务端应用上，通过这个管子来传递交换信息

#### 2.Socket的类型

Socket套接字有3钟不同的类型

1.流套接字：主要用于实现TCP通信，提供了面向连接、可靠的、有序的、数据无差错且无重复的数据传输服务  
2.数据报套接字：主要用于实现UDP通信，提供了无连接的通信服务，数据包的长度不能大于32kb，不提供正确性检查，不保证顺序，可能出现重发、丢失等情况  
3.原始套接字（不常用）：主要用于实现IP数据包通信，用于直接访问协议的较低层，常用于倾听和分析数据包

通过Socket的构造函数，我们可以声明不同类型的套接字  
参数一：AddressFamily 网络寻址，枚举类型，决定寻址方案

常用：

1.  InterNetwork IPv4寻址
2.  InterNetwork6 IPv6寻址

做了解：

1.  UNIX        UNIX本地到主机地址
2.  ImpLink     ARPANETIMP地址
3.  Ipx             IPX或SPX地址
4.  Iso              ISO协议的地址
5.  Osi             OSI协议的地址
6.  NetBios      NetBios地址
7.  Atm            本机ATM服务地址

参数二：SocketType 套接字枚举类型，决定使用的套接字类型

常用：

1.  Dgram        支持数据报，最大长度固定的无连接、不可靠的信息（主要用于UDP）
2.  Stream       支持可靠、双向、基于连接的字节流（主要用于TCP）

做了解：

1.  Raw        支持对基础传输协议的访问
2.  Rdm        支持无连接、面向消息、以可靠方式发送的信息
3.  Seqpacket 提供排序字节流的面向连接且可靠的双向传输

参数三：ProtocolType        协议类型枚举类型，决定套接字使用的通信协议

常用：

1.  TCP        TCP传输控制协议
2.  UDP       UDP用户数据报协议

做了解：

1.  IP                IP网际协议
2.  Icmp           Icmp网际消息控制协议
3.  Igmp           Igmp网际组管理协议
4.  Ggp            网关到网关协议
5.  IPv4           Internet协议版本4
6.  Pup           PARC通用数据包协议
7.  Idp             Internet数据报协议
8.  Raw           原始IP数据包协议
9.  Ipx            Internet数据包交换协议
10.  Spx           顺序包交换协议
11.  IcmpV6     用于IPv6的Internet控制消息协议

二、三参数的常用搭配：  
SocketType.Dgram + ProtocolType.Udp = UDP协议通信（常用）  
SocketType.Stream + ProtocolType.Tcp = Tcp协议通信（常用）  
SocketType.Raw + ProtocolType.Icmp = UDP协议通信（了解）  
SocketType.Raw + ProtocolType.Raw = UDP协议通信（了解）

需要掌握的：

TCP流套接字

```cs
Socket socketTcp = new Socket(AddressFamily.InterNetwork,SocketType.Stream,ProtocolType.Tcp);
```

UDP数据报套接字

```cs
Socket socketUdp = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
```

#### 3.Socket的常用属性

```cs
socketTcp.Connected.ToString();
socketTcp.SocketType.ToString();
socketTcp.ProtocolType.ToString();
socketTcp.AddressFamily.ToString();
socketTcp.Available.ToString();
EndPoint point = socketTcp.LocalEndPoint;
EndPoint remotePoint = socketTcp.RemoteEndPoint;
```

#### 4.Socket的常用方法

##### 1.主要用于服务端

```cs
IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
socketTcp.Bind(ipPoint);
socketTcp.Listen(10);
socketTcp.Accept();
```

##### 2.主要用于客户端

```cs
socketTcp.Connect(IPAddress.Parse("128.66.66.6"), 8080);
```

##### 3.二者都会用的

```cs
socketTcp.Send(Encoding.UTF8.GetBytes("Hello World"));
socketUdp.SendTo();
socketTcp.Receive();
socketUdp.ReceiveFrom();
socketTcp.SendAsync();
socketTcp.Shutdown(SocketShutdown.Both);
socketTcp.Close();
```

#### 

#### TCP通信

##### 概述

###### 服务端和客户端需要做什么

客户端：1.创建套接字Socket；2.用Connect方法与服务端相连；3.用Send和Receive相关方法收发数据；4.用Shutdown方法释放连接；5.关闭套接字

服务端：1.创建套接字Socket；2.用Bind方法将套接字与本地地址绑定；3.用Listen方法监听；4.用Accept方法等待客户端连接；5.建立连接，Accept返回新套接字；6.用Send和Receive相关方法收发数据；7.用Shutdown方法释放连接；8.关闭套接字

##### 同步

###### 服务端

```cs
static void Main(string[] args)
{
    Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    try
    {
        socketTcp.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080));
    }
    catch (Exception e)
    {
        Console.WriteLine("绑定报错" + e.Message);
        return;
    }
    socketTcp.Listen(10);
    Console.WriteLine("等待客户端连入");
    Socket socketCli = socketTcp.Accept();
    Console.WriteLine("已连接到客户端");
    socketCli.Send(Encoding.UTF8.GetBytes("欢迎连接到服务器！"));
    byte[] result = new byte[1024];
    int receiveNum = socketCli.Receive(result);
    Console.WriteLine($"接收到了{socketCli.RemoteEndPoint.ToString()}发来的信息：{Encoding.UTF8.GetString(result,0,receiveNum)}");
    socketCli.Shutdown(SocketShutdown.Both);
    socketCli.Close();
}
```

###### 客户端

```cs
void Start()
{
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    try
    {
        socket.Connect(IPAddress.Parse("127.0.0.1"), 8080);
    }
    catch (SocketException e)
    {
        if (e.ErrorCode == 10061)
            print("服务器拒绝连接");
        else
            print("连接服务器发生错误，错误代码：" + e.ErrorCode);
        return;
    }
    byte[] receiveBytes = new byte[1024];
    int receiveNum = socket.Receive(receiveBytes);
    print("收到服务器消息：" + Encoding.UTF8.GetString(receiveBytes, 0, receiveNum));
    socket.Send(Encoding.UTF8.GetBytes("Hello, Server!"));
    socket.Shutdown(SocketShutdown.Both);
    socket.Close();
}
```

##### 服务端如何与多个客户端进行通信？

一个简单的示例：在实际开发中，逻辑处理等会比该示例复杂的多，我们需要对服务端和客户端进行良好的代码复用性的管理、封装，来降低维护成本和难度

```cs
public class Program
{
    static Socket socketTcp;
    static List<Socket> cliSockets = new List<Socket>();
    static bool isClose = false;
    static async Task Main(string[] args)
    {
        socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
        socketTcp.Bind(ipPoint);
        socketTcp.Listen(1024);
        Thread acceptThread = new Thread(AcceptClientConnect);
        acceptThread.Start();
        Thread receiveThread = new Thread(ReceiveMessage);
        receiveThread.Start();
        while (true)
        {
            Console.WriteLine("如果要退出，请输入exit");
            string input = Console.ReadLine();
            if (input == "exit")
            {
                isClose = true;
                for (int i = 0; i < cliSockets.Count; i++)
                {
                    cliSockets[i].Shutdown(SocketShutdown.Both);
                    cliSockets[i].Close();
                }
                break;
            }
            else if (input.Substring(0,5) == "send:")
            {
                for (int i = 0; i < cliSockets.Count; i++)
                {
                    cliSockets[i].Send(Encoding.UTF8.GetBytes(input.Substring(5)));
                }
            }
        }
    }
    static void AcceptClientConnect()
    {
        while (!isClose)
        {
            Socket socket = socketTcp.Accept();
            cliSockets.Add(socket);
            socket.Send(Encoding.UTF8.GetBytes("Welcome to the server!"));
        }
    }
    static void ReceiveMessage()
    {
        Socket socket;
        byte[] result = new byte[1024 * 1024];
        int bytesRead;
        string message;
        int i;
        while (!isClose)
        {
            for (i = 0; i < cliSockets.Count; i++)
            {
                socket = cliSockets[i];
                if (socket.Available > 0)
                {
                    result = new byte[1024];
                    bytesRead = socket.Receive(result);
                    message = Encoding.UTF8.GetString(result, 0, bytesRead);
                    ThreadPool.QueueUserWorkItem(HandleMessage,(socket,message));
                }
            }
        }
    }
    static void HandleMessage(object obj)
    {
        (Socket s, string str) info = ((Socket s, string str))obj;
        Console.WriteLine($"收到客户端{info.s.RemoteEndPoint}发来的：{info.str}");
    }
}
```

一个简单的客户端封装：

```cs
class ClientSocket
{
    private static int CLIENT_BEGIN_ID = 1;
    public int clientID;
    public Socket socket;
    public ClientSocket(Socket socket)
    {
        this.socket = socket;
        this.clientID = CLIENT_BEGIN_ID;
        CLIENT_BEGIN_ID++;
    }
    public bool IsConnected => this.socket.Connected;
    public void Close()
    {
        if(socket != null)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
            socket = null;
        }
    }
    public void Send(string message)
    {
        if(socket != null)
        {
            try
            {
                socket.Send(Encoding.UTF8.GetBytes(message));
            }
            catch(Exception e)
            {
                Console.WriteLine("发消息报错："+e.Message);
            }
        }
    }
    public void Receive()
    {
        if(socket != null)
        {
            byte[] buffer = new byte[1024 * 5];
            try
            {
                if(socket.Available > 0)
                {
                    int length = socket.Receive(buffer);
                    ThreadPool.QueueUserWorkItem(MessageHandle, Encoding.UTF8.GetString(buffer, 0, length));
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("收消息报错："+e.Message);
                Close();
            }
        }
    }
    private void MessageHandle(object obj)
    {
        Console.WriteLine($"收到了来自{socket.RemoteEndPoint}发来的消息：{obj as string}");
    }
}
```

一个简单的服务端封装：

```cs
class ServerSocket
{
    public Socket socketTcp;
    public Dictionary<int, ClientSocket> clientSockets = new Dictionary<int, ClientSocket>();

    public void Start(string ipAddress, int port, int listenNum)
    {
        socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
        socketTcp.Bind(ipPoint);
        socketTcp.Listen(listenNum);
        ThreadPool.QueueUserWorkItem(Accept);
        ThreadPool.QueueUserWorkItem(Receive);
    }

    public void Close()
    {
        foreach(ClientSocket client in clientSockets.Values)
        {
            client.Close();
        }
        clientSockets.Clear();
        socketTcp.Shutdown(SocketShutdown.Both);
        socketTcp.Close();
        socketTcp = null;
    }

    private void Accept(object obj)
    {
        while (true)
        {
            try
            {
                Socket clientSocket = socketTcp.Accept();
                ClientSocket client = new ClientSocket(clientSocket);
                clientSockets.Add(client.clientID, client);
                Console.WriteLine("客户端连接，ID：" + client.clientID);
            }
            catch (Exception e)
            {
                Console.WriteLine("客户端连接错误：" + e.Message);
            }
        }
    }
    private void Receive(object obj)
    {
        while (true)
        {
            if(clientSockets.Count == 0)
            {
                Thread.Sleep(100);
                continue;
            }
            else
            {
                foreach(ClientSocket client in clientSockets.Values)
                {
                    client.Receive();
                }
            }
        }
    }
}
```

###### 消息类型

###### 如何发送之前的自定义类的二进制消息

1.继承BaseData类  
2.实现其中的序列化、反序列化、获取字节数等相关方法  
3.发送自定义类数据时，序列化  
4.接收自定义类数据时，反序列化

###### 如何区分消息

为发送的信息添加标识，例如添加消息ID：  
在所有发送的消息头部加上消息ID（根据实际情况选择）

例如：  
用int类型作为消息ID的类型，前4个字节为消息ID，后面的字节为数据类的内容  
这样每次收到消息时，先把前四个字节取出来解析为消息ID，再根据ID进行消息反序列化即可

###### 分包、粘包

**基本概念和逻辑实现**

**什么是分包粘包？**

分包粘包是指在网络通信中由于各种因素（网络环境、API规则等）造成的消息与消息之间出现的两种状态  
分包：一个消息分成了多个消息进行发送  
粘包：一个消息和另一个消息黏在了一起

注意：分包和粘包可能同时发生

**如何解决分包粘包问题？**

我们收到的消息都是以字节数组的形式在程序中体现，目前我们的处理规则是默认传过来的消息就是正常情况：  
前四个字节是消息ID；后面的字节数组全部用来反序列化  
如果出现分包粘包就会导致我们的反序列化报错

那么问题就在于，如何去判断收到的字节数组的状态：正常；分包；粘包。

我们可以用区分消息类型的逻辑一样：为消息添加头部，头部记录消息的长度  
当我们接收到消息时，通过消息长度来判断是否分包粘包，再对消息进行拆分、合并处理  
我们每次只处理完整的消息

###### 心跳信息

解决目前断开不及时的问题：

1.客户端尝试使用Disconnet方法主动断开连接  
Socket当中有一个专门在客户端使用的方法：Disconnet方法  
客户端调用该方法和服务端断开连接  
看是否是因为之前直接Close而没有调用Disconnet造成服务器端无法及时获取状态

```cs
public void Close()
{
    if(socket != null)
    {
        print("客户端主动断开连接");
        socket.Shutdown(SocketShutdown.Both);
        socket.Disconnect(false);
        socket.Close();
        socket = null;
        isConnected = false;
    }
}
```

服务端：  
1.收发消息时判断socket是否已经断开  
2.处理删除记录的socket的相关逻辑（会用到线程锁）

2.自定义退出消息  
让服务器收到该消息就知道是客户端想要主动断开  
然后服务器处理释放socket相关工作

**为什么需要心跳消息？**

1.避免正常关闭客户端时，服务器无法正常收到关闭连接的消息  
通过心跳消息我们可以自定义超时判断，如果超时没有收到客户端消息，证明客户端已经断开连接

2.避免客户端长期不发送消息，防火墙或者路由器会断开连接，我们可以通过心跳消息一直保持活跃状态

**如何实现？**

客户端：定时发送消息

```cs
private int SEND_HEART_MSG_INTERVAL = 2;
private HeartMessage heartMsg = new HeartMessage();
private void Awake()
{
    instance = this;
    DontDestroyOnLoad(this.gameObject);
    InvokeRepeating("SendHeartMsg", 0, SEND_HEART_MSG_INTERVAL);
}
private void SendHeartMsg()
{
    if(isConnected)
        Send(heartMsg);
}
```

服务器：不停检测上次收到某客户端消息的时间，如果超时则认为连接已断开

```cs
public ClientSocket(Socket socket)
{
    this.socket = socket;
    this.clientID = CLIENT_BEGIN_ID;
    CLIENT_BEGIN_ID++;
    ThreadPool.QueueUserWorkItem(CheckTimeOut);
}
private void CheckTimeOut(object obj)
{
    while (IsConnected)
    {
        if (frontTime != -1 && DateTime.Now.Ticks / TimeSpan.TicksPerSecond - frontTime >= TIME_OUT_TIME)
        {
            Console.WriteLine("客户端" + clientID + "心跳超时，已断开连接");
            Program.socket.AddDelSocket(this);
            break;
        }
        Thread.Sleep(5000);
    }
}
```

##### 异步

###### **异步方法和同步方法的区别**

同步方法：方法中逻辑执行完毕后，再继续执行后面的方法  
异步方法：方法中逻辑可能还没有执行完毕，就继续执行后面的内容

异步方法的本质：  
往往异步方法当中都会使用多线程执行某部分逻辑，因为我们不需要等待方法中逻辑执行完毕就可以继续执行下面的逻辑了。

注意：Unity中的协同程序中的某些异步方法，有的使用的是多线程，有的是迭代器分步进行

**异步方法原理：  
例：**

1.线程回调：

```cs
private void Start()
{
    CountDownAsync(5, () =>
    {
        print("倒计时结束");
    });
}
ublic void CountDownAsync(int second, UnityAction callback)
{
    Thread t = new Thread(() =>
    {
        while (true)
        {
            print(second);
            Thread.Sleep(1000);
            --second;
            if (second <= 0)
                break;
        }
        callback?.Invoke();
    });
    t.Start();
    print("开始倒计时");
}
```

2.async和await 会等待线程执行完毕，继续执行后面的逻辑  
相对第一种方式，可以让函数分步执行

```cs
private void Start()
{
    CountDownAsync2(5);
}
public async void CountDownAsync2(int second)
{
    print("开始倒计时");
    await Task.Run(() =>
    {
        while (true)
        {
            print(second);
            Thread.Sleep(1000);
            --second;
            if (second <= 0)
                break;
        }
    });
    print("倒计时结束");
}
```

###### Socket TCP通信中的异步方法（Begin开头方法）

回调函数参数IAsyncResult  
AsyncState 调用异步方法时传入的参数，需要转换  
AsyncWaitHandle 用于同步等待

服务器相关  
BeginAccept  
EndAccept

```cs
private void Start()
{
    Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    socketTcp.BeginAccept(AcceptCallback, socketTcp);
}

private void AcceptCallback(IAsyncResult result)
{
    try
    {
        Socket s = result.AsyncState as Socket;
        Socket clientSocket = s.EndAccept(result);
        s.BeginAccept(AcceptCallback, s);
    }
    catch (SocketException e)
    {
        print(e.SocketErrorCode);
    }
}
```

客户端相关  
BeginConnect  
EndConnect

```cs
private void Start()
{
    Socket socketTcp = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    socketTcp.BeginAccept(AcceptCallback, socketTcp);
    IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
    socketTcp.BeginConnect(ipPoint, (result) =>
    {
        Socket s = result.AsyncState as Socket;
        try
        {
            s.EndConnect(result);
            print("连接成功");
        }
        catch (SocketException e)
        {
            print("连接出错，错误码：" + e.SocketErrorCode + e.Message);
        }
    } , socketTcp);
}
```

服务器客户端通用  
接收消息  
BeginReceive  
EndReceive

```cs
private void Start()
{
    socketTcp.BeginReceive(receiveBytes, 0, receiveBytes.Length, SocketFlags.None, ReceiveCallBack, socketTcp);
}
private void ReceiveCallBack(IAsyncResult result)
{
    try
    {
        Socket s = result.AsyncState as Socket;
        int num = s.EndReceive(result);
        Encoding.UTF8.GetString(receiveBytes, 0, num);
        s.BeginReceive(receiveBytes,0, receiveBytes.Length,SocketFlags.None,ReceiveCallBack,s);
    }
    catch (SocketException e)
    {
        print("接受消息处问题" + e.SocketErrorCode + e.Message);
    }
}
```

发送消息  
BeginSend  
EndSend

```cs
byte[] bytes = Encoding.UTF8.GetBytes("hello world");
socketTcp.BeginSend(bytes,0,bytes.Length, SocketFlags.None, (result) =>
{
    try
    {
        socketTcp.EndSend(result);
        print("发送成功");
    }
    catch (SocketException e)
    {
        print("发送错误" + e.SocketErrorCode + e.Message);
    }
},socketTcp);
```

关键变量类型：SocketAsyncEventArgs  
它会作为Async异步方法的传入值，我们需要通过它进行一些关键参数的赋值

服务器端：AcceptAsync

```cs
SocketAsyncEventArgs e = new SocketAsyncEventArgs();
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
e.Completed += (socket, args) =>
{
    if(args.SocketError == SocketError.Success)
    {
        Socket client = args.AcceptSocket;
        Debug.Log("Client connected: " + client.RemoteEndPoint.ToString());
        (socket as Socket).AcceptAsync(args);
    }
    else
    {
        Debug.LogError("Socket error: " + args.SocketError.ToString());
    }
};
socket.AcceptAsync(e);
```

客户端：ConnectAsync

```cs
e.Completed += (s, args) =>
{
    if (args.SocketError == SocketError.Success)
    {
    }
    else
    {
    }
};
socket.ConnectAsync(e);
```

服务端和客户端  
发送信息：SendAsync

```cs
byte[] bytes = Encoding.UTF8.GetBytes("Hello, World!");
e.SetBuffer(bytes,0,bytes.Length);
e.Completed += (socket, args) =>
{
    if(args.SocketError == SocketError.Success)
    {
        Debug.Log("Message sent successfully!");
    }
    else
    {
        Debug.LogError($"Failed to send message: {args.SocketError}");
    }
};
socket.SendAsync(e);
```

接受信息：ReceiveAsync

```cs
e.SetBuffer(new byte[1024*1024], 0, 1024 * 1024);
e.Completed += (socket, args) =>
{
    if(args.SocketError == SocketError.Success)
    {
        Encoding.UTF8.GetString(args.Buffer, 0, args.BytesTransferred);
        args.SetBuffer(0,1024 * 1024);
        (socket as Socket).ReceiveAsync(args);
    }
    else
    {
    }
};
socket.ReceiveAsync(e);
```

总结：

C#中网络通信异步方法中，主要提供了两种方案  
1.Begin开头的API  
内部开多线程，通过回调形式返回结果，需要和End相关方法配合使用

2.Async结尾的API  
内部开多线程，通过回调形式返回结果，依赖SocketAsyncEventArgs对象配合使用  
可以让我们更加方便的进行操作

###### 服务端

```cs
public class ClientSocket
{
    public Socket socket;
    public int clientID;
    private static int CLIENT_ID_COUNTER = 1;
    private byte[] cacheBytes = new byte[1024 * 1024];
    private int cacheNum = 0;
    public ClientSocket(Socket socket)
    {
        this.socket = socket;
        this.clientID = CLIENT_ID_COUNTER++;
        this.socket.BeginReceive(cacheBytes,cacheNum,cacheBytes.Length,SocketFlags.None, ReceiveCallBack,socket);
    }
    private void ReceiveCallBack(IAsyncResult result)
    {
        try
        {
            cacheNum = socket.EndReceive(result);
            Console.WriteLine(Encoding.UTF8.GetString(cacheBytes,0,cacheNum));
            if(socket.Connected)
                this.socket.BeginReceive(cacheBytes, cacheNum, cacheBytes.Length, SocketFlags.None, ReceiveCallBack, socket);
            else
                Console.WriteLine("没有连接，不用再收消息了");
        }
        catch (SocketException e)
        {
            Console.WriteLine("接受消息错误"+e.SocketErrorCode + e.Message);
        }
    }
    public void Send(string str)
    {
        if(socket.Connected)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            this.socket.BeginSend(bytes,0,bytes.Length,SocketFlags.None,SendCallBack,null);
        }
    }
    private void SendCallBack(IAsyncResult result)
    {
        try
        {
            socket.EndSend(result);
        }
        catch (SocketException e)
        {
            Console.WriteLine("发送失败" + e.SocketErrorCode + e.Message);
        }
    }
}
```

```cs
public class ServerSocket
{
    private Socket socket;
    private Dictionary<int, ClientSocket> clientSockets = new Dictionary<int, ClientSocket>();
    public void Start(string ip,int port,int num)
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        try
        {
            socket.Bind(ipPoint);
            socket.Listen(num);
            socket.BeginAccept(AcceptCallBack, socket);
        }
        catch (Exception e)
        {
            Console.WriteLine("启动服务器失败"+e.Message);
        }
    }
    private void AcceptCallBack(IAsyncResult result)
    {
        try
        {
            Socket clientSocket = socket.EndAccept(result);
            ClientSocket client = new ClientSocket(clientSocket);
            clientSockets.Add(client.clientID,client );
            socket.BeginAccept(AcceptCallBack, socket);
        }
        catch (Exception e)
        {
            Console.WriteLine("客户端连入失败"+e.Message);
        }
    }
    public void Broadcast(string str)
    {
        foreach (ClientSocket client in clientSockets.Values)
        {
            client.Send(str);
        }
    }
}
```

###### 客户端

```cs
public class NetAsyncMgr : MonoBehaviour
{
    private static NetAsyncMgr instance;
    public static NetAsyncMgr Instance => instance;
    private Socket socket;
    private byte[] cacheBytes = new byte[1024 * 1024];
    private int chacheNum = 0;
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
    }
    public void Connect(string ip, int port)
    {
        if(socket != null && socket.Connected)
        return;
        IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.RemoteEndPoint = ipPoint;
            args.Completed += (socket, args) =>
            {
                if(args.SocketError == SocketError.Success)
                {
                    print("连接成功");
                    SocketAsyncEventArgs receiveArgs = new SocketAsyncEventArgs();
                    receiveArgs.SetBuffer(cacheBytes, 0, cacheBytes.Length);
                    receiveArgs.Completed += ReceiveCallBack;
                    this.socket.ReceiveAsync(receiveArgs);
                }
                else
                {
                    print("连接失败，错误码：" + args.SocketError);
                }
            };
            socket.ConnectAsync(args);
        }
        catch (Exception e)
        {
            throw;
        }
    }
    private void ReceiveCallBack(object sender, SocketAsyncEventArgs args)
    {
        if(args.SocketError == SocketError.Success)
        {
            print(Encoding.UTF8.GetString(args.Buffer, 0, args.BytesTransferred));
            args.SetBuffer(0, cacheBytes.Length);
            if (socket != null && socket.Connected)
                socket.ReceiveAsync(args);
            else
                Close();
        }
        else
        {
            print("接收数据失败，错误码：" + args.SocketError);
            Close();
        }
    }
    public void Close()
    {
        if(socket != null)
        {
            socket.Shutdown(SocketShutdown.Both);
            socket.Disconnect(false);
            socket.Close();
            socket = null;
        }
    }
    public void Send(string str)
    {
        if(socket != null && socket.Connected)
        {
            byte[] sendBytes = Encoding.UTF8.GetBytes(str);
            SocketAsyncEventArgs args = new SocketAsyncEventArgs();
            args.SetBuffer(sendBytes, 0, sendBytes.Length);
            args.Completed += (socket, args) =>
            {
                if(args.SocketError == SocketError.Success)
                {
                    print("发送成功");
                }
                else
                {
                    print("发送失败，错误码：" + args.SocketError);
                    Close();
                }
            };
            socket.SendAsync(args);
        }
        else
        {
            Close();
        }
    }
}
```

```cs
public class MainAsync : MonoBehaviour
{
    private void Start()
    {
        if (NetAsyncMgr.Instance == null)
        {
            GameObject go = new GameObject("NetAsyncMgr");
            go.AddComponent<NetAsyncMgr>();
        }
        NetAsyncMgr.Instance.Connect("127.0.0.1", 8080);
    }
}
```

#### UDP通信

##### 概述

客户端和服务端需要做什么：

1.创建套接字Socket  
2.用Bind方法将套接字与本地地址进行绑定  
3.用ReceiveFrom和SendTo方法在套接字上收发消息  
4.用Shutdown方法释放连接  
5.关闭连接

**粘包问题**

UDP本身作为无连接的不可靠的传输协议（适合频繁发送较小的数据包）  
他不会对数据包进行合并发送  
一端发送什么数据，直接就发出去了，他不会对数据合并  
因此在UDP当中不会出现粘包问题（除非手动进行粘包）

**分包问题**

由于UDP是不可靠的连接，消息传递过程中可能出现无序、丢包等情况  
所以如果允许UDP进行分包，那后果将会是灾难性的  
比如分包的后半段丢包或者比上半段先发来，我们在处理消息时将会非常困难  
因此为了避免分包，建议在发送UDP消息时  
控制消息的大小在MTU（最大传输单元）范围内

MTU（Maximum Transmission Unit）最大传输单元，用来通知对方所能接受数据服务单元的最大尺寸  
不同操作系统会提供给用户一个默认值  
以太网和802.3对数据帧的长度限制，其最大值分别是1500字节和1492字节  
由于UDP包本身带有一些信息，因此建议：  
1.局域网环境下：1472字节以内（1500减去UDP头部28为1472）  
2.互联网环境下：548字节以内（老的ISP拨号网络的标准值为576减去UDP头部28为548）  
只要遵守这个规则，就不会出现自动分包的情况

如果想要发送的消息确实比较大，要大于548字节或1472字节这个限制呢？  
比如要发一个5000字节的数据，他是一条完整消息  
我们可以进行手动分包，将5000拆分成多个消息，每个消息不超过限制  
但是手动分包的前提是要解决UDP的丢包和无序问题

我们可以将不可靠的UDP通信实现为可靠的UDP通信  
比如：在消息中加入序号、消息总包数、自己的包ID、长度等等信息，并且实现消息确认、消息重发等功能

**分包、粘包问题**

1.UDP不会粘包  
2.因为UDP不可靠，我们要避免其自动分包，消息大小要控制在一定范围内

但是我们可以根据自己的需求，在实现逻辑时加入分包粘包功能  
1.消息过小可以手动粘包  
2.消息过大可以手动分包  
但是对于手动分包，我们必须解决UDP无序和丢包问题

##### 同步

客户端：

```cs
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
IPEndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
socket.Bind(ipPoint);
IPEndPoint targetPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8081);
socket.SendTo(Encoding.UTF8.GetBytes("Hello UDP"), targetPoint);
byte[] buffer = new byte[1024];
EndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
int length = socket.ReceiveFrom(buffer,0,buffer.Length, SocketFlags.None, ref remotePoint);
print((remotePoint as IPEndPoint).Address.ToString() + "发来了" +
Encoding.UTF8.GetString(buffer, 0, length));
socket.Shutdown(SocketShutdown.Both);
socket.Close();
```

服务端：

```cs
Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
socket.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"),8081));
byte[] buffer = new byte[1024];
EndPoint remotePoint = new IPEndPoint(IPAddress.Any, 0);
int length = socket.ReceiveFrom(buffer, 0, buffer.Length, SocketFlags.None, ref remotePoint);
Console.WriteLine((remotePoint as IPEndPoint).Address.ToString() + "发来了" +
Encoding.UTF8.GetString(buffer, 0, length));
string str = "你好，客户端！";
byte[] data = Encoding.UTF8.GetBytes(str);
socket.SendTo(data, 0, data.Length, SocketFlags.None, remotePoint);
socket.Shutdown(SocketShutdown.Both);
socket.Close();
Console.ReadKey();
```

##### 异步

**UDP通信中的异步方法：**

BeginSendTo:

```cs
private void Start()
{
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    byte[] bytes = Encoding.UTF8.GetBytes("123123123123");
    EndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
    socket.BeginSendTo(bytes,0,bytes.Length,SocketFlags.None,ipPoint,SendToOver,socket);
}
private void SendToOver(IAsyncResult result)
{
    try
    {
        Socket s = result.AsyncState as Socket;
        s.EndSendTo(result);
        print("发送成功");
    }
    catch(SocketException s)
    {
        print("发送消息失败，异常信息：" +s.SocketErrorCode + s.Message);
    }
}
```

BeginReceiveFrom:

```cs
private byte[] cacheBytes = new byte[1024];
private void Start()
{
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    byte[] bytes = Encoding.UTF8.GetBytes("123123123123");
    EndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
    socket.BeginReceiveFrom(cacheBytes, 0, cacheBytes.Length, SocketFlags.None, ref ipPoint, ReceiveCallBack, (socket, ipPoint));
}
private void ReceiveCallBack(IAsyncResult ar)
{
    try
    {
        (Socket s ,EndPoint ipPoint) info = ((Socket, EndPoint))ar.AsyncState;
        info.s.EndReceiveFrom(ar, ref info.ipPoint);
        info.s.BeginReceiveFrom(cacheBytes, 0, cacheBytes.Length, SocketFlags.None, ref info.ipPoint, ReceiveCallBack, info);
    }
    catch (SocketException s)
    {
        print("接收消息出问题" + s.SocketErrorCode + s.Message);
    }
}
```

SendToAsync：

```cs
private void Start()
{
    Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
    byte[] bytes = Encoding.UTF8.GetBytes("123123123123");
    EndPoint ipPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8080);
    SocketAsyncEventArgs args = new SocketAsyncEventArgs();
    args.SetBuffer(bytes, 0, bytes.Length);
    args.Completed += SendCallBack;
    socket.SendToAsync(args);
}
private void SendCallBack(object s, SocketAsyncEventArgs args)
{
    if (args.SocketError == SocketError.Success)
    {
        print("发送成功");
    }
    else
    {
        print("发送失败");
    }
}
```

### 文件传输FTP

#### FTP工作原理

##### 1.FTP是什么

FTP（File Transfer Protocol）文件传输协议，是支持Internet文件传输的各种规划所组成的集合  
这些规则使Internet用户可以把文件从一台主机拷贝到另一台主机上  
除此之外，FTP还提供登录、目录查询以及其他会话控制等功能

说人话：FTP文件传输协议就是一个在网络中上传下载文件的一套规则

##### 2.FTP工作原理

FTP的本质是TCP通信，通过FTP传输文件，双方至少需要建立两个TCP连接  
一个称为控制连接，用于传输FTP命令  
一个称为数据连接，用于传输文件数据

当客户端和FTP服务器建立控制连接后，需要高速服务器采用哪种传输模式  
1.主动模式（Port模式）：服务器主动连接客户端，然后传输文件  
2.被动模式（Passive模式）：客户端主动连接服务器，即控制连接和数据连接都由客户端发起

一般情况下主动模式会受到客户端防火墙影响，所以被动模式用的比较多

在使用FTP进行数据传输时，有两种数据传输方式  
1.ASCII传输方式  
以ASCII编码方式传输数据，适用于传输仅包含英文的命令和参数或者英文文本文件  
2.二进制传输方式（推荐）  
可以指定采用哪种编码传输命令和文件数据  
如果传输的文件不是英文文件则应该采用该方式

一般情况下，使用FTP传输文件时，客户端必须先登录服务器，获得相应权限后才能上传或下载  
服务器也可以允许用户匿名登录FTP，不需要都拥有一个合法账号

#### 搭建FTP服务器

搭建服务器的几种方式：  
1.使用别人做好的FTP服务器软件（学习阶段建议使用）  
2.自己编写FTP服务器应用程序，基于FTP的工作原理，用Socket中TCP通信来编程  
3.将电脑搭建为FTP文件共享服务器

在这里只讲述第一种方式

##### 使用别人做好的FTP服务器软件来搭建FTP服务器

下载Serv-U等FTP服务器软件，在想要作为FTP服务器的电脑上运行  
例：Serv-U  
1.创建域，一直下一步即可  
2.使用单向加密  
3.创建用于上传下载的FTP账号密码

#### FTP关键类

##### 1.NetworkCredential类

命名空间：System.Net  
NetworkCredential通信凭证类  
用于Ftp文件传输时，设置账号密码

```cs
NetworkCredential n = new NetworkCredential("username","password");
```

##### 2.FtpWebRequest类

```cs
FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://127.0.0.1/Test.txt")) as FtpWebRequest;
req.Abort();
Stream s = req.GetRequestStream();
FtpWebResponse res = req.GetResponse() as FtpWebResponse;
```

**重要成员：**

1.Credentials 通信凭证，设置为NetworkCredential对象  
2.KeepAlive bool值，当完成请求时是否关闭到FTP服务器的控制连接（默认为true，不关闭）  
3.Method         操作命令设置  
        WebRequestMethods.Ftp类中的操作命令属性  
        DeleteFile        删除文件  
        DownloadFile        下载文件  
        ListDirectory        获取文件简短列表  
        ListDirectoryDetails        获取文件详细列表  
        MakeDirectory        创建目录  
        RemoveDirectory        删除目录  
        UploadFile        上传文件  
4.UseBinary        是否使用二进制传输  
5.RenameTo        重命名

##### 3.FtpWebResponse类

命名空间：System.Net  
它是用于封装FTP服务器对请求的响应，提供了操作状态以及从服务器下载数据的功能  
我们可以通过FtpWebRequest对象中的GetResponse()方法获取，当使用完毕时，要使用Close释放

重要方法：  
1.Close：释放所有资源  
2.GetResponseStream：返回从FTP服务器下载数据的流

重要成员：  
1.ContentLength：接受到数据的长度  
2.ContentType：接收数据的类型  
3.StatusCode：FTP服务器下发的最新状态码  
4.StatusDescription：FTP服务器下发的状态代码的文本  
5.BannerMessage：登陆前建立连接时FTP服务器发送的消息  
6.ExitMessage：FTP会话结束时服务器发送的信息  
7.LastModified：FTP服务器上的文件的上次修改日期和时间

#### 上传文件

**使用FTP上传文件的关键点**  
1.通信凭证：进行FTP连接操作时需要的账号密码  
2.操作命令：WebRequestMethods.Ftp  
3.文件流相关：FileStream和Stream：上传和下载时都会使用文件流  
4.确保FTP服务器已开启，并且能够正常访问

**FTP上传步骤**

1.创建一个Ftp连接  
2.设置通信凭证（如果不支持匿名，就必须设置这一步）  
请求完毕后，是否关闭控制连接，如果想要关闭，可以设置为false  
3.设置操作命令  
4.指定传输类型  
5.得到用于上传的流对象  
6.开始上传

```cs
try
{
    FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://127.0.0.1/Test.txt")) as FtpWebRequest;
    req.Proxy = null;
    NetworkCredential n = new NetworkCredential("werllra", "61527556");
    req.Credentials = n;
    req.KeepAlive = false;
    req.Method = WebRequestMethods.Ftp.UploadFile;
    req.UseBinary = true;
    Stream upLoadStream = req.GetRequestStream();
    using (FileStream file = File.OpenRead(Application.streamingAssetsPath + "/Test.txt"))
    {
        byte[] bytes = new byte[1024];
        int contentLength = file.Read(bytes, 0, bytes.Length);
        while (contentLength != 0)
        {
            upLoadStream.Write(bytes, 0, contentLength);
            contentLength = file.Read(bytes, 0, bytes.Length);
        }
        file.Close();
        upLoadStream.Close();
        print("上传结束");
    }
}
catch (Exception e)
{
print("上传出错了，错误信息：" + e.Message);
}
```

#### 下载文件

##### 使用FTP下载关键点

1.通信凭证：进行FTP连接操作时需要的账号密码  
2.操作命令：WebRequestMethods.Ftp  
3.文件流相关FileStream和Stream  
        上传和下载时都会使用的文件流  
        下载文件流使用FtpWebResponse类获取  
4.保证FTP服务器已开启，并且能够正常访问

##### FTP下载代码示例

```cs
try
{
    FtpWebRequest req = FtpWebRequest.Create(new Uri("ftp://127.0.0.1/Test.txt")) as FtpWebRequest;
    req.Credentials = new NetworkCredential("werllra", "password");
    req.KeepAlive = false;
    req.Method = WebRequestMethods.Ftp.DownloadFile;
    req.UseBinary = true;
    req.Proxy = null;
    FtpWebResponse response = req.GetResponse() as FtpWebResponse;
    Stream downLoadStream = response.GetResponseStream();
    using (FileStream stream = File.Create(Application.persistentDataPath + "/test.txt"))
    {
        byte[] buffer = new byte[1024];
        int contentLength = downLoadStream.Read(buffer, 0, buffer.Length);
        while(contentLength != 0)
        {
            stream.Write(buffer, 0, contentLength);
            contentLength = downLoadStream.Read(buffer,0, buffer.Length);
        }
        stream.Close();
        downLoadStream.Close();
    }
    print("下载结束");
}
catch (Exception e)
{
print("下载出错" + e.Message);
}
```

#### 其他操作

##### 其他操作指什么？

除了上传和下载，我们可能会对FTP服务器上的内容进行其他操作  
比如：删除文件、获取文件大小、创建文件夹、获取文件列表等等

##### 代码示例

```cs
public async void RemoveFile(string fileName,UnityAction<bool> action = null)
{
    await Task.Run(() =>
    {
        try
        {
            FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + fileName)) as FtpWebRequest;
            req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
            req.KeepAlive = false;
            req.UseBinary = true;
            req.Method = WebRequestMethods.Ftp.DeleteFile;
            req.Proxy = null;
            FtpWebResponse res = req.GetResponse() as FtpWebResponse;
            res.Close();
            action?.Invoke(true);
        }
        catch (Exception e)
        {
            Debug.Log("移除失败"+ e.Message);
            action?.Invoke(false);
        }
    });
}
public async void GetFileSize(string fileName, UnityAction<long> action = null)
{
    await Task.Run(() =>
    {
        try
        {
            FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + fileName)) as FtpWebRequest;
            req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
            req.KeepAlive = false;
            req.UseBinary = true;
            req.Method = WebRequestMethods.Ftp.GetFileSize;
            req.Proxy = null;
            FtpWebResponse res = req.GetResponse() as FtpWebResponse;
            action?.Invoke(res.ContentLength);
            res.Close();
        }
        catch (Exception e)
        {
            Debug.Log("获取失败" + e.Message);
            action?.Invoke(0);
        }
    });
}
public async void CreateDirectory(string dirName, UnityAction<bool> action = null)
{
    await Task.Run(() =>
    {
        try
        {
            FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + dirName)) as FtpWebRequest;
            req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
            req.KeepAlive = false;
            req.UseBinary = true;
            req.Method = WebRequestMethods.Ftp.MakeDirectory;
            req.Proxy = null;
            FtpWebResponse res = req.GetResponse() as FtpWebResponse;
            action?.Invoke(true);
            res.Close();
        }
        catch (Exception e)
        {
            Debug.Log("创建失败" + e.Message);
            action?.Invoke(false);
        }
    });
}
public async void GetFileList(string dirName,UnityAction<List<string>> action = null)
{
    await Task.Run(() =>
    {
        try
        {
            FtpWebRequest req = FtpWebRequest.Create(new Uri(FTP_PATH + dirName)) as FtpWebRequest;
            req.Credentials = new NetworkCredential(USER_NAME, PASSWORD);
            req.KeepAlive = false;
            req.UseBinary = true;
            req.Method = WebRequestMethods.Ftp.ListDirectory;
            req.Proxy = null;
            FtpWebResponse res = req.GetResponse() as FtpWebResponse;
            StreamReader streamReader = new StreamReader(res.GetResponseStream());
            List<string> list = new List<string>();
            string line = streamReader.ReadLine();
            while (line != null)
            {
                list.Add(line);
                line = streamReader.ReadLine();
            }
            res.Close();
            action?.Invoke(list);
        }
        catch (Exception e)
        {
            Debug.Log("获取文件列表失败" + e.Message);
            action?.Invoke(null);
        }
    });
}
```

##### 总结

FTP对于我们的作用  
1.游戏当中的一些上传和下载功能  
2.原生AB包上传下载  
3.上传下载一些语音内容

只要是上传下载相关的功能，都可以使用Ftp来完成

### 超文本传输HTTP

#### HTTP工作原理

##### HTTP是什么

**HTTP（HyperText Transfer Protocol）超文本传输协议**，是因特网上应用最为广泛的一种网络传输协议。最初设计HTTP的目的是为了提供一种发布和接收由文本文件组成的HTML页面的方法，后来发展到除了文本数据外，还可以传输图片、音频、视频、压缩文件以及各种程序文件等等。  
HTTP主要用于超文本传输，因此相对FTP显得更简单一些

HTTP超文本传输协议就是一个在网络中上传下载文件的一套规则

**HTTP的本质也是TCP通信**

HTTP定义了Web客户端（一般指浏览器）如何从Web服务器请求Web页面，以及服务器如何把Web页面传送给客户端  
HTTP客户端首先与服务器建立TCP连接，然后客户端通过套接字发送HTTP请求，并通过套接字接收HTTP响应，由于HTTP采用TCP传输数据，因此不会丢包、不会乱序。

HTTP的工作原理主要有以下三个特点：  
**1.HTTP是以TCP方式工作的  
2.HTTP是无状态的  
3.HTTP使用元信息作为标头**

下面依次讲解：

###### 1.HTTP是以TCP方式工作的

在HTTP/1.0中，客户端和服务器建立TCP连接后，发送一个请求到服务器，服务器发送一个应答给客户端，然后立即断开TCP连接，他们的主要步骤为：  
**1.客户端与服务端建立TCP连接  
2.客户端向服务端发出请求  
3.若服务端接收请求，则回送响应码和所需的信息  
4.客户端与服务端断开TCP连接**

需要注意，HTTP/1.1支持持久连接，即客户端和服务端建立连接后，可以发送请求和接收应答，然后迅速的发送另一个请求和接收另一个应答。  
持久连接也使得在得到上一个请求的应答之前能够发送多个请求，这就是HTTP/1.1和HTTP/1.0的明显不同之处，除此之外，HTTP/1.1可以发送的请求类型也比HTTP/1.0多。  
目前市面上的Web服务器软件和浏览器软件基本都是支持HTTP/1.1版本的，目前使用的基本上都是HTTP/1.1版本

###### 2.HTTP是无状态的

无状态指：客户端发送一次请求后，服务端并没有存储关于该客户端的任何状态信息，即使客户端再次请求同一个对象，服务端仍会重新发送这个对象，不会在意之前是否已经向客户端发送过这个对象

简单来说，HTTP通信就是客户端要什么来什么，想要多少来多少，服务端不会因为要过了就不给，不会记录状态

###### 3.HTTP使用元信息作为标头

HTTP通过添加标头（header）的方式向服务端提供本次HTTP请求的相关信息，即在主要数据前添加一部分额外信息，称为元信息（metainformation）  
元信息里主要包含：传送的对象属于哪种类型，采用的是哪种编码等

简单来说：HTTP的元信息标头，类似于Socket通信时用于区分消息类型、处理分包粘包时，在消息体前方添加的自定义信息  
在HTTP协议中，它也定义了类似的规则，在头部包含了一些额外信息

##### HTTP协议的请求类型和响应状态码

请求类型：  
HTTP/1.0中：GET、POST、HEAD  
HTTP/1.1中：GET、POST、HEAD、PUT、DELETE、OPTIONS、TRACE、CONNECT

响应状态码：1xx、2xx、3xx、4xx、5xx

在这里主要讲解GET和POST，其他请自行查阅资料

GET：请求获取特定的资源，比如请求一个Web页面或请求获取一个资源  
POST：请求提交数据进行处理，比如请求上传一个文件

状态行的主要内容有：  
1.HTTP版本号  
2.3位数字组成的状态码：  
        1xx消息：请求已被接收，继续处理  
        2xx成功：请求已成功被服务端理解并接收  
        3xx重定向：需要后续操作才能完成这一请求  
        4xx请求错误：请求韩游语法错误或者无法被执行  
        5xx服务器错误：服务端在处理某个正确请求时发生错误

常用状态码：

200：OK，找到资源，一切正常  
304：NOT MODIFIED，资源在上次请求后没有任何修改（常用语缓存机制）  
401：UNAUTHORIZED，客户护短无权访问该资源，通常需要输入用户名和密码  
403：FORBIDDEN，客户端无授权，通常是401后输入了错误用户名密码  
404：NOT FOUND，指定位置不存在申请的资源  
405：Method Not Allowed，不支持请求的方法  
501：Not Implemented，服务器不能识别请求或者没有实现指定的请求

#### 搭建HTTP服务器

搭建HTTP服务器的几种方式

1.使用别人做好的HTTP服务器软件，一般作为资源服务器时使用该方式（学习阶段使用）  
2.自己编写HTTP服务器应用程序，一般作为Web服务器或者短连接游戏服务器时使用该方式

#### C#的相关类

##### HTTP关键类

###### HttpWebRequest

命名空间：System.Net  
HttpWebRequest是主要用于发送客户端请求的类  
主要用于：发送HTTP客户端的请求给服务器，可以进行消息通信、上传、下载等操作

```cs
HttpWebRequest req = HttpWebRequest.Create(new Uri("http://192.168.1.2:8080/HTTP_SERVER")) as HttpWebRequest;
Stream s = req.GetRequestStream();
HttpWebResponse res = req.GetResponse() as HttpWebResponse;
req.Credentials = new NetworkCredential("username", "password");
req.PreAuthenticate = true;
req.ContentLength = 100;
req.Method = WebRequestMethods.Http.Get;
```

###### HttpWebResponse

命名空间：System.Net  
它主要用于或偶去服务器反馈信息的类  
我们可以通过HttpWebRequest对象中的GetResponse()方法获取  
当使用完毕时，要使用Close释放

```cs
HttpWebRequest req = HttpWebRequest.Create(new Uri("http://localhost:8080/HTTP_SERVER")) as HttpWebRequest;
HttpWebResponse res = req.GetResponse() as HttpWebResponse;
```

##### 下载数据

检测资源可用性

```cs
try
{
    HttpWebRequest req = HttpWebRequest.Create(new Uri("http://192.168.1.2:8080/HTTP_SERVER/Test.txt")) as HttpWebRequest;
    req.Method = WebRequestMethods.Http.Head;
    req.Timeout = 2000;
    HttpWebResponse res = req.GetResponse() as HttpWebResponse;
    if (res.StatusCode == HttpStatusCode.OK)
    {
        print("文件存在且可用");
        print(res.ContentLength);
        print(res.ContentType);
    }
    else
        print("文件不可用" + res.StatusCode);
    res.Close();
}
catch (WebException w)
{
    print("获取出错" + w.Message + w.Status);
}
```

下载资源

```cs
try
{
    HttpWebRequest req = HttpWebRequest.Create(new Uri("http://192.168.1.2:8080/HTTP_SERVER/Test.txt")) as HttpWebRequest;
    req.Method = WebRequestMethods.Http.Get;
    req.Timeout = 2000;
    HttpWebResponse res = req.GetResponse() as HttpWebResponse;
    if (res.StatusCode == HttpStatusCode.OK)
    {
        print(Application.persistentDataPath);
        using (FileStream fileStream = File.Create(Application.persistentDataPath + "/downloadTest.txt"))
        {
            Stream s = res.GetResponseStream();
            byte[] bytes = new byte[2048];
            int contentLength = s.Read(bytes, 0, bytes.Length);
            while (contentLength > 0)
            {
                fileStream.Write(bytes, 0, contentLength);
                contentLength = s.Read(bytes, 0, bytes.Length);
            }
            fileStream.Close();
            s.Close();
        }
        print("下载成功");
    }
    else
        print("下载失败" + res.StatusCode);
    res.Close();
}
catch (WebException w)
{
print("下载出错" + w.Message + w.Status);
}
```

###### Get请求类型携带额外信息

我们在进行Http通信时，可以在地址后面加一些额外参数传递给服务端  
一般在和短连接游戏服务器通讯时，需要携带额外信息

##### 上传数据

###### Get和Post的区别

1.主要用途：  
Get - 一般从指定的资源请求数据，主要用于获取数据  
Post - 一般向指定的资源提交想要被处理的数据，主要用于上传数据

2.相同点：Get和Post都可以传递一些额外参数数据给服务端

3.不同点：  
        3-1：在传递参数时，Post相对Get更加的安全，因为Post看不到参数  
                Get传递的参数都包含在连接中（URL资源定位位置），是暴露式的  
                Post传递的参数放在请求数据中，不会出现在URL中，是隐藏式的  
        3-2：Get在传递数据时有大小的限制，因为它主要是在连接中拼接参数，而URL的长度是有限制的（最大长度一般为2048个字符）  
                Post在传递数据时没有限制  
        3-3：在浏览器中Get请求能被缓存，Post不能缓存  
        3-4：传输次数可能不同  
                Get：建立连接——请求行、请求头、请求数据一次传输——获取响应——断开  
                Post：建立连接——传输可能分两次——请求行、请求头——请求数据——获取响应——断开

在实际使用中建议使用Get获取，Post上传  
如果想要传递一些不想暴露在外部的参数信息，建议使用Post，它更加的安全

###### Post如何携带额外参数

关键点：讲Content-Type设置为application/x-www-form-urlencoded（键值对类型）

###### ContentType的基本构成

内容类型;charset-编码格式;boundary=边界字符串  
text/html;charset=utf-8;boundary=自定义字符串

具体内容类型请自行查阅

常用的类型

1.通用二进制类型  
application/octet-stream  
2.通用文本类型  
text/plain  
3.键值对参数  
application/x-www-form-urlencoded  
4.复合类型（传递的信息由多种类型组成，上传资源服务器是要使用该类型）  
multipart/form-data

注意：  
Http通讯中，客户端发送给服务端的Get和Post请求都需要服务端和客户端约定一些规则进行处理，比如传递的参数的含义，数据如何处理等，都是需要前后端程序指定对应规则来进行处理的

###### 上传文件到HTTP资源服务器需要遵守的规则

1.ContentType= “multipart/form-data;boundary=边界字符串”;

2.上传的数据必须按照格式写入流中：  
\--边界字符串  
Content-Disposition: form-data; name="字段名字";filename="传到服务器上使用的文件名"  
Content-Type:application/octet-stream  
(传入的内容)  
\--边界字符串--

3.保证服务器允许上传  
4.写入流需要先设置ContentLength内容长度

```cs
try
{
    HttpWebRequest req = HttpWebRequest.Create(new Uri("http://192.168.1.2:8080/HTTP_SERVER/")) as HttpWebRequest;
    req.Method = WebRequestMethods.Http.Post;
    req.ContentType = "multipart/form-data; boundary=werllra";
    req.Timeout = 500000;
    string head = "--werllra\r\n" +
    "Content-Disposition: form-data; name=\"file\";filename=\"http上传的文件.txt\"\r\n" +
    "Content-Type:application/octet-stream\r\n\r\n";
    byte[] headBytes = Encoding.UTF8.GetBytes(head);
    byte[] endBytes = Encoding.UTF8.GetBytes("\r\n--werllra--\r\n");
    using (FileStream localFileStream = File.OpenRead(Application.streamingAssetsPath + "/test.txt"))
    {
        req.ContentLength = headBytes.Length + localFileStream.Length + endBytes.Length;
        Stream upLoadStream = req.GetRequestStream();
        upLoadStream.Write(endBytes, 0, headBytes.Length);
        byte[] buffer = new byte[1024];
        int contentLength = localFileStream.Read(buffer, 0, buffer.Length);
        while (contentLength > 0)
        {
            upLoadStream.Write(buffer, 0, contentLength);
            contentLength= localFileStream.Read(buffer, 0,buffer.Length);
        }
        upLoadStream.Write(endBytes,0, endBytes.Length);
        upLoadStream.Close();
        localFileStream.Close();
    }
    HttpWebResponse res = req.GetResponse() as HttpWebResponse;
    if(res.StatusCode == HttpStatusCode.OK)
    {
        print("上传通信成功");
    }
    else
    {
        print("上传失败"+res.StatusCode);
    }
}
catch (WebException w)
{
    print("获取出错" + w.Message + w.Status);
}
```

#### Unity的相关类

##### WWW类

###### WWW类的作用

WWW是Unity提供给我们简单的访问网页的类，我们可以通过该类下载和上传一些数据  
在使用http协议时，默认耳朵请求类型是Get，如果想要Post上传，需要配合WWWForm使用

它主要支持的协议：  
1.http://和https://超文本传输协议  
2.ftp://文件传输协议（但仅限于匿名下载）  
3.file://本地文件传输协议，可以使用该协议异步加载本地文件（PC、IOS、Android都支持）

注意：  
1.该类一般配合协同程序使用  
2.该类在较新版本中会提示过时，但是仍然可以使用，新版本将其功能整合进了UnityWebRequest类

###### WWW类的常用方法和变量

```cs
WWW www = new WWW("http://192.168.1.2:8080/HTTP_SERVER/");
WWW.LoadFromCacheOrDownload("http://192.168.1.2:8080/HTTP_SERVER/test.assetbundle", 1);
```

###### 利用WWW类来异步下载或加载文件

```cs
StartCoroutine(DownLoadHttp());
StartCoroutine(DownLoadftp());
StartCoroutine(DownLoadLocal());
IEnumerator DownLoadHttp()
{
    WWW www = new WWW("http://192.168.1.2:8080/HTTP_SERVER/Test.txt");
    while(!www.isDone)
    {
        print(www.bytesDownloaded);
        print(www.progress);
        yield return null;
    }
    print(www.bytesDownloaded);
    print(www.progress);
    if (www.error == null)
    {
        print(www.isDone);
    }
}
IEnumerator DownLoadftp()
{
    WWW www = new WWW("ftp://127.0.0.1/Test.txt");
    while (!www.isDone)
    {
        print(www.bytesDownloaded);
        print(www.progress);
        yield return null;
    }
    print(www.bytesDownloaded);
    print(www.progress);
    if (www.error == null)
    {
        print(www.isDone);
    }
}
IEnumerator DownLoadLocal()
{
    WWW www = new WWW("file://" + Application.streamingAssetsPath+"/test.txt");
    while (!www.isDone)
    {
        print(www.bytesDownloaded);
        print(www.progress);
        yield return null;
    }
    print(www.bytesDownloaded);
    print(www.progress);
    if (www.error == null)
    {
        print(www.isDone);
    }
}
```

##### WWWForm类

###### 作用

如果想要使用WWW上传数据，就需要配合WWWForm类进行使用  
而WWWForm主要就是用于集成数据的，我们可以设置上传的参数或者二进制数据  
当结合WWWForm上传数据时，它主要用到的请求类型是Post  
它使用Http协议进行上传处理

注意：使用WWW结合WWWForm上传数据一般需要配合后端程序指定上传规则

###### 常用方法和变量

```cs
WWWForm form = new WWWForm();
```

###### 异步上传数据

```cs
public void Start()
{
    StartCoroutine(UpLoadData());
}
IEnumerator UpLoadData()
{
    WWWForm data = new WWWForm();
    data.AddField("Name", "name", Encoding.UTF8);
    WWW www = new WWW("http://192.168.1.2:8080/HTTP_SERVER", data);
    yield return www;
    if(www.error == null)
    {
        print("上传成功");
    }
    else
    {
        print("上传失败");
    }
}
```

WWW结合WWWFrom上传数据，需要配合后端服务器来制定上传规则  
也就是说我们上传的数据，后端需要知道收到数据后应该如何处理  
通过这种方式我们没办法像C#类当中完成文件的上传，但是这个方式非常适合用于制作短连接游戏的前端网络层

##### UnityWebRequest类

UnityWebRequest是一个Unity提供的一个模块化的系统类，用于构成HTTP请求和处理HTTP响应  
它主要目标是让Unity游戏和Web服务端进行交互  
它将之前WWW的相关功能都集成在了其中，所以新版本都建议使用UnityWebRequest类来代替WWW类

它在使用上和WWW很类似，主要的区别就是UnityWebRequest把下载下来的数据处理单独提取出来了，我们可以根据自己的需求选择对应的数据处理对象来获取数据

注意：  
1.UnityWebRequest和WWW一样，需要配合协同程序使用  
2.UnityWebRequest和WWW一样，支持http、ftp、file协议下载或加载资源  
3.UnityWebRequest能够上传文件到HTTP资源服务器

###### 常用操作

1.使用Get请求获取文本或二进制数据  
2.使用Get请求获取纹理数据  
3.使用Get请求获取AB包数据  
4.使用Post请求发送数据  
5.使用Put请求上传数据

###### 获取数据

```cs
public RawImage image;
private void Start()
{
    StartCoroutine(LoadText());
    StartCoroutine(LoadTexture());
    StartCoroutine(LoadAB());
}
IEnumerator LoadText()
{
    UnityWebRequest req = UnityWebRequest.Get("http://192.168.1.2:8080/HTTP_SERVER/test.txt");
    yield return req.SendWebRequest();
    if (req.result == UnityWebRequest.Result.Success)
    {
        print(req.downloadHandler.text);
        byte[] bytes = req.downloadHandler.data;
    }
    else
    {
        print("获取失败" + req.result + req.error + req.responseCode);
    }
}
IEnumerator LoadTexture()
{
    UnityWebRequest req = UnityWebRequestTexture.GetTexture("http://192.168.1.2:8080/HTTP_SERVER/test.png");
    yield return req.SendWebRequest();
    if(req.result == UnityWebRequest.Result.Success)
    {
        image.texture = DownloadHandlerTexture.GetContent(req);
    }
    else
    {
        print("获取失败" + req.error + req.result + req.responseCode);
    }
}
IEnumerator LoadAB()
{
    UnityWebRequest req = UnityWebRequestAssetBundle.GetAssetBundle("http://192.168.1.2:8080/HTTP_SERVER/test");
    while (!req.isDone)
    {
        print(req.downloadProgress);
        print(req.downloadedBytes);
        yield return null;
    }
    if (req.result == UnityWebRequest.Result.Success)
    {
        AssetBundle ab = DownloadHandlerAssetBundle.GetContent(req);
        print(ab.name);
    }
    else
    {
        print("获取失败" + req.error + req.result + req.responseCode);
    }
}
```

###### 上传数据

父接口：IMultipartFormSection  
数据相关类都继承该接口，我们可以用父类装子类

子类数据：MultipartFormDataSection

```cs
dataList.Add(new MultipartFormDataSection(Encoding.UTF8.GetBytes("123123123")));
dataList.Add(new MultipartFormDataSection("123123123"));
dataList.Add(new MultipartFormDataSection("Name","name111",Encoding.UTF8,"text/..."));
dataList.Add(new MultipartFormDataSection("Name",new byte[1024]));
```

MultipartFormFileSection

```cs
dataList.Add(new MultipartFormFileSection(File.ReadAllBytes(Application.streamingAssetsPath + "/test.txt")));
dataList.Add(new MultipartFormFileSection("上传的文件.txt", File.ReadAllBytes(Application.streamingAssetsPath + "/test.txt")));
dataList.Add(new MultipartFormFileSection("123123123", "text.txt"));
dataList.Add(new MultipartFormFileSection("123123123", Encoding.UTF8, "text.txt"));
dataList.Add(new MultipartFormFileSection("file", new byte[1025], "text.txt", ""));
dataList.Add(new MultipartFormFileSection("file", "123123", Encoding.UTF8, "text.txt"));
```

Post发送相关

```cs
IEnumerator Upload()
{
    List<IMultipartFormSection> data = new List<IMultipartFormSection>();
    data.Add(new MultipartFormDataSection("Name", "name111"));
    data.Add(new MultipartFormFileSection("Test.txt", File.ReadAllBytes(Application.streamingAssetsPath + "/Test.txt")));
    data.Add(new MultipartFormFileSection("123123", "Test.txt"));
    UnityWebRequest req = UnityWebRequest.Post("http://192.168.1.2:8080/HTTP_SERVER/",data);
    req.SendWebRequest();
    while(!req.isDone)
    {
        print(req.uploadProgress);
        print(req.uploadedBytes);
        yield return null;
    }
    print(req.uploadProgress);
    print(req.uploadedBytes);
    if(req.result == UnityWebRequest.Result.Success)
    {
        print("上传成功");
    }
    else
    {
        print("上传失败"+ req.error + req.responseCode + req.result);
    }
}
```

Put上传相关

注意：Put请求类型不是所有的web服务器都能接收，必须要服务器处理该请求类型才能响应

```cs
IEnumerator UploadPut()
{
    UnityWebRequest req = UnityWebRequest.Put("http://192.168.1.2:8080/HTTP_SERVER/",File.ReadAllBytes(Application.streamingAssetsPath + "/Test.txt"));
    yield return req;
    if(req.result == UnityWebRequest.Result.Success)
    {
        print("Put上传成功");
    }
    else
    {
    }
}
```

###### 高级操作

在常用操作中我们使用的是Unity为我们封装好的一些方法  
我们可以方便的进行一些指定类型的数据获取  
比如：下载数据时：1.文本和二进制；2.图片；3.AB包；  
上传数据时：1.可以指定参数和值；2.可以上传文件；  
如果要获取其他类型的数据要如何处理呢？  
同样的，上传其他数据应该如何处理？

高级操作就是用来处理常用操作不能完成的需求的  
它的核心思想就是：UnityWebRequest中可以将数据处理分离开  
比如常规操作中用到的：DownloadHandlerTexture/AssetBundle两个类，就是用来将二进制字节数组转换成对应类型来进行处理的

所以高级操作是指按照规则实现更多的数据获取、上传等功能

UnityWebRequest类的更多内容

```cs
UnityWebRequest req = new UnityWebRequest();
req.url = "服务器地址";
req.method = UnityWebRequest.kHttpVerbPOST;
```

###### 获取数据

 自定义获取数据DownloadHandler相关类

关键类：  
1.DownloadHandlerBuffer 用于简单的数据存储，得到对应的二进制数据  
2.DownloadHandlerFile     用于下载文件并将文件保存到磁盘（内存占用少）  
3.DownloadHandlerTexture 用于下载图像  
4.DownloadHandlerAssetBundle 用于提取AssetBundle  
5.DownloadHandlerAudioClip 用于下载音频文件

以上这些类，就是Unity已经实现好的，用于解析下载下来的数据的类，使用对应的类处理下载数据，他们就会在内部将下载的数据处理为对应的类型，方便使用

DownloadHandlerScript是一个特殊类，就其本身而言，不会执行任何操作  
但是，此类可由用户定义的类继承，此类接收来自UnityWebRequest系统的回调，然后可以使用这些回调在数据从网络到达时执行完全自定义的数据处理

```cs
IEnumerator DownLoadTex()
{
    UnityWebRequest req = new UnityWebRequest("http://192.168.1.2:8080/HTTP_SERVER/test.jpg",
    UnityWebRequest.kHttpVerbGET);
    yield return req.SendWebRequest();
    if(req.result == UnityWebRequest.Result.Success)
    {
    }
}
IEnumerator DownLoadAB()
{
    UnityWebRequest req = new UnityWebRequest("http://192.168.1.2:8080/HTTP_SERVER/test", UnityWebRequest.kHttpVerbGET);
    DownloadHandlerAssetBundle handler = new DownloadHandlerAssetBundle(req.url, 0);
    req.downloadHandler = handler;
    yield return req.SendWebRequest();
    if(req.result == UnityWebRequest.Result.Success)
    {
        AssetBundle ab = handler.assetBundle;
        print(ab.name);
    }
    else
    {
        print("获取数据失败");
    }
}
IEnumerator DownLoadAudioClip()
{
    UnityWebRequest req = UnityWebRequestMultimedia.GetAudioClip("http://192.168.1.2:8080/HTTP_SERVER/test.mp3",AudioType.MPEG);
    yield return req.SendWebRequest();
    if(req.result == UnityWebRequest.Result.Success)
    {
        AudioClip a = DownloadHandlerAudioClip.GetContent(req);
    }
}
```

```cs
public class CustomDownLoadFileHandler:DownloadHandlerScript
{
    private string savePath;
    private byte[] cacheBytes;
    private int index = 0;
    public CustomDownLoadFileHandler() : base()
    {
    }
    public CustomDownLoadFileHandler(byte[] bytes):base(bytes)
    {
    }
    public CustomDownLoadFileHandler(string path): base()
    {
        savePath = path;
    }
    protected override byte[] GetData()
    {
        return cacheBytes;
    }
    protected override bool ReceiveData(byte[] data, int dataLength)
    {
        Debug.Log("收到数据长度:"+data.Length);
        Debug.Log("收到数据长度dataLength"+ dataLength);
        data.CopyTo(cacheBytes, index);
        index += dataLength;
        return true;
    }
    protected override void ReceiveContentLengthHeader(ulong contentLength)
    {
        cacheBytes = new byte[contentLength];
    }
    protected override void CompleteContent()
    {
        Debug.Log("消息收完");
        File.WriteAllBytes(savePath, cacheBytes);
    }
}
```

###### 上传数据

注意：由于UnityWebRequest类的常用操作中，上传数据相关的内容已经封装的很好了，我们可以很方便的上传参数和文件，所以只要了解就可以了。

UploadHandler相关类  
1.UploadHandlerRaw        用于上传字节数组  
2.UploadHandlerFile        用于上传文件

其中比较重要的变量是：  
contentType：内容类型，如果不设置，模式是application/octet-stream 二进制的形式

```cs
IEnumerator UpLoad()
{
    UnityWebRequest req = new UnityWebRequest("http://192.168.1.2:8080/HTTP_SERVER/", UnityWebRequest.kHttpVerbPOST);
    req.uploadHandler = new UploadHandlerFile(Application.streamingAssetsPath + "/test.txt");
    yield return req.SendWebRequest();
    print(req.result);
}
```

###### 一个简单的WWW管理类的封装

```cs
public class NetWWWMgr : MonoBehaviour
{
    private static NetWWWMgr instance;
    public static NetWWWMgr Instance => instance;
    private string HTTP_SERVER_PATH = "http://192.168.1.2:8080/HTTP_SERVER";
    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void LoadRes<T>(string path, UnityAction<T> action) where T : class
    {
        StartCoroutine(LoadResAsync<T>(path, action));
    }
    private IEnumerator LoadResAsync<T>(string path, UnityAction<T> action) where T : class
    {
        WWW www = new WWW(path);
        yield return www;
        if (www.error == null)
        {
            if (typeof(T) == typeof(AssetBundle))
            {
                action?.Invoke(www.assetBundle as T);
            }
            else if (typeof(T) == typeof(Texture))
            {
                action?.Invoke(www.texture as T);
            }
            else if (typeof(T) == typeof(AudioClip))
            {
                action?.Invoke(www.GetAudioClip() as T);
            }
            else if (typeof(T) == typeof(string))
            {
                action?.Invoke(www.text as T);
            }
        }
        else
        {
            Debug.LogError("www加载资源出错" + www.error);
        }
    }
    public void SendMsg<T>(BaseMessage message, UnityAction<T> action) where T : class
    {
        StartCoroutine(SendMsgAsync<T>(message, action));
    }
    private IEnumerator SendMsgAsync<T>(BaseMessage message, UnityAction<T> action) where T : class
    {
        WWWForm data = new WWWForm();
        data.AddBinaryData("Message", message.Writing());
        WWW www = new WWW("HTTP_SERVER_PATH", data);
        yield return www;
        if (www.error == null)
        {
            int index = 0;
            int messageID = BitConverter.ToInt32(www.bytes, index);
            index += 4;
            int msgLength = BitConverter.ToInt32(www.bytes, index);
            index += 4;
            BaseMessage msg = null;
            switch (messageID)
            {
                case 1:
                    msg = new PlayerMessage();
                    msg.Reading(www.bytes, index);
                    break;
            }
            if (msg != null)
            {
                action?.Invoke(msg as T);
            }
        }
        else
            Debug.LogError("发消息出现问题" + www.error);
        }
    public void UploadFile(string fileName, string localPath, UnityAction<UnityWebRequest.Result> action)
    {
        StartCoroutine(UploadFileAsync(fileName, localPath, action));
    }
    private IEnumerator UploadFileAsync(string fileName, string localPath, UnityAction<UnityWebRequest.Result> action)
    {
        List<IMultipartFormSection> list = new List<IMultipartFormSection>();
        list.Add(new MultipartFormFileSection(fileName, File.ReadAllBytes(localPath)));
        UnityWebRequest req = UnityWebRequest.Post(HTTP_SERVER_PATH, list);
        yield return req.SendWebRequest();
        action?.Invoke(req.result);
        if (req.result != UnityWebRequest.Result.Success)
        {
            Debug.LogWarning("上传出现问题" + req.error + req.responseCode);
        }
    }
    public void UnityWebRequestLoad<T>(string path,UnityAction<T> action,string localPath = "",AudioType type = AudioType.MPEG) where T : class
    {
        StartCoroutine(UnityWebRequestLoadAsync<T>(path, action, localPath, type));
    }
    private IEnumerator UnityWebRequestLoadAsync<T>(string path, UnityAction<T> action, string localPath = "",AudioType type = AudioType.MPEG) where T:class
    {
        UnityWebRequest req = new UnityWebRequest(path, UnityWebRequest.kHttpVerbGET);
        if (typeof(T) == typeof(byte[]))
            req.downloadHandler = new DownloadHandlerBuffer();
        else if (typeof(T) == typeof(Texture))
            req.downloadHandler = new DownloadHandlerTexture();
        else if (typeof(T) == typeof(AssetBundle))
            req.downloadHandler = new DownloadHandlerAssetBundle(req.url, 0);
        else if (typeof(T) == typeof(File))
            req.downloadHandler = new DownloadHandlerFile(localPath);
        else if (typeof(T) == typeof(AudioClip))
            req = UnityWebRequestMultimedia.GetAudioClip(path, type);
        else
        {
            Debug.LogWarning("未知类型"+typeof(T));
            yield break;
        }
        yield return req.SendWebRequest();
        if(req.result == UnityWebRequest.Result.Success)
        {
            if (typeof(T) == typeof(byte[]))
                action?.Invoke(req.downloadHandler.data as T);
            else if (typeof(T) == typeof(Texture))
                action?.Invoke(DownloadHandlerTexture.GetContent(req) as T);
            else if (typeof(T) == typeof(AssetBundle))
                action?.Invoke(DownloadHandlerAssetBundle.GetContent(req) as T);
            else if (typeof(T) == typeof(File))
                action?.Invoke(null);
            else if (typeof(T) == typeof(AudioClip))
                action?.Invoke(DownloadHandlerAudioClip.GetContent(req) as T);
        }
        else
        {
            Debug.LogWarning("获取数据失败"+ req.result+req.error + req.responseCode);
        }
    }
}
```

## 消息处理

### 自定义协议工具

#### 什么是协议生成工具

协议生成工具，一般指消息（协议）生成工具，就是专门用于自动化生成消息的程序

当需要一个新消息时，我们需要手动的按照规则去声明新的类，这部分工作费时又费力，技术含量也不高，如果前后端是统一的语言，我们按照语法声明一次就行；但是如果前后端语言不统一，那么前后端分开去声明，也容易造成沟通不一致，声明不统一的问题，所以如果靠我们手动的去声明消息类，是一件费时、费力、还容易出问题的事情

所以我们在开发时，往往就需要用到协议生成工具，来帮助我们自动化的声明消息类  
这样做的好处是：  
1.提升开发效率  
2.降低沟通成本，避免前后端消息不匹配的问题

#### 如何制作协议生成工具

要制作工具，首先需要确定需求，对于协议生成工具来说，主要要求如下：  
1.通过配置文件配置消息或者数据类，名字、变量等  
2.工具根据该配置文件信息动态的生成类文件（脚本文件，代码是自动生成的）  
3.我们可以在开发中直接使用生成文件中声明好的信息和数据结构类进行开发

根据需求分析，我们需要做：  
1.确定协议配置方式：  
        可以使用json、xml、自定义格式进行协议配置，主要目的是通过配置文件确定：  
        1.消息或数据结构类名字  
        2.字段名等  
2.确定生成格式  
        最终我们是要自动生成类声明文件  
        所以具体类应该如何生成需要确定格式  
        比如：继承关系，序列化、反序列化，提取出共同特点  
3.制作生成工具  
        基于配置文件和生成格式，动态的生成对应类文件

#### 协议（消息）配置

我们可以根据自己的喜好去选择一种格式去配置协议，之后根据读取的这些配置信息，再通过代码按照规则自动生成对应的类文件

这里主要使用xml文件进行举例，其他格式都是比较类似的

```undefined
<?xml version="1.0" encoding="UTF-8"?>
<messages>
<enum name="E_PLAYER_TYPE" namespace="GamePlayer">
<field name="MAIN">2</field>
<field name="OTHER"/>
</enum>
<data name="PlayerData" namespace="GamePlayer">
<field type="int" name="id"/>
<field type="float" name="atk" />
<field type="bool" name="sex"/>
<field type="long" name="lev"/>
<field type="array" data="int" name="arrays"/>
<field type="list" T="int" name="list"/>
<field type="dic" Tkey="int" Tvalue="string" name="dic"/>
</data>
<message id="1001" name="PlayerMessage" namespace="GamePlayer">
<field type="int" name="playerID"/>
<field type="PlayerData" name="data"/>
</message>
</messages>
```

```cs
XmlDocument xml = new XmlDocument();
xml.Load(Application.dataPath + "/Scripts/PlayerData.xml");
XmlNode root = xml.SelectSingleNode("messages");
XmlNodeList enumList = root.SelectNodes("enum");
foreach(XmlNode enumNode in enumList)
{
print("枚举名字:" + enumNode.Attributes["name"].Value);
print("枚举所在命名空间:" + enumNode.Attributes["namespace"].Value);
XmlNodeList fields = enumNode.SelectNodes("field");
}
```

#### 协议（消息）生成

协议生成主要是使用配置文件中读取出来的信息，动态的生成对应语言的代码文件  
每次添加消息或者数据结构类时，就不需要再手写代码了，这里用C#语言做演示

协议生成是不会在发布后使用的功能，主要是在开发时使用，所以我们在Unity当中可以把它作为一个编辑器功能来做  
因此，我们可以专门新建一个Editor文件夹（专门放编辑器内容，不会发布），在其中放置配置文件，自动生成脚本相关文件

##### 枚举生成

```cs
public class GenerateCSharp
{
    private string SAVE_PATH = Application.dataPath + "/Scripts/Protocol/";
    public void GenerateEnum(XmlNodeList nodes)
    {
        string namespaceStr = "";
        string enumNameStr = "";
        string fieldStr = "";
        foreach(XmlNode node in nodes)
        {
            namespaceStr = node.Attributes["namespace"].Value;
            enumNameStr = node.Attributes["name"].Value;
            XmlNodeList enumFields = node.SelectNodes("field");
            fieldStr = "";
            foreach(XmlNode field in enumFields)
            {
                fieldStr += "\t\t" + field.Attributes["name"].Value;
                if (field.InnerText != "")
                fieldStr += " = " + field.InnerText;
                fieldStr += ",\r\n";
            }
            string enumStr = $"namespace {namespaceStr}\r\n" +
            "{\r\n" +
            $"\tpublic enum {enumNameStr}\r\n" +
            "\t{\r\n" +
            $"{fieldStr}" +
            "\t}\r\n" +
            "}";
            string path = SAVE_PATH + namespaceStr + "/Enum/";
            if(!Directory.Exists(path))
            Directory.CreateDirectory(path);
            File.WriteAllText(path + enumNameStr + ".cs", enumStr);
        }
        Debug.Log("枚举生成结束");
    }
}
```

##### 数据结构类

```cs
public void GenerateData(XmlNodeList nodes)
{
    string namespaceStr = "";
    string classNameStr = "";
    string fieldStr = "";
    string getBytesNumStr = "";
    string writingStr = "";
    string readingStr = "";
    Debug.Log("数据类生成开始");
    foreach (XmlNode dataNode in nodes)
    {
        Debug.Log(dataNode.Name);
        namespaceStr = dataNode.Attributes["namespace"].Value;
        classNameStr = dataNode.Attributes["name"].Value;
        XmlNodeList fields = dataNode.SelectNodes("field");
        fieldStr = GetFieldStr(fields);
        getBytesNumStr = GetGetBytesStr(fields);
        writingStr = GetWritingStr(fields);
        readingStr = GetReadingStr(fields);
        string dataStr = "using System.Collections.Generic;\r\n" +
                        "using System.Text;\r\n" +
                        "using System;\r\n" +
                        $"namespace {namespaceStr}\r\n" +
                        "{\r\n" +
                            $"\tpublic class {classNameStr} : BaseData\r\n" +
                            "\t{\r\n" +
                            $"{fieldStr}" +
                                "\t\tpublic override int GetBytesNum()\r\n" +
                                "\t\t{\r\n" +
                                    "\t\t\tint num = 0;\r\n" +
                                    $"{getBytesNumStr}" +
                                    "\t\t\treturn num;\r\n" +
                                "\t\t}\r\n" +
                                "\t\tpublic override byte[] Writing()\r\n" +
                                "\t\t{\r\n" +
                                    "\t\t\tint index = 0;\r\n" +
                                    "\t\t\tbyte[] bytes = new byte[GetBytesNum()];\r\n" +
                                    $"{writingStr}" +
                                    "\t\t\treturn bytes;\r\n" +
                                "\t\t}\r\n" +
                                "\t\tpublic override int Reading(byte[] bytes, int beginIndex = 0)\r\n" +
                                "\t\t{\r\n" +
                                    "\t\t\tint index = beginIndex;\r\n" +
                                    $"{readingStr}" +
                                    "\t\t\treturn index - beginIndex;\r\n" +
                                "\t\t}\r\n" +
                            "\t}\r\n" +
                        "}";
        string path = SAVE_PATH + namespaceStr + "/Data/";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        File.WriteAllText(path + classNameStr + ".cs", dataStr);
    }
    Debug.Log("数据类生成结束");
}
private string GetFieldStr(XmlNodeList fields)
{
    string fieldStr = "";
    foreach(XmlNode field in fields)
    {
        string type = field.Attributes["type"].Value;
        string fieldName = field.Attributes["name"].Value;
        if(type == "list")
        {
            string T = field.Attributes["T"].Value;
            fieldStr += "\t\tpublic " + "List<" + T + "> ";
        }
        else if(type == "array")
        {
            string data = field.Attributes["data"].Value;
            fieldStr += "\t\tpublic " + data +"[] ";
        }
        else if(type == "dic")
        {
            string Tkey = field.Attributes["Tkey"].Value;
            string Tvalue = field.Attributes["Tvalue"].Value;
            fieldStr += "\t\tpublic Dictionary<" + Tkey + ", " + Tvalue + "> ";
        }
        else if(type == "enum")
        {
            string data = field.Attributes["data"].Value;
            fieldStr += "\t\tpublic " + data + " ";
        }
        else
        {
            fieldStr += "\t\tpublic " + type + " ";
        }
        fieldStr += fieldName + ";\r\n";
    }
    return fieldStr;
}
private string GetGetBytesStr(XmlNodeList fields)
{
    string bytesNumStr = "";
    string type = "";
    string name = "";
    foreach(XmlNode field in fields)
    {
        type = field.Attributes["type"].Value;
        name = field.Attributes["name"].Value;
        if (type == "list")
        {
            string T = field.Attributes["T"].Value;
            bytesNumStr += "\t\t\tnum += 2;\r\n";
            bytesNumStr += "\t\t\tfor(int i = 0; i < " + name + ".Count; i++)\r\n";
            bytesNumStr += "\t\t\t\tnum += " + GetValueBytesNum(T, name+"[i]") + ";\r\n";
        }
        else if (type == "array")
        {
            string data = field.Attributes["data"].Value;
            bytesNumStr += "\t\t\tnum += 2;\r\n";
            bytesNumStr += "\t\t\tfor(int i = 0; i < " + name + ".Length; i++)\r\n";
            bytesNumStr += "\t\t\t\tnum += " + GetValueBytesNum(data, name + "[i]") + ";\r\n";
        }
        else if (type == "dic")
        {
            string Tkey = field.Attributes["Tkey"].Value;
            string TValue = field.Attributes["Tvalue"].Value;
            bytesNumStr += "\t\t\tnum += 2;\r\n";
            bytesNumStr += "\t\t\tforeach (" + Tkey + " key in " + name + ".Keys)\r\n";
            bytesNumStr += "\t\t\t{\r\n";
            bytesNumStr += "\t\t\t\tnum += " + GetValueBytesNum(Tkey, "key") + ";\r\n";
            bytesNumStr += "\t\t\t\tnum += " + GetValueBytesNum(TValue, name + "[key]") + ";\r\n";
            bytesNumStr += "\t\t\t}\r\n";
        }
        else
            bytesNumStr += "\t\t\tnum += " + GetValueBytesNum(type,name) + ";\r\n";
    }
    return bytesNumStr;
}
private string GetValueBytesNum(string type,string name)
{
    switch (type)
    {
        case "int":
        case "float":
        case "enum":
            return "4";
        case "long":
            return "8";
        case "byte":
        case "bool":
            return "1";
        case "short":
            return "2";
        case "string":
            return "4 + Encoding.UTF8.GetByteCount(" + name + ")";
        default:
            return name + ".GetBytesNum()";
    }
}
private string GetWritingStr(XmlNodeList fields)
{
    string writingStr = "";
    string type = "";
    string name = "";
    foreach (XmlNode field in fields)
    {
        type = field.Attributes["type"].Value;
        name = field.Attributes["name"].Value;
        if(type == "list")
        {
            string T = field.Attributes["T"].Value;
            writingStr += "\t\t\tWriteShort(bytes,(short)" + name + ".Count, ref index);\r\n";
            writingStr += "\t\t\tfor(int i = 0; i < " + name + ".Count; i++)\r\n";
            writingStr += "\t\t\t\t" + GetFieldWritingStr(T, name + "[i]") + "\r\n";
        }
        else if(type == "array")
        {
            string data = field.Attributes["data"].Value;
            writingStr += "\t\t\tWriteShort(bytes,(short)" + name + ".Length, ref index);\r\n";
            writingStr += "\t\t\tfor(int i = 0; i < " + name + ".Length; i++)\r\n";
            writingStr += "\t\t\t\t" + GetFieldWritingStr(data, name + "[i]") + "\r\n";
        }
        else if (type == "dic")
        {
            string Tkey = field.Attributes["Tkey"].Value;
            string Tvalue = field.Attributes["Tvalue"].Value;
            writingStr += "\t\t\tWriteShort(bytes,(short)" + name + ".Count, ref index);\r\n";
            writingStr += "\t\t\tforeach (" + Tkey + " key in " + name + ".Keys)\r\n";
            writingStr += "\t\t\t{\r\n";
            writingStr += "\t\t\t\t" + GetFieldWritingStr(Tkey, "key") + "\r\n";
            writingStr += "\t\t\t\t" + GetFieldWritingStr(Tvalue, name + "[key]") + "\r\n";
            writingStr += "\t\t\t}\r\n";
        }
        else
        {
            writingStr += "\t\t\t" + GetFieldWritingStr(type, name) + "\r\n";
        }
    }
    return writingStr;
}
private string GetFieldWritingStr(string type,string name)
{
    switch (type)
    {
        case "byte":
            return "WriteByte(bytes, " + name + ", ref index);";
        case "int":
            return "WriteInt(bytes, " + name + ", ref index);";
        case "short":
            return "WriteShort(bytes, " + name + ", ref index);";
        case "long":
            return "WriteLong(bytes, " + name + ", ref index);";
        case "float":
            return "WriteFloat(bytes, " + name + ", ref index);";
        case "bool":
            return "WriteBool(bytes, " + name + ", ref index);";
        case "string":
            return "WriteString(bytes, " + name + ", ref index);";
        case "enum":
            return "WriteInt(bytes, Convert.ToInt32(" + name + "), ref index);";
        default:
            return "WriteData(bytes, " + name + ", ref index);";
    }
}
private string GetReadingStr(XmlNodeList fields)
{
    string readingStr = "";
    string type = "";
    string name = "";
    foreach (XmlNode field in fields)
    {
        type = field.Attributes["type"].Value;
        name = field.Attributes["name"].Value;
        if (type == "list")
        {
            string T = field.Attributes["T"].Value;
            readingStr += "\t\t\t" + name + " = new List<" + T + ">();\r\n";
            readingStr += "\t\t\tshort " + name + "Count = ReadShort(bytes, ref index);\r\n";
            readingStr += "\t\t\tfor (int i = 0; i < " + name + "Count; i++)\r\n";
            readingStr += "\t\t\t\t" + name + ".Add(" + GetFieldReadingStr(T) + ");\r\n";
        }
        else if (type == "array")
        {
            string data = field.Attributes["data"].Value;
            readingStr += "\t\t\tshort " + name + "Length = ReadShort(bytes, ref index);\r\n";
            readingStr += "\t\t\t" + name + " = new "+data + "["+name +"Length];\r\n";
            readingStr += "\t\t\tfor (int i = 0; i < " + name + "Length; i++)\r\n";
            readingStr += "\t\t\t\t" + name + "[i] = " + GetFieldReadingStr(data) + ";\r\n";
        }
        else if (type == "dic")
        {
            string Tkey = field.Attributes["Tkey"].Value;
            string Tvalue = field.Attributes["Tvalue"].Value;
            readingStr += "\t\t\t" + name + " = new Dictionary<" + Tkey + ", " + Tvalue + ">();\r\n";
            readingStr += "\t\t\tshort " + name + "Count = ReadShort(bytes, ref index);\r\n";
            readingStr += "\t\t\tfor (int i = 0; i < " + name + "Count; i++)\r\n";
            readingStr += "\t\t\t\t" + name + ".Add(" + GetFieldReadingStr(Tkey) + ", "
            + GetFieldReadingStr(Tvalue) + ");\r\n";
        }
        else if (type == "enum")
        {
            string data = field.Attributes["data"].Value;
            readingStr += "\t\t\t" + name + " = (" + data + ")ReadInt(bytes, ref index);\r\n";
        }
        else
            readingStr += "\t\t\t" + name + " = " + GetFieldReadingStr(type) + ";\r\n";
    }
    return readingStr;
}
private string GetFieldReadingStr(string type)
{
    switch (type)
    {
        case "byte":
            return "ReadByte(bytes, ref index)";
        case "int":
            return "ReadInt(bytes, ref index)";
        case "short":
            return "ReadShort(bytes, ref index)";
        case "long":
            return "ReadLong(bytes, ref index)";
        case "float":
            return "ReadFloat(bytes, ref index)";
        case "bool":
            return "ReadBool(bytes, ref index)";
        case "string":
            return "ReadString(bytes, ref index)";
        default:
            return "ReadData<"+ type+">(bytes, ref index)";
    }
}
```

##### 消息类

```cs
public void GenerateMsg(XmlNodeList nodes)
{
    string idStr = "";
    string namespaceStr = "";
    string classNameStr = "";
    string fieldStr = "";
    string getBytesNumStr = "";
    string writingStr = "";
    string readingStr = "";
    foreach (XmlNode dataNode in nodes)
    {
        idStr = dataNode.Attributes["id"].Value;
        namespaceStr = dataNode.Attributes["namespace"].Value;
        classNameStr = dataNode.Attributes["name"].Value;
        XmlNodeList fields = dataNode.SelectNodes("field");
        fieldStr = GetFieldStr(fields);
        getBytesNumStr = GetGetBytesStr(fields);
        writingStr = GetWritingStr(fields);
        readingStr = GetReadingStr(fields);
        string dataStr = "using System.Collections.Generic;\r\n" +
                        "using System.Text;\r\n" +
                        "using System;\r\n" +
                        $"namespace {namespaceStr}\r\n" +
                        "{\r\n" +
                            $"\tpublic class {classNameStr} : BaseMessage\r\n" +
                            "\t{\r\n" +
                            $"{fieldStr}" +
                                "\t\tpublic override int GetBytesNum()\r\n" +
                                "\t\t{\r\n" +
                                    "\t\t\tint num = 8;\r\n" +
                                    $"{getBytesNumStr}" +
                                    "\t\t\treturn num;\r\n" +
                                "\t\t}\r\n" +
                                "\t\tpublic override byte[] Writing()\r\n" +
                                "\t\t{\r\n" +
                                    "\t\t\tint index = 0;\r\n" +
                                    "\t\t\tbyte[] bytes = new byte[GetBytesNum()];\r\n" +
                                    "\t\t\tWriteInt(bytes, GetID(), ref index);\r\n"+
                                    "\t\t\tWriteInt(bytes, bytes.Length - 8, ref index);\r\n"+
                                    $"{writingStr}" +
                                    "\t\t\treturn bytes;\r\n" +
                                "\t\t}\r\n" +
                                "\t\tpublic override int Reading(byte[] bytes, int beginIndex = 0)\r\n" +
                                "\t\t{\r\n" +
                                    "\t\t\tint index = beginIndex;\r\n" +
                                    $"{readingStr}" +
                                    "\t\t\treturn index - beginIndex;\r\n" +
                                "\t\t}\r\n" +
                                "\t\tpublic override int GetID()\r\n" +
                                "\t\t{\r\n" +
                                    "\t\t\treturn "+idStr+";\r\n" +
                                "\t\t}\r\n" +
                            "\t}\r\n" +
                        "}";
        string path = SAVE_PATH + namespaceStr + "/Msg/";
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        File.WriteAllText(path + classNameStr + ".cs", dataStr);
    }
    Debug.Log("消息类生成结束");
}
```

### 第三方协议工具Protobuf

Protobuf全称是protocol-buffers（协议缓冲区），是谷歌提供给开发者的一个开源的协议生成工具  
它的主要工作原理和上面的自定义协议工具类似，只不过它更加的完善，可以基于协议配置文件生成各种语言的代码文件

它是游戏开发中经常会选择的协议生成工具，因为它的通用性强，稳定性高，可以节约出开发自定义协议工具的时间

官网：[C# 生成代码指南 | Protocol Buffers 文档 - ProtoBuf 文档](https://protobuf.com.cn/reference/csharp/csharp-generated/#invocation "C# 生成代码指南 | Protocol Buffers 文档 - ProtoBuf 文档")

#### 使用流程

1.下载对应语言要使用Protobuf相关内容  
2.根据配置规则编辑协议配置文件  
3.用Protobuf编译器，利用协议配置文件生成对应语言的代码文件  
4.将代码文件导入工程中进行使用

#### 配置后缀

Protobuf中配置文件的后缀统一使用.proto  
可以通过多个后缀为.proto的配置文件进行配置

#### 配置规则

**注释方式**：  
//方式1  
/\*方式2\*/

**第一行版本号**：  
syntax = "proto3";  
如果不写，默认使用2版本

**命名空间**  
package 命名空间名;

**消息类**：  
message 类名{  
字段声明  
}

**成员类型和唯一编号**：  
浮点数：float、double  
整数：  
        变长编码：int32，int64，uint32，uint64  
        根据数字的大小来使用对应的字节数来存储  
        适用于负数的类型：sint32，sint64  
        固定字节数：fixed32，fixed64，sfixed32，sfixed64  
其他类型：bool，string，bytes

唯一编号：配置成员时，需要默认给他们一个编号，从1开始  
这些编号用于标识中的字段信息二进制格式

**特殊标识**：  
1.required：必须赋值的字段  
2.optional：可以不赋值的字段  
3.repeated：数组  
4.map：字典

**枚举**：  
enum 枚举名{  
        常量1 = 0;第一个常量必须映射到0  
        常量2 = 3;  
}

**默认值：**  
string：空字符串  
bytes：空字节  
bool：false  
数值：0  
枚举：0  
message：取决于语言，C#为空

**允许嵌套**:  
嵌套一个类在另一个类中，相当于内部类

**保留字段**：  
如果修改了协议规则，删除了部分内容，为了避免更新时重新使用已经删除了的编号  
我们可以利用 reserved关键字来保留字段，这些内容就不能再被使用了

**导入定义:**  
import "配置文件路径";  
如果在某一个配置中使用了另一个配置的类型，则需要导入另一个配置文件名

#### 生成文件

1.打开cmd窗口  
2.进入protoc.exe所在文件夹（也可以直接将exe文件拖入cmd窗口中）  
3.输入转换指令：protoc.exe -I=配置路径 --csharp\_out=输出路径 配置文件名

注意：路径不要有中文和特殊符号，避免生成失败

##### 自定义快捷生成所有的消息协议

```cs
using System.Diagnostics;
using System.IO;
using UnityEditor;
public class ProtobufTool
{
    private static string PROTO_PATH = "D:\\learn\\games\\GUI\\Protobuf\\proto";
    private static string PROTOC_PATH = "D:\\learn\\games\\GUI\\Protobuf\\protoc.exe";
    private static string CSHARP_PATH = "D:\\learn\\games\\GUI\\Protobuf\\csharp";
    private static string CPP_PATH = "D:\\learn\\games\\GUI\\Protobuf\\cpp";
    [MenuItem("ProtobufTool/生成C#代码")]
    private static void GenerateCSharp()
    {
        Generate("csharp_out",CSHARP_PATH);
    }
    [MenuItem("ProtobufTool/生成C++代码")]
    private static void GenerateCPP()
    {
        Generate("csharp_out", CPP_PATH);
    }
    private static void Generate(string outCmd,string outPath)
    {
        DirectoryInfo directoryInfo = Directory.CreateDirectory(PROTO_PATH);
        FileInfo[] files = directoryInfo.GetFiles();
        for (int i = 0; i < files.Length; i++)
        {
            if (files[i].Extension == ".proto")
            {
                Process cmd = new Process();
                cmd.StartInfo.FileName = PROTOC_PATH;
                cmd.StartInfo.Arguments = $"-I={PROTO_PATH} --{outCmd}={outPath} {files[i].Name}";
                cmd.Start();
                UnityEngine.Debug.Log(files[i] + "所有内容生成结束");
            }
        }
    }
}
```

#### 协议使用

序列化存储为本地文件  
1.生成的类中的WriteTo方法  
2.文件流FileStream对象

反序列化本地文件  
1.生成类中的 Parser.ParseFrom方法  
2.文件流FileStream对象

得到序列化后的字节数组  
1.生成的类中的WriteTo方法  
2.内存流MemoryStream对象

从字节数组反序列化  
1.生成类中的 Parser.ParseFrom方法  
2.内存流MemoryStream对象

##### 如何封装一个静态的工具类

```cs
using Google.Protobuf;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
public static class NetTool
{
    public static byte[] GetProtoBytes(IMessage msg)
    {
        return msg.ToByteArray();
    }
    public static T GetProtoMsg<T>(byte[] bytes) where T:class,IMessage
    {
        Type type = typeof(T);
        PropertyInfo pInfo = type.GetProperty("Parser");
        object parserObj = pInfo.GetValue(null, null);
        Type parserType = parserObj.GetType();
        MethodInfo mInfo = parserType.GetMethod("ParseFrom",new Type[] { typeof(byte[]) });
        object msg = mInfo.Invoke(parserObj, new object[] { bytes });
        return msg as T;
    }
}
```

#### Protobuf-Net

早期的Protobuf并不支持C#

[Protobuf-Net{% asset_img 1.png %}https://github.com/protobuf-net/protobuf-net](https://github.com/protobuf-net/protobuf-net "Protobuf-Net")让我们可以基于Protobuf的规则进行C#的代码生成，对象的序列化和反序列化

注意：  
1.Protobuf不支持Net3.5及以下版本，所以如果想要在Unity的老版本中使用Protobuf，我们只能使用Protobuf-Net  
2.如何判断是否支持？  
只要把Protobuf相关dll包导入后能够正常使用不报错，则证明支持

### 其他

#### 大小端模式

##### 什么是大小端模式

大端模式：  
是指数据的高字节保存在内存的低地址中，而数据的低字节保存在内存的高地址中  
这样的存储模式有点类似于把数据当作字符串顺序处理  
地址由小向大增加，数据从高往低位放，符合人的阅读习惯

小端模式：  
是指数据的高字节保存在内存的高地址中，而数据的低字节保存在内存的低地址中

即：大端是从左往右，小端是从右往左

##### 为什么会有大小端模式

大小端模式其实是计算机硬件的两种存储数据的方式，我们也可以称大小端模式为大小端字节序

对于我们来说，大端字节序阅读起来更加方便，那么为什么还会存在小端字节序呢？  
因为计算机电路先处理低位字节，效率比较高  
计算机处理字节序的时候，不知道什么是高位字节，什么是低位字节，它只知道按照顺序读取字节，先读1，再读2，如果是大端字节序，那么先读到的就是高位字节，后读到的才是低位字节

因为计算机都是从低位开始的，所以计算机的内部处理都是小端字节序

所以一般情况下，除了计算机的内部处理，其他地方几乎都是大端处理序。

##### 大小端对我们的影响

只有读取的时候，需要区分大小端字节序，其他情况都不需要考虑

因此在网络传输当中，我们传输的是字节数组，那么我们在收到字节数组进行解析时，就需要考虑大小端的问题  
虽然TCP/IP协议规定了在网络上必须采用网络字节顺序（大端），但是具体传输时采用哪种模式，都是根据前后端语言、设备决定的

在进行网络通讯，前后端语言不同时，可能会造成大小端不统一  
一般情况下：  
C#和Java/Erlang/AS3通讯需要进行大小端转换，因为C#是小端模式，他们是大端  
C#和C++不需要特殊处理，因为他们都是小端

##### 大小端如何转换

```cs
using System;
using System.Net;
using UnityEngine;
public class Learn1 : MonoBehaviour
{
    private void Start()
    {
        print("是否是小端模式" + BitConverter.IsLittleEndian);
        int i = 99;
        byte[] bytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(i));
        int receI = BitConverter.ToInt32(bytes, 0);
        receI = IPAddress.HostToNetworkOrder(receI);
        if (BitConverter.IsLittleEndian)
            Array.Reverse(bytes);
    }
}
```

##### 总结

大小端模式会根据主机硬件环境不同、语言不同而有所区别，当我们前后端是不同语言开发且运行在不同主机上时，前后端需要对大小端字节序定下统一的规则

一般让前端迎合后端，因为字节序的转换也是会带来些许性能损耗的  
网络游戏中要尽量减轻后端的负担

一般情况下，我们不用死记硬背和谁通讯要注意大小端模式  
当开发时，发现后端收到的消息和前端发的不一样，在协议统一的情况下，往往就是因为大小端造成的

注意：Protobuf已经解决了大小端问题，在使用时不用过多考虑字节序转换的问题