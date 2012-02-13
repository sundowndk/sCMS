// Delay before executing asyncronis request.
_asyncdelay : 10,

create : function (global)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.New", "data", "POST", false);		
	request.send (global);

	return request.respons ();
},		

load : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.Load", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;

	request.send (content);

	return request.respons ();
},

save : function (collectionschema)
{					
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.Save", "data", "POST", false);						
	request.send (collectionschema);
					
	return true;
},

remove : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.Delete", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;

	request.send (content);

	return true;					
},		

list : function ()
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.CollectionSchema.List", "data", "POST", false);		
	request.send ();

	return request.respons ()["collectionschemas"];
}
