Ext.define('Workflow.view.events.avdr.AvdrFormController', {
    extend: 'Workflow.view.events.common.FormBaseController',
    alias: 'controller.event-avdr',
    actionUrl: Workflow.global.Config.baseUrl + 'api/AvdrForm',
    clearData: function () {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var vm = v.getViewModel();
        
        var reporterInfo = r.reporterInfo;
        var bodyinfo = r.bodyinfo;
        bodyinfo.reset();
        vm.set('formRequestData.incidentDate', null);
        vm.set('formRequestData.sdl', 'Damage');
        vm.set('formRequestData.elocation', null);
    },
    loadData: function (data) {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var vm = v.getViewModel();        
        if (data.formRequestData) {
            var incidentDate = data.formRequestData.incidentDate ? new Date(data.formRequestData.incidentDate) : '';
            vm.set('formRequestData', data.formRequestData);
            vm.set('formRequestData.employeeId', data.formRequestData.reporterId);
            vm.set('formRequestData.incidentDate', incidentDate);
        }        
        vm.set('viewSetting', data.viewSetting);
        me.loadReporter(data);
    },
    validateForm: function () {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var vm = v.getViewModel();

        var reporterInfo = r.reporterInfo;
        var bodyinfo = r.bodyinfo;

        if (!reporterInfo.form.isValid() || !bodyinfo.form.isValid()) {
            return "Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.";
        }
    },
    getRequestItem: function () {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var vm = v.getViewModel();
        var data = {
            formRequestData: {
                reporterId: vm.get('formRequestData.employeeId'),
                incidentDate: vm.get('formRequestData.incidentDate'),
                sdl: vm.get('formRequestData.sdl'),
                elocation: vm.get('formRequestData.elocation'),
                dle: vm.get('formRequestData.dle'),
                ein: vm.get('formRequestData.ein'),
                hedl: vm.get('formRequestData.hedl'),
                at: vm.get('formRequestData.at'),
                ecrr: vm.get('formRequestData.ecrr'),
                dcirs: vm.get('formRequestData.dcirs')
            }
        };

        return data;
    }
});
