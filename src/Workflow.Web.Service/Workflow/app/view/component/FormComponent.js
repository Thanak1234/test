Ext.define("Workflow.view.FormComponent", {
    extend: "Ext.form.Panel",
    xtype: 'form-component',
    controller: "form-component",
    viewModel: {
        type: "component"
    },
    //iconCls: 'fa fa-user',
    //minHeight: 100,
    autoWidth: true,
    frame: false,
    bodyPadding: '10',
    method: 'POST',
    defaults: {
        flex: 1,
        anchor: '100%',
        defaultType: 'labelfield',
        labelAlign: 'right',
        labelWidth: 150,
        layout: 'form',
        margin: '5 0 5 0'
    },
    layout: 'anchor',
    // getData: function () {
    //     return this.getViewModel().getData();
    // },
    isValid: function () {
       var isValid = true,
            fields = this.form.getFields();

        fields.each(function (field) {
            if (!field.isHidden || (field.isHidden && !field.isHidden())) {
                isValid = isValid && field.isValid();
            }
        });
        
        return isValid;
    },
    isFormValid: function (form) {
        
    },
    initComponent: function () {
        var me = this;
        var formFields = me.buildComponent();
        me.fieldBinder(formFields);
        me.items = formFields;

        me.callParent(arguments);
    },
    fieldBinder: function (items) {
        var me = this;
        Ext.each(items, function (item) {
            if (item.xtype == 'fieldset' || 
            item.xtype == 'container' || item.xtype == 'fieldcontainer') {
                me.fieldBinder(item.items);
            } else {
                if (item.bind && item.bind.value) {
                    var strValue = item.bind.value;
                    item.bind.readOnly = me.binder(strValue, 'readOnly');
                    item.bind.hidden = me.binder(strValue, 'hidden');
                    item.emptyText = item.fieldLabel;
                }
            }
        });
    },
    binder: function (strValue, fieldName) {
        // {{model}Property.readOnly{field}}
        
        return strValue
            .replace('.', 'Property.')
            .replace('}', ('.' + fieldName + '}'));
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
    // minimumDate: function (field, eOpts) {
    //     field.setMinValue(new Date());
    //     field.validate();
    //     this.dateRangeMin = new Date();
    // },
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
                if (field.xtype == 'datetime' && field.name != validateWith && picker['validateRang']) {
                    if (picker.validateRang == field.validateRang) {
                        field.setMinValue(dateValue);
                        field.validate();
                        field.dateRangeMin = dateValue;
                    }
                }
            });
        }
    },
    buildComponent: function () {

    },
    loadData: function (data, viewSetting) {
        
    }
});
