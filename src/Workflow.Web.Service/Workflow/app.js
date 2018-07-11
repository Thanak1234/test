Ext.enableAria = false;
Ext.enableAriaButtons = false;
Ext.enablePanels = false;

//1000 x 60 x 20 milli x seconds x minutes = 20 minutes
Ext.override(Ext.data.proxy.Ajax, { timeout: 1800000 }); 
Ext.override(Ext.Ajax, { timeout: 1800000 });
Ext.override(Ext.form.action.Action, { timeout: 1800000 });

//Ext.Loader.setConfig({
//    enabled: true,
//    paths: {
//        // developer experience path
//        'Workflow.ux': 'Workflow.Web.UI/classic/src/ux'
//    }
//});

Ext.define("WORKFLOW.CONST", {
    singleton: true,
	DEPT_STORE: Ext.create('Ext.data.Store', {
		autoLoad: true,
		proxy: {
			type: 'rest',
			url: Workflow.global.Config.baseUrl +
				'api/employee/dept-list',
			reader: {
				type: 'json'
			}
		}
	}),
    STATES: new Ext.data.Store({
        fields: ['REQUEST_CODE', 'ACTIVITY_NAME', 'CONFIGURATION']
    })
});


Ext.application({
	name: 'Workflow',
	extend: 'Ext.app.Application',
	requires: [
		'Workflow.*',
		'Workflow.store.common.Priorities',
        'Ng.widgets.SessionMonitor'
	],
    defaultToken : 'dashboard',
    stores: [
        'Navigation',
        'common.Priorities',
        'FormLookup'
    ],
    init: function() {
        this.loadConfiguration();
    },
	mainView: 'Workflow.view.main.Main',
	
	//-------------------------------------------------------------------------
	// Most customizations should be made to Workflow.Application. If you need to
	// customize this file, doing so below this section reduces the likelihood
	// of merge conflicts when upgrading to new versions of Sencha Cmd.
	//-------------------------------------------------------------------------
    
	launch: function () {
	     var me = this;

        Ext.Ajax.on('requestexception', function (conn, response, options) {
            //handle if error occured=>hide mask of exporting simultaneusly
            if (response.status === 504 ||
                response.status === 403 ||
                response.status === 0) {
                Ext.Msg.confirm('Error', 'Session timeout. Are you sure to reload? ', function (btn, text) {
                    if (btn == 'yes') {
                        window.location.reload();
                    }
                });
            } else if (response.status == 500) {
                var alertMsg = 'Internal Error. System cannot connect to server.';
                Ext.Msg.alert('Failed', alertMsg);
            } else if (response.status == 404) {
                Ext.Msg.alert('Failed', 'Internal Error. The requested resource was not found on this server.');
            } else {
                Ext.Msg.alert('Failed', response.responseText);
            }
        });

        var parent = Ext.get('splash');
        parent.fadeOut({
            callback: function () {
                parent.destroy();
            }
        });

        if (window.parent) {
            function CustomEvent(event, params) {
                params = params || { bubbles: false, cancelable: false, detail: undefined };
                var evt = document.createEvent('CustomEvent');
                evt.initCustomEvent(event, params.bubbles, params.cancelable, params.detail);
                return evt;
            }
            CustomEvent.prototype = window.Event.prototype;
            var event = new CustomEvent('onLoadCompleted');
            window.parent.document.dispatchEvent(event);
        }

        //this.sessionMonitor();
    },    
    onAppUpdate: function () {
        window.location.reload();
    },
    loadConfiguration: function () {
        Ext.Ajax.request({
            url: Workflow.global.Config.baseUrl + 'api/configuration/activities-properties',
            method: 'GET',
            params: { cache: false },
            success: function (response) {
                var records = Ext.JSON.decode(response.responseText);
                Ext.each(records, function (record) {
                    WORKFLOW.CONST.STATES.add({
                        REQUEST_CODE: record.RequestCode,
                        ACTIVITY_NAME: record.Name,
                        CONFIGURATION: Ext.JSON.decode(record.Property)
                    });
                });
            },
            failure: function (response) {
                console.log('error');
            }
        });
    },
    getSubmission: function () {
        return {};
    },
    sessionMonitor: function () {
        var me = this;
        var interval, sessionScreen, win = null;

        var check = function () {
            Ext.Ajax.request({
                url: Workflow.global.Config.baseUrl + 'security/ping',
                method: 'GET',
                success: function (response) {
                },
                failure: function (response) {
                    if (win == null) {
                        Ext.MessageBox.confirm({
                            title: 'Session Timeout After <span id="session-timeout-id"></span>',
                            message: 'Do you want to renew session?',
                            icon: Ext.Msg.WARNING,
                            buttons: Ext.MessageBox.YESNO,
                            fn: function (answer) {
                                if (answer == 'yes') {
                                    win = window.open(Workflow.global.Config.renewUrl, "Renew Session", "width=450,height=450");
                                    clearInterval(sessionScreen);

                                } else {
                                    window.location = "/account/logoff";
                                }
                            }
                        });
                        clearInterval(interval);
                        sessionScreen = me.startTimer(120, document.querySelector('#session-timeout-id'));
                    }

                    if (win && win.closed) {
                        console.log('run.. interval');
                        interval = setInterval(check, 5000);
                        win = null;
                    }
                }
            });
        }
        interval = setInterval(check, 5000);
    },
    startTimer: function (duration, display) {
        var timer = duration, minutes, seconds;
        return setInterval(function () {
            minutes = parseInt(timer / 60, 10);
            seconds = parseInt(timer % 60, 10);

            minutes = minutes < 10 ? "0" + minutes : minutes;
            seconds = seconds < 10 ? "0" + seconds : seconds;

            display.textContent = minutes + ":" + seconds;

            if (--timer < 0) {
                timer = duration;
                window.location = "/account/logoff";
            }
        }, 1000);
    }
});
