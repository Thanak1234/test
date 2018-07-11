
Ext.define("Workflow.view.it.requestUser.requestUserWindow",{
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",
    requires: [
        "Workflow.view.it.requestUser.requestUserWindowController",
        "Workflow.view.it.requestUser.requestUserWindowModel"
    ],

    controller: "it-requestuser-requestuserwindow",
    viewModel: {
        type: "it-requestuser-requestuserwindow"
    },
    height: 500,
    
    initComponent: function() {
        var me= this;
        me.items=[{
            xtype: 'form',
            frame: false,
            bodyPadding: '10 10 0',
            reference: 'form', 
            method: 'POST',
            defaultListenerScope: true,
            defaults: {
                flex        : 1,
                anchor      : '100%', 
                allowBlank  : false,
                msgTarget   : 'side',
                labelWidth  : 150, 
                layout      : 'form', 
                xtype       : 'container'  
            },  
            items: [
                        { 
                            fieldLabel  : 'Select Employee',
                            xtype: 'employeePickup',
                            includeInactive: true,
                            allowBlank: true,
                            changeOnly: true,
                            bind: {
                                selection   : '{item}',
                                readOnly: '{!submitBtVisible}'
                            },
                            listeners: {
                                //TODO: to be moved to controller
                                change: function(el, newValue, oldValue){
                                    var team = el.getSelection()?el.getSelection().get('department'): null;
                                    me.getViewModel().set('team', Ext.create('Workflow.model.common.Department', team) );
                                }
                            },
                            triggers: {
                                clear: {
                                    weight: 1,
                                    cls: Ext.baseCSSPrefix + 'form-clear-trigger',
                                    hidden: true,
                                    handler: function (picker) {
                                        picker.onClearClick();
                                        me.getViewModel().set('team', null);
                                    },
                                    scope: 'this'
                                }
                            }
                        },{
                            fieldLabel  : 'Employee Name',
                            xtype       : 'textfield',
                            bind        : { value: '{item.fullName}' , readOnly: '{readOnlyField}' }
                        },{
                            fieldLabel  : 'Employee No',
                            xtype       : 'textfield',
                            bind        : { value: '{item.employeeNo}', readOnly: '{readOnlyField}'}
                            
                        },{
                            fieldLabel  : 'Department',
                            xtype       : 'departmentPickup',
                            bind        : { selection : '{team}', readOnly: '{readOnlyField}'  }
                        },{
                            fieldLabel  : 'Position',
                            xtype       : 'textfield',
                            bind        : { value: '{item.position}', readOnly: '{readOnlyField}'}
                        },{
                            fieldLabel  : 'Report To',
                            xtype       : 'textfield',
                            allowBlank  : true,
                            hidden      : true,
                            bind        : { value: '{item.reportTo}', readOnly: '{readOnlyField}'}
                        },{
                            fieldLabel  : 'Email',
                            xtype       : 'textfield',
                            allowBlank  : true,
                            bind        : { value: '{item.email}', readOnly: '{readOnlyField}'} 
                        },{
                            fieldLabel  : 'Hired Date',
                            xtype       : 'datefield',
                            allowBlank  : true,
                            hidden      : true,
                            bind        : { value: '{item.hiredDate}', readOnly: '{readOnlyField}'}
                        },{
                            fieldLabel  : 'Phone',
                            xtype       : 'textfield',
                            allowBlank  : true,
                            bind        : { value: '{item.phone}', readOnly: '{readOnlyField}'}
                        }
                        
                ] 
        }];
        me.callParent(arguments);
    }
});
