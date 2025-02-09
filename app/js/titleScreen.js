const canvas = document.getElementById("webgl-canvas");
const gl = canvas.getContext("webgl");
const newJourneyButton = document.getElementById("new-journey");

if (!gl) {
  console.error("WebGL not supported");
  alert("WebGL not supported");
}

// Transition to Rocket cutscene after user clicks New Journey
function transitionNewJourney() {
  document.body.classList.add("fade-out");

  // setTimeout function to allow fadeout animation
  setTimeout(() => {
    window.location.href = "rocketScene.html";
  }, 1500);
}

newJourneyButton.addEventListener("click", transitionNewJourney);

// Initial scene setup
gl.viewport(0, 0, canvas.width, canvas.height);
gl.clearColor(0.0, 0.0, 0.0, 1.0);
gl.clear(gl.COLOR_BUFFER_BIT);
