// 
//  Runtime.cs
//  
//  Author:
//       sundown <${AuthorEmail}>
// 
//  Copyright (c) 2012 rvp
// 
//  This program is free software; you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation; either version 2 of the License, or
//  (at your option) any later version.
// 
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
//  GNU General Public License for more details.
//  
//  You should have received a copy of the GNU General Public License
//  along with this program; if not, write to the Free Software
//  Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
// 
using System;
using System.IO;
using System.Xml;
using Mono.Unix;

using sConsole;
using SorentoLib;

namespace sCMS
{
	public static class Runtime
	{
		#region Public Static Fields
		public static Usergroup UsergroupGuest;
		public static Usergroup UsergroupUser;
		public static Usergroup UsergroupModerator;
		public static Usergroup UsergroupAuthor;
		public static Usergroup UsergroupEditor;
		public static Usergroup UsergroupAdministrator;		
		
//		public static Media DefaultMedia;
		#endregion
		
		public static void Initialize ()
		{				
			try
			{
				// Set defaults
				SetDefaults ();
												
				sConsole.Menu.AddCategory ("scms", "Content Management", 10);
				sConsole.Menu.AddItem ("scms", "content", "Content", "/scms/content/", 1);
				sConsole.Menu.AddItem ("scms", "styling", "Construction", "/scms/construction/", 2);
				sConsole.Menu.AddItem ("scms", "settings", "Settings", "/scms/settings/", 3);
									
				// Remove current symlinks
				SNDK.IO.RemoveSymlink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/resources/content/scms");				
				SNDK.IO.RemoveSymlink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/resources/includes/scms");
				SNDK.IO.RemoveSymlink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/resources/xml/scms");
							
				// Create symlinks
				SNDK.IO.CreateSymlink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sCMS/resources/content", SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/resources/content/scms");					
				SNDK.IO.CreateSymlink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sCMS/resources/htdocs", SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/resources/includes/scms");
				SNDK.IO.CreateSymlink (SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sCMS/resources/xml", SorentoLib.Services.Config.Get<string> (SorentoLib.Enums.ConfigKey.path_addins) + "sConsole/resources/xml/scms");
								
				Include.Add (sConsole.Enums.IncludeType.Javascript, "/includes/scms/js/scms.js", "SCMS", 101);
				Include.Add (sConsole.Enums.IncludeType.Javascript, "/includes/sndk/includes/codemirror/mode/css/css.js", "SCMS", 101);
				Include.Add (sConsole.Enums.IncludeType.Javascript, "/includes/sndk/includes/codemirror/mode/javascript/javascript.js", "SCMS", 101);
				Include.Add (sConsole.Enums.IncludeType.Javascript, "/includes/sndk/includes/codemirror/mode/htmlmixed/htmlmixed.js", "SCMS", 101);								
				Include.Add (sConsole.Enums.IncludeType.Javascript, "/includes/sndk/includes/codemirror/mode/xml/xml.js", "SCMS", 101);	
				
				Include.Add (sConsole.Enums.IncludeType.Javascript, "/includes/sndk/includes/tinymce/tiny_mce.js", "SCMS", 101);	
						
			
				// Set default usergroups
//			UsergroupGuest = Usergroup.AddBuildInUsergroup (new Guid ("10de93c3-4d70-445a-8457-97beefd6809d"), "sCMS Guest");			
//			UsergroupUser = Usergroup.AddBuildInUsergroup (new Guid ("50176827-ae08-42cf-aefb-2db768a9b9e1"), "sCMS User");
//			UsergroupModerator = Usergroup.AddBuildInUsergroup (new Guid ("a9754690-646c-4971-96cd-ba16716dcfe4"), "sCMS Moderator");
				UsergroupEditor =	Usergroup.AddBuildInUsergroup (new Guid ("a0a2cf36-fa74-43a7-8f5f-34aa837cccef"), "sCMS Editor");
				UsergroupAuthor =	Usergroup.AddBuildInUsergroup (new Guid ("20e8185e-18a2-4178-a99a-157c2a360426"), "sCMS Author");
				UsergroupAdministrator = Usergroup.AddBuildInUsergroup (new Guid ("3b9ca3c0-67e9-4750-aacf-51b8faa4527f"), "sCMS Administrator");
				
				// Create default media
				
//				string xml = "<sorentolib.media>" +
//					"<id type=\"string\">" +
//						"<![CDATA[2d5ac75f-8438-4bca-b45a-5e2c4ade613e]]>" +
//					"</id>" +
//					"<type type=\"string\">" +
//						"<![CDATA[Public]]>" +
//					"</type>" +
//					"<mimetype type=\"string\">" +
//						"<![CDATA[image/svg+xml]]>" +
//					"</mimetype>" +
//					"<path type=\"string\">" +
//						"<![CDATA[console/includes/scms/css/images/icons/process-stop.svg]]>" +
//					"</path>	" +
//					"</sorentolib.media>";
//					
//				DefaultMedia = Media.FromXmlDocument (SNDK.Convert.StringToXmlDocument (xml));
				
				// GARBAGE COLLECTOR
				SorentoLib.Services.Events.ServiceGarbageCollector += EventhandlerServiceGarbageCollector;
				
			}
			catch (Exception exception)
			{
				// LOG: LogDebug.ExceptionUnknown
				SorentoLib.Services.Logging.LogDebug (string.Format (SorentoLib.Strings.LogDebug.ExceptionUnknown, "SCMS.INITIALIZE", exception.Message));
			}				
		}
		
		private static void SetDefaults ()
		{			
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_templateplaceholdertag, "[PLACEHOLDER_STYLESHEETS]");
			
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_stylesheetpath, "../../html/css/");
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_stylesheetfileextension, ".css");
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_stylesheeturl, "/css/");
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_stylesheethtmltag, "<link rel=\"stylesheet\" href=\"{0}\" type=\"text/css\" />");
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_stylesheetencoding, "ISO-8859-15");
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_stylesheetplaceholdertag, "[PLACEHOLDER_STYLESHEETS]");
			
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_javascriptpath, "../../html/js/");
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_javascriptfileextension, ".js");
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_javascripturl, "/js/");
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_javascripthtmltag, "<script language=\"JavaScript\" type=\"text/javascript\" src=\"{0}\"></script>");
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_javascriptencoding, "ISO-8859-15");
			SorentoLib.Services.Config.SetDefault (Enums.ConfigKey.scms_javascriptplaceholdertag, "[PLACEHOLDER_JAVASCRIPTS]");
		}

		static void EventhandlerServiceGarbageCollector (object Sender, EventArgs E)
		{
			Global.ServiceGarbageCollector ();
		}			
	}
}

