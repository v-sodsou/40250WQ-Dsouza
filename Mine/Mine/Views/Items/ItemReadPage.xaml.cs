﻿using System.ComponentModel;
using Xamarin.Forms;

using Mine.Models;
using Mine.ViewModels;
using System;

namespace Mine.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer

    /// <summary>
    /// The Read Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class ItemReadPage : ContentPage
    {
        // View Model for Item
        ItemViewModel ViewModel;

        /// <summary>
        /// Constructor called with a view model
        /// This is the primary way to open the page
        /// The viewModel is the data that should be displayed
        /// </summary>
        /// <param name="viewModel"></param>
        public ItemReadPage(ItemViewModel data)
        {
            InitializeComponent();

            BindingContext = this.ViewModel = data;
        }

        /// <summary>
        /// Save calls to Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Update_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemUpdatePage(new ItemViewModel(ViewModel.Data)));
        }

        /// <summary>
        /// Calls for Delete
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void Delete_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ItemDeletePage(new ItemViewModel(ViewModel.Data)));
        }
    }
}