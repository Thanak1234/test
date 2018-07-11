Ext.define('Workflow.model.wm.Item', {
    extend: 'Workflow.model.Base',
    idProperty: 'id',
    fields: [
        'rowNumber',
        'id',
        'procName',
        'eventName',
        'activityName',
        'folio',
        'startDate',
        'procInstID',
        'status',
        'destination',
        'procInstStatus',
        'employeeNo',
        'displayName'
    ]
});
