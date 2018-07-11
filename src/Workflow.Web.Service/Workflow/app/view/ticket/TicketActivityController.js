/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.TicketActivityController', {
    extend: 'Workflow.view.AbstractBaseController',

    init: function () {


    },

    openActivityWindow: function (el, actCode, ticket, cbFn) {
        var me = this;
        var actConfig = me.getActivityConfig(actCode);
        actConfig.iconCls = el.iconCls;
        
        var model = { actIdentifier: actCode, ticket: ticket, description: null, height: actConfig.height };

        if ('VIEW_ATTACHMENT' === actCode) {
            var vm = me.getRef().vm;
            var fileList = vm.get('ticket.fileUpload');
            me.showAttachmentWindow(el, fileList, true);
        } else if ('EDIT_TICKET_INFO' === actCode) {
            //me.showTicket(bt, model, actConfig, function () { me.refreshData(ticketId); });
            me.showTicket(el, model, actConfig, cbFn);
        } else if ('SUB_TICKET_POSTING' === actCode) {            
            el.title = Ext.String.format('{0}, Ticket #{1}', 'Subticket Form', ticket.data.ticketNo);
            me.createTicketDialog({el: el, by: 'AGENT', actCode : actCode, mainTicket : ticket }, cbFn)
        } else {
            //this.showWindowDialog(bt, model, actConfig, function () { me.refreshData(ticketId); });            
            actConfig.title = Ext.String.format('{0}, Ticket #{1}', actConfig.title, model.ticket.data.ticketNo);
            this.showWindowDialog(el, model, actConfig, cbFn);
        }
    },

    getActivityConfig: function (itemName) {
        var css = 'Workflow.view.ticket.activity.Activity';
        var title = 'Ticket Activity';
        var height = 600;

        if ('POST_REPLY' === itemName) {
            title = 'Post Reply';
        } else if ('TICKET_ASSIGNED' === itemName) {
            title = 'Ticket Assignment';
            css = 'Workflow.view.ticket.activity.AssignedActivity';
        } else if ('POST_INTERNAL_NOTE' === itemName) {
            title = 'Post Internal Note';
        } else if ('POST_PUBLIC_NOTE' === itemName) {
            title = 'Post Public Note';
        } else if ('CHANGE_STATUS' === itemName) {
            css = 'Workflow.view.ticket.activity.StatusActivity';
            title = 'Change Status';
        } else if ('DELETE_TICKET' === itemName) {
            title = 'Remove Ticket';
        } else if ('EDIT_TICKET_INFO' == itemName) {
            css = 'Workflow.view.ticket.TicketForm';
            title = 'Edit Ticket';
        } else if ('MERGE_TICKET' == itemName) {
            css = 'Workflow.view.ticket.activity.MergedActivity';
            title = 'Merged Ticket';
        } else {
            title = 'Ticket Activity';
        }

        return {
            css: css,
            title: title,
            height: height
        }
    },

    showAttachmentWindow: function (el, files, isAll) {
        var me = this;
        var window = Ext.create('Workflow.view.common.fileUpload.AttachmentFileView',
            {
                mainView: me,
                lauchFrom: el,
                viewModel: { data: { attachedFiles: files, isAll: isAll } }
            });

        window.show(el);
    },

    showTicket: function (el, model, config  , cb) {
        var me = this;
        var ticketId = model.ticket.getId();
        model.createdBy = 'Agent';
        Ext.getBody().mask("Opening...");
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket',
            params: { by: model.createdBy, ticketId:ticketId  },
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            success: function (response) {
                var data = Ext.JSON.decode(response.responseText);
                model.data = data;
                Ext.getBody().unmask();
                me.showTicketWindowDialog(el, config.css, model, function () {
                    if (cb) {
                        cb();
                    }
                });
            },
            failure: function (opt, operation) {
                Ext.getBody().unmask();
                var response = Ext.decode(opt.responseText);
                Ext.MessageBox.show({
                    title: 'Error',
                    msg: response.Message,
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
            }
        });
    },

    showTicketWindowDialog: function (lauchFromEl, windowClass, model, cb) {
        var content = Ext.create(windowClass, {
            viewModel: {
                data: model
            },
            cbFn: cb
        });

        var me = this,
            ticketNo = model.ticket.data.ticketNo
            ;
        ticketNo = ticketNo? ', Ticket #'+ticketNo : '';
        
        var w = new Ext.window.Window({
            rtl: false,
            modal: true,
            title: 'Ticket Form' + ticketNo,
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
            },
            iconCls: lauchFromEl.iconCls
        });
        Ext.getBody().unmask();
        w.show(lauchFromEl);

    },

    showWindowDialog: function (lauchFromEl, model, actConfig, cb) {
        var me = this;
        var w = Ext.create(actConfig.css,
         {
             title: actConfig.title,
             mainView: me,
             height: actConfig.height,
             viewModel: {
                 data: model
             },
             lauchFrom: lauchFromEl,
             cbFn: cb,
             doClose: function (refresh) {
                 w.hide(lauchFromEl, function () {
                     w.destroy();
                 });
             },
             iconCls: actConfig.iconCls
         });
        w.show(lauchFromEl);
    },

    createTicketDialog: function ( config,cbFn) {
        var me = this;
        Ext.getBody().mask("Opening...");
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/ticket',
            headers: { 'Content-Type': 'application/json' },
            params: { by: config.by || 'USER' },
            method: 'GET',
            success: function (response) {
                Ext.getBody().unmask();
                var data = Ext.JSON.decode(response.responseText);
                Ext.apply(data,  {refs : config } );
                me.showCreateTicketWindowDialog(config.el, 'Workflow.view.ticket.TicketForm', {
                    createdBy: config.by || 'USER',
                    data: data
                }, function () {
                    Ext.getBody().unmask();
                    if(cbFn){
                        cbFn();
                    }
                });
            },
            failure: function (opt, operation) {
                Ext.getBody().unmask();

                var response = Ext.decode(opt.responseText);
                Ext.MessageBox.show({
                    title: 'Error',
                    msg: response.Message,
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
            }
        });
    },
    
    showCreateTicketWindowDialog: function (lauchFromEl, windowClass, model, cb) {
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
            title: (lauchFromEl.title ? lauchFromEl.title: 'Ticket Form'),
            layout: 'fit',
            closable: true,
            //collapsable: true,
            items: content,
            height: me.getView().getHeight(),
            width: 970,
            doClose: function () {
                w.hide(lauchFromEl, function () {
                    w.destroy();
                });
            },
            iconCls: lauchFromEl.iconCls
        });

        w.show(lauchFromEl);
    }
});