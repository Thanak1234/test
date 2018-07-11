Ext.define("Workflow.view.hr.att.RequestForm", {
    extend: "Workflow.view.AbstractRequestForm",
    xtype: 'att-request-form',
    title: 'Authorisation To Travel Form',
    header: {
        hidden: true
    },
    controller: "att-requestform",
    viewModel: {
        type: "att-requestform"
    },
    formType: 'ATT_REQ',
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
                xtype: 'att-form-view',
                reference: 'formView',
                mainView: me,
                border: true
            }, {
                margin: 5,
                xtype: 'att-nagatravel-view',
                reference: 'nagaTravelView',
                mainView: me,
                border: true,                
                bind: { hidden: '{hidden}' }
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
        } else if ('HR Review'.toLowerCase() === activity.toLowerCase()) {
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
        } else if ('EXCO Approval'.toLowerCase() === activity.toLowerCase()) {
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
        } else if ('HR Approval'.toLowerCase() === activity.toLowerCase()) {
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
        } else if ('NAGA Travel'.toLowerCase() === activity.toLowerCase()) {
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
        }else if ('EXCO Approval'.toLowerCase() === activity.toLowerCase()){
            config = this.getEXCOApprovalConfig();
        } else if ('HR Review'.toLowerCase() === activity.toLowerCase()) {            
            config = this.getHRReviewConfig();            
        } else if ('HR Approval'.toLowerCase() === activity.toLowerCase()) {
            config = this.getHRApprovalConfig();
        } else if ('NAGA Travel'.toLowerCase() === activity.toLowerCase()) {
            config = this.getNAGATravelConfig();
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
            config = this.getEditConfig();
        } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            config = this.getViewConfig();            
        }

        return config;
    },
    //Form State Configuration
    getSubmissionConfig: function () {

        return {
            requestorFormBlock: {
                readOnly: false
            },

            travelDetailForm: {
                readOnly: false,
                visible: false
            },
            destinationBlock: {
                addEdit: true
            },

            nagaTravelForm: {
                readOnly: false,
                visible: false
            },
            flightDetailBlock: {
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

            travelDetailForm: {
                readOnly: false,
                visible: false
            },
            destinationBlock: {
                addEdit: true
            },


            nagaTravelForm: {
                readOnly: false
            },
            flightDetailBlock: {
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

            travelDetailForm: {
                readOnly: true,
                visible: false
            },
            destinationBlock: {
                addEdit: false
            },

            nagaTravelForm: {
                readOnly: false,
                visible: false
            },
            flightDetailBlock: {
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
    getEXCOApprovalConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            travelDetailForm: {
                readOnly: true,
                visible: false
            },
            destinationBlock: {
                addEdit: false
            },


            nagaTravelForm: {
                readOnly: false
            },
            flightDetailBlock: {
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
                requiredActions: ['Rejected'],
                visible: true
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getHRReviewConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            travelDetailForm: {
                readOnly: true,
                visible: true
            },
            destinationBlock: {
                addEdit: false
            },


            nagaTravelForm: {
                readOnly: false
            },
            flightDetailBlock: {
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
                requiredActions: ['Rejected'],
                visible: true
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getHRApprovalConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            travelDetailForm: {
                readOnly: true,
                visible: false
            },
            destinationBlock: {
                addEdit: false
            },


            nagaTravelForm: {
                readOnly: true,
                visible : false
            },
            flightDetailBlock: {
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
    getNAGATravelConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            travelDetailForm: {
                readOnly: true,
                visible: true
            },
            destinationBlock: {
                addEdit: false
            },


            nagaTravelForm: {
                readOnly: false,
                visible: true
            },
            flightDetailBlock: {
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
                visible: true
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getEditConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            travelDetailForm: {
                readOnly: true,
                visible: true
            },
            destinationBlock: {
                addEdit: true
            },

            nagaTravelForm: {
                readOnly: false,
                visible: true
            },
            flightDetailBlock: {
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
                requiredActions: ['Edit'],
                visible: true
            },


            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
        };
    },
    getViewConfig: function () {
        var me = this;
        var vm = me.getViewModel();
        var config = {
            requestorFormBlock: {
                readOnly: true
            },

            travelDetailForm: {
                readOnly: true,
                visible: true
            },
            destinationBlock: {
                addEdit: false
            },

            nagaTravelForm: {
                readOnly: true,
                visible: true
            },
            flightDetailBlock: {
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

        var lastActivity = vm.get('record').data.lastActivity;
        console.log('lss ', lastActivity);
        vm.set('hidden', false);
        return config;
    }
});
