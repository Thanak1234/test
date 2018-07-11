Ext.define("Workflow.view.ticket.setting.team.RegisteredAgentWindow", {
    extend: "Ext.window.Window",
    xtype: 'ticket-setting-team-registeredagentwindow',
    controller: "ticket-setting-team-registeredagentwindowcontroller",
    viewModel: {
        type: "ticket-setting-team-registeredagentwindowmodel"
    },
    title: 'Agent',
    formReadonly: false,
    frame: true,
    scrollable : 'y',
    width: 600,
    height: 600,
    // minHeight: 200,
    layout: {
        type: 'anchor',
        pack: 'start',
        align: 'stretch'
    },
    // autoWidth: true,
    bodyPadding: 10,
    resizable: false,
    modal: true,
    iconCls: 'fa fa-street-view',
    defaults: {
        defaultType: 'textfield',
        flex: 1
    },
    initComponent: function () {
        var me = this;

        me.buildItems();
        me.buildButtons();

        me.callParent(arguments);
    },
    getAgentPickup: function(){
        return {
            fieldLabel: 'Search Agent',
            allowBlank: false,
            xtype: 'combo',
            anchor: '100%',
            typeAhead: true,
            displayField: 'fullName',
            typeAheadDelay: 100,
            minChars: 2,
            valueField: 'empId',
            queryMode: 'remote',
            forceSelection: true,
            emptyText: 'agent information',
            bind: {
                store: '{agentStore}',
                selection: '{form.agent}',
                readOnly: '{isEdit}'
            },
            listConfig: {
                minWidth: 500,
                resizable: true,
                loadingText: 'Searching...',
                emptyText: 'No matching posts found.',
                itemSelector: '.search-item',
                itemTpl: [
                     '<a class="tpl-list-employee">',
                        '<h3><span>{fullName}</span> ({employeeNo})</h3>',
                        '<span>{email}</span>',
                    '</a>'
                ]
            },
            triggers: {
                clear: {
                    weight: 1,
                    cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                    hidden: true,
                    //handler: 'onClearClick',
                    scope: 'this'
                }
            },
            listeners: {
                //change: 'onEmplyeePickupChanged',
                //expand: 'onEmplyeePickupChanged'//'onEmplyeeExpandCombobox'
                beforequery: 'onAgentPickupChanged'
                //afterrender: function (combo) {
                //    //var store = combo.getStore();
                //    //combo.getTrigger('clear').hide();
                //    //combo.updateLayout();
                //}
            }
        };
    },
    buildItems: function() {
        var me = this;
        me.items = [{
        xtype: 'form',
        anchor: '100%',
        reference: 'agentPickupFormRef',
        items: [
            me.getAgentPickup(),
            {
                xtype: 'checkboxfield',
                boxLabel  : 'Immediate Assign',
                bind: '{form.immediateAssign}',
                margin: '0 0 0 105'
            },
            {
            xtype: 'fieldset',
            title: 'Agent Info',
            columnWidth: 0.5,
                defaults: {anchor: '100%'},
                defaultType: 'textfield',
                items: [{
                    fieldLabel: 'Status',
                    readOnly: true,
                    bind: { value: '{form.agent.status}' }
                }, {
                    fieldLabel: 'Group Policy',
                    readOnly: true,
                    bind: { value: '{form.agent.groupPolicyGroupName}' }
                }, {
                    fieldLabel: 'Department',
                    readOnly: true,
                    bind: { value: '{form.agent.department.fullName}' }
                },
                {
                    xtype: 'textarea',
                    readOnly: true,
                    fieldLabel: 'Description',
                    bind: { value: '{form.agent.description}' }
                }]
            },{
                xtype: 'fieldset',
                title: 'Employee Info',
                columnWidth: 0.5,
                defaultType: 'textfield',
                defaults: {anchor: '100%'},
                items: [
                    me.employeePickup,
                    { fieldLabel: 'Employee No', bind: '{form.agent.employeeNo}', readOnly: true },
                    { fieldLabel: 'Position', bind: '{form.agent.position}', readOnly: true },
                    { fieldLabel: 'Sub Department', bind: '{form.agent.subDept}', readOnly: true },
                    { fieldLabel: 'Group', bind: '{form.agent.groupName}', readOnly: true },
                    { fieldLabel: 'Devision', bind: '{form.agent.devision}', readOnly: true }
                ]
            }]
        }];
    },
    buildButtons: function () {
        var me = this;

        me.buttons = [
            {
                text:'Add',
                handler: 'onAddAgentToTeamHanler',
                iconCls: 'fa fa-plus-circle'
            },
            {
                text: 'Close',
                handler: 'onCloseClick', iconCls: 'fa fa-times-circle-o'
            }
        ];
    }
});
