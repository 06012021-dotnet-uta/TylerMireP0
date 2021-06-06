using System;
using System.Collections.Generic;

namespace Domain
{
    public class Location
    {
        public Dictionary<string, Stack<IPurchaseable>> inventory;

        public Location()
        {
            inventory = new Dictionary<string, Stack<IPurchaseable>>();
        }

        public bool getProduct(string name, out IPurchaseable item)
        {
            bool success = false;
            Stack<IPurchaseable> itemsFromInventory = null;
            item = null;

            try{
                success = inventory.TryGetValue(name, out itemsFromInventory);
            }
            catch(Exception e)
            {
                success = false;
                Console.WriteLine(e.ToString());
            }

            if(itemsFromInventory == null || itemsFromInventory.Count < 1)
                success = false;
            else
                item = itemsFromInventory.Pop();

            return success;
        }
    }
}