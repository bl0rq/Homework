using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Utilis.Extensions;

namespace Homework.UI
{
    public partial class App
    {
        protected override string GetAppContainerName()
        {
            return "Catliis v3";
        }

        protected override Bootstrapper CreateMaster()
        {
            return new Bootstrapper();
        }

        protected override bool IsSingleton
        {
            get { return true; }
        }

        protected override void HandleException(Exception ex, string source)
        {
            Action act = () =>
            {
                System.Windows.MessageBox.Show("Unhandled exception in " + source + ": " + ex.ToFullString());
                //Lei.Common.LeiLog.Write(EIBZ + 01601, source, ex);
                System.Windows.Application.Current.Shutdown(-1);
            };

            if (Dispatcher.CheckAccess())
                act();
            else
            {
                Dispatcher.Invoke(act);
            }
        }
    }

    public abstract class BaseApplication : Utilis.UI.Win.BaseApplication<Bootstrapper>
    {
    }
}
