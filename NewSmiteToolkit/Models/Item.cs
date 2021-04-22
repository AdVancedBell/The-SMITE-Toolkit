using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NewSmiteToolkit.Models
{
    public class Item
    {
        /*
         
        {"ActiveFlag":"y","ChildItemId":0,"DeviceName":"Iron Mail","IconId":2866,"ItemDescription":{"Description":"Physical Protection and Health.",
        "Menuitems":[{"Description":"Health","Value":"+75"},{"Description":"Physical Protection","Value":"+10"}],"SecondaryDescription":null},
        "ItemId":7526,"ItemTier":1,"Price":650,"RestrictedRoles":"no restrictions","RootItemId":7526,"ShortDesc":"Physical Protection and Health.","StartingItem":false,"Type":"Item",
        "itemIcon_URL":"https:\/\/webcdn.hirezstudios.com\/smite\/item-icons\/iron-mail.jpg","ret_msg":null}

         */

        public string DeviceName { get; set; }  // item name
        public Menuitem ItemDescription { get; set; }
        public Menuitem SecondaryDescription { get; set; }
        public List<Menuitem> Menuitems { get; set; }
        public string ItemId { get; set; }  
        public string ChildItemId { get; set; }     // item 1 tier below
        public string RootItemId { get; set; }      // base item
        public string ItemTier { get; set; }
        public string Price { get; set; }
        public string RestrictedRoles { get; set; }
        public string ShortDesc { get; set; }
        public bool StartingItem { get; set; }
        public string Type { get; set; }

        public string itemIcon_URL { get; set; }
    }
}