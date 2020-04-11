using System;
using System.Collections.Generic;
using System.Text;

namespace Meihana.Models
{
    public enum MenuItemType
    {
        About,
        Placeholder,
        Diagnosis,
        Resources,
        Contact,
    }
    public class HomeMenuItem
    {
        public MenuItemType Id { get; set; }

        public string Title { get; set; }
    }
}
