
Ext.define("Workflow.view.common.profile.EmployeeProfile",{
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    xtype: 'form-employeeprofile',
    
    requires: [
        "Workflow.view.common.profile.EmployeeProfileController",
        "Workflow.view.common.profile.EmployeeProfileViewModel" 
    ], 
    
    controller: "common-profile-employeeprofilecontroller",
    viewModel: {
        type: "employeeprofile"
    }, 
    title: "Profile", 
    reference:'emp-profile-form',
    width: 850,  
    viewConfig: {
    },
    initComponent: function() {
        var me= this; 
        
        me.items=[me.buildItems];
        me.callParent(arguments);
    },
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
            items:[
            {
            width: 400,
            items: [
                { fieldLabel: 'Employee No',    bind: '{employeeprofile.empNo}',        readOnly  :true}, 
                { fieldLabel: 'Position',       bind: '{employeeprofile.position}',     readOnly  :true}, 
                { fieldLabel: 'Sub Department', bind: '{employeeprofile.teamName}',     readOnly  :true},
                { fieldLabel: 'Group',          bind: '{employeeprofile.groupName}',    readOnly  :true},
                { fieldLabel: 'Devision',       bind: '{employeeprofile.devision}',     readOnly  :true},  
                { fieldLabel: 'Hired Date',     bind: '{employeeprofile.hiredDate}',    readOnly  :true},
                { fieldLabel: 'Address',        bind: '{employeeprofile.address}',      fieldCls  :'required'}
            ]}, {
            width: 400,
            items: [
                { fieldLabel: 'Display Name',   bind: {value: '{employeeprofile.displayName}'}, fieldCls:'required'},
                { fieldLabel: 'Email',          bind: '{employeeprofile.email}',        readOnly  :true},
                { fieldLabel: 'Telephone(ext)', bind: '{employeeprofile.telephone}' ,   fieldCls  :'required'},
                { fieldLabel: 'Mobile Phone',   bind: '{employeeprofile.mobilePhone}' , fieldCls  :'required'}, 
                { fieldLabel: 'HoD.',           bind: '{employeeprofile.hod}',          readOnly  :true },
                { fieldLabel: 'Report To',      bind: '{employeeprofile.reportTo}' ,    readOnly  :true}
                ]
            }]
            
            
            
        }];
    }
     
});
