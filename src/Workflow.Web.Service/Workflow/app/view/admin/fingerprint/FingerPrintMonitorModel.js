Ext.define('Workflow.view.admin.FingerPrintMonitorModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.admin-fingerprintmonitor',
    data: {
        name: 'workflow',
        scheduleData: null,
        selectedRow: null
    },
    stores: {
        fingerprintStore: {
            model: 'Workflow.model.fingerprint.FingerprintMachine',
            autoLoad: true,
            pageSize: 100,
            proxy: {
                type: 'rest',
                url: Workflow.global.Config.baseUrl + 'api/fingerprints',
                reader: {
                    type: 'json',
                    rootProperty: 'data',
                    totalProperty: 'totalCount'
                }
            }
        }
    }
});
