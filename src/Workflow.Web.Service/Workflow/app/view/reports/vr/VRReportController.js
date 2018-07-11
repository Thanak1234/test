Ext.define('Workflow.view.reports.vr.VRReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-vr',
    buildStore: function (store, params, viewmodel) {

        if (params.IsHotelVoucher != null && params.IsHotelVoucher == false) {
            params.IsHotelVoucher = null;
        }

        if (params.IsGamingVoucher != null && params.IsGamingVoucher == false) {
            params.IsGamingVoucher = null;
        }

        if (params.IsReprint != null && params.IsReprint == false) {
            params.IsReprint = null;
        }

        return store;
    }
});
