Ext.define('Workflow.model.common.worklists.Activity', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'WorkflowId', type: 'string', mapping: 'WorkflowId' },
        { name: 'Type', type: 'string', mapping: 'Type' },
        { name: 'Prefix', type: 'string', mapping: 'Prefix' },
        { name: 'Name', type: 'string', mapping: 'Name' },
        { name: 'DisplayName', type: 'string', mapping: 'DisplayName' },
        { name: 'ActCode', type: 'string', mapping: 'ActCode' },
        { name: 'IconIndex', mapping: 'IconIndex' },
        { name: 'Sequence', mapping: 'Sequence' },
        { name: 'Active', mapping: 'Active' }
    ],
    proxy:{
        type:'rest',
        url: Workflow.global.Config.baseUrl + 'api/worklists/activities',
        reader:{
                type: 'json',
                rootProperty: 'Records'
        }
    }
});