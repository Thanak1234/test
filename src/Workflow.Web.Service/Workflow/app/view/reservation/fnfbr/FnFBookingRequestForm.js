Ext.define("Workflow.view.reservation.fnfbr.FnFBookingRequestForm", {
    extend: "Workflow.view.AbstractRequestForm",
    xtype: 'fnf-booking-request',
    title: 'Friends and Family Booking Request Form',
    header: {
        hidden: true
    },
    controller: "fnfbr-requestform",
    viewModel: {
        type: "fnfbr-requestform"
    },
    formType: 'RSVNFF_REQ',
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
                xtype: 'fnfbr-reservation-view',
                reference: 'reservationView',
                mainView: me,
                border: true
            }, {
                xtype: 'fnf-occupancyview',
                reference: 'occupancyView',
                margin: 5,
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
        } else if ('Reservation Review'.toLowerCase() === activity.toLowerCase()) {
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
                    text: 'Reviewed',
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
        } else if ('Final Reservation Review'.toLowerCase() === activity.toLowerCase()) {
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
                    text: 'Done',
                    listeners: {
                        click: 'formActions'
                    }
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

        if (!activity) {
            throw new Error("No activity found for worflow form config building");
        }

        if (activity.toUpperCase() === 'Submission'.toUpperCase()) {
            return this.getSubmissionConfig();
        } else if ('Requestor Rework'.toUpperCase() === activity.toUpperCase()) {
            return this.getReworkedConfig();
        } else if ('HoD Approval'.toLowerCase() === activity.toLowerCase()) {
            return this.getHoDApprovalConfig();
        }else if ('Reservation Approval'.toLowerCase() === activity.toLowerCase()){
            return this.getReservationApprovalConfig();
        }else if ('Reservation Review'.toLowerCase() === activity.toLowerCase()){
            return this.getReservationConfig();
        } else if ('Final Reservation Review'.toLowerCase() === activity.toLowerCase()) {
            var config = this.getReservationConfig();
            config.reservationForm.require = true;
            return config;
        } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            return this.getViewConfig();
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
            return this.getEditConfig();
        }
    },
    //Form State Configuration
    getSubmissionConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: false
            },

            reservationForm: {
                readOnly: false,
                reservationOnly: false,
                reservationReadOnly: true
            },
            occupancyBlock: {
                addEdit: false,
                reservationOnly: false
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

            reservationForm: {
                readOnly: false,
                reservationOnly: false,
                reservationReadOnly: true
            },

            occupancyBlock: {
                addEdit: false,
                reservationOnly: false
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

            reservationForm: {
                readOnly: true,
                reservationOnly: false,
                reservationReadOnly: true
            },

            occupancyBlock: {
                addEdit: false,
                reservationOnly: false
            },

            //Activity history
            activityHistoryForm: {
                visible: true
            },
            //Form Uploaded Block

            formUploadBlock: {
                visible: false,
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
    getReservationApprovalConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            reservationForm: {
                readOnly: true,
                reservationOnly: true,
                reservationReadOnly: false
            },

            occupancyBlock: {
                addEdit: true,
                reservationOnly: true
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

            occupancyBlock: {
                addEdit: true,
                reservationOnly: true
            },

            reservationForm: {
                readOnly: true,
                reservationOnly: true,
                reservationReadOnly: false
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

            occupancyBlock: {
                addEdit: false,
                reservationOnly: true
            },

            reservationForm: {
                readOnly: true,
                reservationOnly: true,
                reservationReadOnly: true
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

            reservationForm: {
                readOnly: true,
                reservationOnly: true,
                reservationReadOnly: false
            },

            occupancyBlock: {
                addEdit: true,
                reservationOnly: true
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
