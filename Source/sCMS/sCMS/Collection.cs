// 
// Collection.cs
//  
// Author:
//       Rasmus Pedersen <rasmus@akvaservice.dk>
// 
// Copyright (c) 2010-2012 Rasmus Pedersen
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
	public class Collection
	{
		#region Public Static Fields
		public static string DatastoreAisle = "scms_collections";
		#endregion

		#region Private Fields
		private Guid _id;
		private int _createtimestamp;
		private int _updatetimestamp;
		internal Guid _collectionschemaid;
		private string _title;
		private int _sort;
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
		
		public Guid CollectionSchemaId
		{
			get
			{
				return this._collectionschemaid;
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
		
		public int Sort
		{
			get
			{
				return this._sort;
			}
			
			set
			{
				this._sort = value;
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

		#region Constructor
		public Collection (CollectionSchema CollectionSchema, string Title, int Sort)
		{
			this._id = Guid.NewGuid ();
			this._createtimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._collectionschemaid = CollectionSchema.Id;
			this._title = Title;
			this._sort = Sort;
			this._contents = new List<Content> ();						
		}
		
		public Collection (CollectionSchema CollectionSchema, string Title)
		{
			this._id = Guid.NewGuid ();
			this._createtimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._collectionschemaid = CollectionSchema.Id;
			this._title = Title;
			this._sort = 0;
			this._contents = new List<Content> ();			
		}

		private Collection ()
		{
			this._id = Guid.Empty;
			this._createtimestamp = 0;
			this._updatetimestamp = 0;
			this._collectionschemaid = Guid.Empty;
			this._title = string.Empty;
			this._sort = 0;
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
				item.Add ("collectionschemaid", this._collectionschemaid);
				item.Add ("title", this._title);
				item.Add ("sort", this._sort);					 
				item.Add ("contents", this._contents);
				
				SorentoLib.Services.Datastore.Meta meta = new SorentoLib.Services.Datastore.Meta ();
				meta.Add ("collectionschemaid", this._collectionschemaid);

				SorentoLib.Services.Datastore.Set (DatastoreAisle, this._id.ToString (), SNDK.Convert.ToXmlDocument (item, this.GetType ().FullName.ToLower ()), meta);
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.COLLECTION", exception.Message));
 
				// EXCEPTION: Exception.CollectionSave
				throw new Exception (string.Format (Strings.Exception.CollectionSave, this._id.ToString ()));
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
					return Field.DefaultValue (CollectionSchema.Load (this._collectionschemaid).GetField (Id).Type);
				}
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.COLLECTION", exception.Message));
				
				// EXCEPTION: Exception.CollectionGetContent
				throw new Exception (string.Format (Strings.Exception.CollectionGetContent, Id));
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
					CollectionSchema collectionschema = CollectionSchema.Load (this._collectionschemaid);					
					content = new Content (collectionschema.GetField (Id));					
					content.Data = Data;
				}
				else
				{
					content.Data = Data;
					this._contents.Add (content);
				}
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.COLLECTION", exception.Message));
				
				// EXCEPTION: Exception.CollectionSetContent
				throw new Exception (string.Format (Strings.Exception.CollectionSetContent, Id.ToString ()));
			}
		}

		public XmlDocument ToXmlDocument ()
		{
			Hashtable result = new Hashtable ();

			result.Add ("id", this._id);
			result.Add ("createtimestamp", this._createtimestamp);
			result.Add ("updatetimestamp", this._updatetimestamp);
			result.Add ("collectionschemaid", this._collectionschemaid);
			result.Add ("title", this._title);
			result.Add ("sort", this._sort);
			result.Add ("contents", this._contents);

			return SNDK.Convert.ToXmlDocument (result, this.GetType ().FullName.ToLower ());
		}		
		#endregion

		#region Public Static Methods
		public static Collection Load (Guid id)
		{		
			Collection result;
			
			try
			{
				Hashtable item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (SorentoLib.Services.Datastore.Get<XmlDocument> (DatastoreAisle, id.ToString ()).SelectSingleNode ("(//scms.collection)[1]")));
				result = new Collection ();

				result._id = new Guid ((string)item["id"]);

				if (item.ContainsKey ("createtimestamp"))
				{
					result._createtimestamp = int.Parse ((string)item["createtimestamp"]);
				}

				if (item.ContainsKey ("updatetimestamp"))
				{
					result._updatetimestamp = int.Parse ((string)item["updatetimestamp"]);
				}
				
				if (item.ContainsKey ("collectionschemaid"))
				{					
					result._collectionschemaid = new Guid ((string)item["collectionschemaid"]);
				}
								
				if (item.ContainsKey ("title"))
				{					
					result._title = (string)item["title"];
				}
				
				if (item.ContainsKey ("sort"))
				{
					result._sort = int.Parse ((string)item["sort"]);
				}
				
				if (item.ContainsKey ("contents"))
				{
					foreach (XmlDocument content in (List<XmlDocument>)item["contents"])
					{
						result._contents.Add (Content.FromXmlDocument (content));
					}
				}				
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.COLLECTION", exception.Message));

				// EXCEPTION: Excpetion.CollectionLoadGuid
				throw new Exception (string.Format (Strings.Exception.CollectionLoadGuid, id.ToString ()));
			}

			return result;
		}

		public static void Delete (Guid Id)
		{
			try
			{
				SorentoLib.Services.Datastore.Delete (DatastoreAisle, Id.ToString ());
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.COLLECTION", exception.Message));

				// EXCEPTION: Exception.CollectionDelete
				throw new Exception (string.Format (Strings.Exception.CollectionDelete, Id.ToString ()));
			}		
		}
		
		public static List<Collection> List (CollectionSchema CollectionSchema)
		{
			return List (CollectionSchema.Id);
		}
		
		public static List<Collection> List (Guid CollectionSchemaId)
		{
			List<Collection> result = new List<Collection> ();
			
			foreach (string id in SorentoLib.Services.Datastore.ListOfShelfs (DatastoreAisle, new SorentoLib.Services.Datastore.MetaSearch ("collectionschemaid", SorentoLib.Enums.DatastoreMetaSearchCondition.Equal, CollectionSchemaId)))
			{
				try
				{
					result.Add (Load (new Guid (id)));
				}
				catch
				{
					// LOG: LogDebug.CollectionList
					SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.CollectionList, id));
				}			
			}
			
			result.Sort (delegate (Collection collection1, Collection collection2) {return collection1.Sort.CompareTo (collection2.Sort);});
			
			return result;
		}
		
		public static List<Collection> List ()
		{
			List<Collection> result = new List<Collection> ();

			foreach (string id in SorentoLib.Services.Datastore.ListOfShelfs (DatastoreAisle))
			{
				try
				{
					result.Add (Load (new Guid (id)));
				}
				catch
				{
					// LOG: LogDebug.CollectionList
					SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.CollectionList, id));
				}
			}

			return result;
		}

		public static Collection FromXmlDocument (XmlDocument xmlDocument)
		{
			Hashtable item;
			Collection result;

			try
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (xmlDocument.SelectSingleNode ("(//scms.collection)[1]")));
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
					result = new Collection ();
					result._id = new Guid ((string)item["id"]);
				}
			}
			else
			{
				// EXCEPTION: Exception.CollectionFromXMLDocument
				throw new Exception (Strings.Exception.CollectionFromXMLDocument);
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
			
			if (item.ContainsKey ("collectionschemaid"))
			{					
				result._collectionschemaid = new Guid ((string)item["collectionschemaid"]);
			}
			
			if (item.ContainsKey ("sort"))
			{
				result._sort = int.Parse ((string)item["sort"]);
			}
			
			if (item.ContainsKey ("contents"))
			{
				foreach (XmlDocument content in (List<XmlDocument>)item["contents"])
				{
					result._contents.Add (Content.FromXmlDocument (content));
				}
			}

			return result;
		}								
		#endregion
	}
}

