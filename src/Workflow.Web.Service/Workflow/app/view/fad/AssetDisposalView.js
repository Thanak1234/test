/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.fad.AssetDisposalView", {
    extend: "Workflow.view.FormComponent",
    xtype: 'fad-disposal-view',
    //title: 'Originator',
    modelName: 'assetDisposal',
    loadData: function (data, viewSetting) {
        var me = this, 
            viewmodel = me.getViewModel(),
            reference = me.getReferences();
        
        if(data){
            viewmodel.set('assetDisposal', data.assetDisposal);
        }
    },
    buildComponent: function () {
        var me = this;

        return [{
            xtype: 'combo',
            fieldLabel: 'Company',
			labelAlign:'left', 
			labelWidth:100, 
            maxWidth: 420,
            editable: false,
            store: Ext.create('Ext.data.Store', {
                fields: ['name', 'label'],
                data: [
                    { "name": "Gaming", "label": "N1 - Gaming" },
                    { "name": "Hotel", "label": "N1 - Hotel" },
                    { "name": "N2 - Gaming", "label": "N2 - Gaming" },
                    { "name": "N2 - Hotel", "label": "N2 - Hotel" },
                    { "name": "Shared", "label": "Shared" }
                ]
            }),
            queryMode: 'local',
            displayField: 'label',
            valueField: 'name',
            allowBlank: false,
            bind: {
                value: '{assetDisposal.coporationBranch}'
            },
            margin: '5 0 10 0'
        }, {
            xtype: 'combo',
            fieldLabel: 'Asset Category',
            maxWidth: 420,
            mainView: me,
            store: {
                autoLoad: true,
                proxy: {
                    type: 'rest',
                    url: Workflow.global.Config.baseUrl +
                        'api/fadrequest/asset-category',
                    reader: {
                        type: 'json'
                    }
                }
            },
            queryMode: 'local',
            minChars: 0,
            forceSelection: true,
            editable: true,
            displayField: 'value',
            valueField: 'id',
            allowBlank: false,
            mainView: me,
            bind: {
                value: '{assetDisposal.assetGroupId}'
            },
            listeners: {
                change: function (combo) {
                    combo.store.load();
                }
            },
            margin: '5 0 10 0'
        }];
    }
});