/**
*@author : Phanny
*/
Ext.define("Workflow.view.ticket.TicketForm",{
    extend: "Ext.panel.Panel",

    requires: [
        "Workflow.view.ticket.TicketFormController",
        "Workflow.view.ticket.TicketFormModel"
    ],

    controller: "ticket-ticketform",
    viewModel: {
        type: "ticket-ticketform"
    },
    scrollable : 'y',

    initComponent: function () {
        var me = this;
        var vm = me.getViewModel();

        me.items = [];
        me.items.push(me.requestorPanel());
        me.items.push(me.requestInfoPanel());

        if (vm.get('createdBy').toUpperCase() === 'AGENT' || vm.get('ticket') != null) {
            me.items.push(me.moreInfoPanel());
        }

        var isSubTicket = vm.get('data.refs') && vm.get('data.refs.mainTicket')

        if (vm.get('ticket') != null || isSubTicket) {
            me.items.push(me.commentPanel());
        }

        me.buttons = [
            {
                xtype: 'button', align: 'right',
                text: 'Submit',
                handler: 'onFormSubmit',
                iconCls: 'fa fa-play-circle-o'
            },
            { xtype: 'button', align: 'right', text: 'Close', handler: 'onWindowClosedHandler', iconCls: 'fa fa-times-circle-o' }
        ];

        me.callParent(arguments);
    },


    requestorPanel: function () {
        var me = this;        
        return {
                margin: 5,
                xtype: 'form-requestor',
                reference: 'requestor',
                allowAddRequestor : false,
                optaional : true,
                //mainView: me,
                itemSelectionCB: function (selectedItem) {                    
                    me.getViewModel().set('form.requestor', selectedItem);
                },
                priorityHidden: true,
                collapsible: true,
                border: true,
                //formReadonly: requestorFromBlockConfig.readOnly,
                //width: '100%',
                //listeners: {
                //    afterrender: function () {

                //    }
                //}
                bind: {
                    title: 'Requestor: {form.requestor.fullName}, ID: {form.requestor.employeeNo}, Dept: {form.requestor.deptName}'
                }
        };
    },

    requestInfoPanel: function () {
        return {
                margin: 5,
                xtype: 'form',
            //reference: 'requestInfo',
                title: 'Request Info',
                iconCls: 'fa fa-user',
                collapsible: true,
                //width: '100%',
                bodyPadding: 10,
                defaultType: 'textfield',
                border: true,
                items: [{
                fieldLabel: 'Subject <span style="color:red;">*</span>',
                bind: { value: '{form.subject}' },
                    width: 900
                }, {
                    fieldLabel: 'Description <span style="color:red;">*</span>',
                    xtype: 'htmleditor',
                    labelAlign: 'top',
                    bind: { value: '{form.description}' }//,
                    // border: '0 0 2 0',
                    // style: {
                    //     borderColor: 'blue',
                    //     borderStyle: 'solid'
                    // }
                }, {
                    xtype: 'grid',
                    dock: 'bottom',
                //reference: 'userUploadedFiles',
                    bind: {
                        store: '{userUploadFilesStore}'
                    },
                    hideHeaders: true,
                    columns: [
                       { text: 'UploadFile', dataIndex: 'fileName', flex: 1 },
                       {
                           menuDisabled: true,
                           sortable: false,
                           xtype: 'actioncolumn',
                           align: 'center',
                           //bind: {
                           //    hidden: '{!canAddRemove}'
                           //},
                           width: 30,
                           items: [{
                               iconCls: 'fa fa-trash-o',
                               tooltip: 'Remove',
                               handler: 'oneRemoveFileHandler'
                           }]
                       }]
                }],
                dockedItems: [{
                    xtype: 'toolbar',
                    dock: 'bottom',
                items: ['->', {
                    text: 'Upload file',
                        itemId: 'userUploadedFile',
                    xtype: 'button',
                    handler: 'onFileAddedHander',
                    iconCls: 'fa fa-upload'
                    }]
                }]
        };
            },


    moreInfoPanel: function () {
        return {
            margin: 5,
            xtype: 'form',
            layout: 'column',
            //reference: 'itInfo',
            title: 'IT Group Info (IT Use Only)',
            iconCls: 'fa fa-user-secret',
            collapsible: true,
            //width: '100%',
            border: true,
            bodyPadding: 10,
            bind: {
                hidden: '{!createdByAgent}'
            },
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
                        {
                            fieldLabel: 'Type <span style="color:red;">*</span>',
                            xtype: 'combo',
                            forceSelection: true,
                            queryMode: 'local',
                            displayField: 'typeName',
                            valueField: 'id',
                            bind: {
                                selection: '{form.ticketType}',
                                store: '{ticketTypeStore}'
                            },
                            listeners: {
                                select: 'onTicketTypeChange',
                                scope: 'controller'
                            },
                            reference: 'refTicketType',
                            listConfig: {
                                minWidth: 200,
                                //resizable: true,
                                //loadingText: 'Searching...',
                                //itemSelector: '.search-item',
                                itemTpl: [
                                    '<div><img src="{icon}"/><span style="margin-left: 10px!important;">{typeName}</span></div>'
                                ]
                            }
                        },
                        {
                            fieldLabel: 'Status <span style="color:red;">*</span>',
                            xtype: 'combo',
                            forceSelection: true,
                            queryMode: 'local',
                            displayField: 'status',
                            valueField: 'id',
                            bind: {
                                selection: '{form.status}',
                                store: '{ticketStatusStore}'//,
                                //readOnly: '{isEdit}'
                            }

                        }, {
                            fieldLabel: 'Source <span style="color:red;">*</span>',
                            xtype: 'combo',
                            forceSelection: true,
                            queryMode: 'local',
                            displayField: 'source',
                            // reference: 'ticketSourceRef',
                            valueField: 'id',
                            bind: {
                                selection: '{form.source}',
                                store: '{ticketSourceStore}'
                                // readOnly: '{readOnlyField}'
                            }
                        },
                        {
                            fieldLabel: 'Impact <span style="color:red;">*</span>',
                            xtype: 'combo',
                            forceSelection: true,
                            queryMode: 'local',
                            displayField: 'impactName',
                            //reference: 'ticketImpactRef',
                            valueField: 'id',
                            bind: {
                                selection: '{form.impact}',
                                store: '{ticketImpactStore}',
                                hidden: true
                                // readOnly: '{readOnlyField}'
                            },
                            listeners: {
                                //change: 'onGetPriorityHandler',
                                //scope: 'controller'
                            }
                            
                        },
                        {
                            fieldLabel: 'Urgency <span style="color:red;">*</span>',
                            xtype: 'combo',
                            forceSelection: true,
                            queryMode: 'local',
                            displayField: 'urgencyName',
                            //reference: 'ticketUrgencyRef',
                            valueField: 'id',
                            bind: {
                                selection: '{form.urgency}',
                                store: '{ticketUrgencyStore}',
                                hidden: true
                                // readOnly: '{readOnlyField}'
                            },
                            listeners: {
                                //change: 'onGetPriorityHandler',
                                //scope: 'controller'
                            }
                        },{
                            fieldLabel: 'Priority <span style="color:red;">*</span>',
                            xtype: 'combo',                            
                            //readOnly: true,
                            //reference: 'ticketPriorityRef',
                            displayField: 'priorityName',                            
                            valueField: 'id',
                            forceSelection: true,
                            queryMode: 'local',
                            bind: {
                                selection: '{form.priority}',
                                store: '{ticketPriorityStore}'
                                // readOnly: '{readOnlyField}'
                            },
                            listeners: {
                                select: 'onTicketPriorityChange',
                                scope: 'controller'
                            },
                            reference: 'refTicketPriority'
                        }, {
                            fieldLabel: 'SLA',
                            xtype: 'textfield',          
                            allowBlank: false,                                              
                            bind: {
                                value: '{form.sla.slaName}',
                                //store: '{ticketSlaStore}',
                                readOnly: true
                            }
                        }, {
                            fieldLabel: 'Estimated (Hours)',
                            xtype: 'numberfield',
                            bind: '{form.estimatedHours}'
                        }
                    ]
                },
                {
                    width: 450,
                    items: [
                        {
                            fieldLabel: 'Site <span style="color:red;">*</span>',
                            xtype: 'combo',
                            forceSelection: true,
                            queryMode: 'local',
                            displayField: 'siteName',
                            //reference: 'ticketSiteRef',
                            valueField: 'id',
                            bind: {
                                selection: '{form.site}',
                                store: '{ticketSiteStore}'
                                // readOnly: '{readOnlyField}'
                            }
                        }, {
                            fieldLabel: 'Group <span style="color:red;">*</span>',
                            xtype: 'combo',
                            forceSelection: true,
                            queryMode: 'local',
                            displayField: 'teamName',
                            //reference: 'ticketTeamRef',
                            valueField: 'id',
                            listeners: {
                                change: 'onTeamChanged',
                                scope: 'controller'

                            },
                            bind: {
                                selection: '{form.team}',
                                store: '{ticketTeamStore}'//,
                                //readOnly: '{isEdit}'
                            }
                        }, {
                            fieldLabel: 'Assignee',
                            xtype: 'combo',
                            //reference: 'ticketAgentRef',
                            bind: {
                                selection: '{form.assignee}',
                                store: '{ticketAgentStore}'//,
                               // readOnly: '{isEdit}'
                            },
                            displayField: 'display',
                            valueField: 'id',
                            queryMode: 'local',
                            forceSelection: true,
                            minChars: 2,
                            listConfig: {
                                minWidth: 500,
                                resizable: true,
                                loadingText: 'Searching...',
                                itemSelector: '.search-item',
                                itemTpl: [
                                    '<a >',
                                        '<h3><span>{display}</span>({display1})</h3>',
                                        '{display2}',
                                    '</a>'
                                ]
                            }

                        }, {
                            fieldLabel: 'Category <span style="color:red;">*</span>',
                            xtype: 'combo',
                            forceSelection: true,
                            queryMode: 'local',
                            displayField: 'cateName',
                            //reference: 'ticketCateRef',
                            valueField: 'id',
                            bind: {
                                selection: '{form.category}',
                                store: '{ticketCateStore}'
                                // readOnly: '{readOnlyField}'
                            },
                            listeners: {
                                change: 'onCateChanged',
                                scope: 'controller'
                            }
                        }, {
                            fieldLabel: 'Sub Category <span style="color:red;">*</span>',
                            xtype: 'combo',
                            displayField: 'display',
                            valueField: 'id',
                            queryMode: 'local',
                            //reference: 'ticketSubCateRef',
                            forceSelection: true,
                            listeners: {
                                change: 'onSubCateChanged',
                                scope: 'controller'
                            },
                            bind: {
                                selection: '{form.subCate}',
                                store: '{ticketSubCateStore}'
                            }

                        }, {
                            fieldLabel: 'Item <span style="color:red;">*</span>',
                            xtype: 'combo',
                            displayField: 'display',
                            valueField: 'id',
                            queryMode: 'local',
                            reference: 'ticketItemRef',
                            forceSelection: true,
                            bind: {
                                selection: '{form.item}',
                                store: '{ticketItemStore}'
                            }
                        },
                        {
                            fieldLabel: 'Due Date',
                            xtype: 'datefield',
                            bind: '{form.dueDate}',
                            emptyText : 'Leave blank to be set by SLA'

                        }
                    ]
                }
            ]
        };
    },

    commentPanel: function () {
        return {
            margin: 5,
            xtype: 'form',
            layout: 'column',
            //width: '100%',
            bodyPadding: '10 10 10',
            border: false, 
            items: [
                {
                    fieldLabel: 'Comment',
                    xtype: 'htmleditor',
                    allowBlank: false,
                    labelAlign: 'top',
                    width: '100%',
                    bind: {
                        value: '{form.comment}'
                        //hidden: '{!isEdit}'
                    }//,
                    // border: '0 0 2 0',
                    // style: {
                    //     borderColor: 'blue',
                    //     borderStyle: 'solid'
                    // }
                }
            ]

            
        };
    }

});
