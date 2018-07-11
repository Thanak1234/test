/* global Ext */
/**
 * Author: Phanny
 */
Ext.define('Workflow.view.AbstractBaseController', {
    extend: 'Ext.app.ViewController',
    requires: [
        'Ext.window.Toast'
    ],

    showToast: function (s, title) {
        Ext.toast({
            html: s,
            closable: false,
            align: 't',
            slideInDuration: 200,
            minWidth: 400
        });
    },

    getOriginDataFromCollection: function (records) {
        var newItems = []
        if (records && records.length > 0) {
            for (var i in records) {
                newItems.push(records[i].data);
            };
        }
        return newItems;
    },

    isEmptyOrSpaces: function (str) {
        return str === null || str.match(/^ *$/) !== null || !str;
    },

    showWindowDialog: function (lauchFromel, windowClass, model, title, cb) {
        console.log(lauchFromel, windowClass);
        var me = this;
        var window = Ext.create(windowClass,
         {
             title: title,
             mainView: me,
             viewModel: {
                 data: model
             },
             lauchFrom: lauchFromel,
             cbFn: cb
         });

        window.show(lauchFromel);
    },
    showWindowDialogCmp: function (el, dir, data, callback) {
        var me = this, component;
        
        if(dir.windowCmp){
            var window = Ext.create(dir.windowCmp,
            {
                title: dir.title,
                mainView: me,
                modelName: dir.modelName,
                viewModel: {
                    data: data
                },
                directive: dir,
                lauchFrom: el,
                cbFn: callback
            });
            
            window.show(el);
        }else{
            var dataStore = dir.store?dir.store.getData():null;
            var records = dataStore.items;

            var window = Ext.create('Workflow.view.WindowComponent', {
                alias: 'window-dialog-component',
                title: dir.title,
                mainView: me,
                modelName: dir.modelName,
                viewModel: {
                    data: data
                },
                store: dir.store,
                lauchFrom: el,
                cbFn: callback,
                buildFormComponent: function () {
                    component = this;
                    return dir.buildWindowComponent(this);
                }
            });
            window.show(el);
            dir.afterDialogRender(component);
        }
    },

    getRef: function () {
        var me = this;
        var view = me.getView();
        var refs = me.getView().getReferences();
        var vm = view.getViewModel();
        return {
            view: view,
            refs: refs,
            vm : vm
        }
    },
    /**
    datas = datas;
    checker = [{propName:'', prop:'prp' }, {} ,...]

    [{ propName: 'Impact', prop: 'impactId' ,isRequired: true},
    { propName: 'Estimated Hours', prop: 'estimatedHours',isRequired: false, check:function(val){
            if(val<0){
                return "Estimated Hours is always positive.";
            } else{
                return null;
            }
        }
    });
    ]
    */
    validation: function (datas, checker ) {
        if (!checker) {
            return;
        }
        var msgs  = checker.map(function(item){
            var val = datas[item.prop];
            if(!val && item.isRequired){
                return Ext.String.format('{0} is required. <br>', item.propName);
            }
            if(val && item.check){
                return item.check(val);
            }
        });


        msgs =msgs.filter(
            function( element ) {
                return element?true:false;
            });
        if (msgs.length>0) {
            throw msgs;
        }
    }

});
