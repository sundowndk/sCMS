//
// PageResponder.cs
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
using System.Collections.Generic;

using SorentoLib;

namespace sCMS.Addin
{
	public class PageResponder : SorentoLib.Addins.IPageResponder
	{
		#region Constructor
		public PageResponder ()
		{
		}
		#endregion

		#region Public Methods
		public bool Process (SorentoLib.Session Session)
				{
						string path = string.Empty;
						if (Session.Request.QueryJar.Exist ("cmd.path"))
						{
								path = Session.Request.QueryJar.Get ("cmd.path").Value + "/";
						}

						Page page = null;

						// TODO: Responder needs to be fixed.
						try
						{
				
				
//				page = Page.Load (Session.Request.QueryJar.Get ("cmd.page").Value.TrimStart ("/".ToCharArray ()));
										
//				if (SorentoLib.Services.Config.Get<bool> ("core","enabled"))
//				{
//					page = Page.Load (Session.Request.QueryJar.Get ("cmd.page").Value.TrimStart ("/".ToCharArray ()));
				
//				Console.WriteLine ("SCMS: "+ Session.Request.QueryJar.Get ("cmd.page").Value);
				
								page = Page.Load (Session.Request.QueryJar.Get ("cmd.page").Value);

//			}
//				else
//				{
//					page = Page.Load (SorentoLib.Services.Config.Get<Guid> ("scms","erroroffline"));
//				}
						} catch
						{
								return false;
						}
			
//			return false;

						List<SorentoLib.ParserVariable> variables = new List<ParserVariable> ();
						variables.Add (new ParserVariable ("Session", Session));


			variables.Add (new ParserVariable ("PAGE", page));			

			foreach (sCMS.Field field in page.Template.AllFields)
			{
				variables.Add (new ParserVariable (field.Name, page.GetContent (field.Id)));			
			}

			foreach (Global global in Global.List ())
			{
				variables.Add (new ParserVariable ( "G"+ global.Name, global.Content.Data));			
			}

			string bla = string.Empty;
			foreach (string line in page.Template.Build ())
			{
				bla += line +"\n";
			}

			SorentoLib.Parser parser = new SorentoLib.Parser (new SorentoLib.Template (bla), variables);






//			SorentoLib.Render.Template template = new SorentoLib.Render.Template (Session, page.Template.Build ());
			
//			Session.Page.Variables.Add ("!PAGE", page);

//			foreach (sCMS.Field field in page.Template.AllFields)
//			{
//				Content content = page.GetContent (field.Id);
				
//				Console.WriteLine (field.Name);
//				Console.WriteLine (page.GetContent (field.Id));
				
//				Session.Page.Variables.Add (field.Name, page.GetContent (field.Id));
				
//				Content content = page.Contents.Find (delegate (Content c) { return c.FieldId == field.Id; });
//				if (content != null)
//				{
//					Session.Page.Variables.Add ("!"+ field.Name, content.Data);
//				}
//				else
//				{
//					Session.Page.Variables.Add ("!"+ field.Name, field.DefaultValue);
//				}
//			}

//			foreach (Global global in Global.List ())
//			{
//				Console.WriteLine (global.Name +" "+ global.Content.ToString ());
//				Session.Page.Variables.Add ("@"+ global.Name, global.Content.Data);
//			}

//			template.Render ();
//			template = null;

			Session.Responder.Request.SendOutputText (SorentoLib.Page2.Write (Session, parser.Output));

//			Session.Responder.Request.SendOutputText (Session.Page.Write (Session));


			return true;
		}
		#endregion
	}
}
