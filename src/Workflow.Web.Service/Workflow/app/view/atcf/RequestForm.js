Ext.define("Workflow.view.atcf.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'atcf-request-form',
    title: 'Additional Time Worked Claim Form',
    formType: 'ATCF_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/atcfrequest',
    bindPayloadData: function (reference) {
        var me = this,
            additionalTimeWorked = reference.additionalTimeWorked;

        var store = additionalTimeWorked.getStore();

        var data = {
            addAdditionalTimeWorkeds: me.getOriginDataFromCollection(store.getNewRecords()),
            editAdditionalTimeWorkeds: me.getOriginDataFromCollection(store.getUpdatedRecords()),
            delAdditionalTimeWorkeds: me.getOriginDataFromCollection(store.getRemovedRecords())
        }

        return data;
    },
    loadDataToView: function (reference, data, acl) {
        var me = this,
            viewmodel = me.getViewModel();

        var viewSetting = me.currentActivityProperty;

        this.fireEventLoad(reference.additionalTimeWorked, data);
    },
    clearData: function (reference) {
        reference.additionalTimeWorked.fireEvent('onDataClear');
    },
    validateForm: function(reference, data){
        var me = this, additionalTimeWorked = reference.additionalTimeWorked;

        var store = additionalTimeWorked.getStore();
        if (data) {
            if (!additionalTimeWorked.isHidden() && !store.getAt(0)) {
                return "Please add item to Additional Time Worked List before you take action.";
            }
        }
    },
    buildComponent: function () {
        var me = this;
        return [{
            xtype: 'panel',
            layout: {
                type: 'vbox',
                pack: 'start',
                align: 'stretch'
            },
            title: 'Additional Time Worked',
            bodyPadding: 0,
            margin: 5,
            border: true,
            items: [{
                margin: 5,
                minHeight: 200,
                border: true,
                xtype: 'atcf-additional-time-worked-view',
                reference: 'additionalTimeWorked',
                mainView: me,
                bind: {
                    hidden: '{additionalTimeWorkedProperty.hidden}'
                }
            }, {
                xtype: 'form',
                layout: {
                    type: 'vbox',
                    align: 'stretch'
                },

                bodyPadding: 5,
                border: false,

                items: [{
                    xtype: 'label', padding: 10,
                    html: 'Purpose:&nbsp;&nbsp;&nbsp;&nbsp; This form must be completed by staff (executive and above) ' +
                    'when working overtime, off day, and public holiday to cliam for a replacement day.</br>' +
                    'Instruction: Please enter Code additional time worked and <font style="color:#000;">number of hrs</font> to indicate ' +
                    'additional time worked form the item below # 1, 2 or <font style="color:#000;">3. 8 hrs</font> equivalent to 1 day claim.'
                }, {
                    xtype: 'label',
                    padding: '5 5 5 80',
                    html: '<font style="color:#000;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;' +
                        'Example: write PH2, if you have work 2 hrs overtime on public Holiday.</font></br>' +
                        'Code Additional time worked on: <font style="color:#000;">Reqular Day (RD); OFF Day (OD); Public Holiday (PH)</font>'
                }]
            }]
        }];
    }
});
