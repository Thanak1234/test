/**
 * 
 *Author : Phanny 
 */

Ext.define("Workflow.view.hr.MdcRequestForm",{
    extend: "Workflow.view.AbstractRequestForm",

    requires: [
        "Workflow.view.hr.MdcRequestFormController",
        "Workflow.view.hr.MdcRequestFormModel"
    ],
    
    title: 'Medical Treatment',
    header: {
        hidden: true
    },
    controller: "hr-mdcrequestform",
    viewModel: {
        type: "hr-mdcrequestform"
    },
    
    formConfig : {
        
    },
    
    buildItems:function() {
        return {
                xtype: 'form',
                align:'center',
                margin : 5,
                width: 850,
               
                collapsible  : true,
                title: 'Medical Treatment',
                defaultType: 'textfield',
            
                fieldDefaults: {
                    labelWidth: 60
                },
                
                layout: {
                    type: 'vbox',
                    align: 'stretch'
                },
                
                bodyPadding: 5,
                border: true,
        
                items: [{
                    fieldLabel: 'Send To',
                    name: 'to'
                }, {
                    fieldLabel: 'Subject',
                    name: 'subject'
                }, {
                    xtype: 'textarea',
                    hideLabel: true,
                    name: 'msg'
                }]
            };
    },
    buildButtons : function(activity){
        return [{
            xtype: 'splitbutton',
            text: 'Actions',
            iconCls: 'toolbar-overflow-list',
            menu:[{
                    text:'Approved',
                    listeners : {
                        click : 'formAcations'
                    }  
                },
                {
                    text: 'Reworked',
                    listeners : {
                        click : 'formAcations'
                    } 
                },
                {
                    text:'Rejected',
                    listeners : {
                        click : 'formAcations'
                    } 
                 }]
        }];
    }     
});
