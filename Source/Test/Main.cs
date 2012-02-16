// 
// Main.cs
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
using Mono.CSharp;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using SNDK.DBI;
using SNDK.Enums;
using System.Xml;
using Mono.Unix;

using System.Reflection;

using SorentoLib;
using sCMS;

namespace Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			SorentoLib.Services.Database.Connection = new Connection (SNDK.Enums.DatabaseConnector.Mysql,
				"localhost",
//				"10.0.0.40",
				"sorento",
//				"sorentotest.sundown.dk",
				"sorentotest",
				"scumbukket",
				true);

			SorentoLib.Services.Database.Prefix = "sorento_";
			SorentoLib.Services.Database.Connection.Connect ();

			SorentoLib.Services.Config.Initialize ();

			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_temp, "/home/rvp/Skrivebord/mediatest/temp/");
			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_media, "/home/rvp/Skrivebord/mediatest/media/");
			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_publicmedia, "/home/rvp/Skrivebord/mediatest/public/");
			SorentoLib.Services.Config.Set (sCMS.Enums.ConfigKey.scms_stylesheetpath, "/home/rvp/Skrivebord/stylesheettest/");						
			
//			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_temp, "/home/sundown/Skrivebord/mediatest/temp/");
//			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_media, "/home/sundown/Skrivebord/mediatest/media/");
//			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_publicmedia, "/home/sundown/Skrivebord/mediatest/public/");			
//			SorentoLib.Services.Config.Set (sCMS.Enums.ConfigKey.scms_stylesheetpath, "/home/sundown/Skrivebord/stylesheettest/");
			
			SorentoLib.Services.Config.Set (sCMS.Enums.ConfigKey.scms_stylesheeturl, "/css/");			
			SorentoLib.Services.Config.Set (sCMS.Enums.ConfigKey.scms_stylesheetencoding, "UTF-8");
			SorentoLib.Services.Config.Set (sCMS.Enums.ConfigKey.scms_stylesheetfileextension, ".css");
			SorentoLib.Services.Config.Set (sCMS.Enums.ConfigKey.scms_stylesheethtmltag, "<link rel=\"stylesheet\" href=\"{0}\" type=\"text/css\" />");
			SorentoLib.Services.Config.Set (sCMS.Enums.ConfigKey.scms_stylesheetplaceholdertag, "[STYLESHEET_PLACEHOLDER]");
			SorentoLib.Services.Config.Set (sCMS.Enums.ConfigKey.scms_templateplaceholdertag, "[CHILD_TEMPLATE_PLACEHOLDER]");
			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_addins, "Addins/");
						
