Ext.define('Workflow.view.mtf.UnfitToWorkWindow', {
    extend: 'Ext.window.Window',
    xtype: 'mtf-unfittowork-window',
    title: 'Add Unfit To Work - Day',
    width: 350,
	height: 250,
	modal: true,
	items: [{
	    xtype: 'form',

	    defaultType: 'textfield',
	    fieldDefaults: {
	        labelWidth: 70
	    },

	    layout: {
	        type: 'vbox',
	        align: 'stretch'
	    },

	    bodyPadding: 5,
	    border: false,

	    items: [{
	        xtype: 'combo',
	        name: 'Status',
	        fieldLabel: 'Status',
	        value: 'Leave',
	        store: Ext.create('Ext.data.Store', {
	            fields: ['name', 'label'],
	            data: [
                    { "name": "Leave", "label": "Leave" },
                    { "name": "Rest", "label": "Rest" },
                    { "name": "PH", "label": "PH" }
	            ]
	        }),
	        displayField: 'label',
	        valueField: 'name',
	        allowBlank: false,
	        margin: '5 0 10 0'
	    }, {
	        name: 'UtwDateFrom',
	        fieldLabel: 'Date From',
	        allowBlank: false,
	        value: new Date(),
	        xtype: 'datefield'
	    }, {
	        fieldLabel: 'Day Range',
	        name: 'NumOfDayLeave',
            value: 1,
            minValue: 1,
	        allowBlank: false,
            allowDecimals: false,
	        xtype: 'numberfield'
	    }]
	}]
});
/** AUTHOR : YIM SAMAUNE **/
Ext.define("Workflow.view.mtf.UnfitToWorkView", {
    extend: "Workflow.view.GridComponent",
    xtype: "mtf-unfittowork-view",
    title: 'UNFIT TO WORK',
    modelName: 'unfittowork',
    collectionName: 'unfitToWorks',
    duplicate: false,
    actionListeners: {
        // beforeAdd: function(datamodel){
        //     datamodel.unfittowork = {
        //         Status: 'Leave',
        //         UtwDateFrom: new Date(),
        //         UtwDateTo: new Date()
        //     };
        // },
        add: function (grid, store, record) {
            var window = null;
            var window = Ext.create('Workflow.view.mtf.UnfitToWorkWindow', {
                alias: 'window-dialog-unfitToWorks',
                lauchFrom: grid,
                buttons: [{
                    text: 'Add',
                    handler: function (btn, eOpts) {
                        var window = btn.up('window'),
                            form = window.down('form');

                        if (form.isValid()) {
                            var record = form.getForm().getValues();
                            var records = [];
                            for (var i = 0; i < record.NumOfDayLeave; i++) {
                                var utwDateFrom = new Date(record.UtwDateFrom);
                                var dateForm = Ext.Date.add(utwDateFrom, Ext.Date.DAY, i);
                                var hasRecord = false;
                                
                                store.each(function (record) {
                                    if (Ext.Date.format(dateForm, 'm/d/Y') == Ext.Date.format(record.get('UtwDate'), 'm/d/Y')) {
                                        hasRecord = true;
                                    }
                                });
                                if(!hasRecord) {
                                    var newRecord = store.createModel({
                                        Status: record.Status,
                                        UtwDate: Ext.Date.format(dateForm, 'm/d/Y'),
                                        NoDay: 1,
                                        Exists: 0,
                                        Folio: null
                                    });
                                    records.push(newRecord);
                                }
                            }

                            store.add(records);
                            store.sort('UtwDate', 'DESC');
                            grid.summaryRenderer();
                            form.reset();
                            window.close();
                        }
                    }
                }, {
                    text: 'Close',
                    handler: function () { this.up('window').close(); }
                }]
            });
            window.show();
        }
    },
    summaryRenderer: function() {
        var me = this,
            data = me.store.getData(),
            viewmodel = me.getViewModel();

        var statusArray = {
                Leave: 0,
                Rest: 0,
                PH: 0,
                Total: 0
            };

        var summaryText = '';

        if(data){
            var mainView = me.mainView,
                mainData = mainView.getViewModel().getData();

            if (mainData) {
                me.loadUnfitList(mainData.requestorId, mainData.requestHeaderId, function (records) {

                    me.duplicate = false;
                    Ext.each(data.items, function (record) {
                        record.set('Exists', null);
                        record.set('Folio', null);
                        
                        if (record.get('Status') == 'Leave') {
                            statusArray['Leave'] += record.get('NoDay');
                        } else if (record.get('Status') == 'Rest') {
                            statusArray['Rest'] += record.get('NoDay');
                        } else if (record.get('Status') == 'PH') {
                            statusArray['PH'] += record.get('NoDay');
                        }
                        statusArray['Total'] += record.get('NoDay');
                    });


                    summaryText = Ext.String.format('{0}{1}{2}{3}',
                        statusArray.Leave > 0 ? (statusArray.Leave + ' (Leave)') : '',
                        statusArray.PH > 0 ? (' ' + statusArray.PH + ' (PH)') : '',
                        statusArray.Rest > 0 ? (' ' + statusArray.Rest + ' (Rest)') : '',
                        statusArray.Total > 0 ? (' total is ' + statusArray.Total) + ' day(s)' : ''
                    );

                    Ext.each(records, function (record) {
                        var utwDate = new Date(record['utwDate']);
                        var duplicate = me.store.findRecord('UtwDate', utwDate);

                        if (duplicate) {
                            duplicate.beginEdit();
                            duplicate.set('Exists', record['processInstanceId']);
                            duplicate.set('Folio', record['title']);
                            duplicate.endEdit();
                            me.duplicate = true;
                        }

                    });
                });
            }
        }

        viewmodel.set('summaryText', summaryText);
    },
    loadUnfitList: function (requestrId, requestId, callback) {
        Ext.Ajax.request({
            url: '/api/mtfrequest/unfit-towork-list?requestorId=' +
                (requestrId + '&requestId=' + requestId),

            success: function (response, opts) {
                var obj = Ext.decode(response.responseText);
                callback(obj);
            }
        });
    },
    buildGridComponent: function (component) {
        var me = this;
        component.maxHeight = 450;
        component.editableRow = true;
        component.dockedItems = [{
            xtype: 'toolbar',
            dock: 'bottom',
            items: ['->', {
                    xtype: 'label',
                    margin: '0 60 0 0',
                    bind: {
                        text: '{summaryText}'
                    }
                }
            ]
        }];

        component.tbar.push({
            xtype: 'button',
            text: 'Refresh',
            iconCls: 'fa fa-refresh',
            handler: function(){
                me.summaryRenderer();
            }
        });
        
        return [{
            header: 'STATUS',
            width: 100,
            dataIndex: 'Status',
            editor: {
                xtype: 'combo',
                editable: false,
                store: Ext.create('Ext.data.Store', {
                    fields: ['name', 'label'],
                    data: [
                        { "name": "Leave", "label": "Leave" },
                        { "name": "Rest", "label": "Rest" },
                        { "name": "PH", "label": "PH" }
                    ]
                }),
                displayField: 'label',
                valueField: 'name',
                allowBlank: false
            }
        }, {
            header: 'DATE',
            xtype: 'datecolumn',
            width: 120,
            dataIndex: 'UtwDate',
            renderer: function (value, metaData, rec) {
                var dateValue = Ext.Date.format(value, 'm/d/Y');
                var procInstId = rec.get('Exists'), folio = rec.get('Folio');
                if (procInstId) {
                    metaData.tdAttr = 'data-qtip="Overlap date on: ' + folio + '"';
                    return '<span style="z-index: 99999;"><a href="/#mtf-request-form/SN=' + procInstId
                        + '_99999" style="color:#cf4c35">' + dateValue
                        + '</a></span>';
                } else {
                    metaData.tdAttr = '';
                    return dateValue;
                }
            },
            //renderer:Ext.util.Format.dateRenderer('m/d/Y'),
            editor: {
                xtype: 'datefield',
                allowBlank: false,
                format: 'm/d/Y'
                // minValue: Ext.Date.format(new Date(), 'm/d/Y'),
                // minText: 'Cannot have a start date before the company existed!',
                // maxValue: Ext.Date.format(new Date(), 'm/d/Y')
            }
        }, {
            header: 'NO DAY',
            flex: 1,
            dataIndex: 'NoDay',
            editor: {
                xtype: 'numberfield',
                allowBlank: false,
                minValue: 0.5,
                maxValue: 1
            }
        }];
    },
    
    buildWindowComponent: function(component){
        var me = component;
        component.width = 420;
        component.height = 300;
        component.labelWidth = 80;
        component.closeAfterAdd = true;

        return [{
            xtype: 'combo',
            fieldLabel: 'Status',
            store: Ext.create('Ext.data.Store', {
                fields: ['name', 'label'],
                data: [
                    { "name": "Leave", "label": "Leave" },
                    { "name": "Rest", "label": "Rest" },
                    { "name": "PH", "label": "PH" }
                ]
            }),
            displayField: 'label',
            valueField: 'name',
            allowBlank: false,
            bind: {
                value: '{unfittowork.Status}'
            },
            margin: '5 0 10 0'
        }, {
            fieldLabel: 'Date From',
            xtype: 'datefield',
            name: 'vUtwDateFrom',
            validateRang: 'vUtwDateFrom',
            listeners: { change: me.validateDate },
            bind: {
                value: '{unfittowork.UtwDateFrom}'
            }
        }, {
            fieldLabel: 'Date To',
            xtype: 'datefield',
            validateRang: 'vUtwDateFrom',
            listeners: { change: me.validateDate },
            bind: {
                value: '{unfittowork.UtwDateTo}'
            }
        }];
    }
});