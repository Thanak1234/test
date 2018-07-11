Ext.define("Workflow.view.admcpp.AdmCppForm", {
    extend: "Workflow.view.AbstractRequestForm",
    title: 'Car Park Permit',
    header: {
        hidden: true
    },
    controller: "admcppform",
    viewModel: {
        type: "admcppform"
    },
    formType: 'ADMCPP_REQ',
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
            items: [
               {
                   margin: 5,
                   xtype: 'admcppdetailview',
                   reference: 'detailview'
               }
            ]
        };
    },
    getWorkflowFormConfig: function () {

        var activity = this.getViewModel().get('activity');

        if (!activity) {
            throw new Error("No activity found for worflow form config building");
        }

        if (activity.toUpperCase() === 'Submission'.toUpperCase()) {
            return this.getSubmissionConfig();
        }
        else if ('Requestor Rework'.toUpperCase() === activity.toUpperCase()) {
            return this.getReworkedConfig();
        }
        else if ('HOD Approval'.toLowerCase() === activity.toLowerCase()) {
            return this.getHoDDeptApprovalConfig();
        } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            return this.getViewConfig();
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
            return this.getEditConfig();
        } else if ('Admin Review'.toLowerCase() === activity.toLowerCase() || 'Admin Approval'.toLowerCase() === activity.toLowerCase()) {
            return this.getAdminReviewConfig();
        } else if ('Admin Issue'.toLowerCase() === activity.toLowerCase()) {
            return this.getAdminIssueConfig();
        }        
    },
    getAdminIssueConfig: function () {
        return {
            requestorFormBlock: {
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
                requiredActions: ['Reworked'],
                visible: true
            },

            detailview: {
                editable: false,
                editIssue: true,
                visibleIssue: true
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getAdminReviewConfig: function () {
        return {
            requestorFormBlock: {
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
                requiredActions: ['Rejected', 'Reworked'],
                visible: true
            },

            detailview: {
                editable: false,
                editIssue: false,
                visibleIssue: false
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getViewConfig: function () {
        var me = this;
        var record = this.getViewModel().get('record');
        var activity = record.data.lastActivity;

        var config = {
            requestorFormBlock:{
                readOnly: true,
                visible: true
            },
            
            //Activity history
            activityHistoryForm: {
                visible: true
            },
            
            //Form Uploaded Block
            
            formUploadBlock: {
                visible: true,
                readOnly : true     
            },
            //Action Form Block

            commentBlock :{
                visible: false  
            },
            
            detailview: {
                editable: false,
                editIssue: false,
                visibleIssue: true
            },
            afterActonState : 'CLOSE'
        };

        if (activity.toLocaleLowerCase() != 'Admin Issue'.toLocaleLowerCase()) {
            config.detailview.visibleIssue = false;
        }

        return config;
    },
    //Form State Configuration
    getSubmissionConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: false
            },

            //Activity history
            activityHistoryForm: {
                visible: false
            },

            //Form Uploaded Block

            formUploadBlock: {
                visible: true,
                readOnly: false,
                requiredActions: ['Submitted']
            },

            //Action Form Block
            commentBlock: {
                requiredActions: [],
                visible: false
            },

            detailview: {
                editable: true,
                editIssue: false,
                visibleIssue: false
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

            //Activity history
            activityHistoryForm: {
                visible: true
            },

            //Form Uploaded Block

            formUploadBlock: {
                visible: true,
                readOnly: false,
                requiredActions: ['Resubmitted']
            },

            //Action Form Block
            commentBlock: {
                requiredActions: ['Cancelled'],
                visible: true
            },

            detailview: {
                editable: true,
                editIssue: false,
                visibleIssue: false
            },
            openIn: 'DIALOG',
            afterActonState: 'CLOSE'

        };
    },

    getEditConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: true,
                visible: true
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
                requiredActions: ['Edit', 'Reworked'],
                visible: true
            },

            detailview: {
                editable: false,
                editIssue: true,
                visibleIssue: true
            },
            InvisibleBtnCancel: true,
            openIn: 'DIALOG',
            afterActonState: 'CLOSE'

        };
    },

    getHoDDeptApprovalConfig: function () {
        return {
            requestorFormBlock: {
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
                requiredActions: ['Rejected', 'Cancelled', 'Reworked'],
                visible: true
            },
            detailview: {
                editable: false,
                editIssue: false,
                visibleIssue: false
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    }
});
