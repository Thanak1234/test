Ext.define("Workflow.view.reports.avir.AvirReportCriteria", {
    extend: "Workflow.view.reports.procInst.ProcessInstanceCriteria",
    xtype: 'report-avirCriteria',
    title: 'AV Incident Report - Criteria',
    viewModel: {
        type: "report-avir"
    },
    buildFields: function (fields) {

        var me = this;

        me.setFieldHidden(fields);

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Incident Date',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [
                {
                    xtype: 'datefield',
                    name: 'incidentDate',
                    fieldLabel: 'Incident Date',
                    emptyText: 'START DATE',
                    margin: '0 5 0 0',
                    bind: {
                        value: '{criteria.IncidentStartDate}',
                        readOnly: '{!editable}'
                    }
                },
                {
                    xtype: 'datefield',
                    name: 'incidentDate',
                    fieldLabel: 'Incident Date',
                    emptyText: 'END DATE',
                    margin: '0 5 0 0',
                    bind: {
                        value: '{criteria.IncidentEndDate}',
                        readOnly: '{!editable}'
                    }
                }]
        });

        fields.push({
            xtype: 'fieldcontainer',
            fieldLabel: 'Reported By',
            layout: 'hbox',
            defaults: {
                flex: 1,
                hideLabel: true,
                maxWidth: 250
            },
            items: [
                {
                    fieldLabel: 'Reported By',
                    hidden: false,
                    emptyText: 'REPORTER <ALL>',
                    xtype: 'employeePickup',
                    maxWidth: 250,
                    loadCurrentUser: false,
                    afterClear: function (combo) {
                        var viewmodel = me.getViewModel();
                        viewmodel.set('criteria.ReporterId', null);
                    },
                    listConfig: {
                        minWidth: 250,
                        resizable: true,
                        loadingText: 'Searching...',
                        emptyText: 'No matching posts found.',
                        itemSelector: '.search-item',

                        // Custom rendering template for each item
                        itemTpl: ['<span>{employeeNo} - {fullName}</span>']
                    },
                    bind: {
                        value: '{criteria.ReporterId}'
                    }
                }]
        });

        return fields;
    },

    setFieldHidden: function (fields) {
        var field = fields[0];
        field.items[1].hidden = true;
        field = fields[1];
        field.items[0].hidden = true;
        field.maxWidth = 610;
    }
});