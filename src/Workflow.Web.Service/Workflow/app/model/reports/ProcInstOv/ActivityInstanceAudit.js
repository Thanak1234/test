Ext.define('Workflow.model.reports.ProcInstOv.ActivityInstanceAudit', {
    extend: 'Ext.data.Model',
    fields: [
        'procInstID',
        'actInstID',
        'procName',
        'actName',
        'folio',
        'userName',
        'date',
        'auditDescription'
    ]
});