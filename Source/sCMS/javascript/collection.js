new : function (collectionSchemaId, Title)
{
	var content = new Array ();
	content["collectionschemaid"] = collectionSchemaId;
	content["title"] = Title;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.New", "data", "POST", false);		
	request.send (content);

	return request.respons ()["scms.collection"];
},		

load : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.Load", "data", "POST", false);	
	request.send (content);

	return request.respons ()["scms.collection"];
},

save : function (collection)
{					
	var content = new Array ();
	content["scms.collection"] = collection

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.Save", "data", "POST", false);						
	request.send (content);
					
	return true;
},

delete : function (id)
{
	var content = new Array ();
	content["id"] = id;

	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.Delete", "data", "POST", false);	
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
							attributes.onDone (respons["scms.collections"]);
						};
						
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.List", "data", "POST", true);
		request.onLoaded (onDone);
		request.send ();
	}
	else
	{
		var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Collection.List", "data", "POST", false);		
		request.send ();

		return request.respons ()["scms.collections"];	
	}
}

