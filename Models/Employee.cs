﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZoneFranche.Models
{
    public class Employee
    {
        [Key]
        public int id { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string numTel { get; set; }
        public string cin { get; set; }
        public string email { get; set; }
        public string idEntreprise { get; set; }
        public int idLogin { get; set; }

        [ForeignKey("idLogin")]
        public Login Login { get; set; }

    }
}
