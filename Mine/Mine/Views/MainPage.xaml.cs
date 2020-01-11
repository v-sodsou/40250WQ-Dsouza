﻿using Mine.Models;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Mine.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    /// <summary>
    /// Main Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        // Collection of Navigation Pages
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        /// <summary>
        /// Constructor setups the behavior and menu pages
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;
        }

        /// <summary>
        /// Process the Menu Selected item
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task NavigateFromMenu(int id)
        {
            // See if the Page is in memory, if not load it
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemEnum.Browse:
                        MenuPages.Add(id, new NavigationPage(new ItemIndexPage()));
                        break;
                    case (int)MenuItemEnum.About:
                        MenuPages.Add(id, new NavigationPage(new AboutPage()));
                        break;
                }
            }

            // Switch to the Page
            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                // Android needs a deal, iOS and UWP does not
                if (Device.RuntimePlatform == Device.Android)
                {
                    await Task.Delay(100);
                }

                IsPresented = false;
            }
        }
    }
}