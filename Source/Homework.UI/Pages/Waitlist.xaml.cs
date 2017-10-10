using System;
using System.Collections.Generic;
using System.Text;

namespace Homework.UI.Pages
{
    public partial class Waitlist
    {
        public Waitlist ( )
        {
            InitializeComponent ( );
        }
    }

    public abstract class WaitlistBase : Utilis.UI.Win.BasePage<Core.ViewModel.Waitlist>
    {

    }
}
