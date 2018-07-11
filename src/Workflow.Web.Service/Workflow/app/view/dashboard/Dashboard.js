Ext.define("Workflow.view.dashboard.Dashboard",{
    extend: 'Ext.tab.Panel',
    ui: 'ng-tab-panel-ui',
    cls: 'ng-tab-panel-ui',
    controller: "dashboard-dashboard",
    viewModel: {
        type: "dashboard-dashboard"
    },
    bodyPadding: 0,
    width: '100%',
    margin: 0,
    layout: 'border',
    defaults: {
        bodyStyle: 'padding:0px'
    },
    ngconfig: {
        layout: 'fullScreen'
    },
    initComponent: function() {
        var me = this;

        me.items = [{
            xtype: "common-worklist",
            title: 'Worklist',
            active: true,
            reference: 'worklist',
            cls: 'ng-tab-item-ui',
            hidden: false,
            border: false
        }, {
            xtype: 'dashboard-taskcontributed',
            title: 'All Tasks I Contributed',
            reference: 'taskcontributed',
            cls: 'ng-tab-item-ui',
            border: false,
            hidden: false,
            tabConfig: {
                listeners: {
                    click: function () {
                        var ref = me.getReferences(),
                            taskcontributed = ref.taskcontributed;

                        if (taskcontributed) {
                            taskcontributed.loadData();
                        }
                    }
                }
            }
        }, {
            xtype: "dashboard-tasksubmitted",
            title: 'All Tasks I Submitted',
            reference: 'tasksubmitted',
            cls: 'ng-tab-item-ui',
            hidden: false,
            border: false,
            height: 500,
            tabConfig: {
                listeners: {
                    click: function () {
                        var ref = me.getReferences(),
                            tasksubmitted = ref.tasksubmitted;

                        if (tasksubmitted) {
                            tasksubmitted.loadData();
                        }
                    }
                }
            }
        }, {
            xtype: "report-processinstancereport",
            title: 'Process Instance Report',
            active: true,
            reference: 'searchTask',
            cls: 'ng-tab-item-ui',
            hidden: false,
            border: false
        }, {
            xtype: "draftlist",
            title: 'Draft List',
            active: true,
            reference: 'draftlist',
            cls: 'ng-tab-item-ui',
            hidden: false,
            border: false
        }];
        me.callParent(arguments);
    }
    //draftlist
});
