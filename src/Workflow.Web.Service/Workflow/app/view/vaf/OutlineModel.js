Ext.define('Workflow.view.vaf.OutlineModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.vaoutline',
    data: {
        selectedRow: null,
        viewsetting: null,
        totalAmount: Ext.util.Format.number(0, '$0,000.00')
    },

    formulas: {
        crud: function (get) {
            if (get('viewSetting') && get('viewSetting').container.crud) {
                return true;
            } else {
                return false;
            }
        }
    }
});
