Ext.define('Workflow.view.admcpp.DetailViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.admcppdetailview',
    
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
            model.set('admcpp', {
                model: data.data.model,
                plateNo: data.data.plateNo,
                color: data.data.color,
                yop: data.data.yop,
                remark: data.data.remark,
                cpsn: data.data.cpsn,
                issueDate: data.data.issueDate ? new Date(data.data.issueDate): new Date()
            });
        }
       me.getView().getViewModel().set('viewSetting', data.viewSetting );      
    },
    clearData : function(){
        this.getView().reset();
    }    
});
