Ext.define('Workflow.view.ccr.CCRModel', {
    extend: 'Workflow.view.common.GenericModel',
    alias: 'viewmodel.ccr',
    data: {
        radStatus: {
            status: 'New'
        },
        capex: {
            isCapex: true
        },
        ContractDraft: {
            Name: null,
            Vat: null,
            Address: null,
            Email: null,
            RegistrationNo: null,
            Phone: null,
            ContactName: null,
            Position: null,
            IssueedBy: null,
            Term: null,
            StartDate: null,
            InclusiveTax: null,
            EndingDate: null,
            PaymentTerm: null,
            AtSa: null,
            AtSpa: null,
            AtLa: null,
            AtCa: null,
            AtLea: null,
            AtEa: null,
            AtOther: null,
            Status: null,
            Vis: null,
            IsCapex: null,
            BcjNumber: null,
            ActA: null,
            ActB: null,
            ActC: null,
            ActD: null
        },
        viewSetting: null
    },
    stores: {
        bcjStore: {
            model: 'Workflow.model.ccr.FormLookup',
            pageSize: 25,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/ccrrequest/forms?requestCode=BCJ_REQ',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    },
    formulas: {
        readOnly: function (get) {
            if (get('viewSetting') && get('viewSetting').container.readOnly) {
                return true;
            } else {
                return false;
            }
        }
    }
});

Ext.define('Workflow.model.ccr.FormLookup', {
    extend: 'Ext.data.Model',
    fields: [
        'ProcId',
        'Folio',
        'Link',
        'Submittor',
        'Requestor'
    ]
});

