Ext.define('Workflow.view.ticket.report.ReportViewController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.ticket-report-reportview',
    init: function () {
        var me = this,
            model = me.getViewModel(),
            ticketSubCateSore = model.getStore('ticketSubCateStore'),
            ticketItemStore = model.getStore('ticketItemStore'),
            ticketAgentSore = model.getStore('ticketAgentStore'),
            ticketSlaStore = model.getStore('ticketSlaStore'),
            ticketStatusStore = model.getStore('ticketStatusStore')
            ;
        ticketAgentSore.load();

        ticketSubCateSore.getProxy().extraParams = { breadcrumb: true };
        ticketSubCateSore.load();        

        ticketItemStore.getProxy().extraParams = { breadcrumb: true };
        ticketItemStore.load();

        //ticketSlaStore.getProxy().extraParams = { typeId: 0, priorityId: 0};
        ticketSlaStore.load();

        ticketStatusStore.getProxy().extraParams = { includeRemoved: false };
        ticketStatusStore.load();
    },
    deptStore: null,
    onClearClick: function(el) {
        var me = this;
        var vm = this.getView().getViewModel();
        var refs = this.getReferences();
        vm.set('form.depts', null);
        vm.set('deptDisplay', null);
        me.deptStore = null;
    },
    onEditClick: function (el) {
        var me = this;
        var vm = me.getView().getViewModel();

        var store = null;

        if (me.deptStore) {
            store = me.clone(me.deptStore);
        }

        var win = Ext.create('Workflow.view.ticket.report.DeptsWin', {
            main: me,
            deptStore: store
        });

        win.show(el, function () {
        });
    },
    setDepts: function (store) {
        var me = this;
        var vm = this.getView().getViewModel();
        var refs = this.getReferences();
        if (store) {
            var records = store.getRange();
            vm.set('form.depts', me.getDeptIds(records));
            vm.set('deptDisplay', me.getDisplayText(records));
            me.deptStore = store;
        }
    },
    clone: function (source) {
        var records = [];
        source.each(function (r) {
            records.push(r.copy());
        });
        var destination = new Ext.data.Store({
            recordType: source.recordType
        });
        destination.add(records);
        return destination;
    },
    getDisplayText: function (records) {
        var display = '';
        Ext.each(records, function (rec) {
            display += rec.get('fullName') + ', ';
        });

        if (!Ext.isEmpty(display)) {
            display = display.substr(0, display.length - 2);
        }

        return display;
    },
    getDeptIds: function (records) {
        var deptIds = '';
        Ext.each(records, function (rec) {
            if (rec.get('id')) {
                deptIds += Ext.String.format('{0},', rec.get('id'));
            }
        });
        return deptIds.length > 0 ? deptIds.substr(0, deptIds.length - 1) : deptIds;
    },
    onSearchHanlder: function () {
        var me = this,
            view = me.getView(),
            vm = view.getViewModel(),
            r = me.getReferences();

        var store = r.gd.getStore();
        var criteria = vm.get('form');
        store.proxy.extraParams = criteria;
        store.loadPage(1);
    },
    onClearHanlder: function (el, eOpts) {
        var viewmodel = this.getView().getViewModel();
        var form = this.getView().form;
        form.reset();
        viewmodel.set('deptDisplay', null);
        viewmodel.set('showDept', false);
        viewmodel.set('showEmp', true);
        this.deptStore = null;
        viewmodel.set('form.depts', null);
    },
    onExportPdf: function () {
        this.exportXLSPDF('pdf');
    },
    onExportExcel: function () {
        this.exportXLSPDF('xls');
    },
    exportXLSPDF: function (type) {
        var me = this;
        var viewmodel = me.getView().getViewModel();
        var refs = me.getReferences();
        var extraParams = viewmodel.get('form');
        extraParams.exportType = type;
        window.location.href = Workflow.global.Config.baseUrl + 'api/ticketrpt/export/?' + me.serialize(extraParams);
    },
    serialize: function (obj) {
        var str = [];
        for (var p in obj) {
            if (obj.hasOwnProperty(p)) {
                var value = obj[p];
                if (Object.prototype.toString.call(obj[p]) === '[object Date]') {
                    value = Ext.util.Format.date(obj[p], 'Y-m-d');
                }
                str.push(encodeURIComponent(p) + "=" + encodeURIComponent(value));
            }
        }
        return str.join("&");
    },
    onTeamChanged: function (el, newValue, oldValue, eOpts) {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = me.getReferences();
        refs.assignee.clearValue();
        var ticketAgentSore = model.getStore('ticketAgentStore');
        var teamId = newValue;
        if (teamId == null) {
            model.set('disable.assignee', true);
        } else {
            model.set('disable.assignee', false);
            ticketAgentSore.getProxy().extraParams = { teamId: teamId };
            ticketAgentSore.load();
        }
    },
    onCateChanged: function (el, newValue, oldValue, eOpts) {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var cateId = newValue;
        var ticketSubCateSore = model.getStore('ticketSubCateStore');
        var ticketItemStore = model.getStore('ticketItemStore');
        me.clear(true, true)
        if (cateId) {
            ticketSubCateSore.getProxy().extraParams = { cateId: cateId, breadcrumb: true };
            ticketSubCateSore.load();

            ticketItemStore.getProxy().extraParams = { cateId: cateId, breadcrumb: true };
            ticketItemStore.load();
            //me.disable(false, true)
        } else {
            ticketSubCateSore.setData([]);
            ticketItemStore.setData([]);
            //me.disable(true, true);
        }
    },
    onSubCateChanged: function (self, newValue, oldValue, eOpts) {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var subCateId = newValue;
        var ticketItemStore = model.getStore('ticketItemStore');
        me.clear(false, true)
        if (subCateId) {
            //me.disable(false, false)
            ticketItemStore.getProxy().extraParams = { subCateId: subCateId, breadcrumb:true };
            ticketItemStore.load();
        } else {
            //me.disable(false, true)
            model.set('form.subCateId', null);
            ticketItemStore.setData([]);
        }
    },
    disable: function (subCate, item) {
        var viewmodel = this.getView().getViewModel();
        viewmodel.set('disable.subCate', subCate);
        viewmodel.set('disable.item', item);
    },
    clear: function (subCate, item) {
        var refs = this.getReferences();

        if (subCate)
            refs.subcate.clearValue();

        if (item)
            refs.item.clearValue();
    },
    onTicketTypeChanged: function (self, newValue, oldValue, eOpts) {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var ticketSlaStore = model.getStore('ticketSlaStore');
        var selectedValue = newValue.length > 0 ? newValue : 0;
        
        ticketSlaStore.getProxy().extraParams = Ext.Object.merge(ticketSlaStore.getProxy().extraParams, { typeId: newValue }); 
        ticketSlaStore.load();
    },
    onPriorityChanged: function (self, newValue, oldValue, eOpts) {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var ticketSlaStore = model.getStore('ticketSlaStore');
        var selectedItems = self.valueCollection.items;
        var selectedValues = [];

        selectedItems.forEach(function (element, index, array) {
            selectedValues.push(element.id);
        });
        
        ticketSlaStore.getProxy().extraParams = Ext.Object.merge(ticketSlaStore.getProxy().extraParams, { priorityId: selectedValues});
        ticketSlaStore.load();
    },
    onIncludeRemovedChecked: function (elm, newValue, oldValue, eOpts) {
        var me = this;
        var view = me.getView();
        var model = view.getViewModel();
        var refs = this.getReferences();'ticketStatusCombo'
        var ticketStatusStore = model.getStore('ticketStatusStore');

        refs.ticketStatusCombo.reset();

        ticketStatusStore.getProxy().extraParams = { includeRemoved: newValue };
        ticketStatusStore.load();
    }
});
