
$(function () {
    window.HUB_FINGER_PRINT = $.connection.fingerPrintHub;
    window.HUB_FINGER_PRINT.client.reloadPatientList = function (toasts) {
        var patientDashboard = Ext.getCmp('ng-patient-dashboard');
        if (patientDashboard) {
            if (toasts && toasts[0]) {
                var toast = toasts[0];
                if (toast.STATE == 'CALL') {
                    var audio = new Audio();
                    audio.src = '/api/sounds/english?empId=' + toast.TITLE.replace('ID ', '');
                    audio.playbackRate = 1.2;
                    audio.onended = function () {
                  
                    };
                    audio.play();
                } else {
                    patientDashboard.refresh(toast.STATE);
                }

                if (toast.TITLE && toast.MESSAGE) {
                    patientDashboard.showToast(toast.TITLE, toast.MESSAGE);
                }
            }
        }
    };    
});