/**
 * @license Copyright (c) 2003-2022, CKSource Holding sp. z o.o. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
	// config.uiColor = '#AADC6E';

	//config.extraPlugins = 'syntaxhighlight';
	config.syntaxhighlight_lang = 'csharp';
	config.syntaxhighlight_hideControls = true;
	config.language = 'vi';
	config.filebrowserBrowseUrl = '/Areas/Admin/Content/ckfinder/ckfinder.html';
	config.filebrowserImageBrowseUrl = '/Areas/Admin/Content/ckfinder/ckfinder.html?Type=Images';
	config.filebrowserFlashUrl = '/Areas/Admin/Content/ckfinder/ckfinder.html?Type=Flash';
	config.filebrowserUploadUrl = '/Areas/Admin/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=File';
	config.filebrowserImageUploadUrl ='Data'
	config.filebrowserFlashUploadUrl = '/Areas/Admin/Content/ckfinder/core/connector/aspx/connector.aspx?command=QuickUpload&type=Flash';

	CKFinder.setupCKEditor(null, '/Areas/Admin/Content/ckfinder/');
	
};
