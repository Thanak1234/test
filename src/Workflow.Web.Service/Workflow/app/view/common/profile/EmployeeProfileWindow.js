
Ext.define("Workflow.view.common.profile.EmployeeProfileWindow",{
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.common.profile.EmployeeProfileWindowController",
        "Workflow.view.common.profile.EmployeeProfileWindowModel",
        "Ext.ProgressBar"
    ],

    controller: "common-profile-employeeprofilewindow",
    viewModel: {
        type: "common-profile-employeeprofilewindow"
    },
    iconCls: 'fa fa-user',
    title: "My Profile", 
    reference:'emp-profile-form',
    width: 850,
    buttons: [
       {
           xtype: 'button',
           text: 'Close',
           handler: 'closeWindow'
       }
    ],
    buildItems : function (){
        return [{
            xtype: 'form',
            layout: 'column',
            autoWidth: true,
            bodyPadding: 10,
            defaults: {
                layout: 'form',
                xtype: 'container',
                defaultType: 'textfield',
                flex : 1
            },
            items: [
            {
                width: 400,
                items: [                   
                    { fieldLabel: 'Employee No', bind: '{employeeInfo.employeeNo}', readOnly: true },
                    { fieldLabel: 'Position', bind: '{employeeInfo.position}', readOnly: true },
                    { fieldLabel: 'Sub Department', bind: '{employeeInfo.subDept}', readOnly: true },
                    { fieldLabel: 'Group', bind: '{employeeInfo.groupName}', readOnly: true },
                    { fieldLabel: 'Division', bind: '{employeeInfo.devision}', readOnly: true }
                ]
            }, {
                width: 400,
                items: [
                        { fieldLabel: 'Phone', bind: '{employeeInfo.phone}', readOnly: true },
                        { fieldLabel: 'Phone (Ext)', bind: '{employeeInfo.ext}', readOnly: true },
                        { fieldLabel: 'Email', bind: '{employeeInfo.email}', readOnly: true },
                        { fieldLabel: 'HoD', bind: '{employeeInfo.hod}', readOnly: true }
                ]
            }]
            
            
            
        }];
    }
     
});
