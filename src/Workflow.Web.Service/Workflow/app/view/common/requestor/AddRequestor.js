/**
 * 
 *Author : Phanny 
 */
Ext.define("Workflow.view.common.requestor.AddRequestor",{
    extend: "Workflow.view.common.requestor.AbstractWindowDialog",

    requires: [
        "Workflow.view.common.requestor.AddRequestorController",
        "Workflow.view.common.requestor.AddRequestorModel"
    ],

    controller: "common-requestor-addrequestor",
    viewModel: {
        type: "common-requestor-addrequestor"
    },
    title: 'Adding Requestor',
    
    
    width: 850,
    
    buildItems : function (){
        return [{
            xtype: 'form',
            layout: 'column',
            autoWidth: true,
            bodyPadding: 10,
            defaults: {
                layout: 'form',
                xtype: 'container',
                defaultType: 'textfield',
                flex : 1
            },
            items:[
            {
            width: 400,
            items: [
                { fieldLabel: 'Employee No',bind: '{submitBtText}' },
                {  fieldLabel: 'First Name' },
                { fieldLabel: 'Display Name'},
                { fieldLabel: 'Department'},
                {   
                    fieldLabel: 'Report To', 
                    xtype: 'combo',
                    store: {
                        type: 'requestor'
                    },
                    
                    displayField    : 'fullName',
                    typeAhead       : false,
                    anchor          : '100%',
                    width           : 500,
                    pageSize        : 20,
                    listConfig      : {
                        minWidth    : 500,
                        resizable   : true,
                        loadingText : 'Searching...',
                        emptyText   : 'No matching posts found.',
                        itemSelector: '.search-item',
                        itemTpl     : [
                            '<a >',
                                '<h3><span>{fullName}</span>({employeeNo})</h3>',
                                '{email}',
                            '</a>'
                        ]
                    }
                },
                { fieldLabel: 'Telephone' },
                { fieldLabel: 'Address' }
            ]}, {
            width: 400,
            items: [
                { fieldLabel: 'Position', bind: '{employeeInfo.position}'},
                {  fieldLabel: 'Last Name' },
                { fieldLabel: 'Hired Date'},
                { fieldLabel: 'Email', bind: '{employeeInfo.email}' },
                { fieldLabel: 'Mobile Phone'},
                { fieldLabel: 'Ext.'}
                ]
            }]

        }];
    }
    
});
