﻿using System;
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
        }
        public DateTime AddedDate { get; set; }
    }

    public class LoginVM : BaseVM
    {
        [
            Required(ErrorMessage = "Email boş bırakılamaz."),
            EmailAddress(ErrorMessage = "Geçersiz email formatı."),
            MaxLength(100, ErrorMessage = "Email alanı çok fazla karakter içeriyor.")
            ]
        public string Email { get; set; }

        [
            Required(ErrorMessage = "Parola alanı boş bırakılamaz."),
            MaxLength(20, ErrorMessage = "Parola alanı çok fazla karakter içeriyor.")
            ]
        public string Password { get; set; }
    }

    public class StudentVM : BaseVM
    {
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim alanı boş bırakılamaz")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Cinsiyet alanı boş bırakılamaz")]
        public string Gender { get; set; }
        public DateTime? BirthDay { get; set; }
        public string Phone { get; set; }
        [EmailAddress(ErrorMessage = "Geçersiz email formatı.")]
        public string Email { get; set; }
        public string Reference { get; set; }
    }

    public class TeacherVM : BaseVM
    {
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim alanı boş bırakılamaz")]
        public string Surname { get; set; }
        public DateTime? BirthDay { get; set; }
    }

    public class RoomVM : BaseVM
    {
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz")]
        public string Name { get; set; }
    }

    public class LectureVM : BaseVM
    {
        [Required(ErrorMessage = "İsim alanı boş bırakılamaz")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Açıklama alanı boş bırakılamaz")]
        public string Description { get; set; }
    }
}