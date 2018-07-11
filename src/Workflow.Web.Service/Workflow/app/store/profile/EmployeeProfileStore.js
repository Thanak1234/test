Ext.define('Workflow.store.profile.EmployeeProfileStore', {
    extend: 'Ext.data.Store', 
    model: 'Workflow.model.profile.EmployeeProfileModel',  
    alias: 'store.employeeprofile', 
    storeId: 'employeeprofileId', 
    autoLoad: true 
    
});
