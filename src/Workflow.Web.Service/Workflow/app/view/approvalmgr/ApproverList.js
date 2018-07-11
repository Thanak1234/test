Ext.define('Workflow.view.approvalmgr.ApproverList', {
    extend: 'Ext.grid.Panel',
    xtype: 'approver-list',
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
    selModel: {
        type: 'checkboxmodel'
    },
    store: new Ext.create('Ext.data.Store', {
        autoLoad: false,
        proxy: {
            type: 'rest',
            url: '/api/organization/approvers',
            reader: {
                type: 'json'
            }
        }
    }),
    approverStore: new Ext.create('Ext.data.Store', {
        autoLoad: false,
        proxy: {
            type: 'rest',
            url: '/api/organization/available-approvers',
            reader: {
                type: 'json'
            }
        }
    }),
    columns: [{
        xtype: 'templatecolumn',
        tpl: '<span><i class="fa fa-user"></i>&nbsp;{displayName}</span><br/>' +
            '<span style="font-size:11px;">Email. {email}</span><br/>' +
            '<span style="font-size:11px;">Line. {lineName}</span><br/>' +
            '<span style="color:#777;font-size:11px;">Position.{level} - {position}</span>',
        flex: 1
    }],
    
    loadData: function (params, callback) {
        var me = this,
            refs = me.getReferences();
            tagApprover = refs.tagApprover,
            btnSave = refs.btnSave,
            tagApprover.reset();

        btnSave.setDisabled(true);
        this.params = params;
        this.getStore().load({
            params: params,
            callback: function (approvers) {
                me.approverStore.load({
                    params: {
                        type: params.type,
                        ids: params.ids
                    },
                    callback: function (records) {
                        Ext.each(approvers, function (approver) {
                            tagApprover.addValue(approver.get('id'));
                        });
                        callback(approvers);
                    }
                })
            }
        });
    },
    initComponent: function () {
        var me = this;

        this.tbar = [{
            iconCls: 'fa fa-user',
            menu: {
                items: [{
                    xtype: 'tagfield',
                    displayField: 'displayName',
                    valueField: 'id',
                    publishes: 'selection',
                    reference: 'tagApprover',
                    queryMode: 'local',
                    store: me.approverStore,
                    hideLabel: true,
                    filterPickList: true,
                    multiSelect: true,
                    scope: this,
                    width: 450,
                    margin: '0 0 0 0',
                    padding: '0 0 0 0',
                    listeners: {
                        select: function (combo, records) {
                            var refs = me.getReferences(),
                                btnSave = refs.btnSave,
                                store = me.getStore();
                            
                            btnSave.setDisabled(false);
                            store.removeAll();
                            Ext.each(records, function (record) {
                                var data = record.getData();
                                if (!store.getById(data.id)) {
                                    store.add(records);
                                }
                            });
                            
                            combo.inputEl.dom.value = '';
                        },
                        beforequery: function (record) {
                            record.query = new RegExp(record.query, 'i');
                            record.forceAll = true;
                        }
                    }
                }]
            }
        }, {
            xtype: 'textfield',
            width: 200,
            selectOnFocus: true,
            triggers: {
                clear: {
                    type: 'clear',
                    hideWhenEmpty: true,
                    clearOnEscape: true,
                    weight: -1,
                    handler: function (field) {
                        field.reset();
                        me.searchApprover();
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
            emptyText: '-- APPROVER --',
            reference: 'txtApproverSeach',
            bind: {
                value: '{filter.displayName}'
            },
            listeners: {
                keypress: function (field, event) {
                    if (event.getKey() == event.ENTER) {
                        me.searchApprover();
                        field.selectText();
                    }
                }
            }
        }, '->', {
            iconCls: 'fa fa-save',
            reference: 'btnSave',
            hidden: false,
            disabled: true,
            handler: function () {
                var refs = me.getReferences(),
                    records = [],
                    store = me.getStore(),
                    tagApprover = refs['tagApprover'];

                var approvers = tagApprover.getValue();
                store.each(function (record) {
                    records.push(record);
                });
                me.saveApprovers(me, records, store);
            }
        }];

        this.callParent(arguments);
    },
    saveApprovers: function (grid, store) {
        
    },
    searchApprover: function () {
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