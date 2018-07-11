/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.fad.AssetDisposalDetailView", {
    extend: "Workflow.view.GridComponent",
    xtype: 'fad-disposal-detail-view',
    modelName: 'assetDisposalDetail',
    collectionName: 'assetDisposalDetails',
    title: 'Asset Disposal Details',
    header: false,
    actionListeners: {
        beforeAdd: function (grid, datamodel) {
            
        },
        add: function (grid, store, record) {
            var newRecord = store.createModel(record);
            store.add(newRecord);
        }
    },
    afterSaveChange: function (grid) {
        // TODO: SOMTHING
    },
    buildGridComponent: function (component) {
        var me = this;
       
        return [{
            header: 'Asset Description',
            flex: 1,
            sortable: true,
            dataIndex: 'description'
        }, {
            header: 'Serial No',
            width: 120,
            sortable: true,
            dataIndex: 'serialNo'
        }, {
            xtype: 'numbercolumn',
            header: 'Quantity',
            width: 70,
            sortable: true,
            dataIndex: 'quantity'
        }, {
            header: 'Location',
            width: 120,
            sortable: true,
            dataIndex: 'location'
        }, {
            header: 'Reason of Disposal',
            flex: 1,
            sortable: true,
            dataIndex: 'reason'
        }, {
            xtype: 'numbercolumn',
            header: 'Estimated Realisable Value',
            format: '$0,000.00',
            width: 200,
            dataIndex: 'estimatedRealisableValue'
        }];
    },
    buildWindowComponent: function (component) {
        component.width = 520;
        component.height = 410;
        component.labelWidth = 160;

        return [{
            xtype: 'textfield',
            fieldLabel: 'Asset Description',
            allowBlank: false,
            bind: {
                value: '{assetDisposalDetail.description}'
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Serial No',
            allowBlank: false,
            bind: { value: '{assetDisposalDetail.serialNo}' }
        }, {
            xtype: 'numberfield',
            fieldLabel: 'Quantity',
            allowBlank: false,
            minValue: 0.001,
            bind: { value: '{assetDisposalDetail.quantity}' }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Location',
            allowBlank: false,
            bind: {
                value: '{assetDisposalDetail.location}'
            }
        }, {
            xtype: 'textarea',
            fieldLabel: 'Reason of Disposal',
            allowBlank: false,
            bind: {
                value: '{assetDisposalDetail.reason}'
            }
        }, {
            xtype: 'currencyfield',
            fieldLabel: 'Estimated Realisable Value',
            allowBlank: false,
            minValue: 0.001,
            bind: { value: '{assetDisposalDetail.estimatedRealisableValue}' }
        }];
    }
});