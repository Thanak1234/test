Ext.define('Workflow.view.hr.att.FlightDetailViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.att-flightdetailview',

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
    addFlightDetailAction: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.hr.att.FlightDetailWindow', { action: 'ADD' }, 'Add FlightDetail', function (record) {
            me.getView().getStore().add(record);
            return true;
        });
    },

    editFlightDetailAction: function (el, e, eOpts) {
        var me = this,
            currRecord = me.getView().getViewModel().get('selectedItem'),            
            id = currRecord.get('id');

        me.showWindowDialog(el, 'Workflow.view.hr.att.FlightDetailWindow', {
            action: 'EDIT',
            fromDestination: currRecord.get('fromDestination'),
            toDestination: currRecord.get('toDestination'),
            date: currRecord.get('date'),
            time: currRecord.get('time'),
            requestHeaderId: currRecord.get('requestHeaderId')
        }, 'Edit Request Flight Detail', function (record) {
            currRecord.set(record.getData());
            currRecord.set('id', id);
            return true;
        });
    },

    viewFlightDetailAction: function (el) {
        var me = this,
        currRecord = me.getView().getViewModel().get('selectedItem'),
        viewModel = me.getView().getViewModel();
        me.showWindowDialog(el, 'Workflow.view.hr.att.FlightDetailWindow', {
            action: 'VIEW',
            fromDestination: currRecord.get('fromDestination'),
            toDestination: currRecord.get('toDestination'),
            date: currRecord.get('date'),
            time: currRecord.get('time'),
            requestHeaderId: currRecord.get('requestHeaderId'),
            viewSetting: viewModel.viewSetting
        }, 'VIEW Flight Detail', function (record) {
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
                    me.showToast(Ext.String.format('Request Flight Detail {0} has been removed', rec.get('name')));
                }
            }
        });
    },

    checkExisting: function record(record) {
        var store = this.getView().getStore();
        return store.query('name', record.get('name')).items.length > 0
    }



});
