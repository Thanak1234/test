Ext.define("Workflow.view.common.GenericForm", {
    extend: "Workflow.view.AbstractRequestForm",
    name: 'AE9D5A6A-FF72-44DE-BD35-ADA86DF03F0D',
    header: {
        hidden: true
    },
    controller: "genericform",
    viewModel: {
        type: "genericform"
    },
    buildItems: function () {
        var me = this;
        return {
            xtype: 'form',
            align: 'center',
            reference: 'container',
            width: '100%',
            layout: {
                type: 'vbox',
                pack: 'start',
                align: 'stretch'
            },
            items: me.buildComponents()
        };
    },
    buildComponents: function () {
        return [];
    },
    buildConfigs: function (config) {
        return config;
    },
    getWorkflowFormConfig: function () {
        var me = this;
        var viewmodel = me.getViewModel();
        var data = viewmodel.getData();
        var url = me.actionUrl + '/config?req=' + me.formType + '&activity=' + data.activity;
        Ext.Ajax.request({
            url: url,
            async: false,
            method: 'GET',
            headers: { 'Content-Type': 'application/json' },
            success: function (response, operation) {
                var config = Ext.JSON.decode(response.responseText);
                var viewSetting = config;
                if (Ext.isFunction(me.buildConfigs) && config) {
                    var lastAct = data.record? data.record.get('lastActivity'): '';
                    viewSetting = me.buildConfigs(data.activity, lastAct, config);
                }
                viewmodel.set('viewSetting', viewSetting);
            },
            failure: function (data, operation) {
                Ext.MessageBox.show({
                    title: 'Error',
                    msg: response.responseText,
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
            }
        });
        return viewmodel.get('viewSetting');
    },
    getData: function (records, excludeId) {
        var newItems = []
        if (records && records.length > 0) {
            for (var i in records) {
                var data = records[i].data;
                if (data && excludeId) {
                    delete data.Id;
                    delete data.id;
                }
                newItems.push(data);
            };
        }
        return newItems;
    },
    dowloadFile: function (url) {
        var me = this;
        window.open(url);
        //Ext.core.DomHelper.append(document.body, {
        //    tag: 'iframe',
        //    id: 'filedownload_' + me.generateUUID(),
        //    frameBorder: 0,
        //    width: 0,
        //    height: 0,
        //    src: link
        //});
    },
    generateUUID: function () {
        var d = new Date().getTime();
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function(c) {
            var r = (d + Math.random()*16)%16 | 0;
            d = Math.floor(d/16);
            return (c=='x' ? r : (r&0x3|0x8)).toString(16);
        });
        return uuid;
    },
    getOriginDataFromCollection: function (records) {
        var newItems = []
        if (records && records.length > 0) {
            Ext.each(records, function (record) {
                if (record.data) {
                    if (isNaN(parseInt(record.data.id))) {
                        record.data.id = 0;
                    }
                    newItems.push(record.data);
                }
            });
        }
        return newItems;
    }
});
