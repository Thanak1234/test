Ext.define('Workflow.store.GuestFloorLookup', {
    extend: 'Ext.data.Store',
    alias: 'store.mwo-worktypes',
    storeId: 'mwo-worktypes',
    model: 'Workflow.model.mwo.WorkType',
    autoLoad: true,
    proxy: {
        type: 'rest',
        url: Workflow.global.Config.baseUrl + 'api/mwoitem/workTypes'
    }
});
