//
// Stylesheet.cs
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
using System.IO;
using System.Xml;
using System.Text;
using System.Collections;
using System.Collections.Generic;

using SNDK;

namespace sCMS
{
	public class Stylesheet
	{
		#region Private Fields
		private string _filename;
		private string _content;

		private bool _contentloaded;
		#endregion

		#region Public Fields
		public string Id
		{
			get
			{
				return this._filename;
			}
		}

		public string Title
		{
			get
			{
				return Path.GetFileNameWithoutExtension (this._filename);
			}
		}

		public string Filename
		{
			get
			{
				return this._filename;
			}
		}

		public string Content
		{
			get
			{
				if (!this._contentloaded)
				{
					try
					{
						foreach (string line in SNDK.IO.ReadTextFile (SorentoLib.Services.Config.Get<string>(sCMS.Enums.ConfigKey.scms_stylesheetpath) + this._filename, Encoding.GetEncoding (SorentoLib.Services.Config.Get<string>(sCMS.Enums.ConfigKey.scms_stylesheetencoding))))
						{
							this._content += line +"\n";
						}
					}
					catch
					{
						this._content = string.Empty;
						SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.StylesheetLoadContent, this.Filename));
					}
					this._contentloaded = true;
				}

				return this._content;
			}

			set
			{
				this._content = value;
			}
		}
		#endregion

		#region Constructor
		public Stylesheet (string Title)
		{
			this._filename = Path.GetFileNameWithoutExtension (Helpers.MakeStringURLSafe (Title)) + SorentoLib.Services.Config.Get<string> (Enums.ConfigKey.scms_stylesheetfileextension);
			this._content = string.Empty;
			this._contentloaded = false;

			this._filename = SNDK.IO.IncrementFilename (SorentoLib.Services.Config.Get<string>(sCMS.Enums.ConfigKey.scms_stylesheetpath) + this._filename);
		}

		private Stylesheet ()
		{
			this._filename = string.Empty;
			this._content = string.Empty;
			this._contentloaded = false;
		}
		#endregion
		
		#region Public Methods
		public void Save ()
		{
			SNDK.IO.WriteTextFile (SorentoLib.Services.Config.Get<string>(sCMS.Enums.ConfigKey.scms_stylesheetpath) + this._filename, this._content, Encoding.GetEncoding (SorentoLib.Services.Config.Get<string>(sCMS.Enums.ConfigKey.scms_stylesheetencoding)));
		}
		
		public XmlDocument ToXmlDocument ()
		{
			Hashtable result = new Hashtable ();

			result.Add ("id", this.Id);
			result.Add ("filename", this._filename);
			result.Add ("title", this.Title);
			result.Add ("content", this.Content);
			
			return SNDK.Convert.ToXmlDocument (result, this.GetType ().FullName.ToLower ());
		}		
		#endregion

		#region Public Static Methods
		public static Stylesheet Load (string Id)
		{
			Stylesheet stylesheet = new Stylesheet ();
			stylesheet._filename = Path.GetFileNameWithoutExtension (Id) + SorentoLib.Services.Config.Get<string> (Enums.ConfigKey.scms_stylesheetfileextension);

			return stylesheet;
		}

		public static void Delete (string Id)
		{
			try
			{
				File.Delete (SorentoLib.Services.Config.Get<string> (sCMS.Enums.ConfigKey.scms_stylesheetpath) + Path.GetFileNameWithoutExtension (Id) + SorentoLib.Services.Config.Get<string> (Enums.ConfigKey.scms_stylesheetfileextension));
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.STYLESHEET", exception.Message));
				
				// EXCEPTION: Exception.StylesheetDelete
				throw new Exception (string.Format (Strings.Exception.StylesheetDelete, Id.ToString ()));
			}							
		}

		public static List<Stylesheet> List ()
		{
			List<Stylesheet> result = new List<Stylesheet> ();

			foreach (string path in Directory.GetFiles (SorentoLib.Services.Config.Get<string>(sCMS.Enums.ConfigKey.scms_stylesheetpath)))
			{
				try
				{
					result.Add (Load (Path.GetFileName (path)));
				}
				catch
				{
					SorentoLib.Services.Logging.LogDebug (string.Format (Strings.LogDebug.StylesheetList, path));
				}
			}

			return result;
		}
		
		public static Stylesheet FromXmlDocument (XmlDocument xmlDocument)
		{
			Hashtable item;
			Stylesheet result;
			
			try
			{				
				item = (Hashtable)SNDK.Convert.FromXmlDocument (SNDK.Convert.XmlNodeToXmlDocument (xmlDocument.SelectSingleNode ("(//scms.stylesheet)[1]")));
			}
			catch
			{
				item = (Hashtable)SNDK.Convert.FromXmlDocument (xmlDocument);
			}
						
			if (item.ContainsKey ("id"))
			{
				try
				{
					result = Load ((string)item["id"]);
				}
				catch
				{
					result = new Stylesheet ((string)item["id"]);
				}
			}
			else
			{
				throw new Exception (Strings.Exception.StylesheetFromXMLDocument);
			}

			if (item.ContainsKey ("content"))
			{
				result.Content = (string)item["content"];
			}

			return result;
		}		
		#endregion
	}
}
