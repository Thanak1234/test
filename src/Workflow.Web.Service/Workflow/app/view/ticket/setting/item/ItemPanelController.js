Ext.define('Workflow.view.ticket.setting.item.ItemPanelController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-item-item',
    init: function () {
        // this.loadData();
    },
    loadData: function (params) {
        var me = this;
        var model = me.getView().getViewModel();

        var ticketSettingItemStore = model.getStore('ticketSettingItemStore');
        if (params != null) {
            var oriParams = ticketSettingItemStore.getProxy().extraParams;
            ticketSettingItemStore.getProxy().extraParams = Ext.Object.merge(oriParams, params);
        }
        ticketSettingItemStore.load();

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
        this.loadData({ query: query});
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
        this.filler();
    },
    onItemDblClickHandler: function( el , record , item , index , e , eOpts ) {
        this.onItemEditHandler(el, index);
    },
    onItemEditHandler: function (el, rowIndex, checked, obj) {
        var me = this, vm = me.getView().getViewModel();
        var record = el.getStore().getAt(rowIndex);
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.item.TicketItemForm', {

            form: {
                'id': record.get('id'),
                'itemName': record.get('itemName'),
                'slaId': record.get('slaId'),
                'description': record.get('description'),
                'createdDate': record.get('createdDate'),
                'modifiedDate': record.get('modifiedDate'),
                team: Ext.create('Workflow.model.ticket.TicketTeam',{id: record.get('teamId')}),
                subCate: Ext.create('Workflow.model.common.GeneralLookup', {id: record.get('subCateId')}),
                status: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('statusId')})
            }

        }, function () {
            me.getView().getStore().reload();
        });
    },
    onItemAddHander: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.item.TicketItemForm', {
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
            title: 'Ticket Item Form',
            layout: 'fit',
            closable: true,
            iconCls: 'fa fa-plug',
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
