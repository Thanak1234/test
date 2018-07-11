/**
 * 
 *Author : Phanny 
 */
Ext.define('Workflow.view.common.AbstractWindowDialogController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.common-windowdialog',
	closeWindow: function(){
        var me=this,
            w=me.getView();
            w.close();
    },
    sumbmitForm: function (){ 
        var me = this,
            view = me.getView(),
            form = view.getReferences().form.getForm();

        if (form.isValid() && view.cbFn) {

            if (me.moreValidation && !me.moreValidation()) {
                return;
            }

            var item = null;
            if (me.getFormItem) {
                item = me.getFormItem();
            } else {
                item = view.getViewModel().get('item');
            }

            if (me.parseItem) {
                item = me.parseItem();
            }

            if (view.cbFn(item)) {
                if (view.getViewModel().get('action').toUpperCase() === 'EDIT') {
                    me.closeWindow();
                    return;
                }
                console.log('clear form');
                form.reset();
                view.getViewModel().set('item', null);
            }

        }
        
    }
	
});