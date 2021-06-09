using System;
using System.Collections.Generic;

namespace Domain
{
    public class Location
    {
        public Dictionary<string, Stack<IPurchaseable>> inventory;
    }
}