/* global Ext */
/**
 * Author: Phanny
 * 
 * 
 * Abstract methods: 
 *                  
 *          buildItems  : build subform items,
 *          buildButtons: build for buttons in main form according to acitivity,
 *          getWorkflowFormConfig: form setting according to acitivity
 * 
 */
Ext.define("Workflow.view.AbstractRequestForm",{
    extend: "Ext.panel.Panel",
    scrollable: true,
    viewModel: {
        data: {
            hiddenAction: {
            
            }
        }
    },
    layout: {
       type: 'vbox',
       pack: 'start',
       align: 'center'
    },
    width: 970,
    workFlowConfig: {
        serial: null,
        activity: 'Submission',
        record  : null
    },
    currentActivityProperty: null,
    //Requestor Form Block
    getDataHeader: function (){
      return this.workFlowConfig;  
    },
    //buildActivityAction: function (activities, actions) {
    //    console.log('activity', activity);
    //    this.buildActionButton(actions, false);
    //},
    setHiddenActions: function(actionNames, hidden){
        var vm = this.getViewModel();
        if(actionNames && vm){
            actionNames.forEach(function (action) 
            {
                var hiddenAction = ('hiddenAction.' + action);
                vm.set(hiddenAction, hidden);
            })
        }
    },
    buildActionButton: function (actions, hiddenDefBtn) {
        var menuItems = new Array();
        var config = this.getWorkflowFormConfig();

        if (config && config.button) {
            var cancel = config.button['Cancelled'];
            if (cancel.hidden) {
                Ext.Array.remove(actions, "Cancelled")
            }
        }

        if (actions) {
            actions.forEach(function (action) {
                var hiddenAction = ('{hiddenAction.' + action + '}');
                var item = {
                    bind: {
                        text: action,
                        hidden: hiddenAction
                    },
                    listeners: {
                        click: 'formActions'
                    }
                };
                if (!(action == 'Auto Cancelled' || action == 'Auto Cancel')) {
                    menuItems.push(item);
                }
            });
        }

        var actionComponent = [{
            xtype: 'button',
            iconCls: 'fa fa-file-pdf-o',
            text: 'Export PDF',
            hidden: hiddenDefBtn ? true : false,
            listeners: {
                click: 'exportPDFHandler'
            }
        }];

        if (Ext.isFunction(this.buildExtraButtons)) {
            var buttons = this.buildExtraButtons();
            if (!Ext.isEmpty(buttons)) {
                Ext.each(buttons, function (action) {
                    actionComponent.push(action);
                });
            }
        }

        if (this.customButtons.length > 0) {
            Ext.each(this.customButtons, function (b) {
                actionComponent.push(b);
            });
        }

        actionComponent.push('->');

        if (actions && actions.length > 1) {
            actionComponent.push({
                xtype: 'button',
                text: 'Actions',
                iconCls: 'toolbar-overflow-list',
                menu: menuItems
            });
        } else {
            var action = actions[0];
            actionComponent.push({
                xtype: 'button',
                text: action,
                iconCls: (action == 'Close'?'fa fa-times':'fa fa-gavel'),
                listeners: {
                    click: action == 'Close' ? 'closeWindow' : 'formActions'
                }
            });
        }

        return actionComponent;
    },
    loadActivityProperty: function () {
        var me = this, 
            activity = me.workFlowConfig.activity,
            record = me.workFlowConfig.record;
        
        if (me.formType) {
            var submissionActivityList = WORKFLOW.CONST.STATES.queryBy(function (record, id) {
                return (
                    record.get('REQUEST_CODE') == me.formType &&
                    record.get('ACTIVITY_NAME') == activity
                );
            });

            if (submissionActivityList && submissionActivityList.getAt(0)) {
                
                var activityState = submissionActivityList.getAt(0).get('CONFIGURATION');

                if (activityState && activityState.activityProperty) {
                    me.currentActivityProperty = activityState.activityProperty.property;

                    var properties = activityState.activityProperty;

                    for (key in properties) {
                        if (properties.hasOwnProperty(key)) {
                            if (key != 'property') {
                                me.currentActivityProperty[key] = properties[key];
                            }
                        }
                    }

                    if(me.currentActivityProperty != null && record != null){
                        me.currentActivityProperty.lastActivity = record.get('lastActivity'); 
                        me.currentActivityProperty.activityName = activity;
                    }
                }
            }
        }
        if (!me.currentActivityProperty) {
            //console.info('The activity [' + activity + '] need to be configure');
        }
    },
    initComponent: function () {
        
		// create config object
        var me      = this,config = {}, 
            activity= me.workFlowConfig.activity,
            serial  = me.workFlowConfig.serial,
            record = me.workFlowConfig.record;
           
        // load activity configuration property
        me.loadActivityProperty();

        // build config properties   
        me.getViewModel().set("serial", serial);
        me.getViewModel().set("activity", activity);
        me.getViewModel().set("record", record);
         
        var workflowFormConfig= me.getWorkflowFormConfig();
        
       
        var items = this.buildItems();
        if (items) {
            config.items = [];
            
            var requestorForm = me.getRequestorForm(workflowFormConfig.requestorFormBlock);
            // force register form request header or requestor
            if (requestorForm) {
                config.items.push(requestorForm);
            }
            config.items.push(items);    
            
            var fileUpload =  me.getFileUpload(workflowFormConfig.formUploadBlock); 
            // force register form upload
            if (fileUpload) {
                if (me.formType) {
                    fileUpload.processCode = me.formType;
                }
                config.items.push(fileUpload);
            }
            
            if(workflowFormConfig.activityHistoryForm.visible){
                config.items.push(me.getActivityHistory());
            }
            
            
            if(workflowFormConfig.commentBlock.visible){
                config.items.push(me.getUserComment());
            }
            
        }else{
            
            console.error('Requestor form items is required.');
        }
        
        // build or register button
        var buttons =  this.getFormButtons();
        if(buttons){
            config.buttons = buttons;
        }
        
        // tool bar
        var tbar = this.buildTbar();
        if(tbar ) { 
            config.tbar = tbar;
        }
        
        // buttom bar
        var bbar = this.buildBbar();
        if(bbar){
            config.bbar = bbar;
        }
		
		// apply config
        Ext.apply(this, Ext.apply(this.initialConfig, config));
		
		 // call parent
         
		me.callParent(arguments);
        
        //Loading 
        //if (activity.toUpperCase()!== 'Submission'.toUpperCase()){            
        this.fireEvent('loadFormData');   
        //} 

	},
	
	buildItems:function() {
        return undefined;
    }
 
    ,buildButtons:function() {
        var me = this,
            activity = me.workFlowConfig.activity,
            actions = me.workFlowConfig.record.get('actions'),
			workflowFormConfig = me.getWorkflowFormConfig();

        if ('Form View'.toLowerCase() === activity.toLowerCase()) {
            return this.buildActionButton(['Close']);
        } else if ('Modification'.toLowerCase() === activity.toLowerCase()) {
			var buttons = ['Edit'];
			if(!workflowFormConfig.InvisibleBtnCancel){
				buttons.push('Cancelled');
			}
            return this.buildActionButton(buttons);
        } else if ('Draft'.toLowerCase() === activity.toLowerCase()) {
            return [{
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
            },
            {
                xtype: 'button',
                iconCls: 'fa fa-play-circle-o',
                text: 'Submit',
                listeners: {
                    click: 'formSubmission'
                }
            }];
        }

        return this.buildActionButton(actions);
    }
 
    ,buildTbar:function() {
        return undefined;
    }
 
    ,buildBbar:function() {
        return undefined;
    },
    getRequestorForm : function(requestorFromBlockConfig){
        var me = this;
        return {
            margin      : 5,
            xtype       : 'form-requestor',
            reference   : 'requestor',
            mainView: me,
            collapsible : true,
            border      : true,
            formReadonly: requestorFromBlockConfig.readOnly,
            hidden     : requestorFromBlockConfig.visible == null || requestorFromBlockConfig.visible == undefined || requestorFromBlockConfig.visible == true ? false : true,
            width       : '100%'
        };
    },
    
    getActivityHistory : function(){
        return {
            margin      : 5,
            xtype       : 'activity-activity-history',
            reference   : 'acitityHistory',
            collapsible : true,
            border      : true,
            width       : '100%'
        }
    },
    
    getFileUpload: function(fileUploadFormBlockConfig){
          return {
            margin      : 5,
            xtype       : 'form-fileupload',
            reference   : 'fileUpload',
            collapsible : true,
            border      : true,
            //formReadonly: fileUploadFromBlockConfig.readOnly,  
            hidden     : (fileUploadFormBlockConfig.visible == null || fileUploadFormBlockConfig.visible == undefined || fileUploadFormBlockConfig.visible == true) ? false : true,
            width: '100%'
        };
    },
    
    
    getUserComment : function () {
        return {        
            xtype       : 'user-comment',
            margin      : 5,
            reference   : 'userComment',
            collapsible : true,
            border: true,
            width: '100%'
        }
    },
    hasSaveDraft: false,
    customButtons: [],
    getFormButtons: function () {
        var me = this;
        var activity = this.getViewModel().get('activity');
        var actionComponent = [];
        
       if (activity === 'Submission') {
           
           if (Ext.isFunction(this.buildExtraButtons)) {
               var buttons = this.buildExtraButtons();
               if (!Ext.isEmpty(buttons)) {
                   Ext.each(buttons, function (action) {
                       actionComponent.push(action);
                   });
               }
           };
           
           if (me.customButtons.length > 0) {
               Ext.each(me.customButtons, function (b) {
                   actionComponent.push(b);
               });
           }

           actionComponent.push('->');
           actionComponent.push({
                xtype: 'button',
                text: 'Save as Draft',
                hidden: !me.hasSaveDraft,
                iconCls: 'fa fa-floppy-o',
                listeners: {
                    click: 'formSaveDraft'
                }
            },{
                xtype: 'button',
                text: 'Submit',
                iconCls: 'fa fa-play-circle-o',
                listeners: {
                    click: 'formSubmission'
                }
           });
           return actionComponent;
        } else {
            if(this.buildButtons){
                return this.buildButtons(activity);    
            }
            
        }
    },
    getWorkflowFormConfig: function(){
        
        var activity = this.getViewModel().get('activity');
        
        console.log('supper class activiey: === ' + activity );
        
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
                visible: false,
                readOnly : true     
            },
            
            //Action Form Block
            commentBlock :{
                visible: false  
            },
            
            //Close, RESET
            afterActonState : 'CLOSE'
        };
    }
    
 
});
