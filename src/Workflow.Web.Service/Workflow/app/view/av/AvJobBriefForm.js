/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.av.AvJobBriefForm",{
    extend: "Workflow.view.AbstractRequestForm",

    requires: [
        "Workflow.view.av.AvJobBriefFormController",
        "Workflow.view.av.AvJobBriefFormModel"
    ],
    title: 'AV Job Brief',
    header: {
        hidden: true
    },
    controller: "av-avjobbriefform",
    viewModel: {
        type: "av-avjobbriefform"
    },
    formType: 'AVJ_REQ',
     /*******************************Item building ********************************************/
    
    buildItems: function () {
        var me = this;
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
                       margin   : 5,
                       xtype    : 'av-job-detail-view',
                       reference: 'avJobDetailView',
                       border : true
                   },{
                       margin: 5,
                       title: 'Scope Needed - Sound',
                       xtype    : 'av-request-item-view',
                       reference: 'avRequestItemView',
                       scopeType: 'Sound',
                       border : true 
                   }, {
                       margin: 5,
                       title: 'Scope Needed - Visual',
                       xtype: 'av-request-item-view',
                       scopeType: 'Visual',
                       reference: 'avRequestItemVisualView',
                       border: true
                   }, {
                       margin: 5,
                       title: 'Scope Needed - Lights',
                       xtype: 'av-request-item-view',
                       reference: 'avRequestItemLightsView',
                       scopeType: 'Lights',
                       border: true
                   }, {
                       xtype: 'panel',
                       iconCls: 'fa fa-list-alt',
                       title: 'Scope Needed - Other',
                       layout: 'vbox',                  
                       margin: 5,
                       items: [
                           {
                               xtype: 'textareafield',
                               width: '100%',
                               reference: 'avJobDetailOther',
                               readOnly: me.reqFormBlockReadOnly(),
                               bind: {
                                   value: '{jobDetail.other}'
                               },
                               margin: 5
                           }
                       ]
                   } 
                ]
        };
     },

    reqFormBlockReadOnly: function () {
        var me = this;
        return me.getWorkflowFormConfig().requestorFormBlock.readOnly;
    },
    buildButtons : function(){
        
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
                listeners : {
                    click : 'closeWindow'
                }  
            }];
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
             actions =[
               '->',
               {
                         xtype: 'button',
                         text: 'Edit',
                                 listeners: {
                                 click: 'formActions'
                        }
                        }];
        }else if('AV Technician'.toLowerCase() === activity.toLowerCase()) {
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
                        listeners : {
                            click : 'formActions'
                        }  
                    }];
        } else if ('Requestor Rework'.toLowerCase() === activity.toLowerCase()) {
           actions= [{
                xtype: 'button',
                text: 'Actions',
                iconCls: 'toolbar-overflow-list',
                menu:[{
                        text: 'Resubmitted',
                        listeners : {
                            click : 'formActions'
                        } 
                    },
                    {
                        text:'Cancelled',
                        listeners : {
                            click : 'formActions'
                        } 
                    }]
            }];
       }
       else{
           actions = [
               {
                   xtype: 'button',
                   text: 'Export PDF',
                   listeners: {
                       click: 'exportPDFHandler'
                   }
               },
                , '->', {
                xtype: 'button',
                text: 'Actions',
                iconCls: 'toolbar-overflow-list',
                menu:[{
                        text:'Approved',
                        listeners : {
                            click : 'formActions'
                        }  
                    },
                    {
                        text: 'Reworked',
                        listeners : {
                            click : 'formActions'
                        } 
                    },
                    {
                        text:'Rejected',
                        listeners : {
                            click : 'formActions'
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
       
        if(activity.toUpperCase()==='Submission'.toUpperCase()) {
            return this.getSubmissionConfig();
        } 
        else if ('Requestor Rework'.toUpperCase() === activity.toUpperCase()) {
            return this.getReworkedConfig();   
        } else if('HoD Approval'.toLowerCase() === activity.toLowerCase() 
             || 'AV Technician'.toLowerCase() === activity.toLowerCase())
        {
            return this.getHoDDeptApprovalConfig();
        }
        else if ('AV Approval'.toLowerCase() === activity.toLowerCase())
        {
            return this.getAvApprovalConfig();
       } else if ('Form View'.toLowerCase() === activity.toLowerCase()) {
           return this.getViewConfig();
       } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
           return this.getEditConfig();
       }

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
            openIn          : 'TAB',
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
            openIn          : 'DIALOG',
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
            
            openIn          : 'DIALOG',
            afterActonState : 'CLOSE'
        };
    },
    

    getAvApprovalConfig: function () {
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
                requiredActions: ['Rejected'],
                visible: true
            },

            requestItemBlock: {
                addEdit: false
            },

            requestUserBlock: {
                addEdit: false
            },

            openIn: 'DIALOG',
            afterActonState: 'CLOSE'
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
            openIn          : 'TAB',
            afterActonState : 'CLOSE'
        };
    },

    getEditConfig: function () {
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
                requiredActions : ['Edit'],
                visible: true  
            },
            
            requestItemBlock : {
                addEdit : true
            },
            
            requestUserBlock : {
                addEdit : false
            },
            openIn          : 'DIALOG',
            afterActonState : 'CLOSE'
        };
    }
       
});
