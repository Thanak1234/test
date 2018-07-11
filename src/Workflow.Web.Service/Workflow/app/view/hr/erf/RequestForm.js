Ext.define("Workflow.view.hr.erf.RequestForm", {
    extend: "Workflow.view.AbstractRequestForm",
    xtype: 'erf-request-form',
    title: 'Employee Requisition Form',
    header: {
        hidden: true
    },
    controller: "erf-requestform",
    viewModel: {
        type: "erf-requestform"
    },
    formType: 'ERF_REQ',
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
                xtype: 'erf-form-view',
                reference: 'formView',
                mainView: me,
                border: true
            }]
        };
    },
    buildButtons: function (activity) {
        var activity = this.getViewModel().get('activity');

        var actions = [];

        if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            actions = [
             {
                 xtype: 'button',
                 text: 'Export PDF',
                 listeners: {
                     click: 'exportPDFHandler'
                 }
             }, '->', {
                 xtype: 'button',
                 text: 'Close',
                 listeners: {
                     click: 'closeWindow'
                 }
             }];
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
            actions = [{
                xtype: 'button',
                text: 'Export PDF',
                listeners: {
                    click: 'exportPDFHandler'
                }
            }, '->', {
                xtype: 'button',
                text: 'Actions',
                iconCls: 'toolbar-overflow-list',
                menu: [{
                    text: 'Edit',
                    listeners: {
                        click: 'formActions'
                    }
                }, {
                    text: 'Cancelled',
                    listeners: {
                        click: 'formActions'
                    }
                }]
            }];
        } else if ('Requestor Rework'.toLowerCase() === activity.toLowerCase()) {
            actions = [{
                xtype: 'button',
                text: 'Actions',
                iconCls: 'toolbar-overflow-list',
                menu: [{
                    text: 'Resubmitted',
                    listeners: {
                        click: 'formActions'
                    }
                },
                    {
                        text: 'Cancelled',
                        listeners: {
                            click: 'formActions'
                        }
                    }]
            }];
        } else {
            actions = [{
                xtype: 'button',
                text: 'Export PDF',
                listeners: {
                    click: 'exportPDFHandler'
                }
            }, '->', {
                xtype: 'button',
                text: 'Actions',
                iconCls: 'toolbar-overflow-list',
                menu: [{
                    text: 'Approved',
                    listeners: {
                        click: 'formActions'
                    }
                }, {
                    text: 'Reworked',
                    listeners: {
                        click: 'formActions'
                    }
                }, {
                    text: 'Rejected',
                    listeners: {
                        click: 'formActions'
                    }
                }]
            }];
        }

        return actions;
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
        } else if ('HoD Approval'.toLowerCase() === activity.toLowerCase()) {
            config = this.getHoDApprovalConfig();
        } else if ('Department Executive'.toLowerCase() === activity.toLowerCase()) {
            config = this.getExComApprovalConfig();
        } else if ('HR Recruitment'.toLowerCase() === activity.toLowerCase()) {
            config = this.getHRRecruitmentApprovalConfig();
        } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            config = this.getViewConfig();
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
            config = this.getEditConfig();
        }

        return config;
    },
    //Form State Configuration
    getSubmissionConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: false
            },

            requisitionForm: {
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

            openIn: 'TAB',
            afterActonState: 'RESET'

        };
    },

    getReworkedConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: false
            },

            requisitionForm: {
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

            requisitionForm: {
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
                visible: true
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getExComApprovalConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            requisitionForm: {
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
                visible: true
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getHRRecruitmentApprovalConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            requisitionForm: {
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

            requisitionForm: {
                readOnly: true,
                showRefNo: true
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

            requisitionForm: {
                readOnly: true,
                showRefNo: true
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


            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    }
});
