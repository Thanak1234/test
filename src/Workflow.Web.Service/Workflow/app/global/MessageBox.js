Ext.define('Workflow.global.ErrorMessageBox', {
    statics: {
        show: function (error) {
            var window = Ext.create('Workflow.view.common.errors.ErrorWindow', {
                viewModel: {
                    data: {
                        message: error.message,
                        detail: error.detail
                    }
                }
            });

            if (window) {
                window.show();
            }
        }
    }
});