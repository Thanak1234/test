/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.attach.AttachFilePanel',{
    extend: 'Ext.grid.Panel',
    xtype: 'ticket-attach-file-panel',
    requires: [
        'Workflow.view.ticket.attach.AttachFilePanelController',
        'Workflow.view.ticket.attach.AttachFilePanelModel'
    ],
    controller: 'ticket-attach-attachfile',
    viewModel: {
        type: 'ticket-attach-attachfile'
    },
    scrollable: 'y',
    //reference: 'attachedFileList',
    bind : {
        store: '{attchedFilesStore}'
    },
    hideHeaders: true,
    dockedItems: [{
        xtype: 'toolbar',
        dock: 'top',
        items: [
            '->',
            {
                text: 'Upload file',
                xtype: 'button',
                handler: 'onFileAddedHander',
                iconCls: 'fa fa-upload'
            }]
    }],
    height : 100,
    initComponent: function () {
        var me = this;
        this.columns = [
            { text: 'UploadFile', dataIndex: 'fileName', flex: 1 },
            {
                menuDisabled: true,
                sortable: false,
                xtype: 'actioncolumn',
                //bind: {
                //    hidden: '{!canAddRemove}'
                //},
                width: 40,
                items: [{
                    iconCls: 'fa fa-trash-o',
                    tooltip: 'Remove',
                    handler: 'oneRemoveFileHandler'
                }]
            }];

        this.callParent(arguments);
    }
});
