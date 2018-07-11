Ext.define('Workflow.view.ticket.setting.priority.PriorityPanelController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-priority-priority',
    init: function () {
        // this.loadData();
    },
    loadData: function (params) {
        var me = this;
        var model = me.getView().getViewModel();

        var ticketSettingPriorityStore = model.getStore('ticketSettingPriorityStore');
        ticketSettingPriorityStore.getProxy().extraParams = params;
        ticketSettingPriorityStore.load();

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
    onDblClickHandler: function( el , record , item , index , e , eOpts ) {
        this.onEditHandler(el, index);
    },
    onEditHandler: function (el, rowIndex, checked, obj) {
        var me = this;
        var record = el.getStore().getAt(rowIndex);
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.priority.TicketPriorityForm', {
            form: {
                'id': record.get('id'),
                'priorityName': record.get('priorityName'),
                'description': record.get('description'),
                'createdDate': record.get('createdDate'),
                'modifiedDate': record.get('modifiedDate'),
                'sla': Ext.create('Workflow.model.ticket.TicketSla', {id: record.get('slaId')})
            }

        }, function () {
            me.getView().getStore().reload();
        });
    },
    onAddHander: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.priority.TicketPriorityForm', {
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
            title: 'Ticket Priority Form',
            layout: 'fit',
            closable: true,
            iconCls: 'fa fa-sort-amount-desc',
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
