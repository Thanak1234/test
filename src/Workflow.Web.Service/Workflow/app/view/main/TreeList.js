/**
 *Author : Phanny 
 *
 */
Ext.define("Workflow.view.main.TreeList",{
    extend: "Ext.tree.Panel",
    xtype: 'navigation-tree',
    //id: 'navigation-tree',
    reference: 'navigationTreeList',
    title: 'Forms',
    ui: 'navigation',
    rootVisible: false,
    lines: false,
    useArrows: true,
    hideHeaders: true,
    collapseFirst: false,
    width: 250,
    minWidth: 100,
    height: 200,
    split: true,
    stateful: true,
    iconCls: 'fa fa-folder-open',
    stateId: 'mainnav.west',
    collapsible: true,
    
    columns: [{
        xtype: 'treecolumn',
        flex: 1,
        
        dataIndex: 'text',
        scope: 'controller',
        renderer: 'treeNavNodeRenderer'
    }],
    // bind: {
    //     selection: '{selectedView}'
    // },

    // store: 'NavigationTree',
    store : Ext.create('Workflow.store.Navigation'),
    
    root: {
        loaded: true
    },
  
    listeners: {
        selectionchange: 'onNavigationTreeSelectionChange'
    },
    
    dockedItems: [{
        xtype: 'textfield',
        reference: 'navtreeFilter',
        dock: 'top',
        emptyText: 'Search',

        triggers: {
            clear: {
                cls: 'x-form-clear-trigger',
                handler: 'onNavFilterClearTriggerClick',
                hidden: true,
                scope: 'controller'
            },
            search: {
                cls: 'x-form-search-trigger',
                weight: 1,
                handler: 'onNavFilterSearchTriggerClick',
                scope: 'controller'
            }
        },

        listeners: {
            change: 'onNavFilterFieldChange',
            
            buffer: 300
        }
    }]
});
