﻿using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private LoginViewModel _loginVM;

        public ShellViewModel(LoginViewModel loginViewModel)
        {
            _loginVM = loginViewModel;
            ActivateItem(_loginVM);
        }
    }
}
