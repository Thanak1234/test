Ext.define("Workflow.view.eom.EOMForm", {
    extend: "Workflow.view.AbstractRequestForm",
    title: 'Employee of The Month',
    header: {
        hidden: true
    },
    controller: "eom-eomform",
    viewModel: {
        type: "eom-eomform"
    },
    formType: 'EOM_REQ',
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
                   xtype: 'eom-eomdetailview',
                   reference: 'eomdetailview'
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
        else if ('HoD Approval'.toLowerCase() === activity.toLowerCase()) {
            return this.getHoDDeptApprovalConfig();
        } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            return this.getViewConfig();
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()
            || 'Payroll'.toLowerCase() === activity.toLowerCase()
            || 'Creative'.toLowerCase() === activity.toLowerCase()) {
            return this.getEditConfig();
        } else if ('T&D Review'.toLowerCase() === activity.toLowerCase()) {
            return this.getTDReviewConfig();
        } else if ('T&D Approval'.toLowerCase() === activity.toLowerCase()) {
            return this.getTDApprovalConfig();
        }        
    },
    getTDApprovalConfig: function () {
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

            eomdetailview: {
                editable: false,
                tdedit: false,
                hidetd: false
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getTDReviewConfig: function () {
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

            eomdetailview: {
                editable: false,
                tdedit: true,
                hidetd: false
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    contain: function (array, v) {
        var rtn = false;
        Ext.each(array, function (val, index) {
            if (v == val) {
                rtn = true;
                return;
            }
        });

        return rtn;
    },
    getViewConfig: function () {
        var me = this;
        var record = this.getViewModel().get('record');
        var lastActivity = record.data.lastActivity;
        var activities = ['Payroll', 'Creative', 'T&D Review', 'T&D Approval', 'Modification'];

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
            
            eomdetailview: {
                editable: false,
                visible: true,
                tdedit: false,
                hidetd: true
            },            
            afterActonState : 'CLOSE'
        };

        if (me.contain(activities, lastActivity)) {
            config.eomdetailview.hidetd = false;
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
                readOnly: false
            },

            //Action Form Block
            commentBlock: {
                requiredActions: [],
                visible: false
            },

            eomdetailview: {
                editable: true,
                tdedit: false,
                hidetd: true
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
                readOnly: false
            },

            //Action Form Block
            commentBlock: {
                requiredActions: [],
                visible: true
            },

            eomdetailview: {
                editable: true,
                tdedit: false,
                hidetd: true
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

            eomdetailview: {
                editable: false,
                tdedit: false,
                hidetd: false
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

            eomdetailview: {
                editable: false,
                tdedit: false,
                hidetd: true
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    }
});
