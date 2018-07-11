Ext.define('Workflow.view.approvalmgr.ApplicationList', {
    extend: 'Ext.grid.Panel',
    xtype: 'application-list',
    controller: true,
    publishes: 'selection',
    viewModel: {
        data: {
            filter: {
                processName: null
            }
        }
    },
    bind: {
        selection: '{application}'
    },
    selModel: {
        type: 'checkboxmodel'
    },
    store: new Ext.create('Ext.data.Store', {
        autoLoad: false,
        proxy: {
            type: 'rest',
            url: '/api/organization/applications',
            reader: {
                type: 'json'
            }
        }
    }),
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
                        me.searchApplication();
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
            emptyText: '-- APPLICATION --',
            reference: 'txtApplicationSeach',
            bind: {
                value: '{filter.processName}'
            },
            listeners: {
                keypress: function (field, event) {
                    if (event.getKey() == event.ENTER) {
                        me.searchApplication();
                        field.selectText();
                    }
                }
            }
        }, '->', {
            iconCls: 'fa fa-refresh',
            handler: function () {
                me.getStore().clearFilter();
                me.getViewModel().set('filter.processName', null);
            }
        }];
        this.callParent(arguments);
    },
    
    columns: [{
        xtype: 'templatecolumn',
        tpl: '<span><i class="fa fa-windows"></i>&nbsp;{processName}</span><br/>' +
            '<span style="font-size:11px;">Code. {requestCode}</span><br/>' +
            '<span style="color:#777;font-size:11px;">Path. {processPath}</span>',
        flex: 1
    }],
    afterRender: function () {
        this.getStore().load();
        this.callParent(arguments);
    },
    searchApplication: function () {
        var refs = this.getReferences(),
            store = this.getStore(),
            viewmodel = this.getViewModel(),
            data = viewmodel.getData();

        var processName = data.filter.processName;

        store.clearFilter();
        if (processName) {
            store.filter({
                property: 'processName',
                value: processName,
                exactMatch: false,
                anyMatch: true,
                caseSensitive: false
            });
        }
    }
});