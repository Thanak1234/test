Ext.define('Workflow.model.reports.ProcInstOv.UserPerformance', {
    extend: 'Ext.data.Model',
    fields: [
        'destinationID',
        'activityInstanceID',
        'processSetFullName',
        'activityName',
        'destinationUser',
        'destinationUserDisplay',
        'finishDate',
        'finalAction',
        'instanceCount',
        'duration',
        'processInstanceID',
        'startDate',
        'status',
        'userCsv',
        'processVersion',
        'rangeStartDate',
        'rangeEndDate'
    ]
});