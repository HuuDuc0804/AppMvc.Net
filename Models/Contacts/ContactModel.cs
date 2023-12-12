using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models.Contacts;

public class ContactModel
{
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "nvarchar")]
    [StringLength(50)]
    [Required(ErrorMessage = "Phải nhập {0}")]
    [Display(Name = "Họ tên")]
    public string FullName { get; set; } = string.Empty;

    [StringLength(150)]
    [Required(ErrorMessage = "Phải nhập {0}")]
    [EmailAddress(ErrorMessage = "Phải là địa chỉ Email")]
    public string Email { get; set; } = string.Empty;

    [StringLength(50)]
    [Phone(ErrorMessage = "Phải là số điện thoại")]
    public string? Phone { get; set; } = string.Empty;
    public DateTime DateSent { get; set; }
    [Display(Name = "Nội dung")]
    public string? Message { get; set; } = string.Empty;
}
