﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Interfaces
{
    public interface ITutorialService
    {
        Task<bool> IsClientInTutorial(string clientId);
        Task<bool> ChangeTutorialStatusAsync(string clientId);
    }
}
