// 
// Page.cs
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
using System.Xml;
using System.Collections;
using System.Collections.Generic;

using SNDK;
using SorentoLib;

namespace sCMS
{
	public class Page
	{
		#region Public Static Fields
		public static string DatastoreAisle = "scms_pages";
		#endregion

		#region Private Fields
		private Guid _id;
		private int _createtimestamp;
		private int _updatetimestamp;
		private Guid _templateid;
		internal Guid _parentid;
		private Guid _rootid;
		private string _title;
		private List<string> _aliases;
		private List<Content> _contents;
		#endregion
		
		#region Public Fields
		public Guid Id
		{
			get
			{
				return this._id;
			}
		}

		public int CreateTimestamp
		{
			get
			{
				return this._createtimestamp;
			}
		}

		public int UpdateTimestamp
		{
			get
			{
				return this._updatetimestamp;
			}
		}
		
		
		public Template Template
		{
			get
			{
				return Template.Load (this._templateid);
			}
		}
		
		public Page Parent
		{
			get
			{
				if (this._parentid != Guid.Empty)
				{
					return Load (this._parentid);
				}
				
				return null;
			}
			
			set
			{
				this._parentid = value.Id;
			}
		}
		
		public Guid RootId
		{
			get
			{
				return this._rootid;
			}
		}
		
		public string Path
		{
			get
			{
				string result = string.Empty;

				try
				{
					if (this._parentid != Guid.Empty)
					{
						result += this.Parent.Path;
					}
				}
				catch
				{
				}

				result += "/";

				result += this._title;

				return result;
			}
		}

		public string Title
		{
			get
			{
				return this._title;
			}

			set
			{
				string newname = Helpers.MakeStringURLSafe (value);

				if (this._title != newname)
				{
					int count = 2;
					string dummy = newname;
					while (List ().Exists (delegate (Page o) { return o.Title == newname; }))
					{
						newname = dummy +"_"+ count++;
					}

					this._title = newname;
				}
			}
		}

		public List<string> Aliases
		{
			get
			{
				return this._aliases;
			}
		}

		public List<Content> Contents
		{
			get
			{
				return this._contents;
			}
		}
		#endregion

		#region Constructors
		public Page (Root Root, Template Template, string Title)
		{
			this._id = Guid.NewGuid ();
			this._createtimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._templateid = Template.Id;
			this._rootid = Root.Id;
			this._parentid = Guid.Empty;
			
			if (Title != string.Empty)
			{
				this.Title = Title;
			}
			else
			{
				this.Title = "untitled";
			}
			
			this._aliases = new List<string> ();
			this._contents = new List<Content> ();
		}

		private Page ()
		{
			this._id = Guid.Empty;
			this._createtimestamp = 0;
			this._updatetimestamp = 0;
			this._templateid = Guid.Empty;
			this._rootid = Guid.Empty;
			this._parentid = Guid.Empty;
			this._title = string.Empty;
			this._aliases = new List<string> ();
			this._contents = new List<Content> ();
		}
		#endregion

		#region Public Methods
		public void Save ()
		{
			try
			{
				this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
				
				Hashtable item = new Hashtable ();

				item.Add ("id", this._id);
				item.Add ("createtimestamp", this._createtimestamp);
				item.Add ("updatetimestamp", this._updatetimestamp);				
				item.Add ("templateid", this._templateid);
				item.Add ("rootid", this._rootid);
				item.Add ("parentid", this._parentid);
				item.Add ("title", this._title);
				item.Add ("aliases", this._aliases);
				item.Add ("contents", this._contents);
				
				SorentoLib.Services.Datastore.Meta meta = new SorentoLib.Services.Datastore.Meta ();
				meta.Add ("parentid", this._parentid);
				meta.Add ("path", this.Path);
				
				foreach (string alias in this._aliases)
				{
					string path = System.IO.Path.GetDirectoryName (this.Path) +"/"+ alias;
					path = path.Replace ("//", "/");
					meta.Add ("path", path);
				}
					
				SorentoLib.Services.Datastore.Set (DatastoreAisle, this._id.ToString (), SNDK.Convert.ToXmlDocument (item, this.GetType ().FullName.ToLower ()), meta);
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));
 
