Ext.define("Workflow.view.common.viewflow.ViewFlow", {
    extend: "Ext.tab.Panel",
    xtype: 'common-viewflow',
    controller: "common-viewflow",
    viewModel: {
        type: "common-viewflow"
    },
    ngconfig: {
        layout: 'fullScreen'
    },
    width: '100%',
    margin: 0,
    bodyBorder: false,
    instanceId: 0,
    title: '',
    initComponent: function () {
        var me = this;
        me.initToolbars();
        me.buildItems();
        me.callParent(arguments);        
    },
    loadImage: function () {
        var me = this;
        var r = me.getReferences();
        var img = r.img;
        img.setSrc('');
        me.getEl().mask("Please wait...", "x-mask-loading");
        Ext.Ajax.request({
            method: 'GET',
            url: Workflow.global.Config.baseUrl + 'api/worklists/imageflow?procInstId=' + me.instanceId,
            success: function (response, operation) {

                function _base64ToArrayBuffer(base64) {
                    var binary_string = window.atob(base64);
                    var len = binary_string.length;
                    var bytes = new Uint8Array(len);
                    for (var i = 0; i < len; i++) {
                        bytes[i] = binary_string.charCodeAt(i);
                    }
                    return bytes.buffer;
                }

                var createObjectURL = window.URL && window.URL.createObjectURL ? window.URL.createObjectURL : webkitURL && webkitURL.createObjectURL ? webkitURL.createObjectURL : null;
                if (createObjectURL) {
                    var binary = _base64ToArrayBuffer(response.responseText);
                    var blob = new Blob([binary], { type: 'image/png' });
                    var url = URL.createObjectURL(blob);
                    img.setSrc(url);
                }

                Ext.Ajax.request({
                    url: Workflow.global.Config.baseUrl + 'api/worklists/approvers?procInstId=' + me.instanceId,
                    success: function (response, operation) {
                        var content = Ext.JSON.decode(response.responseText);
                        var store = Ext.create('Ext.data.Store', {
                            fields: ['User', 'StartDate', 'FinishDate', 'Action', 'Active'],
                            data: content.participant,
                            proxy: {
                                type: 'memory',
                                reader: {
                                    type: 'json',
                                    rootProperty: 'data',
                                    totalProperty: 'total'
                                }
                            }
                        });
                        r.gd.setStore(store);

                        var store = Ext.create('Ext.data.Store', {
                            fields: ['actId', 'name'],
                            data: content.activities,
                            proxy: {
                                type: 'memory',
                                reader: {
                                    type: 'json',
                                    rootProperty: 'data',
                                    totalProperty: 'total'
                                }
                            }
                        });

                        r.act.setStore(store);
                        r.act.setValue(content.activityId);
                    },
                    failure: function (data, operation) {
                        me.getEl().unmask();
                        me.setDisabled(true);
                        var response = Ext.decode(data.responseText);
                        Ext.MessageBox.show({
                            title: 'Error',
                            msg: response.Message,
                            buttons: Ext.MessageBox.OK,
                            icon: Ext.MessageBox.ERROR
                        });
                    }
                });

                me.getEl().unmask();
            },
            failure: function (data, operation) {
                me.getEl().unmask();
                me.setDisabled(true);
                var response = Ext.decode(data.responseText);                
                Ext.MessageBox.show({
                    title: data.statusText,
                    msg: response.ExceptionMessage,
                    buttons: Ext.MessageBox.OK,
                    icon: Ext.MessageBox.ERROR
                });
            }
        });
    },
    initToolbars: function () {
        var me = this;
        me.tools = [{
            xtype: 'button',
            text: 'Refresh',
            iconCls: 'fa fa-refresh',
            disabled: false,
            handler: function () {
                me.loadImage();
            }
        }];
    },
    buildItems: function () {
        var me = this;        

        me.items = [
        {
            title: 'PROCESS',
            scrollable: true,
            iconCls: 'fa fa-sitemap',
            items: [
                {
                    xtype: 'image',
                    reference: 'img'
                }
            ]
        },
        {
            title: 'APPROVERS',
            iconCls: 'fa fa-users',
            scrollable: true,
            items: [
                {
                    xtype: 'combo',
                    fieldLabel: 'ACTIVITY',
                    labelWidth: 80,
                    width: 350,
                    reference: 'act',
                    displayField: 'name',
                    valueField: 'actId',
                    editable: false,
                    listeners: {
                        change: function (self, newValue, oldValue, eOpts) {
                            var r = me.getReferences();
                            r.gd.setLoading(true);
                            Ext.Ajax.request({
                                url: Workflow.global.Config.baseUrl + 'api/worklists/participants?procInstId=' + me.instanceId + '&actInstId=' + newValue,
                                success: function (response, operation) {
                                    var content = Ext.JSON.decode(response.responseText);
                                    var store = Ext.create('Ext.data.Store', {
                                        fields: ['User', 'StartDate', 'FinishDate', 'Action', 'Active'],
                                        data: content.data,
                                        proxy: {
                                            type: 'memory',
                                            reader: {
                                                type: 'json',
                                                rootProperty: 'data',
                                                totalProperty: 'total'
                                            }
                                        }
                                    });
                                    r.gd.setStore(store);
                                    r.gd.setLoading(false);
                                },
                                failure: function (data, operation) {
                                    r.gd.setDisabled(true);
                                    r.gd.setLoading(false);
                                    var response = Ext.decode(data.responseText);
                                    Ext.MessageBox.show({
                                        title: 'Error',
                                        msg: response.Message,
                                        buttons: Ext.MessageBox.OK,
                                        icon: Ext.MessageBox.ERROR
                                    });
                                }
                            });
                        }
                    }
                },
                {
                    xtype: 'grid',
                    border: true,
                    reference: 'gd',
                    height:'100%',
                    columns:
                        [
                            {
                                xtype: 'rownumberer'
                            },
                            {
                                text: "USER",
                                flex: 1,
                                dataIndex: 'User'
                            },
                            {
                                text: "START DATE",
                                flex: 1,
                                dataIndex: 'StartDate',
                                renderer: function (value) {
                                    return Ext.util.Format.date(value, 'Y-m-d H:i:s');
                                }
                            },
                            {
                                text: "FINISH DATE",
                                flex: 1,
                                dataIndex: 'FinishDate',
                                renderer: function (value) {
                                    return Ext.util.Format.date(value, 'Y-m-d H:i:s');
                                }
                            },
                            {
                                text: "ACTION",
                                flex: 1,
                                dataIndex: 'Action'
                            },
                            {
                                text: "ACTIVE",
                                flex: 1,
                                dataIndex: 'Active',
                                hidden: true
                            }
                        ]
                }
            ]
        }]
    }
    
});