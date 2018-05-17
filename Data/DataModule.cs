using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using Autofac;
using Data.Repository;
using Domain.Model;

namespace Data
{
    public class DataModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            if (builder == null)
                throw new ArgumentNullException(nameof(builder));
            builder.Register(c => new GetPayerMasterRepository(ConfigurationManager.ConnectionStrings["AvutoxDB"].ToString())).AsImplementedInterfaces();
            builder.Register(c => new GetLabOrderRepository(ConfigurationManager.ConnectionStrings["AvutoxCRMDB"].ToString())).AsImplementedInterfaces();
            builder.Register(c => new CrmRepository(ConfigurationManager.ConnectionStrings["AvutoxCRMDB"].ToString())).AsImplementedInterfaces();
            builder.Register<RemittanceRepository>(
                c => new RemittanceRepository(ConfigurationManager.ConnectionStrings["AvutoxCRMDB"].ToString()));


        }
    }
 
}
