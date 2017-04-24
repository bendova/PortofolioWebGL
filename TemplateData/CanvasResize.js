window.addEventListener("load", onLoaded);
window.addEventListener("resize", resizeCanvas);

var CANVAS_WIDTH_MIN = 1600.0;
var CANVAS_WIDTH_MAX = 1920.0;

var CANVAS_HEIGHT_MIN = 900.0;
var CANVAS_HEIGHT_MAX = 1080.0;

const ASPECT_RATIO = CANVAS_WIDTH_MAX / CANVAS_HEIGHT_MAX;

function onLoaded()
{
	console.log("onLoaded");
	resizeCanvas();
}

function setCanvasMaxSize(maxWidth, maxHeight)
{
	console.log("setCanvasMaxSize() maxWidth: " + maxWidth + ", maxHeight: " + maxHeight);
	
	CANVAS_WIDTH_MAX = maxWidth;
	CANVAS_HEIGHT_MAX = maxHeight;
	resizeCanvas(); 
}

function resizeCanvas() 
{
	var width = window.innerWidth ||
				document.documentElement.clientWidth ||
				document.body.clientWidth;
	
	var height = window.innerHeight ||
				document.documentElement.clientHeight ||
				document.body.clientHeight;
	
	console.log("resizeCanvas() width: " + width + ", height: " + height);
	
	var widthClamped = ValueMin(width, CANVAS_WIDTH_MAX);
	var heightClamped = ValueMin(height, CANVAS_HEIGHT_MAX);
	if((widthClamped < CANVAS_WIDTH_MIN) || ((heightClamped < CANVAS_HEIGHT_MIN)))
	{
		widthClamped = heightClamped * ASPECT_RATIO;
		if(widthClamped > width)
		{
			widthClamped = width;
			heightClamped = widthClamped / ASPECT_RATIO;
		}
	}
	
	var canvasElement = document.getElementById("canvas");
	if(canvasElement != null)
	{
		if((widthClamped != canvasElement.width) || (heightClamped != canvasElement.height))
		{
			canvasElement.style.width = widthClamped + "px";
			canvasElement.style.height = heightClamped + "px";
			
			var canvasFrame = document.getElementById("canvasFrame");
			if(canvasFrame != null)
			{
				var scaleFactorX = widthClamped / CANVAS_WIDTH_MAX;
				var scaleFactorY = heightClamped / CANVAS_HEIGHT_MAX;
				canvasFrame.style.transform = "scale(" + scaleFactorX + ", " + scaleFactorY + ")";
			}
			
			console.log("resizeCanvas() widthClamped: " + widthClamped + ", heightClamped: " + heightClamped);
		}
	}
}

function ValueMin(a, b)
{
	return (a < b) ? a: b;
}