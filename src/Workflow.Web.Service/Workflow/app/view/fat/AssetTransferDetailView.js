/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.fat.AssetTransferDetail", {
    extend: "Workflow.view.GridComponent",
    xtype: 'fat-transfer-detail-view', 
    title: 'Asset Details',
    header: false,
    modelName: 'assetTransferDetail',
    collectionName: 'assetTransferDetails',
    actionListeners: {
        beforeAdd: function (grid, datamodel) {
            
        },
        add: function (grid, store, record) {
            var newRecord = store.createModel(record);
            store.add(newRecord);
        }
    },
    afterSaveChange: function (grid) {
        //var store = grid.getStore();
        //var ref = grid.getReferences(),
        //        ignoreMedicine = ref.ignoreMedicine;

        //if (store) {
        //    var count = store.count();
        //    ignoreMedicine.setDisabled(count > 0);
        //}
    },
    buildGridComponent: function (component) {
        var me = this;
       
        return [{
            header: 'Fixed Asset Code',
            flex: 1,
            sortable: true,
            dataIndex: 'transferCode'
        }, {
            xtype: 'numbercolumn',
            header: 'Net Book Value',
            format: '$0,000.00',
            width: 120,
            dataIndex: 'netBookValue'
        }, {
            xtype: 'datecolumn',
            header: 'Date Transfer',
            width: 200,
            format: 'm/d/Y',
            dataIndex: 'dateOfPurchase'
        }, {
            header: 'Description',
            width: 250,
            dataIndex: 'description'
        }];
    },
    buildWindowComponent: function (component) {
        component.width = 520;
        component.height = 300;
        component.labelWidth = 130;
        //var viewmodel = component.getViewModel();
        //var property = viewmodel.get('assetTransferDetailProperty.edit');
        var descrReadOnly = false;
        //if (property.readOnly) {
        //    descrReadOnly = property.readOnly.description;
        //}
        //console.log('descrReadOnly', descrReadOnly);

        return [{
            xtype: 'textfield',
            fieldLabel: 'Fixed Asset Code No.',
            allowBlank: false,
            bind: { value: '{assetTransferDetail.transferCode}' }
        }, {
            xtype: 'currencyfield',
            fieldLabel: 'Net Book Value',
            allowBlank: false,
            minValue: 0.001,
            bind: { value: '{assetTransferDetail.netBookValue}' }
        }, {
            xtype: 'datefield',
            fieldLabel: 'Date Of Purchase',
            allowBlank: false,
            bind: { value: '{assetTransferDetail.dateOfPurchase}' }
        }, {
            xtype: 'textarea',
            fieldLabel: 'Description',
            allowBlank: false,
            bind: {
                value: '{assetTransferDetail.description}',
                readOnly: '{assetTransferDetailProperty.edit.readOnly.description}'
            }
        }];
    },
    afterDialogRender: function (component) {
        var viewmodel = component.getViewModel();
        var config = viewmodel.getData();
        var key = 'assetTransferDetailProperty.edit.readOnly.description';

        if (config.property && viewmodel.get(key) === null) {
            viewmodel.set(key, config.property.readOnly);
        }
    }
});