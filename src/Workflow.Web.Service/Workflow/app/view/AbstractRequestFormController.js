/* global Workflow */
/**
 *Author : Phanny 
 *
 *
 *Abstract methods:   
            renderSubForm   : render data in subform while data loading,
            getRequestItem  : Retrieve data from subform for data processing,
            clearData       : Clear data in sub form,
            validateForm    : subform validation
 */
Ext.define('Workflow.view.AbstractRequestFormController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.requestform',

    /**
     * Actions
     */
    actionUrl: null,
    config: {
        control: {
            '*': {
                loadFormData: 'loadFormData'
            }
        }
    },

    loadFormData: function () {

        var me = this,
            model = me.getView().getViewModel(),
            activity = model.get("activity");

        if (!activity || activity === 'Submission') {
            me.renderMainForm(null);
            return;
        }

        me.renderMainForm(model.get('record'));
    },


    renderMainForm: function (data) {
        //debugger;
        var me = this,
            view = me.getView(),
            references = view.getReferences(),
            config = view.getWorkflowFormConfig();

        if (data) {
            view.getViewModel().set('requestHeaderId', data.get('requestHeaderId') ? data.get('requestHeaderId') : 0);
        }
        
        var requestor = references.requestor;
        if (requestor) {
            requestor.fireEvent('loadData', data ? { requestor: data.get("requestor"), priority: data.get("priority"), viewSetting: config, lastActivity: data.get('lastActivity'), status: data.get('status') } : null);
        }      

        var uploadFiles = data && data.get('fileUploads') ? data.get('fileUploads') : null;
        var allFiles = uploadFiles ? uploadFiles.allItems : null;
        references.fileUpload.fireEvent('loadData', { data: data && uploadFiles && allFiles ? allFiles : null, viewSetting: config, lastActivity: data && data.get('activity') });
        var acitityHistory = references.acitityHistory;
        var userCommentView = references.userComment;

        if (acitityHistory) {
            acitityHistory.fireEvent('loadData', data ? data.get("activities") : null);
        }

        if (userCommentView) {
            userCommentView.fireEvent('loadData', data ? data.get("comment") : null);
        }

        //Pass data to subform implementation
        if (this.renderSubForm) {
            var formData = (data ? data.get('dataItem') : null);
            var property = (data ? data.get('property') : null);
            var acl = me.getAclStore((data ? data.get('acl') : null));
            this.renderSubForm(formData, acl);
        }
    },
    getAclStore: function(acl){
        return Ext.create('Ext.data.Store', {
            data: acl,
            getValue: function (key) {
                var record = this.getAclByKey(key);
                if(record){
                    return record.get('Value');
                }
                return false;
            },
            getAclByKey: function (key) {
                return this.findRecord('Key', key);
            }
        });
    },
    formSubmission: function (el, e, eOpts) {

        this.takeAction('Submitted');
    },
    formSaveDraft: function (el, e, eOpts) {
        this.takeAction('Save Draft');
    },
    formActions: function (el, e, eOpts) {

        this.takeAction(el.text);

    },
    afterTakeAction: function (data) {
    
    },
    takeAction: function (action) {
        //debugger;
        if (this.actionUrl == null) {
            throw new Error("Please define URl to take action");
        }
        var me = this,
            data = me.getRequestFormData(action),
            validationMsg = me.validation(data),
            formConfig = me.getView().getWorkflowFormConfig(),
            confirmation = 'Are you sure to take this action ?',
            customMsg = null;

        if(me.confirmMessage){
            customMsg = me.confirmMessage(data);
            confirmation = customMsg?customMsg:confirmation;
        }
  
        if (validationMsg) {
            Ext.MessageBox.show({
                title: 'Form validation',
                msg: validationMsg,
                buttons: Ext.MessageBox.OK,
                icon: Ext.MessageBox.ERROR
            });
            return;
        }

        Ext.MessageBox.show({
            title: 'Confirmation',
            msg: confirmation,
            buttons: Ext.MessageBox.YESNO,
            scope: this,
            icon: Ext.MessageBox.QUESTION,
            fn: function (bt) {
                if (bt == 'yes') {
                    me.getView().mask("Data processing...");
                    Ext.Ajax.request({
                        url: me.actionUrl,
                        method: 'POST',
                        headers: { 'Content-Type': 'application/json' },
                        jsonData: data,
                        success: function (response, operation) {
                            me.getView().unmask();
                            me.showToast("Form is submitted successfully.");
                            me.afterTakeAction(data);
                            if (!formConfig.afterActonState) {
                                return;
                            }
                            
                            var responseObj = Ext.decode(response.responseText);
                            if ('Submission' === data.activity ||
                                'Draft' === data.activity
                                || responseObj.show) {
                                if (action != 'Save Draft') {
                                    Ext.MessageBox.show({
                                        title: 'Information',
                                        msg: responseObj.message,
                                        buttons: Ext.MessageBox.OK,
                                        icon: Ext.MessageBox.INFO
                                    });
                                }
                            }

                            if (formConfig.afterActonState.toUpperCase() === "RESET") {
                                me.clearForm();
                            } else if (formConfig.afterActonState.toUpperCase() === "CLOSE") {
                                me.closeWindow();
                            }
                        },
                        failure: function (data, operation) {
                            me.getView().unmask();
                            me.showToast("Failed to submit form.");
                            var response = Ext.decode(data.responseText);
                            Ext.MessageBox.show({
                                title: 'Error',
                                msg: response.Message,
                                buttons: Ext.MessageBox.OK,
                                icon: Ext.MessageBox.ERROR
                            });
                        }
                    });
                }
            }
        });
    },

    exportPDFHandler: function () {
        var view = this.getView();
        var requestHeaderId = view.getViewModel().get('requestHeaderId');
        var documentType = view.formType;
        Ext.core.DomHelper.append(document.body, {
                tag: 'iframe',
                id: ['forms_download', requestHeaderId].join('_'),
                frameBorder : 0,
                width: 0,
                height: 0,
                src: Workflow.global.Config.baseUrl +
                    'api/forms/download?requestHeaderId=' + requestHeaderId +
                    '&documentType=' + documentType
        });
    },


    /**
     * Methods
     */

    //main data to be used for server service
    getRequestFormData: function (action) {
        var me = this,
            dataHeader  = me.getDataHeader(),
            Refs        = me.getView().getReferences(),
            comment     = Refs.userComment ? Refs.userComment.getViewModel().get('userComment') : null;

        return {
            requestor: me.getRequestorData(),
            priority: me.getPriority(),
            dataItem: me.getRequestItem ? me.getRequestItem() : {},
            comment: comment,
            fileUploads     : me.getFileUpload(),
            action: action,
            serial: dataHeader.serial,
            activity: dataHeader.activity,
            dataHeader: dataHeader
        };
    },

    clearForm: function () {
        this.getView().getReferences().requestor.clearData();

        var uploadView = this.getView().getReferences().fileUpload;
        uploadView.fireEvent('onDataClear');

        if (this.clearData) {
            this.clearData();
        }
    },

    getDataHeader: function () {
        return this.getView().getDataHeader();
    },

    getRequestorData: function () {
        var requestor = this.getView().getReferences().requestor;
        return requestor.getData();
    },

    getPriority: function () {
        var requestor = this.getView().getReferences().requestor;
        return requestor.getPriority();
    },

    getWorkflowFormConfig: function () {
        return this.getView().getWorkflowFormConfig();
    },

    getFileUpload: function () {
        var upload = this.getView().getReferences().fileUpload;
        return upload.getData();
    },

    validation: function (data) {

        var me = this;
        if (!data.requestor) {
            return "Requestor is required. Please specify.";
        }

        var formConfig = this.getView().getWorkflowFormConfig();
        
        var ignoreFormValidation = (data.action == 'Rejected'
            || data.action == 'Reworked' || data.action == 'Cancelled');

        if (me.validateForm && !ignoreFormValidation) {

            
            
            var subFormValidationMsg = me.validateForm(data);

            if (subFormValidationMsg) {
                return subFormValidationMsg;
            }
        }

        var requiredComment = false;
        //console.log('data action', data);

        if (formConfig.commentBlock.visible && me.isEmptyOrSpaces(data.comment)) {

            var requiredActions = formConfig.commentBlock.requiredActions || [];

            if(data.activity === "Modification"){
                requiredActions.push(data.action.toUpperCase());
            }

            for (var i in requiredActions) {
                if (requiredActions[i].toUpperCase() === data.action.toUpperCase()) {
                    requiredComment = true;
                    break;
                }
            }
        }

        var requiredAttachment = false;
        var fileUploadCount = data.fileUploads.allItems.length;

        if (formConfig.formUploadBlock.visible && !me.isUndefineAndFalse(formConfig.formUploadBlock.requiredActions) && fileUploadCount == 0) {
            var requiredActions = formConfig.formUploadBlock.requiredActions || [];
            for (var i in requiredActions) {
                if (requiredActions[i].toUpperCase() === data.action.toUpperCase()) {
                    return "Attachment is required. Please upload the require files.";
                }
            }
        }

        if (requiredComment) {
            return "Comment is required. Please give some comment.";
        }
        return null;
    },

    actionFinish: function () {

    },

    closeWindow: function () {
        var me = this,
            w = me.getView().up(),
            panel = w.up();
        if(panel){
            //Tab panel
            panel.remove(w);
            return;        
        }
        //Window dialog
        w.close();    
        
    },
    isUndefineAndFalse: function (obj) {
        if (obj == null || obj == undefined || obj == false)
            return true;
        return false;
    }
});
