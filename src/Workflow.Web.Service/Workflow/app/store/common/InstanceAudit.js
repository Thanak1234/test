Ext.define('Workflow.store.common.InstanceAudit', {
    extend: 'Ext.data.Store',
    alias: 'store.audit',
    model: 'Workflow.model.common.worklists.InstanceAudit',
    autoLoad: false,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/worklists/audits',
        reader: {
            type: 'json',
            rootProperty: 'Records',
            totalProperty: 'TotalRecords'
        }
    }
});