Tips1:如果看不到图片请参考[这篇文章](https://www.jianshu.com/p/1db303f6ee18 "")    
Tips2:展示视频正在剪辑中 剪辑好以后会放出地址  
* [1\. 声明](#1-声明)
* [2\. 简介](#2-简介)
* [3\. 项目架构](#3-项目架构)
* [4\. 功能模块](#4-功能模块)
* [5\. 项目特色](#5-项目特色)
  * [5\.1\. 带动画UI框架](#51-带动画ui框架)
  * [5\.2\. 装备词条系统](#52-装备词条系统)
  * [5\.3\. 商品限购](#53-商品限购)
* [6\. TODO](#6-todo)
* [7\. 项目展示](#7-项目展示)
    * [7\.1\.1\. 登录注册](#711-登录注册)
    * [7\.1\.2\. 选择角色](#712-选择角色)
    * [7\.2\.1\. 主场景](#721-主场景)
    * [7\.2\.2\. 菜单](#722-菜单)
    * [7\.3\.1\. NPC交互](#731-npc交互)
    * [7\.3\.2\. NPC对话](#732-npc对话)
    * [7\.4\.1\. 道具](#741-道具)
    * [7\.4\.2\. 装备词条](#742-装备词条)
    * [7\.4\.3\. 物品卖出](#743-物品卖出)
    * [7\.5\.1\. 商店](#751-商店)
    * [7\.5\.2\. 商店购买](#752-商店购买)
    * [7\.5\.3\. 获得物品](#753-获得物品)
    * [7\.6\.1\. 玩家搜索](#761-玩家搜索)
    * [7\.6\.2\. 好友请求](#762-好友请求)
    * [7\.6\.3\. 好友添加](#763-好友添加)
    * [7\.6\.4\. 好友面版](#764-好友面版)
    * [7\.7\.1\. 任务面版](#771-任务面版)
    * [7\.7\.2\. 任务接受](#772-任务接受)
    * [7\.7\.3\. Npc任务状态](#773-npc任务状态)
    * [7\.8\.1\. 邮件系统](#781-邮件系统)
    * [7\.9\.1\. 组队界面](#791-组队界面)
    * [7\.9\.2\. 组队邀请](#792-组队邀请)


# 1. 声明 #
此项目内所有取自原神的素材版权归米哈游所有。该项目仅用于个人学习研究。  

# 2. 简介 #
独立完成的个人mmorpg学习项目,包含客户端服务端。  
UI风格参考原神,使用Lua开发,支持热更新。  
客户端： Unity + XLua    
协议： protobuf     
服务端: C# + sqlserver


# 3. 项目架构 #  

![](./Images/客户端架构.png)  

![](./Images/服务端架构.png)  
# 4. 功能模块 #  
1.用户模块：用户注册、用户登录、角色创建、角色选择  
2.基础模块：角色控制、移动同步、地图传送、小地图  
3.基础系统：背包系统、装备系统、商店系统、任务系统、Npc系统、对话系统   
4.社交系统：好友系统、邮件系统、组队系统

# 5. 项目特色 #

## 5.1. 带动画UI框架 ##
![](./Images/UI框架.png)
UI框架在此项目[liuhaopen/UnityMMO](https://github.com/liuhaopen/UnityMMO.git "")基础上改进
## 5.2. 装备词条系统 ##
![](./Images/装备词条.png)  
装备词条 [示例](#742-装备词条)
## 5.3. 商品限购 ##
![](./Images/商品限购.png)  
商品限购 [示例](#752-商店购买)

# 6. TODO #
自动寻路  
红点系统  
聊天系统  

# 7. 项目展示 #
### 7.1.1. 登录注册 ### 
![](./Images/1.1登录注册.png)
### 7.1.2. 选择角色 ### 
![](./Images/1.2选择角色.png)
### 7.2.1. 主场景 ### 
![](./Images/2.1主场景.png)
### 7.2.2. 菜单 ### 
![](./Images/2.2菜单.png)
### 7.3.1. NPC交互 ### 
![](./Images/3.1NPC交互.png)
### 7.3.2. NPC对话 ### 
![](./Images/3.2NPC对话.png)
### 7.4.1. 道具 ### 
![](./Images/4.1道具.png)
### 7.4.2. 装备词条 ### 
![](./Images/4.2装备词条.png)
### 7.4.3. 物品卖出 ### 
![](./Images/4.3物品卖出.png)
### 7.5.1. 商店 ### 
![](./Images/5.1商店.png)
### 7.5.2. 商店购买 ### 
![](./Images/5.2商店购买.png)
### 7.5.3. 获得物品 ### 
![](./Images/5.3获得物品.png)
### 7.6.1. 玩家搜索 ### 
![](./Images/6.1玩家搜索.png)
### 7.6.2. 好友请求 ### 
![](./Images/6.2好友请求.png)
### 7.6.3. 好友添加 ### 
![](./Images/6.3好友添加.png)
### 7.6.4. 好友面版 ### 
![](./Images/6.4好友面版.png)
### 7.7.1. 任务面版 ### 
![](./Images/7.1任务面版.png)
### 7.7.2. 任务接受 ### 
![](./Images/7.2任务接受.png)
### 7.7.3. Npc任务状态 ### 
![](./Images/7.3Npc任务状态.png)
### 7.8.1. 邮件系统 ### 
![](./Images/8.1邮件系统.png)
### 7.9.1. 组队界面 ### 
![](./Images/9.1组队界面.png)
### 7.9.2. 组队邀请 ### 
![](./Images/9.2组队邀请.png)






