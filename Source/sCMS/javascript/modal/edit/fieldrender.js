fieldRender : function (attributes)
{	
	var result = new Array ();

	for (i=0; i < attributes.fields.length; i++)			
	{
		if (!attributes.fields[i].options.hidden)
		{			
			var height;			
	
			switch (attributes.fields[i].type.toLowerCase ())
			{
				case "string":
				{
					height = "50px";
					break;
				}
			
				case "link":
				{
					height = "50px";
					break;
				}
			
				case "text":
				{
					height = "300px";
					break;
				}
			
				default:
				{
					height = "220px";
					break;				
				}
			}
																		
			var layoutbox = new SNDK.SUI.layoutbox ({type: "vertical", height: height});
			layoutbox.addPanel ({tag: "text", size: "100px"});
			layoutbox.addPanel ({tag: "field", size: "*"});			
			layoutbox.getPanel ("text").addUIElement (new SNDK.SUI.label ({text: attributes.fields[i].name}));
																			
			result[attributes.fields[i].id] = new SNDK.SUI.field ({type: attributes.fields[i].type.toLowerCase (), options: attributes.fields[i].options, width: "100%", height: "100%", onChange: attributes.onChange});
		
			layoutbox.getPanel ("field").addUIElement (result[attributes.fields[i].id]);
				
			attributes.appendTo.addUIElement (layoutbox);			
		}
	}		
	
	return result;
}