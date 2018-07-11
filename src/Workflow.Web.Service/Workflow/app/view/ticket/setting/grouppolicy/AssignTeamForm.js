Ext.define("Workflow.view.ticket.setting.grouppolicy.AssignTeamForm",{
    extend: "Ext.window.Window",
    requires: [
        "Workflow.view.ticket.setting.grouppolicy.AssignTeamFormController",
        "Workflow.view.ticket.setting.grouppolicy.AssignTeamFormModel"
    ],
    controller: "ticket-setting-grouppolicy-assignteamform",
    viewModel: {
        type: "ticket-setting-grouppolicy-assignteamform"
    },
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
    defaults: {
        defaultType: 'textfield',
        flex: 1
    },
    title: 'Teams Assign Form',
    initComponent: function () {
        var me = this;
        me.buttons = [
            {
                xtype: 'button', align: 'right',
                text: 'Save',
                handler: 'onFormSubmit',
                iconCls: 'fa fa-save'
            },
            {
                xtype: 'button',
                align: 'right',
                text: 'Close',
                handler: 'onCloseClick', iconCls: 'fa fa-times-circle-o'
            }
        ];

        me.items = me.buildItems();

        me.callParent(arguments);
    },
    getTeamPickup: function(){
        return {
            fieldLabel: 'Search Team',
            allowBlank: false,
            xtype: 'combo',
            anchor: '100%',
            typeAhead: true,
            displayField: 'teamName',
            typeAheadDelay: 100,
            minChars: 2,
            valueField: 'id',
            queryMode: 'remote',
            forceSelection: true,
            emptyText: 'team information',
            bind: {
                store: '{ticketSettingTeamsStore}',
                selection: '{form.team}',
                readOnly: '{!isEdit}'
            },
            listConfig: {
                minWidth: 500,
                resizable: true,
                loadingText: 'Searching...',
                emptyText: 'No matching posts found.',
                itemSelector: '.search-item',
                itemTpl: [
                     '<a class="tpl-list-employee">',
                        '<h3><span>{teamName}</span></h3>',
                        // '<span>{description}</span>',
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
                beforequery: 'onTeamPickupChanged'
                //afterrender: function (combo) {
                //    //var store = combo.getStore();
                //    //combo.getTrigger('clear').hide();
                //    //combo.updateLayout();
                //}
            }
        };
    },
    buildItems: function () {
        var me = this;
        return [{
            xtype: 'form',
            reference: 'teamPickupFormRef',
            anchor: '100%',
            border: true,
            bodyPadding: '10 10 0',
            margin: 5,
            defaultType: 'textfield',
            items: [
                me.getTeamPickup(),
                {
                xtype: 'textfield',
                anchor: '100%',
                fieldLabel: 'Name',
                bind: {
                    value: '{form.team.teamName}',
                    readOnly: '{isEdit}'
                }
            },{
                xtype: 'textarea',
                anchor: '100%',
                fieldLabel: 'Description',
                bind: { value: '{form.team.description}', readOnly: '{isEdit}' }
            },{
                xtype: 'button',
                margin: '5 0 10 0',
                text: 'Add to Team List',
                handler : 'onAddTeamHanler'
            }]
        },{
                xtype:'grid',
                border: true,
                region: 'center',
                hideHeaders: false,
                title: 'Team List',
                minHeight : 180,
                scrollable: 'y',
                margin: '10 5 0 5',
                store : me.assignedTeamStore,
                // bind:{
                //     store: '{gridAssignedTeamStore}'
                // },
                listeners: {
                    // select: 'onRowSelectedHanler'
                },
                columns: [
                    {
                        text: 'Team', dataIndex: 'teamName', flex: 1,
                        renderer : function(value, metadata, record) {
                            return me.showToolTip(value, metadata);
                        }
                    },
                    { text: 'Description', dataIndex: 'description', flex: 1,
                        renderer : function(value, metadata, record) {
                            return me.showToolTip(value, metadata);
                        }
                    },{
                        xtype: 'booleancolumn',
                        text: 'Status',
                        width: '100',
                        trueText: 'Active',
                        falseText: 'Inactive',
                        dataIndex: 'status'
                    },
                    {
                        text: 'Created Date', dataIndex: 'createdDate',
                        renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s')
                    },
                    {
                        text: 'Modified Date', dataIndex: 'modifiedDate',
                        renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s')
                    },{
                    xtype: 'actioncolumn',
                    menuDisabled    : true,
                    //cls: 'tasks-icon-column-header tasks-delete-column-header',
                    width: 35,
                    iconCls: 'fa fa-trash-o',
                    tooltip: 'Remove',
                    sortable: false,
                    handler: 'onDeleteTeamHandler'
                }


                   ]
            }];
    },
    showToolTip: function(value, metadata){
        value ? metadata.tdAttr = 'data-qtip="' + value + '"' : '';
        return value;
    }


});
