using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using ConverterApp.Business;
using ConverterApp.Models;
using ConverterApp.ViewModels;
using ConverterApp.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Unity;
using Unity.Injection;

namespace ConverterApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var builder = new ConfigurationBuilder()
             .SetBasePath(Directory.GetCurrentDirectory())
             .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            var container = new UnityContainer();

            ConfigureContainer(container, config);

            container.Resolve<MainWindow>().Show();
        }

        private void ConfigureContainer(IUnityContainer container, IConfigurationRoot config)
        {
            var appConfig = new AppConfiguration();
            config.GetSection(nameof(AppConfiguration)).Bind(appConfig);
            container.RegisterInstance(appConfig);

            container.RegisterType<ApiClient>(new InjectionConstructor(appConfig.ApiUrl));
        }
    }
}