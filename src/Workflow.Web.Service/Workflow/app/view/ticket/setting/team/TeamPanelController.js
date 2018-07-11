Ext.define('Workflow.view.ticket.setting.team.TeamPanelController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-team-team',
    init: function () {
        // this.loadData();
    },
    loadData: function (params) {
        var me = this;
        var model = me.getView().getViewModel();

        var ticketSettingTeamStore = model.getStore('ticketSettingTeamStore');
        if (params != null) {
            var oriParams = ticketSettingTeamStore.getProxy().extraParams;
            ticketSettingTeamStore.getProxy().extraParams = Ext.Object.merge(oriParams, params);
        }
        ticketSettingTeamStore.load();

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

        me.showWindowDialog(el, 'Workflow.view.ticket.setting.team.TicketTeamForm', {
            form: {
                'id': record.get('id'),
                'teamName': record.get('teamName'),
                'alertAllMembers': record.get('alertAllMembers'),
                'alertAssignedAgent': record.get('alertAssignedAgent'),
                'directoryListing': record.get('directoryListing'),
                'description': record.get('description'),
                'createdDate': record.get('createdDate'),
                'modifiedDate': record.get('modifiedDate'),
                status: Ext.create('Workflow.model.ticket.TicketLookup',{id: record.get('statusId')})
            }

        }, function () {
            me.getView().getStore().reload();
        });
    },
    onAddHandler: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.team.TicketTeamForm', {
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
            title: 'Ticket team Form',
            layout: 'fit',
            closable: true,
            //collapsable: true,
            iconCls: 'fa fa-qrcode',
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
