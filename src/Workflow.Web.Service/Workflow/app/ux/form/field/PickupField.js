Ext.define('Workflow.ux.form.field.PickupField', {
    extend: 'Ext.form.field.ComboBox',
    alias: ['widget.pickupfield'],
    displayField: 'label',
    valueField: 'id',
    forceSelection: true,

    /* start custom config */
    webapi: '',

    /* end custom config */
    minChars: 0,
    queryMode: 'local',
    //typeAhead: true,

    initComponent: function () {
        var me = this;

        Ext.apply(this, {
            store: new Ext.data.Store({
                //autoLoad: true,
                proxy: {
                    type: 'rest',
                    url: Workflow.global.Config.baseUrl + 'api/lookup/' + me.webapi,
                    reader: {
                        type: 'json',
                        rootProperty: 'result',
                        totalProperty: 'count'
                    }
                }
            })
        });

        me.callParent();

    },
    setDisplayValue: function () { // abstrac function for viewmode binding

    },
    listeners: {
        select: function (combo) {
            if (combo.bind.displayValue) {
                combo.component.getViewModel().set(combo.bind.displayValue.stub.path, combo.getRawValue());
            }
        },
        afterrender: function (combo) {
            combo.store.load({
                callback: function (records) {
                    var id = combo.component.getViewModel().get(combo.bind.value.stub.path);
                    //console.log(combo.component.getViewModel(), combo.bind.value.stub.path);	
                    Ext.each(records, function (record) {

                        // var exists = combo.component.store.findRecord(
                        // combo.valueField, 
                        // record.get(combo.valueField)
                        // );

                        // if(exists && id != record.get(combo.valueField)){
                        // combo.store.remove(record);
                        // }
                    });
                }
            });
        },
        beforequery: function (record) {
            // TO DO SOMTHING

            record.query = new RegExp(record.query, 'i');
            record.forceAll = true;
        },
        change: function (el, newValue, oldValue, eOpts) {
            //console.log('el', el);
        }
    }
});