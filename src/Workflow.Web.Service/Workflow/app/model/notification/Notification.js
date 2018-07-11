Ext.define('Workflow.model.notification.Notification', {
    extend: 'Workflow.model.Base',
    fields: [
        { name: 'module', type: 'string' },
        { name: 'activityId', type: 'int' },
        { name: 'activityCode', type: 'string' },
        { name: 'status', type: 'string' },
        { name: 'subject', type: 'string' },
        { name: 'description', type : 'string'},
        { name: 'createdDate', type : 'date'}
    ]
});