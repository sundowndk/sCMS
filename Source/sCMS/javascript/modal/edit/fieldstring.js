fieldString : function (attributes)
{	
	// SET	
	var set = 		function ()
					{
						modal.getUIElement ("string").setAttribute ("value", attributes.current.value);
						
						attributes.checksum = sConsole.helpers.arrayChecksum (attributes.current);
						
						onChange ();
					};
						
	// GET
	var get = 		function ()
					{
						attributes.current.value = modal.getUIElement ("string").getAttribute ("value");
					};
					
	// DISPOSE
	var dispose =	function ()
					{
						modal.dispose ();
					};

	// ONINIT
	var onInit =	function ()
					{
						if (!attributes)
							attributes = new Array ();		

						if (attributes.string == null)
						{					
							attributes.mode = "new";
							attributes.current = {};
							attributes.current.value = ""		
							
							attributes.title =  "Add new string";
							attributes.button1Label = "Add";							
						}
						else
						{
							attributes.mode = "edit";
							attributes.current = attributes.string;		
							
							attributes.title =  "Edit string";
							attributes.button1Label = "Apply";
						}
						
						modal.getUIElement ("string").setAttribute ("onChange", onChange);			
		
						modal.getUIElement ("button1").setAttribute ("onClick", onButton1);
						modal.getUIElement ("close").setAttribute ("onClick", dispose);	
		
						modal.getUIElement ("container").setAttribute ("title", attributes.title);
						modal.getUIElement ("button1").setAttribute ("label", attributes.button1Label);
				
						// SET
						set ();						
			
						// SHOW
						modal.show ();	
					};														
																																										
	// ONBUTTON1
	var onButton1 =	function ()
					{										
						get ();
					
						dispose ();						
												
						if (attributes.onDone != null)
						{	
							setTimeout (function () {attributes.onDone (attributes.current)}, 1);
						}						
					};
					
	// ONCHANGE
	var onChange =	function ()
					{		
						get ();
						
						if (sConsole.helpers.arrayChecksum (attributes.current) != attributes.checksum)		
						{
							modal.getUIElement ("button1").setAttribute ("disabled", false);
						}
						else
						{
							modal.getUIElement ("button1").setAttribute ("disabled", true);
						}	
					};		
																										
	// INIT				
	var modal = new sConsole.modal.window ({width: "600px", height: "150px", titleBarUI: [{type: "button", attributes: {tag: "button1", label: ""}}, {type: "button", attributes: {tag: "close", label: "Close"}}], busy: true, SUIXML: "/console/xml/scms/modal/edit/fieldstring.xml", onInit: onInit});
}

