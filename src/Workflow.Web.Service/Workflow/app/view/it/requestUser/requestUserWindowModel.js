Ext.define('Workflow.view.it.requestUser.requestUserWindowModel', {
    extend: 'Workflow.view.common.AbstractWindowModel',
    alias: 'viewmodel.it-requestuser-requestuserwindow',
    data: {
         team  : null
    },
    formulas:{
         subDepartment: function(get){
           
         }
     },
     parseFromUserRequestItem: function (){
         var me = this;
         var item = me.data.item;
         if( 'Edit'=== me.data.action && me.data.item) {
             var record = Ext.create('Workflow.model.common.EmployeeInfo', {
                     id         : item.empId,
                     employeeNo : item.empNo,
                     fullName   : item.empName,
                     subDeptId  : item.teamId,
                     subDept    : item.teamName,
                     position   : item.position,
                     email      : item.email,
                     hiredDate  : item.hiredDate,
                     reportTo   : item.manager,
                     phone      : item.phone
                 });
            me.data.set('item',record) ;
            me.data.team =  Ext.create('Workflow.model.common.Department', {id:item.teamId, fullName: item.teamName } );
         }
     },
     parseToUserRequestItem : function(){
          var me            = this,
            record          = me.data.item,
            team            = me.data.team;
            var requestUser = null ;
            
            if(record.id || record.id==0 ){
                
                requestUser= Ext.create("Workflow.model.itRequestForm.RequestUser", {
                    empId       : record.get('id') ,
                    empNo       : record.get('employeeNo'),
                    empName     : record.get('fullName'), 
                    teamId      : team.get('id'),
                    teamName    : team.get('fullName'),
                    department  : {id:team.get('id'), fullName: team.get('fullName')},
                    position    : record.get('position'),
                    email       : record.get('email'),
                    hiredDate   :record.get('hiredDate'),
                    manager     : record.get('reportTo'),
                    phone       : record.get('phone')
                });
                
            }else{
                requestUser = Ext.create("Workflow.model.itRequestForm.RequestUser", {
                    empId       : 0,
                    empNo       : record.employeeNo,
                    empName     : record.fullName,
                    department  : {id:team.get('id'), fullName: team.get('fullName')},
                    teamId      : team.get('id'),
                    teamName    : team.get('fullName'),
                    position    : record.position,
                    email       : record.email,
                    hiredDate   : record.hiredDate,
                    manager     : record.reportTo,
                    phone       : record.phone
                });
            }
            
            return requestUser;  
     }
});
