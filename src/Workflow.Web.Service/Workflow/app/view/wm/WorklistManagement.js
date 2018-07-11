Ext.define("Workflow.view.wm.WorklistManagement", {
    extend: "Ext.panel.Panel",
    xtype: 'worklistmanagement',
    controller: 'worklistmanagement',
    viewModel: {
        type: 'worklistmanagement'
    },
    title: '<span style="font-size: 16px;">Worklist Management</span>',
    titleAlign: 'center',
    layout: 'border',
    width: Workflow.global.Config.winSize,
    bodyBorder: true,
    initComponent: function () {
        var me = this;
        me.callParent(arguments);
    },
    items: [
        {
            xtype: 'wm-criteria',
            region: 'north'
        },
        {
            xtype: 'wm-result',
            region: 'center'            
        }
    ]
});
