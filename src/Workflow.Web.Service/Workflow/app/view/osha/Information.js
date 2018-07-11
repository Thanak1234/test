Ext.define("Workflow.view.osha.Information", {
    extend: "Ext.form.Panel",
    xtype: 'osha-information',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        margin: '5 10',
        columnWidth: 1,
        labelWidth: 200
    },
    collapsible: true,
    iconCls: 'fa fa-cogs',
    title: 'Accident/Incident Report',
    initComponent: function () {
        var me = this;        
        me.items = [
           {
               fieldLabel: 'Nature/Type of the Accident',
               allowBlank: false,
               bind: {
                   value: '{information.nta}',
                   readOnly: '{config.information.readOnly}'
               }
           },
           {
               fieldLabel: 'Location of accident/incident',
               allowBlank: true,
               bind: {
                   value: '{information.lai}',
                   readOnly: '{config.information.readOnly}'
               }
           },
           {
               xtype: 'datetime',
               fieldLabel: 'Date/Time of accident/incident',
               allowBlank: false,
               bind: {
                   value: '{information.dta}',
                   readOnly: '{config.information.readOnly}'
               }
           },
           {
               xtype: 'osha-employee',
               reference: 'victims',
               title: 'Victim(s) Information',
               empType: 'VICTIMS'
           },
           {
               xtype: 'osha-employee',
               reference: 'withness',
               title: 'Witness Name (If any)',
               empType: 'WITHNESS'
           },
           {
               xtype: 'label',
               text: 'Cause of accident/incident: (Brief description)'
           },
           {
               xtype: 'textarea',
               bind: {
                   value: '{information.ca}',
                   readOnly: '{config.information.readOnly}'
               }
           },
           {
               xtype: 'label',
               text: 'Indicate body part affected: (Please download and attach the Human Grid Diagram)'
           },
           {
               xtype: 'radiogroup',
               fieldLabel: 'Did the injured employee/Guest see a doctor? Please tick',
               labelWidth: 350,
               allowBlank: false,
               columnWidth: 0.6,
               columns: 2,
               vertical: true,
               name: 'diegsd',
               bind: {
                   value: '{rdiegsd}',
                   readOnly: '{config.information.readOnly}'
               },
               items: [
                   { boxLabel: 'Yes', inputValue: true },
                   { boxLabel: 'No', inputValue: false }
               ]
           },
           {
               xtype: 'label',
               text: 'If "Yes" what is the doctor\'s finding?'
           },
           {
               xtype: 'textarea',
               bind: {
                   value: '{information.df}',
                   readOnly: '{config.information.readOnly}'
               }
           },
           {
               xtype: 'fieldset',
               title: 'Employee',
               layout: 'anchor',
               defaults: {
                   anchor: '60%',
                   labelSeparator: ''
               },
               items: [
                   {
                       xtype: 'radiogroup',
                       fieldLabel: '1. Was the employees sent home and given MC to rest?',
                       labelWidth: 350,
                       allowBlank: true,
                       name: 'e1',
                       bind: {
                           value: '{re1}',
                           readOnly: '{config.information.readOnly}'
                       },
                       items: [
                           { boxLabel: 'Yes', inputValue: true },
                           { boxLabel: 'No', inputValue: false }
                       ]
                   },
                   {
                       xtype: 'radiogroup',
                       fieldLabel: '2. Was the employee referred to the hospital?',
                       labelWidth: 350,
                       allowBlank: true,
                       name: 'e2',
                       bind: {
                           value: '{re2}',
                           readOnly: '{config.information.readOnly}'
                       },
                       items: [
                           { boxLabel: 'Yes', inputValue: true },
                           { boxLabel: 'No', inputValue: false }
                       ]
                   }
               ]
           },
           {
               xtype: 'fieldset',
               title: 'Guest',
               layout: 'anchor',
               defaults: {
                   anchor: '60%',
                   labelSeparator: ''
               },
               items: [
                   {
                       xtype: 'radiogroup',
                       fieldLabel: '3. Did the guest seek any medical assistance?',
                       labelWidth: 350,
                       allowBlank: true,
                       name: 'g3',
                       bind: {
                           value: '{rg3}',
                           readOnly: '{config.information.readOnly}'
                       },
                       items: [
                           { boxLabel: 'Yes', inputValue: true },
                           { boxLabel: 'No', inputValue: false }
                       ]
                   },
                   {
                       xtype: 'radiogroup',
                       fieldLabel: '4. Did the guest make any claim?',
                       labelWidth: 350,
                       allowBlank: true,
                       name: 'g4',
                       bind: {
                           value: '{rg4}',
                           readOnly: '{config.information.readOnly}'
                       },
                       items: [
                           { boxLabel: 'Yes', inputValue: true },
                           { boxLabel: 'No', inputValue: false }
                       ]
                   },
                   {
                       xtype: 'label',
                       text: '5. if yes, what is the claim?'
                   },
                   {
                       xtype: 'textarea',
                       margin: '20 0 15 0',
                       minWidth: 920,
                       bind: {
                           value: '{information.g5}',
                           readOnly: '{config.information.readOnly}'
                       }
                   }
                   /*{
                       xtype: 'radiogroup',
                       fieldLabel: '5. if yes, what is the claim?',
                       labelWidth: 350,
                       allowBlank: false,
                       name: 'g5',
                       bind: {
                           value: '{rg5}',
                           readOnly: '{config.information.readOnly}'
                       },
                       items: [
                           { boxLabel: 'Yes', inputValue: true },
                           { boxLabel: 'No', inputValue: false }
                       ]
                   }*/
               ]
           },
           {
               xtype: 'label',
               text: 'HOD/Supervisor\'s Comments'
           },
           {
               xtype: 'textarea',
               bind: {
                   value: '{information.hsc}',
                   readOnly: '{config.information.readOnly}'
               }
           },
           {
               xtype: 'radiogroup',
               fieldLabel: 'Has Corrective action taken?',
               labelWidth: 350,
               allowBlank: false,
               columnWidth: 0.6,
               columns: 2,
               labelSeparator: '',
               vertical: true,
               name: 'hcat',
               bind: {
                   value: '{rhcat}',
                   readOnly: '{config.information.readOnly}'
               },
               items: [
                   { boxLabel: 'Yes', inputValue: true },
                   { boxLabel: 'No', inputValue: false }
               ]
           },
           {
               xtype: 'label',
               text: 'If yes, what has been done?'
           },
           {
               xtype: 'textarea',
               bind: {
                   value: '{information.yesdone}',
                   readOnly: '{config.information.readOnly}'
               }
           }, {
               xtype: 'label',
               text: 'If no, what needs to be done?'
           },
           {
               xtype: 'textarea',
               margin: '5 10 30 10',
               bind: {
                   value: '{information.nodone}',
                   readOnly: '{config.information.readOnly}'
               }
           }
        ];

        me.callParent(arguments);
    }
});
