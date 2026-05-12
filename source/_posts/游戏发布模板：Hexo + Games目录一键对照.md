---
title: "游戏发布模板：Hexo 博客中快速接入 H5/Unity 游戏"
date: 2026-05-12
categories: 学习笔记
tags:
  - 游戏
  - Hexo
  - Web
---

这篇是一个固定模板，目标是让你在博客里快速发布一个小游戏（Canvas、HTML5、小型 Web 应用，或 Unity WebGL）。

以后每次上新只需要三步：

1. 准备游戏静态文件到 `source/games/<游戏id>/`
2. 在 `source/games/games.json` 增加一个条目
3. 在 `source/games/index.md` 添加展示（或者让列表自动读取）

---

## 一、先创建游戏目录

在 `source/games/` 下创建一个目录，目录名作为 `id`。

```bash
source/games/
  └─ my-game/
      ├─ index.html
      ├─ Build/
      ├─ TemplateData/
      └─ assets/
```

> 关键：  
> `id` 和目录名要一致。  
> 如果是 Unity WebGL，请保留官方导出结果里的 `index.html`、`Build`、`TemplateData`。

---

## 二、填写 `games.json`（重点）

文件路径：`source/games/games.json`  
新增一条，参考格式如下：

```json
{
  "id": "my-game",
  "title": "我的游戏标题",
  "shortDesc": "列表页展示文案",
  "description": "详情页说明：玩法、玩法亮点、注意事项",
  "path": "/games/my-game/",
  "tags": ["Unity", "WebGL", "休闲"],
  "controls": "鼠标 / 键盘 / 触控",
  "difficulty": "中等",
  "updatedAt": "2026-05-12"
}
```

注意：

- `id` 要和文件夹名一致
- `path` 为 `/games/<id>/`（末尾建议带 `/`）
- 详情页会自动读取这个文件：
  - `/games/detail/?id=my-game`

---

## 三、可选：放到 `游戏空间` 列表

- 你可以把它写死在 `source/games/index.md` 的卡片区
- 或者直接依赖 `fetch('/games/games.json')` 自动渲染（已支持）

---

## 四、在文章里引用（你要的“和博客融合”）

方式 A：文章里放详情页（推荐）

```md
[去玩游戏（详情）](/games/detail/?id=my-game)
```

方式 B：文章里直接跳到入口

```md
[直接打开游戏](/games/my-game/)
```

方式 C：文章页内嵌（注意移动端显示和样式冲突）

```html
<iframe src="/games/my-game/" width="100%" height="680" style="border:1px solid #2d3565;border-radius:8px;"></iframe>
```

---

## 五、发布检查清单（复制后打勾）

- [ ] `source/games/my-game/index.html` 能独立打开
- [ ] `games.json` 有完整字段且路径正确
- [ ] `id == 目录名`
- [ ] `details` 页面能打开：`/games/detail/?id=my-game`
- [ ] 推送 `main`，GitHub Actions 成功发布

---

后续加新游戏直接复用这篇文章框架改标题和 id 即可。

参考文件：

- 游戏说明手册：`source/games/GAME_ADD_GUIDE.txt`
- README：`README.md`
- 游戏列表：`source/games/games.json`
- 统一详情页：`/games/detail/?id=<id>`
