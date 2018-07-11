Ext.define("Workflow.view.admsr.Company", {
    extend: "Workflow.view.GridComponent",
    xtype: 'admsr-company',
    title: 'Company/Contractor',
    header: false,
    modelName: 'company',
    collectionName: 'companies',
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
    //afterRender: function(){
    
    //},
    buildGridComponent: function (component) {
        var me = this;
        component.tbar.push({
            xtype: 'checkbox',
            name: 'noQuote',
            reference: 'noQuote',
            boxLabel: 'No Quote',
            bind: {
                disabled: '{companyProperty.add.disabled}'
            },
            handler: function (el, value) {
                var vm = me.getViewModel(); 
                me.getStore().removeAll();
                vm.set('companyProperty.add.disabled', value);
                vm.set('companyProperty.edit.disabled', value);
                vm.set('companyProperty.view.disabled', value);
            }
        });
        
       
        return [{
            header: 'Company/Contractor Name',
            flex: 1,
            sortable: true,
            dataIndex: 'name'
        }, {
            xtype: 'datecolumn',
            header: 'Date Issued',
            width: 120,
            dataIndex: 'dateIssued',
            format: 'm/d/Y'            
        }, {            
            header: 'Valid (days)',
            width: 150,
            dataIndex: 'validDay'
        }, {
            header: 'Price ($USD)',
            width: 120,
            dataIndex: 'price'
        }];
    },
    buildWindowComponent: function (component) {
        var me = this;
        component.width = 530;
        component.height = 350;
        component.labelWidth = 170;
        var descrReadOnly = false;

        return [
            {
                xtype: 'textfield',
                fieldLabel: 'Company/Contractor Name',
                allowBlank: false,
                bind: {
                    value: '{company.name}'
                }
            },
            {
                xtype: 'datefield',
                fieldLabel: 'Date Issued',
                allowBlank: false,
                bind: {
                    value: '{company.dateIssued}'
                }
            },
            {
                xtype: 'numberfield',
                fieldLabel: 'Valid (days)',
                allowBlank: false,
                bind: {
                    value: '{company.validDay}'
                }
            },
            {
                xtype: 'numberfield',
                fieldLabel: 'Price ($USD)',
                allowBlank: false,
                bind: {
                    value: '{company.price}'
                }
            }
        ];
    }
});