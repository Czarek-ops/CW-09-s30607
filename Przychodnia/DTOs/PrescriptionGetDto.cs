using System;
using System.Collections.Generic;

namespace Przychodnia.DTOs
{
    public class PrescriptionGetDto
    {
        public int IdPrescription { get; set; }
        public DateTime Date      { get; set; }
        public DateTime DueDate   { get; set; }
        public DoctorInfoDto Doctor { get; set; } = null!;
        public List<MedicamentInfoDto> Medicaments { get; set; } = new();
    }

    public class DoctorInfoDto
    {
        public int IdDoctor   { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName  { get; set; } = null!;
    }

    public class MedicamentInfoDto
    {
        public int IdMedicament { get; set; }
        public string Name      { get; set; } = null!;
        public int Dose         { get; set; }
        public string Details   { get; set; } = null!;
    }
}