/* global Ext */
Ext.define('Workflow.view.common.fileUpload.FileUploadController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.common-fileupload-fileupload',
    
    
    config: {
        control: {
            '*': {
                loadData: 'loadData',
                onDataClear: 'clearData'
            }      
        }
    },
    
    loadData: function (data) {
        var me = this;

        var dataStore = this.getView().getViewModel().getStore('dataStore');

        if (data.data && data.data.length > 0) {
            dataStore.setData(data.data);
        }
        
        me.getView().setStore(dataStore);
        me.getView().getViewModel().set('viewSetting', data.viewSetting);
        me.getView().getViewModel().set('activity', data.activity);

    },
    clearData: function () {
        this.getView().getStore().removeAll();
        this.getView().getStore().clearData();
    },
    viewFile: function(grid, record, tr, rowIndex, e, eOpts ){
          
        var me = this;
        
        var field = record.getData();
		if(field){
			field.downloadLink = '<span style="margin-right:50px;">Download:</span><a href="' + me.getDownloadLink(grid, record) + '">' + field.fileName + '</a>';
			field.uploadDate = new Date(field.uploadDate);
		}

        var window = Ext.create('Workflow.view.common.fileUpload.FileUploadWindow', 
         {mainView: me,
         viewModel: {
            data: {
                action: 'VIEW',
                item: field
            }
        }, 
        lauchFrom: me.getReferences().editBtn,
        cbFn:function(record){  
             
            // add info files uploads
            me.getView().getStore('fileUpload').add(record);
            me.showToast(Ext.String.format('File name {0} has been update', record.get('name')));
            
        }});
        
        window.show(me.getReferences().editBtn);
    },
    
    addNewFile: function(){
        var me=this,
            window = Ext.create('Workflow.view.common.fileUpload.FileUploadWindow',
                {mainView: me,
                viewModel: {
                    data: {
                        action: 'ADD'
                        //item : Ext.create('Workflow.model.common.FileUpload')
                    }
                }, 
                lauchFrom: me.getReferences().addBt,
                cbFn: function (rec) {
                    var activity = me.getView().getViewModel().get('activity');
                    rec.set('readOnly', false);
                    rec.set('activity', activity);
                    rec.set('uploadDate', new Date());
                    me.getView().getStore().add(rec);
                    me.showToast(Ext.String.format('File name {0} has been added', rec.get('name')));
                    
                }});
        
        window.show(me.getReferences().addBt);
    },
    
    removeFile: function(grid, rowIndex, colIndex) {
        
        var me = this, store = grid.getStore(), rec = store.getAt(rowIndex);


        if (rec.get('readOnly')) {
            Ext.MessageBox.show({
                title: 'Error',
                msg: 'This record cannot be removed. Since it was uploaded by another activity.',
                buttons: Ext.MessageBox.OK,
                icon: Ext.MessageBox.ERROR
            });
            return;
        }
        Ext.MessageBox.show({
            title: 'Alert',
            msg: 'Are you sure to delete this file?',
            buttons: Ext.MessageBox.YESNO,
            scope: this,
            fn: function(bt){
                if(bt==='yes') {
                   store.remove(rec);
                   me.showToast(Ext.String.format('File name {0} has been removed', rec.get('name')));
                }
            } 
        });
    },
     
    dowloadFile: function(grid, rowIndex, colIndex){
        var me = this;
        var record = grid.getStore().getAt(rowIndex);

        Ext.core.DomHelper.append(document.body, {
            tag: 'iframe',
            id: 'attachment_' + record.get('id'),
            frameBorder: 0,
            width: 0,
            height: 0,
            src: me.getDownloadLink(grid, record)
        });
    },
    getDownloadLink: function (grid, record, view) {
        var id = record.get('id');
        var requestHeaderId = record.get('requestHeaderId');
        var isTemp = record.get('isTemp');
        var processCode = this.getView().processCode;

        var attachmentId = [id, requestHeaderId].join('_');
        if (isTemp) {
            attachmentId = id;
        }

        return Workflow.global.Config.baseUrl + 'api/forms/attachments/download?attachmentId=' + attachmentId + '&isTemp=' + isTemp + '&processCode=' + processCode;
    }
});
