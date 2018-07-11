/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.view.TicketDisplay',{
    extend: 'Ext.panel.Panel',
    scrollable: true,

    requires: [
        'Workflow.view.ticket.view.TicketDisplayController',
        'Workflow.view.ticket.view.TicketDisplayModel'
    ],

    controller: 'ticket-view-ticketdisplay',
    viewModel: {
        type: 'ticket-view-ticketdisplay'
    },

    //width : "100%",
    initComponent: function () {
        var me = this;
        var vm = this.getViewModel();
        var empNo = vm.get('ticket.empNo');

        me.items = [];

        if(empNo){
            me.items.push(me.renderRequestor());
        }

        me.items.push(me.renderTicketInfo());
        me.items.push(me.getActivityTypes());
        me.items.push({ xtype: 'ticket-activity-listing', reference: 'activityList' });
        me.buttons = me.getButtons();
        me.callParent(arguments);
    },

    renderRequestor: function () {
        var form = {
            margin: 5,
            bodyPadding: '10 10 0',
            border: true,
            xtype: 'form',
            //reference: 'requestInfo',
            bind: {
                title: 'Requestor: {ticket.empName}, ID: {ticket.empNo}, Dept: {ticket.subDept} '
            },
            collapsible: true,
            iconCls: 'fa fa-user',
            collapsed: true,
            title: 'Requestor',
            formReadonly: false,
            minHeight: 200,
            layout: 'column',
            autoWidth: true,
            defaults: {
                layout: 'form',
                xtype: 'container',
                defaultType: 'textfield',
                flex: 1
            },
            items : [
            {
                width: 450,
                items: [
                    { fieldLabel: 'Employee Name', bind: '{ticket.empName}', readOnly: true },
                    { fieldLabel: 'Employee No', bind: '{ticket.empNo}', readOnly: true },
                    { fieldLabel: 'Department', bind: '{ticket.groupName}', readOnly: true },
                    { fieldLabel: 'Team', bind: '{ticket.subDept}', readOnly: true },
                ]
            }, {
                width: 450,
                items: [
                    { fieldLabel: 'Position', bind: '{ticket.position}', readOnly: true },
                    { fieldLabel: 'Phone', bind: '{ticket.phone}', readOnly: true },
                    { fieldLabel: 'Phone (Ext)', bind: '{ticket.ext}', readOnly: true },
                    { fieldLabel: 'Email', bind: '{ticket.email}', readOnly: true }
                ]
            }]

        };
        return form;
    },

    renderTicketInfo: function () {
        var form = {
            border: true,
            width: '100%',
            xtype: 'form',
            margin: 5,
            bodyPadding: '10 10 0',
            //reference: 'requestInfo',
            minHeight: 200,
            layout: 'column',
            autoWidth: true,
            defaults: {
                layout: 'form',
                xtype: 'container',
                defaultType: 'textfield',
                flex: 1
            },
            items: [
            {
                width: 450,
                items: [
                    { fieldLabel: 'Type', bind: '{ticket.type}', readOnly: true },
                    { fieldLabel: 'Status', bind: '{ticket.status}', readOnly: true },
                    { fieldLabel: 'Source', bind: '{ticket.source}', readOnly: true },
                    { fieldLabel: 'Impact', bind:{value:'{ticket.impact}', hidden: true }, readOnly: true},
                    { fieldLabel: 'Urgency', bind: { value: '{ticket.urgency}', hidden: true}, readOnly: true},
                    { fieldLabel: 'Priority', bind: '{ticket.priority}', readOnly: true },
                    { fieldLabel: 'SLA', bind: '{ticket.sla}', readOnly: true }
                ]
            }, {
                width: 450,
                items: [
                    { fieldLabel: 'Site', bind: '{ticket.site}', readOnly: true },
                    { fieldLabel: 'Group', bind: '{ticket.team}', readOnly: true },
                    { fieldLabel: 'Assignee', bind: '{ticket.assignee}', readOnly: true },
                    { fieldLabel: 'Category', bind: '{ticket.category}', readOnly: true },
                    { fieldLabel: 'Sub Category', bind: '{ticket.subCate}', readOnly: true },
                    { fieldLabel: 'Item', bind: '{ticket.item}', readOnly: true }
                ]
            }]

        };


        var panel = {
            xtype: 'panel',
            margin: 5,
            layout: {
                type: 'vbox',       // Arrange child items vertically
                align: 'stretch'    // Each takes up full width
            },
            bodyPadding: '10 10 0',
            border: true,
            bind: {
                title: 'Status: {ticket.status}, Ticket Item : {ticket.category} / {ticket.subCate} / {ticket.item} '
            },
            collapsible: true,
            collapsed: true,
            iconCls: 'fa fa-info-circle',
            formReadonly: true,
            minHeight: 200,
            items: [
                {
                    margin: 5,
                    xtype: 'form',
                    //reference: 'requestInfo',
                    //width: '100%',
                    //bodyPadding: '10 10 0',
                    defaultType: 'textfield',
                    border: false,
                    items: [{
                        fieldLabel: 'To ',
                        bind: { value: '{ticket.emailItem.to}', hidden: '{hidden}' },
                        width: 900,
                        maxLength: 99999,
                        readOnly: true
                    }, {
                        fieldLabel: 'CC ',
                        bind: { value: '{ticket.emailItem.cc}', hidden: '{hidden}' },
                        width: 900,
                        maxLength: 99999,
                        readOnly: true
                    },  {
                        fieldLabel: 'Subject ',
                        bind: { value: '{ticket.subject}' },
                        width: 900,
                        readOnly: true
                    }, {
                        fieldLabel: 'Description <span style="color:red;">*</span>',
                        xtype: 'htmleditor',
                        layout: 'vbox',
                        height : 400,
                        labelAlign: 'top',
                        bind: { value: '{ticket.description}' },
                        readOnly : true,
                        border: '0 0 1 0',
                        style: {
                            //borderColor: 'blue',
                            borderStyle: 'solid'
                        }
                    }]
                }, {

                    border: false,
                    width: '100%',
                    xtype: 'form',
                    margin: 5,
                    //reference: 'requestInfo',

                    minHeight: 200,
                    layout: 'column',
                    autoWidth: true,
                    defaults: {
                        layout: 'form',
                        xtype: 'container',
                        defaultType: 'textfield',
                        flex: 1
                    },
                    items: [
                    {
                        width: 450,
                        items: [
                            { fieldLabel: 'Type', bind: '{ticket.type}', readOnly: true },
                            { fieldLabel: 'Status', bind: '{ticket.status}', readOnly: true },
                            { fieldLabel: 'Source', bind: '{ticket.source}', readOnly: true },
                            { fieldLabel: 'Impact', bind: { value: '{ticket.impact}', hidden: true }, readOnly: true },
                            { fieldLabel: 'Urgency', bind: { value: '{ticket.urgency}', hidden: true }, readOnly: true },
                            { fieldLabel: 'Priority', bind: '{ticket.priority}', readOnly: true },
                            { fieldLabel: 'SLA', bind: '{ticket.sla}', readOnly: true },
                            { fieldLabel: 'Created Date', bind: '{createdDate}', readOnly: true },
                            { fieldLabel: 'Estimated Hours', bind: '{ticket.estimatedHours}', readOnly: true },
                            { fieldLabel: 'Actual Hours', bind: '{actualTime}', readOnly: true }
                        ]
                    }, {
                        width: 450,
                        items: [
                            { fieldLabel: 'Site', bind: '{ticket.site}', readOnly: true },
                            { fieldLabel: 'Group', bind: '{ticket.team}', readOnly: true },
                            { fieldLabel: 'Assignee', bind: '{ticket.assignee}', readOnly: true },
                            { fieldLabel: 'Category', bind: '{ticket.category}', readOnly: true },
                            { fieldLabel: 'Sub Category', bind: '{ticket.subCate}', readOnly: true },
                            { fieldLabel: 'Item', bind: '{ticket.item}', readOnly: true },
                            { fieldLabel: 'First Response Date', bind: '{firstResponseDate}', readOnly: true },
                            { fieldLabel: 'Due Date', bind: '{dueDate}', readOnly: true },
                            { fieldLabel: 'Finished Date', bind: '{finishedDate}', readOnly: true }
                        ]
                    }]
                }
            ]

        };

        return panel;
    },

    getActivityTypes: function(){
        return {
            xtype: 'panel',
            margin: 5,
            bodyPadding: '10 10 0',
            items :[{
                fieldLabel: 'Activity Filter',
                xtype: 'combo',
                queryMode: 'local',
                displayField: 'display',
                forceSelection: true,
                emptyText:'Any',
                //reference: 'ticketStatusRef', onActivityFilter
                valueField: 'id',
                bind: {
                    selection: '{acitvityType}',
                    store: '{activityTypeStore}'
                },
                listeners: {
                    change: 'onActivityFilter',
                    scope: 'controller'
                }
            }]
        };
    },

    getButtons: function () {
       return [
                {
                    reference :'actionBt',
                    iconCls : 'fa fa-tasks',
                    text :'Actions',
                    menu: []
                }
            ];
    }
});
