Ext.define('Workflow.view.roles.UserWindowController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.roles-userwindow',
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
            var include = vm.get('include');
            var roleName = vm.get('roleName');
            var isDbRole = vm.get('isDbRole');

            var data = {
                roleName: roleName,
                user: employeeInfo.get('loginName'),
                include: include,
                isDbRole: isDbRole
            }

            if (v.action == 'add') {
                data.empId = employeeInfo.get('id');
                me.save(data, 'post');
            } else {
                data.empId = vm.get('empId');
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

        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/roles/save',
            method: method,
            headers: { 'Content-Type': 'application/json' },
            jsonData: data,
            success: function (response) {
                main.onRefreshClick();
                if (v.action == 'add') {
                    employeePickup.onClearClick();
                    form.reset();
                    vm.set('include', true);
                } else {
                    v.close();
                }
            },
            failure: function (response) {
                var error = Ext.JSON.decode(response.responseText);
                Ext.Msg.show({
                    title: 'Save User Role error!',
                    message: error.Message,
                    buttons: Ext.Msg.OK,
                    icon: Ext.MessageBox.ERROR
                });
            }
        });
    }
});

