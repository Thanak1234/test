Ext.define("Workflow.view.iteirq.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'iteirq-request-form',
    title: 'Internet Bandwidth Request For Event',
    formType: 'IBR_REQ',
	header: false,
    actionUrl: Workflow.global.Config.baseUrl + 'api/iteirqrequest',
    bindPayloadData: function (reference) {
        var me = this,
            eventInternet = reference.eventInternet,
            quotation = reference.quotation;

        var eventInternetData = eventInternet.getViewModel().getData().eventInternet,
            quotationstore = quotation.getStore();

        eventInternetData.comment = reference.itComment.getValue();
        return {
            eventInternet: eventInternetData, //Ext.Object.merge(obj1, obj2),
            // asset disposal details
            addQuotations: me.getOriginDataFromCollection(quotationstore.getNewRecords()),
            editQuotations: me.getOriginDataFromCollection(quotationstore.getUpdatedRecords()),
            delQuotations: me.getOriginDataFromCollection(quotationstore.getRemovedRecords())
        };
    },
    loadDataToView: function (reference, data, acl) {
        this.clearData(reference);

        var me = this,
            viewmodel = me.getViewModel(),
            viewSetting = me.currentActivityProperty;
        
        if (data && data.eventInternet) {
            data.eventInternet.startDate = new Date(data.eventInternet.startDate);
            data.eventInternet.endDate = new Date(data.eventInternet.endDate);
            reference.itComment.setValue(data.eventInternet.comment);
        }
        

        this.fireEventLoad(reference.eventInternet, data);
        this.fireEventLoad(reference.quotation, data);
        
    },
    clearData: function (reference) {
        reference.eventInternet.getViewModel().set('eventInternet', {});
        reference.eventInternet.reset();
        reference.quotation.fireEvent('onDataClear');
        reference.itComment.reset();
    },
    validateForm: function(reference, data){
        var me = this,
            eventInternet = reference.eventInternet,
            quotation = reference.quotation;

        if (data) {
            var smg = "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            if (!(eventInternet.isValid())) {
                return smg;
            }

            var eventInternetData = eventInternet.getViewModel().get('eventInternet');
            if (eventInternetData) {
                var startDate = new Date(eventInternetData.startDate);
				if(eventInternetData.endDate){
					var endDate = new Date(eventInternetData.endDate);
					if (Ext.Date.diff(startDate, endDate, "d") < 0) {
						return "Invalid date range selection";
					} 	
				}                
            }
            if (!quotation.isHidden() && !quotation.getStore().getAt(0)) {
                return "Please add item to quotation list before you take action.";
            }

            if (!quotation.isHidden() && !reference.itComment.getValue()) {
                return smg;
            }
        }
    },
    buildComponent: function () {
        var me = this;

        return [{
            margin: 5,
            xtype: 'iteirq-eventInternet-view',
            reference: 'eventInternet',
            title: 'Request Information',
            iconCls: 'fa fa-file-text-o',
            border: true,
            mainView: me,
            bind: {
                hidden: '{eventInternetProperty.hidden}'
            }
        }, {
            xtype: 'panel',
            title: 'Quotation',
            iconCls: 'fa fa-file-text-o',
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            margin: 5,
            border: true,
            bind: {
                hidden: '{quotationProperty.hidden}'
            },
            items: [{
                border: false,
                xtype: 'iteirq-quotation-view',
                reference: 'quotation',
                minHeight: 120,
                mainView: me,
                bind: {
                    hidden: '{quotationProperty.hidden}'
                }
            }, {
                xtype: 'textareafield',
                margin: 5,
                labelWidth: 60,
                labelWidth: 150,
                labelAlign: 'right',
                fieldLabel: 'Comment',
                reference: 'itComment',
                allowBlank: false,
                bind: {
                    readOnly: '{eventInternetProperty.comment.readOnly}'
                }
            }]
        }];
    }
});