/* global Ext */
/**
 * Author: Phanny
 */
Ext.define('Workflow.model.common.FileUpload', {
    extend: 'Workflow.model.Base',
    idProperty: 'id',
    fields: [{ name: 'id', type: 'int' },
                { name: 'serial', type: 'string' },
                { name: 'name', type: 'string' },
                { name: 'description', type: 'string' },
                { name: 'fileName', type: 'string' },
                { name: 'fileType', type: 'string' },
                { name: 'isTemp', type: 'boolean', defaultValue: false },
                { name: 'activity', type: 'string' },
                { name: 'uploadDate', type: 'date' },
                { name: 'readOnly', type: 'boolean' }
    ],
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/forms/attachments',
        reader: {
            type: 'json',
            rootProperty: 'Records'
        }
    }
});
