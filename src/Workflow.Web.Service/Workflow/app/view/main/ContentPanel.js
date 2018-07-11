Ext.define('Ext.ux.TabContextMenu', {
    extend: 'Ext.ux.TabCloseMenu',
    alias: 'plugin.tabcontextmenu',
    fullScreenTabText: 'Full Screen',
    showFullScreen: true,
    onContextMenu : function(event, target){
        var me = this,
            menu = me.createMenu(),
            disableAll = true,
            disableOthers = true,
            tab = me.tabBar.getChildByElement(target),
            index = me.tabBar.items.indexOf(tab);
 
        me.item = me.tabPanel.getComponent(index);
        menu.child('#close').setDisabled(!me.item.closable);
 
        if (me.showCloseAll || me.showCloseOthers) {
            me.tabPanel.items.each(function(item) {
                if (item.closable) {
                    disableAll = false;
                    if (item !== me.item) {
                        disableOthers = false;
                        return false;
                    }
                }
                return true;
            });
 
            if (me.showCloseAll) {
                menu.child('#closeAll').setDisabled(disableAll);
            }
 
            if (me.showCloseOthers) {
                menu.child('#closeOthers').setDisabled(disableOthers);
            }
        }

        if(me.showFullScreen){
            menu.child('#fullScreenTab').setDisabled(!(me.item.ngconfig && me.item.ngconfig.layout === 'fullScreen'));
        }
 
        event.preventDefault();
        me.fireEvent('beforemenu', menu, me.item, me);
 
        menu.showAt(event.getXY());
    },
    createMenu : function() {
        var me = this;
 
        if (!me.menu) {
            var items = [{
                itemId: 'close',
                text: me.closeTabText,
                scope: me,
                handler: me.onClose
            }];
 
            if (me.showCloseAll || me.showCloseOthers) {
                items.push('-');
            }
 
            if (me.showCloseOthers) {
                items.push({
                    itemId: 'closeOthers',
                    text: me.closeOthersTabsText,
                    scope: me,
                    handler: me.onCloseOthers
                });
            }
 
            if (me.showCloseAll) {
                items.push({
                    itemId: 'closeAll',
                    text: me.closeAllTabsText,
                    scope: me,
                    handler: me.onCloseAll
                });
            }

            if (me.showFullScreen) {
                items.push({
                    itemId: 'fullScreenTab',
                    text: me.fullScreenTabText,
                    scope: me,
                    handler: me.onFullScreenTab
                });
            }
 
            if (me.extraItemsHead) {
                items = me.extraItemsHead.concat(items);
            }
 
            if (me.extraItemsTail) {
                items = items.concat(me.extraItemsTail);
            }
 
            me.menu = Ext.create('Ext.menu.Menu', {
                items: items,
                listeners: {
                    hide: me.onHideMenu,
                    scope: me
                }
            });
        }
 
        return me.menu;
    },
    onFullScreenTab: function(){
        var item = this.item;
        if(item.ngconfig && item.ngconfig.layout === 'fullScreen'){
            var window = new Ext.Window({
                title: item.title,
                scrollbars: 1,
                iconCls: 'tree-navigation-form',
                layout: 'fit',					
                closeAction: 'hide',
                maximized: true,
                scope: this,
                items: [{
                    xtype: item.ngconfig.xtype
                }]
            });
            window.show(item);
        }  
    }
});
/**
 * 
 *Author : Phanny 
 */
Ext.define("Workflow.view.main.ContentPanel",{
    extend: "Ext.tab.Panel",
    xtype: 'content-panel',
    ui: 'ng-tab-panel-ui',
    cls: 'ng-tab-panel-ui',
    //cls: 'workflow-placeholder',
    plugins: ['tabcontextmenu'],
    showFullScreen: false,
    header: {
        hidden: true
    },
    title: 'content panel',
    scrollable: true,
    listeners: {
        tabchange: 'oncontentTabSelectionChange',
        beforeremove: function () {
            //window.history.back();
        }
    }
});
