namespace MrCMS.Settings
{
    public static class SettingDefaults
    {
        public const string CkEditorConfig = @"/**
 * @license Copyright (c) 2003-2012, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.html or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function (config) {
    config.extraPlugins = 'justify,autogrow,youtube,mediaembed';
    config.removePlugins = 'elementspath';
    config.forcePasteAsPlainText = true;
    config.allowedContent = true;
    config.contentsCss = ['/Apps/Core/Content/bootstrap/css/bootstrap.css', '/Apps/Core/Content/Styles/style.css'];

    config.toolbar = 'Full';

    config.toolbar_Full =
    [
        { name: 'document', items: ['Source', '-', 'Templates'] },
        { name: 'styles', items: ['Format', 'Styles'] },
        { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'Undo', 'Redo'] },
         ['Scayt'],
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript', '-', 'RemoveFormat'] },

        { name: 'tools', items: ['Maximize', 'ShowBlocks'] },
        {
            name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'BidiLtr', 'BidiRtl']
        },
        { name: 'links', items: ['Link', 'Unlink', 'Anchor'] },
        { name: 'insert', items: ['Image', 'Flash', 'Table', 'HorizontalRule', 'Smiley', 'SpecialChar', 'PageBreak', 'Iframe'] },
        { name: 'Media', items: ['Youtube', 'MediaEmbed'] }

    ];

    config.toolbar_Basic =
    [
        ['Templates', 'Bold', 'Italic', 'RemoveFormat', 'Outdent', 'Indent', '-', 'Blockquote', 'Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', '-', 'Image', 'Flash', 'Table', 'HorizontalRule', 'Format', 'Youtube', 'MediaEmbed']
    ];

};";
    }
}