Ext.define('Workflow.view.dashboard.AuditDialogController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.audit-dialog',
    onOkClick: function (btn, e) {
        var me = this;
        var v = me.getView();
        v.close();
    },
    onRefreshClick: function (btn, e, opts) {
        var me = this;
        var r = me.getReferences();
        var grid = r.grid;
        var store = grid.getStore();

        store.reload();
    }
});

