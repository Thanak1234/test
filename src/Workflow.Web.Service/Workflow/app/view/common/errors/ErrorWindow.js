Ext.define("Workflow.view.common.errors.ErrorWindow", {
    extend: "Ext.window.Window",
    xtype: 'errorwindow',
    controller: "errorwindow",
    viewModel: {
        type: "errorwindow"
    },
    title: 'Error!',
    formReadonly: false,
    frame: true,
    maxHeight: 500,
    width: 600, 
    layout: {
        type: 'vbox',
        pack: 'start',
        align: 'stretch'
    },
    resizable: false,
    modal: true,
    bodyStyle: 'background:white',
    buttonAlign: 'left',
    defaults: {
        color: 'red',
        fontSize: '12px'
    },
    initComponent: function () {
        var me = this;

        me.buildItems();
        me.buildButtons();

        me.callParent(arguments);
    },
    buildItems: function () {
        var me = this;
        
        me.items = [
            {
                xtype: 'label',
                reference: 'message',
                padding: 5,
                style: {
                    color: 'red',
                    fontSize: '13px'
                },
                bind: {
                    html: '{message}'
                }
            },
            {
                xtype: 'panel',
                reference: 'detail',
                bodyPadding: 10,
                autoScroll: true,
                bodyStyle: {
                    backgroundColor: '#ffc'
                },
                hidden: true,
                frame: true,
                padding: 0,
                margin: 0,
                flex: 1,
                items: [
                    {
                        xtype: 'label',
                        reference: 'detailMessage',
                        style: {
                            //color: 'red',
                            fontSize: '13px'
                            
                        },
                        bind: {
                            html: 'Stack Trace: <br/><br/>{detail}'
                        }
                    }
                ]
            }
        ];
    },
    buildButtons: function () {
        var me = this;

        me.buttons = [
            {
                xtype: 'button',
                text: 'Show Detail',
                enableToggle: true,
                listeners: {
                    toggle: 'onToggleClick'
                }                
            },            
            '->',
            {
                text: 'Ok',
                handler: 'onOkClick'
            }
            
        ];
    }
});
