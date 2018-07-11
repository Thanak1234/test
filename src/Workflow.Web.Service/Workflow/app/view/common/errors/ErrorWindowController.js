Ext.define('Workflow.view.common.errors.ErrorWindowController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.errorwindow',
    onToggleClick: function (btn, pressed, eOpts) {
        var me = this;
        var v = me.getView();
        var r = me.getReferences();
        var detail = r.detail;

        if (pressed) {
            detail.setHidden(false);
        } else {
            detail.setHidden(true);
        }        
        
    },
    onOkClick: function (btn, e) {
        var me = this;
        var v = me.getView();

        v.close();
    }
});

