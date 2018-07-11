Ext.define("Workflow.view.ticket.setting.SettingView",{
    extend: "Ext.tab.Panel",
    layout: 'center',
    ui: 'ng-tab-panel-ui',
    cls: 'ng-tab-panel-ui',
    requires: [
        "Workflow.view.ticket.setting.SettingViewController",
        "Workflow.view.ticket.setting.SettingViewModel"
    ],

    controller: "ticket-setting-settingview",
    viewModel: {
        type: "ticket-setting-settingview"
    },

    width: 970,
    
    items: [
    {
        rtl: false,
        title: 'Item',
        xtype: 'ticket-setting-item-panel',
        border: false,
        //cls: 'ng-tab-item-ui',
        iconCls: 'fa fa-plug',
        listeners: {
          activate: 'loadTabData'
        }
    }, {
        title: 'Sub Category',
        xtype: 'ticket-setting-subcategory-panel',
        border: false,
        //cls: 'ng-tab-item-ui',
        iconCls: 'fa fa-object-ungroup',
        listeners: {
          activate: 'loadTabData'
        }
    },{
        title: 'Category',
        xtype: 'ticket-setting-category-panel',
        border: false,
        //cls: 'ng-tab-item-ui',
        iconCls: 'fa fa-object-group',
        listeners: {
          activate: 'loadTabData'
        }
    },
    {
        title: 'Department',
        xtype: 'ticket-setting-department-panel',
        border: false,
        //cls: 'ng-tab-item-ui',
        iconCls: 'fa fa-university',
        listeners: {
          activate: 'loadTabData'
        }
    },{
        title: 'Group Policy',
        xtype: 'ticket-setting-grouppolicy-panel',
        border: false,
        //cls: 'ng-tab-item-ui',
        iconCls: 'fa fa-gg',
        listeners: {
          activate: 'loadTabData'
        }
    },
    {
        title: 'Agent',
        xtype: 'ticket-setting-agent-panel',
        border: false,
        //cls: 'ng-tab-item-ui',
        iconCls: 'fa fa-street-view',
        listeners: {
          activate: 'loadTabData'
        }
    }
    ,{
        title: 'Team',
        xtype: 'ticket-setting-team-panel',
        border: false,
        //cls: 'ng-tab-item-ui',
        iconCls: 'fa fa-qrcode',
        listeners: {
          activate: 'loadTabData'
        }
    },
    {
        title: 'SLA',
        xtype: 'ticket-setting-sla-panel',
        border: false,
        //cls: 'ng-tab-item-ui',
        iconCls: 'fa fa-retweet',
        listeners: {
          activate: 'loadTabData'
        }
    },
    {
        title: 'Priority',
        xtype: 'ticket-setting-priority-panel',
        border: false,
        //cls: 'ng-tab-item-ui',
        iconCls: 'fa fa-sort-amount-desc',
        listeners: {
          activate: 'loadTabData'
        }
    }
    ]
});
