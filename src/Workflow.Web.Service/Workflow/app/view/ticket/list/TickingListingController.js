/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.list.TickingListingController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.ticket-list-tickinglisting',

    init: function () {
        this.refreshData();
    },



    loadTckets: function(fn){
        Ext.getBody().mask("Opening...");
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket/listing',
            headers: { 'Content-Type': 'application/json' },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                
                if (fn) {
                    fn(data);
                }
                Ext.getBody().unmask();

            },
            failure: function (opt, operation) {
                Ext.getBody().unmask();
            }
        });
    },


    itemcontextmenu : function(){
        console.log('test');
    },
    onTicketClassifyClick: function (el, e, eOpts) {
        var ticketFilterButton = this.getView().getReferences().ticketFilterButton;
        var menus = ticketFilterButton.getMenu().items;
        menus.items.forEach(function (item) {
            if (item === el) {
                el.setIconCls('fa fa-check')
            } else {
                item.setIconCls('')
            }
        });

        ticketFilterButton.setText(el.text);

    },
    onNextTicketByUserActionListener: function (el, e, eOpts) {
        this.createTicket(el, 'user');
    },
    onNewTicketActionListener: function (el, e, eOpts) {
        this.createTicket(el, el.getItemId());
        
    },

    onTicketActionHandler: function (item, value) {
        if (value === 'OPEN')
            window.location.href = "#ticket/" + item.get('Id');        
    },
    createTicket: function (el, by) {
        var me = this;
        Ext.getBody().mask("Opening...");
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket',
            headers: { 'Content-Type': 'application/json' },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                me.showWindowDialog(el, 'Workflow.view.ticket.TicketForm', {
                    createdBy: by,
                    data: data,
                    fn: function () {
                        me.refreshData();
                    }
                }, function () {
                    Ext.getBody().unmask();
                });
            },
            failure: function (opt, operation) {
                Ext.getBody().unmask();

                var response = Ext.decode(operation.error.response.responseText);
                Ext.MessageBox.show({
                    title: 'Error',
                    msg: response.Message,
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
            }
        });
    },
    refreshData : function(){
        var me = this;
        var model = me.getView().getViewModel();
        var refs = me.getView().getReferences();
        var ticketListingStore = model.getStore('ticketListingStore');

        this.loadTckets(function (data) {
            ticketListingStore.setData(data);
            me.getView().setStore(ticketListingStore);
        });
    },
    showWindowDialog: function (lauchFromEl, windowClass, model, cb) {
        var content = Ext.create(windowClass, {
            viewModel: {
                data: { formData: model }
            }
        });

        var me = this;
       
        var w = new Ext.window.Window({
            rtl: false,
            modal: true,
            title: 'Ticket Form',
            layout: 'fit',
            closable: true,
            //collapsable: true,
            items: content,
            height: me.getView().getHeight(),
            width: 970,
            doClose: function (refresh) {
                w.hide(lauchFromEl, function () {
                    w.destroy();
                });
            }
        });

        w.show(lauchFromEl);
        if (cb) cb();
    }
});
