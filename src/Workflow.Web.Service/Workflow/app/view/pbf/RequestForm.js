Ext.define("Workflow.view.pbf.RequestForm", {
    extend: "Workflow.view.AbstractRequestForm",
    xtype: 'pbf-request-form',
    title: 'Project Brief',
    header: {
        hidden: true
    },
    controller: "pbf-requestform",
    viewModel: {
        type: "pbf-requestform"
    },
    formType: 'PBF_REQ',
    formConfig: {

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
            items: [{
                margin: 5,
                xtype: 'pbf-form-view',
                reference: 'formView',
                mainView: me,
                border: true
            }]
        };
    },
    //Astract method implementation
    getWorkflowFormConfig: function () {

        var activity = this.getViewModel().get('activity');
        var config = null;

        if (!activity) {
            throw new Error("No activity found for worflow form config building");
        }

        if (activity.toUpperCase() === 'Submission'.toUpperCase()) {
            config = this.getSubmissionConfig();
        } else if ('Requestor Rework'.toUpperCase() === activity.toUpperCase()) {
            config = this.getReworkedConfig();
        } else if ("Submitter's HOD Approval".toLowerCase() === activity.toLowerCase()) {
            config = this.getHoDApprovalConfig();
            /* Start Custome Activity */
        } else if ("First Submitter's Review".toLowerCase() === activity.toLowerCase()
            || "Second Submitter's Review".toLowerCase() === activity.toLowerCase()
            || "Final Submitter's Review".toLowerCase() === activity.toLowerCase()
            ) {
            config = this.getReviewedConfig();
        } else if ('Creative First Draft'.toLowerCase() === activity.toLowerCase()
            || 'Creative Second Draft'.toLowerCase() === activity.toLowerCase()
            || 'Creative Third Draft'.toLowerCase() === activity.toLowerCase()
            || 'Creative Final Artwork'.toLowerCase() === activity.toLowerCase()
            || 'Production'.toLowerCase() === activity.toLowerCase()
        ) {
            config = this.getCompleteTaskConfig();
        } else if ('MARCOM Technical Briefing'.toLowerCase() === activity.toLowerCase()) {
            config = this.getTechCompleteTaskConfig();
        } else if ("E&C-HOD's Approval".toLowerCase() === activity.toLowerCase()) {
            //config = this.getApprovalConfig();
            config = this.getMarComApprovalConfig();
            /* Start Custome Activity */
        } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            config = this.getViewConfig();
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
            config = this.getEditConfig();
        }
        config.formType = this.formType;
        return config;
    },
    //Form State Configuration
    getSubmissionConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: false
            },

            projectBriefForm: {
                readOnly: false,
                showTechnician: false,
                technicianReadOnly: true,
                userRequest: true,
                visibleNr: false
            },

            specificationBlock: {
                addEdit: true,
                addOnly: true,
                visibleNr: false
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

            openIn: 'TAB',
            afterActonState: 'RESET'

        };
    },

    getReworkedConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: false
            },

            projectBriefForm: {
                readOnly: false,
                showTechnician: false,
                technicianReadOnly: true,
                userRequest: true,
                visibleNr: false
            },

            specificationBlock: {
                addEdit: true,
                addOnly: true,
                visibleNr: false
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

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'

        };
    },
    getHoDApprovalConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            projectBriefForm: {
                readOnly: true,
                userRequest: false,
                showTechnician: true,
                technicianReadOnly: true,
                visibleNr: true
            },

            specificationBlock: {
                addEdit: false,
                addOnly: false,
                visibleNr: true
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

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getReviewedConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            specificationBlock: {
                addEdit: false,
                addOnly: false,
                visibleNr: false
            },

            projectBriefForm: {
                readOnly: false,
                userRequest: false,
                showTechnician: true,
                technicianReadOnly: true,
                visibleNr: false
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

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getApprovalConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            specificationBlock: {
                addEdit: false,
                addOnly: false,
                visibleNr: true
            },

            projectBriefForm: {
                readOnly: false,
                userRequest: false,
                showTechnician: true,
                technicianReadOnly: true,
                visibleNr: true
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

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getTechCompleteTaskConfig: function () {
        var config = this.getCompleteTaskConfig();
        config.projectBriefForm = {
            readOnly: false,
            showTechnician: true,
            technicianReadOnly: false,
            userRequest: false,
            visibleNr: true
        };
        return config;
    },
    getMarComApprovalConfig: function () {
        var config = this.getCompleteTaskConfig();
        config.projectBriefForm = {
            readOnly: false,
            showTechnician: true,
            technicianReadOnly: false,
            userRequest: false,
            visibleNr: true
        };
        config.specificationBlock = {
            addEdit: false,
            addOnly: false,
            visibleNr: true
        };

        return config;
    },
    getCompleteTaskConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            projectBriefForm: {
                readOnly: false,
                showTechnician: true,
                technicianReadOnly: true,
                userRequest: false,
                visibleNr: true
            },

            specificationBlock: {
                addEdit: false,
                addOnly: false,
                visibleNr: true
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

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getViewConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            projectBriefForm: {
                readOnly: true,
                showTechnician: true,
                technicianReadOnly: true,
                userRequest: false,
                visibleNr: true
            },

            specificationBlock: {
                addEdit: false,
                addOnly: false,
                visibleNr: true
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

            openIn: 'TAB',
            afterActonState: 'CLOSE'
        };
    },
    getEditConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            projectBriefForm: {
                readOnly: true,
                userRequest: false,
                showTechnician: true,
                technicianReadOnly: true,
                visibleNr: true
            },

            specificationBlock: {
                addEdit: false,
                addOnly: false,
                visibleNr: true
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
                requiredActions: ['Edit'],
                visible: true
            },

            InvisibleBtnCancel: true,
            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    }
});
