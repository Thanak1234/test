Ext.define('Workflow.view.vaf.ItemWindowController', {
    extend: 'Workflow.view.common.AbstractWindowDialogController',
    alias: 'controller.va-itemwindow',
    getFormItem: function () {
        var me = this;
        var viewmodel = me.getView().getViewModel();
        var selection = viewmodel.get('selection');
        var outline = Ext.create('Workflow.model.vaf.Outline', {
            GamingDate: viewmodel.get('outline.GamingDate'),
            McidLocn: viewmodel.get('outline.McidLocn'),
            VarianceType: viewmodel.get('outline.VarianceType'),
            Subject: viewmodel.get('outline.Subject'),
            IncidentRptRef: selection? selection.get('Title'): null,
            ProcessInstanceId: selection? selection.get('ProcessInstanceId'): null,
            Area: viewmodel.get('outline.Area'),
            RptComparison: viewmodel.get('outline.RptComparison'),
            Amount: viewmodel.get('outline.Amount'),
            Comment: viewmodel.get('outline.Comment'),
            RouteId: selection? selection.get('RouteId'): null
        });
        return outline;
    },
    onFormView: function (el, t) {
        var me = this;
        var data = el.findRecordByValue(el.getValue()).data;
        if (data && data.ProcessInstanceId) {
            me.redirectTo(Ext.String.format('#icd-request-form/SN={0}_99999', data.ProcessInstanceId));
            me.getView().close();
        }
    }
});
