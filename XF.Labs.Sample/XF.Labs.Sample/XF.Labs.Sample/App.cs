﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Labs;
using Xamarin.Forms.Labs.Controls;
using Xamarin.Forms.Labs.Mvvm;
using Xamarin.Forms.Labs.Services;
using XF.Labs.Sample.Pages.Controls;

namespace XF.Labs.Sample
{
    public class App
    {

        /// Initializes the application.
        /// </summary>
        public static void Init()
        {

            var app = Resolver.Resolve<IXFormsApp>();
            if (app == null)
            {
                return;
            }

            app.Closing += (o, e) => Debug.WriteLine("Application Closing");
            app.Error += (o, e) => Debug.WriteLine("Application Error");
            app.Initialize += (o, e) => Debug.WriteLine("Application Initialized");
            app.Resumed += (o, e) => Debug.WriteLine("Application Resumed");
            app.Rotation += (o, e) => Debug.WriteLine("Application Rotated");
            app.Startup += (o, e) => Debug.WriteLine("Application Startup");
            app.Suspended += (o, e) => Debug.WriteLine("Application Suspended");
        }

        /// <summary>
        /// Gets the main page.
        /// </summary>
        /// <returns>The Main Page.</returns>
        public static Page GetMainPage()
        {
            // Register our views with our view models
            ViewFactory.Register<MvvmSamplePage, MvvmSampleViewModel>();
            ViewFactory.Register<NewPageView, NewPageViewModel>();
            ViewFactory.Register<GeolocatorPage, GeolocatorViewModel>();
            ViewFactory.Register<CameraPage, CameraViewModel>();
            ViewFactory.Register<CacheServicePage, CacheServiceViewModel>();

            var mainTab = new ExtendedTabbedPage() { Title = "Xamarin Forms Labs" };
            var mainPage = new NavigationPage(mainTab);
            mainTab.CurrentPageChanged += () => Debug.WriteLine("ExtendedTabbedPage CurrentPageChanged {0}", mainTab.CurrentPage.Title);

            var controls = GetControlsPage(mainPage);
            var services = GetServicesPage(mainPage);
            var mvvm = ViewFactory.CreatePage(typeof(MvvmSampleViewModel));
            mainTab.Children.Add(controls);
            mainTab.Children.Add(services);
            mainTab.Children.Add(mvvm);

            return mainPage;
        }

        /// <summary>
        /// Gets the services page.
        /// </summary>
        /// <param name="mainPage">The main page.</param>
        /// <returns>Content Page.</returns>
        private static ContentPage GetServicesPage(VisualElement mainPage)
        {
            var services = new ContentPage { Title = "Services" };
            var lstServices = new ListView
            {
                ItemsSource = new List<string>()
				{
					"TextToSpeech",
					"DeviceExtended",
					"PhoneService",
					"GeoLocator",
					"Camera",
					"Accelerometer",
					"Display",
					"Cache"
				}
            };

            lstServices.ItemSelected += (sender, e) =>
            {
                switch (e.SelectedItem.ToString().ToLower())
                {
                    case "texttospeech":
                        mainPage.Navigation.PushAsync(new TextToSpeechPage());
                        break;
                    case "deviceextended":
                        mainPage.Navigation.PushAsync(new ExtendedDeviceInfoPage(Resolver.Resolve<IDevice>()));
                        break;
                    case "phoneservice":
                        mainPage.Navigation.PushAsync(new PhoneServicePage());
                        break;
                    case "geolocator":
                        mainPage.Navigation.PushAsync(ViewFactory.CreatePage(typeof(GeolocatorViewModel)));
                        break;
                    case "camera":
                        mainPage.Navigation.PushAsync(ViewFactory.CreatePage(typeof(CameraViewModel)));
                        break;
                    case "accelerometer":
                        mainPage.Navigation.PushAsync(new AcceleratorSensorPage());
                        break;
                    case "display":
                        mainPage.Navigation.PushAsync(new AbsoluteLayoutWithDisplayInfoPage(Resolver.Resolve<IDisplay>()));
                        break;
                    case "cache":
                        mainPage.Navigation.PushAsync(ViewFactory.CreatePage(typeof(CacheServiceViewModel)));
                        break;
                    default:
                        break;
                }
            };
            services.Content = lstServices;
            return services;
        }

        /// <summary>
        /// Gets the controls page.
        /// </summary>
        /// <param name="mainPage">The main page.</param>
        /// <returns>Content Page.</returns>
        private static ContentPage GetControlsPage(VisualElement mainPage)
        {
            var controls = new ContentPage { Title = "Controls" };
            var lstControls = new ListView
            {
                ItemsSource = new List<string>()
				{
					"Calendar",
					"Autocomplete",
					"Buttons",
					"Labels",
					"Cells",
					"HybridWebView"
				}
            };
            lstControls.ItemSelected += (sender, e) =>
            {
                switch (e.SelectedItem.ToString().ToLower())
                {
                    case "calendar":
                        mainPage.Navigation.PushAsync(new CalendarPage());
                        break;
                    case "autocomplete":
                        Device.OnPlatform(
                            () => mainPage.Navigation.PushAsync(new AutoCompletePage()),
                            () => mainPage.Navigation.PushAsync(new AutoCompletePage()),
                            () => mainPage.Navigation.PushAsync(new AutoCompletePage()));
                        break;
                    case "buttons":
                        mainPage.Navigation.PushAsync(new ButtonPage());
                        break;
                    case "labels":
                        mainPage.Navigation.PushAsync(new ExtendedLabelPage());
                        break;
                    case "cells":
                        mainPage.Navigation.PushAsync(new ExtendedCellPage());
                        break;
                    case "hybridwebview":
                        mainPage.Navigation.PushAsync(new CanvasWebHybrid());
                        break;
                    default:
                        break;
                }
            };
            controls.Content = lstControls;
            return controls;
        }
    }
}