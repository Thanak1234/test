
Ext.define("Workflow.view.admin.Schedule",{
    extend: "Ext.panel.Panel",
    controller: "admin-schedule",
    viewModel: {
        type: "admin-schedule"
    },
    layout: 'border',
    width: 1100,
    bodyBorder: false,
    initComponent: function() {
        var me = this;
        me.buildItems();
        me.callParent(arguments);        
    },
    buildItems: function() {
        var me = this;
        me.items = [
            {
                xtype: 'panel',
                autoScroll: true,
                region: 'north',
                bodyPadding: 10,
                collapsible: true,
                border: true,
                defaults: {
                    border: false,
                    xtype: 'panel',
                    layout: 'anchor',
                    margin: 10
                },
                title: 'Scheduler',
                layout: 'hbox',
                split: true,
                items: [
                    {
                        layout: 'column',
                        width: '20%',
                        defaults: {
                            margin: '0 0 10 0',
                            textAlign: 'left'
                        },
                        items: [
                            {
                                xtype: 'button',
                                text: 'Start scheduler',
                                method: 'startscheduler',
                                iconCls: 'fa fa-play',
                                columnWidth: 1,
                                bind: {
                                    disabled: '{!scheduleData.CanStart}'
                                },
                                handler: 'takeAction'
                            },
                            {
                                xtype: 'button',
                                text: 'Shutdown',
                                method: 'stopscheduler',
                                iconCls: 'fa fa-stop',
                                columnWidth: 1,
                                bind: {
                                    disabled: '{!scheduleData.CanShutdown}'
                                },
                                handler: 'takeAction',
                                hidden: !Workflow.global.UserAccount.identity.isAdmin
                            },
                            {
                                xtype: 'button',
                                text: 'Refresh',
                                iconCls: 'fa fa-refresh',
                                columnWidth: 1,
                                handler: 'onRefresh',
                                bind: {
                                    disabled: '{scheduleData.CanStart}'
                                }
                            }
                        ]
                    },
                    {
                        title: 'Statistics',
                        width: '40%',
                        layout: 'vbox',
                        border: true,
                        defaults: {
                            width: '100%',
                            padding: '5 5 0 5',
                            labelWidth: '80%',
                            xtype: 'displayfield'
                        },
                        items: [
                            {
                                xtype: 'displayfield',
                                fieldLabel: 'Running since',
                                readOnly: true,
                                renderer: Ext.util.Format.dateRenderer('Y-m-d H:i:s'),
                                bind: {
                                    value: '{scheduleData.RunningSince}'
                                }
                            },
                            {

                                fieldLabel: 'Total Jobs',
                                readOnly: true,
                                bind: {
                                    value: '{scheduleData.JobsTotal}'
                                }
                            },
                            {

                                fieldLabel: 'Executed Jobs',
                                readOnly: true,
                                bind: {
                                    value: '{scheduleData.JobsExecuted}'
                                }
                            }
                        ]
                    },
                    {
                        title: 'Properties',
                        width: '40%',
                        layout: 'vbox',
                        border: true,
                        defaults: {
                            width: '100%',
                            padding: '5 5 0 5',
                            labelWidth: '80%',
                            xtype: 'displayfield'
                        },
                        items: [
                            {

                                fieldLabel: 'Name',
                                readOnly: true,
                                bind: {
                                    value: '{scheduleData.Name}'
                                }
                            },
                            {

                                fieldLabel: 'Instance ID',
                                readOnly: true,
                                bind: {
                                    value: '{scheduleData.InstanceId}'
                                }
                            },
                            {

                                fieldLabel: 'Is remote',
                                readOnly: true,
                                bind: {
                                    value: '{scheduleData.IsRemote}'
                                }
                            },
                            {

                                fieldLabel: 'Scheduler type',
                                readOnly: true,
                                bind: {
                                    value: '{scheduleData.SchedulerTypeName}'
                                }
                            }
                        ]
                    }
                ]
            },
        {
            xtype: 'panel',
            autoScroll: true,
            region: 'center',
            layout: 'anchor',
            defaults: {
                margin: '0 0 10 0',
                border: true
            },
            items: [
                {
                    xtype: 'grid',
                    title: 'Jobs',
                    anchor: '100% 100%',
                    reference: 'defaultGroup',
                    flex: 1,
                    bind: {
                        selection: '{selectedRow}'
                    },
                    tbar: [
                        '->',
                        {
                            xtype: 'button',
                            text: 'Edit',
                            iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
                            handler: 'onEditClick',
                            bind: {
                                disabled: '{!selectedRow}'
                            }
                        },
                        {
                            xtype: 'button',
                            text: 'View',
                            iconCls: 'fa fa-eye',
                            handler: 'onViewClick',
                            bind: {
                                disabled: '{!selectedRow}'
                            }
                        }
                    ],
                    columns: [
                        {
                            xtype: 'rownumberer'
                        },
                        {
                            xtype: 'actioncolumn',
                            width: 50,
                            sortable: false,
                            menuDisabled: true,
                            align: 'center',
                            dataIndex: 'Triggers',
                            items: [{
                                getClass: function (v, metadata, r, rowIndex, colIndex, store) {
                                    var data = r.getData();

                                    return data.CanStart == false ? 'fa fa-circle action-column-blue-color' : 'fa fa-circle action-column-red-color';
                                }
                            }]
                        },
                        {
                            text: "Job",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'Name'
                        },
                        {
                            text: "Schedule",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            renderer: function (value) {
                                return value[0].TriggerType.CronExpression;
                            },
                            dataIndex: 'Triggers'
                        },
                        {
                            text: "Start Date",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'Triggers',
                            renderer: function (value) {
                                return Ext.util.Format.date(value[0].StartDate, 'Y-m-d H:i:s');
                            }
                        },
                        {
                            text: "End Date",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'Triggers',
                            renderer: function (value) {
                                return Ext.util.Format.date(value[0].EndDate, 'Y-m-d H:i:s');
                            }
                        },
                        {
                            text: "Previous Fire Date",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'Triggers',
                            renderer: function (value) {
                                return Ext.util.Format.date(value[0].PreviousFireDate, 'Y-m-d H:i:s');
                            }
                        },
                        {
                            text: "Next Fire Date",
                            sortable: true,
                            menuDisabled: true,
                            flex: 1,
                            dataIndex: 'Triggers',
                            renderer: function (value) {
                                return Ext.util.Format.date(value[0].NextFireDate, 'Y-m-d H:i:s');
                            }
                        },
                        {
                            width: 50,
                            xtype: 'widgetcolumn',
                            menuDisabled: true,
                            widget: {
                                xtype: 'button',
                                textAlign: 'left',
                                width: 28,
                                height: 29,
                                arrowCls: '',
                                iconCls: 'fa fa-chevron-right'
                            },
                            onWidgetAttach: function (column, widget, record) {
                                var data = record.getData();

                                var jsonData = {
                                    group: data.GroupName,
                                    job: data.Name,
                                    trigger: data.Triggers[0].Name,
                                    schedule: data.Triggers[0].TriggerType.CronExpression,
                                    startDate: data.Triggers[0].StartDate,
                                    endDate: data.Triggers[0].EndDate,
                                    previousFireDate: data.Triggers[0].PreviousFireDate,
                                    nextFireDate: data.Triggers[0].NextFireDate
                                };

                                var menus = [
                                    {
                                        text: 'Pause',
                                        iconCls: 'fa fa-pause',
                                        method: 'PauseTrigger',
                                        jsonData: jsonData,
                                        handler: 'onActionClick',
                                        disabled: data.Triggers[0].CanStart
                                    }, {
                                        text: 'Resume',
                                        iconCls: 'fa fa-play',
                                        method: 'ResumeTrigger',
                                        jsonData: jsonData,
                                        handler: 'onActionClick',
                                        disabled: data.Triggers[0].CanPause
                                    },
                                    '-',
                                    {
                                        text: 'Execute Now',
                                        method: 'ExecuteNow',
                                        jsonData: jsonData,
                                        handler: 'onActionClick',
                                        disabled: data.Triggers[0].CanStart
                                    }
                                ];
                                widget.setMenu(menus);
                            }
                        }
                    ]
                }
            ]
        }];
    }
});
