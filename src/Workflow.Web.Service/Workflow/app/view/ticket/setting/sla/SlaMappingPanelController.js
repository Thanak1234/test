Ext.define('Workflow.view.ticket.setting.sla.SlaMappingPanelController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.ticket-setting-sla-slamappingpanelcontroller',
    onCloseClick: function (btn, e) {
        var me = this;
        var v = me.getView();
        v.close();
    },
    init: function(){
        var me = this;
        var model = me.getView().getViewModel();
    },
    onAddSlaMappingHandler: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.sla.TicketSlaMappingForm', {
            form: null

        }, function () {
            me.getView().getStore().reload();
        });
    },
    onDblClickHandler: function (el, record, item, index, e, eOpts) {
        var me = this;
        me.onEditHandler(el, index);
    },
    onEditHandler: function (el, rowIndex, checked, obj) {
        var me = this;
        var record = el.getStore().getAt(rowIndex);
        
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.sla.TicketSlaMappingForm', {
            form: {
                'id': record.get('id'),
                'sla': Ext.create('Workflow.model.ticket.TicketSla', { id: record.get('slaId') }),
                'priority': Ext.create('Workflow.model.ticket.TicketPriority', { id: record.get('priorityId') }),
                'type': Ext.create('Workflow.model.ticket.TicketType', { id: record.get('typeId') }),
                'description': record.get('description'),
                'createdDate': record.get('createdDate'),
                'modifiedDate': record.get('modifiedDate')
            }

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
            title: 'Ticket SLA Mapping Form',
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
    },    
    onWindowClosedHandler: function () {
        var me = this,
            w = me.getView().up();
        w.close();

    }
});

