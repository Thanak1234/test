/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.bcj.RequestItemViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.bcj-requestitemview',
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear : 'clearData'
            }      
        }
    },
    
    loadData: function(data){
        var me  = this,
            grid = me.getView();

        var dataStore = this.getView().getViewModel().getStore('dataStore');
        if(data.data &&  data.data.length>0){
            dataStore.setData(data.data);
        }
        
        me.getView().setStore(dataStore);
        me.getView().getViewModel().set('viewSetting', data.viewSetting);
        me.updateData();
    },
    clearData: function () {
       this.getView().getStore().removeAll();
    },
    addUserAction : function (el, e, eOpts) {
        var me = this,
            grid = me.getView();
        var store = me.getView().getStore();

        me.showWindowDialog(el, 'Workflow.view.bcj.RequestItemWindow',
        { action: 'ADD' },
        'Add Capitial Requirement',
        function (record) {
            store.add(record);
            me.updateData();
            return true;
        });
    },
    
    editItemAction : function(el, e, eOpts){
        var me = this,
            requestItem = me.getView().getViewModel().get('selectedItem');
            
        me.showWindowDialog(el, 'Workflow.view.bcj.RequestItemWindow', {
            action: 'EDIT',
            item: requestItem.get('item'),
            quantity: requestItem.get('quantity'),
            unitPrice: requestItem.get('unitPrice')
        },
        'Edit Capitial Requirement',
        function (record) {
            requestItem.set('item', record.get('item'));
            requestItem.set('quantity', record.get('quantity'));
            requestItem.set('unitPrice', record.get('unitPrice'));
            me.updateData();
            return true;
        });
    },
    
    viewItemAction : function (el) {
        var me = this,
            requestItem = me.getView().getViewModel().get('selectedItem');

        me.showWindowDialog(el, 'Workflow.view.bcj.RequestItemWindow',
        {
            action: 'VIEW',
            item: requestItem.get('item'),
            quantity: requestItem.get('quantity'),
            unitPrice: requestItem.get('unitPrice')
        },
        'Capitial Requirement', function (record) {
            return true;
        });
    },
    removeRecord: function (grid, rowIndex, colIndex){
        var me      = this,
            store   = grid.getStore(), 
            rec     = store.getAt(rowIndex);
            
        Ext.MessageBox.show({
            title       : 'Alert',
            msg         : 'Are you sure to delete this record?',
            buttons     : Ext.MessageBox.YESNO,
            icon        : Ext.MessageBox.QUESTION,
            scope       : this,
            fn          : function(bt){
            if(bt==='yes') {
                store.remove(rec);
                me.updateData();
                me.showToast(Ext.String.format('Request item {0} has been removed', rec.get('Item')));
            }
            } 
        });
    },
    updateData: function () {
        var me = this,
            requestItem = me.getView(),
            store = requestItem.getStore();


        var viewModel = requestItem.mainView.getViewModel(),
            projectDetail = viewModel.getData().projectDetail,
            viewmodel = requestItem.getViewModel();
            totalAmount = 0;

        store.each(function (record) {
            var total = record.get('quantity') * record.get('unitPrice');
            record.set('total', total);
            totalAmount += total;
        });
        viewmodel.set('totalAmount', Ext.util.Format.number(totalAmount, '$0,000.00'));

        if (projectDetail.totalAmount != totalAmount) {
            projectDetail.estimateCapex = totalAmount;
        }
        projectDetail.totalAmount = totalAmount;

        if (projectDetail.incrementalNetContribution > 0) {
            projectDetail.paybackYear = Ext.util.Format.number(totalAmount / projectDetail.incrementalNetContribution, '0.0');
        }
        
        viewModel.set('projectDetail', projectDetail);

        requestItem.getView().refresh(); // Refresh grid summary
    }
});
