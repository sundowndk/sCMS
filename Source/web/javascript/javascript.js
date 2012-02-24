new : function (title)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.New", "data", "POST", false);		
	
	var content = new Array ();
	content["title"] = title;
	
	request.send (content);

	return request.respons ()["scms.javascript"];
},
	
load : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.Load", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;

	request.send (content);

	return request.respons ()["scms.javascript"];
},

save : function (javascript)
{					
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.Save", "data", "POST", false);		
	
	var content = new Array ();
	content["scms.javascript"] = javascript;
	
	request.send (content);
					
	return true;
},

delete : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.Delete", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;

	request.send (content);
					
	return true;					
},

list : function ()
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Javascript.List", "data", "POST", false);		
	request.send ();

	return request.respons ()["scms.javascripts"];
}
