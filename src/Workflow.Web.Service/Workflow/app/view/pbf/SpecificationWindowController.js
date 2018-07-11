/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.pbf.SpecificationWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.pbf-specificationwindow',
    
    getFormItem: function () {
        var me = this,
            viewModel = me.getView().getViewModel();

        var specificationModel = Ext.create('Workflow.model.pbfForm.Specification', {
            description: viewModel.get('description'),
            name: viewModel.get('name'),
            itemId: viewModel.get('itemId'),
            quantity: viewModel.get('quantity'),
            itemRef: viewModel.get('itemRef')
        });

        return specificationModel;
    }
    
});
