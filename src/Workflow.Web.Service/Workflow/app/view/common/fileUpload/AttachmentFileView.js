Ext.define("Workflow.view.common.fileUpload.AttachmentFileView", {
    extend: "Ext.window.Window",

    title: 'Attatchment file list',

    iconCls: 'fa fa-paperclip',

    initComponent: function () {
        
        var me = this;
        var vm = me.getViewModel();
        var datas = vm.get('attachedFiles');
        var isAll = vm.get('isAll');

        var store = Ext.create('Ext.data.Store', {
            model: 'Workflow.model.common.SimpleFileUpload'
        });
        store.setData(datas);
        
        me.items = [{
            xtype: 'grid',
            border: true,
            width: 800,
            height: 200,
            store : store,
            viewConfig: {
                listeners: {
                    cellclick: function (view, cell, cellIndex, record, row, rowIndex, e) {
                        
                        e.preventDefault();
                        
                        if(e.target.tagName == 'A'){
                           var serial = record.get('serial');

                            Ext.core.DomHelper.append(document.body, {
                                tag: 'iframe',
                                id: 'attachment_' + record.get('id'),
                                frameBorder: 0,
                                width: 0,
                                height: 0,
                                src: Workflow.global.Config.baseUrl + 'api/forms/downloadFile?serial=' + serial
                            });
                        }
                        
                        
                    }
                }
            },
            columns: [{
                text: 'File Name ',
                dataIndex: 'fileName',
                flex: 1,
                renderer: this.renderFileName,
                sortable: false
            }, {
                text: 'Activity',
                dataIndex: 'activityName',
                width: 200,
                hidden : !isAll
            }, {
                text: 'Uploaded Date',
                dataIndex: 'createdDate',
                renderer: Ext.util.Format.dateRenderer('d/m/Y H:i:s'),
                width: 150
            }]
        }];

        this.callParent(arguments);
    },    

    renderFileName: function (value, p, record) {
        var link = '<a href="#">{0}</a>';
        return Ext.String.format(link, value);
    }

});