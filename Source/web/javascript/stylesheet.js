// Delay before executing asyncronis request.
_asyncdelay : 10,

new : function (title)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.New", "data", "POST", false);		
	
	var content = new Array ();
	content["title"] = title;
	
	request.send (content);

	return request.respons ()["scms.stylesheet"];
},
	
load : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.Load", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;

	request.send (content);

	return request.respons ()["scms.stylesheet"];
},

save : function (stylesheet)
{					
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.Save", "data", "POST", false);		
	
	var content = new Array ();
	content["scms.stylesheet"] = stylesheet;
	
	request.send (content);
					
	return true;
},

delete : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.Delete", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;

	request.send (content);
					
	return true;					
},

list : function ()
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Stylesheet.List", "data", "POST", false);		
	request.send ();

	return request.respons ()["scms.stylesheets"];
}