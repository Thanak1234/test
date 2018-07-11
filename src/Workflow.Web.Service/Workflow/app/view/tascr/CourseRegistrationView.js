Ext.define("Workflow.view.tascr.CourseRegistrationView", {
    extend: "Workflow.view.FormComponent",
    xtype: 'tascr-course-registration-view',
    modelName: 'courseRegistration',
    title: 'Course Registration',
    loadData: function (data, viewSetting) {
        var me = this,
             viewmodel = me.getViewModel();

        if (data) {
            viewmodel.set('courseRegistration', data.courseRegistration);
        }
    },

    buildComponent: function () {
        var me = this;
            viewmodel = me.getViewModel(),
            mainVM = me.mainView.getViewModel();

        var isOriginalCourse = (me.sectionName == 'COURSE_REG');
        var formFields = [{
            xtype: 'container',
            layout: 'hbox',
            margin: '0 0 5 0',
            defaultType: 'textfield',
            defaults: { labelAlign: 'right', labelWidth: 180 },
            items: [{
                xtype: 'combo',
                fieldLabel: 'Course Title',
                flex: 1,
                mainView: me,
                store: {
                    autoLoad: true,
                    proxy: {
                        type: 'rest',
                        url: Workflow.global.Config.baseUrl +
                            'api/lookup?name=[HR].[COURSE_REGISTRATION].[CourseId]',
                        reader: {
                            type: 'json'
                        }
                    }
                },
                queryMode: 'local',
                minChars: 0,
                forceSelection: true,
                editable: true,
                displayField: 'value',
                valueField: 'id',
                allowBlank: false,
                mainView: me,
                bind: {
                    value: isOriginalCourse ? '{courseRegistration.originalCourseId}':'{courseRegistration.courseId}'
                },
                listeners: {
                    change: function (combo) {
                        combo.store.load();
                    }
                }
            }, {
                xtype: 'textfield',
                fieldLabel: 'Date',
                flex: 1,
                allowBlank: isOriginalCourse,
                bind: {
                    value: isOriginalCourse ? '{courseRegistration.originalCourseDate}':'{courseRegistration.courseDate}'
                }
            }]
        }];

        if (!isOriginalCourse) {
            formFields.push({
                xtype: 'container',
                layout: 'hbox',
                margin: '0 0 5 0',
                defaultType: 'textfield',
                defaults: { labelAlign: 'right', labelWidth: 180 },
                items: [{
                    fieldLabel: 'Trainer/Facilitator',
                    flex: 1,
                    allowBlank: false,
                    bind: { value: '{courseRegistration.trainerName}' }
                }, {
                    fieldLabel: 'Venue',
                    flex: 1,
                    allowBlank: false,
                    bind: { value: '{courseRegistration.venue}' }
                }]
            }, {
                xtype: 'container',
                layout: 'hbox',
                margin: '0 0 5 0',
                defaultType: 'textfield',
                defaults: { labelAlign: 'right', labelWidth: 180 },
                items: [{
                    xtype: 'datefield',
                    fieldLabel: 'Send Reminder On',
                    width: 350,
                    allowBlank: false,
                    minValue: new Date(),
                    bind: { value: '{courseRegistration.reminderOn}' }
                }, {
                    xtype: 'label',
                    margin: '8 5 5 5',
                    width: 100,
                    html: 'At 09:00 AM'
                }, {
                    fieldLabel: 'Duration',
                    flex: 1,
                    bind: { value: '{courseRegistration.duration}' }
                }]
            });
        }
        
        return formFields;
    }
});