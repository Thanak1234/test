/**
 *Author : Phanny 
 *
 */
Ext.define('Workflow.view.bcj.AnalysisItemViewController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.bcj-analysisitemview',
    
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
            grid = me.getView();
        var store = me.getView().getStore();

        me.showWindowDialog(el, 'Workflow.view.bcj.AnalysisItemWindow', { action: 'ADD' }, 'Add Financial Analysis', function (record) {
            store.add(record);
            me.updateData();
            return true;
        });
    },
    
    editItemAction: function (el, e, eOpts) {
        var me = this,
            analysisItem = me.getView().getViewModel().get('selectedItem');

        me.showWindowDialog(el, 'Workflow.view.bcj.AnalysisItemWindow', {
            action: 'EDIT',
            description: analysisItem.get('description'),
            quantity: analysisItem.get('quantity'),
            revenue: analysisItem.get('revenue')
        },
        'Edit Financial Analysis',
        function (record) {
            analysisItem.set('description', record.get('description'));
            analysisItem.set('quantity', record.get('quantity'));
            analysisItem.set('revenue', record.get('revenue'));
            me.updateData();
            return true;
        });
    },

    viewItemAction: function (el) {
        var me = this,
            analysisItem = me.getView().getViewModel().get('selectedItem');

        me.showWindowDialog(el, 'Workflow.view.bcj.AnalysisItemWindow',
        {
            action: 'VIEW',
            description: analysisItem.get('description'),
            quantity: analysisItem.get('quantity'),
            revenue: analysisItem.get('revenue')
        },
        'Financial Analysis', function (record) {
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
            analysisItemView = me.getView(),
            store = analysisItemView.getStore();
            projectDetailView = analysisItemView.mainView,
            reference = projectDetailView.getReferences();


        var viewModel = projectDetailView.getViewModel(),
            requestItemView = reference.requestItemView,
            projectDetail = viewModel.getData().projectDetail,
            viewmodel = analysisItemView.getViewModel();
            totalAmount = 0;

        store.each(function (record) {
            var total = record.get('quantity') * record.get('revenue');
            record.set('total', total);
            totalAmount += total;
        });

        viewmodel.set('totalAmount', Ext.util.Format.number(totalAmount, '$0,000.00'));

        if (totalAmount > 0) {
            projectDetail.paybackYear = Ext.util.Format.number(projectDetail.estimateCapex / totalAmount, '0.0');
            projectDetail.incrementalNetEbita = totalAmount;
        }
        projectDetail.incrementalNetContribution = totalAmount;
        viewModel.set('projectDetail', projectDetail);
        analysisItemView.getView().refresh(); // Refresh grid summary
    }
});
