using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MS.UI.Models
{
    public class BaseDTO
    {
        public BaseDTO()
        {
            AddedDate = DateTime.Now;
        }
        public DateTime AddedDate { get; set; }
    }

    public class StudentDTO
    {
        public int Id { get; set; }
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

    public class TeacherDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim boş bırakılamaz")]
        public string Surname { get; set; }
        public DateTime? BirthDay { get; set; }
    }

    public class LectureDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Açıklama boş bırakılamaz")]
        public string Description { get; set; }
    }

    public class RoomDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "İsim boş bırakılamaz")]
        public string Name { get; set; }
    }
}