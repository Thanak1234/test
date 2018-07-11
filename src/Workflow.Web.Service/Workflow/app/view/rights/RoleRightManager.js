Ext.define('Workflow.view.rights.RoleRightManager', {
    extend: 'Ext.panel.Panel',
    xtype: 'rolerightmanager',
    controller: 'rolerightmanager',
    viewModel: {
        type: 'rolerightmanager'
    },
    title: 'Role Right Management',
    layout: {
        type: 'border',
        padding: 0
    },
    titleAlign: 'center',
    width: 1100,
    frame: true,
    initComponent: function () {
        var me = this;
        me.callParent(arguments);
    },
    items: [
        {
            xtype: 'form',
            region: 'center',
            defaultType: 'textfield',
            fieldDefaults: {
                labelWidth: 120
            },
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            defaults: {
                margin: 5
            },
            items: [
                {
                    xtype: 'panel',
                    title: 'Form Info',
                    frame: true,
                    padding: 10,
                    collapsible: true,
                    layout: {
                        type: 'vbox',
                        align: 'stretch'
                    },
                    items: [
                        {
                            xtype: 'combo',
                            fieldLabel: 'Form :',
                            displayField: 'description',
                            valueField: 'id',
                            reference: 'cmbForm',
                            publishes: 'value',
                            foreselect: true,
                            multiSelect: false,
                            queryMode: 'local',
                            minChar: 0,
                            store: {
                                type: 'rights-forms'
                            },
                            listeners: {
                                select: 'onFormSelect'
                            }
                        },
                        {
                            xtype: 'combo',
                            fieldLabel: 'Activity :',
                            reference: 'cmbActivity',
                            store: {
                                type: 'rights-activities'
                            },
                            displayField: 'description',
                            valueField: 'id',
                            queryMode: 'local',
                            foreselect: true,
                            bind: {
                                disabled: '{!cmbForm.value}',
                                filters: {
                                    property: 'formId',
                                    value: '{cmbForm.value}',
                                    exactMatch: true,
                                    caseSensitive: true
                                }
                            },
                            listeners: {
                                select: 'onActivitySelect'
                            }
                        },
                        {
                            xtype: 'combo',
                            fieldLabel: 'Role :',
                            reference: 'cmbRole',
                            disabled: true,
                            displayField: 'displayName',
                            valueField: 'id',
                            queryMode: 'local',
                            foreselect: true,
                            listeners: {
                                select: 'onRoleSelectChange'
                            }
                        }
                    ]
                },                
                {
                    xtype: 'grid',
                    title: 'Users',
                    reference: 'gridUser',
                    frame: true,
                    disabled: true,
                    flex: 1,
                    multiColumnSort: false,
                    columnLines: true,
                    selModel: {
                        selType: 'checkboxmodel'
                    },
                    bind: {
                        selection: '{user}'
                    },
                    viewConfig: {                
                        listeners: {
                            refresh: function (dataview) {
                                Ext.each(dataview.panel.columns, function(column) {
                                    column.autoSize();
                                })
                            },
                            rowdblclick: 'onUserDoubleClick',
                            selectionchange: 'onUserSelectionChange'
                        }
                    },
                    xhooks: {
                        initComponent: function () {
                            var me = this;
                            me.buildColumns();
                            me.buildPaging();
                            me.buildToolbar();
                            me.callParent();
                        }
                    },
                    buildColumns: function() {
                        var me = this;

                        function hightligh(value) {
                            if (!value) {
                                value = '';
                            }
                            var newValue = me.searchRegex ? value.toString().replace(me.searchRegex, '<span style="color:red;font-weight:bold; font-size:110%">$1</span>') : value;
                            return newValue;
                        }

                        me.columns = [
                                        {
                                            xtype: 'rownumberer'
                                        },
                                        {
                                            text: 'EMP NO *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'employeeNo',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'NAME *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'fullName',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'POSITION *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'position',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'DEPARTMENT *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'subDept',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'EMAIL *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'email',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'Ext *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'ext',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'DESCRIPTION *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'desc',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'CREATED DATE *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'createdDate',
                                            renderer: function (val) {
                                                return hightligh(Ext.util.Format.date(val, 'Y-m-d H:i:s'));
                                            }
                                        },
                                        {
                                            header: 'ACTIVE',
                                            width: 136,
                                            sortable: false,
                                            menuDisabled: true,
                                            dataIndex: 'active',
                                            renderer: function (value) {
                                                return value == true ? 'Yes' : 'No';
                                            }
                                        }
                        ];
                    },
                    buildPaging: function() {
                        var me = this;

                        me.bbar = [
                            {
                                xtype: 'pagingtoolbar',
                                reference: 'paging',
                                displayInfo: true,
                                displayMsg: 'Displaying users {0} - {1} of {2}',
                                emptyMsg: "No users to display"
                            }
                        ]
                    },
                    buildToolbar: function() {
                        var me = this;

                        me.tbar = [
                            {
                                xtype: 'button',
                                text: 'Add',
                                iconCls: 'fa fa-plus-circle',
                                handler: 'onAddClick'
                            }, {
                                xtype: 'button',
                                text: 'Edit',
                                reference: 'editButton',
                                iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
                                handler: 'onEditClick',
                                disabled: true
                            }, {
                                xtype: 'button',
                                text: 'Remove',
                                iconCls: 'fa fa-times',
                                handler: 'onRemoveClick',
                                bind: {
                                    disabled: '{!gridUser.selection}'
                                }
                            },
                            {
                                xtype: 'button',
                                text: 'View',
                                reference: 'viewButton',
                                iconCls: 'fa fa-eye',
                                handler: 'onViewClick',
                                disabled: true
                            },
                            {
                                xtype: 'button',
                                text: 'Refresh',
                                iconCls: 'fa fa-refresh',
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
                                        handler: 'onClearClick',
                                        hidden: true
                                    },
                                    search: {
                                        cls: 'x-form-search-trigger',
                                        weight: 1,
                                        handler: 'onSearchClick'
                                    }
                                },
                                listeners: {
                                    change: 'onSearchChange',
                                    keypress: 'onSearchKeypress',
                                    buffer: 300
                                }
                            }
                        ];
                    }                    
                }
            ]
        }
    ]
});