Ext.define('Workflow.ux.form.field.CurrencyField', {
    extend: 'Ext.form.field.Number',
    alias: ['widget.currencyfield'],
    config: {
        thousandSeparator: ',',
        currencyAtEnd: false,
        currencySign: '$'
    },
    listeners: {
        focus: function (me, eOpts) {
            if (this.getValue() == 0) {
                me.inputEl.dom.value = '';
            } else {
                me.inputEl.dom.value = this.getValue();
            }
        }
    },
    valueToCurrency: function (value) {
        var format = Ext.util.Format;
        format.currencyPrecision = this.decimalPrecision;
        format.thousandSeparator = this.thousandSeparator;
        format.currencySign = this.currencySign;
        format.currencyAtEnd = this.currencyAtEnd;
        return format.currency(value);
    },
    valueToRaw: function (value) {
        return this.valueToCurrency(value);
    },
    processRawValue: function (value) {
        //this.callParent(arguments)
        return this.parseValue(value);
    },
    parseValue: function (value) {
        value = String(value).replace(this.thousandSeparator, "");
        value = String(value).replace(this.currencySign, "");

        value = parseFloat(String(value)
			.replace(this.decimalSeparator, '.')
			.replace(this.thousandSeparator, ''));
        return isNaN(value) ? 0 : value;
    }
});