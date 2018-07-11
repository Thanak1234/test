Ext.define("Workflow.view.fat.AssetTransferView", {
    extend: "Workflow.view.FormComponent",
    xtype: 'fat-transfer-view',
    modelName: 'assetTransfer',
    viewSection: 'BRANCH',// BRANCH/DEPARTMENT
    loadData: function (data, viewSetting) {
        var me = this,
             viewmodel = me.getViewModel();

        if (data) {
            viewmodel.set('assetTransfer', data.assetTransfer);
        }
    },

    buildComponent: function () {
        var me = this;
            viewmodel = me.getViewModel(),
            mainVM = me.mainView.getViewModel();
        
        if (this.viewSection === 'BRANCH') {
            return [{
                xtype: 'combo',
                fieldLabel: 'Company',
                labelAlign: 'left',
                labelWidth: 100,
                maxWidth: 420,
                editable: false,
                store: Ext.create('Ext.data.Store', {
                    fields: ['name', 'label'],
                    data: [
                        { "name": "Gaming", "label": "N1 - Gaming" },
                        { "name": "Hotel", "label": "N1 - Hotel" },
                        { "name": "N2 - Gaming", "label": "N2 - Gaming" },
                        { "name": "N2 - Hotel", "label": "N2 - Hotel" },
                        { "name": "Shared", "label": "Shared" },
                    ]
                }),
                queryMode: 'local',
                displayField: 'label',
                valueField: 'name',
                allowBlank: false,
                bind: {
                    value: '{assetTransfer.companyBranch}'
                }
            }];
        } else {
            return [{
                xtype: 'combo',
                fieldLabel: 'Transfer To Department',
                maxWidth: 550,
                mainView: me,
                store: {
                    autoLoad: true,
                    proxy: {
                        type: 'rest',
                        url: Workflow.global.Config.baseUrl + 'api/employee/departments',
                        reader: {
                            type: 'json'
                        }
                    }
                },
                queryMode: 'local',
                minChars: 0,
                forceSelection: true,
                editable: true,
                displayField: 'fullName',
                valueField: 'id',
                allowBlank: false,
                mainView: me,
                bind: {
                    value: '{assetTransfer.transferToDeptId}'
                },
                listeners: {
                    change: function (combo) {
                        combo.store.load();
                    }
                },
                margin: '5 0 10 0'
            }];
        }
        
    }
});