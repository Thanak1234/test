Ext.define('Workflow.view.vaf.ItemWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.va-itemwindow',
    data: {
        outline: {
            GamingDate: new Date(),
            McidLocn: '',
            VarianceType: '',
            Subject: '',
            IncidentRptRef: '',
            ProcessInstanceId: 0,
            Area: '',
            RptComparison: '',
            Amount: 0,
            Comment: ''
        },
        readOnly: false,
        selection: null
    },
    stores: {
        incidentRefStore: {
            model: 'Workflow.model.vaf.IncidentRef',
            pageSize: 5,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/vafservices',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        },
        varianceTypeStore: {
            fields: [
                'filter',
                'value',
                'id'
            ],
            data: [
                { id: 9, value: 'Actual drop', filter: 'TR-Table drop' },
                { id: 1, value: 'Bill in', filter: 'Slot operation' },
                { id: 8, value: 'Bill in', filter: 'TR-Soft count' },
                { id: 6, value: 'Ecash in', filter: 'Slot operation' },
                { id: 7, value: 'Ecash out', filter: 'Slot operation' },
                { id: 10, value: 'Estimate cash drop', filter: 'TR-Table drop' },
                { id: 5, value: 'Jackport', filter: 'Slot operation' },
                { id: 4, value: 'Manual pay', filter: 'Slot operation' },
                { id: 2, value: 'Ticket in', filter: 'Slot operation' },
                { id: 3, value: 'Ticket out', filter: 'Slot operation' }
            ]
        },
        reportsComparisonStore: {
            fields: [
                'value',
                'filter'
            ],
            data: [
                { filter: 9, value: 'Actual Vs Estimated Cash Drop' },
                { filter: 10, value: 'Estimated Cash Drop Vs Actual' },
                { filter: 4, value: 'Manual Summ. Vs FDR' },
                { filter: 5, value: 'Manual Summ. Vs FDR' },
                { filter: 6, value: 'Promo vs FDR' },
                { filter: 7, value: 'Promo vs FDR' },
                { filter: 3, value: 'Ticket issue vs FDR' },
                { filter: 1, value: 'Soft Count vs FDR (No)' },
                { filter: 2, value: 'Soft Count vs FDR (No)' },
                { filter: 8, value: 'Soft Count vs FDR (No)' }
            ]
        },
        subjectStore: {
            fields: [
                'value',
                'filter'
            ],
            data: [
                { value: 'Acceptor mechanism jammed', filter: 1 },
                { value: 'Acceptor large buy in', filter: 1 },
                { value: 'Bill in no credit', filter: 1 },
                { value: 'Credit post, bill rejected', filter: 1 },
                { value: 'FDR not captured', filter: 6 },
                { value: 'FDR not captured', filter: 7 },
                { value: 'GMU power up & black out', filter: 1 },
                { value: 'GMU power up & black out', filter: 2 },
                { value: 'GMU power up & black out', filter: 3 },
                { value: 'Human error (informed by TSM)', filter: 9 },                
                { value: 'No proceed transaction', filter: 4 },
                { value: 'Meter increment- game meters cleared', filter: 6 },
                { value: 'Meter increment- game did not accept funds transfer', filter: 6 },
                { value: 'Meter increment- no EFS acknowledge', filter: 6 },
                { value: 'Meter increment- game ACK\'d Funds transfer with wrong XID', filter: 6 },
                { value: 'Meter increment- shown in questionable report', filter: 6 },                
                { value: 'Meter increment- game meters cleared', filter: 7 },
                { value: 'Meter increment- game did not accept funds transfer', filter: 7 },
                { value: 'Meter increment- no EFS acknowledge', filter: 7 },
                { value: 'Meter increment- game ACK\'d Funds transfer with wrong XID', filter: 7 },
                { value: 'Meter increment- shown in questionable report', filter: 7 },
                { value: 'System error ticket auto voided', filter: 3 },
                { value: 'Too many bill rejected', filter: 1 },
                { value: 'Wrong Declaration of MCID', filter: 8 }
                
            ]
        },
        areaStore: {
            fields: [
                'value',
                'filter'
            ],
            data: [
                { value: 'AMRET PALACE', filter: 'Slot operation' },
                { value: 'AMRET SZ', filter: 'Slot operation' },
                { value: 'CMC', filter: 'Slot operation' },
                { value: 'CMC SZ', filter: 'Slot operation' },
                { value: 'NAGA ROCK', filter: 'Slot operation' },
                { value: 'PREMIER 1', filter: 'Slot operation' },
                { value: 'PREMIER 2', filter: 'Slot operation' },
                { value: 'PREMIER 4', filter: 'Slot operation' },
                { value: 'PREMIER 5', filter: 'Slot operation' },
                { value: 'PREMIER 6', filter: 'Slot operation' },
                { value: 'RAPID 1', filter: 'Slot operation' },
                { value: 'RAPID 2', filter: 'Slot operation' },
                { value: 'RAPID 3', filter: 'Slot operation' },
                { value: 'SAIGON PALACE', filter: 'Slot operation' },
                { value: 'SP VIP', filter: 'Slot operation' },
                { value: 'SPORTZONE', filter: 'Slot operation' },
                { value: 'AMRET PALACE', filter: 'TR-Soft count' },
                { value: 'AMRET SZ', filter: 'TR-Soft count' },
                { value: 'CMC', filter: 'TR-Soft count' },
                { value: 'CMC SZ', filter: 'TR-Soft count' },
                { value: 'NAGA ROCK', filter: 'TR-Soft count' },
                { value: 'PREMIER 1', filter: 'TR-Soft count' },
                { value: 'PREMIER 2', filter: 'TR-Soft count' },
                { value: 'PREMIER 4', filter: 'TR-Soft count' },
                { value: 'PREMIER 5', filter: 'TR-Soft count' },
                { value: 'PREMIER 6', filter: 'TR-Soft count' },
                { value: 'RAPID 1', filter: 'TR-Soft count' },
                { value: 'RAPID 2', filter: 'TR-Soft count' },
                { value: 'RAPID 3', filter: 'TR-Soft count' },
                { value: 'SAIGON PALACE', filter: 'TR-Soft count' },
                { value: 'SP VIP', filter: 'TR-Soft count' },
                { value: 'SPORTZONE', filter: 'TR-Soft count' },
                { value: 'CG', filter: 'TR-Table drop' },
                { value: 'HG1', filter: 'TR-Table drop' },
                { value: 'HG2', filter: 'TR-Table drop' },
                { value: 'NAGA ROCK', filter: 'TR-Table drop' },
                { value: 'SAIGON PALACE', filter: 'TR-Table drop' },
				{value:'N2GFS',filter:'Slot Operation'},
				{value:'N2LGS',filter:'Slot Operation'},
				{value:'N2LV1S',filter:'Slot Operation'},
				{value:'N2GFS',filter:'TR-Soft Count'},
				{value:'N2LGS',filter:'TR-Soft Count'},
				{value:'N2LV1S',filter:'TR-Soft Count'},
				{value:'N2GF',filter:'TR-Table Drop'},
				{value:'N2Lvl2',filter:'TR-Table Drop'},
				{value:'N2Lvl1',filter:'TR-Table Drop'}
				
            ]
        }
    },
    formulas: {

    }
});
