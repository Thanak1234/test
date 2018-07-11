/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.av.JobDetailViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.av-jobdetailview',
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    
    loadData: function(data){
        var me      = this,
            model   = me.getView().getViewModel();
           
        if(data.data){
                 
            model.set('jobDetail', {
                id          : data.data.id,
                projectName : data.data.projectName,
                location    : data.data.location,
                setupDate   : new Date(data.data.setupDate),
                actualDate  : new Date(data.data.actualDate),
                setupTime   : new Date(data.data.setupDate),
                actualTime: new Date(data.data.actualDate),
                projectBrief: data.data.projectBrief,
                other: data.data.other
            });
                
        }
       me.getView().getViewModel().set('viewSetting', data.viewSetting );      
    },
     clearData : function(){
        this.getView().reset();
    }
    
});
