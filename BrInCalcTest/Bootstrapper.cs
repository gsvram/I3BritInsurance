using System.Web.Mvc;
using BrInCalcTest.BO;
using BrInCalcTest.BO.Interface;
using Microsoft.Practices.Unity;
using Unity.Mvc3;

namespace BrInCalcTest
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();

            container.RegisterType<IProcessFile, ProcessFile>();
            container.RegisterType<ICalculate, Calculate>();

            return container;
        }
    }
}