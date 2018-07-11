Ext.define('Workflow.view.ticket.setting.sla.SlaPanelController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-setting-sla-sla',
    init: function () {
        // this.loadData();
    },
    loadData: function (params) {
        var me = this;
        var model = me.getView().getViewModel();

        var ticketSettingSlaStore = model.getStore('ticketSettingSlaStore');
        ticketSettingSlaStore.getProxy().extraParams = params;
        ticketSettingSlaStore.load();

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
        var me = this;
        me.onEditHandler(el, index);
    },
    renderToDate: function(secs){
        var obj = {day: 0, hour: 0, minute:0};
        var sec_num = parseInt(secs, 10);

        obj.day = Math.floor((secs/86400) % 60);
        var daySec = obj.day*24*3600;
        obj.hour  = Math.floor((sec_num -daySec)/3600);
        obj.minute = Math.floor((sec_num - (daySec+obj.hour*3600)) / 60);
        return obj;
    },
    renderDateStr: function(value){
        var obj = this.renderToDate(value);
        return obj.day+'d '+obj.hour+'h '+obj.minute+'m';
    },
    onEditHandler: function (el, rowIndex, checked, obj) {
        var me = this;
        var record = el.getStore().getAt(rowIndex);
        var resolutionTime = me.renderToDate(record.get('gracePeriod'));
        var responseTime = me.renderToDate(record.get('firstResponsePeriod'));

        me.showWindowDialog(el, 'Workflow.view.ticket.setting.sla.TicketSlaForm', {
            form: {
                'id': record.get('id'),
                'slaName': record.get('slaName'),
                'description': record.get('description'),
                'createdDate': record.get('createdDate'),
                'modifiedDate': record.get('modifiedDate'),
                'numDayResolution': Ext.create('Ext.data.Model', {id: resolutionTime.day}),
                'numHourResolution': Ext.create('Ext.data.Model', {id: resolutionTime.hour}),
                'numMinuteResolution': Ext.create('Ext.data.Model', {id: resolutionTime.minute}),
                'numDayResponse': Ext.create('Ext.data.Model', {id: responseTime.day}),
                'numHourResponse': Ext.create('Ext.data.Model', {id: responseTime.hour}),
                'numMinuteResponse': Ext.create('Ext.data.Model', {id: responseTime.minute})

            }

        }, function () {
            me.getView().getStore().reload();
        });
    },
    onAddHander: function (el, e, eOpts) {
        var me = this;
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.sla.TicketSlaForm', {
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
            title: 'Ticket SLA Form',
            layout: 'fit',
            closable: true,
            iconCls: 'fa fa-retweet',
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
    onSlaMappingHandler: function (el, e, eOpts) {
        var me = this;
        
        me.showWindowDialog(el, 'Workflow.view.ticket.setting.sla.SlaMappingPanel', {
            form: null

        }, function () {
            me.getView().getStore().reload();
        });
    }
});
