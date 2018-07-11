
Ext.define("Workflow.view.common.fileUpload.FileUpload",{
    extend: "Ext.grid.Panel",
    xtype: 'form-fileupload',

    requires: [
        "Workflow.view.common.fileUpload.FileUploadController",
       // "Workflow.view.common.fileUpload.FileUploadModel",
        'Ext.grid.column.Action'
    ],

   controller: "common-fileupload-fileupload",
   viewModel: {
       type: "common-fileupload-fileupload"
   },
   iconCls : 'fa fa-upload',
    title: 'File Attachment',
    stateful: true,
    collapsible: true,
    headerBorders: false, 
    
    viewConfig: {
        enableTextSelection: true
    },
    listeners: {
        rowdblclick: 'viewFile',
        reference: 'editBtn' 
    },
    //store : Ext.create('Workflow.store.common.FileUpload'),
    //bind:{
    //    store:'{uploadFileStoreId}'
    //},
    guid: new Ext.data.identifier.Uuid().generate(),//new Ext.data.identifier.Uuid().generate(),
    reference:'item',
    initComponent: function () {
        var me = this;
        
        me.bbar= ['->',{
                    text: 'Add file',
                    iconCls: 'fa fa-plus',
                    reference: 'addBt',
                    handler: 'addNewFile',
                    bind: {
                        hidden: '{!canAddRemove}'
                    }
                }];
        
        me.columns = [
            {
                text        : 'NAME',
                flex        : 1,
                sortable    : true,
                dataIndex   : 'name'

            },{
                text        : 'DESCRIPTION',
                width       : 200,
                sortable    : true,
                dataIndex   : 'description'
            },{
                text        : 'FILE NAME',
                width       : 200,
                sortable    : true,
                dataIndex   : 'fileName'
            },{
                text        : 'ACTIVITY',
                width       : 180,
                sortable    : true,
                dataIndex: 'activity'
            }, {
                xtype: 'datecolumn',
                text: 'UPLOAD DATE',
                width: 180,
                sortable: true,
                format: 'Y-m-d H:i:s',
                dataIndex: 'uploadDate'
            }, {
                
                menuDisabled: true,
                sortable: false,
                xtype: 'actioncolumn', 
                align: 'center',
                width: 30,
                items: [{
                        iconCls: 'fa fa-download',
                        tooltip: 'Dowload',
                        handler: 'dowloadFile'
                }]
            }, {
                menuDisabled: true,
                sortable: false,
                xtype: 'actioncolumn',
                align: 'center',
                bind: {
                    hidden: '{!canAddRemove}'
                },
                width: 30,
                items: [{
                    iconCls: 'fa fa-trash-o',
                    tooltip: 'Remove',
                    handler: 'removeFile'
                    
                }]
            }
        ];
        

        me.callParent(arguments);
    },
    getData: function () {
        var me              = this, 
            store = me.getStore();

        
        

        return {
            newItems: me.getNewRecords(store.getData()),
            updatedItems    : me.getArrayItems(store.getUpdatedRecords()),
            removedItems: me.getArrayRemovedItems(store.getRemovedRecords()),
            allItems: me.getArrayItems(store.getRange())
        };
    },
    getNewRecords: function(data){
        var newItems = [];
        var items = (data != undefined && data.length > 0) ? data.items : [];
        if (items && items != undefined && items.length > 0) {
            for (var i = 0; i < items.length; i++) {
                if (items[i].data.isTemp == true) {
                    newItems.push(items[i].data);
                }
            }
        }
        return newItems;
    },
    getArrayItems: function(items){
        var newItems=[]
        if (items && items != undefined && items.length > 0) {
            for (var i = 0; i < items.length; i++) {
                newItems.push(items[i].data);
            }
        }
        return newItems;
    },
    getArrayRemovedItems: function(items){
        var newItems=[]
        if (items && items != undefined && items.length > 0) {
            for (var i = 0; i < items.length; i++) {
                var removedRec = items[i].data;
                if (removedRec.isTemp) {
                    removedRec.id = 0;
                }
                newItems.push(removedRec);
            }
        }
        return newItems;
    }
});
