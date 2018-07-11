Ext.define('Workflow.model.common.worklists.Option', {
    extend: 'Ext.data.Model',
    fields: [
        'Name',
        'Duration'
    ],

    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/worklists/basicoptions',
        reader: {
            type: 'json',
            rootProperty: 'Records'
        }
    }
});