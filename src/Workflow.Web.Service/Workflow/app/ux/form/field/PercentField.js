Ext.define('Workflow.ux.form.field.PercentField', {
    extend: 'Ext.form.field.Number',
    alias: ['widget.percentfield'],
    listeners: {
        focus: function (me, eOpts) {
            if (this.getValue() == 0) {
                me.inputEl.dom.value = '';
            } else {
                me.inputEl.dom.value = this.getValue();
            }
        }
    },
    valueToPercent: function (value) {
        return Ext.util.Format.number(value, '000%');
    },
    valueToRaw: function (value) {
        return this.valueToPercent(value);
    },
    processRawValue: function (value) {
        return this.parseValue(value);
    },
    parseValue: function (value) {
        value = String(value).replace('%', "");
        value = parseInt(value);
        return isNaN(value) ? 0 : value;
    }
});