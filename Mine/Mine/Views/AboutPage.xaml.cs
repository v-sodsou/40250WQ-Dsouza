﻿using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace Mine.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    /// <summary>
    /// About Page
    /// </summary>
    [DesignTimeVisible(false)]
    public partial class AboutPage : ContentPage
    {
        /// <summary>
        /// Constructor for About Page
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();
            CurrentDateTime.Text = System.DateTime.Now.ToString("MM/dd/yy hh:mm:ss");
        }

        void DataSource_Toggled(object sender, EventArgs e)
        {

            if (DataSourceValue.IsToggled == true)
            {
                MessagingCenter.Send(this, "SetDataSource", 1);
            }
            else
            {
                MessagingCenter.Send(this, "SetDataSource", 0);
            }
        }
    }
}