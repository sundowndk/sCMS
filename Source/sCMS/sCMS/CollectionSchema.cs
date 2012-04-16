// 
// CollectionSchema.cs
//  
// Author:
//       Rasmus Pedersen <rasmus@akvaservice.dk>
// 
// Copyright (c) 2011 Rasmus Pedersen
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
	public class CollectionSchema
	{
		#region Public Static Fields
		public static string DatastoreAisle = "scms_collectionschemas";
		#endregion

		#region Private Fields
		private Guid _id;
		private int _createtimestamp;
		private int _updatetimestamp;
		private string _title;
		private List<Field> _fields;		
		#endregion

		#region Internal Fields
		internal List<Guid> _collectionids;
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

		public List<Field> Fields
		{
			get
			{
				this._fields.Sort (delegate (Field field1, Field field2) {return field1.Sort.CompareTo (field2.Sort);});
				return this._fields;
			}
		}

		public List<Collection> Collections
		{
			get
			{
				return Collection.List (this);
			}
		}
		#endregion

		#region Constructor
		public CollectionSchema (string Title)
		{
			this._id = Guid.NewGuid ();
			this._createtimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._title = Title;
			this._fields = new List<Field> ();						
		}

		private CollectionSchema ()
		{
			this._id = Guid.Empty;
			this._createtimestamp = 0;
			this._updatetimestamp = 0;
			this._title = string.Empty;
			this._fields = new List<Field> ();
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
				item.Add ("title", this._title);
				item.Add ("fields", this._fields);

				SorentoLib.Services.Datastore.Set (DatastoreAisle, this._id.ToString (), SNDK.Convert.ToXmlDocument (item, this.GetType ().FullName.ToLower ()));
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.GLOBAL", exception.Message));
 
				// EXCEPTION: Exception.CollectionSchemaSave
				throw new Exception (string.Format (Strings.Exception.CollectionSchemaSave, this._id.ToString ()));
			}
		}

		public void AddField (Field Field)
		{
			this._fields.Add (Field);			
		}

		public void RemoveField (Guid Id)
		{
			this._fields.RemoveAll (delegate (Field f) { return f.Id == Id; });
		}
		
		public Field GetField (string Name)
		{
			return this._fields.Find (delegate (Field field) { return field.Name == Name; });
		}
		
		public Field GetField (Guid Id)
		{
			return this._fields.Find (delegate (Field field) { return field.Id == Id; });
		}

		public XmlDocument ToXmlDocument ()
		{
			Hashtable result = new Hashtable ();

			result.Add ("id", this._id);
			result.Add ("createtimestamp", this._createtimestamp);
			result.Add ("updatetimestamp", this._updatetimestamp);
			result.Add ("title", this._title);
			result.Add ("fields", this._fields);

			return SNDK.Convert.ToXmlDocument (result, this.GetType ().FullName.ToLower ());
		}		
		#endregion

		#region Public Static Methods
		public static CollectionSchema Load (Guid id)
		{
			CollectionSchema result;
			
			try
			{
				Hashtable item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (SorentoLib.Services.Datastore.Get<XmlDocument> (DatastoreAisle, id.ToString ()).SelectSingleNode ("(//scms.collectionschema)[1]")));
				result = new CollectionSchema ();

				result._id = new Guid ((string)item["id"]);

				if (item.ContainsKey ("createtimestamp"))
				{
					result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
				}

				if (item.ContainsKey ("updatetimestamp"))
				{
					result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
				}

				if (item.ContainsKey ("title"))
				{					
					result._title = (string)item["title"];
				}
				
				if (item.ContainsKey ("fields"))
				{
					foreach (XmlDocument field in (List<XmlDocument>)item["fields"])
					{
						result._fields.Add (Field.FromXmlDocument (field));
					}
				}
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.COLLECTIONSCHEMA", exception.Message));

				// EXCEPTION: Excpetion.CollectionSchemaLoadGuid
				throw new Exception (string.Format (Strings.Exception.CollectionSchemaLoadGuid, id.ToString ()));
			}

			return result;
		}

		public static void Delete (Guid Id)
		{
			try
			{
				foreach (Collection collection in Collection.List (Id))
				{
					try
					{
						Collection.Delete (collection.Id);
					}
					catch (Exception exception)
					{
						// LOG: LogDebug.ExceptionUnknown
						SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.COLLECTIONSCHEMA", exception.Message));
					}						
				}
				
				SorentoLib.Services.Datastore.Delete (DatastoreAisle, Id.ToString ());
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.COLLECTIONSCHEMA", exception.Message));

				// EXCEPTION: Exception.CollectionSchemaDelete
				throw new Exception (string.Format (Strings.Exception.CollectionSchemaDelete, Id.ToString ()));
			}			
		}

		public static List<CollectionSchema> List ()
		{
			List<CollectionSchema> result = new List<CollectionSchema> ();

			foreach (string id in SorentoLib.Services.Datastore.ListOfShelfs (DatastoreAisle))
			{
				try
				{
					result.Add (Load (new Guid (id)));
				}
				catch
				{
					// LOG: LogDebug.ColletionSchemaList
					SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.CollectionSchemaList, id));
				}
			}

			return result;
		}

		public static CollectionSchema FromXmlDocument (XmlDocument xmlDocument)
		{
			Hashtable item;
			CollectionSchema result;

			try
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (xmlDocument.SelectSingleNode ("(//scms.collectionschemal)[1]")));
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
					result = new CollectionSchema ();
					result._id = new Guid ((string)item["id"]);
				}
			}
			else
			{
				// EXCEPTION: Exception.CollectionSchemaFromXMLDocument
				throw new Exception (Strings.Exception.CollectionSchemaFromXMLDocument);
			}
			
			if (item.ContainsKey ("createtimestamp"))
			{
				result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
			}

			if (item.ContainsKey ("updatetimestamp"))
			{
				result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
			}				
			
			if (item.ContainsKey ("title"))
			{					
				result._title = (string)item["title"];
			}			
			
			if (item.ContainsKey ("fields"))
			{
				result._fields.Clear ();
				foreach (XmlDocument field in (List<XmlDocument>)item["fields"])
				{
					result._fields.Add (Field.FromXmlDocument (field));
				}
			}

			return result;
		}						
		#endregion
	}
}

