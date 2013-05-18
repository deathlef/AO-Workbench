﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WearModule.cs" company="SmokeLounge">
//   Copyright © 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the WearModule type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SmokeLounge.AoWorkbench.Modules.Wear
{
    using System;
    using System.Diagnostics.Contracts;

    using SmokeLounge.AoWorkbench.Models.Domain;
    using SmokeLounge.AoWorkbench.Models.Modules;
    using SmokeLounge.AoWorkbench.Models.Workbench;

    public class WearModule : IModule
    {
        #region Fields

        private readonly Uri iconSource;

        private readonly string name;

        private readonly IProcess process;

        private readonly WearFactory wearFactory;

        #endregion

        #region Constructors and Destructors

        public WearModule(IProcess process, WearFactory wearFactory)
        {
            Contract.Requires<ArgumentNullException>(process != null);
            Contract.Requires<ArgumentNullException>(wearFactory != null);

            this.process = process;
            this.wearFactory = wearFactory;
            this.iconSource = null;
            this.name = "Wear";
        }

        #endregion

        #region Public Properties

        public Uri IconSource
        {
            get
            {
                return this.iconSource;
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
        }

        #endregion

        #region Public Methods and Operators

        public IItem CreateItem()
        {
            return this.wearFactory.CreateItem(this.process.Id);
        }

        #endregion

        #region Methods

        [ContractInvariantMethod]
        private void ObjectInvariant()
        {
            Contract.Invariant(string.IsNullOrWhiteSpace(this.name) == false);
            Contract.Invariant(this.wearFactory != null);
            Contract.Invariant(this.process != null);
        }

        #endregion
    }
}