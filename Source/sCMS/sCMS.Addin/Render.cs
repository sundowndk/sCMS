//
// Render.cs
//
// Author:
// Rasmus Pedersen <rasmus@akvaservice.dk>
//
// Copyright (c) 2009 Rasmus Pedersen
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
using System.Collections;
using System.Collections.Generic;

using SorentoLib;

namespace sCMS.Addin
{
public class Render : SorentoLib.Addins.IRenderBaseClass, SorentoLib.Addins.IRender
{
#region Constructor
public Render ()
{
base.NameSpaces.Add ("scms");
}
#endregion

#region Public Methods
override public object Process (SorentoLib.Session Session, string Fullname, string Method, object Variable, SorentoLib.Render.Resolver.Parameters Parameters)
{
			switch (Fullname)
			{
				#region sCMS.Root
				case "scms.root":
				{
					switch (Method)
					{
						case "":
						{
							return ((sCMS.Root)Variable);
						}
							
						case "id":
						{
							return ((sCMS.Root)Variable).Id;
						}
					}
					break;
				}
				#endregion				
				
				#region sCMS.Page
				case "scms.page":
				{
					switch (Method)
					{
						case "":
						{
							return ((sCMS.Page)Variable);
						}
							
						case "id":
						{
							return ((sCMS.Page)Variable).Id;
						}
							
						case "createtimestamp":
						{
							return ((sCMS.Page)Variable).CreateTimestamp;
						}

						case "updatetimestamp":
						{
							return ((sCMS.Page)Variable).UpdateTimestamp;
						}
							
						case "title":
						{
							return ((sCMS.Page)Variable).Title;
						}

						case "path":
						{
							return ((sCMS.Page)Variable).Path;
						}

						case "template":
						{
							return ((sCMS.Page)Variable).Template;
						}
							
						case "root":
						{
							return ((sCMS.Page)Variable).Root;
						}
							
						case "childpages":
						{
							return ((sCMS.Page)Variable).ChildPages;
						}
							
						case "isparent":
						{
							return ((sCMS.Page)Variable).IsParent (Parameters.Get<Guid>(0));
						}

						case "getcontent":
						{
							switch (Parameters.Type (0).Name.ToLower())
							{
								case "guid":
								{
									return ((sCMS.Page)Variable).GetContent (Parameters.Get<Guid>(0));
								}
									
								case "string":
								{
									return ((sCMS.Page)Variable).GetContent (Parameters.Get<string>(0));
								}
							}
							break;
						}

						case "load":
						{
							return sCMS.Page.Load (Parameters.Get<Guid>(0));
						}
							
						case "list":
						{
							switch (Parameters.Type (0).Name.ToLower())
							{
								case "guid":
								{
									return sCMS.Page.List (Parameters.Get<Guid>(0));
								}
									
								default:
								{
									return sCMS.Page.List ();
								}
							}
							
							
							
						}
					}
					break;
				}
				#endregion


// #region sCMS.Template
// case "scms.template":
//
// switch (Method)
// {
// case "":
// return ((sCMS.Template)Variable);
//
// case "id":
// return ((sCMS.Template)Variable).Id;
//
// case "createtimestamp":
// return ((sCMS.Template)Variable).CreateTimestamp;
//
// case "updatetimestamp":
// return ((sCMS.Template)Variable).UpdateTimestamp;
//
// case "name":
// return ((sCMS.Template)Variable).Title;
//
// case "fields":
// return SorentoLib.Render.Variables.ConvertToListObject<sCMS.Field> (((sCMS.Template)Variable).Fields);
//
// case "list":
//// if (Session.AccessLevel < SorentoLib.Enums.Accesslevel.Editor) throw new Exception (string.Format (sCMS.Strings.Exception.ResolverSessionPriviliges, "template.list"));
//
// return SorentoLib.Render.Variables.ConvertToListObject<sCMS.Template> (sCMS.Template.List ());
// }
// break;
// #endregion

#region sCMS.Field
case "scms.field":
switch (Method)
{
case "":
return ((sCMS.Field)Variable);

case "id":
return ((sCMS.Field)Variable).Id;

case "name":
return ((sCMS.Field)Variable).Name;

case "type":
return ((sCMS.Field)Variable).Type;
}
break;
#endregion

#region sCMS.Content
case "scms.contentdata":

switch (Method)
{
case "":
return ((sCMS.Content)Variable);

case "data":
return ((sCMS.Content)Variable).Data;
}
break;
#endregion

					#region sCMS.CollectionSchema
				case "scms.collectionschema":
				{
					switch (Method)
					{
						case "":
						{
							return ((sCMS.CollectionSchema)Variable);
						}
							
//						case "name":
//						{
//							return ((sCMS.CollectionSchema)Variable).Name;
//						}

						case "collections":
						{
							return ((sCMS.CollectionSchema)Variable).Collections;
						}

						case "load":
						{
							switch (Parameters.Type (0).Name.ToLower())
							{
								case "guid":							
									return sCMS.CollectionSchema.Load (Parameters.Get<Guid>(0));
									
								case "string":
									return sCMS.CollectionSchema.Load (new Guid (Parameters.Get<string>(0)));
							}
							break;
						}
					}
					break;
				}
#endregion

#region sCMS.Collection
				case "scms.collection":
				{
					
					
					switch (Method)
					{
						case "":
						{
							return ((sCMS.Collection)Variable);
						}

						case "id":
						{
							return ((sCMS.Collection)Variable).Id;
						}

						case "title":
						{
							return ((sCMS.Collection)Variable).Title;
						}
						
						case "getcontent":
						{							
							 switch (Parameters.Type (0).Name.ToLower())
							 {
								case "guid":
								{
									return ((sCMS.Collection)Variable).GetContent (Parameters.Get<Guid>(0));
								}
							
								case "string":
								{
									return ((sCMS.Collection)Variable).GetContent (Parameters.Get<string>(0));
								}
							 }
							break;
						}

//						case "load":
//							switch (Parameters.Type (0).Name.ToLower())
//							{
//								case "guid":
//									return sCMS.Collection.Load (Parameters.Get<Guid>(0));
//									
//								case "string":
//									return sCMS.Collection.Load (new Guid (Parameters.Get<string>(0)));
//							}
//							break;
					}
					break;
				}
#endregion
			}



throw new SorentoLib.Exceptions.RenderExceptionMemberNotFound ();
}
#endregion
}
}