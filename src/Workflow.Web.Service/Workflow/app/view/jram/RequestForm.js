Ext.define("Workflow.view.jram.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'jram-request-form',
    title: 'Ram Clear Form',
    formType: 'JRAM_REQ',
	header: false,
    actionUrl: Workflow.global.Config.baseUrl + 'api/jramrequest',
    bindPayloadData: function (reference) {
        var me = this,
            ramClearReq = reference.ramClearReq,
            ramClearInst = reference.ramClearInst,
            vm = ramClearInst.getViewModel();

        var ramInstDt = ramClearInst.getViewModel().getData().ramClear,
            instArrStr = ramInstDt.instances;

		var instances = null;
		if(instArrStr && instArrStr.instances){
			instances = instArrStr.instances.toString();
		}
        var payload = {
            ramClear: Ext.Object.merge(
                ramClearReq.getViewModel().getData().ramClear,
                {
                    instances: instances,
                    descr: ramInstDt.descr
                })
        };

        return payload;
    },
    loadDataToView: function (reference, data, acl) {
        this.clearData(reference);
        
        // transform data
        if (data && data.ramClear) {
            data.ramClear.instances = {
                instances: data.ramClear.instances.split(',').map(Number)
            }

            data.ramClear.clearDate = new Date(data.ramClear.clearDate);
        }

        this.fireEventLoad(reference.ramClearReq, data);
        this.fireEventLoad(reference.ramClearInst, data);
    },
    clearData: function (reference) {
        reference.ramClearReq.getViewModel().set('ramClear', {});
        reference.ramClearInst.getViewModel().set('ramClear', {});
        reference.ramClearReq.reset();
        reference.ramClearInst.reset();
    },
    validateForm: function(reference, data){
        var ramClearReq = reference.ramClearReq;
        var ramClearInst = reference.ramClearInst;

        if (data) {
            var smg = "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            if (!(ramClearReq.isValid())) {
                return smg;
            }
           
            var ramInstDt = data.dataItem.ramClear;
            if (data.activity == 'Submission' && !ramInstDt.instances) {
                return smg;
            }
        }
    },
    buildComponent: function () {
        var me = this;
        me.customButtons = [{
            xtype: 'button',
            text: 'Check List',
            iconCls: 'fa fa-file-text-o',
            listeners: {
                click: function () {
                    window.open("/documents/jram/All%20Checklist%20samples.xlsx", "_blank")
                }
            }
        }];
       
        return [{
            xtype: 'panel',
            iconCls: 'fa fa-file-text-o',
            title: 'Ram Clear Request',
            xtype: 'jram-ramclear-request',
            reference: 'ramClearReq',
            margin: 5,
            border: true,
            mainView: me,
            bind: {
                hidden: '{ramClearProperty.hidden}'
            }
        }, {
            xtype: 'panel',
            iconCls: 'fa fa-file-text-o',
            title: 'Ram Clear Instances',
            xtype: 'jram-ramclear-instance',
            reference: 'ramClearInst',
            margin: 5,
            border: true,
            mainView: me,
            bind: {
                hidden: '{ramClearProperty.hidden}'
            }
        }];
    }
});