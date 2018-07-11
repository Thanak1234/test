/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.common.activity.ActivityHistory",{
    extend: "Ext.grid.Panel",
    xtype: 'activity-activity-history',

    requires: [
        "Workflow.view.common.activity.ActivityHistoryController",
        "Workflow.view.common.activity.ActivityHistoryModel"
    ],

    controller: "common-activity-activityhistory",
    viewModel: {
        type: "common-activity-activityhistory"
    },

   listeners: {
            rowdblclick: 'viewItemAction'
    },
    iconCls : 'fa fa-history',
    title: "Activity History",
    stateful: true,
    collapsible: true,
    headerBorders: false,
     
    bind: {
        selection   : '{selectedItem}'
    },
    
    viewConfig: {
        enableTextSelection: true
    },
    
    initComponent: function () {
        var me = this;

         me.columns = [
            {
                text        : 'User',
                width       : 200,
                sortable    : true,
                dataIndex   : 'appriverDisplayName'
            },{
                text        : 'DATE',
                width       : 150,
                sortable    : true,
                dataIndex: 'actionDate',
                renderer: function (value) {
                    var date = Ext.util.Format.date(value, 'Y-m-d H:i:s');
                    return date;
                }

            },{
                text        : 'ACTIVITY',
                width       : 120,
                sortable    : true,
                dataIndex   : 'activity'
            },{
                text        : 'DECISION',
                width       : 150,
                sortable    : true,
                dataIndex   : 'decision'
            },{
                text        : 'COMMENTS',
                flex        : 1,
                sortable    : true,
                dataIndex   : 'comment'
            }
            
         ];
         
         me.callParent(arguments);
        
    }
});
