Ext.define('Workflow.model.profile.EmployeeProfileModel', {
    extend: 'Ext.data.Model',
    idProperty: 'employeeProfileId',
    fields: [
        { name: 'id' },
        { name: 'empNo', mapping: 'empNo', type: 'string' },
        { name: 'displayName', mapping: 'displayName', type: 'string' },
        { name: 'position', mapping: 'position', type: 'string' },
        { name: 'email', mapping: 'email', type: 'string' },
        { name: 'telephone', mapping: 'telephone', type: 'string' },
        { name: 'mobilePhone', mapping: 'mobilePhone', type: 'string' },
        { name: 'deptId', mapping: 'deptId', type: 'string' },
        { name: 'groupName', mapping: 'groupName', type: 'string' },
        { name: 'deptType', mapping: 'deptType', type: 'string' },
        { name: 'devision', mapping: 'deptType', type: 'string' },
        { name: 'reportTo', mapping: 'reportTo', type: 'string' },
        { name: 'address', mapping: 'address', type: 'string' },
        { name: 'hiredDate', mapping: 'hiredDate', type: "string" },
        { name: 'hod', mapping: 'hod', type: 'string' },
        { name: 'teamName', mapping: 'teamName', type: 'string' },
        { name: 'active', mapping: 'active' },
        { name: 'jobTitle' }
    ],


    proxy: {
        type: 'rest',
        datatype: 'json',
        api: {
            create: Workflow.global.Config.baseUrl + 'api/profile/CreateEmployeeProfile?empNo=013257',
            read: Workflow.global.Config.baseUrl + 'api/employee/currentuser',
            update: Workflow.global.Config.baseUrl + 'api/profile/UpdateEmployeeProfile?empNo=013257'
        },
        reader: {
            type: 'json',
            rootProperty: 'data',
            totalProperty: 'totalCount',
            idProperty: 'id',
            successProperty: 'success',
            messageProperty: 'message'
        },

        writer: {
            type: 'json',
            writeAllFields: true
        }
    }
});
