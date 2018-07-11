Ext.define('Workflow.view.events.avir.AvirFormController', {
    extend: 'Workflow.view.events.common.FormBaseController',
    alias: 'controller.event-avir',
    actionUrl: Workflow.global.Config.baseUrl + 'api/AvirForm',
    clearData: function () {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var vm = v.getViewModel();

        var reporterInfo = r.reporterInfo;
        var bodyinfo = r.bodyinfo;
        r.complaint.reset();

        r.incidentDate.setValue(null);
        vm.set('formRequestData.location', null);
        vm.set('formRequestData.complaintRegarding', null);
        
    },
    loadData: function (data) {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var vm = v.getViewModel();
        if (data.formRequestData) {            
            vm.set('formRequestData', data.formRequestData);
            vm.set('formRequestData.employeeId', data.formRequestData.receiverId);
            var incidentDate = data.formRequestData.incidentDate ? new Date(data.formRequestData.incidentDate) : '';
            r.incidentDate.setValue(incidentDate);
            me.loadReportBy(data);
        }
        vm.set('viewSetting', data.viewSetting);
        me.loadReporter(data);
    },
    loadReportBy: function (data) {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var vm = v.getViewModel();
        r.reportBy.getStore().load({
            id: data.formRequestData.reporterId,
            callback: function (records, operation, success) {
                if (success) {
                    r.reportBy.getTrigger('clear').hide();
                    r.reportBy.getTrigger('edit').hide();
                }
            }
        });
    },
    validateForm: function () {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var vm = v.getViewModel();

        var reporterInfo = r.reporterInfo;
        var report = r.report;
        var complaint = r.complaint;

        if (!reporterInfo.form.isValid() || !report.form.isValid() || !complaint.form.isValid()) {
            return "Some field(s) of Request Information is required. Please input the required field(s) before you click the Submit button.";
        }
    },
    getRequestItem: function () {
        var me = this;
        var v = me.getView();
        var r = v.getReferences();
        var vm = v.getViewModel();
        var incidentDate = r.incidentDate.getValue()
        var data = {
            formRequestData: {
                receiverId: vm.get('formRequestData.employeeId'),
                location: vm.get('formRequestData.location'),
                incidentDate: incidentDate,
                reporterId: vm.get('formRequestData.reporterId'),
                complaintRegarding: vm.get('formRequestData.complaintRegarding'),
                complaint: vm.get('formRequestData.complaint')
            }
        };

        return data;
    }
    
});
