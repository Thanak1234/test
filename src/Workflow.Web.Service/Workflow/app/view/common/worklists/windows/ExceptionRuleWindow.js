Ext.define('Workflow.view.common.worklist.ExceptionRuleWindow', {
    extend: 'Workflow.view.common.requestor.AbstractWindowDialog',
    controller: 'common-worklists-exceptionRuleWindowController',
    viewModel: {
        type: 'common-worklist-exceptionrulewindow'
    },
    
    title: 'Configure Exception Rule',
    closable: true,
    height: 600,
    width: 800,
    closeAction: 'hide',
    maximizable: false,
    layout: {
        type: 'border',
        padding: 0
    },
    initComponent: function () {
        var me = this;
        me.buildItems();
        me.callParent();
    },
    bindExceptionRuleData: function() {
        var me = this;        
    },
    buildItems: function () {
        var me = this;
        me.items = [
            {
                region: 'center',
                xtype: 'form',
                defaultType: 'textfield',
                fieldDefaults: {
                    labelWidth: 120
                },
                layout: {
                    type: 'vbox',
                    align: 'stretch'
                },
                bodyPadding: 5,
                border: true,
                items: [
                    {
                        fieldLabel: 'Rule Name:',
                        name: 'ruleName',
                        bind: '{exceptionTemp.name}'
                    },
                    {
                        xtype: 'combo',
                        fieldLabel: 'Process',
                        reference: 'cmbProcess',
                        displayField: 'RequestDesc',
                        valueField: 'ProcessPath',
                        publishes: 'value',
                        forceSelection: true,
                        bind: {
                            value: '{exceptionTemp.processPath}',
                            store: '{processStoreId}'
                        }
                    }, {
                        xtype: 'combo',
                        reference: 'cmbActivity',
                        fieldLabel: 'Activity',
                        displayField: 'DisplayName',
                        valueField: 'Name',
                        queryMode: 'remote',
                        forceSelection: true,
                        bind: {
                            disabled: '{!cmbProcess.value}',
                            value: '{exceptionTemp.activity}',
                            filters: {
                                property: 'ProcessPath',
                                value: '{cmbProcess.value}'
                            },
                            store: '{activityStoreId}'
                        }
                    },
                    {
                        xtype: 'common.worklist.components.user-grid',
                        title: 'Users',
                        reference: 'destinationGridPanel',
                        bind: {
                            selection: '{selectedDestination}',
                            store: '{destinations}'
                        },
                        height: 350,
                        frame: true
                    }
                ]
            }
        ];
    },
    buttons: [
        {
            text: 'OK',
            handler: 'onExceptionOkClickHandler'
        },
        {
            text: 'Cancel',
            handler: 'closeWindow'
        }
    ]
});