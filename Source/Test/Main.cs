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
//			                                                            "10.0.0.40",
//			                                                            "sorento",
			                                                          "sorentotest.sundown.dk",
			                                                          "sorentotest",
			                                                          "qwerty",
			                                                          true);

			SorentoLib.Services.Database.Prefix = "sorento_";
			SorentoLib.Services.Database.Connection.Connect ();

			
			sCMS.Page p1 = sCMS.Page.Load ("/produkter/hostedexchange");
			
			
			
			Console.WriteLine (p1.Title);
			
			Console.WriteLine (p1.IsParent (new Guid ("fde78197-ac5c-439e-8a3a-eaddf5ad2976")));
			Console.WriteLine (p1.IsParent (new Guid ("df800f14-17d7-4e60-8484-9fed8b6b704a")));
			
			
		}
	}
}
