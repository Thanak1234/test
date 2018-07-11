Ext.define('Workflow.store.GuestFloorLookups', {
    extend: 'Ext.data.Store',
    alias: 'store.mwo-lookup',
    storeId: 'mwo-lookup',
    model: 'Workflow.model.mwo.GuestFloorLookup'
});
