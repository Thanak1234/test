Ext.define('Workflow.model.vaf.Outline', {
    extend: 'Ext.data.Model',
    idProperty: 'Id',
    fields: [
        'Id',
        { name: 'GamingDate', type: 'date' },
        'McidLocn',
        'VarianceType',
        'Subject',
        'IncidentRptRef',
        'ProcessInstanceId',
        'Area',
        'RptComparison',
        'Amount',
        'Comment'
    ]
});
