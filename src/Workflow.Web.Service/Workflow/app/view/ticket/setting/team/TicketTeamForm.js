Ext.define("Workflow.view.ticket.setting.team.TicketTeamForm",{
    extend: "Ext.panel.Panel",
    requires: [
        "Workflow.view.ticket.setting.team.TicketTeamFormController",
        "Workflow.view.ticket.setting.team.TicketTeamFormModel"
    ],
    controller: "ticket-setting-team-ticketteamform",
    viewModel: {
        type: "ticket-setting-team-ticketteamform"
    },
    scrollable : 'y',
    frame: true,
    layout: {
        type: 'anchor',
        pack: 'start',
        align: 'stretch'
    },
    resizable: false,
    modal: true,    
    initComponent: function () {
        var me = this;        
        me.buttons = [
            {
                xtype: 'button', align: 'right',
                text: 'Save',
                handler: 'onFormSubmit',
                iconCls: 'fa fa-save'
            },
            { xtype: 'button', align: 'right', text: 'Close', handler: 'onWindowClosedHandler', iconCls: 'fa fa-times-circle-o'}
        ];

        me.items = me.buildItems();

        me.callParent(arguments);
    },
    buildItems: function () {
        var me = this;
        return [{
            xtype: 'form',
            reference: 'formRef',
            anchor: '100%',            
            border: true,
            bodyPadding: '10 10 0',
            margin: 5,
            defaultType: 'checkboxfield',
            items: [{
                xtype: 'textfield',                
                anchor: '100%',
                allowBlank: false,
                fieldLabel: 'Name',
                bind: { 
                    value: '{form.teamName}',
                    readOnly: '{isEdit}' 
                }
            },{
                fieldLabel: 'Status',
                allowBlank: false,
                anchor: '100%',
                xtype: 'combo',
                forceSelection: true,
                queryMode: 'local',
                displayField: 'display',                
                valueField: 'id',
                listeners: {                    
                    scope: 'controller'                    
                },
                bind: {
                    selection: '{form.status}',
                    store: '{statusStore}',
                    readOnly: '{isEdit}'
                }
            },{      
                boxLabel  : 'Alert All Members',
                name      : 'alertAllMembers',
                inputValue: '1',
                hidden: true,                
                bind: { 
                    value: '{form.alertAllMembers}',
                    readOnly: '{isEdit}' 
                }
            },{      
                boxLabel  : 'Alert Assigned Agent',
                name      : 'alertAssignedAgent',
                inputValue: '1',
                hidden: true,                
                bind: { 
                    value: '{form.alertAssignedAgent}',
                    readOnly: '{isEdit}' 
                }
            },{      
                boxLabel  : 'Directory Listing',
                name      : 'directoryListing',
                inputValue: '1',
                margin: '0 0 0 105',                
                bind: { 
                    value: '{form.directoryListing}',
                    readOnly: '{isEdit}' 
                }
            },{
                xtype: 'textarea',
                anchor: '100%',
                fieldLabel: 'Description',
                bind: { value: '{form.description}' }
            }]
        },{
                xtype:'grid',                
                border: true,
                region: 'center',
                hideHeaders: false,                
                title: 'Agent List',
                minHeight : 180,
                scrollable: 'y',
                margin: '10 5 0 5',
                bind:{
                    store: '{ticketSettingTeamAgentsStore}'
                },
                listeners: {        
                    select: 'onRowSelectedHanler'
                },
                iconCls: 'fa fa-street-view',
                dockedItems: [{
                    xtype: 'toolbar',
                    dock: 'top',
                    items: [
                    '->',
                    {
                        xtype: 'button',
                        handler: 'onAssignImmediateHandler',
                        bind:{
                            text: '{btnAssignImmediateName}',
                            hidden: '{!isAgentSelected}'
                        }
                    }
                    ,
                    // {
                    //     text: 'Remove Immediate Assign',
                    //     xtype: 'button',
                    //     handler: 'onRemoveImmediateHandler',
                    //     bind:{
                    //         hidden: '{!showBtnRemoveAssignImmediate}'
                    //     }
                    // },
                    {
                        text: 'Add',
                        xtype: 'button',
                        handler: 'onRegisterAgentHandler',
                        iconCls: 'fa fa-plus-circle'
                    }]
                }],                           
                columns: [
                    /*{   xtype: 'checkcolumn',
                        dataIndex: 'teamId',
                        text: 'Registered'
                    },*/
                    { xtype: 'rownumberer' },
                   { 
                        text: 'Emp No', dataIndex: 'employeeNo', flex: 1,
                        renderer : function(value, metadata, record) {
                            return me.showToolTip(value, metadata);
                        }        
                    },
                    { 
                        text: 'Emp Full Name', dataIndex: 'fullName', flex: 1,
                        renderer : function(value, metadata, record) {
                            return me.showToolTip(value, metadata);
                        }        
                    },
                    { text: 'Account Type', dataIndex: 'accountType', flex: 1 ,hidden: true,
                        renderer : function(value, metadata, record) {
                        return me.showToolTip(value, metadata);
                        } 
                    },
                    {
                        text: 'Group Policy', dataIndex: 'groupPolicyGroupName', flex: 1,
                        renderer: function (value, metadata, record) {
                            return me.showToolTip(value, metadata);
                        } 
                    },            
                    { text: 'Department', dataIndex: 'deptName', flex: 1 ,
                        renderer : function(value, metadata, record) {
                        return me.showToolTip(value, metadata);
                        } 
                    },
                    { text: 'Description', dataIndex: 'description', flex: 2 ,
                        renderer : function(value, metadata, record) {
                            return me.showToolTip(value, metadata);
                        } 
                    },{
                        xtype: 'booleancolumn',
                        text: 'Immediate Assign',
                        width: '100',
                        trueText: 'Yes',
                        falseText: 'No',
                        dataIndex: 'immediateAssign'
                    },{
                        xtype: 'actioncolumn',
                        menuDisabled    : true,
                        //cls: 'tasks-icon-column-header tasks-delete-column-header',
                        width: 35,                
                        iconCls: 'fa fa-trash-o',
                        tooltip: 'Remove',                
                        sortable: false,
                        handler: 'onDeleteHandler'
                    }

                    /*,{
                      text: 'Immediate Assign',                                            
                      dataIndex: 'immediateAssign',
                      renderer: function(value, meta, record){
                          return '<center><input type="radio" name="radio" ' 
                          + (value ? 'checked' : '') 
                          + ' onclick="var s = Ext.getCmp(\'button-grid\').store; s.getAt(s.findExact(\'id\',\'' 
                          + record.get('id') 
                          + '\')).set(\'immediateAssign\', this.value)"'
                          + ' /></center>';
                      }
                    }*/
                    
                   ]
            }];
    },    
    showToolTip: function(value, metadata){
        value ? metadata.tdAttr = 'data-qtip="' + value + '"' : '';
        return value;
    }


});
