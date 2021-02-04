using System.Collections.Generic;
using StarChart.Models;

namespace StarChart.Data
{
    public interface ICelestialObjectsActions
    {
        CelestialObject GetById(int id);
        CelestialObject GetByName(string Name);
        List<CelestialObject> GetAll();
    }
}
