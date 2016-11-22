var ContactMessage = 
{
	SendEmailMessage: function(gameObjName, senderEmail, message)
	{
		console.log("SendEmailMessage() " + emailjs);
		
		var gameObjName_str = Pointer_stringify(gameObjName);
		var senderEmail_str = Pointer_stringify(senderEmail);
		var message_str = Pointer_stringify(message);
		
		emailjs.init("user_VI3RdEFMhSRYrCm43koiy");
		emailjs.send("gmail", "template_OtYwnFMR", {"sender_email": senderEmail_str, "message": message_str})
			.then(
			function(response) 
			{
				console.log("SUCCESS. status=%d, text=%s", response.status, response.text);
				SendMessage(gameObjName_str, "OnSendEmailMessageSuccess");
			}, 
			function(err) 
			{
				console.log("FAILED. error=", err);
				SendMessage(gameObjName_str, "OnSendEmailMessageFailed", "error: " + err);
			});
	}
};

mergeInto(LibraryManager.library, ContactMessage);
