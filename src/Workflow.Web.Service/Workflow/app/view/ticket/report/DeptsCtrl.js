Ext.define('Workflow.view.ticket.report.DeptsCtrl', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.ticket-report-depts',
    onSaveClick: function () {
        var me = this;
        var main = me.getView().main;

        var vm = me.getView().getViewModel();
        var refs = me.getReferences();

        var store = refs.selector.getStore();

        main.setDepts(store);
        me.getView().close();
    },
    onClearClick: function () {
        var me = this;
        var refs = me.getReferences();

        if (refs.selector.searchPopup)
            refs.selector.searchPopup.lookupReference('searchGrid').getSelectionModel().deselectAll();

        refs.selector.getStore().removeAll();
    }
});
