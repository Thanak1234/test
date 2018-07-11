
Ext.define("Workflow.view.view.common.employee.employeePickup",{
    extend: "Ext.panel.Panel",

    requires: [
        "Workflow.view.view.common.employee.employeePickupController",
        "Workflow.view.view.common.employee.employeePickupModel"
    ],

    controller: "view-common-employee-employeepickup",
    viewModel: {
        type: "view-common-employee-employeepickup"
    },

    html: "Hello, World!!"
});
