Ext.define("Workflow.view.common.worklists.RedirectWindow", {
    extend: "Workflow.view.common.worklists.EmployeeWindow",
    xtype: 'common-worklists-redirectindow',
    title: 'Redirect To User',
    showComment: true,
    buttons: [
        {
            text: 'Redirect',
            handler: 'onAddClickHandler'
        },
        {
            text: 'Cancel',
            handler: 'closeWindow'
        }
    ]
});
