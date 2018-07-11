
Ext.define("Workflow.view.common.department.DepartmentPickup",{
    extend: "Ext.form.field.ComboBox",
    xtype: "departmentPickup",

    requires: [
        "Workflow.view.common.department.DepartmentPickupController",
        "Workflow.view.common.department.DepartmentPickupModel"
    ],

    controller: "common-department-departmentpickup",
    store: {
        type: 'department'
    },
    displayField: 'fullName',
    valueField: 'id',
    typeAhead: true,
    anchor: '100%',
    width: 500,
    pageSize: 20,
    listConfig: {
        minWidth: 500,
        resizable: true,
        loadingText: 'Searching...',
        emptyText: 'No matching posts found.',
        itemSelector: '.search-item'

        // Custom rendering template for each item
        //itemTpl: [
        //    '<a >',
        //        '<h3><span>{fullName}</span></h3>',
        //        '{deptType}',
        //    '</a>'
        //]
    },
    listeners: {
        change: 'onDeptPickupChanged'
    },
    triggers: {
        clear: {
            cls: 'x-form-clear-trigger',
            handler: function (el){
                el.clearValue( );
            },
            hidden: true,
            weight: -2
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
        this.getTrigger('clear').hide();
    }
});
