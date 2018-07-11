Ext.define('Workflow.ux.form.field.LookupField', {
    extend: 'Ext.form.field.ComboBox',
    alias: ['widget.lookupfield'],
    allowBlank: false,
    editable: false,
    displayField: 'value',
    valueField: 'id',
    queryMode: 'remote',
    forceSelection: true,
    /* custom config */
    namespace: '',
    isChild: false,
    diabledClear: true,
    config:{
        parentId: 0
    },
    store: {
        type: 'form-lookup',
        proxy: {
            type: 'rest',
            url: Workflow.global.Config.baseUrl + 'api/forms/lookups',
            reader: {
                type: 'json'
            }
        }
    },
    listeners: {
        beforerender: function (elm, eOpts) {
			var me = this;
			var store = this.getStore();
			
			if (me.isChild && me.parentId == 0) {
                me.parentId = -1;
			}

			store.getProxy().extraParams = {
				name: me.namespace,
				parentId: me.parentId
			};
            
            store.load();
		},
        beforequery: function (q, e) {
            var me = this;
            var store = this.getStore();
            
            if (me.isChild && me.parentId == 0) {
                me.parentId = -1;
            }
            store.getProxy().extraParams = {
                name: me.namespace,
                parentId: me.parentId
            };
            store.filterBy(function (record) {
                return (record.get('active') === true);
            });
        },
        change: function (el, newValue, oldValue, eOpts) {
            if (!el.diabledClear) {
                if (newValue) {
                    el.getTrigger('clear').show();
                } else {
                    el.getTrigger('clear').hide();
                }
            }
        }
    },
    /* Trigger action event handler function */
    triggers: {
        clear: {
            weight: 1,
            cls: Ext.baseCSSPrefix + 'form-clear-trigger',
            hidden: true,
            handler: function (btn, e, eOpts) {
                btn.setValue(null);
            }
        }
    }
});