/**
 * 
 *Author : Phanny 
 */
Ext.define('Workflow.store.common.Priorities', {
    extend: 'Ext.data.ArrayStore',

    alias: 'store.priorities',

    model: 'Workflow.model.common.Priorities',
    
    storeId: 'priorities',
    
    data: [
        [2, 'Low', 'Low'],
        [1, 'Medium', 'Medium'],
        [0, 'High', 'High']
    ]
});
