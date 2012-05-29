javascript : function (attributes)
{
	// SET			
	var set =		function ()
					{
						chooser.getUIElement ("javascripts").setItems (sCMS.javascript.list ());
					};

	// ONINIT					
	var onInit =	function ()
					{				
						chooser.getUIElement ("javascripts").setAttribute ("onChange", onChange);
				
						// SET
						set ();
				
						// SHOW
						chooser.show ();							
					};

	// ONBUTTON1
	var onButton1 =	function ()
					{
						chooser.dispose ();
						
						if (attributes.onDone != null)
						{
							setTimeout( function ()	{ attributes.onDone (chooser.getUIElement ("javascripts").getItem ()); }, 1);
						}
					};
				
	// ONBUTTON2	
	var onButton2 =	function ()
					{
						chooser.dispose ();
						
						if (attributes.onDone != null)
						{
							setTimeout( function ()	{ attributes.onDone (null); }, 1);
						}						
					};
					
	// ONCHANGE	
	var onChange = 	function ()
					{
						if (chooser.getUIElement ("javascripts").getItem ())
						{
							chooser.getUIElement ("button1").setAttribute ("disabled", false);
						}
						else
						{
							chooser.getUIElement ("button1").setAttribute ("disabled", true);
						}
					};					

	var suixml = "";
	suixml += '<sui>';
//	suixml += '	<layoutbox type="horizontal">';
//	suixml += '		<panel size="*">';
//	suixml += '			<layoutbox type="vertical">';
//	suixml += '				<panel size="*">';
	suixml += '					<listview tag="javascripts" width="100%" height="100%" focus="true">';
	suixml += '						<column tag="id" />';
	suixml += '						<column tag="title" label="Title" width="200px" visible="true" />';	
	suixml += '					</listview>';
//	suixml += '				</panel>';
//	suixml += '			</layoutbox>';
//	suixml += '		</panel>';
//	suixml += '	</layoutbox>';
	suixml += '</sui>';
	
	var chooser = new sConsole.modal.chooser.base ({suiXML: suixml, title: "Choose javascript", button1Label: "Select", button2Label: "Close", onClickButton1: onButton1, onClickButton2: onButton2, onInit: onInit});	
}	




