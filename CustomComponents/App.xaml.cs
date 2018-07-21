﻿using System.Collections.Generic;
using CustomComponents.Models;
using CustomComponents.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CustomComponents {
    public partial class App : Application {

        public App() {
            InitializeComponent();

            var items = new List<SampleItem> {
                SampleItem.For<PressedStateButtonPage>("Button with pressed state")
            };

            MainPage = new NavigationPage(new MainPage(items));
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }

    }
}
