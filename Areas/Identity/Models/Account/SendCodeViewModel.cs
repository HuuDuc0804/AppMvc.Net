// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App.Areas.Identity.Models.AccountViewModels
{
    public class SendCodeViewModel
    {
        public string SelectedProvider { get; set; } = string.Empty;

        public ICollection<SelectListItem> Providers { get; set; } = new Collection<SelectListItem>();

        public string ReturnUrl { get; set; } = string.Empty;

        public bool RememberMe { get; set; }
    }
}
