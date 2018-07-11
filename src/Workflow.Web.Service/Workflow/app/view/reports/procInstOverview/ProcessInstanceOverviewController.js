Ext.define('Workflow.view.reports.procInstOverview.ProcessInstanceOverviewController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-processinstanceoverview',
    buildStore: function (store, params, viewmodel) {        
        return store;
    },
    onPerformanceRefreshClick: function (btn, e, opts) {
        var me = this;
        var vm = me.getViewModel();
        var r = me.getReferences();

        var userPerformance = r.userPerformance;
        userPerformance.getStore().load();
    },
    onActivityInstRowClicked: function (grid, record, tr, rowIndex, e, eOpts) {
        var me = this;
        var vm = me.getViewModel();
        var r = me.getReferences();
        var data = record.getData();

        var procSelectedRow = vm.get('procSelectedRow');
        var procData = procSelectedRow.getData();

        var userPerformance = r.userPerformance;
        var store = userPerformance.getStore();
        Ext.apply(store.getProxy().extraParams, {
            procInstId: procData.procInstId,
            procFullName: procData.applicationPath,
            actName: data.activityName
        });

        store.load();
    },
    onActivityDoubleClicked: function (grid, rowIndex, colIndex) {
        this.onViewClicked(grid);
    },
    onRefreshClick: function (btn, e, opts) {
        var me = this;
        var vm = me.getViewModel();
        var r = me.getReferences();

        var instActivity = r.instActivity;
        instActivity.getStore().load();
    },
    onProcInstRowClicked: function (grid, record, tr, rowIndex, e, eOpts) {
        var me = this;
        var vm = me.getViewModel();
        var r = me.getReferences();
        var data = record.getData();
        var procInstId = data.procInstId;
        var instActivity = r.instActivity;
        var store = instActivity.getStore();
        r.userPerformance.getStore().removeAll();
        Ext.apply(store.getProxy().extraParams, {
            procInstId: procInstId
        });
        store.load();
    }
});
