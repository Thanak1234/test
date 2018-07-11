Ext.define("Workflow.view.common.employee.EmployeePickup", {
    extend: "Ext.form.field.ComboBox",
    xtype: 'employeePickup',
    requires: [
        "Workflow.view.common.employee.employeePickupController"
    ],
    controller: "common-employee-employeepickup",
    store: {
        type: 'requestor'
    },

    displayField: 'fullName',
    valueField  : 'id',
    typeAhead   : true,
    anchor: '100%',
    employeeEditable: false,
    changeOnly: false,
    //width: 500,
    queryMode: 'remote',
    pageSize: 20, // just flag to show paging toolbar
    forceSelection: true,
    loadCurrentUser: false,
    listConfig  : {
        minWidth: 400,
        resizable: true,
        loadingText: 'Searching...',
        emptyText: 'No matching posts found.',
        itemSelector: '.search-item',

        // Custom rendering template for each item
        itemTpl: [
            '<a class="tpl-list-employee">',
                '<span class="fa fa-user"></span><span> {employeeNo} - {fullName}</span>',
            '</a>'
        ]
    },
    listeners: {
        //change: 'onEmplyeePickupChanged',
        //expand: 'onEmplyeePickupChanged'//'onEmplyeeExpandCombobox'
        beforequery: 'onEmplyeePickupChanged',
        afterrender: function (combo) {
            var store = combo.getStore();
            var identity = Workflow.global.UserAccount.identity;
            if (identity && combo.loadCurrentUser) {
                store.add(identity);
                //store.add({ id: -1, fullName: null });
                combo.setValue(identity.id);
            }

            combo.getTrigger('clear').hide();
            combo.updateLayout();
        }
    },
    triggers: {
        clear: {
            weight: 1,
            cls: Ext.baseCSSPrefix + 'form-clear-trigger',
            hidden: true,
            handler: 'onClearClick',
            scope: 'this'
        },
        picker: {
            weight: 2,
            handler: 'onTriggerClick',
            scope: 'this'
        },
        add: {
            weight: 3,
            cls : Ext.baseCSSPrefix + 'form-add-trigger',
            scope: 'this',
            hidden: true
        },
        edit: {
            weight: 3,
            cls : Ext.baseCSSPrefix + 'form-edit-trigger',
            scope: 'this',
            hidden: true
        }
    },
    initComponent: function () {
        var me = this;
        this.pickerId = this.getId() + "_picker";
        this.callParent(arguments);
    },
    onClearClick: function () {
        var me = this;

        if (me.disabled) {
            return;
        }

        me.clearValue();
        me.setRawValue(null);
        me.getTrigger('clear').hide();
        if (me.employeeEditable) {
            me.getTrigger('edit').hide();
            me.getTrigger('add').show();
        }
        me.value = null;
        me.afterClear(me);
        me.updateLayout();
        me.fireEvent('clear', me);
    },
    afterClear: function(combo){
        
    },
    /* override extjs combo method */
    updateValue: function () {
        var me = this,
            selectedRecords = me.valueCollection.getRange();

        var data = selectedRecords[0] ? selectedRecords[0].data : null;

        if (selectedRecords.length > 0) {
            me.getTrigger('clear').show();

            if (me.employeeEditable) {
                if (data && data.empType == 'MANUAL') {
                    me.getTrigger('edit').show();
                }
                me.getTrigger('add').hide();
            }

            me.updateLayout();
            me.callParent();
        }
    },
    setReadOnly: function (value) {
        var me = this,
            old = me.readOnly;

        me.callParent(arguments);
        if (value != old) {
            this.readOnly = value;
            me.updateLayout();
        }

        if (this.readOnly || this.changeOnly) {
            me.getTrigger('edit').hide();
            me.getTrigger('add').hide();
        }
        this.getTrigger('clear').hide();
    },
    assertValue: function () {
        var me = this,
            value = me.getRawValue(),
            displayValue = me.getDisplayValue(),
            lastRecords = me.lastSelectedRecords,
            rec;

        if (me.forceSelection) {
            if (me.multiSelect) {
                // For multiselect, check that the current displayed value matches the current
                // selection, if it does not then revert to the most recent selection.
                if (value !== displayValue) {
                    me.setRawValue(displayValue);
                }
            } else {
                // For single-select, match the displayed value to a record and select it,
                // if it does not match a record then revert to the most recent selection.
                rec = me.findRecordByDisplay(value);
                if (rec) {
                    // Prevent an issue where we have duplicate display values with
                    // different underlying values.
                    if (me.getDisplayValue([me.getRecordDisplayData(rec)]) !== displayValue) {
                        me.select(rec, true);
                    }
                } else if (lastRecords && (!me.allowBlank || me.rawValue)) {
                    //me.setValue(lastRecords);
                } else {
                    if (lastRecords) {
                        delete me.lastSelectedRecords;
                    }
                    // We need to reset any value that could have been set in the dom before or during a store load
                    // for remote combos.  If we don't reset this, then ComboBox#getValue() will think that the value
                    // has changed and will then set `undefined` as the .value for forceSelection combos.  This then
                    // gets changed AGAIN to `null`, which will get set into the model field for editors. This is BAD.
                    me.setRawValue('');
                }
            }
        }
        me.collapse();
    }
});
