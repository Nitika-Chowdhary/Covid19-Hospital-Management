using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Covid19HospitalManagement.Pocos
{
    [Table("Hospital")]
    public class HospitalPoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }

        [Column("Hospital_Name")]
        public string HospitalName { get; set; }

        [Column("Slots_Available")]
        public int SlotsAvailable { get; set; }

        public virtual ICollection<PatientPoco> Patients { get; set; }
    }
}