//			if (!Directory.Exists (Services.Config.Get<string> (Enums.ConfigKey.path_publicmedia) + System.IO.Path.GetDirectoryName (this._temppath)))
//			{
//			}

				// Create a new symlink.
			
			if (!Directory.Exists (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/content/scms"))
			{
				UnixFileInfo dirinfo = new UnixFileInfo (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sCMS/data/content/scms");			
				dirinfo.CreateSymbolicLink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/content/scms");
			}
			
			if (!Directory.Exists (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/html/xml/scms"))
			{
				UnixFileInfo dirinfo = new UnixFileInfo (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sCMS/data/html/xml/scms");
				dirinfo.CreateSymbolicLink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/html/xml/scms");
			}
			
			if (!File.Exists (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/html/js/scms.js"))
			{
				UnixFileInfo dirinfo = new UnixFileInfo (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sCMS/data/html/js/scms.js");
				dirinfo.CreateSymbolicLink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/data/html/js/scms.js");
			}
			
//			UnixFileInfo fileinfo = new UnixFileInfo (this.DataPath);
//			unixfileinfo.CreateSymbolicLink (Services.Config.Get<string> (Enums.ConfigKey.path_publicmedia) + this._temppath);			
			
			
			
			Environment.Exit (0);
			
			
			Stylesheet stylesheet1 = new Stylesheet ("Default1");
			stylesheet1.Content = "STYLES";
			stylesheet1.Save ();			
			
			Stylesheet stylesheet2 = new Stylesheet ("Default2");
			stylesheet2.Content = "STUFF";
			stylesheet2.Save ();			
								
			Template template1 = new Template ();
			template1.Title = "Test Template";
			template1.Content = "BLA BLA BLA\n[CHILD_TEMPLATE_PLACEHOLDER]\nBLS BLS BLS\n[STYLESHEET_PLACEHOLDER]\n";
			template1.AddStylesheet (stylesheet1);			
			
			Field field1 = new Field(sCMS.Enums.FieldType.Text, "Field #1", 0);
			Field field2 = new Field(sCMS.Enums.FieldType.Text, "Field #2", 1);
			Field field3 = new Field(sCMS.Enums.FieldType.Text, "Field #3", 2);
			
			template1.AddField (field1);
			template1.AddField (field2);
			template1.AddField (field3);
			
			template1.Save ();
						
			Template template2 = new Template ();
			template2.ParentId = template1.Id;
			template2.Title = "Test Template 2";
			template2.Content = "BLU BLU BLU";
			template2.AddStylesheet (stylesheet2);
			
			Field field4 = new Field(sCMS.Enums.FieldType.Text, "Field #4", 0);
			Field field5 = new Field(sCMS.Enums.FieldType.Text, "Field #5", 1);
			Field field6 = new Field(sCMS.Enums.FieldType.Text, "Field #6", 2);
			
			template2.AddField (field4);
			template2.AddField (field5);
			template2.AddField (field6);
			
			template2.Save ();
			
			
			sCMS.Page page1 = new sCMS.Page (template2);
			page1.Title = "testpage1";
			page1.SetContent (field1, "TEST STRING ON TESTPAGE1");
			page1.SetContent (field2, "MORE STRING ON TESTPAGE1");
			page1.Aliases.Add ("index");
			page1.Aliases.Add ("test");
			page1.Save ();
			
			sCMS.Page page2 = new sCMS.Page (template2);
			page2.Parent = page1;
			page2.Title = "subtestpage";
			page2.SetContent (field3, "SOME SUB TEXT");
			page2.Aliases.Add ("LALA");
			page2.Save ();
			
			foreach (Template t in Template.List ())
			{
				Console.WriteLine (t.Title);
				if (t.ParentId != Guid.Empty)
				{
					Template parent = Template.Load (t.ParentId);
					
					Console.WriteLine ("\t Parent: "+ parent.Title);
				}
				
				Console.WriteLine ("\t Content: ");
				foreach (string l in t.Build ())
				{
					Console.WriteLine (l);
				}
				
				Console.WriteLine ("\t Fields: ");
				foreach (Field f in t.Fields)
				{
					Console.WriteLine ("\t\t Field: "+ f.Name);
				}
				
				Console.WriteLine ();						
			}
			
			foreach (sCMS.Page p in sCMS.Page.List ())
			{
				Console.WriteLine (p.Title);
				Console.WriteLine ("\t Path: "+ p.Path);
				Console.WriteLine ("\t Aliases:");				
				foreach (string a in p.Aliases)
				{
					Console.WriteLine ("\t\t "+ a);
				}
				
				Console.WriteLine ("");
				
				Console.WriteLine ("\t Contents:");
				foreach (Field f in p.Template.Fields)
				{
					Console.WriteLine ("\t\t "+ f.Name +" "+ p.GetContent (f));
				}
			}
			
			Console.WriteLine ("");
			
			sCMS.Page page3 = sCMS.Page.Load ("/testpage1/subtestpage");
			Console.WriteLine (page3.Title);
			
			
			
			foreach (Template t in Template.List ())
			{
				try
				{
					Template.Delete (t.Id);
				}
				catch
				{					
				}
			}
			
			foreach (sCMS.Page p in sCMS.Page.List ())
			{
				try
				{
					sCMS.Page.Delete (p.Id);
				}
				catch
				{				
				}
			}
			
			foreach (Stylesheet s in Stylesheet.List ())
			{
				Stylesheet.Delete (s.Id);
			}
			
			
			
			
			
			
			
//			CollectionSchema schema = new CollectionSchema ("TEST SCHEMA");
//			
//			Field field1 = new Field (sCMS.Enums.FieldType.String, "FIELD 1", 1);
//			Field field2 = new Field (sCMS.Enums.FieldType.String, "FIELD 2", 2);
//			
//			schema.AddField (field1);
//			schema.AddField (field2);
//									
//			schema.Save ();
//			
//			Collection collection1 = new Collection (schema, "Collection 1", 1);
//			collection1.SetContent (field1.Id, "Test string #1");
//			collection1.SetContent (field2.Id, "Test string #2");
//			collection1.Save ();
//			
//			Collection collection2 = new Collection (schema, "Collection 2", 2);			
//			collection2.SetContent (field1.Id, "Test string #3");
//			collection2.SetContent (field2.Id, "Test string #4");
//			collection2.Save ();
//						
//			foreach (CollectionSchema c in CollectionSchema.List ())
//			{
//				Console.WriteLine ("CollectionSchema: "+ c.Title);
//				
//				foreach (Collection col in c.Collections)
//				{
//					Console.WriteLine ("\t Collection: "+ col.Title);
//					
//					foreach (Field f in c.Fields)
//					{
//						Console.WriteLine ("\t\t Content: "+ f.Name +" = "+ col.GetContent (f));					
//					}					
//				}
//				
//				CollectionSchema.Delete (c.Id);
//			}
			
			
		}
	}
}
