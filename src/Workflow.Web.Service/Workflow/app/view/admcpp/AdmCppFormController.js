Ext.define('Workflow.view.admcpp.AdmCppFormController', {
    extend: 'Workflow.view.AbstractRequestFormController',
    alias: 'controller.admcppform',

    actionUrl: Workflow.global.Config.baseUrl + 'api/admcpprequest',

    renderSubForm: function (data) {
        var me = this,
            view = me.getView(),
            admcppdetailview = view.getReferences().detailview,
            viewSetting = view.getWorkflowFormConfig();
        admcppdetailview.fireEvent('onDataClear');
        admcppdetailview.fireEvent('loadData', { data: data && data.admcpp ? data.admcpp : null, viewSetting: viewSetting });
    },
    clearData: function () {
        var view = this.getView();
        view.getReferences().detailview.fireEvent('onDataClear');
    },

    validateForm: function (data) {
        var me = this,
          view = me.getView(),
          admcppdetailview = view.getReferences().detailview,
          viewmodel = admcppdetailview.getViewModel();

        var activity = data.activity;
        var adminissue = admcppdetailview.getReferences().adminissue;
        var verhicle = admcppdetailview.getReferences().verhicle;

        var response = Ext.Ajax.request({
            async: false,
            url: Workflow.global.Config.baseUrl + 'api/employee/serverdate',
            method: 'GET'
        });

        var currentDate = Ext.JSON.decode(response.responseText);
        var date = new Date(currentDate.now);       

        if (activity.toLowerCase() != 'Admin Issue'.toLocaleLowerCase() && !verhicle.form.isValid())
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';

        if (viewmodel.get('admcpp.yop') > date.getFullYear()) {
            return Ext.String.format('Year Of Production must be less than or equal to {0}.', date.getFullYear());
        }
        if (activity.toLowerCase() === 'Admin Issue'.toLocaleLowerCase() && !adminissue.form.isValid()) {
            return 'Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.';
        }
    },
    getRequestItem: function () {
        var me = this,
          view = me.getView(),
          viewmodel = view.getReferences().detailview.getViewModel();

        var admcpp = viewmodel.get('admcpp');

        var data = {
            admcpp: admcpp
        };

        return data;
    }
});
