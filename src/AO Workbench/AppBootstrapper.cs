// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppBootstrapper.cs" company="SmokeLounge">
//   Copyright � 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the AppBootstrapper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SmokeLounge.AoWorkbench
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.Composition;
    using System.ComponentModel.Composition.Hosting;
    using System.Diagnostics.Contracts;
    using System.Linq;

    using Caliburn.Micro;

    using SmokeLounge.AoWorkbench.ViewModels;

    public class AppBootstrapper : Bootstrapper<ShellViewModel>
    {
        #region Fields

        private CompositionContainer container;

        #endregion

        #region Methods

        protected override void BuildUp(object instance)
        {
            Contract.Assume(this.container != null);

            this.container.SatisfyImportsOnce(instance);
        }

        protected override void Configure()
        {
            var catalog =
                new AggregateCatalog(
                    AssemblySource.Instance.Select(x => new AssemblyCatalog(x)).Where(c => c != null));

            this.container = new CompositionContainer(catalog);

            var batch = new CompositionBatch();

            batch.AddExportedValue<IWindowManager>(new WindowManager());
            batch.AddExportedValue<IEventAggregator>(new EventAggregator());
            batch.AddExportedValue(this.container);
            batch.AddExportedValue(catalog);

            this.container.Compose(batch);
        }

        protected override IEnumerable<object> GetAllInstances(Type serviceType)
        {
            Contract.Assume(this.container != null);

            return this.container.GetExportedValues<object>(AttributedModelServices.GetContractName(serviceType));
        }

        protected override object GetInstance(Type serviceType, string key)
        {
            Contract.Assume(this.container != null);

            var contract = string.IsNullOrEmpty(key) ? AttributedModelServices.GetContractName(serviceType) : key;
            var export = this.container.GetExportedValues<object>(contract).FirstOrDefault();

            if (export != null)
            {
                return export;
            }

            throw new Exception(string.Format("Could not locate any instances of contract {0}.", contract));
        }

        #endregion
    }
}