using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Services;
using Domain.Interfaces;
using Data;

namespace AzureFunction.Autofac
{
    public class ToDoApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<ToDoService>()
                .As<IToDoService>();

            builder.RegisterType<ToDoDbContext>();
        }
    }
}
