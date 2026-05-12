Hatanako Blog（Hexo）
======================

这是一个基于 Hexo + Butterfly 主题的个人技术博客仓库，同时也承载小游戏/前端交互内容。

快速入口
--------
- 博客主页：`/`
- 文章：`source/_posts/...`
- 游戏空间：`/games/`
- 游戏详情页模板：`/games/detail/?id=xxx`

游戏目录
---------
位于 `source/games/`，已支持两条链路：

1. **静态游戏入口**：`source/games/<游戏id>/index.html`
2. **统一详情页**：`/games/detail/?id=<游戏id>`，详情内容来自 `source/games/games.json`

游戏接入清单（必须更新）
--------------------------
修改 `source/games/games.json`，每条记录至少包含：

- `id`：游戏 ID（与目录名一致）
- `title`：显示标题
- `shortDesc`：列表短描述
- `description`：详情页描述
- `path`：`/games/<游戏id>/`
- `tags`：标签数组
- `controls`：操作方式
- `difficulty`：难度（可选）
- `updatedAt`：更新时间

Unity WebGL 目录建议结构
-------------------------
- `source/games/<游戏id>/index.html`
- `source/games/<游戏id>/Build/*`
- `source/games/<游戏id>/TemplateData/*`

部署
-----
GitHub Actions 自动在 `main` 分支推送时：
1) 安装依赖
2) `hexo generate`
3) 发布 `public/` 到 `gh-pages`

更多游戏添加说明
----------------
详细写法请看：`source/games/GAME_ADD_GUIDE.txt`

可以按相同格式继续新增游戏、并通过博客文章引用：
- 详情页链接：`/games/detail/?id=<游戏id>`
- 直接入口：`/games/<游戏id>/`
