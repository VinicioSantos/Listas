using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ninject.Modules;
using Ninject;
using Listas.Exercicio5.Repositories.Contracts;
using Listas.Exercicio5.Repositories;
using Listas.Exercicio5.Services.Contracts;
using Listas.Exercicio5.Services;

namespace Listas
{
    public class Bindings : NinjectModule
    {
        public override void Load()
        {
            Bind<IContaRepository>().To<ContaRepository>();
            Bind<IContaService>().To<ContaService>();
        }
    }
}
