// Delay before executing asyncronis request.
_asyncdelay : 10,

create : function (global)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.New", "data", "POST", false);		
	request.send (global);

	return request.respons ();
},		

load : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.Load", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;

	request.send (content);

	return request.respons ();
},

save : function (global)
{					
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.Save", "data", "POST", false);						
	request.send (global);
					
	return true;
},

remove : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.Delete", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;

	request.send (content);

	return true;					
},		

list : function ()
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Global.List", "data", "POST", false);		
	request.send ();

	return request.respons ()["globals"];
}
}
