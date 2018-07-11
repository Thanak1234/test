Ext.define('Workflow.model.common.worklists.InstanceAudit', {
    extend: 'Ext.data.Model',
    fields: [
        'procInstId',
        'actInstId',
        'procName',
        'actName',
        'folio',
        'user',
        'date',
        'auditDesc'
    ]
});