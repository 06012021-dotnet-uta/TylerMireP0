using System;
using System.Collections.Generic;
using System.Linq;
using Domain;
using Persistence;

namespace Application.Handler
{
    internal class LocationHandler : BaseHandler, IHandler
    {

        public LocationHandler(DataContext context)
            : base(context)
        {
        }

        public bool Create(Location newLocation)
        {
            return Create<Location>(newLocation);
        }

        public Location Read(Guid id)
        {
            return Read<Location>(id);
        }

        public bool Update(Location newLocation)
        {
            return Update<Location>(newLocation, newLocation.LocationId);
        }

        public bool Delete(Guid id)
        {
            return Delete<Location>(id);
        }

        public List<Location> List()
        {
            List<Location> locations = _context.Locations.ToList();
            return locations;
        }

        public List<LocationProductInventoryJunction> ListLocationInventory(Guid locationId)
        {
            List<LocationProductInventoryJunction> locationProductInventoryJunctions = _context.LocationProductInventoryJunctions.ToList();

            IEnumerable<LocationProductInventoryJunction> locationInventory =
                from inventory in locationProductInventoryJunctions
                where inventory.LocationId == locationId
                select inventory;

            return locationInventory.ToList();
        }
    }
}