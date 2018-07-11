/**
*@author : Phanny
*/
Ext.define('Workflow.view.ticket.attach.AttachFilePanelController', {
    extend: 'Workflow.view.AbstractBaseController',
    alias: 'controller.ticket-attach-attachfile',

    onFileAddedHander: function (bt, e) {
        var me = this;
        var attachedFileStore =  me.getView().getStore();

        var window = Ext.create('Workflow.view.common.fileUpload.SimpleFileUploadDialog',{
                    mainView: me,
                    lauchFrom: bt,
                    cbFn: function (rec) {
                        attachedFileStore.add(Ext.create('Workflow.model.common.FileUpload', { fileName: rec.fileName, serial: rec.serial, ext: rec.ext }));
                    },
                    doClose: function (refresh) {
                        window.hide(bt, function () {
                            window.destroy();
                        });
                    }
                });

        window.show(bt);

    },
    oneRemoveFileHandler: function (grid, rowIndex, colIndex) {
        var me = this, store = grid.getStore(), rec = store.getAt(rowIndex);
        Ext.MessageBox.show({
            title: 'Alert',
            msg: 'Are you sure to delete this file?',
            buttons: Ext.MessageBox.YESNO,
            scope: this,
            fn: function (bt) {
                if (bt === 'yes') {
                    store.remove(rec);
                    me.showToast(Ext.String.format('File name {0} has been removed', rec.get('fileName')));
                }
            }
        });
    }
    
});
