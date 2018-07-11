Ext.define("Workflow.view.roles.UserRole", {
    extend: "Ext.grid.Panel",
    xtype: 'roles-userrole',
    viewModel: {
        type: "roles-userrole"
    },
    title: 'Users',
    selModel: {
        selType: 'checkboxmodel'
    },
    viewConfig: {
        loadMask: true,
        listeners: {
            // refresh: function (dataview) {
            //     Ext.each(dataview.panel.columns, function (column) {
            //         column.autoSize();                        
            //     })
            // },
            rowdblclick: 'onUserRowDoubleClick',
            selectionchange: 'onUserSelectionChange'
        }
    },
    initComponent: function () {
        var me = this;

        me.buildItems();
        me.buildColumns();
        me.buildToolbars();

        me.callParent(arguments);
    },
    buildItems: function () {

    },
    buildColumns: function () {
        var me = this;

        function colorRender(value) {
            var nVal = me.searchRegex ? value.toString().replace(me.searchRegex, '<span style="color:red;font-weight:bold; font-size:110%">$1</span>') : value;
            return nVal;
        }

        me.columns = [
            {
                xtype: 'rownumberer'
            },
            {
                text: 'EMP NO',
                width: 90,
                sortable: true,
                dataIndex: 'employeeNo',
                renderer: colorRender
            },
            {
                text: 'NAME',
                flex: 1,
                sortable: true,
                dataIndex: 'fullName',
                renderer: colorRender
            },
            {
                header: 'ACTIVE DIRECTORY',
                flex: 1,
                sortable: true,
                dataIndex: 'loginName',
                renderer: colorRender
            },
            {
                header: 'INCLUDE',
                width: 90,
                sortable: true,
                dataIndex: 'include',
                renderer: function (value) {
                    return value == true ? 'Yes' : 'No';
                }
            },
            {
                text: 'POSITION',
                flex: 1,
                sortable: true,
                dataIndex: 'position',
                renderer: colorRender
            },
            {
                text: 'DEPARTMENT',
                flex: 1,
                sortable: true,
                dataIndex: 'subDept',
                renderer: colorRender
            },
            {
                text: 'GROUP',
                flex: 1,
                sortable: true,
                dataIndex: 'groupName',
                renderer: colorRender
            }
        ];

        
    },
    buildToolbars: function () {
        var me = this;

        me.tbar = [{
            xtype: 'button',
            text: 'Add',
            iconCls: 'fa fa-plus-circle',
            disabled: false,
            handler: 'onAddClickHandler'
        }, {
            xtype: 'button',
            text: 'Edit',
            reference: 'editButton',
            iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
            disabled: true,
            handler: 'onEditClickHandler'
        }, {
            xtype: 'button',
            text: 'Remove',
            iconCls: 'fa fa-times',
            bind: {
                disabled: '{!users.selection}'
            },
            handler: 'onRemoveClick'
        }, {
            xtype: 'button',
            text: 'View',
            reference: 'viewButton',
            iconCls: 'fa fa-eye',
            disabled: true,
            handler: 'onViewClickHandler'
        }, {
            xtype: 'button',
            text: 'Refresh',
            iconCls: 'fa fa-refresh',
            disabled: false,
            handler: 'onRefreshClick'
        },
        '->',
        {
            xtype: 'textfield',
            emptyText: 'Search',
            reference: 'search',
            width: 250,
            enableKeyEvents: true,
            triggers: {
                clear: {
                    cls: 'x-form-clear-trigger',
                    handler: 'onUserClearClick',
                    hidden: true,
                    scope: me
                },
                search: {
                    cls: 'x-form-search-trigger',
                    weight: 1,
                    handler: 'onUserSearchClick',
                    scope: me
                }
            },
            listeners: {
                change: 'onUserSearchChange',
                keypress: 'onUserSearchKeypress',
                buffer: 300,
                scope: me
            }
        }];
    },
    onUserClearClick: function (field, e) {
        var me = this;
        field.setValue();
        me.onUserSearchClick(field, e);
    },
    onUserSearchClick: function (field, e) {
        var me = this;
        var val = field.value;
        me.getView().setLoading(true);
        if (val) {
            field.getTrigger('clear').show();
            me.filterStore(val);
        } else {
            me.searchRegex = null;
            field.getTrigger('clear').hide();
            me.filterStore(val);
        }

        me.disabledButton();
        setTimeout(function () {
            me.getView().setLoading(false);
        }, 50);
    },
    disabledButton: function () {
        var me = this;
        var ctrl = me.lookupController();
        ctrl.setDisabledButtons(true, true);
        me.setSelection(null);
    },
    onUserSearchChange: function (field, value) {
        var me = this;
        me.searchRegex = new RegExp('(' + value + ')', "gi");
    },
    onUserSearchKeypress: function (field, e) {
        var me = this;
        if (e.keyCode == '13') {
            me.onUserSearchClick(field, e);
        }
    },
    filterStore: function (v) {        
        var me = this;        
        var store = me.getStore();
        store.clearFilter();
        store.each(function (record, idx) {
            contains = false;
            for (field in record.data) {
                var val = record.data[field].toString().toLowerCase();
                if (val.indexOf(v.toLowerCase()) > -1) {
                    contains = true;
                }
            }
            if (!contains) {
                record.filterMeOut = false;
            } else {
                record.filterMeOut = true;
            }
        });

        store.filterBy(function (rec, id) {
            if (rec.filterMeOut) {
                return true;
            }
            else {
                return false;
            }
        });
        
        
    }
});