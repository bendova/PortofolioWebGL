window.addEventListener("load", onLoaded);
window.addEventListener("resize", resizeCanvas);

var CANVAS_WIDTH_MIN = 1600;
var CANVAS_WIDTH_MAX = 1920;

var CANVAS_HEIGHT_MIN = 900;
var CANVAS_HEIGHT_MAX = 1080;

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
	
	var widthClamped = ClampMinMax(width, CANVAS_WIDTH_MIN, CANVAS_WIDTH_MAX);
	var heightClamped = ClampMinMax(height, CANVAS_HEIGHT_MIN, CANVAS_HEIGHT_MAX);
	
	var canvasElement = document.getElementById("canvas");
	if(canvasElement != null)
	{
		if((widthClamped != canvasElement.width) || (heightClamped != canvasElement.height))
		{
			canvasElement.style.width = widthClamped + "px";
			canvasElement.style.height = heightClamped + "px";
			
			console.log("resizeCanvas() widthClamped: " + widthClamped + ", heightClamped: " + heightClamped);
		}
	}
}

function ClampMinMax(value, minValue, maxValue)
{
	var clampedValue = (value < minValue) ? minValue: value;
	clampedValue = (clampedValue > maxValue) ? maxValue: clampedValue;
	return clampedValue;
}