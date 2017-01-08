function UnityProgress (dom) {
	this.progress = 0.0;
	this.message = "";
	this.dom = dom;
	
	var parent = dom.parentNode;
	
	createjs.CSSPlugin.install(createjs.Tween);
	createjs.Ticker.setFPS(60);
	
	this.SetProgress = function (progress) 
	{
		console.log("SetProgress() progress: " + progress);
		
		if (this.progress < progress)
		{
			this.progress = progress;
			this.RefreshLoadingBar();
		}
	}
	
	this.RefreshLoadingBar = function() 
	{
		console.log("RefreshLoadingBar()");
		
		var barWidthMax = 200.0;
		var length = barWidthMax * Math.min(this.progress, 1.0);
		var bar = document.getElementById("progressBar");
		createjs.Tween.removeTweens(bar);
		
		var tweenDuration = 500.0; // ms
		createjs.Tween.get(bar).to({width: length}, tweenDuration, createjs.Ease.sineOut);
	}
	
	this.HideProgressBar = function() 
	{
		console.log("HideProgressBar()");
		
		var bar = document.getElementById("progressBar");
		createjs.Tween.removeTweens(bar);
		bar.style.display = "none";
	}

	this.SetMessage = function (message) 
	{
		console.log("SetMessage() message: " + message);
		
		this.message = message; 
		document.getElementById("loadingInfo").innerHTML = this.message;
	}

	this.Clear = function() 
	{
		console.log("Clear()");
		this.HideProgressBar();
		document.getElementById("loadingBox").style.display = "none";
		document.getElementById("loadingBg").style.display = "none";
	}

	this.SetMessage("");
	this.RefreshLoadingBar();
}