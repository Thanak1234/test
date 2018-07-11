Ext.define("Workflow.view.ticket.report.DeptsWin", {
    extend: 'Ext.window.Window',
    controller: 'ticket-report-depts',
    viewModel: {
        type: 'ticket-report-depts'
    },
    layout: 'border',
    //title: 'Data Picker',
    header: {
        hidden: true
    },
    modal: true,
    width: 400,
    height: 500,
    layout: 'fit',    
    buttonAlign: 'left',
    closeAction: 'hide',
    constrain: true,
    closable: false,
    initComponent: function () {
        var me = this;
        me.buildItems();
        me.buildButtons();
        me.callParent(arguments);
    },
    buildItems: function () {
        var me = this;
        me.items = [
            {
                xtype: 'multiselector',
                reference: 'selector',
                title: 'Departments',
                fieldName: 'fullName',
                store: me.deptStore,
                viewConfig: {
                    deferEmptyText: false,
                    emptyText: 'No departments selected'
                },
                search: {
                    field: 'fullName',
                    width: 400,
                    height: 500,
                    store: {
                        model: 'Workflow.model.common.Department',
                        autoLoad: true,
                        proxy: {
                            type: 'rest',
                            url: Workflow.global.Config.baseUrl + 'api/employee/departments',
                            reader: {
                                type: 'json',
                                rootProperty: 'data',
                                totalProperty: 'totalCount'
                            }
                        }
                    }
                }
            }];
    },
    buildButtons: function () {
        var me = this;
        me.buttons = [
            {
                text: 'Clear',
                iconCls: 'fa fa-eraser',
                handler: 'onClearClick'
            },
            '->',
            {
                text: 'Save',
                iconCls: 'fa fa-floppy-o',
                handler: 'onSaveClick'
            }
        ];
    }
});