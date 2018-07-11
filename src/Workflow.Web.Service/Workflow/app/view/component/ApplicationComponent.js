Ext.define("Workflow.view.ApplicationComponent", {
    extend: "Workflow.view.AbstractRequestForm",
    xtype: 'application-component',
    header: {
        hidden: true
    },
    controller: "application-component",
    viewModel: {
        type: "component"
    },
    formConfig: {

    },
    getCopyData: function(){
        var data = null;
        if(window.location.hash && window.location.hash.split('/')[1]){
            var id = window.location.hash.split('/')[1].replace('clone=','');
            if(Ext.getCmp(id) != undefined){
                var copyData = Ext.getCmp(id).getViewModel().getData();
                if(copyData && copyData.record && copyData.record.data && copyData.record.data.dataItem){
                    data = copyData.record.data.dataItem;
                }
            }
        }
        return data;
    },
    getNewCollection: function (records) {
        var newItems = []
        Ext.each(records, function(data){
            data.id = null;
            newItems.push(data);
        })
        return newItems;
    },
    fireEventLoad: function(view, data){
        var me = this,
            viewSetting = me.currentActivityProperty,
            viewmodel = me.getViewModel();
        
        if(viewSetting && view.modelName){
            var propertyKey = view.modelName + 'Property';
            viewmodel.set(propertyKey, viewSetting[propertyKey]);
        }
        
        view.fireEvent('loadData', data, viewSetting);
    },
    buildComponent: function () {
        
    },
    afterTakeAction: function (responseObj) {
        
    },
    buildItems: function () {
        var me = this;
        return {
            xtype: 'panel',
            align: 'center',
            width: '100%',
            layout: {
                type: 'vbox',
                pack: 'start',
                align: 'stretch'
            },
            items: me.buildComponent()
        };
    },
    getOriginDataFromCollection: function (records) {
        var newItems = []
        if (records && records.length > 0) {
            for (var i in records) {
                newItems.push(records[i].data);
            };
        }
        return newItems;
    },
    getWorkflowFormConfig: function () {
        var me = this;
        var config = me.currentActivityProperty;
        return config;
    },
    getRequestItem: function (reference) {
        var me = this;
        return null;
    },
    renderSubForm: function (reference, data) {
        var me = this;
    }
});

