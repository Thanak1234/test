Ext.define('Workflow.view.reports.it.ITReportController', {
    extend: 'Workflow.view.reports.procInst.ProcessInstanceReportController',
    alias: 'controller.report-it',
    init: function () {
        var me = this;
    },
    buildStore: function (store, params, viewmodel) {
        
        return store;
    },
    onSessionChanged: function (el, newValue, oldValue, eOpt) {

        this.onSelectChanged(el, newValue, oldValue, eOpt);

        var id = el.getSelection() ? el.getSelection().get('id') : 0;
        
        var me = this, item = me.getReferences().it_item;
        item.clearValue();
        item.getStore().getProxy().extraParams = { sessionId: id };
        item.getStore().load();
    },
    onItemChanged: function (el, newValue, oldValue, eOpt) {

        var me = this,
            itemType = me.getReferences().it_item_type;
        
        var itemId = el.getSelection() ? el.getSelection().get('id') : 0;

        itemType.clearValue();
        itemType.getStore().getProxy().extraParams = { itemId: itemId };
        itemType.getStore().load();

        this.itemChange(el, newValue, oldValue, eOpt);
        me.onSelectChanged(el, newValue, oldValue, eOpt);        
    },

    onItemTypeChanged: function (el, newValue, oldValue, eOpt) {
        this.itemChange(el, newValue, oldValue, eOpt);
    },


    itemChange: function (el, newValue, oldValue, eOpt) {

        var me = this,
             references = me.getReferences(),
             item = references.it_item.getSelection(),
             itemId = item ? item.get('id') : 0,
             itemTypeId = references.it_item_type.getSelection() ? references.it_item_type.getSelection().get('id') : 0;
        me.onSelectChanged(el, newValue, oldValue, eOpt);
        references.it_item_role.clearValue();
        references.it_item_role.getStore().getProxy().extraParams = { itemId: itemId, itemTypeId: itemTypeId };
        references.it_item_role.getStore().load();        
    },
    setDisableCombo: function (item, itemType, role) {
        var me = this;
        var r = me.getReferences();
        r.it_item.setDisabled(item);
        r.it_item_type.setDisabled(itemType);
        r.it_item_role.setDisabled(role);
    },
    onClearClick: function (btn, e, eOpts) {
        btn.setValue(null);
    },
    onSelectChanged: function (el, newValue, oldValue, eOpts) {
        if (newValue) {
            el.getTrigger('clear').show();
        } else {
            el.getTrigger('clear').hide();
        }
    }
});
