Ext.define('Workflow.view.rights.UserWindowController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.rights-userwindow',
    onCloseClick: function (btn, e) {
        var me = this;
        var v = me.getView();
        v.close();
    },
    onSaveClick: function (btn, e) {
        var me = this;
        var vm = me.getViewModel();
        var v = me.getView();
        var r = me.getReferences();

        var form = r.windowForm;

        if (form.isValid()) {
            var action = v.action;
            var employeeInfo = vm.get('employeeInfo');
            var active = vm.get('active');
            var desc = vm.get('desc') == null ? '' : vm.get('desc');

            var data = {
                description: desc,
                active: active,
                DeptApprovalRoleId: vm.get('darId'),
                fullName: employeeInfo.get('fullName')
            }

            if (v.action == 'add') {
                data.employeeId = employeeInfo.get('id');
                me.save(data, 'post');
            } else {
                data.id = vm.get('roleRightId');
                data.employeeId = employeeInfo.get('id');
                me.save(data, 'put');
            }
        }
    },
    save: function (data, method) {
        var me = this;
        var r = me.getReferences();
        var v = me.getView();
        var vm = me.getViewModel();

        var employeePickup = r.employeePickup;

        var main = v.mainController;
        var form = r.windowForm;
        if (form.isValid()) {
            Ext.Ajax.request({
                url: Workflow.global.Config.baseUrl + 'api/rights/user',
                method: method,
                headers: { 'Content-Type': 'application/json' },
                jsonData: data,
                success: function (response) {
                    main.onRefreshClick();
                    if (v.action == 'add') {
                        employeePickup.onClearClick();
                        form.reset(false);
                        vm.set('active', true);
                    } else {
                        v.close();
                    }
                },
                failure: function (response) {
                    var error = Ext.JSON.decode(response.responseText);
                    Ext.Msg.show({
                        title: 'Save a employee error!',
                        message: error.Message,
                        buttons: Ext.Msg.OK,
                        icon: Ext.MessageBox.ERROR
                    });
                }
            });
        }        
    }
});

