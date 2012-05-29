page : function (attributes)
{
	// SET			
	var set =		function ()
					{
						sCMS.root.list ({async: true, onDone: onRootDone});
						sCMS.page.list ({async: true, onDone: onPageDone});														
					};

	// ONINIT					
	var onInit =	function ()
					{				
						chooser.getUIElement ("pages").setAttribute ("onChange", onChange);
				
						// SET
						set ();
				
						// SHOW
						chooser.show ();							
					};

	// ONBUTTON1
	var onButton1 =		function ()
						{
							chooser.dispose ();
							
							if (attributes.onDone != null)
							{
								setTimeout( function () 	{ attributes.onDone (chooser.getUIElement ("pages").getItem ()); }, 1);
							}
						};
				
	// ONBUTTON2	
	var onButton2 =		function ()
						{
							chooser.dispose ();
						
							if (attributes.onDone != null)
							{
								setTimeout( function ()	{ attributes.onDone (null); }, 1);
							}						
						};
					
	// ONCHANGE	
	var onChange = 		function ()
						{
							if (chooser.getUIElement ("pages").getItem ())
							{
								if (chooser.getUIElement ("pages").getItem ().type == "page")
								{
									chooser.getUIElement ("button1").setAttribute ("disabled", false);
								}							
								else
								{
									chooser.getUIElement ("button1").setAttribute ("disabled", true);
								}
							}
							else
							{
								chooser.getUIElement ("button1").setAttribute ("disabled", true);
							}
						};					
													
	var onRootDone =	function (result)
						{
						
							for (index in result)
							{
								result[index].type = "root";
							}							
							chooser.getUIElement ("pages").addItems (result);
								console.log (result)
						};
																			
	var onPageDone =	function (result)
						{
						
							for (index in result)
							{
								result[index].type = "page";
								if (result[index].parentid == "00000000-0000-0000-0000-000000000000")
								{
									result[index].parentid = result[index].rootid;
								}
							}	
							console.log (result)
							chooser.getUIElement ("pages").addItems (result);
						};					

	var suixml = "";
	suixml += '<sui>';
//	suixml += '	<layoutbox type="horizontal">';
//	suixml += '		<panel size="*">';
//	suixml += '			<layoutbox type="vertical">';
//	suixml += '				<panel size="*">';
	suixml += '					<listview tag="pages" width="100%" height="100%" treeview="false" treeviewLinkColumns="id:parentid" treeviewRootValue="00000000-0000-0000-0000-000000000000">';
	suixml += '						<column tag="id" />';
	suixml += '						<column tag="title" label="Title" width="200px" visible="true" />';	
	suixml += '						<column tag="type" />'
	suixml += '						<column tag="parentid" />'
	suixml += '					</listview>';	
//	suixml += '				</panel>';
//	suixml += '			</layoutbox>';	
//	suixml += '		</panel>';
//	suixml += '	</layoutbox>';
	suixml += '</sui>';
	

	//var chooser = new sConsole.modal.chooser.base ({suiXML: suixml, title: "Choose page", buttonLabel: "Ok|Cancel", onClickButton1: onButton1, onClickButton2: onButton2});											
	var chooser = new sConsole.modal.chooser.base ({suiXML: suixml, title: "Choose page", button1Label: "Select", button2Label: "Close", onClickButton1: onButton1, onClickButton2: onButton2, onInit: onInit});
}	






