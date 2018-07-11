Ext.define('Workflow.model.ticket.TkChartData', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'id', type: 'int' },
        { name: 'time', type: 'date' },
        { name: 'keyPath', type: 'string'},
        { name: 'itemName', type: 'string'},
        { name: 'Value'}
    ]
});
