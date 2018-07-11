Ext.define('Workflow.view.eom.EOMFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.eom-eomform',

    actionUrl: Workflow.global.Config.baseUrl + 'api/eomrequest',

    renderSubForm: function (data) {
        var me = this,
            view = me.getView(),
            eomdetailview = view.getReferences().eomdetailview,
            viewSetting = view.getWorkflowFormConfig();
        eomdetailview.fireEvent('onDataClear');
        eomdetailview.fireEvent('loadData', { data: data && data.eomInfo ? data.eomInfo : null, viewSetting: viewSetting });        
    },
    clearData: function () {
        var view = this.getView();
        view.getReferences().eomdetailview.fireEvent('onDataClear');
    },

    validateForm: function (data) {
        var me = this,
          view = me.getView(),
          viewmodel = view.getReferences().eomdetailview.getViewModel(),
        month = view.getReferences().eomdetailview.getReferences().month;

        if (month.getValue() == null) {
            return "Please select the month field before you click the Submit button.";
        }

        if (me.validateRating() != 5) {
            return "Please input 5 Competency/Attributes fields before you click the Submit button.";
        }

        if (viewmodel.get('eomInfo.reason') == null || viewmodel.get('eomInfo.reason') == '') {
            return "Please input HOD/Manager supportive reason for nomination before you click the Submit button.";
        }

        if (data.activity.toLowerCase() == 't&d review' && data.action.toLowerCase() == 'reviewed' && viewmodel.get('eomInfo.cash') == 0 && viewmodel.get('eomInfo.voucher') == 0) {
            return "Please input Cash or Voucher before you click the Submit button.";
        }

        if (data.activity.toLowerCase() == 't&d review' && data.action.toLowerCase() == 'reviewed' && (me.isEmpty(viewmodel.get('eomInfo.cash')) || me.isEmpty(viewmodel.get('eomInfo.voucher')))) {
            return "Please input Cash or Voucher before you click the Submit button.";
        }
    },
    isEmpty: function(model) {
        if (model == undefined || model == null)
            return true;
        return false;
    },
    getRequestItem: function () {
        var me = this,
          view = me.getView(),
          viewmodel = view.getReferences().eomdetailview.getViewModel();

        var eomInfo = viewmodel.get('eomInfo');

        var data = {
            eomInfo: eomInfo
        };

        return data;
    },
    validateRating: function () {
        var me = this;
        var count = 0;
        var view = me.getView();
        var viewmodel = view.getReferences().eomdetailview.getViewModel();
        var eomInfo = viewmodel.get('eomInfo');
        var rates = ['aprd', 'cfie', 'lc', 'tmp', 'psdm'];

        function contain(array, v) {
            var rtn = false;
            Ext.each(array, function (val, index) {
                if (v == val) {
                    rtn = true;
                    return;
                }
            });

            return rtn;
        }

        for (prop in eomInfo) {
            if (contain(rates, prop)) {
                if (eomInfo[prop] > 0) {
                    count = count + 1;
                }
            }
        }

        return count;
    }
});
