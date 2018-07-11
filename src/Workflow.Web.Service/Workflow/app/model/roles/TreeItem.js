Ext.define('Workflow.model.roles.TreeItem', {
    extend: 'Ext.data.Model',
    idProperty: 'key',
    fields: [
        'key',
        'text',
        'name',
        'left',
        'type',
        'value'
    ]
});

