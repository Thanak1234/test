/** AUTHOR : YIM SAMAUNE **/

Ext.define("Workflow.view.rmd.Worksheet1", {
    extend: "Workflow.view.GridComponent",
    xtype: 'rmd-worksheet1-view',
    modelName: 'worksheet1',
    collectionName: 'worksheet1s',    
    header: false,
    isNew: false,
    actionListeners: {
        beforeAdd: function (grid, datamodel) {
            // clear for new form;
            grid.isNew = true;
            grid.actStore.removeAll();
        },
        
        edit: function(grid, store, record){
            var recToUpdate = store.getById(record.id);
            var activitiesText = '',
                index = 0;
            if (grid.actStore.count() > 0) {
                grid.actStore.each(function (rec) {
                    index++;
                    activitiesText += rec.get('activity') + '|';
                });
                record.activities = activitiesText;
                recToUpdate.set(record);
            }
            grid.actStore.removeAll();
        },
        add: function (grid, store, record) {
            var activitiesText = '',
                index = 0;

            if (grid.actStore.count() > 0) {
                grid.actStore.each(function (rec) {
                    index++;
                    activitiesText += rec.get('activity') + '|';
                });
                var newRecord = store.createModel(record);
                newRecord.set('activities', activitiesText);
                store.add(newRecord);
            }
            grid.actStore.removeAll();
        }
    },
    afterSaveChange: function (grid) {
        // TODO: SOMTHING
    },
    buildGridComponent: function (component) {
        var me = this;
       
        return [{
            header: 'Business Process',
            width: 300,
            sortable: true,
            dataIndex: 'businessProcess'
        }, {
            header: 'Activities',
            flex: 1,
            sortable: true,
            dataIndex: 'activities',
            renderer: function(val, meta, record) {
                var activity = '';
                if(val){
                    var activities = val.split('|');
                }
                var index = 0;
                Ext.each(activities, function(act){
                    if(act){
                        index++;
                        activity += (index + ') ' + act + '<br/>');
                    }
                });
                return activity;
            }
        }];
    },
    buildWindowComponent: function (component) {
        var me = this;
        component.width = '50%';
        component.height = 480;
        component.labelWidth = 160;
        component.layout = 'fit';
        component.maximizable = true;

        var viewModel = this.getViewModel();
        if (viewModel) {
            var activities = viewModel.get('worksheet1.activities');
            if(activities){
                var actList = activities.split('|');
                me.actStore.removeAll();
                if (!me.isNew) { // don't add store if not new
                    Ext.each(actList, function (act) {
                        if (act) {
                            me.actStore.add({ activity: act })
                        }
                    });
                }
            }
        }
        me.isNew = false; // reset

        return [{
            xtype: 'textfield',
            fieldLabel: 'Business Process',
            allowBlank: false,
            bind: {
                value: '{worksheet1.businessProcess}'
            }
        }, {
            xtype: 'textarea',
            margin: '20 0 10',
            fieldLabel: 'Activity',
            bind: { 
                value: '{worksheet1.activity}',
                emptyText: 'Press (Ctr + Enter) to add into activity list.'
            },
            enableKeyEvents: true,
            listeners: {
                keydown : function(field, e) {
                    var vm  = component.getViewModel();
                    var key = e.getKey();
                    vm.set('worksheet1.disableBtnAdd') != (field && field.getValue());
                    
                    if (key === 13) {
                        if (e.ctrlKey) {
                            var ref = component.getReferences()['activitygrid'],
                            store = me.actStore,
                            value = field.getValue();
                            if(value){
                                store.add({activity: value});
                                vm.set('worksheet1.activity', null);
                            }
                        }
                    }
                }
            }
        }, {
            xtype: 'button',
            text: 'Add',
            margin: '0 0 5 580',
            disabled: false,
            iconCls: 'fa fa-plus-circle',
            bind: {
                disabled: '{worksheet1.disableBtnAdd}'
            },
            handler: function(){
                var vm  = component.getViewModel(),
                ref = component.getReferences()['activitygrid'],
                store = me.actStore,
                value = vm.get('worksheet1.activity');
                if(value){
                    store.add({activity: value});
                    vm.set('worksheet1.activity', null);
                }
            }
        } ,{
            xtype: 'grid',
            border: true,
            reference: 'activitygrid',
            store: me.actStore,

            columns: [
                { text: 'Activities',  dataIndex: 'activity', flex: 1 },
                {
                    xtype:'actioncolumn',
                    text: 'Action',
                    width:80,
                    items: [{
                        iconCls: 'fa fa-pencil-square-o',
                        tooltip: 'Edit',
                        handler: function(grid, rowIndex, colIndex) {
                            var vm  = component.getViewModel();
                            var rec = grid.getStore().getAt(rowIndex);
                            Ext.MessageBox.show({
                                title: 'Edit - Activity',
                                msg: '&nbsp;',
                                width:300,
                                value: rec.get('activity'),
                                buttons: Ext.MessageBox.OKCANCEL,
                                multiline: true,
                                fn: function(btn, text) {
                                    if(btn == 'ok'){
                                        rec.set('activity', text);
                                    }
                                },
                                animateTarget: grid
                            });
                        }
                    },{
                        iconCls: 'fa fa-times',
                        tooltip: 'Delete',
                        handler: function(grid, rowIndex, colIndex) {
                            Ext.MessageBox.confirm('Confirm', 'Are you sure you want to do that?', function(btn){
                                console.log('text', btn);
                                if(btn == 'yes'){
                                    grid.getStore().removeAt(rowIndex);
                                }
                            }, this);
                        }
                    }]
                }
            ],
            height: 150,
            layout: 'fit',
            fullscreen: true
        }];
    },
    actStore: Ext.create('Ext.data.Store', {data : []})
});