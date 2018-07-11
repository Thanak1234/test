
Ext.define("Workflow.view.common.employee.form.EmployeeForm",{
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",
    xtype: 'employeeForm',
    requires: [
    ],
    controller: "common-employee-form-employeeform",
    //store: {
    //    type: 'employee'
    //},
    
    viewModel: {
        type: "common-employee-form-employeeform"
    },
    bind:{
        title: '{formTitle}'
    },    
    border:false,
    width: 850,
    buttons: [
        {
            xtype: 'button',
            bind : {
                text : '{submitBtText}',
                visible : '{submitBtVisible}'
            },
            handler: 'submitForm'
        },{
            xtype: 'button',
            text: 'Close',
            handler: 'closeWindow'
        }
    ],
    buildItems: function () {
        return [{
            xtype: 'form',
            layout: 'column',
            reference: 'form',
            autoWidth: true,
            bodyPadding: 10,
            defaults: {
                layout: 'form',
                xtype: 'container',
                defaultType: 'textfield',
                flex: 1
            },
            items: [
            {
                width: 400,
                items: [
		                {
		                    fieldLabel: 'Employee No <span class="require_asterisk">*</span>',
		                    bind: '{employee.EmpNo}',
		                    name: 'EmpNo',
		                    allowBlank: false,
		                    enforceMaxLength: true,
		                    maxLength: 50
		                },
		                {
		                    fieldLabel: 'First Name <span class="require_asterisk">*</span>',
		                    allowBlank: false,
		                    bind: '{employee.FirstName}',
		                    name: 'FirstName'
		                },
		                {
		                    fieldLabel: 'Display Name <span class="require_asterisk">*</span>',
		                    allowBlank: false,
		                    bind: '{employee.DisplayName}',
		                    name: 'DisplayName'
		                },
		                {
		                    fieldLabel: 'Department <span class="require_asterisk">*</span>',
		                    xtype: 'departmentPickup',
		                    allowBlank: false,
		                    forceSelection: true,
		                    bind: {
		                        store: '{departments}',
		                        value: '{employee.DeptId}',
		                        selection: '{team}'
		                    },
		                    name: 'DeptId'
		                },
		                {
		                    fieldLabel: 'Report To',
		                    xtype: 'employeePickup',
                            bind: {
                                selection: '{reportTo}',
		                        readOnly : '{readOnly}'
		                    },
		                    name: 'ReportTo'
		                },
		                { fieldLabel: 'Home Phone', bind: '{employee.HomePhone}', name: 'HomePhone' },
		                { fieldLabel: 'Address', bind: '{employee.Address}', name: 'Address', width:500, anchor: '100%'}
                ]
            }, {
                width: 400,
                items: [
                    { fieldLabel: 'Position', bind: '{employee.JobTitle}', name: 'JobTitle' },
                    {
                        fieldLabel: 'Last Name <span class="require_asterisk">*</span>',
                        allowBlank: false,
                        bind: '{employee.LastName}',
                        name: 'LastName'
                    },
                    {
                        fieldLabel: 'Hired Date', xtype: 'datefield', bind: '{employee.HiredDate}', name: 'HiredDate'
                    },
                    {
                        fieldLabel: 'Email', bind: '{employee.Email}', name: 'Email'
                    },

                    {
                        fieldLabel: 'Mobile Phone (Phone)',
                        bind: '{employee.MobilePhone}',
                        name: 'MobilePhone'
                    },
                    {
                        fieldLabel: 'Phone (Ext)',
                        bind: '{employee.Telephone}',
                        name: 'Telephone'
                    }, { fieldLabel: 'IP Phone', bind: '{employee.IpPhone}', name: 'IpPhone' }
                ]
            }]



        }];
    }
});
