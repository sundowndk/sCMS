//
// Ajax.cs
//
// Author:
//       Rasmus Pedersen <rasmus@akvaservice.dk>
//
// Copyright (c) 2010 Rasmus Pedersen
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using System;
using System.Collections.Generic;
using System.Collections;

using SorentoLib;

namespace sCMS.Addin
{
	public class Ajax : SorentoLib.Addins.IAjaxBaseClass, SorentoLib.Addins.IAjax		
	{
		#region Constructor
		public Ajax ()
		{
			base.NameSpaces.Add ("scms");
		}
		#endregion

		#region Public Methods
		new public SorentoLib.Ajax.Respons Process (SorentoLib.Session Session, string Fullname, string Method)
		{
			SorentoLib.Ajax.Respons result = new SorentoLib.Ajax.Respons ();
			SorentoLib.Ajax.Request request = new SorentoLib.Ajax.Request (Session.Request.QueryJar.Get ("data").Value);
			
			switch (Fullname.ToLower ())
			{
//				#region sCMS.Template
//				case "scms.template":
//				
//					switch (Method.ToLower ())
//					{
//						
//						case "new":
//						{
//					//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Editor) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "template.new"));
//					
//							Template template = Template.FromAjaxRequest (request);
//							template.Save ();
//					template.ToAjaxRespons (result);
//					
//					break;
//				}
//
//				case "load":
//				{
//					sCMS.Template template = Template.Load (new Guid (request.Key<string> ("id")));
//					template.ToAjaxRespons (result);
//					
//					break;
//				}
//
//						case "save":
//						{
////							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Editor) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "template.save"));
//
//							Template template = Template.FromAjaxRequest (request);
//							template.Save ();
//
//							break;
//						}
//
//						case "delete":
//						{
////							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Editor) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "template.delete"));
//
//							sCMS.Template.Delete (new Guid (request.Key<string> ("id")));
//
//							break;
//						}
//
//						case "list":
//						{
////							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "template.list"));
//
//							List<Hashtable> templates = new List<Hashtable> ();
//							foreach (Template template in Template.List ())
//							{
//								templates.Add (template.ToAjaxItem ());
//							}
//							result.Data.Add ("templates", templates);
//
//							break;
//						}
//					}
//					break;
//				#endregion

				#region sCMS.Root
				case "scms.root":
					switch (Method.ToLower ())
					{
						case "new":
						{
							result.Add (new Root (request.getValue<string> ("title")));
							break;
						}

						case "load":
						{
							result.Add (Root.Load (request.getValue<Guid> ("id")));
							break;
						}

						case "save":
						{
							request.getValue<sCMS.Root> ("scms.root").Save ();
							break;
						}

						case "delete":
						{
							Root.Delete (request.getValue<Guid> ("id"));
							break;
						}

						case "list":
						{
							result.Add (Root.List ());
							break;
						}
					}
					break;
				#endregion

//				#region sCMS.Page
//				case "scms.page":
//				{
//					switch (Method.ToLower ())
//					{
//						case "new":
//						{
//							result.Add (new Page (Template.Load (request.getValue<Guid> ("templateid"))));
//							break;
//						}
//
//						case "load":
//						{
//							result.Add (Page.Load (request.getValue<Guid> ("id")));
//							break;
//						}
//
//						case "save":
//						{
//							request.getValue<Page> ("scms.page").Save ();
//							break;
//						}
//
//						case "delete":
//						{
//							Page.Delete (request.getValue<Guid> ("id"));
//							break;
//						}
//
//						case "list":
//						{
//							result.Add (Page.List ());
//							break;
//						}
//					}
//					break;
//				}
//				#endregion

				#region sCMS.Collection
				case "scms.collection":
					switch (Method.ToLower ())
					{
						case "new":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "collection.new"));

//							Collection collection = Collection.FromAjaxRequest (request);
//							collection.ToAjaxResponse (result);
							//							collection.Save ();

							break;
						}

