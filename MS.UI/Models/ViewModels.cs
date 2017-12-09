using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MS.UI.Models
{
    public class BaseVM
    {
        public BaseVM()
        {
            AddedDate = DateTime.Now;
            isActive = true;
        }
        public DateTime AddedDate { get; set; }
        public bool isActive { get; set; }
    }

    public class LoginVM : BaseVM
    {
        [
            Required(ErrorMessage = "Email boş bırakılamaz."),
            EmailAddress(ErrorMessage = "Geçersiz email formatı."),
            MaxLength(100, ErrorMessage = "Email çok fazla karakter içeriyor.")
            ]
        public string Email { get; set; }

        [
            Required(ErrorMessage = "Parola boş bırakılamaz."),
            MaxLength(20, ErrorMessage = "Parola çok fazla karakter içeriyor.")
            ]
        public string Password { get; set; }
    }

    public class StudentVM : BaseVM
    {
        [Required(ErrorMessage = "İsim boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim boş bırakılamaz")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Cinsiyet boş bırakılamaz")]
        public string Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Geçersiz email formatı.")]
        public string Email { get; set; }
        public string School { get; set; }
        public string Job { get; set; }
        public string Reference { get; set; }
    }

    public class TeacherVM : BaseVM
    {
        [Required(ErrorMessage = "İsim boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim boş bırakılamaz")]
        public string Surname { get; set; }
        public DateTime? BirthDay { get; set; }
    }

    public class RoomVM : BaseVM
    {
        [Required(ErrorMessage = "İsim boş bırakılamaz")]
        public string Name { get; set; }
    }

    public class LectureVM : BaseVM
    {
        [Required(ErrorMessage = "İsim boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Açıklama boş bırakılamaz")]
        public string Description { get; set; }
    }

    public class ProgramVM : BaseVM
    {
        [Required(ErrorMessage = "Eğitmen boş bırakılamaz")]
        public int TeacherId { get; set; }
        [Required(ErrorMessage = "Enstrüman boş bırakılamaz")]
        public int LectureId { get; set; }
        [Required(ErrorMessage = "Öğrenci boş bırakılamaz")]
        public int StudentId { get; set; }
        [Required(ErrorMessage = "Sınıf boş bırakılamaz")]
        public int RoomId { get; set; }
        [Required(ErrorMessage = "Gün boş bırakılamaz")]
        public int Day { get; set; }
        [Required(ErrorMessage = "Saat boş bırakılamaz")]
        public int Hour { get; set; }
        [Required(ErrorMessage = "Başlangıç tarihi boş bırakılamaz")]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required(ErrorMessage = "Fiyat boş bırakılamaz")]
        public int Price { get; set; }
        public string Note { get; set; }

    }
}