/** AUTHOR : YIM SAMAUNE **/
Ext.define("Workflow.view.mtf.Prescription", {
    extend: "Workflow.view.GridComponent",
    xtype: "mtf-prescription-view",
    title: 'MEDICINE',
    modelName: 'prescription',
    collectionName: 'prescriptions',
    actionListeners: {
        beforeAdd: function (grid, datamodel) {
            
            // datamodel[grid.modelName] = {
            //     medicineRecord : grid.store.getData()
            // };
            
        },
        add: function (grid, store, record) {
            var newRecord = store.createModel(record);
            
            var iFound = store.findExact('medicineId', newRecord.get('medicineId'));
            if (iFound == -1) {
                store.add(newRecord);
            }

            // rowEditing = grid.plugins[0];
            // rowEditing.cancelEdit();
            // store.insert(0, newRecord);
            // rowEditing.startEdit(newRecord, 0);
            
        }
    },
    afterSaveChange: function (grid) {
        var store = grid.getStore();
        var ref = grid.getReferences(),
                ignoreMedicine = ref.ignoreMedicine;

        if (store) {
            var count = store.count();
            ignoreMedicine.setDisabled(count > 0);
        }
    },
    checkIgnorMedicine: function (grid, value) {
        var store = grid.getStore();
        var viewmodel = grid.getViewModel();
        viewmodel.set('prescriptionProperty.add.disabled', value);
    },
    buildGridComponent: function (component) {
        var me = this;
        component.tbar.push({
            xtype: 'checkbox',
            name: 'ignoreMedicine',
            reference: 'ignoreMedicine',
            boxLabel: 'No Medicine',
            handler: function (el, value) {
                me.checkIgnorMedicine(me, value);
            }
        });
        //component.editableRow = true;
        return [{
            hidden: true,
            dataDisplay: 'medicineId'
        },{
            header: 'MEDICINE',
            flex: 1,
            sortable: true,
            dataIndex: 'medicine',
            dataDisplay: 'medicine'
            // renderer: function(val, metaData, record){
            //     var combo = metaData.column.getEditor();
            //     if(val && combo && combo.store && combo.displayField){
            //         var index = combo.store.findExact(combo.valueField, val);
            //         if(index >= 0){
            //             return combo.store.getAt(index).get(combo.displayField);
            //         }
            //     }
            //     return record.get(metaData.column.dataDisplay);
            // },
            // editor: {
            //     xtype: 'pickupfield',
            //     webapi: 'medicines',
            //     valueField: 'medicineId',
            //     allowBlank: false
            // }
        }, {
            header: 'QUANTITY',
            width: 200,
            dataIndex: 'quantity',
            editor: {
                xtype: 'numberfield',
                allowBlank: false,
                minValue: 0.0001
            }
        }, {
            header: 'USAGE',
            width: 250,
            dataIndex: 'usage',
            editor: {
                xtype: 'textfield',
                allowBlank: false
            }
        }];
    },
    buildWindowComponent: function(component){
        component.width = 420;
        component.height = 300;
        component.labelWidth = 65;

        return [{
            xtype: 'pickupfield',
            fieldLabel: 'Medicine',
            webapi: 'medicines',
            allowBlank: false,
            valueField: 'medicineId',
            bind: {
                value: '{prescription.medicineId}',
                displayValue: '{prescription.medicine}'
            }
        },{
            xtype: 'numberfield',
            fieldLabel: 'Quantity',
            minValue: 0.0001,
            allowBlank: false,
            bind: { value: '{prescription.quantity}' }
        }, {
            xtype: 'textarea',
            fieldLabel: 'Usage',
            allowBlank: false,
            bind: { value: '{prescription.usage}' }
        }];
    }
});