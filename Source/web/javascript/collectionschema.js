new : function (title)
{
	var content = new Array ();
	content["title"] = title;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.New", "data", "POST", false);		
	request.send (content);

	return request.respons ()["scms.collectionschema"];
},		

load : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.Load", "data", "POST", false);	
	request.send (content);

	return request.respons ()["scms.collectionschema"];
},

save : function (collectionschema)
{					
	var content = new Array ();
	content["scms.collectionschema"] = collectionschema

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.Save", "data", "POST", false);						
	request.send (content);
					
	return true;
},

delete : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.Delete", "data", "POST", false);	
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
							attributes.onDone (respons["scms.collectionschemas"]);
						};
						
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.List", "data", "POST", true);
		request.onLoaded (onDone);
		request.send ();
	}
	else
	{
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.List", "data", "POST", false);		
		request.send ();

		return request.respons ()["scms.collectionschemas"];	
	}
}
