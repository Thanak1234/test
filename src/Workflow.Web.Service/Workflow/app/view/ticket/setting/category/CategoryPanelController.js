Ext.define('Workflow.view.ticket.setting.category.CategoryPanelController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-category-category',
    init: function () {
        // this.loadData();
    },
    loadData: function (params) {
        var me = this;
        var model = me.getView().getViewModel();

        var ticketSettingCategoryStore = model.getStore('ticketSettingCategoryStore');
        if (params != null) {
            var oriParams = ticketSettingCategoryStore.getProxy().extraParams;
            ticketSettingCategoryStore.getProxy().extraParams = Ext.Object.merge(oriParams, params);
        }        
        ticketSettingCategoryStore.load();

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
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.category.TicketCategoryForm', {
            form: {
                'id': record.get('id'),
                'cateName': record.get('cateName'),
                'deptId': record.get('deptId'),
                'description': record.get('description'),
                'createdDate': record.get('createdDate'),
                'modifiedDate': record.get('modifiedDate'),
                dept: Ext.create('Workflow.model.ticket.TicketDepartment',{id: record.get('deptId')}),
                status: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('statusId')})
            }

        }, function () {
            me.getView().getStore().reload();
        });
    },
    onAddHander: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.category.TicketCategoryForm', {
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
            title: 'Ticket Category Form',
            layout: 'fit',
            closable: true,
            iconCls: 'fa fa-object-group',
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
