Ext.define('Workflow.view.GridComponentController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.grid-component',
    viewSetting: null,
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear: 'clearData'
            }
        }
    },
    selectRecord: function () {
        var me = this,
            grid = me.getView();

        var propertyKey = (grid.modelName + 'Property');
        
        var property = {
            add: { disabled: false, hidden: false },
            edit: { disabled: false, hidden: false },
            view: { disabled: false, hidden: false },
            remove: { disabled: false, hidden: false }
        }
        me.setProperty('SELECTED');
    },
    setProperty: function (state) {
        var me = this,
            grid = me.getView(),
            store = grid.getStore(),
            viewmodel = grid.getViewModel(),
            mainVM = grid.mainView.getViewModel();

        var propertyKey = (grid.modelName + 'Property');
        var property = {
            hidden: false,
            add: { disabled: true, hidden: false },
            edit: { disabled: true, hidden: false },
            view: { disabled: true, hidden: false },
            remove: { disabled: true, hidden: true }
        };

        if(me.viewSetting){
            
            if(state == 'LOAD'){
                property = {
                    hidden: false,
                    add: { disabled: false, hidden: false },
                    edit: { disabled: true, hidden: false },
                    view: { disabled: true, hidden: false },
                    remove: { disabled: false, hidden: false }
                }
            }else if(state == 'REMOVED'){
                property = {
                    hidden: false,
                    add: { disabled: false, hidden: false },
                    edit: { disabled: true, hidden: false },
                    view: { disabled: true, hidden: false },
                    remove: { disabled: false, hidden: false }
                }
            }else if(state == 'SELECTED'){
                property = {
                    hidden: false,
                    add: { disabled: false, hidden: false },
                    edit: { disabled: false, hidden: false },
                    view: { disabled: false, hidden: false },
                    remove: { disabled: false, hidden: false }
                }
            }
            customProperty = me.viewSetting[propertyKey];
            var properties = ['add','edit','view','remove'];
            
            Ext.each(properties, function (prop) {
                if (customProperty) {
                    if (customProperty[prop].disabled) {
                        property[prop].disabled = customProperty[prop].disabled;
                    }
                    if (customProperty[prop].hidden) {
                        property[prop].hidden = customProperty[prop].hidden;
                    }
                    if (customProperty[prop].readOnly) {
                        property[prop].readOnly = customProperty[prop].readOnly;
                    }
                }
            });

            if(grid.editableRow && customProperty['edit'].disabled) { 
                grid.plugins[0].disable(true);
            }

            if (customProperty) {
                property.hidden = customProperty['hidden'];
            } else {
                property = {
                    hidden: false,
                    add: { disabled: true, hidden: false },
                    edit: { disabled: true, hidden: false },
                    view: { disabled: false, hidden: false },
                    remove: { disabled: true, hidden: true }
                }
            }
        }
        
        mainVM.set(propertyKey, property);
    },
    loadData: function (data, viewSetting) {
        var me = this,
            grid = me.getView(),
            store = grid.getStore(),
            viewmodel = me.getView().getViewModel();

        if (viewSetting) {
            me.viewSetting = viewSetting;
            me.setProperty('LOAD');
        }
        
        // TODO: expose function to view if need to modify data store before added
        var dataStore = grid.getStore();
        if (data && data[grid.collectionName]) {
            
            dataStore.setData(data[grid.collectionName]);
            grid.setStore(dataStore);
            me.afterChange(grid);
        }
    },
    clearData: function () {
        this.getView().getStore().removeAll();
    },
    addRecord: function (el) {
        var me = this,
            grid = me.getView(),
            store = grid.getStore(),
            viewmodel =  me.getView().getViewModel(),
            mainViewModel = grid.mainView.getViewModel();
        
        me.resetForm();
        var model = { action: 'ADD', viewSetting: viewmodel.get('viewSetting') };
        model[grid.modelName] = {};

        // Fire event befoer add new (form popup)
        grid.defaultListeners.beforeAdd(grid, model);
        
        if(!grid.editableRow){
            me.showWindowDialogCmp(el, grid, model, function (record) {
                var newRecord = {};
                for (key in record) {
                    if (record.hasOwnProperty(key)) {
                        newRecord[key] = record[key]; // clone and clean
                    }
                }
                
                grid.defaultListeners.add(grid, store, newRecord);
                me.afterChange(grid);
                return true;
            });
        }else{
            grid.defaultListeners.add(grid, store, model[grid.modelName]);
        }
        
    },
    editRecord: function (el) {
        var me = this,
            grid = me.getView(),
            store = grid.getStore(),
            viewmodel = me.getView().getViewModel();

        me.resetForm();
        var selectedRecord = viewmodel.get('selectedRecord');
        if (selectedRecord) {
            viewmodel.set('action', 'EDIT');
            viewmodel.set(grid.modelName, selectedRecord.getData());

            if(!grid.editableRow){
                me.showWindowDialogCmp(el, grid, viewmodel.getData(), function (record) {
                    grid.defaultListeners.edit(grid, store, record);
                    return true;
                });
            }else{
                grid.defaultListeners.edit(grid, store, selectedRecord);
            }
            me.afterChange(grid);
        }
    },
    removeRecord: function (el, rowIndex, colIndex) {
        var me = this,
            grid = me.getView(),
            store = grid.getStore(),
            record = store.getAt(rowIndex);

        me.resetForm();
        Ext.MessageBox.show({
            title: 'Confirmation',
            msg: 'Are you sure to delete this record?',
            buttons: Ext.MessageBox.YESNO,
            icon: Ext.MessageBox.QUESTION,
            scope: this,
            fn: function (bt) {
                if (bt === 'yes') {
                    //if(!grid.editableRow){
                    //    store.remove(record);
                    //}else{
                    //    grid.defaultListeners.remove(grid, store, record);
                    //}
                    grid.defaultListeners.remove(grid, store, record);
                    

                    me.setProperty('REMOVED');
                    grid.getSelectionModel().deselectAll();
                    me.afterChange(grid);
                    me.showToast(Ext.String.format('The selected record has been removed'));
                }
            }
        });
    },
    afterChange: function(grid){
        grid.afterSaveChange(grid);
    },
    previewRecord: function (el) {
        var me = this,
            grid = me.getView(),
            store = grid.getStore(),
            viewmodel = me.getView().getViewModel();
            
        me.resetForm();
        var selectedRecord = viewmodel.get('selectedRecord'),
            viewSetting = viewmodel.get('viewSetting');
        if (selectedRecord) {
            viewmodel.set(grid.modelName, selectedRecord.getData());
            viewmodel.set('action', 'VIEW');
            viewmodel.set('property.readOnly', true);

            me.showWindowDialogCmp(el, grid, viewmodel.getData(), function (record) {
                return true;
            });
        }
    },
    resetForm: function () {
        var me = this,
            viewmodel = me.getView().getViewModel();

        viewmodel.set('property.readOnly', false);
    }
});
