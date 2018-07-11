Ext.define('Workflow.model.common.Employee', {
    extend: 'Workflow.model.Base',
    idProperty: 'Id',
    fields: [
        "Id","DeptId","LoginName","EmpNo","JobTitle","DisplayName","Email"
        , "Telephone", "MobilePhone", "HomePhone", "IpPhone", "Address"
        , {
            name: "HiredDate",
            convert: function (value) { return value ? Ext.Date.format(new Date(value), 'Y-m-d'): value; }
        }
        , "ReportTo", "DeptName", "FirstName", "LastName", "EmpType", "Active"
    ],
    proxy: {
        writer: { writeAllFields: true },
        type: 'rest',
        api: {
            read: Workflow.global.Config.baseUrl + 'api/employee/manual',
            create: Workflow.global.Config.baseUrl + 'api/employee',
            update: Workflow.global.Config.baseUrl + 'api/employee',
            destroy: Workflow.global.Config.baseUrl + 'api/employee'
        },
        actionMethods: {
            read: 'GET',
            destroy: 'DELETE',
            update: 'PUT',
            create: 'POST'
        },
        reader: {
            type: 'json',
            rootProperty: 'Records',
            totalProperty: 'TotalRecords'
        }
    }
});