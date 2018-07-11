Ext.define("Workflow.view.tascr.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'tascr-request-form',
    title: 'Course Registration Form',
    formType: 'TASCR_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/tascrrequest',
    bindPayloadData: function (reference) {
        var me = this,
            courseRegistration = reference.courseRegistration,
            courseInformation = reference.courseInformation,
            courseEmployee = reference.courseEmployee;

        var courseRegistrationData = courseRegistration.getViewModel().getData().courseRegistration,
            courseInformationData = courseInformation.getViewModel().getData().courseRegistration,
            store = courseEmployee.getStore();

        var data = {
            courseRegistration: Ext.merge(courseRegistrationData, courseInformationData),
            addCourseEmployees: me.getOriginDataFromCollection(store.getNewRecords()),
            editCourseEmployees: me.getOriginDataFromCollection(store.getUpdatedRecords()),
            delCourseEmployees: me.getOriginDataFromCollection(store.getRemovedRecords())
        }

        return data;
    },
    loadDataToView: function (reference, data, acl) {
        var me = this,
            viewmodel = me.getViewModel();

        var viewSetting = me.currentActivityProperty;
		
        if (data && data.courseRegistration && data.courseRegistration.reminderOn) {
			data.courseRegistration.reminderOn = new Date(data.courseRegistration.reminderOn);
		}

        this.fireEventLoad(reference.courseRegistration, data);
		this.fireEventLoad(reference.courseInformation, data);
        this.fireEventLoad(reference.courseEmployee, data);
        
    },
    clearData: function (reference) {
        reference.courseRegistration.getForm().reset();
        reference.courseInformation.getForm().reset();
        reference.courseEmployee.fireEvent('onDataClear');
    },
    validateForm: function(reference, data){
        var me = this,
            courseRegistration = reference.courseRegistration,
            courseInformation = reference.courseInformation,
            courseEmployee = reference.courseEmployee;

        var treatmentData = courseRegistration.getViewModel().getData().courseRegistration,
            courseEmployeeStore = courseEmployee.getStore();
        if (data) {
            if (!courseRegistration.isValid()) {
                return "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            }
            if (!courseInformation.isHidden() && !courseInformation.isValid()) {
                return "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            } 
            if (!courseEmployee.isHidden() && !courseEmployeeStore.getAt(0)) {
                return "Please add item to employee list before you take action.";
            }
        }
    },
    buildComponent: function () {
        var me = this;
        return [{
            margin: 5,
            sectionName: 'COURSE_REG',
            title: 'Course Registration',
            xtype: 'tascr-course-registration-view',
            reference: 'courseRegistration',
            mainView: me,
            //bind: {
            //    hidden: '{courseRegistrationProperty.hidden}'
            //},
            border: true
        },{
            margin: 5,
            minHeight: 150,
            title: 'Employee Information',
            xtype: 'tascr-course-employee-view',
            reference: 'courseEmployee',
            mainView: me,
            border: true,
            bind: {
                hidden: '{courseEmployeeProperty.hidden}'
            }
        }, {
            margin: 5,
            sectionName: 'COURSE_INFO',
            title: 'Course Information',
            xtype: 'tascr-course-registration-view',
            reference: 'courseInformation',
            mainView: me,
            bind: {
                hidden: '{courseRegistrationProperty.hidden}'
            },
            border: true
        }];
    }
});
