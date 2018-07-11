using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workflow.Domain.Entities.Reservation;
using Workflow.DataAcess.Infrastructure;
using Workflow.DataAcess.Repositories.Interfaces;
using Workflow.DataObject.Reservation;
using System.Data.SqlClient;
using System.Data.Entity;

namespace Workflow.DataAcess.Repositories.Reservation
{
    public class ComplimentaryCheckItemRepository : RepositoryBase<ComplimentaryCheckItem>, IComplimentaryCheckItemRepository
    {
        public ComplimentaryCheckItemRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public ComplimentaryCheckItem GetComplimentaryCheckItemByRequestHeader_TypeId(int RequestHeaderId,int TypeId)
        {
            IDbSet<ComplimentaryCheckItem> dbSet = DbContext.Set<ComplimentaryCheckItem>();
            try
            {


                return dbSet.Where(n => n.RequestHeaderId == RequestHeaderId && n.ExplId == TypeId).SingleOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public IList<ComplimentaryCheckItem> GetComplimentaryCheckItemByRequestHeader(int id)
        {
            IDbSet<ComplimentaryCheckItem> dbSet = DbContext.Set<ComplimentaryCheckItem>();
            try
            {
                return dbSet.Where(n => n.RequestHeaderId == id).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public IList<ComplimentaryCheckItemExt> GetComplimentaryCheckItem(int RequestHeaderId)
        {
            IEnumerable<ComplimentaryCheckItemExt> checkitem = new List<ComplimentaryCheckItemExt>();

            string sql = @"SELECT * FROM ( 
                            SELECT expl.ID id,expcl.REQUEST_HEADER_ID RequestHeaderId,expcl.EXPLID ExplId, 
                            expcl.CREATEDATE CreatedDate,expl.NAME CheckName, 
                            CAST(CASE WHEN expcl.EXPLID IS NULL THEN 0 ELSE 1 END AS BIT) 'Check' 
                            FROM RESERVATION.COMPLIMENTARY_EXPENSE_LIST expl 
                            INNER JOIN RESERVATION.COMPLIMENTARY_EXPENSE_CHECKLIST expcl ON expcl.EXPLID = expl.ID 
                            WHERE expcl.REQUEST_HEADER_ID = @requestheaderid 

                            UNION 

                            SELECT expl.ID id,NULL RequestHeaderId,NULL ExplId, 
                            NULL CreatedDate,expl.NAME CheckName, 
                            CAST(0 AS BIT) 'Check' 
                            FROM RESERVATION.COMPLIMENTARY_EXPENSE_LIST expl 
                                WHERE expl.ID NOT IN (SELECT expcl.EXPLID FROM RESERVATION.COMPLIMENTARY_EXPENSE_CHECKLIST expcl WHERE expcl.REQUEST_HEADER_ID = @requestheaderid) 
                            ) A 
                            ORDER BY A.id ASC";

            IEnumerable<ComplimentaryCheckItemExt> CheckItemList = SqlQuery<ComplimentaryCheckItemExt>(sql, new object[]
                                                                {
                                                                    new SqlParameter("@requestheaderid", RequestHeaderId.ToString())
                                                                });
            return CheckItemList.ToList();
        }


        public ComplimentaryCheckItemLS GetPivotComplimentaryCheckItem(int RequestHeaderId)
        {
            ComplimentaryCheckItemExt checkitem = new ComplimentaryCheckItemExt();

            string sql = @"SELECT 
                            CAST([Meal Excluding Alcohol] AS BIT) 'MealExcludingAlcohol',
                            CAST([Alcohol] AS BIT) 'Alcohol',
                            CAST([Tobacco] AS BIT) 'Tobacco',
                            CAST([Spa] AS BIT) 'Spa',
                            CAST([Souvenir Shop] AS BIT) 'SouvenirShop',
                            CAST([Airport Transfers] AS BIT) 'AirportTransfers',
                            CAST([Other Transport within City] AS BIT) 'OtherTransportwithinCity',
                            CAST([Extra Bed] AS BIT) 'ExtraBeds'
                            FROM ( 
                                SELECT expl.NAME CheckName,
                                CAST(CASE WHEN expcl.EXPLID IS NULL THEN 0 ELSE 1 END AS INT) 'Check' 
                                FROM RESERVATION.COMPLIMENTARY_EXPENSE_LIST expl 
                                INNER JOIN RESERVATION.COMPLIMENTARY_EXPENSE_CHECKLIST expcl ON expcl.EXPLID = expl.ID 
                                WHERE expcl.REQUEST_HEADER_ID = @requestheaderid 

                                UNION 

                                SELECT expl.NAME CheckName,
                                CAST(0 AS INT) 'Check' 
                                FROM RESERVATION.COMPLIMENTARY_EXPENSE_LIST expl 
                                    WHERE expl.ID NOT IN (SELECT expcl.EXPLID FROM RESERVATION.COMPLIMENTARY_EXPENSE_CHECKLIST expcl WHERE expcl.REQUEST_HEADER_ID = @requestheaderid) 
                            ) AA 
                            PIVOT(MIN([Check]) FOR CheckName IN ([Meal Excluding Alcohol],[Alcohol],[Tobacco],
                            [Spa], [Souvenir Shop],[Airport Transfers],[Other Transport within City],[Extra Bed])) AS PVT";

            ComplimentaryCheckItemLS CheckItem = SqlQuery<ComplimentaryCheckItemLS>(sql, new object[]
                                                                {
                                                                    new SqlParameter("@requestheaderid", RequestHeaderId.ToString())
                                                                }).SingleOrDefault();
            return CheckItem;
        }


    }
}
