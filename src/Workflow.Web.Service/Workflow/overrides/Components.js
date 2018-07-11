Ext.define('Workflow.form.field.Text', {
    override : 'Ext.form.field.Text',
    maxLength: 100, 
    enforceMaxLength: true,
    constructor: function () {
        var me = this;
        this.callParent(arguments);
        var readOnly = me.readOnly;
        if (me.getBind()) {
            readOnly = me.getBind().readOnly;
        }
        if (me.allowBlank === false && !me.hiddenLabel && readOnly) {
            var label = me.getFieldLabel() + ' <span class="req" style="color:red">*</span>';
            me.setFieldLabel(label);
        }
    },
    validator: function (text) {
        if (this.allowBlank == false && 
			text && 
			(!Ext.isNumber(text)) &&
			Ext.util.Format.trim(text).length == 0)
            return false;
        else
            return true;
    }
});

Ext.form.field.Text.override({
    maxLength: 100, 
    enforceMaxLength: true,
    constructor: function () {
        var me = this;
        this.callParent(arguments);
        var readOnly = me.readOnly;
        if (me.getBind()) {
            readOnly = me.getBind().readOnly;
        }
        if (me.allowBlank === false && !me.hiddenLabel && readOnly) {
            var label = me.getFieldLabel() + ' <span class="req" style="color:red">*</span>';
            me.setFieldLabel(label);
        }
    },
    validator: function (text) {
        if (this.allowBlank == false && 
			text && 
			(!Ext.isNumber(text)) &&
			Ext.util.Format.trim(text).length == 0)
            return false;
        else
            return true;
    }
});

Ext.grid.CellContext.override({
    setRow: function (row) {
        // Fixed row null cause the property isModel not found.
        if (row === null) {
            row = undefined;
        }
        this.callParent(arguments);
    }
})


Ext.form.field.TextArea.override({
    annotation: true,
    maxLength: 2000,
    enforceMaxLength: true,
    constructor: function () {
        var me = this;
        this.callParent(arguments);
        if (me.maxLength && me.maxLength > 0 && this.annotation ) {
            me.addCls('max-length-' + me.maxLength);
        }
    }
});

//Ext.define('Workflow.AdvancedVType', {
//    override: 'Ext.form.field.VTypes',

//    daterange: function(val, field) {
//        var date = field.parseDate(val);

//        if (!date) {
//            return false;
//        }
//        if (field.startDateField && (!this.dateRangeMax || (date.getTime() != this.dateRangeMax.getTime()))) {
//            var start = field.up('form').down('#' + field.startDateField);
//            start.setMaxValue(date);
//            start.validate();
//            this.dateRangeMax = date;
//        }
//        else if (field.endDateField && (!this.dateRangeMin || (date.getTime() != this.dateRangeMin.getTime()))) {
//            var end = field.up('form').down('#' + field.endDateField);
//            end.setMinValue(date);
//            end.validate();
//            this.dateRangeMin = date;
//        }
//        /*
//         * Always return true since we're only using this vtype to set the
//         * min/max allowed values (these are tested for after the vtype test)
//         */
//        return true;
//    },

//    daterangeText: 'Start date must be less than end date',

//    password: function(val, field) {
//        if (field.initialPassField) {
//            var pwd = field.up('form').down('#' + field.initialPassField);
//            return (val == pwd.getValue());
//        }
//        return true;
//    },

//    passwordText: 'Passwords do not match'
//});