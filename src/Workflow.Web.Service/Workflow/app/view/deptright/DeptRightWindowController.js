Ext.define('Workflow.view.deptright.DeptRightWindowController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.deptrightwindow',
    onOkClick: function (btn, e) {
        var me = this;
        var v = me.getView();
        v.close();
    },
    onRefreshClick: function (btn, e, opts) {
        var me = this;
        var r = me.getReferences();
        var grid = r.grid;
        var store = grid.getStore();
        store.reload();
    },
    onRemoveClick: function (btn, e, opts) {
        var me = this;
        var r = me.getReferences();
        var vm = me.getViewModel();
        var v = me.getView();
        var g = r.grid;
        var records = g.getSelection();

        if (records && records.length > 0) {

            var data = me.getArray(records);

            Ext.Msg.show({
                title: 'Remove Confirm',
                message: 'Are you sure to remove this user from these Role Rights?',
                buttons: Ext.Msg.YESNO,
                icon: Ext.Msg.QUESTION,
                fn: function (btn) {
                    if (btn === 'yes') {
                        Ext.Ajax.request({
                            url: Workflow.global.Config.baseUrl + 'api/rights/deleteRoleRights',
                            method: 'post',
                            headers: { 'Content-Type': 'application/json' },
                            jsonData: data,
                            success: function (response) {                                
                                me.onRefreshClick();
                                v.mainController.onRefreshClick();
                            },
                            failure: function (response) {
                                var error = Ext.JSON.decode(response.responseText);
                                Ext.Msg.show({
                                    title: 'Error!',
                                    message: error.Message,
                                    buttons: Ext.Msg.OK,
                                    icon: Ext.MessageBox.ERROR
                                });
                            }
                        });
                    }
                }
            });

        }
    },
    getArray: function (records) {
        var result = [];
        for (var i = 0; i < records.length; i++) {
            var data = records[i].data;
            result.push(data);
        }
        return result;
    }
});

