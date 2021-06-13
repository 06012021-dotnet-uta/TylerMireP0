using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Persistence;

namespace Application.Handler
{
    public class LocationHandler : BaseHandler, IHandler
    {

        public LocationHandler(DataContext context)
            : base(context)
        {
        }

        public bool Create(Location newLocation)
        {
            return base.Create<Location>(newLocation);
        }

        public Location Read(Guid id)
        {
            return base.Read<Location>(id);
        }

        public bool Update(Location newLocation)
        {
            return base.Update<Location>(newLocation, newLocation.LocationId);
        }

        public bool Delete(Guid id)
        {
            return base.Delete<Location>(id);
        }

        public List<Location> List()
        {
            List<Location> locations = _context.Locations.ToList();
            return locations;
        }
    }
}