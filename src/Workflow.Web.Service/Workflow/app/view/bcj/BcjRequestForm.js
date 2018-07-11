/**
 * 
 *Author : Phanny 
 */

Ext.define("Workflow.view.bcj.BcjRequestForm", {
    extend: "Workflow.view.AbstractRequestForm",
    requires: [
        "Workflow.view.bcj.BcjRequestFormController",
        "Workflow.view.bcj.BcjRequestFormModel"
    ],
    title: 'Business Cass Justification',
    header: {
        hidden: true
    },
    controller: "bcj-bcjrequestform",
    viewModel: {
        type: "bcj-bcjrequestform"
    },
    formType: 'BCJ_REQ',
    hasSaveDraft: true,
    formConfig: {

    },
    buildItems: function () {
        return {
            xtype: 'panel',
            align: 'center',
            width: '100%',

            layout: {
                type: 'vbox',
                pack: 'start',
                align: 'stretch'
            },
            items: [{
                margin: 5,
                xtype: 'bcj-project-detail-view',
                reference: 'projectDetailView',
                //hidden: true,
                border: true
            }, {
                margin: 5,
                xtype: 'bcj-purchaseorderitem',
                reference: 'purchaseOrderItemView',
                border: true
            }]
        };
    },

    //Astract method implementation
    getWorkflowFormConfig: function (acl) {
        var activity = this.getViewModel().get('activity');

        if (!activity) {
            throw new Error("No activity found for worflow form config building");
        }

        if (activity.toUpperCase() === 'Submission'.toUpperCase()) {
            return this.getSubmissionConfig();
        } else if ('Requestor Rework'.toUpperCase() === activity.toUpperCase()
            || 'Draft'.toUpperCase() === activity.toUpperCase()) {
            return this.getReworkedConfig();
        } else if ('Department Head'.toLowerCase() === activity.toLowerCase()
            || 'Line of Department'.toLowerCase() === activity.toLowerCase()
            || 'Department Executive'.toLowerCase() === activity.toLowerCase()
            || 'Level 1 Approval'.toLowerCase() === activity.toLowerCase()
            || 'Level 2 Approval'.toLowerCase() === activity.toLowerCase()) {
            return this.getApprovalConfig();
        } else if ('CFO'.toLowerCase() === activity.toLowerCase()
            || 'CFO DyCFO'.toLowerCase() === activity.toLowerCase()
            || 'Capex Committee'.toLowerCase() === activity.toLowerCase()
            || 'Finance Group'.toLowerCase() === activity.toLowerCase()) {
            return this.getFinanceConfig();
        } else if ('Purchasing'.toLowerCase() === activity.toLowerCase()) {
            return this.getPurchasingConfig();
        } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            return this.getViewConfig(acl);
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
            return this.getEditConfig(acl);
        }
    },
    //Form State Configuration
    getSubmissionConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: false
            },

            projectItem: {
                readOnly: false
            },

            projectDetail: {
                readOnly: false
            },
            purchaseOrderItemView: {
                hidden: true,
                readOnly: true
            },
            //Activity history
            activityHistoryForm: {
                visible: false
            },

            //Form Uploaded Block

            formUploadBlock: {
                visible: true,
                readOnly: false
            },

            //Action Form Block
            commentBlock: {
                requiredActions: [],
                visible: false
            },

            requestItemBlock: {
                addEdit: true
            },

            analysisItemBlock: {
                addEdit: true
            },
            openIn: 'TAB',
            afterActonState: 'RESET'

        };
    },

    getReworkedConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: false
            },

            projectItem: {
                readOnly: false
            },

            projectDetail: {
                readOnly: false
            },
            purchaseOrderItemView: {
                hidden: true,
                readOnly: true
            },
            //Activity history
            activityHistoryForm: {
                visible: true
            },

            //Form Uploaded Block

            formUploadBlock: {
                visible: true,
                readOnly: false
            },

            //Action Form Block
            commentBlock: {
                requiredActions: [],
                visible: false
            },

            requestItemBlock: {
                addEdit: true
            },

            analysisItemBlock: {
                addEdit: true
            },
            openIn: 'DIALOG',
            afterActonState: 'CLOSE'

        };
    },

    getApprovalConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            projectItem: {
                readOnly: true
            },

            projectDetail: {
                readOnly: false
            },
            purchaseOrderItemView: {
                hidden: true,
                readOnly: true
            },
            //Activity history
            activityHistoryForm: {
                visible: true
            },

            //Form Uploaded Block

            formUploadBlock: {
                visible: true,
                readOnly: false
            },

            //Action Form Block
            commentBlock: {
                requiredActions: ['Rejected'],
                visible: true
            },

            requestItemBlock: {
                addEdit: true
            },

            analysisItemBlock: {
                addEdit: true
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getFinanceConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            projectItem: {
                readOnly: true
            },

            projectDetail: {
                readOnly: true
            },

            purchaseOrderItemView: {
                hidden: true,
                readOnly: true
            },
            //Activity history
            activityHistoryForm: {
                visible: true
            },

            //Form Uploaded Block

            formUploadBlock: {
                visible: true,
                readOnly: false
            },

            //Action Form Block
            commentBlock: {
                requiredActions: ['Rejected'],
                visible: true
            },

            requestItemBlock: {
                addEdit: false
            },

            analysisItemBlock: {
                addEdit: false
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getPurchasingConfig: function(){
        return {
            requestorFormBlock: {
                readOnly: true
            },

            projectItem: {
                readOnly: true
            },

            projectDetail: {
                readOnly: true
            },
            purchaseOrderItemView: {
                hidden: false,
                readOnly: false
            },
            //Activity history
            activityHistoryForm: {
                visible: true
            },

            //Form Uploaded Block

            formUploadBlock: {
                visible: true,
                readOnly: false
            },

            requestItemBlock: {
                addEdit: false
            },

            analysisItemBlock: {
                addEdit: false
            },

            //Action Form Block
            commentBlock: {
                requiredActions: [],
                visible: true
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getViewConfig: function (acl) {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            projectItem: {
                readOnly: true
            },

            projectDetail: {
                readOnly: true
            },
            purchaseOrderItemView: {
                hidden: !(acl ? acl.getValue('SHOW_PURCHASING_PANEL') : false),
                readOnly: true
            },
            //Activity history
            activityHistoryForm: {
                visible: true
            },

            //Form Uploaded Block

            formUploadBlock: {
                visible: true,
                readOnly: true
            },

            //Action Form Block
            commentBlock: {
                requiredActions: ['Rejected'],
                visible: false
            },

            requestItemBlock: {
                addEdit: false
            },

            analysisItemBlock: {
                addEdit: false
            },
            openIn: 'TAB',
            afterActonState: 'CLOSE'
        };
    },
    getEditConfig: function (acl) {
        var viewmodel = this.getViewModel(),
            data = viewmodel.getData();

        var isPurchasing = false;//(acl ? acl.getValue('SHOW_PURCHASING_PANEL') : false);
        if (data && data.record && data.record.getData()) {
            var identity = Workflow.global.UserAccount.identity;
            Ext.each(data.record.getData().activities, function (activity) {
                if (activity.activity == 'Purchasing' && activity.decision == 'Done') {
                    if ( identity && activity.approver && identity.loginName &&
                        (identity.loginName.toUpperCase() == activity.approver.toUpperCase().replace('K2:', ''))
                       ) {
                        isPurchasing = true;
                    }
                }
            });
        }

        console.log('isPurchasing', isPurchasing);
        return {
            requestorFormBlock: {
                readOnly: true
            },

            projectItem: {
                readOnly: true
            },

            projectDetail: {
                readOnly: true
            },
            purchaseOrderItemView: {
                hidden: false,
                readOnly: !isPurchasing
            },
            //Activity history
            activityHistoryForm: {
                visible: true
            },

            //Form Uploaded Block

            formUploadBlock: {
                visible: true,
                readOnly: false
            },

            requestItemBlock: {
                addEdit: false
            },

            analysisItemBlock: {
                addEdit: false
            },

            //Action Form Block
            commentBlock: {
                requiredActions: ['Edit'],
                visible: true
            },

            InvisibleBtnCancel: true,
            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    }
});
