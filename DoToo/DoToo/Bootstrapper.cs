using Autofac;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;
using DoToo.Repositories;
using DoToo.ViewModels;
using DoToo.Utils;
using DoToo.Repositories.Database;

namespace DoToo
{
    public class Bootstrapper
    {
        protected ContainerBuilder ContainerBuilder { get; private set; }
        public Bootstrapper()
        {
            Initialize();
            FinishInitialization();
        }

        protected virtual void Initialize() 
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            ContainerBuilder = new ContainerBuilder();

            foreach (var type in currentAssembly.DefinedTypes.Where(e =>
                                                    e.IsSubclassOf(typeof(Page)) ||
                                                    e.IsSubclassOf(typeof(ViewModel))))
            {
                ContainerBuilder.RegisterType(type.AsType());
            }

            ContainerBuilder.RegisterInstance(new SqLiteDoToo()).As<ISqLiteDoToo>();

            ContainerBuilder.RegisterType<TodoItemRepository>().SingleInstance();
            ContainerBuilder.RegisterType<SubTaskRepository>().SingleInstance();
            //ContainerBuilder.RegisterInstance(new SubTaskRepository()).As<ISubTaskRepository>();

            ContainerBuilder.RegisterInstance(new MessageService()).As<IMessageServices>();
        }

        private void FinishInitialization()
        {
            var container = ContainerBuilder.Build();
            Resolver.Initialize(container);
        }

    }
}
