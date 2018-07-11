Ext.define('Workflow.view.WindowComponentController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.window-component',
    getFormItem: function () {
        var me = this,
            view = me.getView(),
            viewmodel = me.getView().getViewModel();
          
        
        if (view.modelName) {
            return viewmodel.get(view.modelName);
        }
    },
    sumbmitForm: function (){ 
        var me = this,
            view = me.getView(),
            viewmodel = view.getViewModel(),
            form = view.getReferences().form.getForm();

        if (form.isValid() && view.cbFn) {

            if (me.moreValidation && !me.moreValidation()) {
                return;
            }

            var record = null;
            if (me.getFormItem) {
                record = me.getFormItem();
            } else {
                record = viewmodel.get(view.modelName);
            }

            if (me.parseItem) {
                record = me.parseItem();
            }

            if (view.cbFn(record)) {
                if (viewmodel.get('action').toUpperCase() === 'EDIT') {
                    me.closeWindow();
                    return;
                }
                form.reset();
                viewmodel.set(view.modelName, null);

                if(view.closeAfterAdd){
                    me.closeWindow();
                    return;
                }
            }

        }
        
    }
});
