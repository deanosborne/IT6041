using System;
using System.Collections.Generic;
using System.Text;

namespace App5.Models
{
    public enum MenuItemType
    {
        Home,
        About,
        Resources,
        Contributors,
        Contact,
        Diagnosis
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
