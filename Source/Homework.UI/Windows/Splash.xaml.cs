using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.UI.Windows
{
    public partial class Splash : Utilis.Messaging.IListener<Core.Messages.LoadingProgress>
    {
        private IDisposable m_busToken;

        public Splash ( )
        {
            InitializeComponent ( );

            Closing += Splash_Closing;

            m_busToken = Utilis.Messaging.Bus.Instance.ListenFor ( this );
        }

        void Splash_Closing ( object sender, System.ComponentModel.CancelEventArgs e )
        {
            if ( m_busToken != null )
            {
                m_busToken.Dispose ( );
                m_busToken = null;
            }
        }

        public void Receive ( Core.Messages.LoadingProgress message )
        {
            this.Dispatcher.Invoke ( ( ) => tbLoading.Text = message.Message );
        }
    }
}
