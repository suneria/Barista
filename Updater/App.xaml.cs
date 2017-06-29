using Microsoft.Practices.Unity;
using Stock;
using StockXing;
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
            };
            _connection.login();
            //
            UnityContainer container = new UnityContainer();
            container.RegisterType<Feed<Tick>, TickFeed>(new ContainerControlledLifetimeManager());
            Feed<Tick> tickFeed = container.Resolve<Feed<Tick>>();
            tickFeed.request(new Request(), null);
        }

        private Connection _connection = new Connection();
    }
}
