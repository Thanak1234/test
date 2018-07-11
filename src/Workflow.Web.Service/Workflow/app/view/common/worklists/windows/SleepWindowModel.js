Ext.define('Workflow.view.common.worklists.SleepWindowModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.common-worklists-sleepwindow',
    data: {
        duration: 24,
        status: true
    },
    stores: {
        options: {
            type: 'store',
            model: 'Workflow.model.common.worklists.Option',
            autoLoad: true
        }
    }
});
