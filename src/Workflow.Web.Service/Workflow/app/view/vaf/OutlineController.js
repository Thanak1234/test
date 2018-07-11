Ext.define('Workflow.view.vaf.OutlineController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.vaoutline',
    viewOutline: function(el) {
        var me = this;
        var viewmodel = me.getView().getViewModel();
        var currRecord = viewmodel.get('selectedRow');
        var data = currRecord.data;
        me.showWindowDialog(el, 'Workflow.view.vaf.ItemWindow', {
            action: 'VIEW',
            outline: data,
            readOnly: true
        }, 'Outline', function (record) {            
            return true;
        });
    },
    editOutline: function(el) {
        var me = this;
        var viewmodel = me.getView().getViewModel();
        var currRecord = viewmodel.get('selectedRow');
        var data = currRecord.data;
        var id = currRecord.get('Id');
        var v = me.getView();
        var parent = v.parent;
        var viewmodel = parent.getViewModel();
        me.showWindowDialog(el, 'Workflow.view.vaf.ItemWindow', {
            action: 'EDIT',
            outline: data,
            readOnly: false,
            adjustType: viewmodel.get('Information.AdjType')
        }, 'Outline', function (record) {
            if (id !== record.get('Id') && me.isIn(record)) {
                Ext.Msg.alert({
                    title: 'Information',
                    msg: 'The item is already added.',
                    icon: Ext.MessageBox.ERROR
                });
                return false;
            }

            currRecord.set(record.getData());
            currRecord.set('Id', id);
            v.refreshData();
            return true;
        });
        
    },
    addOutline: function (el) {
        var me = this;
        var v = me.getView();
        var parent = v.parent;
        var viewmodel = parent.getViewModel();
        me.showWindowDialog(el, 'Workflow.view.vaf.ItemWindow', {
            action: 'ADD',
            readOnly: false,
            varianceType: null,
            adjustType: viewmodel.get('Information.AdjType')
        }, 'Outline', function (record) {
            if (me.isIn(record)) {
                Ext.Msg.alert({
                    title: 'Information',
                    msg: 'The item is already added.',
                    icon: Ext.MessageBox.ERROR
                });
                return false;
            }
            me.getView().getStore().add(record);
            v.refreshData();
            return true;
        });        
    },
    isIn: function (record) {
        var itemStore = this.getView().getStore();
        return itemStore.query('Id', record.get('Id')).items.length > 0;
    },
    removeRecord: function (grid, rowIndex, colIndex) {
        var me = this,
            store = grid.getStore(),
            rec = store.getAt(rowIndex);
        var v = me.getView();
        Ext.MessageBox.show({
            title: 'Alert',
            msg: 'Are you sure to delete this record?',
            buttons: Ext.MessageBox.YESNO,
            icon: Ext.MessageBox.QUESTION,
            scope: this,
            fn: function (bt) {
                if (bt === 'yes') {
                    store.remove(rec);
                    v.refreshData();
                    me.showToast(Ext.String.format('Outline item is removed'));
                }
            }
        });
    }
});
