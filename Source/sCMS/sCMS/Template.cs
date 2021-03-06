// 
// Template.cs
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
	public class Template
	{
		#region Public Static Fields
		public static string DatastoreAisle = "scms_templates";
		#endregion

		#region Private Fields
		private Guid _id;
		private int _createtimestamp;
		private int _updatetimestamp;
		private Guid _parentid;			
		private string _title;
		private string _content;
		private List<Field> _fields;
		private List<string> _stylesheetids;
		private List<string> _javascriptids;
		
		private string _stylesheetidsasstring
		{
			get
			{
				string result = string.Empty;				
				foreach (string id in this._stylesheetids)
				{
					result += id +";";
				}				
				return result;
			}
			
			set
			{
				this._stylesheetids.Clear ();				
				foreach (string id in value.Split (";".ToCharArray (), StringSplitOptions.RemoveEmptyEntries))
				{
					this._stylesheetids.Add (id);
				}
			}
		}
		
		private string _javascriptidsasstring
		{
			get
			{
				string result = string.Empty;				
				foreach (string id in this._javascriptids)
				{
					result += id +";";
				}				
				return result;
			}
			
			set
			{
				this._javascriptids.Clear ();				
				foreach (string id in value.Split (";".ToCharArray (), StringSplitOptions.RemoveEmptyEntries))
				{
					this._javascriptids.Add (id);
				}
			}
		}		
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
		
		public Guid ParentId
		{
			get
			{
				return this._parentid;
			}
							
			set
			{
				this._parentid = value;
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
				this._title = value;
			}
		}

		public string Content
		{
			get
			{
				return this._content;
			}

			set
			{
				this._content = value;
			}
		}
		
		public List<Field> Fields
		{
			get
			{
//				List<Field> result = this.CompileFields ();				
//				result.Sort (delegate (Field field1, Field field2) {return field1.Sort.CompareTo (field2.Sort);});
//				return result;
				return CompileFields ();
			}
		}
		
		public List<Field> AllFields
		{
			get
			{
//				List<Field> result = this.CompileFields ();				
//				result.Sort (delegate (Field field1, Field field2) {return field1.Sort.CompareTo (field2.Sort);});
//				return result;
				return CompileFields ();				
			}
		}
		
		public List<Stylesheet> Stylesheets
		{
			get
			{
				List<Stylesheet> result = new List<Stylesheet> ();
				
				foreach (string id in this._stylesheetids)
				{
					try
					{
						result.Add (Stylesheet.Load (id));
					}
					catch (Exception exception)
					{
						// LOG: LogDebug.ExceptionUnknown
						SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));
						
						// LOG: LogDebug.StylesheetList
						SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.StylesheetList, id));
					}
				}
				
				return result;
			}
		}
		
		public List<Javascript> Javascripts
		{
			get
			{
				List<Javascript> result = new List<Javascript> ();
				
				foreach (string id in this._javascriptids)
				{
					try
					{
						result.Add (Javascript.Load (id));
					}
					catch (Exception exception)
					{
						// LOG: LogDebug.ExceptionUnknown
						SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));
						
						// LOG: LogDebug.JavascriptList
						SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.JavascriptList, id));
					}
				}
				
				return result;
			}
		}		
 		#endregion

		#region Public Constructors
		public Template ()
		{
			this._id = Guid.NewGuid ();
			this._createtimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._parentid = Guid.Empty;
			this._title = string.Empty;
			this._content = string.Empty;
			this._fields = new List<Field> ();
			this._stylesheetids = new List<string> ();
			this._javascriptids = new List<string> ();
		}
		#endregion

		#region Private Methods
		private List<Field> CompileFields ()
		{
			List<Field> result = new List<Field> ();
			
			try 
			{
//			    this._fields.Sort (delegate (Field f1, Field f2) { return f1.Sort.CompareTo (f2.Sort); });
				result.AddRange (this._fields);
			
				if (this._parentid != Guid.Empty)
				{				
					Template parent = Template.Load (this._parentid);
					
					foreach (Field field in parent.CompileFields ())
					{
						result.Add (field);
					}
				}							
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));
						
				// EXCEPTION: Exception.TemplateCompileField
				throw new Exception (Strings.Exception.TemplateCompileField);
			}
			
			result.Sort (delegate (Field f1, Field f2) { return f1.Sort.CompareTo (f2.Sort); });
			
			return result;
		}
		
		private string CompileContent ()
		{
			string result = string.Empty;
			
			try
			{									
				if (this._parentid != Guid.Empty)
				{										
					result = Template.Load (this._parentid).CompileContent ().Replace (SorentoLib.Services.Config.Get<string> (Enums.ConfigKey.scms_templateplaceholdertag), this._content);
				}
				else
				{
					result = this._content;
				}
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));
						
				// EXCEPTION: Exception.TemplateCompileContent
				throw new Exception (Strings.Exception.TemplateCompileContent);
			}
			
			return result;
		}
		
		private List<string> CompileStylesheet ()
		{
			List<string> result = new List<string> ();
		
			try
			{														
				if (this._parentid != Guid.Empty)
				{
					result.AddRange (Load (this._parentid).CompileStylesheet ());
				}
				
				result.AddRange (this._stylesheetids);
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));
						
				// EXCEPTION: Exception.TemplateCompileStylesheet
				throw new Exception (Strings.Exception.TemplateCompileStylesheet);
			}
			
			return result;
		}
		
		private List<string> CompileJavascript ()
		{
			List<string> result = new List<string> ();
		
			try
			{														
				if (this._parentid != Guid.Empty)
				{
					result.AddRange (Load (this._parentid).CompileJavascript ());
				}
				
				result.AddRange (this._javascriptids);
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));
						
				// EXCEPTION: Exception.TemplateCompileJavascript
				throw new Exception (Strings.Exception.TemplateCompileJavascript);
			}
			
			return result;
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
				item.Add ("parentid", this._parentid);
				item.Add ("title", this._title);
				item.Add ("content", this._content);
				item.Add ("fields", this._fields);
				item.Add ("stylesheetids", this._stylesheetidsasstring);
				item.Add ("javascriptids", this._javascriptidsasstring);
					
				SorentoLib.Services.Datastore.Set (DatastoreAisle, this._id.ToString (), SNDK.Convert.ToXmlDocument (item, this.GetType ().FullName.ToLower ()));
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));
 
				// EXCEPTION: Exception.TemplateSave
				throw new Exception (string.Format (Strings.Exception.TemplateSave, this._id.ToString ()));
			}
		}
		
		
		public List<string> Build ()
		{
			List<string> result = new List<string> ();

			foreach (string line in this.CompileContent ().Split ("\n".ToCharArray ()))
			{
				if (line.Contains (SorentoLib.Services.Config.Get<string> (Enums.ConfigKey.scms_stylesheetplaceholdertag)))
				{
					foreach (string id in this.CompileStylesheet ())
					{
						result.Add (string.Format (SorentoLib.Services.Config.Get<string> (Enums.ConfigKey.scms_stylesheethtmltag), SorentoLib.Services.Config.Get<string> (Enums.ConfigKey.scms_stylesheeturl) + id));
					}
					continue;
				}
				
				if (line.Contains (SorentoLib.Services.Config.Get<string> (Enums.ConfigKey.scms_javascriptplaceholdertag)))
				{
					foreach (string id in this.CompileJavascript ())
					{
						result.Add (string.Format (SorentoLib.Services.Config.Get<string> (Enums.ConfigKey.scms_javascripthtmltag), SorentoLib.Services.Config.Get<string> (Enums.ConfigKey.scms_javascripturl) + id));
					}
					continue;
				}
				
				result.Add (line);
			}

			return result;	
		}
		
		public void AddStylesheet (Stylesheet Stylesheet)
		{
			this._stylesheetids.Add (Stylesheet.Id);
		}
		
		public void RemoveStylesheet (string Id)
		{
			this._stylesheetids.RemoveAll (delegate (string s) { return s == Id; });
		}
		
		public void AddJavascript (Javascript Javascript)
		{
			this._stylesheetids.Add (Javascript.Id);
		}
		
		public void RemoveJavascript (string Id)
		{
			this._javascriptids.RemoveAll (delegate (string s) { return s == Id; });
		}		
		
		public void AddField (Field Field)
		{
			this._fields.Add (Field);			
		}
		
		public void RemoveField (Field Field)
		{
			this._fields.RemoveAll (delegate (Field f) { return f.Id == Field.Id; });
		}
		
		public void RemoveField (Guid Id)
		{
			this._fields.RemoveAll (delegate (Field f) { return f.Id == Id; });
		}
		
		public Field GetField (string Name)
		{
			return this.Fields.Find (delegate (Field field) { return field.Name == Name; });
		}		
		
		public Field GetField (Guid Id)
		{
			return this.Fields.Find (delegate (Field field) { return field.Id == Id; });
		}		
		
		public XmlDocument ToXmlDocument ()
		{
			Hashtable result = new Hashtable ();

			result.Add ("id", this._id);
			result.Add ("createtimestamp", this._createtimestamp);
			result.Add ("updatetimestamp", this._updatetimestamp);
			result.Add ("parentid", this._parentid);
			result.Add ("title", this._title);
			result.Add ("content", this._content);
			result.Add ("fields", this._fields);
			result.Add ("allfields", this.CompileFields ());
			result.Add ("stylesheets", this.Stylesheets);
			result.Add ("javascripts", this.Javascripts);
			
			return SNDK.Convert.ToXmlDocument (result, this.GetType ().FullName.ToLower ());
		}		
		#endregion

		#region Public Static Methods
		public static Template Load (Guid id)
		{
			Template result;
			
			try
			{
				Hashtable item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (SorentoLib.Services.Datastore.Get<XmlDocument> (DatastoreAisle, id.ToString ()).SelectSingleNode ("(//scms.template)[1]")));
				result = new Template ();

				result._id = new Guid ((string)item["id"]);

				if (item.ContainsKey ("createtimestamp"))
				{
					result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
				}

				if (item.ContainsKey ("updatetimestamp"))
				{
					result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
				}

				if (item.ContainsKey ("parentid"))
				{					
					result._parentid = new Guid ((string)item["parentid"]);
				}
				
				if (item.ContainsKey ("title"))
				{
					result._title = (string)item["title"];
				}
				
				if (item.ContainsKey ("content"))
				{
					result._content = (string)item["content"];
				}
				
				if (item.ContainsKey ("fields"))
				{					
					foreach (XmlDocument field in (List<XmlDocument>)item["fields"])
					{
						result._fields.Add (Field.FromXmlDocument (field));
					}
				}	
				
				if (item.ContainsKey ("stylesheetids"))
				{
					result._stylesheetidsasstring = (string)item["stylesheetids"];
				}				
				
				if (item.ContainsKey ("javascriptids"))
				{
					result._javascriptidsasstring = (string)item["javascriptids"];
				}			
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));

				// EXCEPTION: Excpetion.TemplateLoad
				throw new Exception (string.Format (Strings.Exception.TemplateLoad, id.ToString ()));
			}	
			
			return result;
		}

		public static void Delete (Guid Id)
		{
			try
			{
				Template template = Load (Id);
				
				foreach (Page page in Page.List ())
				{
					if (page.Template._id == template._id)
					{
						Page.Delete (page.Id);
					}
				}
				
				foreach (Template template2 in Template.List ())
				{
					if (template2._parentid == template._id)
					{
						Template.Delete (template2._id);
					}
				}
				
				SorentoLib.Services.Datastore.Delete (DatastoreAisle, Id.ToString ());
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));
				
				// EXCEPTION: Exception.TemplateDelete
				throw new Exception (string.Format (Strings.Exception.TemplateDelete, Id.ToString ()));
			}
		}

		public static List<Template> List ()
		{
			List<Template> result = new List<Template> ();

			foreach (string id in SorentoLib.Services.Datastore.ListOfShelfs (DatastoreAisle))
			{
				try
				{
					result.Add (Load (new Guid (id)));
				}
				catch (Exception exception)
				{
					// LOG: LogDebug.ExceptionUnknown
					SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.TEMPLATE", exception.Message));
					
					// LOG: LogDebug.TemplateListe
					SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.TemplateList, id));
				}
			}

			return result;
		}
		
		public static Template FromXmlDocument (XmlDocument xmlDocument)
		{
			Hashtable item;
			Template result;

			try
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (xmlDocument.SelectSingleNode ("(//scms.template)[1]")));
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
					result = new Template ();
					result._id = new Guid ((string)item["id"]);
				}
			}
			else
			{
				// EXCEPTION: Exception.TemplateFromXMLDocument
				throw new Exception (Strings.Exception.TemplateFromXMLDocument);
			}
			
			if (item.ContainsKey ("createtimestamp"))
			{
				result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
			}

			if (item.ContainsKey ("updatetimestamp"))
			{
				result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
			}				
			
			if (item.ContainsKey ("parentid"))
			{					
				result._parentid = new Guid ((string)item["parentid"]);
			}
				
			if (item.ContainsKey ("title"))
			{
				result._title = (string)item["title"];
			}
				
			if (item.ContainsKey ("content"))
			{
				result._content = (string)item["content"];
			}
			
			if (item.ContainsKey ("fields"))
			{
				int sort = 0;
				result._fields.Clear ();
				foreach (XmlDocument field in (List<XmlDocument>)item["fields"])
				{
					Field field2 = Field.FromXmlDocument (field);
					field2.Sort = sort;
					result._fields.Add (field2);
					sort++;
				}
			}
			
			if (item.ContainsKey ("stylesheets"))
			{
				result._stylesheetids.Clear ();
				foreach (XmlDocument stylesheet in (List<XmlDocument>)item["stylesheets"])
				{
					result._stylesheetids.Add (Stylesheet.FromXmlDocument (stylesheet).Id);
				}
			}	
			
			if (item.ContainsKey ("javascripts"))
			{
				result._javascriptids.Clear ();
				foreach (XmlDocument javascript in (List<XmlDocument>)item["javascripts"])
				{
					result._javascriptids.Add (Javascript.FromXmlDocument (javascript).Id);
				}
			}	

			return result;
		}						
		#endregion
	}
}
