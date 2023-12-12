﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.AccountViewModels
{
    public class VerifyCodeViewModel
    {
        [Required]
        public string Provider { get; set; } = string.Empty;

        [Required]
        public string Code { get; set; } = string.Empty;

        public string? ReturnUrl { get; set; } = string.Empty;

        [Display(Name = "Nhớ cho trình duyệt này?")]
        public bool RememberBrowser { get; set; }

        [Display(Name = "Nhớ thông tin đăng nhập?")]
        public bool RememberMe { get; set; }
    }
}