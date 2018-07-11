
Ext.define("Workflow.view.common.fileUpload.FileUploadWindow",{
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.common.fileUpload.FileUploadWindowController",
        "Workflow.view.common.fileUpload.FileUploadWindowModel",
        "Ext.ProgressBar"
    ],

    controller: "common-fileupload-fileuploadwindow",
    viewModel: {
        type: "common-fileupload-fileuploadwindow"
    },
    
    title: "File Attachment", 

    initComponent: function () {
        var me = this;
        me.items = [{
            xtype: 'form',
            frame: false,
            bodyPadding: '10 10 0',
            reference: 'form',
            id: 'uploadForm',
            method: 'POST',
            url: Workflow.global.Config.baseUrl + 'api/forms/attachments',
            defaultListenerScope: true,
            defaults: {
                flex: 1,
                anchor: '100%',
                allowBlank: false,
                msgTarget: 'side',
                labelWidth: 100,
                layout: 'form',
                xtype: 'container',
                defaultType: 'textfield'
            },
            items: [{
                fieldLabel: 'Name',
                xtype: 'textfield',
                name: 'name',
                bind: {
                    value: '{item.name}',
                    readOnly: '{!submitBtVisible}'
                }
            }, {
                fieldLabel: 'Description',
                xtype: 'textarea',
                name: 'description',
                allowBlank: true,
                bind: {
                    value: '{item.description}',
                    readOnly: '{!submitBtVisible}'
                }
            }, {
                fieldLabel: 'Activity',
                xtype: 'textfield',
                name: 'activity',
                allowBlank: true,
                readOnly: true,
                bind: {
                    value: '{item.activity}',
                    hidden: '{submitBtVisible}'
                }
            }, {
                fieldLabel: 'Upload Date',
                xtype: 'datefield',
                name: 'uploadDate',
                allowBlank: true,
                format: 'Y-m-d H:i:s',
                readOnly: true,
                bind: {
                    value: '{item.uploadDate}',
                    hidden: '{submitBtVisible}'
                }
            }, {
                emptyText: 'Select a file...',
                fieldLabel: 'File',
                xtype: 'fileuploadfield',
                name: 'fileName',
                id: 'fileName',
                msgTarget: 'side',
                bind: {
                    value: '{item.fileName}',
                    hidden: '{!submitBtVisible}',
                    readOnly: '{!submitBtVisible}'
                },
                buttonText: 'Browse'
            }, {
                fieldLabel: 'File',
                //margin: '0 0 0 75',
                xtype: 'label',
                bind: {
                    html: '{item.downloadLink}',
                    hidden: '{submitBtVisible}'
                }
            }, {
                margin: '0 0 0 75',
                xtype: 'label',
                bind: {
                    hidden: '{!submitBtVisible}'
                },
                text: 'File must be less than 50 megabytes.'
            }]
        }];
        me.callParent(arguments);
    }
    
});
