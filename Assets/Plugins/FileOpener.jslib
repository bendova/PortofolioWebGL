var FileOpener = 
{
	FileOpenPdf: function(url)
	{
		var strUrl = Pointer_stringify(url);
		var win = window.open(strUrl, "_blank");
		if(win != null)
		{
			win.focus();
		}
	}
};
mergeInto(LibraryManager.library, FileOpener);
