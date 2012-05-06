new : function (type, name)
{
	var content = new Array ();
	content["type"] = type;
	content["name"] = name;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.New", "data", "POST", false);		
	request.send (content);

	return request.respons ()["scms.global"];
},		

load : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.Load", "data", "POST", false);	
	request.send (content);

	return request.respons ()["scms.global"];
},

save : function (global)
{					
	var content = new Array ();
	content["scms.global"] = global;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.Save", "data", "POST", false);						
	request.send (content);
					
	return true;
},

delete : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.Delete", "data", "POST", false);	
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
							attributes.onDone (respons["scms.globals"]);
						};
						
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.List", "data", "POST", true);
		request.onLoaded (onDone);
		request.send ();
	}
	else
	{
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.List", "data", "POST", false);		
		request.send ();

		return request.respons ()["scms.globals"];	
	}
}
