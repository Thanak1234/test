Ext.define("Workflow.view.reports.atcf.ATCFSummaryReport", {
    xtype: 'report-summary-atcf',
    extend: 'Ext.grid.Panel',
    columnLines: true,
    ngconfig: {
        layout: 'fullScreen'
    },
    viewModel: Ext.create('Ext.app.ViewModel', {
        data: {
            deptValues: null,
            deptIds: null,
            deptNames: null,
            employeeId: null,
            dateFrom: Ext.Date.add(new Date(), Ext.Date.MONTH, -1),
            dateTo: new Date(),
            fullMonthRange: Ext.Date.format(Ext.Date.add(new Date(), Ext.Date.MONTH, 0), 'F')
            /* + ' - ' + Ext.Date.format(new Date(), 'F')*/
        }
    }),
    store: {
        proxy: {
            type: 'rest',
            url: 'api/atcfreport/summary',
            reader: {
                type: 'json',
                rootProperty: 'result'
            }
        },
        sorters: {
            property: 'employee',
            direction: 'ASC'
        },
        render: function (type) {
            var me = this;
            var param = me.getProxy().extraParams;

            param.exportType = type;
            window.location.href =
                Workflow.global.Config.baseUrl +
                (me.getProxy().url) + '/export?' +
                me.serialize(param);
        },
        serialize: function (obj) {
            var str = [];
            for (var p in obj) {
                if (obj.hasOwnProperty(p)) {
                    var value = obj[p];
                    if (Object.prototype.toString.call(obj[p]) === '[object Date]') {
                        value = Ext.util.Format.date(obj[p], 'Y-m-d');
                    }
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(value));
                }
            }
            return str.join("&");
        }
    },
    title: '',
    columnWidth: 70,
    initComponent: function () {
        var me = this;


        this.dockedItems = [{
            xtype: 'toolbar',
            dock: 'top',

            items: [ {
                xtype: 'monthfield',
                reference: 'month',
                format: 'F, Y',
                emptyText: 'From Month',
                reference: 'monthFrom',
                bind: {
                    value: '{dateFrom}'
                },
                hidden: true,
                allowBlank: false,
                listeners: {
                    select: function (picker, value) {
                        var viewmodel = me.getViewModel();
                        viewmodel.set('dateTo', Ext.Date.add(value, Ext.Date.MONTH, 1));
                    }
                }
            }, {
                hidden: true,
                text: 'to'
            }, {
                xtype: 'monthfield',
                reference: 'month',
                format: 'F, Y',
                emptyText: 'To Month',
                reference: 'monthTo',
                allowBlank: false,
                hidden: false,
                bind: {
                    value: '{dateTo}'
                },
                listeners: {
                    select: function (picker) {
                        console.log('select - dateto');
                    }
                }
            }, {
                xtype: 'deptcombo',
                queryMode: 'local',
                minChars: 0,
                forceSelection: true,
                editable: true,
                flex: 1,
                fieldLabel: null,
                afterClear: function (combo) {
                    var viewmodel = me.getViewModel();
                    viewmodel.set('deptIds', null);
                    viewmodel.set('deptNames', null);
                },
                listeners: {
                    select: function (combo, records) {
                        var viewmodel = me.getViewModel();
                        var deptList = [], deptFullnames = [];
                        Ext.Array.each(records, function (record, index) {
                            //deptList.push(record.get('id'));
                            deptFullnames.push(record.get('fullName'));
                        });

                        //viewmodel.set('deptIds', deptList.join(','));
                        viewmodel.set('deptNames', deptFullnames.join(','));
                    }
                },
                store: {
                    autoLoad: true,
                    proxy: {
                        type: 'rest',
                        url: Workflow.global.Config.baseUrl + 'api/employee/department-right',
                        reader: {
                            type: 'json',
                            rootProperty: 'data'
                        }
                    }
                },
                bind: {
                    value: '{deptValues}'
                }
            }, {
                emptyText: 'EMPLOYEE <ALL>',
                xtype: 'employeePickup',
                flex: 1,
                includeInactive: true,
                includeGenericAcct: true,
                loadCurrentUser: false,
                afterClear: function(combo){
                    var viewmodel = me.getViewModel();
                    viewmodel.set('employeeId', null);
                },
                listConfig: {
                    minWidth: 250,
                    resizable: true,
                    loadingText: 'Searching...',
                    emptyText: 'No matching posts found.',
                    itemSelector: '.search-item',

                    // Custom rendering template for each item
                    itemTpl: ['<span>{employeeNo} - {fullName}</span>']
                },
                bind: {
                    value: '{employeeId}',
                    selection: '{employee}'
                }
            }, {
                xtype: 'button',
                iconCls: 'fa fa-search',
                handler: function () {
                    me.loadData('json');
                }
            }, {
                xtype: 'button',
                iconCls: 'fa fa-file-excel-o',
                handler: function () {
                    me.loadData('xls');
                }
            }]
        }];

        this.columns = [{
            text: 'Employee',
            dataIndex: 'employee',
            locked: true,
            width: 280,
            sortable: true
        }, {
            columns: me.buildDayColumns(),
            bind: {
                text: '{fullMonthRange}'
            }
        }, {
            text: 'Total',
            dataIndex: 'total',
            locked: false,
            width: 80
        }];
        

        me.callParent(arguments);
        //me.loadData();
    },

    loadData: function(type){
        var viewmodel = this.getViewModel(), data = viewmodel.getData();
        var store = this.getStore(), proxy = store.getProxy()
        
        var deptValues = viewmodel.get('deptValues');
        if (deptValues) {
            viewmodel.set('deptIds', deptValues.join(','));
        } else {
            viewmodel.set('deptIds', '');
            viewmodel.set('deptNames', '');
        }
        
        
        console.log('data-criteria', data);
        if (this.isValid()) {
            viewmodel.set('fullMonthRange',
                Ext.Date.format(data.dateTo, 'F')
            );
            proxy.extraParams = data;
            if (type == 'xls') {
                store.render(type);
            } else {
                store.load();
            }
        }
    },
    isValid: function(){
        var viewmodel = this.getViewModel(), data = viewmodel.getData();

        if (data && data.dateFrom && data.dateTo
            //&& (Ext.Date.diff(data.dateFrom, data.dateTo, Ext.Date.MONTH) >= 0)
            ) {
            return true;
        } else {
            Ext.MessageBox.alert('Warning',
                'You are about to query an invalid date range!',
                function () {
          
            }, this);

            return false;
        }
    },
    /* Override to fixed extjs bug - as work around */
    syncHeaderVisibility: function(){
        //me.callParent(arguments); 
    },
    buildDayColumns: function () {
        var me = this, columns = [];
        for (var i = 1; i <= 31; i++) {
            columns.push({
                text: i.toString(),
                dataIndex: ('_' + i.toString()),
                width: me.columnWidth
            })
        }
        return columns;
    }
});
