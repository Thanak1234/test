/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.pbf.SpecificationController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.pbf-specification',
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    
    loadData: function (data) {
        var me = this;
        var dataStore = this.getView().getViewModel().getStore('dataStore');
        if (data.data && data.data.length > 0) {
            dataStore.setData(data.data);
        }

        me.getView().setStore(dataStore);
        me.getView().getViewModel().set('viewSetting', data.viewSetting);
        me.updateData();
    },

    clearData: function () {
        this.getView().getStore().removeAll();
    },
    addUserAction: function (el, e, eOpts) {
        var me = this,
            grid = me.getView(),
            mainViewModel = grid.mainView.getViewModel();
        var store = me.getView().getStore();
        var viewSetting = me.getView().getViewModel().get('viewSetting');
        var projectReference = mainViewModel.get('projectBrief.projectReference');
        var viewmodel = { action: 'ADD', viewSetting: viewSetting };

        if (projectReference) {
            var num = store.count() + 1;
            viewmodel.itemRef = projectReference + '-' + ("0" + num).slice(-2);
        }

        me.showWindowDialog(el, 'Workflow.view.pbf.SpecificationWindow', viewmodel, 'Add Item Request', function (record) {
            store.add(record);
            me.updateData();
            return true;
        });
    },
    
    editItemAction: function (el, e, eOpts) {
        var me = this,
            specification = me.getView().getViewModel().get('selectedItem');

        var viewSetting = me.getView().getViewModel().get('viewSetting');

        me.showWindowDialog(el, 'Workflow.view.pbf.SpecificationWindow', {
            action: 'EDIT',
            viewSetting: viewSetting,
            description: specification.get('description'),
            quantity: specification.get('quantity'),
            itemId: specification.get('itemId'),
            name: specification.get('name'),
            itemRef: specification.get('itemRef')
        },
        'Edit Item Request',
        function (record) {
            specification.set('description', record.get('description'));
            specification.set('quantity', record.get('quantity'));
            specification.set('itemId', record.get('itemId'));
            specification.set('name', record.get('name'));
            specification.set('itemRef', record.get('itemRef'));
            
            me.updateData();
            return true;
        });
    },

    viewItemAction: function (el) {
        var me = this,
            specification = me.getView().getViewModel().get('selectedItem');
        var viewSetting = me.getView().getViewModel().get('viewSetting');

        me.showWindowDialog(el, 'Workflow.view.pbf.SpecificationWindow',
        {
            action: 'VIEW',
            viewSetting: viewSetting,
            description: specification.get('description'),
            quantity: specification.get('quantity'),
            itemId: specification.get('itemId'),
            name: specification.get('name'),
            itemRef: specification.get('itemRef')
        },
        'Item Request', function (record) {
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
                    me.updateData();
                    me.showToast(Ext.String.format('Request item {0} has been removed', rec.get('Item')));
                }
            }
        });
    },
    updateData: function () {
        var me = this,
            specificationView = me.getView();

        specificationView.getView().refresh(); // Refresh grid summary
    }
});
