// 
// Field.cs
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

using SorentoLib;

namespace sCMS
{	
	public class Field
	{
		#region Private Fields
		private Guid _id;
		private sCMS.Enums.FieldType _type;
		private string _name;
		private Hashtable _options;
		private int _sort;
		#endregion

		#region Public Fields
		public Guid Id
		{
			get
			{
				return this._id;
			}
		}

		public sCMS.Enums.FieldType Type
		{
			get
			{
				return this._type;
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

		public string Name
		{
			get
			{
				return this._name;
			}
			
			set
			{
				this._name = value.Replace (" ", "_").ToUpper ();
			}

		}

		public Hashtable Options
		{
			get
			{
				return this._options;
			}
		}
		#endregion

		#region Constructors		
		public Field (sCMS.Enums.FieldType Type, string Name, int Sort)
		{
			this._id = Guid.NewGuid ();
			this._type = Type;
			this.Name = Name;
			this._sort = Sort;			
			this._options = new Hashtable ();			
		}
		
		public Field (sCMS.Enums.FieldType Type, string Name)
		{			
			this._id = Guid.NewGuid ();
			this._type = Type;
			this.Name = Name;
			this._sort = 0;			
			this._options = new Hashtable ();
		}

		private Field ()
		{
			this._id = Guid.NewGuid ();
			this._name = string.Empty;
			this._sort = 0;
			this._options = new Hashtable ();
		}
		#endregion

		#region Public Methods		
		public XmlDocument ToXmlDocument ()
		{
			Hashtable result = new Hashtable ();
			
			result.Add ("id", this._id);
			result.Add ("type", this._type);
			result.Add ("name", this._name);			
			result.Add ("sort", this._sort);
			result.Add ("options", this._options);

			return SNDK.Convert.ToXmlDocument (result, this.GetType ().FullName.ToLower ());
		}
		#endregion
		
		#region Public Static Methods
		public static Field FromXmlDocument (XmlDocument xmlDocument)
		{
			Hashtable item;
			Field result = new Field ();

			try
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (xmlDocument.SelectSingleNode ("(//scms.field)[1]")));
			}
			catch
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (xmlDocument);
			}
									
			try
			{
				result._id = new Guid ((string)item["id"]);
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.FIELD", exception.Message));
				
				// EXCEPTION: Exception.FieldFromXMLDocument
				throw new Exception (Strings.Exception.FieldFromXMLDocument);
			}
			
			if (item.ContainsKey ("type"))
			{
				result._type = SNDK.Convert.StringToEnum<Enums.FieldType> ((string)item["type"]);
			}
			
			if (item.ContainsKey ("name"))
			{
				result._name = (string)item["name"];
			}
			
			if (item.ContainsKey ("sort"))
			{
				result._sort = int.Parse ((string)item["sort"]);
			}
			
//			if (item.ContainsKey ("options"))
//			{
//				result._options = (Hashtable)item["options"];
//			}

			return result;
		}				
		
		public static object DefaultValue (Enums.FieldType type)
		{
			switch (type)
			{
				case sCMS.Enums.FieldType.String:
				{
					return string.Empty;
				}
						
				case sCMS.Enums.FieldType.Text:
				{
					return string.Empty;
				}
					
				case sCMS.Enums.FieldType.ListString:
				{
					string[] result = {};
					return result;
				}

				case sCMS.Enums.FieldType.Image:
				{									
					return Media.Default ();
				}						

				case sCMS.Enums.FieldType.ListImage:
				{
					return new List<Media> ();
				}

				case sCMS.Enums.FieldType.ListPage:
				{
					return new List<Page> ();
				}
					
				case sCMS.Enums.FieldType.Link:
				{
					return string.Empty;
				}
						
				default:
				{
					return string.Empty;
				}
			}
		}		
		#endregion
				
	}
}

