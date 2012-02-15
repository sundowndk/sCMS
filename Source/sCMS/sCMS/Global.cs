//
// Global.cs
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
	public class Global
	{
		#region Public Static Fields
		public static string DatastoreAisle = "scms_globals";
		#endregion

		#region Private Fields
		private Guid _id;
		private int _createtimestamp;
		private int _updatetimestamp;
		private Field _field;
		private Content _content;
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

		public string Name
		{
			get
			{
				return this._field.Name;
			}
			
			set
			{
				this._field.Name = value;
			}
		}
		
		public Enums.FieldType Type
		{
			get
			{
				return this._field.Type;
			}
		}

		public Field Field
		{
			get
			{
				return this._field;
			}

			set
			{
				this._field = value;
			}
		}

		public Content Content
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
		#endregion

		#region Public Constructors
		public Global (sCMS.Enums.FieldType Type, string Name)
		{
			this._id = Guid.NewGuid ();
			this._createtimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			
			this._field = new Field (Type, Name);
			this._content = new Content (this._field);
		}

		private Global ()
		{
			this._id = Guid.Empty;
			this._createtimestamp = 0;
			this._updatetimestamp = 0;		
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
				item.Add ("field", this._field);
				item.Add ("content", this._content);

				SorentoLib.Services.Datastore.Set (DatastoreAisle, this._id.ToString (), SNDK.Convert.ToXmlDocument (item, this.GetType ().FullName.ToLower ()));
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.GLOBAL", exception.Message));
 
				// EXCEPTION: Exception.GlobalSave
				throw new Exception (string.Format (Strings.Exception.GlobalSave, this._id.ToString ()));
			}
		}
		
		public XmlDocument ToXmlDocument ()
		{
			Hashtable result = new Hashtable ();

			result.Add ("id", this._id);
			result.Add ("createtimestamp", this._createtimestamp);
			result.Add ("updatetimestamp", this._updatetimestamp);
			result.Add ("name", this.Name);
			result.Add ("type", this.Type);
			result.Add ("field", this._field);
			result.Add ("content", this._content);

			return SNDK.Convert.ToXmlDocument (result, this.GetType ().FullName.ToLower ());
		}
		#endregion

		#region Public Static Methods
		public static Global Load (Guid id)
		{
			Global result;
			
			try
			{
				Hashtable item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (SorentoLib.Services.Datastore.Get<XmlDocument> (DatastoreAisle, id.ToString ()).SelectSingleNode ("(//scms.global)[1]")));
				result = new Global ();

				result._id = new Guid ((string)item["id"]);

				if (item.ContainsKey ("createtimestamp"))
				{
					result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
				}

				if (item.ContainsKey ("updatetimestamp"))
				{
					result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
				}

				if (item.ContainsKey ("field"))
				{					
					result._field = Field.FromXmlDocument ((XmlDocument)item["field"]);
				}
				
				if (item.ContainsKey ("content"))
				{
					result._content = Content.FromXmlDocument ((XmlDocument)item["content"]);
				}
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.GLOBAL", exception.Message));

				// EXCEPTION: Excpetion.GlobalLoad
				throw new Exception (string.Format (Strings.Exception.GlobalLoadGuid, id.ToString ()));
			}

			return result;
		}

		public static void Delete (Guid id)
		{
			try
			{
				SorentoLib.Services.Datastore.Delete (DatastoreAisle, id.ToString ());
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.GLOBAL", exception.Message));

				// EXCEPTION: Exception.GlobalDelete
				throw new Exception (string.Format (Strings.Exception.GlobalDelete, id.ToString ()));
			}
		}

		public static List<Global> List ()
		{
			List<Global> result = new List<Global> ();

			foreach (string id in SorentoLib.Services.Datastore.ListOfShelfs (DatastoreAisle))
			{
				try
				{
					result.Add (Load (new Guid (id)));
				}
				catch (Exception exception)
				{
					// LOG: LogDebug.ExceptionUnknown
					SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.GLOBAL", exception.Message));
					
					// LOG: LogDebug.GlobalList
					SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.GlobalList, id));
				}
			}

			return result;
		}
		
		public static Global FromXmlDocument (XmlDocument xmlDocument)
		{
			Hashtable item;
			Global result;

			try
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (xmlDocument.SelectSingleNode ("(//scms.global)[1]")));
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
					result = new Global ();
					result._id = new Guid ((string)item["id"]);
				}
			}
			else
			{
				// EXCEPTION: Exception.RootFromXMLDocument
				throw new Exception (Strings.Exception.RootFromXMLDocument);
			}
			
			if (item.ContainsKey ("createtimestamp"))
			{
				result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
			}

			if (item.ContainsKey ("updatetimestamp"))
			{
				result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
			}				
			
			if (item.ContainsKey ("field"))
			{					
				result._field = Field.FromXmlDocument ((XmlDocument)item["field"]);
			}
				
			if (item.ContainsKey ("content"))
			{
				result._content = Content.FromXmlDocument ((XmlDocument)item["content"]);
			}

			return result;
		}				
		#endregion
	}
}
