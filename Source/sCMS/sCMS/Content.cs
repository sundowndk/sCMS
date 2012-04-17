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

		public object Data
		{
			get
			{
				object result = string.Empty;

				switch (this._type)
				{
					#region STRING
					case sCMS.Enums.FieldType.String:
					{
						result = this._data;
						break;
					}
					#endregion

					#region LISTSTRING
					case sCMS.Enums.FieldType.ListString:
					{
						result = ((string)this._data).Split ("\n".ToCharArray (), StringSplitOptions.RemoveEmptyEntries);
						break;
					} 
					#endregion

					#region TEXT
					case sCMS.Enums.FieldType.Text:
					{
						result = this._data;
						break;
					}
					#endregion
						
					#region LINK
					case sCMS.Enums.FieldType.Link:
					{
						result = this._data;
						break;
					}
					#endregion
						
					#region IMAGE
					case sCMS.Enums.FieldType.Image:
					{
						result = this._data;
						break;
					}
					#endregion

//					#region IMAGE
//					case sCMS.Enums.FieldType.Image:
//						try
//						{
//							result = SorentoLib.Media.Load (new Guid ((string)this._data));
//						}
//						catch
//						{}
//						break;
//					#endregion

//					#region IMAGELIST
//					case sCMS.Enums.FieldType.ListImage:
//						List<SorentoLib.Media> medias = new List<SorentoLib.Media> ();
//
//						foreach (string mediaid in ((string)this._data).Split (";".ToCharArray (), StringSplitOptions.RemoveEmptyEntries))
//						{
//							try
//							{
//								medias.Add (SorentoLib.Media.Load (new Guid (mediaid)));
//							}
//							catch
//							{}
//						}
//
//						result = medias;
//						break;
//					#endregion

//					#region PAGELIST
//					case sCMS.Enums.FieldType.ListPage:
//						List<sCMS.Page> pages = new List<sCMS.Page> ();
//						foreach (string pageid in ((string)this._data).Split (";".ToCharArray ()))
//						{
//							try
//							{
//								pages.Add (sCMS.Page.Load (new Guid (pageid)));
//							}
//							catch
//							{}
//						}
//
//						result = pages;
//						break;
//					#endregion

//					#region LISTHASHTABLE
//					case sCMS.Enums.FieldType.ListHashtable:
//
//						break;
//					#endregion
				}

				return result;
			}

			set
			{
				switch (this._type)
				{
					#region STRING
					case sCMS.Enums.FieldType.String:						
					{
						this._data = value;
						break;
					}
					#endregion

					#region LISTSTRING
					case sCMS.Enums.FieldType.ListString:
					{
						switch (value.GetType ().FullName.ToLower ())
						{
							#region STRING
							case "system.string":
								this._data = value;
								break;
							#endregion

							#region STRING[]
							default:
								string strings = string.Empty;
								foreach (string s in (string[])value)
								{
									strings += s +"\n";
								}								

								this._data = strings;
								break;
							#endregion
						}
						break;
					}
					#endregion

					#region TEXT
					case sCMS.Enums.FieldType.Text:
					{					
						this._data = value;
						break;
					}
					#endregion
						
					#region LINK
					case sCMS.Enums.FieldType.Link:
					{
						this._data = value;
						break;
					}
					#endregion
						
					#region IMAGE
					case sCMS.Enums.FieldType.Image:
					{					
						this._data = value;
						break;
					}
					#endregion						

//					#region IMAGE
//					case sCMS.Enums.FieldType.Image:
//						// If _content allready contains an IMAGE, make Media PublicTemporary.
////						if ((string)this._data != string.Empty)
////						{
////							try
////							{
////								SorentoLib.Media media = SorentoLib.Media.Load (new Guid ((string)this._data));
////								media.Status = SorentoLib.Enums.MediaStatus.PublicTemporary;
////								media.Save ();
////								media = null;
////							}
////							catch
////							{
////							}
////						}
//
//						// Figure out if a String or Media was passed.
//						switch (value.GetType ().FullName.ToLower ())
//						{
//							#region STRING
//							case "system.string":
//								// Make Media Public.
//								if ((string)value != string.Empty)
//								{
////									try
////									{
////										SorentoLib.Media media = SorentoLib.Media.Load (new Guid ((string)value));
////										media.Status = SorentoLib.Enums.MediaStatus.Public;
////										media.Save ();
////										media = null;
////									}
////									catch
////									{
////										this._data = string.Empty;
////										break;
////									}
//								}
//
//								this._data = value;
//								break;
//							#endregion
//
//							#region MEDIA
//							default:
//								// Make Media Public.
//								((SorentoLib.Media)value).Status = SorentoLib.Enums.MediaStatus.Public;
//								((SorentoLib.Media)value).Save ();
//
//								this._data = ((SorentoLib.Media)value).Id.ToString ();
//								break;
//							#endregion
//						}
//						break;
//					#endregion

//					#region LISTIMAGE
//					case sCMS.Enums.FieldType.ListImage:
//						// If _content allready contains LISTIMAGE, make all Media PublicTemporary.
////						if ((string)this._data != string.Empty)
////						{
////							foreach (string mediaid in ((string)this._data).Split (";".ToCharArray (), StringSplitOptions.RemoveEmptyEntries))
////							{
////								try
////								{
////									SorentoLib.Media media = SorentoLib.Media.Load (new Guid (mediaid));
////									media.Status = SorentoLib.Enums.MediaStatus.PublicTemporary;
////									media.Save ();
////									media = null;
////								}
////								catch
////								{}
////							}
////						}
//
//						// Figure out if a String or List<Media> was passed.
//						switch (value.GetType ().FullName.ToLower ())
//						{
//							#region string
//							case "system.string":
//								// Make all Media Public.
////								foreach (string mediaid in ((string)value).Split (";".ToCharArray (), StringSplitOptions.RemoveEmptyEntries))
////								{
////									SorentoLib.Media media = SorentoLib.Media.Load (new Guid (mediaid));
////									media.Status = SorentoLib.Enums.MediaStatus.Public;
////									media.Save ();
////									media = null;
////								}
//
//								this._data = value;
//								break;
//							#endregion
//
//							#region list
//							default:
//								string mediaids = string.Empty;
//								foreach (SorentoLib.Media media in (List<SorentoLib.Media>)value)
//								{
////									media.Status = SorentoLib.Enums.MediaStatus.Public;
////									media.Save ();
//									mediaids += media.Id.ToString () +";";
//								}
//								mediaids = mediaids.TrimEnd(";".ToCharArray ());
//
//								this._data = mediaids;
//								break;
//							#endregion
//						}
//						break;
//					#endregion

//					#region LISTPAGE
//					case sCMS.Enums.FieldType.ListPage:
//						// Figure out if a String or List<Page> was passed.
//						switch (value.GetType ().FullName.ToLower ())
//						{
//							#region STRING
//							case "system.string":
//								this._data = value;
//								break;
//							#endregion
//	
//							#region LIST
//							default:
//								string pageids = string.Empty;
//								foreach (sCMS.Page page in (List<sCMS.Page>)value)
//								{
//									pageids += page.Id.ToString () +";";
//								}
//								pageids = pageids.TrimEnd(";".ToCharArray ());
//	
//								this._data = pageids;
//								break;
//							#endregion
//						}
//	
//						break;
//					#endregion

//					#region HASHTABLE
//					case sCMS.Enums.FieldType.ListHashtable:
//						// Figure out if a String or Hashtable was passed.
//						switch (value.GetType ().FullName.ToLower ())
//						{
//							#region STRING
//							case "system.string":
//								this._data = value;
//								break;
//							#endregion
//
//							#region HASHTABLE
//							default:
//								string hashtables = string.Empty;
//
//								foreach (Hashtable hashtable in (List<Hashtable>)value)
//								{
//									string item = string.Empty;
//									foreach (string key in hashtable)
//									{
//										item += key +"_|_"+ hashtable[key] +"_||_";
//
//
//
//
//									}
//
//
//								}
////								pageids = pageids.TrimEnd(";".ToCharArray ());
//
//								this._data = items;
//								break;
//							#endregion
//						}
//
//						break;
//					#endregion
				}
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

