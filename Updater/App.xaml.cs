using Microsoft.Practices.Unity;
using Stock;
using Stock.Model;
using StockXing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks.Dataflow;
using System.Windows;
using Updater.ViewModels;

namespace Updater
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            MainWindow window = new MainWindow();
            MainViewModel mainViewModel = new MainViewModel();
            window.DataContext = mainViewModel;
            window.Show();
            _connection.LoggedIn += () =>
            {
                //
                UnityContainer container = new UnityContainer();
                container.RegisterType<Feed<Tick, OneDay>, TickFeed>(new ContainerControlledLifetimeManager());
                container.RegisterType<RecentRecord<Tick>, RecentDatabaseRecord>();
                var printTick = new ActionBlock<Tick>(tick =>
                {
                    
                });
                Feed<Tick, OneDay> tickFeed = container.Resolve<Feed<Tick, OneDay>>();
                tickFeed.Target = printTick;
                tickFeed.request(new OneDay
                {
                    Stock = new ListedStock
                    {
                        Name = "POSCO",
                        Code = "005490",
                        Market = Market.KOSPI
                    },
                    Date = DateTime.Today
                });
            };
            _connection.login();
        }

        private Connection _connection = new Connection();
    }
}
