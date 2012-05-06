if (sorentoUI != null)
{
	sorentoUI.sCMS = {};

	sorentoUI.sCMS.modal = {};
	
	sorentoUI.sCMS.widgets = {};
	
	// -------------------------------------------------------------------------------------------------------------------------
	// fieldList (options)
	// -------------------------------------------------------------------------------------------------------------------------
	// .getValue (string)	
	// .setValue (bool)	
	// -------------------------------------------------------------------------------------------------------------------------	
	sorentoUI.sCMS.widgets.fieldList = function (options)
	{
		_options = options;
		_temp =	{ initalized: false, 
			  id: SNDK.tools.newGuid (),
			  elements: new Array ()			 
			};

		this.getValue = getValue;
		this.setValue = setValue;
				
		window.onDomReady (init);		

		// ------------------------------------
		// init
		// ------------------------------------			
		function init ()
		{
			// CONTAINER
			_temp.elements["container"] = SNDK.tools.newElement ("div", {appendTo: _options.appendTo});
						
			// FIELDS
			var columns = new Array ();
			columns[0] = {title: "Id", tag: "id", width: "0px", visible: false};
			columns[1] = {title: "Name", tag: "name", width: "150px", visible: true};
			columns[2] = {title: "Type", tag: "type", width: "100px", visible: true};
			_temp.elements["listview"] = sorentoUI.addControl ({type: "listview", label: "Fields" +" :", stylesheet: sorentoUI.defaults.stylesheet.listview, height: "200px", columns: columns, onChange: eventOnChange, appendTo: _temp.elements["container"]});

			// FIELDS BUTTONS
			_temp.elements["buttonbar"] = sorentoUI.addControl ({type: "buttonbar", label: "", appendTo: _options.appendTo});
			_temp.elements["add"] = _temp.elements["buttonbar"].addButton ({label: "Add", stylesheet: sorentoUI.defaults.stylesheet.smallButton, onClick: eventOnAdd});
			_temp.elements["edit"] = _temp.elements["buttonbar"].addButton ({label: "Edit", stylesheet: sorentoUI.defaults.stylesheet.smallButton, onClick: eventOnEdit, disabled: true});
			_temp.elements["remove"] = _temp.elements["buttonbar"].addButton ({label: "Remove", stylesheet: sorentoUI.defaults.stylesheet.smallButton, onClick: eventOnRemove, disabled: true});

			_temp.elements["up"] = _temp.elements["buttonbar"].addButton ({label: "Up", stylesheet: sorentoUI.defaults.stylesheet.smallButton, onClick: "", disabled: true}, "right");
			_temp.elements["down"] = _temp.elements["buttonbar"].addButton ({label: "Down", stylesheet: sorentoUI.defaults.stylesheet.smallButton, onClick: "", disabled: true}, "right");						
										
			// Done
			_temp.initialized = true;
		
			setValue (_options.value);		
		}

		// ------------------------------------
		// refresh
		// ------------------------------------			
		function refresh ()
		{
			if (_temp.elements["listview"].getItem () != null)
			{
				_temp.elements["edit"].disabled (false);
				_temp.elements["remove"].disabled (false);
			}
			else
			{
				_temp.elements["edit"].disabled (true);
				_temp.elements["remove"].disabled (true);
				_temp.elements["up"].disabled (true);
				_temp.elements["down"].disabled (true);
			}
		}
		
		// -------------------------------------------------------------------------------------------------------------------------
		// Events
		// -------------------------------------------------------------------------------------------------------------------------		
		// ------------------------------------
		// eventOnChange
		// ------------------------------------			
		function eventOnChange ()
		{
			refresh ();
			
			if (_options.onChange)
			{
				setTimeout (_options.onChange, 0);
			}		
		}
		
		// ------------------------------------
		// eventOnAdd
		// ------------------------------------			
		function eventOnAdd ()
		{
			var ondone = 	function (field)
					{
						_temp.elements["listview"].addItem (field);					
					};
							
			sorentoUI.sCMS.modal.newField ({onDone: ondone});
		}

		// ------------------------------------
		// eventOnEdit
		// ------------------------------------			
		function eventOnEdit ()
		{
			var ondone =	function (field)
					{
						_temp.elements["listview"].setItem (field);	
					};

			sorentoUI.sCMS.modal.editField ({field: _temp.elements["listview"].getItem (), onDone: ondone});
		}
		
		// ------------------------------------
		// eventOnRemove
		// ------------------------------------			
		function eventOnRemove ()
		{
			_temp.elements["listview"].removeItem ();
		}	
		
		// -------------------------------------------------------------------------------------------------------------------------
		// Get/Set
		// -------------------------------------------------------------------------------------------------------------------------		
		// ------------------------------------
		// getValue
		// ------------------------------------			
		function getValue ()
		{
			if (_temp.initialized)
			{
				var result = new Array ();
				var data = _temp.elements["listview"].getItems ();
				for (var index in data)
				{
					result[index] = new Array ();
					result[index]["id"] = data[index]["id"]; 
					result[index]["type"] = data[index]["type"];
					result[index]["name"] = data[index]["name"];
					result[index]["sort"] = index;	
					result[index]["options"] = data[index]["options"];
				}			
				
				return result;
			}
			else
			{
				return _options.value;
			}		
		}
		
		// ------------------------------------
		// setValue
		// ------------------------------------			
		function setValue (value)
		{
			if (_temp.initialized)
			{
				if (value != null)
				{
					_temp.elements["listview"].setItems (value);
				}			
			}
			else
			{
				_options.value = value;
			}
		}
	}
		
	sorentoUI.sCMS.fields = function (options)
	{		
		_options = options;
		_temp =	{initalized: false, 
			 id: SNDK.tools.newGuid (),
			 elements: new Array (),
			 staticFields: new Array ()
			};

		_temp.elements = new Array ();
		_temp.elements["string"] = new Array ();
		_temp.elements["text"] = new Array ();				
		_temp.elements["image"] = new Array ();
		_temp.elements["liststring"] = new Array ();					
		_temp.elements["listimage"] = new Array ();
		_temp.elements["listpage"] = new Array ();	
		
		this.getFields = getFields;
				
		window.onDomReady (init);
	
		function init ()
		{
			_temp.elements["container"] = SNDK.tools.newElement ("div", {appendTo: _options.appendTo});
										
			for (var index in _options.fields)
			{
				createField ({type: _options.fields[index]["type"], field: _options.fields[index]});
			}											
			
			_temp.initalized = true;
		}	
		
		function tinyMCELink (options)
		{					
			var collect =		function ()
						{
							var result = elements["link"].value ()							
							return result;
						};
						
			var choosepage =	function ()
						{
							var onselect =	function (page)
									{
										elements["link"].value (page["path"]);
									};
									
							parent.sorentoUI.modalPageChooser ({title: "Select page", onSelect: onselect});
						};									
						
			var done =		function ()
						{
							options.onDone (collect ());
							modalwindow.dispose ();
						};
														
			// Create MODALWINDOW
			var modalwindow = new parent.sorentoUI.modalWindow ("Edit", "Insert link");
							
			// Create MODALWINDOW content.
			var elements = new Array ();

			// LINK
			elements["linkcontainer"] = modalwindow.addControl ({type: "empty", label: "Link :"});			
			elements["link"] = new SNDK.SUI.textbox ({appendTo: elements["linkcontainer"], width: "501px", value: options.value, focus: true, onKeyUp: null});
			elements["linkbutton"] = new SNDK.SUI.button ({appendTo: elements["linkcontainer"], label: "&raquo;", width: "30px", onClick: choosepage});

			// TARGET
//			elements["target"] = modalwindow.addControl ({type: "dropbox", label: "Target :"});
																						
			// BUTTONS
			elements["add"] = modalwindow.addButton ({label: "Apply", disabled: false, onClick: done});
			elements["close"] = modalwindow.addButton ({label: "Close", onClick: modalwindow.dispose});
							
			modalwindow.show ();
		}
		
		function createField (options)
		{
			options["appendTo"] = _temp.elements["container"];
			options["onRefresh"] = _options.onRefresh;
			
			switch (options.type)
			{
				// ---------------------------------------------------
				// SEPERATOR
				// ---------------------------------------------------
				case "seperator":
				{						
					SNDK.tools.newElement ("div", {className: "UIElementSeperator", appendTo: options.appendTo});
//					_temp.staticFields[_temp.staticFields.length] = options.field;
					break;
				}

				// ---------------------------------------------------
				// STRING
				// ---------------------------------------------------
				case "string":
				{		
//					_temp.elements.string[_temp.elements.string.length] = sorentoUI.addControl ({type: "textbox", label: options.field["name"] +" :", appendTo: options.appendTo}, {value: options.field["data"], tag: options.field["id"], width: sorentoUI.elementDefaultWidth, onKeyUp: options.onRefresh});
					_temp.elements[options.field["id"]] = sorentoUI.addControl ({type: "textbox", label: options.field["name"] +" :", appendTo: options.appendTo}, {value: options.field["data"], tag: options.field["id"], width: sorentoUI.elementDefaultWidth, onKeyUp: options.onRefresh});

					break;
				}											

				// ---------------------------------------------------
				// TEXT
				// ---------------------------------------------------
				case "text":
				{		
					var providerconfig = {};			
					providerconfig.theme = "advanced";
					providerconfig.plugins = "table,paste";
					providerconfig.theme_advanced_toolbar_location = "top";
					providerconfig.theme_advanced_toolbar_align = "left";
					providerconfig.theme_advanced_buttons1 = "bold,italic,underline,|,justifyleft, justifycenter, justifyright, justifyfull,|,formatselect,removeformat,|,numlist,bullist,|,undo|,link,unlink";
					providerconfig.theme_advanced_buttons2 = "";
					providerconfig.theme_advanced_buttons3 = "";
					providerconfig.theme_advanced_blockformats = "h1,h2,h3,h4,h5,h6,blockquote";
					providerconfig.paste_auto_cleanup_on_paste = true;
					providerconfig.paste_remove_spans = true;
					providerconfig.paste_remove_styles = true;
					providerconfig.paste_remove_styles_if_webkit = true;
					providerconfig.paste_strip_class_attributes = "mso";
					providerconfig.convert_urls = false;
					providerconfig.execcommand_callback = 	function (id, element, command, ui, value) 
										{   
											return sorentoUI.tinyMCE.execcommand_callback ({id: id, element: element, command: command, ui: ui, value: value, callback: tinyMCELink});
										};

//					_temp.elements.text[_temp.elements.text.length] = sorentoUI.addControl ({type: "textarea", label: options.field["name"] +" :", appendTo: options.appendTo}, {value: options.field["data"], tag: options.field["id"], width: sorentoUI.elementDefaultWidth, height: "400px", provider: "tinymce", providerConfig: providerconfig, onKeyUp: options.onRefresh});
					_temp.elements[options.field["id"]] = sorentoUI.addControl ({type: "textarea", label: options.field["name"] +" :", appendTo: options.appendTo}, {value: options.field["data"], tag: options.field["id"], width: sorentoUI.elementDefaultWidth, height: "400px", provider: "tinymce", providerConfig: providerconfig, onChange: options.onRefresh});



					break;
				}

				// ---------------------------------------------------
				// IMAGEworked
				// ---------------------------------------------------
				case "image":
				{		
					var element = sorentoUI.addControl ({type: "iconview", label: options.field["name"] +" :", appendTo: options.appendTo}, {tag: options.field["id"], width: "117px", height: "102px", readOnly: true, onChange: options.onRefresh});
																																	
					var div5 = SNDK.tools.newElement ("div", {className: "UIElement", appendTo: options.appendTo});
					var div6 = SNDK.tools.newElement ("div", {className: "UIElementText", appendTo: div5});
					var div7 = SNDK.tools.newElement ("div", {className: "UIElementControl", appendTo: div5});
		
					var buttonadd = new SNDK.SUI.button ({appendTo: div7, label: "Select image", stylesheet: "SUIButtonSmall", width: "111px"});								
					buttonadd.onClick (	function ()
								{		
									if (element.count () > 0)
									{
										element.clear ();
										buttonadd.label ("Select image");																																				
									}
									else
									{
										var onadd = 	function (media) 
												{ 
													buttonadd.label ("Clear");																						
													element.addItem ([media["id"], "/administration/cache/thumbnails/" + media["id"] +".jpg"]);
												};
							
										parent.sorentoUI.modalMediaChooser ({type: "image", title: "Select image", path: "/media/content/%%FILENAME%%%%EXTENSION%%", mimetypes: "image/jpeg;image/png;image/gif", status: "public", mediatransformations: options.field["options"]["mediatransformations"], postUploadScript: "media_image_thumbnail.xml",onAdd: onadd});
									}
								});
							
					try
					{
						if (options.field["data"] != "")
						{
							var media = sorentoLib.media.load (options.field["data"]);
							element.addItem ([media["id"], "/administration/cache/thumbnails/" + media["id"] +".jpg"]);
							buttonadd.label ("Clear");
						}																
					} 
					catch (error)
					{					
					}
																								
//					_temp.elements.image[_temp.elements.image.length] = element;
					_temp.elements[options.field["id"]] = element;

					break;
				}
			
				// ---------------------------------------------------
				// LISTSTRING
				// ---------------------------------------------------
				case "liststring":
				{		
					var update = 	function () 
							{			
								if (element.getItem () != null)
								{											
									edit.disabled (false);											
									remove.disabled (false);
								}
								else
								{
									edit.disabled (true);
									remove.disabled (true);
								}
									
								options.onRefresh ();
							};	
							
					var add =	function ()
							{												
								var update = 	function ()
										{
											if ((elements["string"].value () != ""))
											{
												elements["add"].disabled (false)
											}
											else
											{
											elements["add"].disabled (true)
											}
										};
										
								var add = 	function ()
										{								
											element.addItem ({content: elements["string"].value ()});
											modalwindow.dispose ();	
										};
										
								var close =	function ()
										{												
											modalwindow.dispose ();
										};
														
								// Create MODALWINDOW
								var modalwindow = new parent.sorentoUI.modalWindow ("Edit", "Add string");
												
								// Create MODALWINDOW content.
								var elements = new Array ();
			
								// NAME
								elements["string"] = modalwindow.addControl ({type: "textbox", label: "String :", value: "", focus: true, onKeyUp: update});
																											
								// BUTTONS
								elements["add"] = modalwindow.addButton ({label: "Add", disabled: true, onClick: add});		
								elements["close"] = modalwindow.addButton ({label: "Close", onClick: close});
												
								modalwindow.show ();				
							};
						
					var edit =	function ()
							{
								var item = element.getItem ();
								
								var update = 	function ()
										{
											if ((elements["string"].value () != ""))
											{
												elements["apply"].disabled (false)
											}
											else
											{
												elements["apply"].disabled (true)
											}
										};
									
								var apply_ = 	function ()
										{												
											item["content"] = elements["string"].value ();
											element.setItem (item);
							
											modalwindow.dispose ();							
										};
									
								var close = 	function ()
										{
											modalwindow.dispose ();								
										};									
					
								// Create MODALWINDOW
								var modalwindow = new parent.sorentoUI.modalWindow ("Edit", "Add string");
											
								// Create MODALWINDOW content.
								var elements = new Array ();
		
								// STRING
								elements["string"] = modalwindow.addControl ({type: "textbox", label: "String :", value: item["content"], focus: true, onKeyUp: update})
																										
								// APPLY
								elements["apply"] = modalwindow.addButton ({label: "Apply", disabled: true, onClick: apply_});
		
								// CLOSE
								elements["close"] = modalwindow.addButton ({label: "Close", onClick: close});
								modalwindow.show ();										
							};
						
					var remove =	function ()
							{
								element.removeItem ();
							};
			
					var columns = new Array ();
					columns[0] = {title: "Content", tag: "content", width: "400px", visible: true};							

					var element = sorentoUI.addControl ({type: "listview", label: options.field["name"] +" :", appendTo: options.appendTo}, {columns: columns, tag: options.field["id"], width: sorentoUI.elementDefaultWidth, height: "102px", onChange: update});
				
					var items = new Array ();
					var strings = options.field["data"].split (";");							
					for (var i = 0; i < strings.length; i++)
					{					
						if (strings[i] != "")
						{
							items[items.length] = {content: [strings[i]]};
						}
					}							
				
					element.setItems (items)
				
					var div5 = SNDK.tools.newElement ("div", {className: "UIElement", appendTo: options.appendTo});
					var div6 = SNDK.tools.newElement ("div", {className: "UIElementText", appendTo: div5});							
					var div7 = SNDK.tools.newElement ("div", {className: "UIElementControl", appendTo: div5});

					var add = new SNDK.SUI.button ({appendTo: div7, label: "Add", stylesheet: "SUIButtonSmall", width: "55px", onClick: add});
					var edit = new SNDK.SUI.button ({appendTo: div7, label: "Edit", disabled: true, stylesheet: "SUIButtonSmall", width: "55px", onClick: edit});							
					var remove = new SNDK.SUI.button ({appendTo: div7, label: "Remove", disabled: true, stylesheet: "SUIButtonSmall", width: "55px", onClick: remove});
			
					//_temp.elements.liststring[_temp.elements.liststring.length] = element;
					_temp.elements[options.field["id"]] = element;					
					break;
				}							
			
				// ---------------------------------------------------
				// LISTIMAGE
				// ---------------------------------------------------
				case "listimage":
				{
					var update = 	function () 
							{			
								if (element.selectedIndex () != -1)
								{											
									remove.disabled (false);
								}
								else
								{
									remove.disabled (true);
								}
								
								options.onRefresh ();
							};
						
					var add = 	function ()
							{																	
								var onadd = function (media) { element.addItem ([media["id"], "/administration/cache/thumbnails/" + media["id"] +".jpg"]);};							
								parent.sorentoUI.modalMediaChooser ({title: "Select image", path: "/media/content/%%FILENAME%%%%EXTENSION%%", mimetypes: "image/jpeg;image/png;image/gif", status: "public", mediatransformations: options.field["options"]["mediatransformations"], postUploadScript: "media_image_thumbnail.xml",onAdd: onadd});
							};
						
					var remove =	function ()
							{
								element.removeItem ();
							};
			
					var element = sorentoUI.addControl ({type: "iconview", label: options.field["name"] +" :", appendTo: options.appendTo}, {tag: options.field["id"], width: sorentoUI.elementDefaultWidth, height: "204px", onChange: update});
								
					var mediaids = options.field["data"].split (";");
					for (var index = 0; index < mediaids.length; index++)
					{
						try
						{
							if (mediaids[index] != "")
							{
								var media = sorentoLib.media.load (mediaids[index]);
								element.addItem ([media["id"], "/administration/cache/thumbnails/" + media["id"] +".jpg"]);
							}	
						}
						catch (error)
						{}
					}										

					var div5 = SNDK.tools.newElement ("div", {className: "UIElement", appendTo: options.appendTo});
					var div6 = SNDK.tools.newElement ("div", {className: "UIElementText", appendTo: div5});							
					var div7 = SNDK.tools.newElement ("div", {className: "UIElementControl", appendTo: div5});

					var add = new SNDK.SUI.button ({appendTo: div7, label: "Add", stylesheet: "SUIButtonSmall", width: "55px", onClick: add});
					var remove = new SNDK.SUI.button ({appendTo: div7, label: "Remove", disabled: true, stylesheet: "SUIButtonSmall", width: "55px", onClick: remove});
									
					//_temp.elements.listimage[_temp.elements.listimage.length] = element;
					_temp.elements[options.field["id"]] = element;					
					break;
				}
			
				// ---------------------------------------------------
				// LISTPAGE
				// ---------------------------------------------------
				case "listpage":
				{		
					var update = 	function () 
							{			
								if (element.getItem () != null)
								{											
									remove.disabled (false);
								}
								else
								{
									remove.disabled (true);
								}
							
								options.onRefresh ();
							};
						
					var add = 	function ()
							{																	
								parent.sorentoUI.modalPageChooser ({title: "Select page", onSelect: 	function (page) 
																	{	
																		element.addItem ({id: page["id"], name: page["name"]});
																	} });
							};
			
					var remove = 	function ()
							{
								element.removeItem ();
							};
			
					var columns = new Array ();
					columns[0] = {title: "Id", tag: "id", visible: false};
					columns[1] = {title: "Name", tag: "name", width: "400px", visible: true};							

					var element = sorentoUI.addControl ({type: "listview", label: options.field["name"] +" :", appendTo: options.appendTo}, {columns: columns, tag: options.field["id"], width: sorentoUI.elementDefaultWidth, height: "200px", onChange: update});
			
					var items = new Array ();
					var pageids = options.field["data"].split (";");
					for (var i = 0; i < pageids.length; i++)
					{
						if (pageids[i] != "")
						{
							try
							{
								var page1 = sCMS.page.load (pageids[i]);
								items[items.length] = {id: page1["id"], name: page1["name"]}
	//						element.addItem ([page1["id"],page1["name"]]);
							}
							catch (error)
							{}
						}
					}												
				
					element.setItems (items);
				
					var div5 = SNDK.tools.newElement ("div", {className: "UIElement", appendTo: options.appendTo});
					var div6 = SNDK.tools.newElement ("div", {className: "UIElementText", appendTo: div5});							
					var div7 = SNDK.tools.newElement ("div", {className: "UIElementControl", appendTo: div5});

					var add = new SNDK.SUI.button ({appendTo: div7, label: "Add", stylesheet: "SUIButtonSmall", width: "55px", onClick: add});
					var remove = new SNDK.SUI.button ({appendTo: div7, label: "Remove", disabled: true, stylesheet: "SUIButtonSmall", width: "55px", onClick: remove});
															
					//_temp.elements.listpage[_temp.elements.listpage.length] = element;
					_temp.elements[options.field["id"]] = element;					
					break;
				}
			}								
		}	
		
		function collect ()
		{
			var fields = new Array ();
			
			for (var index in _options.fields)
			{
				var id = _options.fields[index]["id"];
				
				var field = new Array ();
				field["id"] = _options.fields[index]["id"];
				field["name"] = _options.fields[index]["name"];
				field["type"] = _options.fields[index]["type"];
				
				switch (_options.fields[index]["type"])
				{			
					// SEPERATOR	
					case "seperator":
					{
						field = _options.fields[index];
						break;
					}
					
					// STRING
					case "string":
					{
						var element = _temp.elements[id];
						field["data"] = element.value ();						
						break;
					}

					// TEXT
					case "text":
					{
						var element = _temp.elements[id];
						field["data"] = element.value ();
						break;
					}

					// IMAGE
					case "image":
					{
						var element = _temp.elements[id];

						if (element.count () > 0)
						{
							field["data"] = element.items()[0][0];
						}
						else
						{
							field["data"] = "";
						}
				
						break;
					}

					// LISTSTRING
					case "liststring":
					{
						var element = _temp.elements[id];
						var items = element.getItems ();

						var strings = "";
						for (var index2 in items)
						{
							strings += items[index2]["content"] +";";
						}
									
						field["data"] = SNDK.tools.trimEnd (strings, ";");

						break;
					}

					// LISTIMAGE
					case "listimage":
					{
						var element = _temp.elements[id];
						var items = element.items ();

						var mediaids = "";
						for (var index2 in items)
						{
							mediaids += items[index2][0] +";";
						}
			
						field["data"] = SNDK.tools.trimEnd (mediaids, ";");
					
						break;
					}
					
					// LISTPAGE
					case "listpage":
					{
						var element = _temp.elements[id];
						var items = element.getItems ();
												
						var pageids = "";
						for (var index2 in items)
						{
							pageids += items[index2]["id"] +";"
						}
					
						field["data"] = SNDK.tools.trimEnd (pageids, ";");
									
						break;
					}					
				}
				
				fields[fields.length] = field;				
			}		
		
			return fields;	
		}
		
		function getFields ()
		{
			return collect ();
		}
	}
		
	sorentoUI.sCMS.modal.newField = function (options)
	{
		// Create MODALWINDOW
		var modalwindow = new parent.sorentoUI.modalWindow ("Edit", "Add field");
														
		// Create MODALWINDOW content.
		var elements = new Array ();
					
		// NAME
		elements["name"] = modalwindow.addControl ({type: "textbox", label: "Name :", focus: true});
		elements["name"].onKeyUp (	function ()
						{
							if ((elements["name"].value () != "") && (elements["type"].selectedItem () != null))
							{
								elements["add"].disabled (false)
							}
							else
							{
								elements["add"].disabled (true)
							}
						});
								
		// TYPE
		elements["type"] = modalwindow.addControl ({type: "dropbox", label: "Type :"});
		elements["type"].addItem (["seperator", "Seperator"]);
		elements["type"].addItem (["string", "String"]);
		elements["type"].addItem (["liststring", "List of strings"]);
		elements["type"].addItem (["text", "Text"]);
		elements["type"].addItem (["image", "Image"]);
		elements["type"].addItem (["listimage", "List of images"]);
		elements["type"].addItem (["listpage", "List of pages"]);
					
		elements["type"].onChange (	function ()
						{
							if ((elements["name"].value () != "") && (elements["type"].selectedItem () != null))
							{
								elements["add"].disabled (false)
							}
							else
							{
								elements["add"].disabled (true)
							}
						});
																		
		// ADD
		elements["add"] = modalwindow.addButton ({label: "Add", disabled: true});																			
		elements["add"].onClick (	function ()
						{		
						  	// TODO: ID should not be added to the list, this needs to be fixed on server side.					
							options.onDone ({id: "", name: elements["name"].value (),  type: elements["type"].selectedItem ()[0]});
						
							modalwindow.dispose ();	
						});
		
		elements["close"] = modalwindow.addButton ({label: "Close"});
		elements["close"].onClick (	function ()
						{
							modalwindow.dispose ();
						});

		modalwindow.show ();
		
	}
	
	sorentoUI.sCMS.modal.editField  = function (options)
	{
		// Create MODALWINDOW
		var modalwindow = new parent.sorentoUI.modalWindow ("Edit", "Edit field");
														
		// Create MODALWINDOW content.
		var elements = new Array ();

		var	refresh =	function ()	
					{
						if (sorentoUI.compareDefaults ({array1: options.field, array2: collect ()}))
						{
							elements["apply"].disabled (false)						
						}
						else
						{
							elements["apply"].disabled (true)
						}
					};
					
		var	collect =	function ()
					{
						var field = new Array ();
						field["id"] = options.field["id"];
						field["name"] = elements["name"].value ();
						field["type"] = elements["type"].selectedItem ()[0];
						field["options"] = {};
					
						if (options.field["type"] == "image" || options.field["type"] == "listimage")
						{
							field["options"]["mediatransformations"] = elements["mediatransformations"].getValue ();
						}
					
						return field;
					};					
									
		// NAME
		elements["name"] = modalwindow.addControl ({type: "textbox", label: "Name :", value: options.field["name"], onKeyUp: refresh, disabled: true});
		
		// TYPE
		elements["type"] = modalwindow.addControl ({type: "dropbox", label: "Type :", disabled: true});
		elements["type"].addItem (["seperator", "Seperator"]);
		elements["type"].addItem (["string", "String"]);
		elements["type"].addItem (["listimage", "List of strings"]);
		elements["type"].addItem (["text", "Text"]);
		elements["type"].addItem (["image", "Image"]);
		elements["type"].addItem (["listimage", "List of images"]);
		elements["type"].addItem (["listpage", "List of pages"]);					
		elements["type"].selectItemByValue (options.field["type"]);

		if (options.field["type"] == "image" || options.field["type"] == "listimage")
		{
//			refresh =	function ()	
//					{
//						if ((elements["name"].value () != "") && (elements["type"].selectedItem () != null))
//						{
//							elements["apply"].disabled (false)
//						}
//						else
//						{
//							elements["apply"].disabled (true)
//						}					
//					};
								
//			collect =	function ()
//					{
						
//						var field = new Array ();
//						field["id"] = options.field["id"];
//						field["name"] = elements["name"].value ();
//						field["type"] = elements["type"].selectedItem ()[0];
//					
//						var mediatransformations = elements["mediatransformations"].getItems ();
//						var mediatransformationids = "";								
//						for (index in mediatransformations)
//						{
//							mediatransformationids += mediatransformations[index]["id"] +";";
//						}
//					
//						field["options"] = {};
//						field["options"]["mediatransformations"] = SNDK.tools.trimEnd (mediatransformationids, ";");
//					
//						return field;
//					};	

			elements["test"] = modalwindow.addControl ({type: "empty"});		
			elements["mediatransformations"] = new sorentoUI.media.transformations ({value: options.field["options"]["mediatransformations"], appendTo: elements["test"], onChange: refresh});


//								
//			var add = 	function () 
//					{
//						var onselect = 	function (item) 
//								{	
//									var columns = new Array ();
//									columns[0] = {title: "Id", tag: "id", visible: false};					
//									columns[1] = {title: "Title", tag: "title", width: "600px", visible: true};
//								
//									elements["mediatransformations"].addItem ([item["id"], item["title"]]);
//								};
//						
//						parent.sorentoUI.modalChooser ({icon: "Edit", title: "Select mediatransformation", buttonLabel: "Add", columns: columns, items: sorentoLib.mediaTransformation.list (), onSelect: onselect});
//					};
//							
//			var remove =	function ()
//					{
//						elements["mediatransformations"].removeItem ();
//					};									
//								
//			// MEDIATRANSFORMATIONS
//			var columns = new Array ();
//			columns[0] = {title: "Id", tag: "id", visible: false};					
//			columns[1] = {title: "Title", tag: "title", width: "600px", visible: true};									
//
//			elements["mediatransformations"] = modalwindow.addControl ({type: "listview", label: "MediaT... :", columns: columns, unique: "id"});
//			elements["mediatransformations"].onChange (refresh);
//		
//			if (options.field["options"] != null)	
//			{
//				if (options.field["options"]["mediatransformations"] != null)
//				{
//					var mediatransformationids = options.field["options"]["mediatransformations"].split (";");
//					for (var index = 0; index < mediatransformationids.length; index++)
//					{
//						try
//						{
//							if (mediatransformationids[index] != "")
//							{
//								var mediatransformation = sorentoLib.mediaTransformation.load (mediatransformationids[index]);
//								elements["mediatransformations"].addItem ([mediatransformation["id"], mediatransformation["title"]]);
//							}	
//						}
//						catch (error)
//						{}
//					}
//				}
//			}
//					
//			elements["buttons"] = modalwindow.addControl ({type: "empty", label: ""});
//			elements["mediatransformationadd"] = new SNDK.SUI.button ({appendTo: elements["buttons"], label: "Add", stylesheet: "SUIButtonSmall", width: "50px", onClick: add});
//			elements["mediatransformationremove"] = new SNDK.SUI.button ({appendTo: elements["buttons"], label: "Remove", stylesheet: "SUIButtonSmall", width: "50px", disabled: true, onClick: remove});
		}

					

																	
		// APPLY
		elements["apply"] = modalwindow.addButton ({label: "Apply", disabled: true});
		elements["apply"].onClick (	function ()
						{	
							options.onDone (collect ());
							modalwindow.dispose ();	
						});
		
		elements["close"] = modalwindow.addButton ({label: "Close"});
		elements["close"].onClick (	function ()
						{
							modalwindow.dispose ();
						});	

		modalwindow.show ();									
	}




	sorentoUI.sCMS.getFieldTypes = function (onlydatafields)
	{
		var result = new Array ();
		
		if (!onlydatafields && onlydatafields != null)
		{
			result[result.length] = "Seperator";
		}		
		
		result[result.length] = ["string", "String"];
		result[result.length] = ["text", "Text"];
		result[result.length] = ["image", "Image"];
		result[result.length] = ["liststring", "List of Strings"];
		result[result.length] = ["listimage", "List of Images"];
		result[result.length] = ["listpage", "List of Pages"];
		
		return result;
	}

	sorentoUI.sCMS.newFieldElement = function (options)
	{
		switch (options.type)
		{
			// ---------------------------------------------------
			// SEPERATOR
			// ---------------------------------------------------
			case "seperator":
			{						
				SNDK.tools.newElement ("div", {className: "UIElementSeperator", appendTo: options.appendTo});
				break;
			}

			// ---------------------------------------------------
			// STRING
			// ---------------------------------------------------
			case "string":
			{		
				return sorentoUI.addControl ({type: "textbox", label: options.field["name"] +" :", appendTo: options.appendTo}, {value: options.field["data"], tag: options.field["id"], width: sorentoUI.elementDefaultWidth, onKeyUp: options.onRefresh});
			}											

			// ---------------------------------------------------
			// TEXT
			// ---------------------------------------------------
			case "text":
			{		
				var providerconfig = {};			
				providerconfig.theme = "advanced";
				providerconfig.plugins = "table,paste";
				providerconfig.theme_advanced_toolbar_location = "top";
				providerconfig.theme_advanced_toolbar_align = "left";
				providerconfig.theme_advanced_buttons1 = "bold,italic,underline,|,justifyleft, justifycenter, justifyright, justifyfull,|,formatselect,removeformat,|,numlist,bullist,|,undo";
				providerconfig.theme_advanced_buttons2 = "";
				providerconfig.theme_advanced_buttons3 = "";
				providerconfig.theme_advanced_blockformats = "h1,h2,h3,h4,h5,h6,blockquote";
				providerconfig.paste_auto_cleanup_on_paste = true;
				providerconfig.paste_remove_spans = true;
				providerconfig.paste_remove_styles = true;
				providerconfig.paste_remove_styles_if_webkit = true;
				providerconfig.paste_strip_class_attributes = "mso";

				return sorentoUI.addControl ({type: "textarea", label: options.field["name"] +" :", appendTo: options.appendTo}, {value: options.field["data"], tag: options.field["id"], width: sorentoUI.elementDefaultWidth, height: "400px", provider: "tinymce", providerConfig: providerconfig, onKeyUp: options.onRefresh});
			}

			// ---------------------------------------------------
			// IMAGE
			// ---------------------------------------------------
			case "image":
			{		
				var element = sorentoUI.addControl ({type: "iconview", label: options.field["name"] +" :", appendTo: options.appendTo}, {tag: options.field["id"], width: "117px", height: "102px", readOnly: true, onChange: options.onRefresh});
																																	
				var div5 = SNDK.tools.newElement ("div", {className: "UIElement", appendTo: options.appendTo});
				var div6 = SNDK.tools.newElement ("div", {className: "UIElementText", appendTo: div5});
				var div7 = SNDK.tools.newElement ("div", {className: "UIElementControl", appendTo: div5});
		
				var buttonadd = new SNDK.SUI.button ({appendTo: div7, label: "Select image", stylesheet: "SUIButtonSmall", width: "111px"});								
				buttonadd.onClick (	function ()
							{		
								if (element.count () > 0)
								{
									element.clear ();
									buttonadd.label ("Select image");																																				
								}
								else
								{
									var onadd = 	function (media) 
											{ 
												buttonadd.label ("Clear");																						
												element.addItem ([media["id"], "/administration/cache/thumbnails/" + media["id"] +".jpg"]);
											};
							
									parent.sorentoUI.modalMediaChooser ({title: "Select image", path: "/media/content/%%FILENAME%%%%EXTENSION%%", mimetypes: "image/jpeg;image/png;image/gif", status: "public", mediatransformations: options.field["options"]["mediatransformations"], postUploadScript: "media_image_thumbnail.xml",onAdd: onadd});
								}
							});
							
				try
				{
					if (options.field["data"] != "")
					{
						var media = sorentoLib.media.load (options.field["data"]);
						element.addItem ([media["id"], "/administration/cache/thumbnails/" + media["id"] +".jpg"]);
						buttonadd.label ("Clear");
					}																
				} 
				catch (error)
				{					
				}
																								
				return element;
			}
			
			// ---------------------------------------------------
			// LISTSTRING
			// ---------------------------------------------------
			case "liststring":
			{		
				var update = 	function () 
						{			
							if (element.getItem () != null)
							{											
								edit.disabled (false);											
								remove.disabled (false);
							}
							else
							{
								edit.disabled (true);
								remove.disabled (true);
							}
								
							options.onRefresh ();
						};	
						
				var add =	function ()
						{												
							var update = 	function ()
									{
										if ((elements["string"].value () != ""))
										{
											elements["add"].disabled (false)
										}
										else
										{
											elements["add"].disabled (true)
										}
									};
									
							var add = 	function ()
									{								
										element.addItem ({content: elements["string"].value ()});
										modalwindow.dispose ();	
									};
									
							var close =	function ()
									{												
										modalwindow.dispose ();
									};
													
							// Create MODALWINDOW
							var modalwindow = new parent.sorentoUI.modalWindow ("Edit", "Add string");
											
							// Create MODALWINDOW content.
							var elements = new Array ();
		
							// NAME
							elements["string"] = modalwindow.addControl ({type: "textbox", label: "String :", value: "", focus: true, onKeyUp: update});
																										
							// BUTTONS
							elements["add"] = modalwindow.addButton ({label: "Add", disabled: true, onClick: add});		
							elements["close"] = modalwindow.addButton ({label: "Close", onClick: close});
											
							modalwindow.show ();				
						};
						
				var edit =	function ()
						{
							var item = element.getItem ();
							
							var update = 	function ()
									{
										if ((elements["string"].value () != ""))
										{
											elements["apply"].disabled (false)
										}
										else
										{
											elements["apply"].disabled (true)
										}
									};
									
							var apply_ = 	function ()
									{												
										item["content"] = elements["string"].value ();
										element.setItem (item);
						
										modalwindow.dispose ();							
									};
									
							var close = 	function ()
									{
										modalwindow.dispose ();								
									};									
					
							// Create MODALWINDOW
							var modalwindow = new parent.sorentoUI.modalWindow ("Edit", "Add string");
											
							// Create MODALWINDOW content.
							var elements = new Array ();
		
							// STRING
							elements["string"] = modalwindow.addControl ({type: "textbox", label: "String :", value: item["content"], focus: true, onKeyUp: update})
																										
							// APPLY
							elements["apply"] = modalwindow.addButton ({label: "Apply", disabled: true, onClick: apply_});
		
							// CLOSE
							elements["close"] = modalwindow.addButton ({label: "Close", onClick: close});
							modalwindow.show ();										
						};
						
				var remove =	function ()
						{
							element.removeItem ();
						};
			
				var columns = new Array ();
				columns[0] = {title: "Content", tag: "content", width: "400px", visible: true};							

				var element = sorentoUI.addControl ({type: "listview", label: options.field["name"] +" :", appendTo: options.appendTo}, {columns: columns, tag: options.field["id"], width: sorentoUI.elementDefaultWidth, height: "102px", onChange: update});
				
				var items = new Array ();
				var strings = options.field["data"].split (";");							
				for (var i = 0; i < strings.length; i++)
				{					
					if (strings[i] != "")
					{
						items[items.length] = {content: [strings[i]]};
					}
				}							
				
				element.setItems (items)
				
				var div5 = SNDK.tools.newElement ("div", {className: "UIElement", appendTo: options.appendTo});
				var div6 = SNDK.tools.newElement ("div", {className: "UIElementText", appendTo: div5});							
				var div7 = SNDK.tools.newElement ("div", {className: "UIElementControl", appendTo: div5});

				var add = new SNDK.SUI.button ({appendTo: div7, label: "Add", stylesheet: "SUIButtonSmall", width: "55px", onClick: add});
				var edit = new SNDK.SUI.button ({appendTo: div7, label: "Edit", disabled: true, stylesheet: "SUIButtonSmall", width: "55px", onClick: edit});							
				var remove = new SNDK.SUI.button ({appendTo: div7, label: "Remove", disabled: true, stylesheet: "SUIButtonSmall", width: "55px", onClick: remove});
			
				return element;
			}						
			
			// ---------------------------------------------------
			// LISTIMAGE
			// ---------------------------------------------------
			case "listimage":
			{
				var update = 	function () 
						{			
							if (element.selectedIndex () != -1)
							{											
								remove.disabled (false);
							}
							else
							{
								remove.disabled (true);
							}
								
							options.onRefresh ();
						};
						
				var add = 	function ()
						{																	
							var onadd = function (media) { element.addItem ([media["id"], "/administration/cache/thumbnails/" + media["id"] +".jpg"]);};							
							parent.sorentoUI.modalMediaChooser ({title: "Select image", path: "/media/content/%%FILENAME%%%%EXTENSION%%", mimetypes: "image/jpeg;image/png;image/gif", status: "public", mediatransformations: options.field["options"]["mediatransformations"], postUploadScript: "media_image_thumbnail.xml",onAdd: onadd});
						};
						
				var remove =	function ()
						{
							element.removeItem ();
						};
			
				var element = sorentoUI.addControl ({type: "iconview", label: options.field["name"] +" :", appendTo: options.appendTo}, {tag: options.field["id"], width: sorentoUI.elementDefaultWidth, height: "102px", onChange: update});
								
				var mediaids = options.field["data"].split (";");
				for (var index = 0; index < mediaids.length; index++)
				{
					try
					{
						if (mediaids[index] != "")
						{
							var media = sorentoLib.media.load (mediaids[index]);
							element.addItem ([media["id"], "/administration/cache/thumbnails/" + media["id"] +".jpg"]);
						}	
					}
					catch (error)
					{}
				}										

				var div5 = SNDK.tools.newElement ("div", {className: "UIElement", appendTo: options.appendTo});
				var div6 = SNDK.tools.newElement ("div", {className: "UIElementText", appendTo: div5});							
				var div7 = SNDK.tools.newElement ("div", {className: "UIElementControl", appendTo: div5});

				var add = new SNDK.SUI.button ({appendTo: div7, label: "Add", stylesheet: "SUIButtonSmall", width: "55px", onClick: add});
				var remove = new SNDK.SUI.button ({appendTo: div7, label: "Remove", disabled: true, stylesheet: "SUIButtonSmall", width: "55px", onClick: remove});
									
				return element;
			}
			
			// ---------------------------------------------------
			// LISTPAGE
			// ---------------------------------------------------
			case "listpage":
			{		
				var update = 	function () 
						{			
							if (element.getItem () != null)
							{											
								remove.disabled (false);
							}
							else
							{
								remove.disabled (true);
							}
							
							options.onRefresh ();
						};
						
				var add = 	function ()
						{																	
							parent.sorentoUI.modalPageChooser ({title: "Select page", onSelect: 	function (page) 
																{	
																	element.addItem ({id: page["id"], name: page["name"]});
																} });
						};
			
				var remove = 	function ()
						{
							element.removeItem ();
						};
			
				var columns = new Array ();
				columns[0] = {title: "Id", tag: "id", visible: false};
				columns[1] = {title: "Name", tag: "name", width: "400px", visible: true};							

				var element = sorentoUI.addControl ({type: "listview", label: options.field["name"] +" :", appendTo: options.appendTo}, {columns: columns, tag: options.field["id"], width: sorentoUI.elementDefaultWidth, height: "200px", onChange: update});
			
				var items = new Array ();
				var pageids = options.field["data"].split (";");
				for (var i = 0; i < pageids.length; i++)
				{
					if (pageids[i] != "")
					{
						try
						{
							var page1 = sCMS.page.load (pageids[i]);
							items[items.length] = {id: page1["id"], name: page1["name"]}
//						element.addItem ([page1["id"],page1["name"]]);
						}
						catch (error)
						{}
					}
				}												
				
				element.setItems (items);
				
				var div5 = SNDK.tools.newElement ("div", {className: "UIElement", appendTo: options.appendTo});
				var div6 = SNDK.tools.newElement ("div", {className: "UIElementText", appendTo: div5});							
				var div7 = SNDK.tools.newElement ("div", {className: "UIElementControl", appendTo: div5});

				var add = new SNDK.SUI.button ({appendTo: div7, label: "Add", stylesheet: "SUIButtonSmall", width: "55px", onClick: add});
				var remove = new SNDK.SUI.button ({appendTo: div7, label: "Remove", disabled: true, stylesheet: "SUIButtonSmall", width: "55px", onClick: remove});
															
				return element;
			}
		}								
	}

	sorentoUI.modalStylesheetChooser = function (options) 
	{
		var columns = new Array ();
		columns[0] = {title: "Filename", tag: "filename", visible: false};					
		columns[1] = {title: "Title", tag: "title", width: "400px", visible: true};
	
		options.columns = columns;
		options.items =	sCMS.stylesheet.list ();										
		options.buttonLabel = "Add";
	
		sorentoUI.modalChooser (options);	
	}			

	sorentoUI.modalPageChooser = function (options)
	{
		var columns = new Array ();
		columns[0] = {tag: "id", visible: false, width: "60px"};					
		columns[1] = {title: "Name", tag: "name", width: "400px", visible: true};
		columns[2] = {tag: "parentid", visible: false, width: "60px"};
	
		options.columns = columns;
		options.treeview = true;
		options.treeviewLinkColumns = "id:parentid";

		var roots = sCMS.root.list ();
		var pages = sCMS.page.list ();
		var tree = new Array ();
					
		for (index in roots)
		{
			roots[index]["type"] = "root";
			tree[tree.length] = roots[index];
		}

		for (index in pages)
		{
			pages[index]["type"] = "page";					
			tree[tree.length] = pages[index];
		}				
					
		options.items = tree;
		
		options.validate = 	function (item) 
					{
						var result = false;
						if (item["type"] == "page") result = true;
						return result;							
					}
					
		options.buttonLabel = "Add";
	
		sorentoUI.modalChooser (options);
	}	

	sorentoUI.modalPageChooser1 = function (options) 
	{
		// Init
		var _initialized = false;
		var _options = options;	
			
		var _temp =	{ currentTab: 0,				  				  
				};
				
		var _elements = new Array ();
				
		init ();

		// ------------------------------------
		// Private functions
		// ------------------------------------
		// ------------------------------------
		// init
		// ------------------------------------
		function init ()
		{
			// Create MODALWINDOW
			_modalwindow = new sorentoUI.modalWindow (_options.icon, _options.title);

			// CONTENT
			_elements["content"] = _modalwindow.contentDiv ();					

			// TABS	
//			_elements["submenu"] = SUI.tools.newElement ("div", {className: "Submenu", appendTo: _elements["content"]});
//			_elements["submenubutton1"] = SUI.tools.newElement ("div", {className: "SubmenuButton", appendTo: _elements["submenu"]});
//			_elements["submenubutton1"].onclick = function () {changeTab (1)};
//			SUI.tools.newElement ("div", {innerHTML: "Pages", className: "SubmenuButtonText", appendTo: _elements["submenubutton1"]});
			
			// SUBMENUPAGE1
//			_elements["submenupage1"] = SUI.tools.newElement ("div", {className: "SubmenuPage", display: "none", height: "300px", appendTo: _elements["content"]});
			
			// ICONVIEW
			var columns = new Array ();
			columns[0] = {title: "Id", tag: "id", visible: false};					
			columns[1] = {title: "Name", tag: "name", width: "400px", visible: true};
			
//			_elements["listviewpages"] = new SUI.listview ({columns: columns, width: "628px", height: "253px", disabled: false, appendTo: _elements["submenupage1"]});
			_elements["listviewpages"] = _modalwindow.addControl ({type: "listview", columns: columns, height: "253px"});
			_elements["listviewpages"].onChange ( function ()
								{
									if (_elements["listviewpages"].selectedIndex() != -1)
									{
										_elements["add"].disabled (false);
									}
									else
									{
										_elements["add"].disabled (true);
									}
								});
				
			var pages = sCMS.page.list ();
			for (var page in pages)
			{
				_elements["listviewpages"].addItem ([pages[page]["id"], pages[page]["name"]]);
			}						
											
			// ADD					
			_elements["add"] = _modalwindow.addButton ({label: "Add", disabled: true, onClick: eventAdd});
			
			// CLOSE	
			_elements["close"] = _modalwindow.addButton ({label: "Close", onClick: eventClose});
											
			// Done
			_initalized = true;
			
//			changeSubmenuPage ();
			_modalwindow.show ();										
		}		
		
		// ------------------------------------
		// changeSubmenuPage
		// ------------------------------------				
		function changeTab (page)
		{
			if (_options.currentSubmenuPage == null)
			{
				_options.currentSubmenuPage = 1;
			}
		
			_elements["submenupage"+ _options.currentSubmenuPage].style.display = "none";
			_elements["submenubutton"+ _options.currentSubmenuPage].className = "SubmenuButton";						
	
			if (page != null)
			{
				_options.currentSubmenuPage = page;				
			}

			triggerEvent ("resize");

			_elements["submenupage"+ _options.currentSubmenuPage].style.display = "block";
			_elements["submenubutton"+ _options.currentSubmenuPage].className = "SubmenuButton SubmenuButtonSelected";	
		}					
			
		// ------------------------------------
		// Events
		// ------------------------------------			
		// ------------------------------------
		// add
		// ------------------------------------					
		function eventAdd ()
		{
			_page = _elements["listviewpages"].selectedItem ();

			_modalwindow.dispose ();
						
			if (_options.onAdd != null)									
			{
				setTimeout (function () { _options.onAdd (_page) }, 1);
			}			
		}	
		
		// ------------------------------------
		// close
		// ------------------------------------				
		function eventClose ()
		{
			_modalwindow.dispose ();
		}		
	}
}

