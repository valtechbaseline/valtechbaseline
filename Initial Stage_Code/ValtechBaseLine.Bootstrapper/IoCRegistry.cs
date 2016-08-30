// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System.Reflection;
using System.Web.Mvc;
using Autofac;
using Autofac.Integration.Mvc;
using ValtechBaseLine.Data.EF;
using ValtechBaseLine.Repository.Common;
using ValtechBaseLine.Repository.Components;
using ValtechBaseLine.RepositoryContract.Common;
using ValtechBaseLine.RepositoryContract.Components;
using Module = Autofac.Module;
using ValtechBaseLine.Common;
using ValtechBaseLine.Model.Components;
using ValtechBaseLine.Model.Interfaces;
using ILanguageSwitcher = ValtechBaseLine.RepositoryContract.Components.ILanguageSwitcher;


namespace ValtechBaseLine.Bootstrapper
{
    public static class IoC
    {
        /// <summary>
        /// Configure this container for all the components
        /// </summary>
        /// <returns></returns>
        public static ContainerBuilder ConfigureContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TeaserRepository>().As<ITeaserContract>().SingleInstance();
            builder.RegisterType<GalleryRepository>().As<IGalleryContract>().SingleInstance();
            builder.RegisterType<RegistrationRepository>().As<IRegistrationContract>().SingleInstance();
            builder.RegisterType<FooterRepository>().As<IFooterContract>().SingleInstance();
            builder.RegisterType<HeaderRepository>().As<IHeaderContract>().SingleInstance();
            builder.RegisterType<ContactUsRepository>().As<IContactUsContract>().SingleInstance();
            builder.RegisterType<ForgotPasswordRepository>().As<IForgotPasswordContract>().SingleInstance();
            builder.RegisterType<EmailSender>().As<IEmailSender>().SingleInstance();
            builder.RegisterType<LanguageSwitcher>().As<ILanguageSwitcher>().SingleInstance();
            builder.RegisterType<ArticleRepository>().As<IArticleContract>().SingleInstance();
            builder.RegisterType<CarouselRepository>().As<ICarouselContract>().SingleInstance();
            builder.RegisterType<AnalyticsRepository>().As<IAnalyticsContract>().SingleInstance();
            builder.RegisterType<TrackingVisitorRespositary>().As<ITrackVisitor>().SingleInstance();
            return builder;
        }


        /// <summary>
        /// Initialize
        /// </summary>
        public static void Initialize()
        {

            var builder = IoC.ConfigureContainer();
            // Register your MVC controllers.
            builder.RegisterControllers(Assembly.Load("ValtechBaseLine.Web"));

            // OPTIONAL: Register model binders that require DI.
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterModelBinderProvider();

            // OPTIONAL: Register web abstractions like HttpContextBase.
            builder.RegisterModule<AutofacWebTypesModule>();

            // OPTIONAL: Enable property injection in view pages.
            builder.RegisterSource(new ViewRegistrationSource());

            // OPTIONAL: Enable property injection into action filters.
            builder.RegisterFilterProvider();

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));

        }

    }
}