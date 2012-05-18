template : function (attributes)
{
	// SET			
	var set =		function ()
					{
						chooser.getUIElement ("templates").setItems (sCMS.template.list ());										
					};

	// ONINIT					
	var onInit =	function ()
					{				
						chooser.getUIElement ("templates").setAttribute ("onChange", onChange);
				
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
							setTimeout( function ()	{ attributes.onDone (chooser.getUIElement ("templates").getItem ()); }, 1);
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
						if (chooser.getUIElement ("templates").getItem ())
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
	suixml += '					<listview tag="templates" width="100%" height="100%" focus="true" treeview="true" treeviewLinkColumns="id:parentid" treeviewRootValue="00000000-0000-0000-0000-000000000000">';
	suixml += '						<column tag="id" />';
	suixml += '						<column tag="title" label="Title" width="200px" visible="true" />';	
	suixml += '						<column tag="parentid" />'
	suixml += '					</listview>';
//	suixml += '				</panel>';
//	suixml += '			</layoutbox>';
//	suixml += '		</panel>';
//	suixml += '	</layoutbox>';
	suixml += '</sui>';

	var chooser = new sConsole.modal.chooser.base ({suiXML: suixml, title: "Choose template", buttonLabel1: "Select", buttonLabel2: "Close", onClickButton1: onButton1, onClickButton2: onButton2, onInit: onInit});
}





