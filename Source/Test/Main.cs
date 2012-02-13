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
using Mono.CSharp;
using System.Threading;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using SNDK.DBI;
using SNDK.Enums;
using System.Xml;

using System.Reflection;

using SorentoLib;
using sCMS;
//using sBlog;
//using Akvabase;

namespace Test
{
	class MainClass
	{
		public static void Main (string[] args)
		{
//			Evaluator.Compile ("using SorentoLib; Test (object lala) {};");



			SorentoLib.Services.Database.Connection = new Connection (SNDK.Enums.DatabaseConnector.Mysql,
//			                                                            "localhost",
			                                                            "10.0.0.40",
//			                                                            "sorento",
								"sorentotest.sundown.dk",
			                                                            "sorentotest",
			                                                            "scumbukket",
			                                                            true);

			SorentoLib.Services.Database.Prefix = "sorento_";
			SorentoLib.Services.Database.Connection.Connect ();

			SorentoLib.Services.Config.Initialize ();

//			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_temp, "/home/rvp/Skrivebord/mediatest/temp/");
//			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_media, "/home/rvp/Skrivebord/mediatest/media/");
//			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_publicmedia, "/home/rvp/Skrivebord/mediatest/public/");

			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_temp, "/home/sundown/Skrivebord/mediatest/temp/");
			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_media, "/home/sundown/Skrivebord/mediatest/media/");
			SorentoLib.Services.Config.Set (SorentoLib.Enums.ConfigKey.path_publicmedia, "/home/sundown/Skrivebord/mediatest/public/");
			
			
			CollectionSchema schema = new CollectionSchema ("TEST SCHEMA");
			
			Field field1 = new Field (sCMS.Enums.FieldType.String, "FIELD 1", 1);
			Field field2 = new Field (sCMS.Enums.FieldType.String, "FIELD 2", 2);
			
			schema.AddField (field1);
			schema.AddField (field2);
									
			schema.Save ();
			
			Collection collection1 = new Collection (schema, "Collection 1", 1);
			collection1.SetContent (field1.Id, "Test string #1");
			collection1.SetContent (field2.Id, "Test string #2");
			collection1.Save ();
			
			Collection collection2 = new Collection (schema, "Collection 2", 2);			
			collection2.SetContent (field1.Id, "Test string #3");
			collection2.SetContent (field2.Id, "Test string #4");
			collection2.Save ();
//			Environment.Exit (0);
						
			foreach (CollectionSchema c in CollectionSchema.List ())
			{
				Console.WriteLine ("CollectionSchema: "+ c.Title);
				
				foreach (Collection col in c.Collections)
				{
					Console.WriteLine ("\t Collection: "+ col.Title);
					
					foreach (Field f in c.Fields)
					{
						Console.WriteLine ("\t\t Content: "+ f.Name +" = "+ col.GetContent (f));					
					}					
				}
				
				CollectionSchema.Delete (c.Id);
			}
			
			
		}
	}
}
