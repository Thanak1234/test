/**
 * 
 *Author : Phanny 
 */

Ext.define("Workflow.view.it.ItRequestForm",{
    extend: "Workflow.view.AbstractRequestForm",

    requires: [
        "Workflow.view.it.ItRequestFormController",
        "Workflow.view.it.ItRequestFormModel"
    ],
    
    title: 'IT Request Form',
    header: {
        hidden: true
    },
    controller: "it-itrequestform",
    viewModel: {
        type: "it-itrequestform"
    },
    formType: 'IT_REQ',
   /*******************************Item building ********************************************/
    hasSaveDraft: true,
    buildItems:function() {
        // var me = this;
        return {
                xtype: 'panel',
                align:'center',
                width: '100%',                
                layout: {
                    type: 'vbox',
                    pack: 'start',
                    align: 'stretch'
                },
                items: [
                   {
                       xtype    : 'it-request-user',
                       reference: 'requestUser',
                    //    viewModel : {
                    //        viewSetting : me.getWorkflowFormConfig()
                    //    },
                       border : true,
                       margin   : 5
                   },{
                       margin   : '10 0 0 0',
                       xtype    : 'it-request-item',
                       reference: 'requestItem',
                       border : true,
                       margin   : 5
                    }
                ]
        };
    },
    buildButtons : function(){
        
       var activity = this.getViewModel().get('activity');
        
        var actions = [];
        
        if ('Form View'.toLowerCase() === activity.toLowerCase()) {
           actions = [
                {
                   xtype: 'button',
                   text: 'Export PDF',
                   iconCls: 'fa-file-pdf-o',
                   listeners: {
                       click: 'exportPDFHandler'
                   }
                },
                , '->',
                {
                    xtype: 'button',
                    //icon: 'fa-times',
                    text: 'Close',
                    listeners : {
                        click : 'closeWindow'
                    }  
                }];
       }else if('IT Implementation'.toLowerCase() === activity.toLowerCase()){
           actions = [
                    {
                        xtype: 'button',
                        text: 'Export PDF',
                        iconCls: 'fa-file-pdf-o',
                        listeners: {
                            click: 'exportPDFHandler'
                        }
                    },
                    ,'->',
                    {
                        xtype: 'button',
                        iconCls : 'fa-check-square-o',
                        text: 'Done',
                        listeners : {
                            click : 'formActions'
                        }  
                    }];
       } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
           actions = [
                    {
                        xtype: 'button',
                        text: 'Export PDF',
                        iconCls: 'fa-file-pdf-o',
                        listeners: {
                            click: 'exportPDFHandler'
                        }
                    },
                    , '->',
                    {
                        xtype: 'button',
                        iconCls: 'fa-chevron-up',
                        text: 'Edit',
                        listeners : {
                            click: 'formActions'
                        }  
                    }];
       } else if ('Draft'.toLowerCase() === activity.toLowerCase()) {
           actions = [{
                        xtype: 'button',
                        text: 'Export PDF',
                        iconCls: 'fa fa-file-pdf-o',
                        listeners: {
                            click: 'exportPDFHandler'
                        }
                    }, '->',
                    {
                        xtype: 'button',
                        iconCls: 'fa fa-floppy-o',
                        text: 'Save as Draft',
                        listeners: {
                            click: 'formSaveDraft'
                        }
                    }, {
                        xtype: 'button',
                        iconCls: 'fa fa-play-circle-o',
                        text: 'Submit',
                        listeners: {
                            click: 'formSubmission'
                        }
                    }];
       } else if ('Requestor Rework'.toLowerCase() === activity.toLowerCase()) {
           actions = [{
               xtype: 'button',
               text: 'Actions',
               iconCls: 'toolbar-overflow-list',
               menu: [{
                   text: 'Resubmitted',
                   iconCls: 'fa fa-play-circle-o',
                   listeners: {
                       click: 'formActions'
                   }
               },
                   {
                       text: 'Cancelled',
                       iconCls: 'fa-stop-circle-o',
                       listeners: {
                           click: 'formActions'
                       }
                   }]
           }];
       }
       else {
           actions = [
               {
                   xtype: 'button',
                   text: 'Export PDF',
                   iconCls: 'fa-file-pdf-o',
                   listeners: {
                       click: 'exportPDFHandler'
                   }
               },
                , '->',
               {
                   xtype: 'button',
                   text: 'Actions',
                   iconCls: 'fa-chevron-up',
                   menu: [{
                       text: 'Approved',
                       listeners: {
                           click: 'formActions'
                       }
                   },
                       {
                           text: 'Reworked',
                           listeners: {
                               click: 'formActions'
                           }
                       },
                       {
                           text: 'Rejected',
                           listeners: {
                               click: 'formActions'
                           }
                       }]
               }];
       }
            
       return actions;
    },
   
   
   /*******************************View setting ********************************************/
    
   //Astract method implementation
    getWorkflowFormConfig: function(){
        
        var activity = this.getViewModel().get('activity');
        
        if(!activity){
           throw new Error("No activity found for worflow form config building");
        }
       
        if(activity.toUpperCase()==='Submission'.toUpperCase() ) {
            return this.getSubmissionConfig();
        }
        else if ('Requestor Rework'.toUpperCase() === activity.toUpperCase()
             || 'Draft'.toLowerCase() === activity.toLowerCase()) {
            return this.getReworkedConfig();   
       } else if('HoD Approval'.toLowerCase() === activity.toLowerCase() 
            || 'IT Approval'.toLowerCase() === activity.toLowerCase() 
            || 'IT Implementation'.toLowerCase() === activity.toLowerCase()
            || 'IT HoD Approval'.toLowerCase() === activity.toLowerCase() ){
           return this.getHoDDeptApprovalConfig();
       } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
           return this.getViewConfig();
       } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
           return this.getEditConfig();
       }
    },
    getEditConfig : function () {
        return {
            requestorFormBlock:{
                readOnly: true
            },
            
            //Activity history
            activityHistoryForm: {
                visible: true
            },
            
            //Form Uploaded Block
            
            formUploadBlock: {
                visible: true,
                readOnly : false     
            },
            
            //Action Form Block
            
            requestItemBlock : {
                addEdit : false
            },
            
            requestUserBlock : {
                addEdit : false
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
    },  
    
   //Form State Configuration
    getSubmissionConfig : function(){
               
          return {
            requestorFormBlock:{
                readOnly: false
            },
            
            //Activity history
            activityHistoryForm: {
                visible: false
            },
            
            //Form Uploaded Block
            
            formUploadBlock: {
                visible: true,
                readOnly : false     
            },
            
            //Action Form Block
            commentBlock :{
                requiredActions: [],
                visible: false  
            },
            
            requestItemBlock : {
                addEdit : true
            },
            
            requestUserBlock : {
                addEdit : true
            },
            
            afterActonState : 'RESET'
            
        };
    },
    
    getReworkedConfig : function(){
               
          return {
            requestorFormBlock:{
                readOnly: false
            },
            
            //Activity history
            activityHistoryForm: {
                visible: true
            },
            
            //Form Uploaded Block
            
            formUploadBlock: {
                visible: true,
                readOnly : false     
            },
            
            //Action Form Block
            commentBlock :{
                requiredActions: [],
                visible: false  
            },
            
            requestItemBlock : {
                addEdit : true
            },
            
            requestUserBlock : {
                addEdit : true
            },
            openIn: 'DIALOG',
            afterActonState : 'CLOSE'
            
        };
    },
    getHoDDeptApprovalConfig : function(){
          return {
            requestorFormBlock:{
                readOnly: true
            },
            
            //Activity history
            activityHistoryForm: {
                visible: true
            },
            
            //Form Uploaded Block
            
            formUploadBlock: {
                visible: true,
                readOnly : false     
            },
            
            //Action Form Block
            commentBlock :{
                requiredActions : ['Rejected'],
                visible: true  
            },
            
            requestItemBlock : {
                addEdit : false
            },
            
            requestUserBlock : {
                addEdit : false
            },
            openIn: 'DIALOG',
            afterActonState : 'CLOSE'
        };
    },
    
    getViewConfig : function () {
         return {
            requestorFormBlock:{
                readOnly: true
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
                requiredActions : ['Rejected'],
                visible: false  
            },
            
            requestItemBlock : {
                addEdit : false
            },
            
            requestUserBlock : {
                addEdit : false
            },
            
            afterActonState : 'CLOSE'
        };
    }    
});
