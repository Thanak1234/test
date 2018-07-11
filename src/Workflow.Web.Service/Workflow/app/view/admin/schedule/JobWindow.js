
Ext.define("Workflow.view.admin.schedule.JobWindow", {
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",
    controller: "schedule-jobwindow",
    viewModel: {
        type: "schedule-jobwindow"
    },
    
    initComponent: function () {
        var me = this;
         me.items=[{
            xtype: 'form',
            frame: false,
            bodyPadding: '10 10 0',
            reference: 'form', 
            method: 'POST',
            defaultListenerScope: true,
            defaults: {
                flex        : 1,
                anchor      : '100%', 
                msgTarget   : 'side',
                labelWidth  : 150, 
                layout      : 'form', 
                xtype: 'displayfield'
            },  
            items: [
                {
                    fieldLabel: 'Job',
                    readOnly: true,
                    bind: {
                        value: '{item.job}'
                    }

                },
                {
                    xtype: 'textfield',
                    fieldLabel: 'Schedule',
                    bind: {
                        value: '{item.schedule}',
                        readOnly: '{!canChange}'
                    }
                },
                {
                    fieldLabel: 'Start Date',
                    readOnly: true,
                    frame: true,
                    bind: {
                        value: '{item.startDate}'
                    },
                    renderer: function (value, field) {
                        return Ext.util.Format.date(value, 'Y-m-d H:i:s');
                    }

                },
                {
                    fieldLabel: 'End Date',
                    readOnly: true,
                    bind: {
                        value: '{item.endDate}'
                    },
                    renderer: function (value, field) {
                        return Ext.util.Format.date(value, 'Y-m-d H:i:s');
                    }

                },
                {
                    fieldLabel: 'Prevoius Fire Date',
                    readOnly: true,
                    bind: {
                        value: '{item.previousFireDate}'
                    },
                    renderer: function (value, field) {
                        return Ext.util.Format.date(value, 'Y-m-d H:i:s');
                    }

                },
                {
                    fieldLabel: 'Next Fire Date',
                    readOnly: true,
                    bind: {
                        value: '{item.nextFireDate}'
                    },
                    renderer: function (value, field) {
                        return Ext.util.Format.date(value, 'Y-m-d H:i:s');
                    }
                }
            ]
        }];
        me.callParent(arguments);
    }
});
