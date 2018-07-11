Ext.define("Workflow.view.gmu.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'gmu-request-form',
    title: 'GMU Ram Clear Form',
    formType: 'GMU_REQ',
    header: false,
    actionUrl: Workflow.global.Config.baseUrl + 'api/gmurequest',
    bindPayloadData: function (reference) {
        var me = this,
            gmuRamClearReq = reference.gmuRamClearReq;

        var ramInstDt = gmuRamClearReq.getViewModel().getData().gmuRamClear,
            instArrStr = ramInstDt.gmus;

        var data = gmuRamClearReq.getViewModel().getData().gmuRamClear;
        if (instArrStr && instArrStr.gmus) {
            data.gmus = instArrStr.gmus.toString();
        }

        var payload = { gmuRamClear: data };

        return payload;
    },
    loadDataToView: function (reference, data, acl) {
        this.clearData(reference);

        // transform data
        if (data && data.gmuRamClear) {
            data.gmuRamClear.gmus = {
                gmus: data.gmuRamClear.gmus.split(',').map(Number)
            }

            data.gmuRamClear.clearDate = new Date(data.gmuRamClear.clearDate);
        }

        this.fireEventLoad(reference.gmuRamClearReq, data);
    },
    clearData: function (reference) {
        reference.gmuRamClearReq.getViewModel().set('gmuRamClear', {
            disabledMacAddress: true,
            disabledDescr: true
        });
        reference.gmuRamClearReq.reset();
    },
    validateForm: function (reference, data) {
        var gmuRamClearReq = reference.gmuRamClearReq;
        

        if (data) {
            var smg = "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            if (!(gmuRamClearReq.isValid())) {
                return smg;
            }
            
            var ramInstDt = data.dataItem.gmuRamClear;
            if(data.activity == 'Submission' && (!ramInstDt.gmus || ramInstDt.gmus.toString() == "[object Object]")){
                return smg;
            }
        }
    },
    buildComponent: function () {
        var me = this;
        //me.customButtons = [{
        //    xtype: 'button',
        //    text: 'Check List',
        //    iconCls: 'fa fa-file-text-o',
        //    listeners: {
        //        click: function () {
        //            window.open("/documents/", "_blank")
        //        }
        //    }
        //}];

        return [{
            xtype: 'panel',
            iconCls: 'fa fa-file-text-o',
            title: 'GMU',
            xtype: 'gmu-ramclear-request',
            reference: 'gmuRamClearReq',
            margin: 0,
            border: true,
            mainView: me
        }];
    }
});