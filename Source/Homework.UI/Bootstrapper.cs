using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework.UI
{
    public class Bootstrapper : Utilis.UI.Controller.IBootStrapper
    {
        public void Dispose ( )
        {

        }

        public async Task StartAsync ( )
        {
            Utilis.Messaging.Bus.Instance.Send ( new Core.Messages.LoadingProgress ( "Beginning Bootstrap..." ) );

            Utilis.Runner.Dispatcher = new Utilis.Win.DispatcherWrapper ( System.Windows.Application.Current.Dispatcher );

            IoC.Start ( );

            //await Task.Delay ( TimeSpan.FromSeconds ( 1 ) );

            Utilis.Messaging.Bus.Instance.Send ( new Core.Messages.LoadingProgress ( "Building Main Window..." ) );
            Windows.Main mainWindow = null;
            await Utilis.Runner.RunOnDispatcherThreadAsync ( ( ) =>
            {
                mainWindow = new Windows.Main ( );
                mainWindow.Show ( );
            } );

            var service = StartNavigation ( mainWindow );

            Utilis.Messaging.Bus.Instance.Send ( new Core.Messages.LoadingProgress ( "Showing initial Page..." ) );
            service.Navigate ( Utilis.ServiceLocator.Instance.GetInstance<Core.ViewModel.Waitlist> ( ) );
        }

        private Utilis.UI.Navigation.IService StartNavigation ( Windows.FrameContainerWindow mainWindow )
        {
            Utilis.UI.Navigation.Win.Service service =
                new Utilis.UI.Navigation.Win.Service (
                    mainWindow.GetFrame ( ),
                    new Utilis.UI.ViewMapper ( new Utilis.UI.ViewFinder ( ), GetType ( ).Assembly ) );

            Utilis.ServiceLocator.Instance.RegisterInstance<Utilis.UI.Navigation.IService> ( service );

            return service;
        }
    }
}
