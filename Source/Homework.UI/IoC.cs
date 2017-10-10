using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Utilis.Extensions;

namespace Homework.UI
{
    public static class IoC
    {
        public static void Start ( )
        {
            ContainerBuilder builder = new ContainerBuilder ( );

            builder.RegisterByAttribute ( typeof ( IoC ).Assembly );

            //builder.Register ( context => new Lei.Config.ConfigDeviceAssemblyGuidance ( ) )
            //    .AsImplementedInterfaces ( )
            //    .AsSelf ( )
            //    .SingleInstance ( );

            //TODO: add classes here

            Utilis.ServiceLocator.Instance = new Utilis.ServiceLocator ( builder.Build ( ) );
        }
    }
}
