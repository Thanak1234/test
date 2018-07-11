Ext.define('Workflow.ux.form.field.DateSvrField', {
    extend: 'Ext.form.field.Date',
    alias: ['widget.datesvrfield'],
    valueToRaw: function (value) {
        var date = new Date(value);
        return value?this.formatDate(date):null;
    }
});