Ext.define("Workflow.view.ticket.setting.agent.TicketAgentForm",{
    extend: "Ext.panel.Panel",
    requires: [
        "Workflow.view.ticket.setting.agent.TicketAgentFormController",
        "Workflow.view.ticket.setting.agent.TicketAgentFormModel"
    ],
    controller: "ticket-setting-agent-ticketagentform",
    viewModel: {
        type: "ticket-setting-agent-ticketagentform"
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

        me.employeePickup = {
            fieldLabel: 'Employee',
            xtype: 'employeePickup',
            reference: 'employeePickup',
            integrated: true,
            excludeOwner: false,
            allowBlank: false,
            bind: {
                selection: '{form.employee}',
                readOnly: '{readOnly}'
            }
        };

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
            border: false,            
            width: '100%', 
            margin: 5,
            items: [
            
               {
                    xtype: 'fieldset',
                    title: 'Employee Info',
                    columnWidth: 0.5,
                    defaultType: 'textfield',
                    defaults: {anchor: '100%'},
                    items: [
                        me.employeePickup,
                        { fieldLabel: 'Employee No', bind: '{form.employee.employeeNo}', readOnly: true },
                        { fieldLabel: 'Position', bind: '{form.employee.position}', readOnly: true },
                        { fieldLabel: 'Sub Department', bind: '{form.employee.subDept}', readOnly: true },
                        { fieldLabel: 'Group', bind: '{form.employee.groupName}', readOnly: true },
                        { fieldLabel: 'Devision', bind: '{form.employee.devision}', readOnly: true }
                    ]
                },{
                    xtype: 'fieldset',
                    title: 'Agent Info',
                    columnWidth: 0.5,
                    defaults: {anchor: '100%'},
                    items: [{
                        fieldLabel: 'Account Type',
                        allowBlank: false,                        
                        hidden: true,
                        xtype: 'combo',
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'display',                
                        valueField: 'id',
                        listeners: {                    
                            scope: 'controller'                    
                        },
                        bind: {
                            selection: '{form.accountType}',
                            store: '{accountTypeStore}',
                            readOnly: '{isEdit}'
                        }
                    },{
                        fieldLabel: 'Status',
                        allowBlank: false,
                        
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
                    }, {
                        fieldLabel: 'Group Policy',
                        allowBlank: false,
                        
                        xtype: 'combo',
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'display',                
                        valueField: 'id',
                        listeners: {                    
                            scope: 'controller'                    
                        },
                        bind: {
                            selection: '{form.groupPolicy}',
                            store: '{groupPolicyStore}',
                            readOnly: '{isEdit}'
                        }
                    }, {
                        fieldLabel: 'Department',
                        allowBlank: false,
                        
                        xtype: 'combo',
                        forceSelection: true,
                        queryMode: 'local',
                        displayField: 'deptName',                
                        valueField: 'id',
                        listeners: {                    
                            scope: 'controller'                    
                        },
                        bind: {
                            selection: '{form.dept}',
                            store: '{departmentStore}',
                            readOnly: '{isEdit}'
                        }
                    
                    },{
                        xtype: 'textarea',
                        
                        fieldLabel: 'Description',
                        bind: { value: '{form.description}' }
                    }]
        
      }]
     }]     
    }
});
