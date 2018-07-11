/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.fad.AssetControlDetailView", {
    extend: "Workflow.view.GridComponent",
    xtype: 'fad-control-detail-view', 
    modelName: 'assetControlDetail',
    collectionName: 'assetControlDetails',
    
    initComponent: function () {
        var me = this,
            parent = me.mainView,
            refs = parent.getReferences();

        var assetDisposal = refs.assetDisposal,
            assetDisposalViewModel = assetDisposal.getViewModel(),
            viewmodel = me.getViewModel();

        this.actionListeners = {
            load: function (grid) {
                // load total net book value
                var totalNetBookValue = grid.getStore().sum('netBookValue');
                viewmodel.set(
                    'assetDisposal.totalNetBookValue',
                    totalNetBookValue
                );
            },
            add: function (grid, store, record) {
                var newRecord = store.createModel(record);
                store.add(newRecord);
            },
            edit: function (grid, store, record) {
                var recToUpdate = store.getById(record.id);
                recToUpdate.set(record);

                // calculation sum
                var totalNetBookValue = store.sum('netBookValue');
                viewmodel.set('assetDisposal.totalNetBookValue', totalNetBookValue);
                assetDisposalViewModel.set('assetDisposal.totalNetBookValue', totalNetBookValue);
            }
        };

        this.callParent(arguments);
    },
    buildGridComponent: function (component) {
        var me = this;

        me.bbar = ['->', {
            fieldLabel: 'Total Net Book Value',
            labelWidth: 120,
            maxWidth: 250,
            xtype: 'currencyfield',
            format: '$0,000.00',
            margin: '0 120 0 0',
            bind: {
                value: '{assetDisposal.totalNetBookValue}',
                readOnly: true
            }
        }];

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
            header: 'Asset No',
            width: 120,
            sortable: true,
            dataIndex: 'assetNo'
        }, {
            xtype: 'numbercolumn',
            header: 'Original Cost',
            format: '$0,000.00',
            width: 120,
            dataIndex: 'originalCost'
        }, {
            xtype: 'datecolumn',
            header: 'Date of Purchase',
            width: 120,
            format: 'm/d/Y',
            dataIndex: 'dateOfPurchase'
        }, {
            xtype: 'numbercolumn',
            header: 'Net Book Value',
            format: '$0,000.00',
            width: 120,
            dataIndex: 'netBookValue'
        }, {
            xtype: 'datecolumn',
            header: 'Date of NBV',
            width: 120,
            format: 'm/d/Y',
            dataIndex: 'dateOfNBV'
        }];
    },
    buildWindowComponent: function (component) {
        component.width = 520;
        component.height = 410;
        component.labelWidth = 130;
        component.title = 'Asset Control Details';

        return [{
            xtype: 'textfield',
            fieldLabel: 'Asset Description',
            allowBlank: false,
            bind: {
                value: '{assetControlDetail.description}',
                readOnly: true
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Serial No',
            allowBlank: false,
            bind: {
                value: '{assetControlDetail.serialNo}',
                readOnly: true
            }
        }, {
            xtype: 'textfield',
            fieldLabel: 'Asset No',
            allowBlank: false,
            bind: { value: '{assetControlDetail.assetNo}' }
        }, {
            xtype: 'currencyfield',
            fieldLabel: 'Original Cost',
            allowBlank: false,
            minValue: 0.001,
            bind: { value: '{assetControlDetail.originalCost}' }
        }, {
            xtype: 'datefield',
            fieldLabel: 'Date of Purchase',
            format: 'm/d/Y',
            allowBlank: false,
            bind: { value: '{assetControlDetail.dateOfPurchase}' }
        }, {
            xtype: 'currencyfield',
            fieldLabel: 'Net Book Value',
            format: '$0,000.00',
            allowBlank: false,
            minValue: 0.001,
            bind: { value: '{assetControlDetail.netBookValue}' }
        }, {
            xtype: 'datefield',
            fieldLabel: 'Date of NBV',
            format: 'm/d/Y',
            allowBlank: false,
            bind: { value: '{assetControlDetail.dateOfNBV}' }
        }];
    }
});