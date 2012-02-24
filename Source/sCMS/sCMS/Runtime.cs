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
		
				// Create symlinks if they dont exist.
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
										
				Include.Add (sConsole.Enums.IncludeType.Javascript, "/js/scms.js", "SCMS", 101);
			
				// Set default usergroups
//			UsergroupGuest = Usergroup.AddBuildInUsergroup (new Guid ("10de93c3-4d70-445a-8457-97beefd6809d"), "sCMS Guest");			
//			UsergroupUser = Usergroup.AddBuildInUsergroup (new Guid ("50176827-ae08-42cf-aefb-2db768a9b9e1"), "sCMS User");
//			UsergroupModerator = Usergroup.AddBuildInUsergroup (new Guid ("a9754690-646c-4971-96cd-ba16716dcfe4"), "sCMS Moderator");
				UsergroupEditor =	Usergroup.AddBuildInUsergroup (new Guid ("a0a2cf36-fa74-43a7-8f5f-34aa837cccef"), "sCMS Editor");
				UsergroupAuthor =	Usergroup.AddBuildInUsergroup (new Guid ("20e8185e-18a2-4178-a99a-157c2a360426"), "sCMS Author");
				UsergroupAdministrator = Usergroup.AddBuildInUsergroup (new Guid ("3b9ca3c0-67e9-4750-aacf-51b8faa4527f"), "sCMS Administrator");
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
	}
}

