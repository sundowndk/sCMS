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

using sConsole;

namespace sCMS
{
	public static class Runtime
	{
		public static void Initialize ()
		{				
			sConsole.Menu.AddCategory ("scms", "sCMS", 10);
			sConsole.Menu.AddItem ("scms", "content", "Content", "/scms/content/", 1);
			sConsole.Menu.AddItem ("scms", "styling", "Construction", "/scms/construction/", 2);
			sConsole.Menu.AddItem ("scms", "settings", "Settings", "/scms/settings/", 3);
			
			// Set defaults
			SetDefaults ();
			
//			UsergroupGuest = Usergroup.AddBuildInUsergroup (new Guid ("2b46cce5-0234-4fb7-a226-acc676a093c9"), "Guest");
//			UsergroupUser = Usergroup.AddBuildInUsergroup (new Guid ("476b824f-86a1-4d8d-baff-f341b110ef08"), "User");
//			UsergroupModerator = Usergroup.AddBuildInUsergroup (new Guid ("76b80364-bc8f-4177-8c08-26697ac8dfbd"), "Moderator");
//			UsergroupAuthor =	Usergroup.AddBuildInUsergroup (new Guid ("8016152c-bb4c-4af0-ad4e-8867f196e334"), "Author");
//			UsergroupEditor =	Usergroup.AddBuildInUsergroup (new Guid ("c7be09c5-e23f-4a9f-b218-28b982299a54"), "Editor");
//			UsergroupAdministrator = Usergroup.AddBuildInUsergroup (new Guid ("c76e32de-7e4c-4152-868f-e450d0a6c145"), "Administrator");

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
		}
	}
}

