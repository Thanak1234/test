Ext.define("Workflow.view.common.fileUpload.SimpleFileUploadDialog", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.common.fileUpload.SimpleFileUploadDialogController",
        "Workflow.view.common.fileUpload.SimpleFileUploadDialogModel",
        "Ext.ProgressBar"
    ],

    controller: "simple-file-upload-dialog",

    viewModel: {
        type: "simple-file-upload-dialog"
    },
    height : 150,
    title: 'File Uploading',
    iconCls: 'fa fa-upload',
    initComponent: function () {
        var me = this;
        me.items = [{
            width: '100%',
            bodyPadding: 10,
            reference :'form',
            xtype: 'form',
            frame: false,
            items: [{
                xtype: 'filefield',
                name: 'file',
                fieldLabel: 'File',
                labelWidth: 50,
                msgTarget: 'side',
                allowBlank: false,
                anchor: '100%',
                buttonText: 'Browse'
            }]
        }];

        me.callParent(arguments);

    }
});