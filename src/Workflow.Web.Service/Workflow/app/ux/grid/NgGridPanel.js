Ext.define('Workflow.ux.grid.NgGridPanel', {
    extend: 'Ext.grid.Panel',
    xtype: 'ngGridPanel',
    config: {
        name: 'info',
        crudWinSize: {
            width: 400,
            minHeight: 400
        },
        require: false
    },
    viewModel: {
        data: {
            selectedRecord: null,
            config: null
        }
    },
    bind: {
        selection: '{selectedRecord}'
    },
    dataDefault: {
    },
    initComponent: function () {
        var me = this;
        me.buildInit();
        me.buildEvents();
        me.buildStore();
        me.buildButtons();
        me.buildColumns();
        me.callParent(arguments);
    },
    buildEvents: function () {
        var me = this;
        me.listeners = {
            rowdblclick: function (el, record, item, index, e, eOpts) {
                me.onViewClicked(el);
            },
            afterrender: function (el, eOpts) {                
                me.loadDataAfterRender();                
            },
            refresh: function (dataview) {
                Ext.each(dataview.panel.columns, function (column) {
                    if (column.autoSizeColumn === true)
                        column.autoSize();
                })
            }
        };
    },
    buildInit: function() {
        var me = this,
            main = me.up('panel[name="AE9D5A6A-FF72-44DE-BD35-ADA86DF03F0D"]'),
            mainViewModel = main.getViewModel(),
            viewmodel = me.getViewModel();
        viewmodel.set('config', mainViewModel.get('viewSetting.container'));
        me.hidden = viewmodel.get(Ext.String.format('config.{0}.hidden', this.name));
    },
    buildButtons: function () {
        var me = this;
        me.tbar = ['->', {
            xtype: 'button',
            text: 'Add',
            iconCls: 'fa fa-plus-circle',
            disabled: true,
            handler: function (el) {
                if (Ext.isFunction(me.onAddClicked)) {
                    me.onAddClicked(el);
                }
            },
            bind: {
                disabled: Ext.String.format('{!config.{0}.add}', me.name)
            }
        }, {
            xtype: 'button',
            text: 'Edit',
            disabled: true,
            bind: {
                disabled: Ext.String.format('{!(selectedRecord && config.{0}.edit)}', me.name)
            },
            iconCls: 'fa fa-plus-circlefa fa-pencil-square-o',
            handler: function (el) {
                if (Ext.isFunction(me.onEditClicked)) {
                    me.onEditClicked(el);
                }
            }
        }, {
            xtype: 'button',
            text: 'View',
            disabled: true,
            bind: {
                disabled: Ext.String.format('{!selectedRecord || !config.{0}.view}', me.name)
            },
            iconCls: 'fa fa-eye',
            handler: function (el) {
                if (Ext.isFunction(me.onViewClicked)) {
                    me.onViewClicked(el);
                }                
            }
        }];
    },    
    buildColumns: function () {
        var me = this;
        me.columns = []; 
        if (!Ext.isEmpty(me.providedFields)) {
            me.createRemoveButton();
            Ext.each(me.sortOf(me.providedFields, 'visibleIndex', 'asc'), function (item, index, array) {
                me.columns.push(me.kindOfColumn(item));
            });            
        }
    },    
    onAddClicked: function (el) {
        var me = this,
            store = me.getStore();        
        var crudWin = me.buildCrudWinForm(null, 'create', function (record) {
            record.id = null;
            var model = store.add(record);
            me.createMainData();
        });
        crudWin.show(el);
    },
    onEditClicked: function (el) {
        var me = this,
            viewmodel = me.getViewModel(),
            selectedRecord = viewmodel.get('selectedRecord'),
            tempRecord = Ext.apply({}, selectedRecord.getData());
        var crudWin = me.buildCrudWinForm(tempRecord, 'modification', function (record) {
            selectedRecord.setId(record.id);
            record.phantom = true;
            me.updateRecord(selectedRecord, record);
            me.createMainData();
        });
        crudWin.show(el);
    },
    onViewClicked: function (el) {
        var me = this,
            viewmodel = me.getViewModel(),
            selectedRecord = viewmodel.get('selectedRecord'),
                record = selectedRecord.getData();
        var crudWin = me.buildCrudWinForm(record, 'preview', function () {
        });
        crudWin.show(el);
    },
    onRemoveClicked: function (record) {
        var me = this,
            store = me.getStore();
        Ext.MessageBox.show({
            title: 'Warning',
            msg: 'Are you sure to delete this record?',
            buttons: Ext.MessageBox.YESNO,
            icon: Ext.MessageBox.WARNING,
            fn: function (btn) {
                if (btn === 'yes') {
                    store.remove(record);
                    me.createMainData();
                }
            }
        });
    },
    /*
    * @private
    */
    updateRecord: function(oRecord, nRecord) {
        Ext.Object.each(nRecord, function (key, value, el) {
            try {
                oRecord.set(key, value);
            } catch (e) {
            }
        });
    },
    /*
    * @private
    */
    createRemoveButton: function () {
        var me = this;
        me.columns.push({
            menuDisabled: true,
            sortable: false,
            width: 50,
            xtype: 'actioncolumn',
            align: 'center',
            bind: {
                hidden: Ext.String.format('{!config.{0}.remove}', me.name)
            },
            items: [{
                iconCls: 'fa fa-trash-o',
                tooltip: 'Remove',
                width: 50,
                handler: function (grid, rowIndex, colIndexe) {
                    var store = me.getStore(),
                    record = store.getAt(rowIndex);
                    me.onRemoveClicked(record);                    
                }
            }]
        });
    },
    /*
    * @private
    */
    uuid: function () {
        var d = new Date().getTime();
        var uuid = 'xxxxxxxx-xxxx-4xxx-yxxx-xxxxxxxxxxxx'.replace(/[xy]/g, function (c) {
            var r = (d + Math.random() * 16) % 16 | 0;
            d = Math.floor(d / 16);
            return (c == 'x' ? r : (r & 0x3 | 0x8)).toString(16);
        });
        return uuid;
    },
    createMainData: function () {
        var me = this,
            main = me.up('panel[name="AE9D5A6A-FF72-44DE-BD35-ADA86DF03F0D"]'),
            viewmodel = main.getViewModel(),
            store = me.getStore();

        viewmodel.set(Ext.String.format('{0}NewRecords', me.name), me.getRawRecordData(store.getNewRecords()));
        viewmodel.set(Ext.String.format('{0}UpdatedRecords', me.name), me.getRawRecordData(store.getUpdatedRecords()));
        viewmodel.set(Ext.String.format('{0}RemovedRecords', me.name), me.getRawRecordData(store.getRemovedRecords()));
    },

    /*
    * @private
    */
    getRawRecordData: function (records) {
        var newItems = []
        if (records && records.length > 0) {
            Ext.each(records, function (record) {
                if (record.data) {
                    if (isNaN(parseInt(record.data.id))) {
                        record.data.id = 0;
                    }
                    newItems.push(record.data);
                }
            });
        }
        return newItems;
    },
    loadDataAfterRender: function () {
        var me = this,
            main = me.up('panel[name="AE9D5A6A-FF72-44DE-BD35-ADA86DF03F0D"]'),
            mainViewModel = main.getViewModel(),
            viewmodel = me.getViewModel();
        var dataField = Ext.String.format('{0}Records', me.name);
        var records = mainViewModel.get(dataField);
        if (!Ext.isEmpty(records) && records.length > 0) {
            me.getStore().setData(records);
        }
    },
    buildStore: function() {
        var me = this,
            fields = [];       

        if (!Ext.isEmpty(me.providedFields)) {
            Ext.each(me.sortOf(me.providedFields, 'visibleIndex', 'asc'), function (item, index, array) {
                fields.push({ name: item.name, type: 'auto' });
            });
        }

        var store = Ext.create('Ext.data.Store', {
            fields: fields
        });

        me.store = store;
    },
    buildCrudWinForm: function (record, operation, fn) {
        var me = this,
            crudWindow = null,
            buttons = [{
                text: 'Close',
                handler: function (el) {
                    if (!Ext.isEmpty(crudWindow))
                        crudWindow.close();
                }
            }];

        if (operation != 'preview') {
            Ext.Array.insert(buttons, 0, [{
                text: operation == 'create' ? 'Save': 'Save Change',
                handler: function (el) {
                    var container = crudWindow.down('form'),
                        viewmodel = crudWindow.getViewModel();
                    if (container.form.isValid()) {
                        var formInfo = Ext.apply({}, viewmodel.get('formInfo'));
                        crudWindow.fn(formInfo);
                        container.reset();
                        if (operation == 'modification' && !Ext.isEmpty(crudWindow)) {
                            crudWindow.close();
                        }
                    }
                }
            }]);
        }

        crudWindow = Ext.create('Ext.window.Window', {
            viewModel: {
                data: {
                    formInfo: Ext.isEmpty(record) ? me.dataDefault : record,
                    readOnly: (operation == 'preview')
                }
            },
            id: me.name + '-window-id',
            modal: true,
            minHeight: me.crudWinSize.minHeight,
            width: me.crudWinSize.width,
            title: Ext.String.format('{0} ({1})', me.title, (operation == 'preview') ? 'Preview': ((operation == 'create') ? 'New Record': 'Modification')),
            scrollable: false,
            bodyPadding: 5,
            operation: operation, 
            fn: fn,
            layout: 'fit',
            items: [{
                xtype: 'form',
                layout: 'column',
                defaults: {
                    columnWidth: 1,
                    xtype: 'textfield',
                    margin: '0 0 5 0',
                    labelWidth: 125
                },
                items: me.buildCrudItems()                
            }],
            buttons: buttons
        });

        return crudWindow;
    },
    buildCrudItems: function() {
        var me = this,
            items = [],
            viewmodel = me.getViewModel();
        if (!Ext.isEmpty(me.providedFields)) {
            var mutableFields = viewmodel.get(Ext.String.format('config.{0}.mutableFields', me.name));
            Ext.each(me.sortOf(me.providedFields, 'visibleIndex', 'asc'), function (item, index, array) {
                var field = me.kindOfField(item);
                if (me.isContains(item.text, mutableFields)) {
                    field.readOnly = true;
                    delete field.bind.readOnly;
                }
                items.push(field);
            });
        }
        return items;
    },
    isContains: function (text, container) {
        var certain = false;
        if (Ext.isEmpty(container) || container == [])
            return certain;

        Ext.each(container, function (item, index, a) {
            if (text === item) {
                certain = true;
                return;
            }
        });

        return certain;
    },
    sortOf: function (columns, key, direction) {

        return columns;
    },
    kindOfField: function(field) {
        var kind = undefined;

        switch (field.kind) {
            case 'time': {
                kind = {
                    xtype: 'timefield',
                    fieldLabel: field.text,
                    allowBlank: Ext.isEmpty(field.allowBlank) ? true : field.allowBlank,
                    columnWidth: Ext.isEmpty(field.width) ? 1 : field.width,
                    bind: {
                        readOnly: '{readOnly}',
                        value: Ext.String.format('{formInfo.{0}}', field.name)
                    },
                    format: 'H:i'
                }
                break;
            }
            case 'datetime': {
                kind = {
                    xtype: 'datetime',
                    fieldLabel: field.text,
                    allowBlank: Ext.isEmpty(field.allowBlank) ? true : field.allowBlank,
                    columnWidth: Ext.isEmpty(field.width) ? 1 : field.width,
                    format: 'd/m/Y H:i',
                    minValue: field.minValue,
                    maxValue: field.maxValue,
                    bind: {
                        readOnly: '{readOnly}',
                        value: Ext.String.format('{formInfo.{0}}', field.name),
                        minValue: Ext.isEmpty(field.ref) ? null : Ext.String.format('{formInfo.{0}}', field.ref.min),
                        maxValue: Ext.isEmpty(field.ref) || Ext.isEmpty(field.ref.max) ? null : Ext.String.format('{formInfo.{0}}', field.ref.max)
                    },
                    listeners: {
                        beforedestroy: function (el) {
                            var me = this;
                            //delete me.bind.minValue;
                            //delete me.bind.maxValue;                                
                            return false;
                        }
                    }
                }
                break;
            }
            case 'date': {
                kind = {
                    xtype: 'datefield',
                    fieldLabel: field.text,
                    allowBlank: Ext.isEmpty(field.allowBlank) ? true : field.allowBlank,
                    columnWidth: Ext.isEmpty(field.width) ? 1 : field.width,
                    format: 'd/m/Y',
                    altFormats: 'c',
                    minValue: field.minvalue,
                    maxvalue: field.maxvalue,
                    bind: {
                        readOnly: '{readOnly}',
                        value: Ext.String.format('{formInfo.{0}}', field.name),
                        minValue: Ext.isEmpty(field.ref) || Ext.isEmpty(field.ref.min) ? null : Ext.String.format('{formInfo.{0}}', field.ref.min),
                        maxValue: Ext.isEmpty(field.ref) || Ext.isEmpty(field.ref.max) ? null : Ext.String.format('{formInfo.{0}}', field.ref.max)
                    },
                    listeners: {
                        beforedestroy: function (el) {
                            var me = this;
                            delete me.bind.minValue;
                            delete me.bind.maxValue;
                            
                            return true;
                        }
                    }
                }
                break;
            }
            case 'text': {
                kind = {
                    xtype: 'textfield',
                    fieldLabel: field.text,
                    allowBlank: Ext.isEmpty(field.allowBlank) ? true : field.allowBlank,
                    columnWidth: Ext.isEmpty(field.width) ? 1 : field.width,
                    bind: {
                        readOnly: '{readOnly}',
                        value: Ext.String.format('{formInfo.{0}}', field.name)
                    }
                }
                break;
            }
            case 'lookup': {
                kind = {
                    xtype: 'ngLookup',
                    fieldLabel: field.text,
                    editable: Ext.isEmpty(field.editable) ? false : field.editable,
                    displayField: field.displayField,
                    valueField: field.valueField,
                    url: field.url,
                    reference: field.reference,
                    allowBlank: Ext.isEmpty(field.allowBlank) ? true : field.allowBlank,
                    columnWidth: Ext.isEmpty(field.width) ? 1 : field.width,
                    queryMode: 'local',
                    forceSelection: true,
                    minChars: 1,
                    bind: {
                        selection: Ext.String.format('{{0}.selection}', field.name),
                        readOnly: '{readOnly}',
                        value: Ext.String.format('{formInfo.{0}}', field.name),
                        disabled: Ext.isEmpty(field.ref) ? null : Ext.String.format('{!{0}.selection}', field.ref.name),
                        filters: Ext.isEmpty(field.ref) ? null : {
                            property: field.ref.filter.property,
                            value: Ext.String.format('{{0}.selection.{1}}', field.ref.name, field.ref.property)
                        }
                    },
                    listeners: {
                        beforedestroy: function(el) {
                            var me = this;
                            delete me.bind.disabled;
                            delete me.bind.filters; 
                            return true;
                        },
                        change: function(cb, value){
                            if(field.onChange){
                                field.onChange(cb, value);
                            }
                            
                        }
                    }
                }
                break;
            }
            case 'email': {
                kind = {
                    xtype: 'textfield',
                    fieldLabel: field.text,
                    allowBlank: Ext.isEmpty(field.allowBlank) ? true : field.allowBlank,
                    columnWidth: Ext.isEmpty(field.width) ? 1 : field.width,
                    //regex: /^((([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z\s?]{2,5}){1,25})*(\s*?;\s*?)*)*$/,
                    //regexText: 'This field must contain single or multiple valid email addresses separated by semicolon (;)',
                    blankText: 'Please enter email address(s)',
                    bind: {
                        readOnly: '{readOnly}',
                        value: Ext.String.format('{formInfo.{0}}', field.name)
                    }
                }
                break;
            }
            case 'phone': {
                kind = {
                    xtype: 'textfield',
                    fieldLabel: field.text,
                    allowBlank: Ext.isEmpty(field.allowBlank) ? true : field.allowBlank,
                    columnWidth: Ext.isEmpty(field.width) ? 1 : field.width,
                    //regex: /^[0-9]*$/,
                    //regexText: 'This field must contain number only.',
                    blankText: 'Please enter phone number.',
                    //maskRe: /[0-9.]/,
                    bind: {
                        readOnly: '{readOnly}',
                        value: Ext.String.format('{formInfo.{0}}', field.name)
                    }
                }
                break;
            }
            case 'textarea': {
                kind = {
                    xtype: 'textarea',
                    fieldLabel: field.text,
                    allowBlank: Ext.isEmpty(field.allowBlank) ? true : field.allowBlank,
                    columnWidth: Ext.isEmpty(field.width) ? 1 : field.width,
                    bind: {
                        readOnly: '{readOnly}',
                        value: Ext.String.format('{formInfo.{0}}', field.name)
                    }
                }
                break;
            }
            case 'numeric': {                
                kind = {
                    xtype: 'numberfield',
                    fieldLabel: field.text,
                    allowBlank: Ext.isEmpty(field.allowBlank) ? true : field.allowBlank,
                    columnWidth: Ext.isEmpty(field.width) ? 1 : field.width,
                    minValue: field.minValue,
                    bind: {
                        readOnly: '{readOnly}',
                        value: Ext.String.format('{formInfo.{0}}', field.name)
                    }
                }
                break;
            }

            case 'currency': {
                kind = {
                    xtype: 'currencyfield',
                    fieldLabel: column.text,
                    allowBlank: Ext.isEmpty(column.allowBlank)? true: column.allowBlank, 
                    columnWidth: Ext.isEmpty(column.width) ? 1 : column.width,
                    bind: {
                        readOnly: '{readOnly}',
                        value: Ext.String.format('{formInfo.{0}}', column.name)
                    }
                }
                break;
            }
        }

        return kind;
    },
    kindOfColumn: function (column) {
        var me = this,
            kind = undefined;

        switch (column.kind) {
            case 'time': {
                kind = {
                    text: column.text,
                    width: Ext.isEmpty(column.width) ? 125 : column.width,
                    sortable: Ext.isEmpty(column.sortable) ? true : column.sortable,
                    dataIndex: column.name,
                    flex: Ext.isEmpty(column.flex) ? undefined : column.flex,
                    autoSizeColumn: Ext.isEmpty(column.autoSizeColumn) ? undefined : column.autoSizeColumn,
                    menuDisabled: true,
                    renderer: Ext.isEmpty(column.formatDate) ? Ext.util.Format.dateRenderer('H:i') : column.formatDate
                }
                break;
            }
            case 'datetime': {
                kind = {
                    text: column.text,
                    width: Ext.isEmpty(column.width) ? 125 : column.width,
                    sortable: Ext.isEmpty(column.sortable) ? true : column.sortable,
                    dataIndex: column.name,
                    flex: Ext.isEmpty(column.flex) ? undefined : column.flex,
                    menuDisabled: true,
                    autoSizeColumn: Ext.isEmpty(column.autoSizeColumn) ? undefined : column.autoSizeColumn,
                    renderer: Ext.isEmpty(column.formatDate) ? Ext.util.Format.dateRenderer('d/m/Y H:i') : column.formatDate
                }
                break;
            }
            case 'date': {
                kind = {
                    text: column.text,
                    width: Ext.isEmpty(column.width) ? 125 : column.width,
                    sortable: Ext.isEmpty(column.sortable) ? true : column.sortable,
                    dataIndex: column.name,
                    flex: Ext.isEmpty(column.flex) ? undefined : column.flex,
                    menuDisabled: true,
                    autoSizeColumn: Ext.isEmpty(column.autoSizeColumn) ? undefined : column.autoSizeColumn,
                    renderer: Ext.isEmpty(column.formatDate) ? Ext.util.Format.dateRenderer('d/m/Y') : column.formatDate
                }
                break;
            }
            case 'email': {
                kind = {
                    text: column.text,
                    width: Ext.isEmpty(column.width) ? 125 : column.width,
                    sortable: Ext.isEmpty(column.sortable) ? true : column.sortable,
                    flex: Ext.isEmpty(column.flex) ? undefined : column.flex,
                    menuDisabled: true,
                    autoSizeColumn: Ext.isEmpty(column.autoSizeColumn) ? undefined : column.autoSizeColumn,
                    dataIndex: column.name
                }
                break;
            }
            case 'phone':
            case 'lookup':
            case 'text': {
                kind = {
                    text: column.text,
                    width: Ext.isEmpty(column.width) ? 125 : column.width,
                    sortable: Ext.isEmpty(column.sortable) ? true : column.sortable,
                    flex: Ext.isEmpty(column.flex) ? undefined : column.flex,
                    menuDisabled: true,
                    autoSizeColumn: Ext.isEmpty(column.autoSizeColumn) ? undefined : column.autoSizeColumn,
                    dataIndex: column.name
                }
                break;
            }
            case 'textarea': {
                kind = {
                    text: column.text,
                    width: Ext.isEmpty(column.width) ? 225 : column.width,
                    sortable: Ext.isEmpty(column.sortable) ? true : column.sortable,
                    flex: Ext.isEmpty(column.flex) ? undefined : column.flex,
                    menuDisabled: true,
                    autoSizeColumn: Ext.isEmpty(column.autoSizeColumn) ? undefined : column.autoSizeColumn,
                    dataIndex: column.name
                }
                break;
            }
            case 'numeric': {
                kind = {
                    text: column.text,
                    width: Ext.isEmpty(column.width) ? 125 : column.width,
                    sortable: Ext.isEmpty(column.sortable) ? true : column.sortable,
                    dataIndex: column.name,
                    flex: Ext.isEmpty(column.flex) ? undefined : column.flex,
                    menuDisabled: true,
                    autoSizeColumn: Ext.isEmpty(column.autoSizeColumn) ? undefined : column.autoSizeColumn,
                    renderer: Ext.isEmpty(column.formatNumber) ? me.formatNumber : column.formatNumber
                }
                break;
            }

            case 'currency': {
                kind = {
                    text: column.text,
                    width: Ext.isEmpty(column.width) ? 125 : column.width,
                    sortable: Ext.isEmpty(column.sortable) ? true : column.sortable,
                    dataIndex: column.name,
                    flex: Ext.isEmpty(column.flex) ? undefined : column.flex,
                    menuDisabled: true,
                    autoSizeColumn: Ext.isEmpty(column.autoSizeColumn) ? undefined : column.autoSizeColumn,
                    renderer: Ext.isEmpty(column.formatCurrency) ? me.formatCurrency : column.formatCurrency
                }
                break;
            }
        }

        return kind;
    },
    formatNumber: function (value) {
        var format = '0,000';
        if (this.floating) {
            format = '0,000.00';
        }
        return Ext.util.Format.number(value, format);
    },
    formatCurrency: function (value) {
        var format = Ext.util.Format;
        format.thousandSeparator = ',';
        format.currencySign = Ext.isEmpty(this.currencySign) ? '$' : this.currencySign;
        format.currencyAtEnd = false;
        return format.currency(value);
    },
    reset: function () {
        var me = this,
            store = me.getStore();
        store.removeAll();
        store.setData([]);
    },
    validate: function (action) {
        var me = this,
            viewmodel = me.getViewModel(),
            requiredRecord = viewmodel.get(Ext.String.format('config.{0}.requiredRecord', me.name));

        if (requiredRecord && me.getStore().count() == 0)
            return Ext.String.format('{0} Records could be not empty. Please input the required field(s) before you click the {1} button.', me.title, action);

        var missFields = me.validateRequiredFields(action)

        if (missFields.length > 0)
            return Ext.String.format('{0} Fields [{1}] could be not empty. Please input the required field(s) before you click the {2} button.', me.title, missFields, action);

        return "";
    },
    validateRequiredFields: function (action) {
        var me = this,
            missing = [],
            viewmodel = me.getViewModel(),
            requiredFields = viewmodel.get(Ext.String.format('config.{0}.requiredFields', me.name));
        
        if (Ext.isEmpty(requiredFields) || requiredFields == [])
            return missing;

        Ext.each(requiredFields, function (field, index) {            
            var dataIndex = me.getDataIndex(field.name);
            var fieldAction = field.action;
            if (dataIndex != '' && fieldAction === action) {
                me.getStore().each(function (record) {                    
                    var content = record.get(dataIndex);
                    if (Ext.isEmpty(content)) {
                        missing.push(field.name);
                    }
                });
            }            
        });

        return missing;
    },
    getDataIndex: function(columnName) {
        var me = this,
            dataIndex = '';
        Ext.each(me.columns, function (item) {
            if (columnName === item.text) {
                dataIndex = item.dataIndex;
                return;
            }                
        });

        return dataIndex;
    }
});