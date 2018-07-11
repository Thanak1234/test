Ext.define("Workflow.view.rmd.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'rmd-request-form',
    title: 'Self Assessment Form',
    formType: 'RMD_REQ',
	header: false,
    hasSaveDraft: true,
    actionUrl: Workflow.global.Config.baseUrl + 'api/rmdrequest',
    bindPayloadData: function (reference) {
        var me = this,
            riskAssessment = reference.riskAssessment,
            worksheet1 = reference.worksheet1,
            worksheet2 = reference.worksheet2;
            //worksheet3 = reference.worksheet3;

        var worksheet1store = worksheet1.getStore();
        var worksheet2store = worksheet2.getStore();
        //var worksheet3store = worksheet3.getStore();

        var payload = {
            riskAssessment: riskAssessment.getViewModel().getData().riskAssessment,
            // worksheet 1
            addWorksheet1s: me.getOriginDataFromCollection(worksheet1store.getNewRecords()),
            editWorksheet1s: me.getOriginDataFromCollection(worksheet1store.getUpdatedRecords()),
            delWorksheet1s: me.getOriginDataFromCollection(worksheet1store.getRemovedRecords()),
            // worksheet 2
            addWorksheet2s: me.getOriginDataFromCollection(worksheet2store.getNewRecords()),
            editWorksheet2s: me.getOriginDataFromCollection(worksheet2store.getUpdatedRecords()),
            delWorksheet2s: me.getOriginDataFromCollection(worksheet2store.getRemovedRecords()),
            // worksheet 3
            //addWorksheet3s: me.getOriginDataFromCollection(worksheet3store.getNewRecords()),
            //editWorksheet3s: me.getOriginDataFromCollection(worksheet3store.getUpdatedRecords()),
            //delWorksheet3s: me.getOriginDataFromCollection(worksheet3store.getRemovedRecords())
        };

         return payload;
    },
    loadDataToView: function (reference, data, acl) {
        this.clearData(reference);
        var cloneData = this.getCopyData();
        if(cloneData){
            data = cloneData;

            data.riskAssessment.id = 0;
            data.worksheet1s = this.getNewCollection(cloneData.worksheet1s);
            data.worksheet2s = this.getNewCollection(cloneData.worksheet2s);
        }

        this.fireEventLoad(reference.riskAssessment, data);
        this.fireEventLoad(reference.worksheet1, data);
        this.fireEventLoad(reference.worksheet2, data);
        //this.fireEventLoad(reference.worksheet3, data);
    },
    clearData: function (reference) {
        reference.riskAssessment.getViewModel().set('riskAssessment', {});

        reference.riskAssessment.reset();
        reference.worksheet1.fireEvent('onDataClear');
        reference.worksheet2.fireEvent('onDataClear');
        //reference.worksheet3.fireEvent('onDataClear');
    },
    validateForm: function(reference, data){
        var me = this,
            riskAssessment = reference.riskAssessment,
            worksheet1 = reference.worksheet1,
            worksheet2 = reference.worksheet2;
            //worksheet3 = reference.worksheet3;
        
        if (data && data.action != "Save Draft") {
            var smg = "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            if (!(riskAssessment.isValid())) {
                return smg;
            }

            if (!worksheet1.isHidden() && !worksheet1.getStore().getAt(0)) {
                return "Please add item to Worksheet1 before you take action.";
            }

            if (!worksheet2.isHidden() && !worksheet2.getStore().getAt(0)) {
                return "Please add item to Worksheet2 before you take action.";
            }

            //if (!worksheet3.isHidden() && !worksheet3.getStore().getAt(0)) {
            //    return "Please add item to Worksheet3 before you take action.";
            //}
        }
    },
    buildComponent: function () {
        var me = this;
        me.customButtons = [{
            xtype: 'button',
            hidden: false,
            text: 'Copy Form',
            iconCls: 'fa fa-copy',
            listeners: {
                click: function(){
                    var vm = me.getViewModel();
                    window.location = "#rmd-request-form/clone=" + me.getId();
                }
            }
        }, {
            xtype: 'button',
            text: 'Risk Category',
            iconCls: 'fa fa-question-circle',
            hidden: true,
            listeners: {
                click: function () {
                    window.open("/documents/rmd/RISK%20CATEGORY.pdf", "_blank")
                }
            }
        }, {
            xtype: 'button',
            text: 'Control Ratings',
            iconCls: 'fa fa-question-circle',
            hidden: true,
            listeners: {
                click: function () {
                    window.open("/documents/rmd/CONTROL%20RATINGS.pdf", "_blank")
                }
            }
        }, {
            xtype: 'button',
            text: 'Risk Measurement Matrix',
            iconCls: 'fa fa-question-circle',
            listeners: {
                click: function(){
                    window.open("/documents/rmd/Risk%20Measurement%20Matrix.pdf", "_blank");
                }
            }
        }];

        return [{
            xtype: 'panel',
            iconCls: 'fa fa-file-text-o',
            title: 'Risk Assessment',
            xtype: 'rmd-riskAssessment-view',
            reference: 'riskAssessment',
            margin: 5,
            border: true,
            mainView: me,
            bind: {
                hidden: '{riskAssessmentProperty.hidden}'
            }
        }, {
            xtype: 'panel',
            iconCls: 'fa fa-file-text-o',
            title: 'Risk Assessment Worksheet No.1',
            margin: 5,
            border: true,
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            bind: {
                hidden: '{worksheet1Property.hidden}'
            },
            items: [{
                border: false,
                title: 'Risk Assessment Worksheet No.1',
                xtype: 'rmd-worksheet1-view',
                reference: 'worksheet1',
                minHeight: 120,
                mainView: me,
                bind: {
                    hidden: '{worksheet1Property.hidden}'
                }
            }]
        }, {
            xtype: 'panel',
            iconCls: 'fa fa-file-text-o',
            title: 'Risk Assessment Worksheet No.2',
            margin: 5,
            border: true,
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            bind: {
                hidden: '{worksheet2Property.hidden}'
            },
            items: [{
                border: false,
                title: 'Risk Assessment Worksheet No.2',
                xtype: 'rmd-worksheet2-view',
                reference: 'worksheet2',
                minHeight: 350,
                mainView: me,
                bind: {
                    hidden: '{worksheet2Property.hidden}'
                }
            }]
        }/*, {
            xtype: 'panel',
            iconCls: 'fa fa-file-text-o',
            title: 'Risk Assessment Worksheet 3 - Risk Evaluation and Risk Treatment',
            margin: 5,
            border: true,
            layout: {
                type: 'vbox',
                align: 'stretch'
            },
            bind: {
                hidden: '{worksheet3Property.hidden}'
            },
            items: [{
                title: 'Risk Assessment Worksheet 3 - Risk Evaluation and Risk Treatment',
                border: false,
                xtype: 'rmd-worksheet3-view',
                reference: 'worksheet3',
                minHeight: 350,
                mainView: me,
                bind: {
                    hidden: '{worksheet2Property.hidden}'
                }
            }]
        }*/];
    }
});