Ext.define("Workflow.view.GridComponent", {
    extend: "Ext.grid.Panel",
    xtype: 'grid-component',
    controller: "grid-component",
    
    viewModel: {
        type: "component"
    },
    //iconCls: 'fa fa-gears',
    bind: {
        selection: '{selectedRecord}'
    },
    viewConfig: {
        enableTextSelection: true
    },
    stateful: false,
    //collapsible: true,
    //headerBorders: false,
    getDataIndex: function (items) {
        var me = this;
        var fields = ['id'];
        Ext.each(items, function (item) {
            if (item.dataIndex) {
                //{ name: 'date',   type: 'date' },
                if(item.xtype == 'datecolumn'){
                    fields.push({ name: item.dataIndex,   type: 'date' });
                }else{
                    fields.push(item.dataIndex);
                }
            }
        });
        return fields;
    },
    listeners: {
        //rowdblclick: 'previewRecord',
        rowclick: 'selectRecord'
        // edit ( editor , context , eOpts ){
        //     console.log(editor);
        // }
    },
    actionListeners: {},
    defaultListeners: {
        beforeAdd: function(grid, datamodel){
            if(grid.actionListeners.beforeAdd){
                grid.actionListeners.beforeAdd(grid, datamodel);
            }
        },
        add: function(grid, store, record){
            if(grid.actionListeners.add){
                grid.actionListeners.add(grid, store, record);
            } else {
                console.log(record);
                store.add(record);
            }
        },
        edit: function(grid, store, record){
            if(grid.actionListeners.edit){
                grid.actionListeners.edit(grid, store, record);
            }else{
                if(!grid.editableRow){
                    var recToUpdate = store.getById(record.id);
                    recToUpdate.set(record);
                }else{
                    rowEditing = grid.plugins[0];
                    rowEditing.cancelEdit();
                    rowEditing.startEdit(record, 0);
                }
            }
        },
        remove: function(grid, store, record){
            
            if(grid.actionListeners.remove){
                grid.actionListeners.remove(grid, store, record);
            }else{
                store.remove(record);
            }
            if(grid.summaryRenderer){
                grid.summaryRenderer();
            }
        },
        rowedit: function(editor, context) {
            if(context.grid.actionListeners.rowedit){
                context.grid.actionListeners.rowedit(editor, context);
            }else{
                /* do not commit record, it will not flag record as edit */
                //context.record.commit(); 
            }

            if(context.grid.summaryRenderer){
                context.grid.summaryRenderer();
            }
        },
        canceledit: function(editor, context) {
            if(context.grid.actionListeners.canceledit){
                context.grid.actionListeners.canceledit(editor, context);
            }
        }
    },
    buildWindowComponent: function(component){
        //console.log('component', component);
    },
    afterDialogRender: function(component){
        //console.log('afterDialogRender', component);
    },
    initComponent: function () {
        var me = this;
        me.tbar = [];
        var items = me.buildGridComponent(me);
        me.tbar.push('->', {
            xtype: 'button',
            text: 'Add',
            iconCls: 'fa fa-plus-circle',
            bind: {
                disabled: me.binder('add.disabled'),
                hidden: me.binder('add.hidden')
            },
            handler: 'addRecord'
        }, {
            xtype: 'button',
            text: 'Edit',
            iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
            bind: {
                disabled: me.binder('edit.disabled'),
                hidden: me.binder('edit.hidden')
            },
            handler: 'editRecord'
        }, {
            xtype: 'button',
            text: 'View',
            iconCls: 'fa fa-eye',
            bind: {
                disabled: me.binder('view.disabled'),
                hidden: me.binder('view.hidden')
            },
            handler: 'previewRecord'
        });

        var rowEditing = Ext.create('Ext.grid.plugin.RowEditing', {
            clicksToMoveEditor: 1,
            autoCancel: false
        });

        if(me.editableRow){
            me.plugins = [rowEditing];
            me.on('edit', function(editor, context) {
                me.defaultListeners.rowedit(editor, context);
            });
            me.on('canceledit', function(editor, context) {
                me.defaultListeners.canceledit(editor, context);
            }); 
        }
        
        // add function remove to column of grid
        items.push({
            menuDisabled: true,
            hideable: false,
            sortable: false,
            width: 50,
            xtype: 'actioncolumn',
            align: 'center',
            bind: {
                disabled: me.binder('remove.disabled'),
                hidden: me.binder('remove.hidden')
            },
            items: [{
                iconCls: 'fa fa-trash-o',
                tooltip: 'Remove',
                width: 50,
                handler: 'removeRecord'
            }]
        });

        Ext.apply(this, {
            store: new Ext.data.Store({
                fields: me.getDataIndex(items)
            })
        });

        me.columns = items;

        

        me.callParent(arguments);

    },
    afterSaveChange: function(grid){

    },
    afterRender: function () {
        var me = this;
        me.callParent(arguments);
        if(me.summaryRenderer){
            me.summaryRenderer();
        }
        if(me.actionListeners.load) {
            me.actionListeners.load(this);
        }
    },
    binder: function (name) {
        var me = this;
        return '{' + me.modelName + 'Property.' + name + '}';
    }
});
