//
// Exception.cs
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

namespace sCMS.Strings
{
	public class Exception
	{
		#region TEMPLATE
		public static string TemplateLoad = "TEMPLATE with id: {0} was not found.";
		public static string TemplateSave = "Could not save TEMPLATE with id: {0}";
		public static string TemplateDelete = "Could not delete TEMPLATE with id: {0}";
		public static string TemplateFromXMLDocument = "Failed to create TEMPLATE from XMLDocument.";
		public static string TemplateCompileContent = "Failed to compile template content.";
		public static string TemplateCompileStylesheet = "Failed to compile template stylesheets.";
		public static string TemplateCompileJavascript = "Failed to compile template javascripts.";
		public static string TemplateCompileField = "Failed to compile template fields.";		
 		#endregion

		#region PAGE
		public static string PageLoadGuid = "PAGE with id: {0} was not found.";
		public static string PageLoadPath = "PAGE with path: {0} was not found.";
		public static string PageSave = "Could not save PAGE with id: {0}";
		public static string PageDelete = "Could not delete PAGE with id: {0}";
		public static string PageGetGuid = "PAGE does not contain a FIELD with id: {0}";
		public static string PageGetName = "PAGE does not contain a FIELD with name: {0}";
		public static string PageFromAjaxItem = "Could not create PAGE from AjaxItem, missing key: {0}";
		public static string PageGetContent = "Could not get content. No content or field with id: {0}";
		public static string PageSetContent = "Could not set content. No content with id: {0}";
		public static string PageFromXMLDocument = "Failed to create PAGE from XMLDocument.";
		#endregion
		
		
		#region FIELD
		public static string FieldFromXMLDocument = "Failed to create FIELD from XMLDocument.";
		#endregion
		
		#region CONTENT		
		public static string ContentFromXMLDocument = "Failed to create CONTENT from XMLDocument.";
		#endregion
		
		
		
		#region ROOT
		public static string RootLoad = "ROOT with id: {0} was not found.";
		public static string RootSave = "Could not save ROOT with id: {0}";
		public static string RootDelete = "Could not delete ROOT with id: {0}";
		public static string RootFromAjaxItem = "Could not create ROOT from AjaxItem, missing key: {0}";
		public static string RootFromXMLDocument = "Failed to create ROOT from XMLDocument.";
		#endregion

		#region RootFilter
		public static string RootFilterFromAjaxItem = "Could not create ROOTFILTER from AjaxItem, missing key: {0}";
		public static string RootFilterFromXMLDocument = "Failed to create ROOTFILTER from XMLDocument.";
		#endregion

		#region Global
		public static string GlobalLoadGuid = "GLOBAL with id: {0} was not found.";
		public static string GlobalLoadName = "GLOBAL with name: {0} was not found.";
		public static string GlobalSave = "Could not save GLOBAL with id: {0}";
		public static string GlobalDelete = "Could not delete GLOBAl with id: {0}";
		#endregion

		#region Collection
		public static string CollectionLoadGuid = "COLLECTION with id: {0} was not found.";
		public static string CollectionSave = "Could not save COLLECTION with id: {0}";
		public static string CollectionDelete = "Could not delete COLLECTION with id: {0}";
		public static string CollectionSetContent = "Could not set content. No content with id: {0}";
		public static string CollectionGetContent = "Could not get content. No content or field with id: {0}";
		public static string CollectionFromAjaxItem = "Could not create COLLECTION from AjaxItem, missing key: {0}";
		public static string CollectionFromXMLDocument = "Failed to create COLLECTION from XMLDocument.";
		#endregion

		#region CollectionSchema
		public static string CollectionSchemaLoadGuid = "COLLETIONSCHEMA with id: {0} was not found.";
		public static string CollectionSchemaSave = "Could not save COLLECTIONSCHEMA with id: {0}";
		public static string CollectionSchemaDelete = "Could not delete COLLECTIONSCHEMA with id: {0}";
		public static string CollectionSchemaFromAjaxItem = "Could not create COLLECTIONSCHEMA from AjaxItem, missing key: {0}";		
		public static string CollectionSchemaFromXMLDocument = "Failed to create COLLECTIONSCHEMA from XMLDocument.";
		#endregion
		
		#region Javascript
		public static string StylesheetFromXMLDocument = "Failed to create STYLESHEET from XMLDocument.";
		public static string StylesheetDelete = "Could not delete STYLESHEET with id: {0}";
		#endregion
		
		#region Javascript
		public static string JavascriptFromXMLDocument = "Failed to create JAVASCRIPT from XMLDocument.";
		public static string JavascriptDelete = "Could not delete JAVASCRIPT with id: {0}";
		#endregion		
		
		#region RESOLVER
		public static string ResolverMethodNotFound = "[sCMS]: Method {0} was not found.";
		public static string ResolverSessionPriviliges = "[sCMS]: Current session does not have priviliges to access method '{0}'";
		#endregion

		#region FUNCTION
		public static string FunctionMethodNotFound = "[Autoform]: Method '{0}' was not found.";
		#endregion

		#region AJAX
		public static string AjaxMethodNotFound = "[sCMS]: Method '{0}' was not found.";
		public static string AjaxSessionPriviliges = "[sCMS]: Current session does not have priviliges to access method '{0}'";
		#endregion
	}
}

