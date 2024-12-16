const body = document.body;

let posX = 0;
let posY = 0;
const step = 0.2; // viteza

function moveBackground() {
    posX -= step;
    posY -= step;
    body.style.backgroundPosition = `${posX}px ${posY}px`;
    // console.log(`Background position: ${posX}px ${posY}px`); // debug
    requestAnimationFrame(moveBackground);
}

moveBackground();