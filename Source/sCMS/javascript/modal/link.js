link : function (attributes)
{					
	if (!attributes) attributes = new Array ();
					
	var choosepage =	function ()
						{
							var onDone =	function (result)
											{							
												if (result != null)
												{																					
													var page = sCMS.page.load (result.id);													
													modal.getUIElement ("link").setAttribute ("value", page.path);													
												}
											};
	
							sCMS.modal.chooser.page ({onDone: onDone});								
						};									
					
	var onDone =	function ()
					{
						attributes.onDone (get ());
						modal.dispose ();
					};
					
	var onChange =	function ()
					{
						if (modal.getUIElement ("link").getAttribute ("value") != "")
						{
							modal.getUIElement ("apply").setAttribute ("disabled", false);
						}
						else
						{
							modal.getUIElement ("apply").setAttribute ("disabled", true);
						}
					}					
						
	var set = 		function ()
					{
						modal.getUIElement ("link").setAttribute ("value", attributes.value);
					};
					
	var get =		function ()
					{
						var result = modal.getUIElement ("link").getAttribute ("value");
						return result;
					};
					
	var suixml = "";
	suixml += '<sui elementheight="50px">';
	suixml += '	<canvas width="600px" height="180px" canScroll="false">';
	suixml += '		<container title="Insert link" icon="Icon32Edit" stylesheet="SUIContainerModal">';
	suixml += '			<layoutbox type="horizontal" stylesheet="LayoutboxNoborder">';
	suixml += '				<panel size="%elementheight%">';
	suixml += '					<layoutbox type="vertical">';
	suixml += '						<panel size="100px">';
	suixml += '							<label text="Link"/>';
	suixml += '						</panel>';		
	suixml += '						<panel size="*">';
	suixml += '							<textbox tag="link" width="80%" focus="true" />';
	suixml += '							<button tag="choosepage" label="choose" width="20%" />';
	suixml += '						</panel>';				
	suixml += '					</layoutbox>';
	suixml += '				</panel>';
	suixml += '				<panel size="*">';
	suixml += '				</panel>';
	suixml += '				<panel size="45px">';
	suixml += '					<layoutbox type="vertical">';
	suixml += '						<panel size="*">';
	suixml += '						</panel>';
	suixml += '						<panel size="210px">';
	suixml += '							<button tag="apply" label="Apply" width="100px" disabled="true"/>';
	suixml += '							<button tag="close" label="Close" width="100px" />';
	suixml += '						</panel>';					
	suixml += '					</layoutbox>';
	suixml += '				</panel>';
	suixml += '			</layoutbox>';
	suixml += '		</container>';
	suixml += '	</canvas>';
	suixml += '</sui>';
															
	var modal = new sConsole.modal.window ({XML: suixml});
					
	// LINK
//	elements["linkcontainer"] = modalwindow.addControl ({type: "empty", label: "Link :"});			
//	elements["link"] = new SNDK.SUI.textbox ({appendTo: elements["linkcontainer"], width: "501px", value: options.value, focus: true, onKeyUp: null});
//	elements["linkbutton"] = new SNDK.SUI.button ({appendTo: elements["linkcontainer"], label: "&raquo;", width: "30px", onClick: choosepage});

			// TARGET
//			elements["target"] = modalwindow.addControl ({type: "dropbox", label: "Target :"});
																						
	// BUTTONS
//	elements["add"] = modalwindow.addButton ({label: "Apply", disabled: false, onClick: done});
//	elements["close"] = modalwindow.addButton ({label: "Close", onClick: modalwindow.dispose});

	modal.getUIElement ("apply").setAttribute ("onClick", onDone);
	modal.getUIElement ("close").setAttribute ("onClick", modal.dispose);
	
	modal.getUIElement ("link").setAttribute ("onChange", onChange);
	modal.getUIElement ("choosepage").setAttribute ("onClick", choosepage);
			
	// SET		
	set ();
			
	// SHOW		
	modal.show ();
}	
