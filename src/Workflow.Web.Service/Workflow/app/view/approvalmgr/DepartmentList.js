Ext.define('Workflow.view.approvalmgr.DepartmentList', {
    extend: 'Ext.grid.Panel',
    xtype: 'departments-list',
    controller: true,
    viewModel: {
        data: {
            filter: {
                departmentLabel: null
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
            url: '/api/organization/departments',
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
            tpl: '<span><i class="fa fa-sitemap"></i>&nbsp;{groupName} > {deptName}</span><br/>' +
                '<span style="font-size:11px;">Team. {teamName}</span><br/>' +
                '<span style="color:#777;font-size:11px;">Director. 009951 - HOE SIEW NEE</span>',
            flex: 1
        }];

        this.tbar = me.buildDepartmentHeader();
        this.callParent(arguments);
    },
    afterRender: function () {
        this.getStore().load({
            callback: function () {

            }
        });

        this.callParent(arguments);
    },
    searchDepartment: function () {
        var refs = this.getReferences(),
            store = this.getStore(),
            viewmodel = this.getViewModel(),
            data = viewmodel.getData();

        var departmentLabel = data.filter.departmentLabel;

        store.clearFilter();
        if (departmentLabel) {
            store.filter({
                property: 'fullDeptName',
                value: departmentLabel,
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
            iconCls: 'fa fa-user',
            handler: function () {
                me.hideMe()
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
                        me.searchDepartment();
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
            emptyText: '-- DEPARTMENT --',
            reference: 'txtDepartmentSeach',
            bind: {
                value: '{filter.departmentLabel}'
            },
            listeners: {
                keypress: function (field, event) {
                    if (event.getKey() == event.ENTER) {
                        me.searchDepartment();
                        field.selectText();
                    }
                }
            }
        }, {
            iconCls: 'fa fa-refresh',
            handler: function () {

            }
        }];
    },
    hideMe: function () {

    }
});