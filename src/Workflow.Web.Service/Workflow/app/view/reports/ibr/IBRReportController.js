﻿Ext.define('Workflow.view.reports.ibr.IBRReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-ibr',
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
