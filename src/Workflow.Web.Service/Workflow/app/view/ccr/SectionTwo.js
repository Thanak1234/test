Ext.define("Workflow.view.ccr.SectionTwo", {
    extend: "Ext.form.Panel",
    xtype: 'ccr-section-two',
    border: true,
    layout: 'column',
    margin: '0 0 10 0',
    defaults: {
        xtype: 'textfield',
        width: '100%',
        margin: '5 10',
        columnWidth: 1,
        labelWidth: 200
    },
    width: '100%',
    collapsible: false,
    iconCls: 'fa fa-cogs',
    title: 'SECTION 2 - AGREEMENT COMMERCIAL TERMS (For Non-Capex Only)',
    bind: {
        disabled: '{capex.isCapex}'
    },
    initComponent: function () {
        var me = this;
        me.items = [
            {
                xtype: 'label',
                html: 'A. Existing Arrangement(if any): When will the current contract expire? What are the price under the existing contract?<br/> * if the price will be increased for the renewal, please provide justification for increase.'
            },
            {
                xtype: 'textarea',
                hideLabel: true,
                bind: {
                    value: '{ContractDraft.ActA}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'label',
                html: 'B. Need of Expenditure: Outline the need for this transaction/expenditure ie. to support operational or services needs...?'
            },
            {
                xtype: 'textarea',
                hideLabel: true,
                bind: {
                    value: '{ContractDraft.ActB}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'label',
                html: 'C. Outline the Need for this Expenditure: Cost breakdown or estimated expenditure. Please explain pricing, services, quote etc.<br/> Why do you need this? Will it result in an increase in revenue or a reduction in expenditure? Please quantify where possible.<br/> Why is this supplier/product recommended? if the contract term is more than 1 year, why does it need to be?<br /> Are there any additional related cost that will be incurred(custome duties, annual maintenance cost, etc.)? How did you manage without it so far?'
            },
            {
                xtype: 'textarea',
                hideLabel: true,
                bind: {
                    value: '{ContractDraft.ActC}',
                    readOnly: '{readOnly}'
                }
            },
            {
                xtype: 'label',
                html: 'D. Risk Analysis: What can go wrong? Please outline the risks (If nothing is stated, this shall mean requesting department foresees no risks)'
            },
            {
                xtype: 'textarea',
                hideLabel: true,
                bind: {
                    value: '{ContractDraft.ActD}',
                    readOnly: '{readOnly}'
                }
            }
        ];

        me.callParent(arguments);
    }
});
