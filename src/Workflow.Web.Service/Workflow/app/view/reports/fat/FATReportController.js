Ext.define('Workflow.view.reports.fat.FATReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-fat',
    onClearClick: function (btn, e, eOpts) {
        btn.setValue(null);
    },
    onSelectChanged: function (el, newValue, oldValue, eOpts) {
        if(newValue){
            el.getTrigger('clear').show();
        }else{
            el.getTrigger('clear').hide();
        }
    }
});
