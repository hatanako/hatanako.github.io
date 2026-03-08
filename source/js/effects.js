// ==================
// 0. 入场加载特效
// ==================
;(function () {
  // 创建遮罩
  const loader = document.createElement('div')
  loader.id = 'page-loader'
  loader.innerHTML = `
    <div class="loader-content">
      <div class="loader-circle"></div>
      <div class="loader-text">垃圾回收厂</div>
    </div>
  `
  document.body.appendChild(loader)

  // 页面加载完毕后淡出
  window.addEventListener('load', function () {
    setTimeout(() => {
      loader.style.opacity = '0'
      setTimeout(() => loader.remove(), 800)
    }, 500)
  })
})()

document.addEventListener('DOMContentLoaded', function () {

  // ==================
  // 1. 打字机效果
  // ==================
  const titleEl = document.querySelector('#site-title') || document.querySelector('.site-title')
  if (titleEl) {
    const text = titleEl.innerText
    titleEl.innerText = ''
    let i = 0
    const typing = setInterval(() => {
      titleEl.innerText += text[i]
      i++
      if (i >= text.length) clearInterval(typing)
    }, 150)
  }

  // ==================
  // 2. 粒子特效
  // ==================
  const header = document.querySelector('#page-header')
  if (header) {
    const canvas = document.createElement('div')
    canvas.id = 'particles-js'
    canvas.style.cssText = 'position:absolute;top:0;left:0;width:100%;height:100%;z-index:1;pointer-events:none;'
    header.style.position = 'relative'
    header.insertBefore(canvas, header.firstChild)

    if (typeof particlesJS !== 'undefined') {
      particlesJS('particles-js', {
        particles: {
          number: { value: 60 },
          color: { value: '#ffffff' },
          opacity: { value: 0.5, random: true },
          size: { value: 3, random: true },
          move: {
            enable: true,
            speed: 1.5,
            direction: 'none',
            random: true,
            out_mode: 'out'
          },
          line_linked: {
            enable: true,
            distance: 120,
            color: '#ffffff',
            opacity: 0.2,
            width: 1
          }
        },
        interactivity: {
          detect_on: 'canvas',
          events: {
            onhover: { enable: true, mode: 'grab' }
          }
        }
      })
    }
  }

  // ==================
  // 3. 鼠标涟漪效果
  // ==================
  document.addEventListener('click', function (e) {
    const ripple = document.createElement('div')
    ripple.style.cssText = `
      position: fixed;
      left: ${e.clientX - 10}px;
      top: ${e.clientY - 10}px;
      width: 20px;
      height: 20px;
      border-radius: 50%;
      background: rgba(255, 255, 255, 0.6);
      pointer-events: none;
      z-index: 9999;
      animation: ripple-effect 0.6s ease-out forwards;
    `
    document.body.appendChild(ripple)
    setTimeout(() => ripple.remove(), 600)
  })

  document.addEventListener('mousemove', function (e) {
    const glow = document.createElement('div')
    glow.style.cssText = `
      position: fixed;
      left: ${e.clientX - 5}px;
      top: ${e.clientY - 5}px;
      width: 10px;
      height: 10px;
      border-radius: 50%;
      background: rgba(255, 255, 255, 0.3);
      pointer-events: none;
      z-index: 9999;
      animation: fade-out 0.5s ease-out forwards;
    `
    document.body.appendChild(glow)
    setTimeout(() => glow.remove(), 500)
  })

  // ==================
  // 4. 标题下方打字机循环
  // ==================
function initTyped() {
  const titleEl = document.querySelector('#site-title')
  if (!titleEl) return
  if (document.querySelector('#typed-subtitle')) return

  const typedDiv = document.createElement('div')
  typedDiv.id = 'typed-subtitle'
  typedDiv.style.cssText = `
    color: rgba(255,255,255,0.85) !important;
    font-size: 1rem;
    margin-top: 8px;
    letter-spacing: 3px;
    text-shadow: 0 1px 4px rgba(0,0,0,0.5);
    text-align: center;
    display: block;
  `
  typedDiv.innerHTML = '<span id="typed-text"></span>'
  titleEl.insertAdjacentElement('afterend', typedDiv)

    new Typed('#typed-text', {
      strings: [
        '不看看吗？',
        '真的不看看吗？',
        '真的真的不看看吗？',
        '真的真的真的不看看吗？',
        '真的真的真的真的不看看吗？',
        '真的真的真的真的真的不看看吗？',
        '真的真的真的真的真的真的不看看吗？',
        '真的真的真的真的真的真的真的不看看吗？',
        '真的真的真的真的真的真的真的真的不看看吗？',
        '你好',
        '早上好',
        '中午好',
        '晚上好',
        '早上好中午好晚上好半夜好',
        '不知道说啥了总之你好',
        '还在看这里呢，别看了',
        '这里没什么东西的',
        '真的没东西',
        '真的真的没东西',
        '真的真的真的没东西',
        '真的真的真的真的没东西',
        '真的真的真的真的真的没东西',
        '好吧，你赢了',
        '我的QQ是',
        '是.........',
        '是2397663712',
        '不要视奸我TvT',
        '欢迎来扩列好吗，谢谢你OvO'
      ],
      typeSpeed: 120,
      backSpeed: 60,
      backDelay: 2000,
      startDelay: 500,
      loop: true,
      showCursor: true,
      cursorChar: '|',
    })
  }


  waitForTyped()
})