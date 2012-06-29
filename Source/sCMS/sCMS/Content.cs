// 
// Content.cs
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
	public class Content
	{
		#region Private Fields
		private Guid _id;
		private object _data;		
		private sCMS.Enums.FieldType _type;
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

		public string DataAsString
		{
			get
			{
				return (string)this._data;
			}

			set
			{
				this.Data = value;
			}
		}
		
		public static object EncodeData (Enums.FieldType Type, string Value)
		{
			object result = string.Empty;

			switch (Type)
			{
				#region STRING
				case sCMS.Enums.FieldType.String:
				{
					result = Value;
					break;
				}
				#endregion

				#region LISTSTRING
				case sCMS.Enums.FieldType.ListString:
				{
					result = ((string)Value).Split ("\n".ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
					break;
				} 
				#endregion

				#region TEXT
				case sCMS.Enums.FieldType.Text:
				{
					result = Value;
					break;
				}
				#endregion
					
				#region LINK
				case sCMS.Enums.FieldType.Link:
				{
					result = Value;
					break;
				}
				#endregion
					
				#region IMAGE
				case sCMS.Enums.FieldType.Image:
				{
					if (new Guid ((string)Value) == Guid.Empty)
					{
						result = Media.Default ();
					}
					else
					{
						result = Media.Load (new Guid ((string)Value));
					}						
					break;
				}
				#endregion
			}

			return result;
		}
		
		public static object DecodeData (Enums.FieldType Type, object Value)
		{
			object result = string.Empty;
			
			switch (Type)
			{
				#region STRING
				case sCMS.Enums.FieldType.String:						
				{
					result = Value;
					break;
				}
				#endregion

				#region LISTSTRING
				case sCMS.Enums.FieldType.ListString:
				{
					switch (Value.GetType ().FullName.ToLower ())
					{
						#region STRING
						case "system.string":
						{
							result = Value;
							break;
						}
						#endregion

						#region STRING[]
						default:
						{
							string strings = string.Empty;
							foreach (string s in (string[])Value)
							{
								strings += s +"\n";
							}								

							result = strings;
							break;
						}
						#endregion
					}
					break;
				}
				#endregion

				#region TEXT
				case sCMS.Enums.FieldType.Text:
				{					
					result = Value;
					break;
				}
				#endregion
						
				#region LINK
				case sCMS.Enums.FieldType.Link:
				{
					result = Value;
					break;
				}
				#endregion
						
				#region IMAGE
				case sCMS.Enums.FieldType.Image:
				{					
					switch (Value.GetType ().FullName.ToLower ())
					{
						#region STRING
						case "system.string":
						{
							result = new Guid ((string)Value).ToString ();
							break;
						}
						#endregion

						#region MEDIA
						default:
							result = ((Media)Value).Id.ToString ();
							break;
						#endregion
					}						
					break;
				}
				#endregion	
			}
			
			return result;
		}
			
		public object Data
		{
			get
			{
				return EncodeData (this.Type, (string)this._data);
			}

			set
			{
				this._data = DecodeData (this._type, value);
			}
		}
		#endregion

		#region Constructors					
		public Content (Field field)
		{
			this._id = field.Id;
			this._type = field.Type;
			this.Data = Field.DefaultValue (field.Type);
		}
						
		public Content (Field Field, object Data)
		{
			this._id = Field.Id;
			this._type = Field.Type;			
			this.Data = Data;
		}
		
		private Content ()
		{
			this._id = Guid.Empty;
			this._type = Enums.FieldType.String;
			this._data = Field.DefaultValue (Enums.FieldType.String);
		}
		#endregion
		
		#region Public Methods
		public XmlDocument ToXmlDocument ()
		{
			Hashtable result = new Hashtable ();

			result.Add ("id", this._id);
			result.Add ("type", this._type);
			result.Add ("data", this._data);

			return SNDK.Convert.ToXmlDocument (result, this.GetType ().FullName.ToLower ());
		}
		#endregion
		
		#region Public Static Methods
		public static Content FromXmlDocument (XmlDocument xmlDocument)
		{
			Hashtable item;
			Content result = new Content ();

			try
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (xmlDocument.SelectSingleNode ("(//scms.content)[1]")));
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
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.CONTENT", exception.Message));
				
				// EXCEPTION: Exception.FieldFromXMLDocument
				throw new Exception (Strings.Exception.ContentFromXMLDocument);
			}
			
			if (item.ContainsKey ("type"))
			{
				result._type = SNDK.Convert.StringToEnum<Enums.FieldType> ((string)item["type"]);
			}
			
			if (item.ContainsKey ("data"))
			{				
				result._data = (string)item["data"];
			}
			
			return result;
		}
		#endregion
	}
}

