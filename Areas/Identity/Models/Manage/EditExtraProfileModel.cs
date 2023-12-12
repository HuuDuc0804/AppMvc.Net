using System;
using System.ComponentModel.DataAnnotations;

namespace App.Areas.Identity.Models.ManageViewModels
{
  public class EditExtraProfileModel
  {
      [Display(Name = "Tên tài khoản")]
      public string UserName { get; set; } = string.Empty;

      [Display(Name = "Địa chỉ email")]
      public string UserEmail { get; set; }= string.Empty;
      [Display(Name = "Số điện thoại")]
      public string? PhoneNumber { get; set; }

      [Display(Name = "Địa chỉ")]
      [StringLength(400)]
      public string HomeAdress { get; set; } = string.Empty;


      [Display(Name = "Ngày sinh")]
      public DateTime? BirthDate { get; set; }
  }
}