Ext.define('Workflow.model.reports.ProcInstOv.ActivityInstance', {
    extend: 'Ext.data.Model',
    fields: [
        'processInstanceID',
        'activityInstanceID',
        'activityName',
        'startDate',
        'finishDate',
        'priority',
        'status',
        'expectedDuration',
        'duration'
    ]
});