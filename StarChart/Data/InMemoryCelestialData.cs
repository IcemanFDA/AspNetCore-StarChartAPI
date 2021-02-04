using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StarChart.Models;

namespace StarChart.Data
{
    public class InMemoryCelestialData : ICelestialObjectsActions
    {
        private readonly ApplicationDbContext _context;
        public InMemoryCelestialData(ApplicationDbContext context)
        {
            _context = context;
        }

        public CelestialObject GetById(int Id)
        {
            var celObj = _context.CelestialObjects.SingleOrDefault(c => c.Id == Id);
            var satelites = from c in _context.CelestialObjects
                            where c.OrbitedObjectId == celObj.Id
                            select c;

            celObj.Satellites = satelites.ToList();
            return celObj;
        }

        public CelestialObject GetByName(string Name)
        {
            var celObj = _context.CelestialObjects.SingleOrDefault(c => c.Name == Name);
            var satelites = from c in _context.CelestialObjects
                            where c.OrbitedObjectId == celObj.Id
                            select c;

            celObj.Satellites = satelites.ToList();
            return celObj;
        }

        public List<CelestialObject> GetAll()
        {
            throw new NotImplementedException();
        }

    }
}
