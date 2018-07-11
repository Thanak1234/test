Ext.define('Workflow.ux.form.field.DateRangField', {
    extend: 'Ext.form.field.ComboBox',
    alias: ['widget.deptcombo'],
    fieldLabel: 'Departments',
    editable: false,
    emptyText: 'DEPARTMENTS <ALL>',
    displayField: 'fullName',
    valueField: 'id',
    triggerAction: 'all',
    maxWidth: 400,
    maxLength: 1000000,
    multiSelect: true,
    forceSelection: true,
    value: null,
    listConfig: {
        getInnerTpl: function () {
            return '<div class="x-combo-list-item checkbox-list">&nbsp; {fullName}</div>';
        }
    },
    updateValue: function () {
        var me = this,
             selectedRecords = me.valueCollection.getRange(),
             len = selectedRecords.length,
             valueArray = [],
             displayTplData = me.displayTplData || (me.displayTplData = []),
             inputEl = me.inputEl,
             i, record, displayValue;

        
        /* custom logic here */
        if (me.value && me.value.length > 0) {
            me.getTrigger('clear').show();
        } else {
            me.getTrigger('clear').hide();
        }

        // Loop through values, matching each from the Store, and collecting matched records 
        displayTplData.length = 0;
        for (i = 0; i < len; i++) {
            record = selectedRecords[i];
            displayTplData.push(me.getRecordDisplayData(record));

            // There might be the bogus "value not found" record if forceSelect was set. Do not include this in the value. 
            if (record !== me.valueNotFoundRecord) {
                valueArray.push(record.get(me.valueField));
            }
        }

        // Set the value of this field. If we are multiselecting, then that is an array. 
        me.setHiddenValue(valueArray);
        me.value = me.multiSelect ? valueArray : valueArray[0];
        if (!Ext.isDefined(me.value)) {
            me.value = undefined;
        }
        me.displayTplData = displayTplData; //store for getDisplayValue method 

        displayValue = me.getDisplayValue();
        // Calculate raw value from the collection of Model data 
        me.setRawValue(displayValue);
        //me.refreshEmptyText();
        me.checkChange();

        if (inputEl && me.typeAhead && me.hasFocus) {
            // if typeahead is configured, deselect any partials 
            me.selectText(displayValue.length);
        }
    },
    triggers: {
        clear: {
            weight: 1,
            hidden: true,
            cls: Ext.baseCSSPrefix + 'form-clear-trigger',
            handler: function () {
                this.clearValue();
                this.getTrigger('clear').hide();
                this.updateLayout();

                this.afterClear(this);
            }
        }/*,
        selectAll: {
            weight: 2,
            hidden: false,
            cls: Ext.baseCSSPrefix + 'form-add-trigger',
            handler: function (combo) {
                var store = combo.getStore();
                var range = store.getRange();
                combo.select(range);
                combo.selectAll(range);
            }
        }*/
    },
    // overwrite function
    selectAll: function (range) {
        // TODO: derive class
    },
    afterClear: function (combo) {

    }
});