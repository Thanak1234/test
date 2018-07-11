Ext.define('Workflow.view.reports.procInst.ProcessInstanceReportController', {
    extend: 'Ext.app.ViewController',
    alias: 'controller.report-processinstancereport',
    config: {
        control: {
            'form textfield': {
                keypress: 'onKeyEnterSearch'
            }
        }
    },
    getResultSet: function () { // return store
        var me = this,
                report = me.getView(),
                processInstanceCriteria = report.getReferences().processInstanceCriteria,
                processInstanceReport = report.getReferences().processInstanceReport,
                viewmodel = processInstanceCriteria.getViewModel();

        var store = processInstanceReport.getStore();
        store.clearFilter();
        store.getProxy().extraParams = {};
        var param = viewmodel.get('criteria');
        var empDept = viewmodel.get('empDept');
        
        if (param && param.exportType) {
            param.exportType = null;
        }
        if (param && param.Activities) {
            param.CurrentActivity = param.Activities.toString();
            param.Activities = null;
        }

        param.deptList = me.getDepartmentList(param);
        if (empDept === 'DEPT' && param.EmployeeId > 0) {
            param.EmployeeId = param.EmployeeId;// * (-1);
        } else if (param.EmployeeId == null) {
            param.EmployeeId = 0;
        }
        // overwrite param and custom store
        var customStore = me.buildStore(store, param, viewmodel);
        customStore.getProxy().url = processInstanceReport.url;
        var modelClassName = '_' + viewmodel.$className;
        customStore.setModel(Ext.create(modelClassName));
        customStore.getProxy().extraParams = param;
        return customStore;
    },
    resetCriteriaForm: function (el) {
        var me = this,
            report = me.getView(),
            processInstanceCriteria = report.getReferences().processInstanceCriteria,
            processInstanceReport = report.getReferences().processInstanceReport,
            viewmodel = processInstanceCriteria.getViewModel();
        processInstanceCriteria.reset();
        viewmodel.set('showEmp', true);
        viewmodel.set('showDept', false);
        viewmodel.set('criteria', {});
        //if(el.folio != 'undefined'){
        //    viewmodel.set('criteria', {
        //        Folio: el.folio
        //    });
        //}
        
        var currentUser = Workflow.global.UserAccount.identity;
        if (currentUser) {
        //   viewmodel.set('criteria.EmployeeId', currentUser.id);
        }

        var store = processInstanceReport.getStore();
        store.clearFilter();
        store.getProxy().extraParams = {};
        
        //if(el.folio){
        //    this.onSearch();
        //}
    },
    getDepartmentList: function (param) {
        var me = this,
            report = me.getView(),
            criteria = report.getReferences().processInstanceCriteria;
        var deptList = [],
            store = criteria.teamStore;

        store.clearFilter();
        store.filterBy(function (record, id) {
            if (param.teamIds && param.teamIds.length > 0) {
                return (Ext.Array.indexOf(param.teamIds, record.get("id")) !== -1);
            }
            if (param.deptNames && param.deptNames.length > 0) {
				if(param.groupNames && param.groupNames.length > 0){
					return (Ext.Array.indexOf(param.deptNames, record.get("deptName")) !== -1) && (Ext.Array.indexOf(param.groupNames, record.get("groupName")) !== -1);	
				}else{
					return (Ext.Array.indexOf(param.deptNames, record.get("deptName")) !== -1);
				}
            }
            if (param.groupNames && param.groupNames.length > 0) {
                return (Ext.Array.indexOf(param.groupNames, record.get("groupName")) !== -1);
            }
            return false;
        }, this);

        store.each(function (record) {
            console.log('Team', record.get("fullName"));
            deptList.push(record.get("id"));
        });

        return deptList;
    },
    reloadCriteria: function () {
        
    },
    buildStore: function (store, params, viewmodel) {
        store.getProxy().extraParams = params;
        return store;
    },
    onKeyEnterSearch: function (field, event) {
        if (event.getKey() == event.ENTER) {
            this.onSearch();
        }
    },
    onSearch: function () {
        this.getResultSet().loadPage(1);
    },
    onExportPdf: function () {
        this.getResultSet().render('pdf');
    },
    onExportExcel: function () {
        this.getResultSet().render('xls');
    }
});
