Ext.define('Workflow.model.common.worklists.Process', {
    extend: 'Ext.data.Model',
    fields: [
        { name: 'RequestDesc', type: 'string', mapping: 'RequestDesc' },
        { name: 'ProcessName', type: 'string', mapping: 'ProcessName' },
        { name: 'ProcessCode', type: 'string', mapping: 'ProcessCode' },
        { name: 'ProcessPath', type: 'string', mapping: 'ProcessPath' },
        { name: 'FormUrl', type: 'string', mapping: 'FormUrl' },
        { name: 'GenId', mapping: 'GenId' },
        { name: 'IconIndex', mapping: 'IconIndex' },
        { name: 'Active', mapping: 'Active' }
    ],
    proxy:{
        type:'rest',
        url: Workflow.global.Config.baseUrl + 'api/worklists/processes',
        reader:{
                type: 'json',
                rootProperty: 'Records'
        }
    }
});