						case "load":
						{
							Collection collection = Collection.Load (new Guid (request.Key<string> ("id")));
//							collection.ToAjaxResponse (result);

							break;
						}

						case "save":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "collection.save"));

//							Collection collection = Collection.FromAjaxRequest (request);
//							collection.Save ();

							break;
						}

						case "delete":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "collection.delete"));

							Collection.Delete (new Guid (request.Key<string> ("id")));

							break;
						}

						case "list":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "collection.list"));

							List<Hashtable> collections = new List<Hashtable> ();
							foreach (Collection collection in Collection.List ())
							{
//								collections.Add (collection.ToAjaxItem ());
							}
							result.Data.Add ("collections", collections);

							break;
						}
					}
					break;
				#endregion

				#region sCMS.CollectionSchema
				case "scms.collectionschema":
					switch (Method.ToLower ())
					{
						case "new":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "collectionschema.new"));

//							CollectionSchema collectionschema = CollectionSchema.FromAjaxRequest (request);
//							collectionschema.Save ();
//							collectionschema.ToAjaxResponse (result);

							break;
						}

						case "load":
						{
//							CollectionSchema collectionschema = CollectionSchema.Load (new Guid (request.Key<string> ("id")));
//							collectionschema.ToAjaxResponse (result);

							break;
						}

						case "save":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "collectionschema.save"));

//							CollectionSchema collectionschema = CollectionSchema.FromAjaxRequest (request);
//							collectionschema.Save ();

							break;
						}

						case "delete":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "collectionschema.delete"));

							CollectionSchema.Delete (new Guid (request.Key<string> ("id")));

							break;
						}

						case "list":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "collectionschema.list"));

							List<Hashtable> collectionschemas = new List<Hashtable> ();
							foreach (CollectionSchema collectionschema in CollectionSchema.List ())
							{
//								collectionschemas.Add (collectionschema.ToAjaxItem ());
							}
							result.Data.Add ("collectionschemas", collectionschemas);

							break;
						}
					}
					break;
				#endregion

				#region sCMS.Stylesheet
				case "scms.stylesheet":
					switch (Method.ToLower ())
					{
						case "new":
						{
							result.Add (new Stylesheet (request.getValue<string> ("title")));
							break;
						}

						case "load":
						{
							result.Add (Stylesheet.Load (request.getValue<string> ("id")));
							break;
						}

						case "save":
						{
							request.getValue<Stylesheet> ("scms.stylesheet").Save ();
							break;
						}

						case "delete":
						{
							Stylesheet.Delete (request.getValue<string> ("id"));
							break;
						}

						case "list":
						{
							result.Add (Stylesheet.List ());
							break;
						}
					}
					break;
				#endregion
//
				#region sCMS.Global
				case "scms.global":
					switch (Method.ToLower ())
					{
						case "new":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Editor) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "global.new"));

//							Global global = Global.FromAjaxRequest (request);
//							global.Save ();
//							global.ToAjaxRespons (result);

							break;
						}

						case "load":
						{
//							Global global = Global.Load (new Guid (request.Key<string> ("id")));
//							global.ToAjaxRespons (result);

							break;
						}

						case "save":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "global.save"));

//							Global global = Global.FromAjaxRequest (request);
//							global.Save ();

							break;
						}

						case "delete":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Editor) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "global.delete"));

							Global.Delete (new Guid (request.Key<string> ("id")));

							break;
						}

						case "list":
						{
//							if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Author) throw new Exception (string.Format (sCMS.Strings.Exception.AjaxSessionPriviliges, "stylesheet.list"));

//							List<Hashtable> globals = new List<Hashtable> ();
//							foreach (Global global in Global.List ())
//							{
//								globals.Add (global.ToAjaxItem ());
//							}
//							result.Data.Add ("globals", globals);
//
							break;
						}
					}
					break;
				#endregion
			}

			return result;
		}
		#endregion
	}
}
