/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.bcj.AnalysisItemWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.bcj-analysisItemwindow',
    
    getFormItem: function () {
        var me = this,
            viewModel = me.getView().getViewModel();

        var analysisItemModel = Ext.create('Workflow.model.bcjForm.AnalysisItem', {
            description: viewModel.get('description'),
            revenue: viewModel.get('revenue'),
            quantity: viewModel.get('quantity')
        });

        return analysisItemModel;
    }
    
});
