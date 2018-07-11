Ext.define('Workflow.view.common.worklists.SleepWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.common-worklists-sleepwindow',
    REQUEST: {
        SLEEP: Workflow.global.Config.baseUrl + 'api/worklists/sleep'
    },
    init: function () {
        var me = this;
        me.setOptionDisabled(false, true);
    },
    onOptionChange: function (field, newValue, oldValue) {
        var me = this;
        var cmbDay = me.getView().getReferences().cmbDay;
        var dfSleep = me.getView().getReferences().dfSleep;
        if (newValue.option == true) {
            me.setOptionDisabled(false, true);
        } else {
            me.setOptionDisabled(true, false);
        }
    },
    onOkClickHandler: function (btn, e, eOpts) {
        var me = this;
        var cmbDay = me.getView().getReferences().cmbDay;
        var dfSleep = me.getView().getReferences().dfSleep;

        var basic = me.getView().getReferences().optionRadio.getValue().option;

        var serialNumber = me.getView().serialNumber;
        var sharedUser = me.getView().sharedUser;
        var managedUser = me.getView().managedUser;

        var data = {
            basic: basic,
            serialNumber: serialNumber,
            sharedUser: sharedUser,
            managedUser: managedUser
        };

        if (basic == true) {
            data.duration = cmbDay.getValue();
            if (!data.duration) {
                Ext.MessageBox.alert('Blank Error', 'Basic can\'t blank.');
                return;
            }
        } else {
            var date = dfSleep.getValue();
            if (!date) {
                Ext.MessageBox.alert('Blank Error', 'Date can\'t blank.');
                return;
            }
            data.duration = date;
        }
        me.saveWorklistItemSleep(data);
        
    },
    setOptionDisabled: function (showCmbDay, showDfSleep) {
        var me = this;

        var cmbDay = me.getView().getReferences().cmbDay;
        var dfSleep = me.getView().getReferences().dfSleep;
        cmbDay.setDisabled(showCmbDay);
        dfSleep.setDisabled(showDfSleep);
    },
    saveWorklistItemSleep: function (data) {
        var me = this;
        Ext.Ajax.request(
            {
                url: me.REQUEST.SLEEP,
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                jsonData: data,
                success: function (response) {
                    me.getView().mainView.refreshWorklist();
                    me.closeWindow();
                },
                failure: function (response) {
                    var error = Ext.JSON.decode(response.responseText);
                    Workflow.global.ErrorMessageBox.show(error);
                }
            }
        );
    }
});

