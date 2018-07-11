Ext.define('Workflow.view.dashboard.panels.TaskSubmittedController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.dashboard-tasksubmitted',
    onClearClickHandler: function (btn, e, eOpts) {
        var me = this;
        btn.setValue();
        me.loadStore('');
    },
    onViewAuditClicked: function (btn, e, opts) {
        var me = this;
        var vm = me.getViewModel();
        var selectedRow = vm.get('selectedRow');
        var data = selectedRow.getData();
        var dialog = Ext.create('Workflow.view.dashboard.AuditDialog', {
            mainController: me,
            title: Ext.String.format('Audit Log ( {0} )', data.folio),
            extraParam: {
                procInstId: data.processInstanceId
            }
        });
        dialog.show(btn);
    },
    onOpenFormAction: function (grid, rowIndex, colIndex) {
        //var rec = grid.getStore().getAt(rowIndex);
        var rec = grid.getSelection();
        rec = rec[0];
        var url = rec.get('formUrl');
        var noneK2 = rec.get('noneK2');
        //if (noneK2) {
            window.location.href = url;
        //} else {
        //    //window.open(url, '_black');
        //    var k2Url = encodeURIComponent(url);
        //    k2Url = k2Url.replace(/\./g, '__');
        //    window.location.href = "#k2/form/" + k2Url + "/" + rec.get('folio');
        //}
    },

    onOpenWorkFlowHandler: function (grid, rowIndex, colIndex) {
        var rec = grid.getStore().getAt(rowIndex);
        window.location.href = "#k2/flow/" + rec.get('processInstanceId') + "/" + rec.get('folio');
    },

    onSearchClickHandler: function (btn, e, eOpts) {
        var me = this;
        me.loadStore(e.field.value);
    },
    onSearchChangeHandler: function (field, value) {
        var me = this;

        if (value) {
            field.getTrigger('clear').show();
        } else {
            field.getTrigger('clear').hide();
            me.loadStore(field.value);
        }
    },
    onSearchKeypressHandler: function (field, e, eOpts) {
        var me = this;
        if (e.keyCode == '13') {
            me.loadStore(field.value);
        }
    },
    loadStore: function (query) {
        var me = this;
        var view = me.getView();
        var store = view.store;

        view.searchRegex = new RegExp('(' + query + ')', "gi");

        Ext.apply(store.getProxy().extraParams, {
            query: query
        });

        view.store.loadPage(1);
    },
    onRefreshClick: function (btn, e) {
        var me = this;
        var v = me.getView();
        var vm = v.getViewModel();

        vm.set('selectedRow', null);
        var r = me.getReferences();
        var searchText = r.searchTextBox;
        searchText.setValue();
        me.loadStore(null);
    }
});
