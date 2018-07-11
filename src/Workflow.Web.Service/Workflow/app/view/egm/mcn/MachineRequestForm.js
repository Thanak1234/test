Ext.define('Workflow.view.mcn.MachineRequestForm', {
    extend: 'Workflow.view.AbstractRequestForm',
    xtype: 'mcn-request-form',
    title: 'Machine Form Request',
    header: {
        hidden: true
    },
    controller: "mcn-machinerequestform",
    viewModel: {
        type: "mcn-machinerequestform"
    },
    formType: 'EGMMR_REQ',
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
                xtype: 'mcn-employeelist-requestemployee',
                reference: 'employeeList',
                mainView: me,
                border: true
            }
            , {
                margin: 5,
                xtype: 'mcn-machine-location',
                reference: 'machineInformation',
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
            
            machineForm: {
                readOnly: false,
                showConfirmationNumber: false,
                editConfirmationNumber: false
            },
            machineEmployeeBlock: {                
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

            machineForm: {
                readOnly: true,
                showConfirmationNumber: false,
                editConfirmationNumber: false
            },
            machineEmployeeBlock: {
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

            machineForm: {
                readOnly: true,
                showConfirmationNumber: true,
                editConfirmationNumber: true
            },
            machineEmployeeBlock: {
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
