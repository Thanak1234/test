/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.bcj.RequestItemWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.bcj-requestitemwindow',
    
    getFormItem: function () {
        var me = this,
            viewModel = me.getView().getViewModel();

        var requestItemModel = Ext.create('Workflow.model.bcjForm.RequestItem', {
            item: viewModel.get('item'),
            unitPrice: viewModel.get('unitPrice'),
            quantity: viewModel.get('quantity')
        });
        
        return requestItemModel;
    }
    
});
