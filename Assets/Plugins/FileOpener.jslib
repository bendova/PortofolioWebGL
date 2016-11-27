var FileOpener = 
{
	FileOpenPdf: function(url)
	{
		var strUrl = Pointer_stringify(url);
		var OpenWindow = function()
		{
			var win = window.open(strUrl, "_blank");
			if(win != null)
			{
				win.focus();
			}
			document.getElementById('canvas').removeEventListener('click', OpenWindow);
		};
		document.getElementById('canvas').addEventListener('click', OpenWindow, false);
	}
};
mergeInto(LibraryManager.library, FileOpener);
