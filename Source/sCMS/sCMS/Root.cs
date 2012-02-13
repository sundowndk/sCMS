// 
// Root.cs
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
	[Serializable]
	public class Root
	{
		#region Public Static Fields
		public static string DatastoreAisle = "scms_roots";		
		#endregion

		#region Private Fields
		private Guid _id;
		private int _createtimestamp;
		private int _updatetimestamp;
		private string _title;
		private List<RootFilter> _filters;
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

		public List<RootFilter> Filters
		{
			get
			{
				return this._filters;
			}
		}

		public int DependantPages
		{
			get
			{
				int result = 0;

//				foreach (Page page in Page.List ())
//				{
////					try
////					{
//						if (page.ParentId == this._id)
//						{
//							result++;
//						}
////					}
////					catch
////					{}
//				}

				return result;
			}
		}
		#endregion

		#region Constructor
		public Root (string Title)
		{
			this._id = Guid.NewGuid ();
			this._createtimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._updatetimestamp = SNDK.Date.CurrentDateTimeToTimestamp ();
			this._title = Title;
			this._filters = new List<RootFilter> ();
		}

		private Root ()
		{
			this._id = Guid.Empty;
			this._createtimestamp = 0;
			this._updatetimestamp = 0;
			this._title = string.Empty;
			this._filters = new List<RootFilter> ();
		}
		#endregion

		#region Public Methods
		public void Save ()
		{
			try
			{
				this._updatetimestamp = Date.CurrentDateTimeToTimestamp ();
				
				Hashtable item = new Hashtable ();

				item.Add ("id", this._id);
				item.Add ("createtimestamp", this._createtimestamp);
				item.Add ("updatetimestamp", this._updatetimestamp);
				item.Add ("title", this._title);
				item.Add ("filters", this._filters);
				
				SorentoLib.Services.Datastore.Set (DatastoreAisle, this._id.ToString (), SNDK.Convert.ToXmlDocument (item, this.GetType ().FullName.ToLower ()));
			}
			catch (Exception exception)
			{
				Console.WriteLine (exception);
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.ROOT", exception.Message));

				// EXCEPTION: Exception.RootSave
				throw new Exception (string.Format (Strings.Exception.RootSave, this._id));
			}			
		}
		
		public XmlDocument ToXmlDocument ()
		{
			Hashtable result = new Hashtable ();

			result.Add ("id", this._id);
			result.Add ("createtimestamp", this._createtimestamp);
			result.Add ("updatetimestamp", this._updatetimestamp);
			result.Add ("title", this._title);
			result.Add ("filters", this._filters);

			return SNDK.Convert.ToXmlDocument (result, this.GetType ().FullName.ToLower ());
		}
		
		
		public void ToAjaxRespons (SorentoLib.Ajax.Respons Respons)
		{
			Respons.Data = this.ToAjaxItem ();
		}

		public Hashtable ToAjaxItem ()
		{
			Hashtable result = new Hashtable ();

			result.Add ("id", this._id);
			result.Add ("createtimestamp", this._createtimestamp);
			result.Add ("updatetimestamp", this._updatetimestamp);
			result.Add ("name", this._title);

			List<Hashtable> filters = new List<Hashtable> ();
			foreach (RootFilter filter in this._filters)
			{
				filters.Add (filter.ToAjaxItem ());
			}

			result.Add ("filters", filters);
			result.Add ("dependantpages", this.DependantPages);

			return result;
		}
		#endregion

		#region Public Static Methods
		public static Root Load (Guid id)
		{		
			Root result;
			
			try
			{
				Hashtable item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (SorentoLib.Services.Datastore.Get<XmlDocument> (DatastoreAisle, id.ToString ()).SelectSingleNode ("(//scms.root)[1]")));
				result = new Root ();

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
				
				if (item.ContainsKey ("filters"))
				{
					result._filters.Clear ();

					foreach (XmlDocument filter in (List<XmlDocument>)item["filters"])
					{
						result._filters.Add (RootFilter.FromXmlDocument (filter));
					}
				}
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.ROOT", exception.Message));

				// EXCEPTION: Excpetion.RootLoad
				throw new Exception (string.Format (Strings.Exception.RootLoad, id));
			}
			
			return result;
		}

		public static void Delete (Guid id)
		{
			try
			{
//				foreach (Page page in Page.List ())
//				{
//					if (page.ParentId == id)
//					{
//						Page.Delete (page.Id);
//					}
//				}
							
				SorentoLib.Services.Datastore.Delete (DatastoreAisle, id.ToString ());
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.ROOT", exception.Message));

				// EXCEPTION: Exception.RootDelete
				throw new Exception (string.Format (Strings.Exception.RootDelete, id));
			}
		}

		public static List<Root> List ()
		{
			List<Root> result = new List<Root> ();

			foreach (string shelf in SorentoLib.Services.Datastore.ListOfShelfs (DatastoreAisle))
			{
				result.Add (Load (new Guid (shelf)));
			}

			return result;
		}

		public static Root FromAjaxRequest (SorentoLib.Ajax.Request Request)
		{
			return FromAjaxItem (Request.Data);
		}

		public static Root FromAjaxItem (Hashtable Item)
		{
			Root result;

			Guid id = Guid.Empty;

			try
			{
				id = new Guid ((string)Item["id"]);
			}
			catch {}

			if (id != Guid.Empty)
			{
				try
				{
					result = Root.Load (id);
				}
				catch
				{
					result = new Root ();
					result._id = id;
					if (Item.ContainsKey ("createtimestamp"))
					{
						result._createtimestamp = int.Parse ((string)Item["createtimestamp"]);
					}

					if (Item.ContainsKey ("updatetimestamp"))
					{
						result._createtimestamp = int.Parse ((string)Item["updatetimestamp"]);
					}
				}
			}
			else
			{
				try
				{
					result = new Root ((string)Item["name"]);
				}
				catch
				{
					throw new Exception (string.Format (Strings.Exception.RootFromAjaxItem, "Name"));
				}
			}

			if (Item.ContainsKey ("title"))
			{
				result._title = (string)Item["title"];
			}

			if (Item.ContainsKey ("filters"))
			{
				result._filters.Clear ();

				foreach (Hashtable item in (List<Hashtable>)Item["filters"])
				{
					result._filters.Add (RootFilter.FromAjaxItem (item));
				}
			}

			return result;
		}
		
		public static Root FromXmlDocument (XmlDocument xmlDocument)
		{
			Hashtable item;
			Root result;

			try
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (xmlDocument.SelectSingleNode ("(//scms.root)[1]")));
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
					result = new Root ();
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
			
			if (item.ContainsKey ("title"))
			{
				result._title = (string)item["title"];
			}
			
			if (item.ContainsKey ("filters"))
			{
				result._filters.Clear ();

				foreach (XmlDocument filter in (List<XmlDocument>)item["filters"])
				{
					result._filters.Add (RootFilter.FromXmlDocument (filter));
				}
			}

			return result;
		}		
		#endregion
	}
}

