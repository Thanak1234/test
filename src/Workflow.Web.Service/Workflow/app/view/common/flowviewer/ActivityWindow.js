Ext.define("Workflow.view.common.flowviewer.ActivityWindow", {
    extend: 'Ext.ux.Window',
    title: 'Activity',
    layout: 'border',
    width: 800,
    minWidth: 350,
    height: 450,
    closable: true,
    maximizable: true,
    modal: true,
    initComponent: function () {
        var me = this;
        me.buildItems();
        me.buildButtons();
        me.callParent(arguments);
    },
    buildItems: function () {
        console.log('store', this.procInstId);
        var me = this;
        me.items = [{
            region: 'center',
            xtype: 'tabpanel',
            ui: 'ng-tab-panel-ui',
            cls: 'ng-tab-panel-ui',
            items: [{
                xtype: 'grid',
                title: 'Approvers',
                store: me.store,
                cls: 'ng-tab-item-ui',
                anchor: '100%, 100%',
                scrollable: true,
                columns: [{
                    xtype: 'rownumberer'
                }, {
                    text: "USER",
                    flex: 1,
                    dataIndex: 'User',
                    renderer: function (value) {
                        return value.replace(/K2:NAGAWORLD\\/ig, '').toUpperCase();
                    }
                }, {
                    text: "START DATE",
                    flex: 1,
                    dataIndex: 'StartDate',
                    renderer: function (value) {
                        return Ext.util.Format.date(Ext.Date.parse(value, "Y-m-d H:i:s"), 'Y-m-d H:i:s');
                    }
                }, {
                    text: "FINISH DATE",
                    flex: 1,
                    dataIndex: 'FinishDate',
                    renderer: function (value) {
                        return Ext.util.Format.date(Ext.Date.parse(value, "Y-m-d H:i:s"), 'Y-m-d H:i:s');
                    }
                }, {
                    text: "ACTION",
                    flex: 1,
                    dataIndex: 'Action'
                }, {
                    text: "ACTIVE",
                    flex: 1,
                    dataIndex: 'Active',
                    hidden: true
                }]
            }]
        }];
    },
    buildButtons: function() {
        var me = this;
        me.buttons = [{
            text: 'Close',
            handler: function () {
                me.close();
            }
        }];
    }
});