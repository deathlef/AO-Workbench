﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AnchorableItemViewModel.cs" company="SmokeLounge">
//   Copyright © 2013 SmokeLounge.
//   This program is free software. It comes without any warranty, to
//   the extent permitted by applicable law. You can redistribute it
//   and/or modify it under the terms of the Do What The Fuck You Want
//   To Public License, Version 2, as published by Sam Hocevar. See
//   http://www.wtfpl.net/ for more details.
// </copyright>
// <summary>
//   Defines the AnchorableItemViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace SmokeLounge.AOWorkbench.Components.Workbench
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Windows.Input;

    using SmokeLounge.AOtomation.Bus;
    using SmokeLounge.AOWorkbench.Models.Workbench;

    public abstract class AnchorableItemViewModel : ItemViewModel, IAnchorableItem
    {
        #region Fields

        private ICommand autoHideCommand;

        private bool canHide = true;

        private ICommand dockCommand;

        private ICommand hideCommand;

        #endregion

        #region Constructors and Destructors

        protected AnchorableItemViewModel(IBus bus)
            : base(bus)
        {
            Contract.Requires<ArgumentNullException>(bus != null);
        }

        #endregion

        #region Public Properties

        public ICommand AutoHideCommand
        {
            get
            {
                return this.autoHideCommand;
            }

            protected set
            {
                if (Equals(value, this.autoHideCommand))
                {
                    return;
                }

                this.autoHideCommand = value;
                this.NotifyOfPropertyChange();
            }
        }

        public bool CanHide
        {
            get
            {
                return this.canHide;
            }

            set
            {
                if (value.Equals(this.canHide))
                {
                    return;
                }

                this.canHide = value;
                this.NotifyOfPropertyChange();
            }
        }

        public ICommand DockCommand
        {
            get
            {
                return this.dockCommand;
            }

            protected set
            {
                if (Equals(value, this.dockCommand))
                {
                    return;
                }

                this.dockCommand = value;
                this.NotifyOfPropertyChange();
            }
        }

        public ICommand HideCommand
        {
            get
            {
                return this.hideCommand;
            }

            protected set
            {
                if (Equals(value, this.hideCommand))
                {
                    return;
                }

                this.hideCommand = value;
                this.NotifyOfPropertyChange();
            }
        }

        #endregion
    }
}