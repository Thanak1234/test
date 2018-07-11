Ext.define('Workflow.model.dashboard.Task', {
    extend: 'Workflow.model.Base',
    idProperty: 'id',
    fields: [
        'id',
        'folio',
        'requestCode',
        'appName',
        'submittedBy',
        'submittedDate',
        'lastActivity',
        'lastActionDate',
        'lastActionBy',
        'status',
        'processInstanceId',
        'formUrl',
        'requestor',
        'noneSmartFromUrl',
        'noneK2',
        "flowUrl"
    ]
});
