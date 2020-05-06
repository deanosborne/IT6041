﻿using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using App5.Views.Folder;
using Xamarin.Forms;

namespace App5.Views.Catalog
{
    /// <summary>
    /// Page to list out article items.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ArticleListPage
    {
        UserContext _usercontext;
        /// <summary>
        /// Initializes a new instance of the <see cref="ArticleListPage" /> class.
        /// </summary>
        public ArticleListPage()
        {
            InitializeComponent();
        }
    }
}