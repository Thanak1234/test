/**
 * 
 *Author : Phanny 
 */
Ext.define('Workflow.view.main.Main', {
    extend: 'Ext.panel.Panel',
    xtype: 'app-main',

    requires: [
        'Ext.plugin.Viewport',
        'Ext.window.MessageBox',

        'Workflow.view.main.MainController',
        'Workflow.view.main.MainModel',
        'Workflow.view.dashboard.Dashboard'
    ],

    controller: 'main',
    viewModel: {
        type:'main'
    },
    layout: 'border',
    hideByNotifyBt : false,
    notifyDialog : null,
   
    initComponent: function () {
        var me  = this;
		var qParam = Ext.urlDecode(window.location.search.substring(1));
        var singleForm = (qParam.singleForm == 'true');
        
        new Ext.util.KeyMap(document.body, {
            key: "f",
            ctrl:true,
            shift:true,
            fn: function(keycode, e) {
                e.stopEvent();
                Ext.MessageBox.prompt('Find', 'Form Number:', 
                function(btn, text) {
                    if(btn == 'ok'){
                        var profileWin = Ext.create('Ext.window.Window', {
                            title: 'Request Instance - Criteria',
                            scrollable: true,
                            bodyPadding: 0,
                            modal: true,
                            items: [{
                                xtype: 'report-processinstancereport',
                                width: 1200,
                                height: 600,
                                folio: text,
                                layout: 'border'
                            }],
                            maximizable: false,
                            constrain: true,
                            closable: true
                        });
                        profileWin.show();
                    }
                }
                , this);
            }
        });
		
        me.items = [{
                    xtype: 'toolbar',
                    cls: 'sencha-dash-dash-headerbar toolbar-btn-shadow workflow-logo',
                    region:'north',
					hidden: singleForm,
                    height: 68,
                    padding:0,
                    itemId: 'headerBar',
                    items: [
                        {
                            xtype: 'component',
                            html: '<a href="#dashboard" class="naga-logo"></a>',
                            width   : 100
                        },{
                            xtype   : 'tbspacer',
                            flex: 1
                        },{
                            xtype: 'tbtext',
                            bind: {
                                html: '<h4 class="login-style"> {identity.fullName} ({identity.employeeNo})<h4>'
                            }
                        },
                        {
                            xtype : 'button',
                            bind: {
                                text : '{notifiedCount}',
                                hidden : '{notifyBtHide}'
                            },
                            //text : '7',
                            reference : 'notifyBt',
                            iconCls : 'fa fa-bell',
                            cls: 'notification-icon',
                            handler : function(el, event){

                                me.fireEvent('refreshNotification');

                                if(!me.notifyDialog){
                                    me.notifyDialog = Ext.create('Workflow.view.notification.NotificationView', {
                                        cbFn:function(){
                                            me.fireEvent('refreshNotification');
                                        } 
                                    });
                                }

                                if(me.notifyDialog.isVisible()){
                                    me.hideByNotifyBt = true;
                                }

                                if(!me.notifyDialog.isVisible() && !me.hideByNotifyBt){
                                    var x = el.getX() - 400 + el.getWidth();
                                    var y = el.getY()+el.getHeight();
                                    me.notifyDialog.showAt(x,y);
                                }

                                if(me.hideByNotifyBt) {
                                    me.hideByNotifyBt = false;
                                }
                                        
                            }
                        },{ 
                            iconCls: 'fa fa-bars',
                            tooltip : 'See your profile',
                            menu    : {
                                    items: [{
                                            text    : 'My Profile',
                                            iconCls : 'fa fa-user', 
                                            handler : 'onProfileView', 
                                            reference:'onProfile',
                                            id      : 'emp-profile-form', 
                                            view    : 'Workflow.view.common.profile.EmployeeProfileWindow'
                                            
                                        },{
                                            text    : 'Logout',
                                            iconCls: 'fa fa-sign-out',
                                            handler : 'onLogout' 
                                        }
                               ]
                            }
                        } 
                    ]
                }, {
                    margin: '0 0 0 0',
                    xtype: 'navigation-tree',
                    width: 300,
                    hidden: singleForm,
                    split: true,
                    region: 'west',
                    useArrows: true
                }, /*{
                    xtype: 'panel',
                    padding: 0,
                    layout: 'accordion',
                    width: 300,
                    hidden: singleForm,
                    split: true,
                    region: 'west',
                    useArrows: false,
                    items: [{
                        margin: '0 0 0 0',
                        xtype: 'navigation-tree'
                    }, {
                        margin: '1 0 0 0',
                        title: 'Reports',
                        iconCls: 'fa fa-pie-chart',
                        xtype: 'navigation-tree'
                    }, {
                        margin: '1 0 0 0',
                        title: 'Management',
                        iconCls: 'fa fa-cogs',
                        xtype: 'navigation-tree'
                    }]
                }, */{
                    xtype: 'content-panel',
                    reference: 'contentPanel', 
                    region: 'center'
                }
            ];
		me.callParent(arguments);
    }
   
});
