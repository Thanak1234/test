Ext.define('Workflow.view.icd.IncidentRequestForm', {
    extend: 'Workflow.view.AbstractRequestForm',
    xtype: 'icd-request-form',
    title: 'Incident Form Request',
    header: {
        hidden: true
    },
    controller: "icd-incidentrequestform",
    viewModel: {
        type: "icd-incidentrequestform"
    },
    formType: 'EGMIR_REQ',
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
            }
            ,
            items: [{
                margin: 5,
                xtype: 'icd-employeelist-requestemployee',
                reference: 'employeeList',
                mainView: me,
                border: true
            }
            , {
                margin: 5,
                xtype: 'icd-incident-location',
                reference: 'incidentInformation',
                mainView: me,
                border: true
            }
            , {
                margin: 5,
                xtype: 'icd-incident-outline',
                reference: 'incidentOutline',
                mainView: me,
                border: true
            }
            ]
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
            actions = [
                    {
                        xtype: 'button',
                        text: 'Export PDF',
                        listeners: {
                            click: 'exportPDFHandler'
                        }
                    }
                 , '->',
                {
                xtype: 'button',
                text: 'Edit',
                //iconCls: 'toolbar-overflow-list',
                listeners: {
                    click: 'formActions'
                }
                //menu: [{
                //    text: 'Edit',
                //    listeners: {
                //        click: 'formActions'
                //    }
                //}
                //, {
                //    text: 'Cancelled',
                //    listeners: {
                //        click: 'formActions'
                //    }
                //}
                //]
            }];
        }  else {
            actions = [{
                xtype: 'button',
                text: 'Export PDF',
                listeners: {
                    click: 'exportPDFHandler'
                }
            }, '->', {
                xtype: 'button',
                text: 'Actions',
                iconCls: 'toolbar-overflow-list'
            }];
        }

        return actions;
    },
    //Astract method implementation
    getWorkflowFormConfig: function () {
        //debugger;
        var activity = this.getViewModel().get('activity');
        var config = null;

        if (!activity) {
            throw new Error("No activity found for worflow form config building");
        }
        
        if (activity.toUpperCase() === 'Submission'.toUpperCase()) {            
            config = this.getSubmissionConfig();
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
            
            incidentForm: {
                readOnly: false,
                showConfirmationNumber: false,
                editConfirmationNumber: false
            },
            incidentEmployeeBlock: {                
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

   
    getViewConfig: function () {
        return {
            requestorFormBlock: {
                readOnly: true
            },

            incidentForm: {
                readOnly: true,
                showConfirmationNumber: false,
                editConfirmationNumber: false
            },
            incidentEmployeeBlock: {
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

            incidentForm: {
                readOnly: true,
                showConfirmationNumber: true,
                editConfirmationNumber: true
            },
            incidentEmployeeBlock: {
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
