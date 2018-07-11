Ext.define('Workflow.view.eom.EOMDetailViewModel', {
    extend: 'Ext.app.ViewModel',
    alias: 'viewmodel.eom-eomdetailview',
    data: {
        eomInfo: {
            month: null,
            aprd: 0,
            cfie: 0,
            lc: 0,
            tmp: 0,
            psdm: 0,
            totalScore: 0,            
            reason: null,
            cash: 0,
            voucher: 0
        },
        count: 0,
        viewSetting : null
    },
    formulas: {
        editable : function(get) {
            if (get('viewSetting') && get('viewSetting').eomdetailview.editable) {
                return true;
            } else {
                return false;
            }
        },
        hidetd: function (get) {
            if (get('viewSetting') && get('viewSetting').eomdetailview.hidetd) {
                return true;
            } else {
                return false;
            }
        },
        tdedit: function (get) {
            if (get('viewSetting') && get('viewSetting').eomdetailview.tdedit) {
                return true;
            } else {
                return false;
            }
        },
        total: {
            get: function (get) {
                var aprd = get('eomInfo.aprd') ? get('eomInfo.aprd') : 0,
                cfie = get('eomInfo.cfie') ? get('eomInfo.cfie') : 0,
                lc = get('eomInfo.lc') ? get('eomInfo.lc') : 0,
                tmp = get('eomInfo.tmp') ? get('eomInfo.tmp') : 0,
                psdm = get('eomInfo.psdm') ? get('eomInfo.psdm') : 0;

                var total = aprd + cfie + lc + tmp + psdm;
                this.set('eomInfo.totalScore', total);
                return total;
            }
        }
    }
});
