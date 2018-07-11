Ext.define('Workflow.view.approvalmgr.RoleList', {
    extend: 'Ext.grid.Panel',
    xtype: 'role-list',
    controller: true,
    viewModel: {
        data: {
            filter: {
                name: null
            }
        }
    },
    selModel: {
        type: 'checkboxmodel'
    },
    width: 380,
    publishes: 'selection',
    store: new Ext.create('Ext.data.Store', {
        autoLoad: false,
        proxy: {
            type: 'rest',
            url: '/api/organization/roles',
            params: { actIds: '0'},
            reader: {
                type: 'json'
            }
        },
        pageSize: 100
    }),
    initComponent: function () {
        var me = this;
        this.columns = [{
            xtype: 'templatecolumn',
            tpl: '<span><i class="fa fa-users"></i>&nbsp;{name}</span><br/>' +
                '<span style="font-size:11px;">Code. {roleId}</span><br/>',
            flex: 1
        }];

        this.tbar = me.buildDepartmentHeader();
        this.callParent(arguments);
    },
    searchRole: function () {
        var refs = this.getReferences(),
            store = this.getStore(),
            viewmodel = this.getViewModel(),
            data = viewmodel.getData();

        var name = data.filter.name;

        store.clearFilter();
        if (name) {
            store.filter({
                property: 'name',
                value: name,
                exactMatch: false,
                anyMatch: true,
                caseSensitive: false
            });
        }
    },
    buildDepartmentHeader: function () {
        var me = this;
        return [{
            xtype: 'button',
            iconCls: 'fa fa-users',
            handler: function () {
                me.setSelection(null);
                me.hide();
                me.switchView(me);
            }
        },/*{
            iconCls: 'fa fa-pencil',
            handler: function () {

            }
        }, {
            iconCls: 'fa fa-plus-circle',
            handler: function () {

            }
        }, */{
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
                        me.searchRole();
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
            emptyText: '-- ROLE --',
            reference: 'txtDepartmentSeach',
            bind: {
                value: '{filter.name}'
            },
            listeners: {
                keypress: function (field, event) {
                    if (event.getKey() == event.ENTER) {
                        me.searchRole();
                        field.selectText();
                    }
                }
            }
        }, {
            iconCls: 'fa fa-refresh',
            handler: function () {
                me.getStore().clearFilter();
                me.getViewModel().set('filter.name', null);
            }
        }];
    },
    switchView: function (grid) {

    }
});