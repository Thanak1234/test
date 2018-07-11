Ext.define('Workflow.view.ticket.setting.subcategory.SubCategoryPanelController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-subcategory-subcategory',
    init: function () {
        // this.loadData();
    },
    loadData: function (params) {
        var me = this;
        var model = me.getView().getViewModel();

        var ticketSettingSubCategoryStore = model.getStore('ticketSettingSubCategoryStore');        
        if (params != null) {
            var oriParams = ticketSettingSubCategoryStore.getProxy().extraParams;
            ticketSettingSubCategoryStore.getProxy().extraParams = Ext.Object.merge(oriParams, params);
        }
        ticketSettingSubCategoryStore.load();

    },
   onEnterFilterHandler: function (el, e) {
        //keyword
        if (e.getKey() == e.ENTER) {
            this.filler();
        }
    },
    onFilterHandler: function () {
        this.filler();
    },


    filler: function () {
        var vm = this.getView().getViewModel();
        var query = vm.get('query') || null;
        var status = vm.get('status') || 'ACTIVE';
        this.loadData({ query: query, status: status});
    },

    onNavFilterChanged: function (field, value) {
        if (value) {
            field.getTrigger('clear').show();
        } else {
            field.getTrigger('clear').hide();
        }
    },

    onNavFilterClearTriggerClick: function (field, value) {
        field.getTrigger('clear').hide();
        var vm = this.getView().getViewModel();
        vm.set('query', null);
        vm.set('status', 'ACTIVE');
        this.filler();
    },
    onDblClickHandler: function( el , record , item , index , e , eOpts ) {
        this.onEditHandler(el, index);
    },
    onEditHandler: function (el, rowIndex, checked, obj) {
        var me = this;
        var record = el.getStore().getAt(rowIndex);
        console.log('test ', record.get('statusId'));
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.subcategory.TicketSubCategoryForm', {
            form: {
                'id': record.get('id'),
                'subCateName': record.get('subCateName'),
                'cateId': record.get('cateId'),
                'description': record.get('description'),
                'createdDate': record.get('createdDate'),
                'modifiedDate': record.get('modifiedDate'),
                cate: Ext.create('Workflow.model.ticket.TicketSettingSubCategory',{id: record.get('cateId')}),
                status: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('statusId')})
            }

        }, function () {
            me.getView().getStore().reload();
        });
    },
    onAddHander: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.subcategory.TicketSubCategoryForm', {
            form: null

        }, function () {
            me.getView().getStore().reload();
        });
    },
    showWindowDialog: function (lauchFromEl, windowClass, model, cb) {
        var content = Ext.create(windowClass, {
            viewModel: {
                data: model
            },
            cbFn: cb
        });
        var me = this;
        var w = new Ext.window.Window({
            rtl: false,
            modal: true,
            title: 'Ticket Sub Category Form',
            layout: 'fit',
            closable: true,
            iconCls: 'fa fa-object-ungroup',
            //collapsable: true,
            items: content,
            height: me.getView().getHeight(),
            width: 970,
            doClose: function () {
                w.hide(lauchFromEl, function () {
                    w.destroy();
                });
            }
        });
        w.show(lauchFromEl);
    }
});
