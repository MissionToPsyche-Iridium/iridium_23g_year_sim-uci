const canvas = document.getElementById("webgl-canvas");
const gl = canvas.getContext("webgl");

if (!gl) {
  console.error("WebGL not supported");
  alert("WebGL not supported");
}

// Transition to Rocket cutscene after user clicks New Journey
function transitionToGame() {
  // setTimeout function to allow fadeout animation
  setTimeout(() => {
    document.body.classList.add("fade-out");
    setTimeout(() => {
      window.location.href = "asteroidUI.html";
    }, 1500);
  }, 4000);
}

// Initial scene setup after fading in
window.addEventListener("load", function () {
  this.document.body.classList.add("fade-in");
});

gl.viewport(0, 0, canvas.width, canvas.height);
gl.clearColor(0.0, 0.0, 1.0, 1.0);
gl.clear(gl.COLOR_BUFFER_BIT);

transitionToGame();
