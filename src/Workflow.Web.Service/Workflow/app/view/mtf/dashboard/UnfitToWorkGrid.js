Ext.define('Workflow.view.mtf.dashboard.UnfitToWorkGrid', {
    extend: 'Ext.grid.Panel',
    xtype: 'mtf-patient-utw-panel',
    viewModel: Ext.create('Ext.app.ViewModel'),
    //height: 400,
    viewConfig: {
        loadMask: false
    },
    autoLoad: true,
    initComponent: function() {
        var me = this,
            viewmodel = me.getViewModel();

        me.store = new Ext.create('Ext.data.Store', {
            autoLoad: me.autoLoad,
            proxy: {
                type: 'rest',
                extraParams: viewmodel.get('params'),
                url: Workflow.global.Config.baseUrl + 'api/mtfrequest/patient-dashboard-utw',
                reader: {
                    type: 'json'
                }
            },
            pageSize: 100
        });
        
        me.columns = [{
            text: 'Nº',
            width: 50,
            align: 'center',
            hidden: false,
            xtype: 'rownumberer'
        }, {
            text: "REQUEST NO",
            menuDisabled: true,
            sortable: false,
            width: 100,
            dataIndex: 'folio',
            renderer: function (value, metaData, record) {
                return value;
            }
        }, {
            text: "EMPLOYEE",
            menuDisabled: true,
            sortable: false,
            flex: 1,
            minWidth: 200,
            dataIndex: 'patient'
        }, {
            text: "POSITION",
            menuDisabled: true,
            sortable: false,
            flex: 1,
            minWidth: 200,
            dataIndex: 'position'
        }, {
            text: "DEPARTMENT",
            menuDisabled: true,
            sortable: false,
            flex: 1,
            dataIndex: 'department'
        }, {
            text: "WORK SHIFT",
            menuDisabled: true,
            sortable: false,
            minWidth: 150,
            dataIndex: 'workShift'
        }, {
            text: "DOCTOR",
            menuDisabled: true,
            sortable: false,
            flex: 1,
            minWidth: 150,
            dataIndex: 'lastActionBy'
        }, {
            text: 'APPROVAL TIME',
            menuDisabled: true,
            sortable: false,
            width: 150,
            renderer: Ext.util.Format.dateRenderer('H:i:s'),
            dataIndex: 'lastActionDate'
        }];

        me.callParent(arguments);
    },
    buildColumnAction: function (data) {
        var me = this;

        return [{
            text: 'Open',
            iconCls: 'fa fa-folder-open',
            hidden: true,
            bind: {
                hidden: '{action.hidden.open}'
            },
            handler: function (button) {
                
            }
        }];
    }
});