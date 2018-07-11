Ext.define('Workflow.view.deptright.DeptRightManager', {
    extend: 'Ext.panel.Panel',
    xtype: 'deptrightmanager',
    controller: 'deptrightmanager',
    viewModel: {
        type: 'deptrightmanager'
    },
    title: 'Dept Right Management',
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
                margin: '0 0 5 0'
            },
            items: [
                {
                    xtype: 'panel',
                    title: 'Department Info',
                    frame: true,
                    padding: 10,
                    collapsible: true,
                    border: true,
                    layout: {
                        type: 'vbox',
                        align: 'stretch'
                    },
                    defaults: {
                        margin: 10
                    },
                    items: [
                        {
                            xtype: 'combo',
                            fieldLabel: 'WorkFlow',
                            displayField: 'description',
                            valueField: 'id',
                            reference: 'cmbForm',
                            publishes: 'value',
                            foreselect: true,
                            multiSelect: false,
                            queryMode: 'local',
                            minChar: 0,
                            store: {
                                type: 'dept-rights-forms'
                            },
                            listeners: {
                                select: 'onFormSelect'
                            }
                        },
                        {
                            xtype: 'combo',
                            fieldLabel: 'Department :',
                            reference: 'cmbDept',
                            store: {
                                type: 'dept-rights-DeptRightStore'
                            },
                            displayField: 'deptname',
                            valueField: 'id',
                            queryMode: 'remote',
                            foreselect: true,
                            triggers: {
                                clear: {
                                    cls: 'x-form-clear-trigger',
                                    handler: 'onDepartmentFilterClearClick'
                                }
                                //,
                                //search: {
                                //    cls: 'x-form-search-trigger',
                                //    weight: 1,
                                //    handler: ''
                                //}

                            }
                            ,
                            //bind: {
                            //    disabled: '{!cmbForm.value}',
                            //    filters: {
                            //        property: 'formId',
                            //        value: '{cmbForm.value}',
                            //        exactMatch: true,
                            //        caseSensitive: true
                            //    }
                            //},
                            listeners: {
                                select: 'onDepartmentSelect'
                            }
                        }
                    ]
                }
                ,
                {
                    xtype: 'grid',
                    title: 'Access Rights',
                    reference: 'deptacessright',
                    border: true,
                    disabled: false,
                    flex: 1,
                    multiColumnSort: false,
                    columnLines: true,
                    //selModel: {
                    //    selType: 'checkboxmodel'
                    //},
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
                                            dataIndex: 'empno',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'EMPLOYEE NAME *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'displayname',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'CREATED BY *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'createdby',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'CREATED DATE *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'createddate',
                                           
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'MODIFIED BY *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'modifiedby',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'MODIFIED DATE *',
                                            flex: 1,
                                           
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'modifieddate',
                                            renderer: hightligh
                                        },
                                        {
                                            text: 'STATUS *',
                                            flex: 1,
                                            sortable: true,
                                            menuDisabled: true,
                                            dataIndex: 'status',
                                            renderer: hightligh
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
                                displayMsg: 'Displaying employees {0} - {1} of {2}',
                                emptyMsg: "No employee to display"
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
                            },
                            //{
                            //    xtype: 'button',
                            //    text: 'Remove',
                            //    iconCls: 'fa fa-times',
                            //    handler: 'onRemoveClick',
                            //    bind: {
                            //        disabled: '{!gridUser.selection}'
                            //    }
                            //},
                            {
                                xtype: 'button',
                                text: 'Role Mngt Sync',
                                reference: 'syncButton',
                                iconCls: 'fa fa-eye',
                                handler: 'onRoleManagementSync',
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