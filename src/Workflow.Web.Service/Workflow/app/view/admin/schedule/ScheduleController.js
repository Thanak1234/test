Ext.define('Workflow.view.admin.ScheduleController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.admin-schedule',
    init: function () {
        var me = this;
        me.loadData();
    },
    onActionClick: function (el, v) {
        var me = this;

        el.jsonData.method = el.method;

        Ext.Msg.show({
            title: 'Action Confirm',
            message: 'Would you like to take this action?',
            buttons: Ext.Msg.YESNO,
            icon: Ext.Msg.QUESTION,
            fn: function (btn) {
                if (btn === 'yes') {
                    Ext.Ajax.request({
                        url: '/api/scheduler/executeAction',
                        method: 'POST',
                        headers: { 'Content-Type': 'text/html' },
                        jsonData: el.jsonData,
                        success: function (response) {
                            me.loadData();
                        },
                        failure: function (response) {
                            Ext.Msg.show({
                                title: 'System error',
                                message: response.responseText,
                                buttons: Ext.Msg.OK,
                                icon: Ext.Msg.ERROR
                            });
                        }
                    });
                }
            }
        });
    },
    onViewClick: function(el) {
        var me = this;
        var vm = me.getView().getViewModel();
        var selectedRow = vm.get('selectedRow');
        var data = selectedRow.getData();

        me.showWindowDialog(el, Ext.String.format('Edit Schedule ( {0} )', 'Error Sender'),
            {
                submitBtText: 'Update',
                action: 'View',
                submitBtVisible: false,
                canChange: false,
                item: {
                    id: 0,
                    group: data.GroupName,
                    job: data.Name,
                    trigger: data.Triggers[0].Name,
                    schedule: data.Triggers[0].TriggerType.CronExpression,
                    startDate: data.Triggers[0].StartDate,
                    endDate: data.Triggers[0].EndDate,
                    previousFireDate: data.Triggers[0].PreviousFireDate,
                    nextFireDate: data.Triggers[0].NextFireDate
                }
            });
    },
    onEditClick: function (el) {
        var me = this;
        var vm = me.getView().getViewModel();
        var selectedRow = vm.get('selectedRow');
        var data = selectedRow.getData();

        me.showWindowDialog(el, Ext.String.format('Edit Schedule ( {0} )', 'Error Sender'),
            {
                submitBtText: 'Update',
                action: 'Edit',
                canChange: true,
                item: {
                    id: 1,
                    group: data.GroupName,
                    job: data.Name,
                    trigger: data.Triggers[0].Name,
                    schedule: data.Triggers[0].TriggerType.CronExpression,
                    startDate: data.Triggers[0].StartDate,
                    endDate: data.Triggers[0].EndDate,
                    previousFireDate: data.Triggers[0].PreviousFireDate,
                    nextFireDate: data.Triggers[0].NextFireDate
                }
            },
            function (item) {
                Ext.Ajax.request({
                    url: '/api/scheduler/reschedule',
                    method: 'POST',
                    headers: { 'Content-Type': 'text/html' },
                    jsonData: {
                        Group: item.group,
                        Job: item.job,
                        Trigger: item.trigger,
                        CronExpression: item.schedule
                    },
                    success: function (response) {
                        me.loadData();
                    },
                    failure: function (response) {
                        Ext.Msg.show({
                            title: 'System error',
                            message: response.responseText,
                            buttons: Ext.Msg.OK,
                            icon: Ext.Msg.ERROR
                        });
                    }
                });
                return true;
            });
    },
    showWindowDialog: function (el, title, data, cbFn) {
        var me = this;
        var dialog = Ext.create('Workflow.view.admin.schedule.JobWindow', {
            mainController: me,
            title: title,
            viewModel: {
                data: data
            },
            cbFn: cbFn
        });
        dialog.show(el);
    },
    takeAction: function (el) {
        var me = this;
        var m = el.method;

        var msg = 'Would you like to start scheduler?';

        if (m === "stopscheduler") {
            msg = 'Would you like to shutdown scheduler? If you shutdown you can\'t start scheduler so you need restart web server.';
        }

        Ext.Msg.show({
            title: 'Action Confirm',
            message: msg,
            buttons: Ext.Msg.YESNO,
            icon: Ext.Msg.QUESTION,
            fn: function (btn) {
                if (btn === 'yes') {
                    Ext.Ajax.request({
                        url: '/api/scheduler/' + m,
                        method: 'POST',
                        headers: { 'Content-Type': 'text/html' },
                        success: function (response) {
                            me.loadData();
                        },
                        failure: function (response) {
                            Ext.Msg.show({
                                title: 'System error',
                                message: response.responseText,
                                buttons: Ext.Msg.OK,
                                icon: Ext.Msg.ERROR
                            });
                        }
                    });
                }
            }
        });
    },
    onRefresh: function() {
        var me = this;
        var vm = me.getView().getViewModel();
        vm.set('selectedRow', null);
        me.loadData();
    },
    loadData: function () {
        var me = this;
        var vm = me.getView().getViewModel();
        var r = me.getReferences();

        Ext.Ajax.request({
            url: '/api/scheduler/data',
            method: 'GET',
            headers: { 'Content-Type': 'text/html' },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);                
                vm.set('scheduleData', data);
                if (data.JobGroups != null && data.JobGroups.length > 0) {
                    var store = new Ext.data.JsonStore();
                    store.loadData(data.JobGroups[0].Jobs);
                    r.defaultGroup.setStore(store);
                }
            },
            failure: function (response) {
                Ext.Msg.show({
                    title: 'System error',
                    message: response.responseText,
                    buttons: Ext.Msg.OK,
                    icon: Ext.Msg.ERROR
                });
            }
        });
    }
});
