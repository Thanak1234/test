Ext.define('Workflow.model.reports.ProcInst', {
    extend: 'Ext.data.Model',
    fields: [
            { name: 'id', type: 'int' },
            { name: 'folio', type: 'string' },
            { name: 'requestCode', type: 'string' },
            { name: 'application', type: 'string' },
            { name: 'originator', type: 'string' },
            { name: 'submitDate', type: 'date' },
            { name: 'lastActivity', type: 'string' },
            { name: 'lastActionDate', type: 'date' },
            { name: 'lastActionBy', type: 'string' },
            { name: 'status', type: 'string' },
            { name: 'action', type: 'string' },
            { name: 'procInstId', type: 'int' },
            { name: 'formUrl', type: 'string' },
            { name: 'workflowUrl', type: 'string' },
            { name: 'requestor', type: 'string' },
            { name: 'noneK2', type: 'boolean' }
    ]
});