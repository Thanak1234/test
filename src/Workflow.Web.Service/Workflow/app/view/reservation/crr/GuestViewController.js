/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.reservation.crr.GuestViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.crr-guestview',

    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear: 'clearData'
            }
        }
    },

    loadData: function (data) {

        var me = this;
        var dataStore = this.getView().getViewModel().getStore('dataStore');

        if (data.data && data.data.length > 0) {
            //Load data to from    



            dataStore.setData(data.data);
        }

        me.getView().setStore(dataStore);
        me.getView().getViewModel().set('viewSetting', data.viewSetting);

    },

    clearData: function () {
        this.getView().getStore().removeAll();
    },
    addGuestAction: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.reservation.crr.GuestWindow', { action: 'ADD' }, 'Add Guest', function (record) {
            //if (me.checkExisting(record)) {
            //    Ext.Msg.alert({
            //        title: 'Information',
            //        msg: 'The guest is already added.',
            //        icon: Ext.MessageBox.ERROR
            //    });
            //    return false;
            //}            
            console.log('add record', record);
            me.getView().getStore().add(record);
            return true;
        });
    },

    editItemAction: function (el, e, eOpts) {
        var me = this,
            currRecord = me.getView().getViewModel().get('selectedItem'),            
            id = currRecord.get('id');
            

        me.showWindowDialog(el, 'Workflow.view.reservation.crr.GuestWindow', {
            action: 'EDIT',
            name: currRecord.get('name'),
            title: currRecord.get('title'),
            companyName: currRecord.get('companyName'),
            requestHeaderId: currRecord.get('requestHeaderId')
        }, 'Edit Request Guest', function (record) {
            //if (currRecord.get('id') !== record.get('id') && me.checkExisting(record)) {
            //    Ext.Msg.alert({
            //        title: 'Information',
            //        msg: 'The guest is already added.',
            //        icon: Ext.MessageBox.ERROR
            //    });
            //    return false;
            //}

            currRecord.set(record.getData());
            currRecord.set('id', id);
            return true;
        });
    },

    viewItemAction: function (el) {
        var me = this,
        currRecord = me.getView().getViewModel().get('selectedItem'),
        viewModel = me.getView().getViewModel();
        me.showWindowDialog(el, 'Workflow.view.reservation.crr.GuestWindow', {
            action: 'VIEW',
            name: currRecord.get('name'),
            title: currRecord.get('title'),
            companyName: currRecord.get('companyName'),
            requestHeaderId: currRecord.get('requestHeaderId'),
            viewSetting: viewModel.viewSetting
        }, 'VIEW Request Guest', function (record) {
            return true;
        });
    },
    removeRecord: function (grid, rowIndex, colIndex) {
        var me = this,
            store = grid.getStore(),
            rec = store.getAt(rowIndex);

        Ext.MessageBox.show({
            title: 'Alert',
            msg: 'Are you sure to delete this record?',
            buttons: Ext.MessageBox.YESNO,
            icon: Ext.MessageBox.QUESTION,
            scope: this,
            fn: function (bt) {
                if (bt === 'yes') {
                    store.remove(rec);
                    me.showToast(Ext.String.format('Request Guest {0} has been removed', rec.get('name')));
                }
            }
        });
    },

    checkExisting: function record(record) {
        var store = this.getView().getStore();
        return store.query('name', record.get('name')).items.length > 0
    }



});
