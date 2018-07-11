Ext.define("Workflow.view.mtf.RequestForm", {
    extend: "Workflow.view.ApplicationComponent",
    xtype: 'mtf-request-form',
    title: 'Medical Treatment Form',
    formType: 'MT_REQ',
    actionUrl: Workflow.global.Config.baseUrl + 'api/mtfrequest',
    bindPayloadData: function (reference) {
        var me = this,
            treatmentView = reference.treatmentView,
            prescriptionView = reference.prescriptionView,
            unfittoworkView = reference.unfittoworkView;

        var treatmentData = treatmentView.getViewModel().getData().treatment,
            prescriptionStore = prescriptionView.getStore(),
            unfittoworkStore = unfittoworkView.getStore();

            

        var data = {
            treatment: treatmentData,
            // Prescription
            addPrescriptions: me.getOriginDataFromCollection(prescriptionStore.getNewRecords()),
            editPrescriptions: me.getOriginDataFromCollection(prescriptionStore.getUpdatedRecords()),
            delPrescriptions: me.getOriginDataFromCollection(prescriptionStore.getRemovedRecords()),
            // UnfitToWork
            addUnfitToWorks: me.getOriginDataFromCollection(unfittoworkStore.getNewRecords()),
            editUnfitToWorks: me.getOriginDataFromCollection(unfittoworkStore.getUpdatedRecords()),
            delUnfitToWorks: me.getOriginDataFromCollection(unfittoworkStore.getRemovedRecords())
        }

        return data;
    },
    afterTakeAction: function (data) {
        if (data && data.dataHeader.record) {
            var header = data.dataHeader.record.getData();
            this.triggerSignalR(header.requestHeaderId, data.action);
        }
    },
    triggerSignalR: function(requestHeaderId, state){
        if (!window.hubReady) {
            window.hubReady = $.connection.hub.start();
        }
        window.hubReady.done(function () {
            window.HUB_FINGER_PRINT.server.processPatient(requestHeaderId, state);
        });
    },
    loadDataToView: function (reference, data, acl) {
        var me = this,
            treatmentView = reference.treatmentView,
            prescriptionView = reference.prescriptionView,
            unfittoworkView = reference.unfittoworkView,
            viewmodel = me.getViewModel();

        var viewSetting = me.currentActivityProperty;
        
        this.fireEventLoad(reference.treatmentView, data);
        this.fireEventLoad(reference.unfittoworkView, data);
        this.fireEventLoad(reference.prescriptionView, data);

        var refMC = prescriptionView.getReferences();
		var hasMedicine = (data && data.prescriptions && data.prescriptions.length > 0);
		
        if (viewSetting && viewSetting.activityName == 'Modification') {
            if (refMC.ignoreMedicine) {
                // if has [prescriptions] data set [ignoreMedicine:false]
                refMC.ignoreMedicine.setValue((hasMedicine ? false : true));
            }
        }

        if (viewSetting && viewSetting.activityName == 'Form View') {
            var showDoctorInfo = false;
            var showSymptom = (acl ? acl.getValue('SHOW_DOCTOR_PANEL') : false);
                //data.treatment ? data.treatment.ShowSymptom : false;
            console.log('showSymptom', showSymptom);
            if (
                viewSetting.lastActivity == 'Submission' ||
                viewSetting.lastActivity == 'Request' ||
                viewSetting.lastActivity == 'HoD Decision' ||
                viewSetting.lastActivity == 'Requestor Rework'
            ) {
                showDoctorInfo = false;
            }

            if (
                viewSetting.lastActivity == 'Doctor Examine and Treat' ||
                viewSetting.lastActivity == 'Modification'
            ) {
                showDoctorInfo = true;
            }
			
			if (refMC.ignoreMedicine) {
                // if has [prescriptions] data set [ignoreMedicine:false]
                refMC.ignoreMedicine.setValue((hasMedicine ? false : true));
            }

            viewmodel.set('treatmentProperty', {
                hidden: false,

                WorkShift: { readOnly: true, hidden: false },
                Comment: { readOnly: true, hidden: false },
                FitToWork: { readOnly: true, hidden: !showDoctorInfo },
                TimeArrived: { readOnly: true, hidden: !showDoctorInfo },
                TimeDeparted: { readOnly: true, hidden: !showDoctorInfo },
                CheckInDateTime: { readOnly: true, hidden: !showDoctorInfo },
                CheckOutDateTime: { readOnly: true, hidden: !showDoctorInfo },
                Days: { readOnly: true, hidden: !showDoctorInfo },
                Symptom: { readOnly: true, hidden: !(showDoctorInfo && showSymptom) }, // confidential information only for doctor
                Diagnosis: { readOnly: true, hidden: !(showDoctorInfo && showSymptom) }, // confidential information only for doctor
                Hours: { readOnly: true, hidden: !showDoctorInfo },
                Remark: { readOnly: true, hidden: !showDoctorInfo },
                Annotation: { hidden: !showDoctorInfo }
            });
           
            viewmodel.set('prescriptionProperty', {
                hidden: !(showDoctorInfo && showSymptom), // confidential information only for doctor
                
                add: { disabled: true, hidden: false },
                edit: { disabled: true, hidden: false },
                view: { disabled: true, hidden: true },
                remove: { disabled: true, hidden: true }
            });
            
            if (refMC.ignoreMedicine) {
                refMC.ignoreMedicine.setDisabled(true);
            }

            viewmodel.set('unfittoworkProperty', {
                hidden: !showDoctorInfo,
                
                add: { disabled: true, hidden: false },
                edit: { disabled: true, hidden: false },
                view: { disabled: true, hidden: false },
                remove: { disabled: true, hidden: true }
            });
        }

        if (viewSetting && viewSetting.activityName == 'Doctor Examine and Treat') {
            this.triggerSignalR(0, 'OPEN');
        }
    },
    clearData: function (reference) {
        reference.treatmentView.getForm().reset();
        reference.prescriptionView.fireEvent('onDataClear');
        reference.unfittoworkView.fireEvent('onDataClear');
    },
    confirmMessage: function(reference, data){
        var me = this,
            treatmentView = reference.treatmentView,
            prescriptionView = reference.prescriptionView,
            unfittoworkView = reference.unfittoworkView;

        var prescriptionStore = prescriptionView.getStore()
        if (data && !(data.action == "Rejected" || data.action == "Reworked")) {
            if (!prescriptionView.isHidden() && !prescriptionStore.getAt(0)) {
                return ("<span style='color:#1abc9c'>Are you sure you don't want to add " +
                "prescription and take this action?</span>");
            }
        }
    },
    validateForm: function(reference, data){
        var me = this,
            treatmentView = reference.treatmentView,
            prescriptionView = reference.prescriptionView,
            unfittoworkView = reference.unfittoworkView;
    
        var treatmentData = treatmentView.getViewModel().getData().treatment,
            prescriptionStore = prescriptionView.getStore(),
            unfittoworkStore = unfittoworkView.getStore();
        if (data && !(data.action == "Rejected" || data.action == "Reworked")) {
            
            if (!treatmentView.isValid()) {
                return "Some fields of form request are required. Please input the required field(s) before you click the take action.";
            }
            if (!unfittoworkView.isHidden() && treatmentData.FitToWork == 0) {
                if (!unfittoworkStore.getAt(0)) {
                    return "Please add item to unfit to work list before you take action.";
                }
                if (unfittoworkView.duplicate) {
                    return "Unfit to work date are overlapped on previous request.";
                }
            }

            if(data.activity == 'Doctor Examine and Treat' || data.activity == 'Modification') {
                if (prescriptionStore && prescriptionStore.count() <= 0) {
                    refMC = prescriptionView.getReferences();
                    if (refMC.ignoreMedicine && !refMC.ignoreMedicine.value) {
                        return "Please add medicine or check [No need medicine] anyway.";
                    }
                } else {
                    var k, repeat = [], state = {};
                    prescriptionStore.each(function (r) {
                        k = r.get('medicineId');
                        if (state[k]) repeat.push(r);
                        else state[k] = true;
                    });

                    if (repeat.length > 0) {
                        return 'Duplicate medicine code [' + repeat[0].getData().medicine + ']';
                    }
                }
            }
        }
    },
    buildComponent: function () {
        var me = this;
        return [{
            margin: 5,
            xtype: 'mtf-treatment-view',
            reference: 'treatmentView',
            mainView: me,
            bind: {
                hidden: '{treatmentProperty.hidden}'
            },
            border: true
        }, {
            xtype: 'mtf-unfittowork-view',
            reference: 'unfittoworkView',
            margin: 5,//'5 0 10 155',
            mainView: me,
            bind: {
                hidden: '{unfittoworkProperty.hidden}'
            },
            border: true
        }, {
            xtype: 'mtf-prescription-view',
            reference: 'prescriptionView',
            margin: 5,
            mainView: me,
            bind: {
                hidden: '{prescriptionProperty.hidden}'
            },
            border: true
        }];
    }
});
