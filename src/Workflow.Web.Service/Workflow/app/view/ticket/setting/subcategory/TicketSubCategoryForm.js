Ext.define("Workflow.view.ticket.setting.subcategory.TicketSubCategoryForm",{
    extend: "Ext.panel.Panel",
    requires: [
        "Workflow.view.ticket.setting.subcategory.TicketSubCategoryFormController",
        "Workflow.view.ticket.setting.subcategory.TicketSubCategoryFormModel"
    ],
    controller: "ticket-setting-subcategory-ticketsubcategoryform",
    viewModel: {
        type: "ticket-setting-subcategory-ticketsubcategoryform"
    },
    scrollable : 'y',
    frame: true,
    layout: {
        type: 'anchor',
        pack: 'start',
        align: 'stretch'
    },
    
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
        return [{
            xtype: 'form',
            reference: 'formRef',
            anchor: '100%',
            border: true,
            bodyPadding: '10 10 0',
            margin: 5,
            items: [{
                xtype: 'textfield',
                anchor: '100%',
                allowBlank: false,
                fieldLabel: 'Name',
                bind: {
                    value: '{form.subCateName}',
                    readOnly: '{isEdit}'
                }
            }, {
                xtype: 'textarea',
                anchor: '100%',
                fieldLabel: 'Description',
                bind: { value: '{form.description}' }
            }, {
                fieldLabel: 'Category',
                allowBlank: false,
                anchor: '100%',
                xtype: 'combo',
                forceSelection: true,
                queryMode: 'local',
                displayField: 'cateName',
                valueField: 'id',
                listeners: {
                    scope: 'controller'
                },
                bind: {
                    selection: '{form.cate}',
                    store: '{ticketCateStore}',
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
            }]
        }];
    }

});
