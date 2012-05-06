new : function ()
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.New", "data", "POST", false);	
	request.send ();

	return request.respons ()["scms.template"];
},
	
load : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.Load", "data", "POST", false);		
	request.send (content);

	return request.respons ()["scms.template"];
},
		
save : function (template)
{	
	var content = new Array ();
	content["scms.template"] = template;
								
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.Save", "data", "POST", false);	
	request.send (content);

	return true;
},		

delete : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.Delete", "data", "POST", false);	
	request.send (content);
			
	return true;
},				
		
list : function (attributes)
{
	if (!attributes) attributes = new Array ();
	
	if (attributes.async)
	{
		var onDone = 	function (respons)
						{
							attributes.onDone (respons["scms.templates"]);
						};
		
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.List", "data", "POST", true);
		request.onLoaded (onDone);
		request.send ();						
	}
	else
	{
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.List", "data", "POST", false);		
		request.send ();

		return request.respons ()["scms.templates"];		
	}
}	

