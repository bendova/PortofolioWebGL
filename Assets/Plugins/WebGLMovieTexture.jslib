var LibraryWebGLMovieTexture = {
$videoInstances: [],
$videoInstancesRecycled: [],

WebGLMovieTextureCreate: function(url)
{
	var str = Pointer_stringify(url);
	var video = null;
	if(videoInstancesRecycled.length > 0)
	{
		video = videoInstancesRecycled.pop();
	}
	else
	{
		video = document.createElement('video');
	}
	video.style.display = "none";
	video.src = str;
	video.preload = "metadata";
	var videoIndex = videoInstances.push(video) - 1;
	return videoIndex;
},

WebGLMovieTextureDispose: function(videoIndex)
{
	if(videoIndex < videoInstances.length)
	{
		var video = videoInstances.splice(videoIndex, 1)[0];
		
		video.pause();
		video.src = "";
		video.load();
		
		videoInstancesRecycled.push(video);
	}
},

WebGLMovieTextureUpdate: function(video, tex)
{
	if (videoInstances[video].paused)
		return;
	
	GLctx.bindTexture(GLctx.TEXTURE_2D, GL.textures[tex]);
    GLctx.pixelStorei(GLctx.UNPACK_FLIP_Y_WEBGL, true);
	GLctx.texImage2D(GLctx.TEXTURE_2D, 0, GLctx.RGBA, GLctx.RGBA, GLctx.UNSIGNED_BYTE, videoInstances[video]);
	GLctx.pixelStorei(GLctx.UNPACK_FLIP_Y_WEBGL, false);
},

WebGLMovieTexturePlay: function(video)
{
	videoInstances[video].play();
},

WebGLMovieTexturePause: function(video)
{
	videoInstances[video].pause();
},

WebGLMovieTextureSeek: function(video, time)
{
	videoInstances[video].currentTime = time;
},

WebGLMovieTextureLoop: function(video, loop)
{
	videoInstances[video].loop = loop;
},

WebGLMovieTextureHeight: function(video)
{
	return videoInstances[video].videoHeight;
},

WebGLMovieTextureWidth: function(video)
{
	return videoInstances[video].videoWidth;
},

WebGLMovieTextureTime: function(video)
{
	return videoInstances[video].currentTime;
},

WebGLMovieTextureDuration: function(video)
{
	return videoInstances[video].duration;
},

WebGLMovieTextureIsReady: function(video)
{
	return videoInstances[video].readyState >= videoInstances[video].HAVE_CURRENT_DATA;
},

WebGLMovieTextureHasEnded: function(video)
{
	return videoInstances[video].ended;
}

};
autoAddDeps(LibraryWebGLMovieTexture, '$videoInstances');
autoAddDeps(LibraryWebGLMovieTexture, '$videoInstancesRecycled');
mergeInto(LibraryManager.library, LibraryWebGLMovieTexture);
