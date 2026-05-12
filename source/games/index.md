---
title: 游戏空间
date: 2026-05-12
layout: page
---

这里会放一些**可直接运行的网页内容**，包括：

- Unity 导出的 WebGL 小游戏
- Canvas/HTML5 小应用
- 其他想挂在博客里的互动内容

本站是静态站点，`/games` 目录下的文件会原样发布，适合放小游戏与前端 Demo。

## 示例游戏

<div class="games-entry-list">
  <div class="game-card">
    <h3>HTML5 贪吃蛇</h3>
    <p>方向键 / WASD 控制，适合键盘操作。</p>
    <p>
      <a href="/games/detail/?id=snake">查看详情 / 试玩</a>
    </p>
  </div>
  <div class="game-card">
    <h3>HTML5 扫雷</h3>
    <p>左键开格、右键插旗，可快速验证交互体验。</p>
    <p>
      <a href="/games/detail/?id=minesweeper">查看详情 / 试玩</a>
    </p>
  </div>
</div>

## 目录约定

### 新建一个小游戏（推荐）

1. 在 `source/games/` 下创建独立目录，如：
   - `source/games/my-game/index.html`
2. 将该目录里的 `index.html` 作为游戏入口（可以放本地静态资源、Unity WebGL 导出目录等）。
3. 在 [games 清单](/games/games.json) 中补充一条记录。
4. 详情页会自动按清单生成入口卡片。

### Unity WebGL 可发布目录推荐

放法示例：

- `source/games/my-3d-game/index.html`
- `source/games/my-3d-game/Build/*`
- `source/games/my-3d-game/TemplateData/*`

发布路径是：

`https://你的域名/games/my-3d-game/`

<script>
  fetch('/games/games.json')
    .then((resp) => resp.json())
    .then((list) => {
      const mount = document.querySelector('.games-entry-list')
      if (!mount) return
      if (!Array.isArray(list) || list.length === 0) return
      list.forEach((g) => {
        if (g.hideInList) return
        if (g.id === 'snake' || g.id === 'minesweeper') return
        const card = document.createElement('div')
        card.className = 'game-card'
        card.innerHTML = `
          <h3>${g.title}</h3>
          <p>${g.shortDesc}</p>
          <p>
            <a href="/games/detail/?id=${encodeURIComponent(g.id)}">查看详情 / 试玩</a>
          </p>
        `
        mount.appendChild(card)
      })
    })
    .catch(() => {})
</script>

<style>
  .games-entry-list {
    display: grid;
    gap: 12px;
  }
  .game-card {
    background: rgba(255, 255, 255, 0.08);
    border: 1px solid rgba(255, 255, 255, 0.16);
    border-radius: 10px;
    padding: 12px;
    margin: 8px 0;
  }
  .game-card h3 {
    margin: 0 0 8px;
  }
  .game-card p {
    margin: 0 0 8px;
    color: #cfd3ff;
  }
  .game-card a {
    color: #82b5ff;
    text-decoration: none;
  }
  .game-card a:hover {
    text-decoration: underline;
  }
</style>
