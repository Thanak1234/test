Ext.define('Workflow.view.approvalmgr.EmployeeList', {
    extend: 'Ext.grid.Panel',
    xtype: 'employee-list',
    controller: true,
    viewModel: {
        data: {
            filter: {
                teamId: 0,
                employeeLabel: null
            }
        }
    },
    width: 380,
    publishes: 'selection',
    store: new Ext.create('Ext.data.Store', {
        autoLoad: false,
        proxy: {
            type: 'rest',
            url: '/api/organization/employees',
            reader: {
                type: 'json'
            }
        },
        pageSize: 100
    }),
    selModel: {
        type: 'checkboxmodel'
    },
    initComponent: function () {
        var me = this;
        this.columns = [{
            xtype: 'templatecolumn',
            tpl: '<span><i class="fa fa-user"></i>&nbsp;{displayName}</span><br/>' +
                '<span style="font-size:11px;">Line. {lineName}</span><br/>' +
                '<span style="color:#777;font-size:11px;">Position.{level} - {position}</span>',
            flex: 1
        }];
    
        this.tbar = me.buildEmployeeHeader();
        this.callParent(arguments);
    },
    afterRender: function () {
        this.getStore().load();
        this.callParent(arguments);
    },
    searchEmployee: function(){
        var refs = this.getReferences(),
            store = this.getStore(),
            viewmodel = this.getViewModel(),
            data = viewmodel.getData();

        var teamId = data.filter.teamId,
            employeeLabel = data.filter.employeeLabel;

        store.clearFilter();
        if (employeeLabel) {
            store.filter({
                property: 'displayName',
                value: employeeLabel,
                exactMatch: false,
                anyMatch: true,
                caseSensitive: false
            });
        }

        if (teamId > 0) {
            store.filter('teamId', teamId);
        }

    },
    buildEmployeeHeader: function () {
        var me = this;
        return [ /*{
            iconCls: 'fa fa-pencil',
            handler: function () {

            }
        }, {
            iconCls: 'fa fa-plus-circle',
            handler: function () {

            }
        }, */
        {
            iconCls: 'fa fa-user',
            handler: function () {
                me.setSelection(null);
                me.hide();
                me.switchView(me)
            }
        }, {
            xtype: 'textfield',
            width: 220,
            selectOnFocus: true,
            triggers: {
                clear: {
                    type: 'clear',
                    hideWhenEmpty: true,
                    clearOnEscape: true,
                    weight: -1,
                    handler: function (field) {
                        field.reset();
                        me.searchEmployee();
                    }
                },
                search: {
                    cls: 'x-form-search-trigger',
                    weight: 1,
                    hideWhenEmpty: false,
                    clearOnEscape: false,
                    handler: function () {

                    }
                }
            },
            enableKeyEvents: true,
            emptyText: '-- EMPLOYEE --',
            reference: 'txtEmployeeSeach',
            bind: {
                value: '{filter.employeeLabel}'
            },
            listeners: {
                keypress: function (field, event) {
                    if (event.getKey() == event.ENTER) {
                        me.searchEmployee();
                        field.selectText();
                    }
                }
            }
        }, {
            iconCls: 'fa fa-refresh',
            handler: function () {
                me.getStore().clearFilter();
                me.getViewModel().set('filter.employeeLabel', null);
            }
        }, '->' , {
            iconCls: 'fa fa-sitemap',
            hidden: false,
            menu: {
                items: [{
                    xtype: 'tagfield',
                    hideLabel: true,
                    iconCls: 'fa fa-sitemap',
                    store: new Ext.create('Ext.data.Store', {
                        autoLoad: false,
                        proxy: {
                            type: 'rest',
                            url: '/api/organization/departments',
                            reader: {
                                type: 'json'
                            }
                        }
                    }),
                    displayField: 'fullDeptName',
                    valueField: 'teamId',
                    filterPickList: true,
                    multiSelect: false,
                    publishes: 'selection',
                    scope: this,
                    queryMode: 'local',
                    width: 450,
                    margin: '0 0 0 0',
                    padding: '0 0 0 0',
                    bind: {
                        value: '{filter.teamId}'
                    },
                    listeners: {
                        change: function (combo, value, oldValue) {
                            if (!value) {
                                me.getViewModel().set('filter.teamId', 0);
                                me.searchEmployee();
                            }
                        },
                        select: function (combo) {
                            me.searchEmployee();
                            combo.inputEl.dom.value = '';
                        },
                        render: function (combo) {
                            combo.getStore().load();
                        },
                        beforequery: function (record) {
                            record.query = new RegExp(record.query, 'i');
                            record.forceAll = true;
                        }
                    }
                }]
            }
        }];
    },
    switchView: function (grid) {

    }
});