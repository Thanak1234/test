Ext.define("Workflow.view.WindowComponent", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",
    xtype: 'window-component',
    controller: "window-component",
    viewModel: {
        type: "component"
    },
    iconCls: 'fa fa-file-text-o',
    fieldBinder: function (items) {
        var me = this;
        Ext.each(items, function (item) {
            if (item.xtype == 'fieldset' || item.xtype == 'container' || item.xtype == 'fieldcontainer') {
                me.fieldBinder(item.items);
            } else {
                if (item.bind && item.bind.value) {
                    if (!(item.bind.readOnly)) {
                        item.bind.readOnly = '{property.readOnly}';
                    }
                    item.emptyText = item.fieldLabel;
                    item.component = me; // passing directive object
                }
            }
        });
    },
    buildFormComponent: function () {
        
    },
    initComponent: function () {
        var me = this;
        var fieldItems = me.buildFormComponent();
        me.fieldBinder(fieldItems);

        me.items = [{
            xtype: 'form',
            frame: false,
            autoScroll: true,
            viewModel: true,
            bodyPadding: '10 10 0',
            reference: 'form',
            method: 'POST',
            defaultListenerScope: true,
            defaults: {
                flex: 1,
                anchor: '100%',
                defaultType: 'labelfield',
                msgTarget: 'side',
                labelAlign: 'right',
                labelWidth: me.labelWidth ? me.labelWidth : 180,
                layout: 'form',
                xtype: 'container'
            },
            items: fieldItems
        }];

        me.callParent(arguments);
    },
    defaultsFieldSet: function () {
        return {
            flex: 1,
            anchor: '100%',
            defaultType: 'textfield',
            labelAlign: 'right',
            labelWidth: 150,
            layout: 'form',
            margin: '5 0 5 0'
        };
    },
    validateDate: function (picker) {
        var frm = picker.up('form') ? picker.up('form') : picker.down('form');
        
        if (frm) {
            var validateWith = picker['validateRang'];
            var form = frm.getForm(),
            fields = form.getFields(),
            dateField = form.findField(validateWith),
            dateValue = dateField.getValue();
            var valid = true;

            fields.each(function (field) {
                if (
                (field.xtype == 'datetime' || field.xtype == 'datefield') && 
                field.name != validateWith && picker['validateRang']) {
                    field.setMinValue(dateValue);
                    field.validate();
                    field.dateRangeMin = dateValue;
                }
            });
        }
    }
});
