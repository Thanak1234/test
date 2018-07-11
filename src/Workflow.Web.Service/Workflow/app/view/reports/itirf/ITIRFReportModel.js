Ext.define('Workflow.view.reports.itirf.ITIRFReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-itirf',
    buildConfig: function (showField) {
        showField.AppName = false;
        return showField;
    },
    buildParam: function (criteria) {
        criteria.VenderName = null;
        return criteria;
    },
    buildModel: function (fields) {
        fields = [
             { name: 'ItemName', type: 'string' },
             { name: 'ItemModel', type: 'string' },
             { name: 'SerialNo', type: 'string' },
             { name: 'PartNo', type: 'string' },
             { name: 'Qty', type: 'int' },
             { name: 'SendDate', type: 'date' },
             { name: 'ReturnDate', type: 'date' },
             { name: 'Vendor', type: 'string' },
             { name: 'ContactNumber', type: 'string' },
             { name: 'Email', type: 'string' },
             { name: 'Address', type: 'string' },
             { name: 'Remark', type: 'string' }
        ]
        return fields;
    }
});