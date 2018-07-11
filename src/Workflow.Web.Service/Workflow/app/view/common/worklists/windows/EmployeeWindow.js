
Ext.define("Workflow.view.common.worklists.EmployeeWindow",{
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",
    xtype: 'common-worklists-employeewindow',

    requires: [

    ],
    
    controller: "common-worklists-employeewindow",
    viewModel: {
        type: "common-worklists-employeewindow"
    },
    showComment: false,
    title: 'Employee',
    reference: 'form',
    formReadonly : false,
    frame: true,
    width: 580,
    //height: 400,
    layout: {
        type: 'vbox',
        align: 'stretch'
    },
    autoWidth: true,
    bodyPadding: 10, 
    initComponent: function() {
        var me=this;
        me.items= [{
            xtype: 'form',
            bodyPadding: '0 5 0',
            width: 600,

            fieldDefaults: {
                labelAlign: 'top',
                msgTarget: 'side'
            },

            defaults: {
                border: false,
                xtype: 'panel',
                flex: 1,
                layout: 'anchor'
            },

            layout: 'hbox',

            items: me.buildForm()
        },{
            xtype: 'textarea',
            margin: '0 5 0 5',
            hidden: !me.showComment,
            emptyText: 'Comment',
            allowBlank: false,
            bind: {
                value: '{comment}'
            }
        }];
        me.callParent(arguments);
    },
    buildForm: function(){
        var me = this;
        return [{
            items: [{
                emptyText: 'Employee',
                anchor: '-5',
                xtype: 'employeePickup',
                integrated: true,
                excludeOwner: true,
                bind: {
                    selection: '{employeeInfo}',
                    readOnly: '{readOnly}'
                }
            },{ 
                xtype: 'textfield',
                emptyText: 'Employee No', 
                anchor: '-5',
                bind: '{employeeInfo.employeeNo}',
                readOnly: true 
            },{ 
                xtype: 'textfield',
                emptyText: 'Position', 
                anchor: '-5',
                bind: '{employeeInfo.position}', 
                readOnly: true 
            },{ 
                xtype: 'textfield',
                emptyText: 'Department', 
                anchor: '-5',
                bind: '{employeeInfo.subDept}', 
                readOnly: true 
            }]
        }, {
            items: [{ 
                xtype: 'textfield',
                emptyText: 'Phone', 
                anchor: '100%',
                bind: '{employeeInfo.phone}', 
                readOnly: true 
            },{ 
                xtype: 'textfield',
                emptyText: 'Ext', 
                anchor: '100%',
                bind: '{employeeInfo.ext}', 
                readOnly: true 
            },{ 
                xtype: 'textfield',
                emptyText: 'Email', 
                anchor: '100%',
                bind: '{employeeInfo.email}', 
                readOnly: true 
            }]
        }];
    },
    buttons : [
        {
            text: 'Add',
            handler: 'onAddClickHandler'
        },
        {
            text: 'Cancel',
            handler: 'closeWindow'
        }
    ],
    getData: function (){
        return this.getViewModel().get('employeeInfo');
    }

});
