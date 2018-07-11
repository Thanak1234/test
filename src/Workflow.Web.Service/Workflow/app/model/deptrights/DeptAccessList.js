Ext.define('Workflow.model.deptrights.DeptAccessList', {
    extend: 'Ext.data.Model',
    fields: [
        'id',
        'userid',
        'deptid',
        'reqapp',
        'requestdesc',
        'displayname',
        'deptname',
        'empno',
        'createdby',
        'createddate',
        'modifiedby',
        'modifieddate',
        'status'
    ]
});