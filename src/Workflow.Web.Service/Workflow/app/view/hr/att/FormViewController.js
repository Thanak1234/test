Ext.define('Workflow.view.hr.att.FormViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.att-form',
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear: 'clearData',
                updateTokenInYear: 'updateTokenInYear'
            }
        }
    },
    updateTokenInYear: function (data) {
        var me = this;
        var v = me.getView();
        var vm = v.getViewModel();

        var purposeId = me.getReferences().purposeTravel.getValue().purposeTravel;

        if (!purposeId) {
            purposeId = 0;
        }

        if (data.requestorId) {
            vm.set('travelDetail.noRequestTaken', v.getTokenInThisYear(data.requestorId, purposeId));
        } else {
            vm.set('travelDetail.noRequestTaken', 0);
        }
    },
    loadData: function (data) {
        var me = this,
            view = me.getView(),
            model = me.getView().getViewModel();

        me.view.getForm().reset(); // clean form before bind model.
        if (data.travelDetail) {

            //if (data.travelDetail.arrivalDate)
            //    data.travelDetail.arrivalDate = new Date(data.complimentary.arrivalDate);

            //if (data.travelDetail.departureDate)
            //    data.travelDetail.departureDate = new Date(data.complimentary.departureDate);

            model.set('travelDetail', data.travelDetail);
            
        }
        model.set('viewSetting', data.viewSetting);
    },
    clearData : function(){
        this.getView().reset();
    },
    onPurposeChanged: function (cb, nv, ov) {
        var requestorId = this.getViewModel().get('requestorId');
        this.updateTokenInYear({ requestorId: requestorId });

    }
});
