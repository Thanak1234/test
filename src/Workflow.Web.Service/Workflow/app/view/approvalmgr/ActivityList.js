Ext.define('Workflow.view.approvalmgr.ActivityList', {
    extend: 'Ext.grid.Panel',
    xtype: 'activity-list',
    controller: true,
    viewModel: {
        data: {
            filter: {
                displayName: null
            }
        }
    },
    width: 300,
    publishes: 'selection',
    store: new Ext.create('Ext.data.Store', {
        autoLoad: false,
        proxy: {
            type: 'rest',
            url: '/api/organization/activities',
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
        this.tbar = [/*{
            iconCls: 'fa fa-pencil',
            handler: function () {

            }
        }, {
            iconCls: 'fa fa-plus-circle',
            handler: function () {

            }
        }, */{
            xtype: 'textfield',
            width: 240,
            selectOnFocus: true,
            triggers: {
                clear: {
                    type: 'clear',
                    hideWhenEmpty: true,
                    clearOnEscape: true,
                    weight: -1,
                    handler: function (field) {
                        field.reset();
                        me.searchActivity();
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
            emptyText: '-- ACTIVITY --',
            reference: 'txtActivitySeach',
            bind: {
                value: '{filter.displayName}'
            },
            listeners: {
                keypress: function (field, event) {
                    if (event.getKey() == event.ENTER) {
                        me.searchActivity();
                        field.selectText();
                    }
                }
            }
        }, '->', {
            iconCls: 'fa fa-refresh',
            handler: function () {
                me.getStore().clearFilter();
                me.getViewModel().set('filter.displayName', null);
            }
        }];
        this.callParent(arguments);
    },
    columns: [{
        xtype: 'templatecolumn',
        tpl: '<span><i class="fa fa-cog"></i>&nbsp;{displayName}</span><br/>' +
            '<span style="font-size:11px;">Act Code. {actCode}</span><br/>' +
            '<span style="color:#777;font-size:11px;">Path. {processName}</span>',
        flex: 1
    }],
    bind: {
        selection: '{activities}'
    },
    
    searchActivity: function () {
        var refs = this.getReferences(),
            store = this.getStore(),
            viewmodel = this.getViewModel(),
            data = viewmodel.getData();

        var displayName = data.filter.displayName;

        store.clearFilter();
        if (displayName) {
            store.filter({
                property: 'displayName',
                value: displayName,
                exactMatch: false,
                anyMatch: true,
                caseSensitive: false
            });
        }
    }
});