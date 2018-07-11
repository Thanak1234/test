Ext.define('Workflow.view.reports.mwo.MWOReportModel', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportModel',
    alias: 'viewmodel.report-mwo',
    buildConfig: function (showField) {
        showField.AppName = false;
        return showField;
    },
    buildParam: function (criteria) {
        criteria.LocationType = null;
        return criteria;
    },
    buildModel: function (fields) {
        fields = [
             { name: 'Mode', type: 'string' },
             { name: 'RequestType', type: 'string' },
             { name: 'ReferenceNumber', type: 'string' },
             { name: 'Location', type: 'string' },
             { name: 'LocationType', type: 'string' },
             { name: 'Description', type: 'string' },
             { name: 'CCD', type: 'string' },
             { name: 'Department', type: 'string' },
             { name: 'TechnicianNo', type: 'string' },
             { name: 'TechnicianName', type: 'string' },
             { name: 'AssignDate', type: 'date' },
             { name: 'Instruction', type: 'string' },
             { name: 'WorkType', type: 'string' },
             { name: 'RoomNo', type: 'string' },
             { name: 'SubLocation', type: 'string' },
             { name: 'WorkRequest', type: 'string' },
             { name: 'ItemCode', type: 'string' },
             { name: 'ItemDescription', type: 'string' },
             { name: 'Unit', type: 'float' },
             { name: 'UnitPrice', type: 'float' },
             { name: 'QtyRequested', type: 'float' },
             { name: 'QtyIssued', type: 'float' },
             { name: 'QtyReturn', type: 'float' },
             { name: 'Amount', type: 'float' },
             { name: 'PartType', type: 'string' }
        ]
        return fields;
    }
});