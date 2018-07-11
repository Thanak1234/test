/**
 * navigation register menus
 *Author : Phanny 
 */

Ext.define('Workflow.model.Navigation', {
    extend: 'Ext.data.Model',
    fields: [
                { name: 'text' },
                { name: 'view' },
                { name: 'leaf' },
                { name: 'iconCls' },
                { name: 'routeId' },
                { name: 'closableTab' }
            ]
    
});
