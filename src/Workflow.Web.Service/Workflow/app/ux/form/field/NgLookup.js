﻿Ext.define('Workflow.ux.field.NgLookup', {
    extend: 'Ext.form.field.ComboBox',
    alias: ['widget.ngLookup'],
    allowBlank: false,
    editable: true,
    queryMode: 'local',
    forceSelection: true,
    initComponent: function () {
        var me = this;
        me.buildStore();
        me.callParent(arguments);
    },
    buildStore: function() {
        var me = this;
        this.store = {
            autoLoad: true,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + me.url,
                reader: {
                    type: 'json'
                }
            }
        };
    },
    listeners: {
        beforerender: function (elm, eOpts) {
            var me = this;
            var store = this.getStore();
            if (me.params) {
                store.getProxy().extraParams = me.params;
            };
            store.load();
        }
    }
});