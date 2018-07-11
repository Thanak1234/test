Ext.define('Workflow.view.reports.mt.MTReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-mt',
    buildParam: function (criteria) {
        
        criteria.AppName = 'MT_REQ';
        criteria.Shift = null;
        return criteria;
    },
    buildModel: function (fields) {
        //fields = [
        //     { name: 'Shift', type: 'string' }
        //]
        return fields;
    }
});