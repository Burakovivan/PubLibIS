using PubLibIS.DAL.Interfaces;
using PubLibIS.DAL.Models;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Data;

namespace PubLibIS.DAL.Repositories.Dapper
{
    public class BrochureRepository : Repository<Brochure>, IBrochureRepository
    {

        public BrochureRepository(DapperConnectionFactory dapperConnectionFactory)
        : base(dapperConnectionFactory) { }



        //private void LoadNavigationProperties(Brochure brochure, IDbConnection db)
        //{
        //    if(brochure.PublishingHouse_Id == null)
        //    {
        //        return;
        //    }
        //    brochure.PublishingHouse = db.QuerySingleOrDefault<PublishingHouse>($"SELECT * FROM [PublishingHouses] WHERE Id = @id", new { id = brochure.PublishingHouse_Id });
        //}

        //private void LoadNavigationProperties(IEnumerable<Brochure> brochures, IDbConnection db)
        //{
        //    brochures = brochures.ToList();
        //    ((List<Brochure>)brochures).ForEach(p => LoadNavigationProperties(p, db));
        //}
    }
}
