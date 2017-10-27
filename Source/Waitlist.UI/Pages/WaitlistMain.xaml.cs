using System;
using System.Collections.Generic;
using System.Text;

namespace Waitlist.UI.Pages
{
    public partial class WaitlistMain
    {
        public WaitlistMain ( )
        {
            InitializeComponent ( );
        }
    }

    public abstract class WaitlistMainBase : Utilis.UI.Win.BasePage<Core.ViewModel.WaitlistMain>
    {

    }
}