				// EXCEPTION: Exception.PageSave
				throw new Exception (string.Format (Strings.Exception.PageSave, this._id.ToString ()));
			}			
		
		}
		public object GetContent (Field Field)
		{
			return GetContent (Field.Id);
		}
		
		public object GetContent (Guid Id)
		{
			try
			{
			    Content content = this._contents.Find (delegate (Content c) { return c.Id == Id; });
				
				if (content != null)
				{
					return content.Data;
				}
				else
				{						
					return Field.DefaultValue (CollectionSchema.Load (this._templateid).GetField (Id).Type);
				}
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.PAGE", exception.Message));
				
				// EXCEPTION: Exception.PageGetContent
				throw new Exception (string.Format (Strings.Exception.PageGetContent, Id));
			}			
		}
		
		public void SetContent (Field Field, object Data)
		{
			SetContent (Field.Id, Data);
		}
		
		public void SetContent (Guid Id, object Data)
		{
			try
			{
				Content content = this._contents.Find (delegate (Content c) { return c.Id == Id;});
				
				if (content == null)
				{
					Template template = Template.Load (this._templateid);
					content = new Content (template.GetField (Id));					
				}
				
				content.Data = Data;
				
				this._contents.Add (content);
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.PAGE", exception.Message));
				
				// EXCEPTION: Exception.PageSetContent
				throw new Exception (string.Format (Strings.Exception.PageSetContent, Id.ToString ()));
			}
		}		
		
		public XmlDocument ToXmlDocument ()
		{
			Hashtable result = new Hashtable ();

			result.Add ("id", this._id);
			result.Add ("createtimestamp", this._createtimestamp);
			result.Add ("updatetimestamp", this._updatetimestamp);
			result.Add ("templateid", this._templateid);
			result.Add ("rootid", this._rootid);
			result.Add ("parentid", this._parentid);			
			result.Add ("path", this.Path);
			result.Add ("title", this._title);
			result.Add ("aliases", this._aliases);
			result.Add ("contents", this._contents);

			return SNDK.Convert.ToXmlDocument (result, this.GetType ().FullName.ToLower ());
		}
		#endregion

		#region Public Static Methods
		public static Page Load (string Path)
		{
			try
			{
				return Load (new Guid (SorentoLib.Services.Datastore.FindShelf (DatastoreAisle, new SorentoLib.Services.Datastore.MetaSearch ("path", SorentoLib.Enums.DatastoreMetaSearchCondition.Equal, Path))));
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.PAGE", exception.Message));

				// EXCEPTION: Excpetion.PageLoadPath
				throw new Exception (string.Format (Strings.Exception.PageLoadPath, Path));
			}	
		}
		
		public static Page Load (Guid Id)
		{
			Page result;
			
			try
			{
				Hashtable item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (SorentoLib.Services.Datastore.Get<XmlDocument> (DatastoreAisle, Id.ToString ()).SelectSingleNode ("(//scms.page)[1]")));
				result = new Page ();

				result._id = new Guid ((string)item["id"]);

				if (item.ContainsKey ("createtimestamp"))
				{
					result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
				}

				if (item.ContainsKey ("updatetimestamp"))
				{
					result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
				}

				if (item.ContainsKey ("templateid"))
				{					
					result._templateid = new Guid ((string)item["templateid"]);
				}
				
				if (item.ContainsKey ("rootid"))
				{
					result._rootid = new Guid ((string)item["rootid"]);
				}
				
				if (item.ContainsKey ("parentid"))
				{
					result._parentid = new Guid ((string)item["parentid"]);
				}
				
				if (item.ContainsKey ("title"))
				{
					result._title = (string)item["title"];
				}
				
				if (item.ContainsKey ("contents"))
				{
					result._contents.Clear ();
					foreach (XmlDocument content in (List<XmlDocument>)item["contents"])
					{
						result._contents.Add (Content.FromXmlDocument (content));
					}
				}	
				
				if (item.ContainsKey ("aliases"))
				{
					result._aliases = new List<string> ();						
					foreach (XmlDocument alias in (List<XmlDocument>)item["aliases"])
					{					
						result._aliases.Add ((string)((Hashtable)SNDK.Convert.FromXmlDocument (alias))["value"]);
					}
				}								
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.PAGE", exception.Message));

				// EXCEPTION: Excpetion.PageLoadName
				throw new Exception (string.Format (Strings.Exception.PageLoadGuid, Id));
			}	

			return result;
		}

		public static void Delete (Guid Id)
		{
			try
			{
				foreach (Page page in List (Id))
				{
					try
					{
						Delete (page.Id);
					}
					catch (Exception exception)
					{
						// LOG: LogDebug.ExceptionUnknown
						SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.PAGE", exception.Message));
					}						
				}
				
				SorentoLib.Services.Datastore.Delete (DatastoreAisle, Id.ToString ());
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.PAGE", exception.Message));
				
				// EXCEPTION: Exception.PageDelete
				throw new Exception (string.Format (Strings.Exception.PageDelete, Id.ToString ()));
			}			
		}
		
		public static List<Page> List ()
		{
			List<Page> result = new List<Page> ();

			foreach (string id in SorentoLib.Services.Datastore.ListOfShelfs (DatastoreAisle))
			{
				try
				{
					result.Add (Load (new Guid (id)));
				}
				catch (Exception exception)
				{
					// LOG: LogDebug.ExceptionUnknown
					SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.PAGE", exception.Message));
					
					// LOG: LogDebug.PageList
					SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.PageList, id));
				}
			}

			return result;
		}
		
		public static List<Page> List (Guid ParentId)
		{
			List<Page> result = new List<Page> ();

			foreach (string id in SorentoLib.Services.Datastore.ListOfShelfs (DatastoreAisle, new SorentoLib.Services.Datastore.MetaSearch ("parentid", SorentoLib.Enums.DatastoreMetaSearchCondition.Equal, ParentId)))
			{
				try
				{
					result.Add (Load (new Guid (id)));
				}
				catch (Exception exception)
				{
					// LOG: LogDebug.ExceptionUnknown
					SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.PAGE", exception.Message));
					
					// LOG: LogDebug.PageList
					SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.PageList, id));
				}
			}

			return result;
		}
		
		public static Page FromXmlDocument (XmlDocument xmlDocument)
		{
			Hashtable item;
			Page result;

			try
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (xmlDocument.SelectSingleNode ("(//scms.page)[1]")));
			}
			catch
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (xmlDocument);
			}

			if (item.ContainsKey ("id"))
			{
				try
				{
					result = Load (new Guid ((string)item["id"]));
				}
				catch
				{
					result = new Page ();
					result._id = new Guid ((string)item["id"]);
				}
			}
			else
			{
				// EXCEPTION: Exception.PageFromXMLDocument
				throw new Exception (Strings.Exception.PageFromXMLDocument);
			}
			
			if (item.ContainsKey ("createtimestamp"))
			{
				result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
			}

			if (item.ContainsKey ("updatetimestamp"))
			{
				result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
			}				
			
			if (item.ContainsKey ("templateid"))
			{					
				result._templateid = new Guid ((string)item["templateid"]);
			}
			
			if (item.ContainsKey ("rootid"))
			{
				result._rootid = new Guid ((string)item["rootid"]);
			}
			
			if (item.ContainsKey ("parentid"))
			{
				result._parentid = new Guid ((string)item["parentid"]);
			}
				
			if (item.ContainsKey ("title"))
			{
				result._title = (string)item["title"];
			}
				
			if (item.ContainsKey ("contents"))
			{
				result._contents.Clear ();
				foreach (XmlDocument content in (List<XmlDocument>)item["contents"])
				{
					result._contents.Add (Content.FromXmlDocument (content));
				}
			}		
			
			if (item.ContainsKey ("aliases"))
			{
				result._aliases = new List<string> ();						
				foreach (XmlDocument alias in (List<XmlDocument>)item["aliases"])
				{					
					result._aliases.Add ((string)((Hashtable)SNDK.Convert.FromXmlDocument (alias))["value"]);
				}
			}											

			return result;
		}			
		#endregion
	}
}

