// Delay before executing asyncronis request.
_asyncdelay : 10,
	
create : function (template)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.New", "data", "POST", false);	
	request.send (template);

	return request.respons ();
},
	
load : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.Load", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;
		
	request.send (content);

	return request.respons ();
},
		
save : function (template)
{					
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.Save", "data", "POST", false);	
	request.send (template);

	return true;
},		

remove : function (id)
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.Delete", "data", "POST", false);	

	var content = new Array ();
	content["id"] = id;

	request.send (content);
			
	return true;
},				
		
list : function ()
{
	var request = new SNDK.ajax.request ("/", "cmd=Ajax;cmd.function=sCMS.Template.List", "data", "POST", false);		
	request.send ();

	return request.respons ()["templates"];		
}	

