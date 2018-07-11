Ext.define('Workflow.model.Process', {
    extend: 'Workflow.model.Base',
    fields: [
        { name: 'id', type: 'int' },
        { name: 'processName' },
        { name: 'processPath' },
        { name: 'requestCode' },
        { name: 'processCode' },
        { name: 'xtype' },
        { name: 'formNumber', type: 'int' },
        { name: 'active', type: 'bool' },
        { name: 'users' }
    ],
    proxy: {
        url: Workflow.global.Config.baseUrl + 'api/workflow/process',
        type: 'rest',
        reader: { type: 'json' }
    }
})