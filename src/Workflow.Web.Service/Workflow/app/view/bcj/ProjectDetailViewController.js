/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.bcj.ProjectDetailViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.bcj-projectdetailview',
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    
    loadData: function(data){
        var me = this,
            view = me.getView(),
            model = me.getView().getViewModel();

        me.view.getForm().reset(); // clean form before bind model.

        if (data.projectDetail) {
            data.projectDetail.completion = data.projectDetail.completion ? new Date(data.projectDetail.completion) : null;
            data.projectDetail.commencement = data.projectDetail.commencement ? new Date(data.projectDetail.commencement) : null;
            model.set('projectDetail', data.projectDetail);
        }
        model.set('viewSetting', data.viewSetting);
    },
    clearData : function(){
        this.getView().reset();
    }
    
});
