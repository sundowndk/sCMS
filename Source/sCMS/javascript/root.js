// Delay before executing asyncronis request.
_asyncdelay : 10,

new : function (title)
{
	var content = new Array ();
	content["title"] = title;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.New", "data", "POST", false);			
	request.send (content);

	return request.respons ()["scms.root"];
},
	
load : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.Load", "data", "POST", false);	
	request.send (content);

	return request.respons ()["scms.root"];
},

save : function (root)
{					
	var content = new Array ();
	content["scms.root"] = root;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.Save", "data", "POST", false);		
	request.send (content);

	return true;
},

delete : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.Delete", "data", "POST", false);	
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
							attributes.onDone (respons["scms.roots"]);
						};
						
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.List", "data", "POST", true);
		request.onLoaded (onDone);
		request.send ();
	}
	else
	{
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Root.List", "data", "POST", false);		
		request.send ();

		return request.respons ()["scms.roots"];	
	}
}

