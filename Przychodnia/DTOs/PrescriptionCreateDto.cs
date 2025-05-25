using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Przychodnia.DTOs
{
    public class PrescriptionCreateDto
    {
        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime DueDate { get; set; }

        [Required]
        public PatientDto Patient { get; set; } = null!;

        [Required]
        public int IdDoctor { get; set; }

        [Required, MaxLength(10)]
        public ICollection<MedicamentOnPrescriptionDto> Medicaments { get; set; } = new List<MedicamentOnPrescriptionDto>();
    }

    public class PatientDto
    {
        public int? IdPatient { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = null!;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = null!;

        [Required]
        public DateTime Birthdate { get; set; }
    }

    public class MedicamentOnPrescriptionDto
    {
        [Required]
        public int IdMedicament { get; set; }

        [Required]
        public int Dose { get; set; }

        [Required, MaxLength(100)]
        public string Details { get; set; } = null!;
    }
}