Ext.define('Workflow.view.ticket.setting.grouppolicy.GroupPolicyPanelController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-grouppolicy-grouppolicy',
    init: function () {
        // this.loadData();
    },
    loadData: function (params) {
        var me = this;
        var model = me.getView().getViewModel();

        var ticketSettingGroupPolicyStore = model.getStore('ticketSettingGroupPolicyStore');
        ticketSettingGroupPolicyStore.getProxy().extraParams = params;
        ticketSettingGroupPolicyStore.load();

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
        var me = this, view=me.getView(), vm = view.getViewModel();
        var record = el.getStore().getAt(rowIndex);
        var id = record.get('id');
        var limitAccessId = record.get('limitAccessId');
        var reportAccessId = record.get('reportAccessId');
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.grouppolicy.TicketGroupPolicyForm', {
            form: {
                'id': id,
                'groupName': record.get('groupName'),
                'createTicket': record.get('createTicket'),
                'editTicket': record.get('editTicket'),
                'editRequestor': record.get('editRequestor'),
                'postTicket': record.get('postTicket'),
                'closeTicket': record.get('closeTicket'),
                'assignTicket': record.get('assignTicket'),
                'mergeTicket': record.get('mergeTicket'),
                'deleteTicket': record.get('deleteTicket'),
                'deptTransfer': record.get('deptTransfer'),
                'changeStatus': record.get('changeStatus'),
                'description': record.get('description'),
                'createdDate': record.get('createdDate'),
                'modifiedDate': record.get('modifiedDate'),
                'subTicket': record.get('subTicket'),
                status: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('statusId')}),
                limitAccess: Ext.create('Workflow.model.ticket.TicketLookup',{id: limitAccessId}),
                newTicketNotify: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('newTicketNotifyId')}),
                assignedNotify: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('assignedNotifyId')}),
                replyNotify: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('replyNotifyId')}),
                changeStatusNotify: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('changeStatusNotifyId')}),
                reportLimitAccess: Ext.create('Workflow.model.ticket.TicketLookup',{id: reportAccessId})
            },
            limitAccessId : limitAccessId,
            reportAccessId : reportAccessId

        }, function () {
            me.getView().getStore().reload();
        });
    },
    onAddHandler: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.grouppolicy.TicketGroupPolicyForm', {
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
            title: 'Ticket Group Policy Form',
            layout: 'fit',
            closable: true,
            iconCls: 'fa fa-gg',
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
