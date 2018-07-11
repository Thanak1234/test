Ext.define('Workflow.view.eom.EOMDetailViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.eom-eomdetailview',
    
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
            model.set('eomInfo', {
                month: new Date(data.data.month),
                aprd: data.data.aprd,
                cfie: data.data.cfie,
                lc: data.data.lc,
                tmp: data.data.tmp,
                psdm: data.data.psdm,
                totalScore: data.data.totalScore,
                reason: data.data.reason,
                cash: data.data.cash,
                voucher: data.data.voucher
            });
        }
       me.getView().getViewModel().set('viewSetting', data.viewSetting );      
    },
    clearData : function(){
        this.getView().reset();
    }    
});
