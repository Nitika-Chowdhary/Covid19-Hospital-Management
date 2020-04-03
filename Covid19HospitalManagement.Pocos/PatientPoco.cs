using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Covid19HospitalManagement.Pocos
{
    [Table("Patient")]
    public class PatientPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Patient_Name")]
        public string PatientName { get; set; }

        [ForeignKey("Hospital_ID")]
        public virtual HospitalPoco Hospital { get; set; }

    }
}
