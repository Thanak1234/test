Ext.define("Workflow.view.reservation.crr.RequestForm", {
    extend: "Workflow.view.AbstractRequestForm",
    xtype: 'crr-request-form',
    title: 'Complimentary Room Request',
    header: {
        hidden: true
    },
    controller: "crr-requestform",
    viewModel: {
        type: "crr-requestform"
    },
    formType: 'RSVNCR_REQ',
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
                xtype: 'crr-guestview',
                reference: 'guestView',
                mainView: me,
                border: true
            },{
                margin: 5,
                xtype: 'crr-form-view',
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
             },
             , '->',
             {
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
            },  '->', {
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
            actions = [ {
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
        } else if ('Reservation Review'.toLowerCase() === activity.toLowerCase()) {
            actions = [
                {
                    xtype: 'button',
                    text: 'Export PDF',
                    listeners: {
                        click: 'exportPDFHandler'
                    }
                },
                 , '->',
                {
                    xtype: 'button',
                    text: 'Actions',
                    iconCls: 'toolbar-overflow-list',
                    menu: [{
                        text: 'Done',
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
        }else if ('GM Approval'.toLowerCase() === activity.toLowerCase()){
            config = this.getGMApprovalConfig();
        } else if ('Reservation Review'.toLowerCase() === activity.toLowerCase()) {            
            config = this.getReservationConfig();
            config.complimentaryForm.require = true;                        
        } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            config = this.getViewConfig();
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
            config = this.getEditConfig();
        }

        console.log('test ', config);

        return config;
    },
    //Form State Configuration
    getSubmissionConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: false
            },

            complimentaryForm: {
                readOnly: false,
                showConfirmationNumber: false,
                editConfirmationNumber: false
            },
            guestBlock: {                
                addEdit: true 
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

            complimentaryForm: {
                readOnly: false,
                showConfirmationNumber: false,
                editConfirmationNumber: false
            },
            guestBlock: {
                addEdit: true
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

            complimentaryForm: {
                readOnly: true,
                showConfirmationNumber: false,
                editConfirmationNumber: false
            },
            guestBlock: {
                addEdit: false
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
    getGMApprovalConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            complimentaryForm: {
                readOnly: true,
                showConfirmationNumber: false,
                editConfirmationNumber: false
            },
            guestBlock: {
                addEdit: false
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
    getReservationConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            complimentaryForm: {
                readOnly: true,
                showConfirmationNumber: true,
                editConfirmationNumber: true
            },
            guestBlock: {
                addEdit: false
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

            complimentaryForm: {
                readOnly: true,
                showConfirmationNumber: true,
                editConfirmationNumber: false
            },
            guestBlock: {
                addEdit: false
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

            complimentaryForm: {
                readOnly: true,
                showConfirmationNumber: true,
                editConfirmationNumber: true
            },
            guestBlock: {
                addEdit: false
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
