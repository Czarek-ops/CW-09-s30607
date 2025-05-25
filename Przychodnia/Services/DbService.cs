using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Przychodnia.Data;
using Przychodnia.DTOs;
using Przychodnia.Exceptions;
using Przychodnia.Models;
using Przychodnia.Services;

namespace Przychodnia.Services;

public class DbService(AppDbContext data) : IDbService
    { 
    public async Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionData)
        {
            if (prescriptionData.Medicaments.Count > 10) 
                throw new NotFoundException("Max 10 medicaments allowed");
            if (prescriptionData.DueDate < prescriptionData.Date)
                throw new NotFoundException("DueDate must be >= Date");
            Patient patient;
            if (prescriptionData.Patient.IdPatient.HasValue)
            {
                patient = await data.Patients.FindAsync(prescriptionData.Patient.IdPatient.Value) ?? throw new NotFoundException($"Patient {prescriptionData.Patient.IdPatient.Value} not found");
            }
            else
            {
                patient = new Patient {
                    FirstName = prescriptionData.Patient.FirstName,
                    LastName  = prescriptionData.Patient.LastName,
                    Birthdate = prescriptionData.Patient.Birthdate
                };
                await data.Patients.AddAsync(patient);
            }
            var prescription = new Prescription
            {
                Date                    = prescriptionData.Date,
                DueDate                 = prescriptionData.DueDate,
                Patient                 = patient,
                IdDoctor                = prescriptionData.IdDoctor,
                PrescriptionMedicaments = prescriptionData.Medicaments.Select(m =>
                {
                    var med = data.Medicaments.Find(m.IdMedicament)
                              ?? throw new NotFoundException($"Medicament {m.IdMedicament} not found");
                    return new PrescriptionMedicament {
                        Medicament = med,
                        Dose       = m.Dose,
                        Details    = m.Details
                    };
                }).ToList()
            };
            await data.Prescriptions.AddAsync(prescription);
            await data.SaveChangesAsync();

            return new PrescriptionGetDto
            {
                IdPrescription = prescription.IdPrescription,
                Date           = prescription.Date,
                DueDate        = prescription.DueDate,
                Doctor         = new DoctorInfoDto
                {
                    IdDoctor  = prescription.IdDoctor,
                    FirstName = prescription.Doctor.FirstName,
                    LastName  = prescription.Doctor.LastName
                },
                Medicaments = prescription.PrescriptionMedicaments.Select(pm => new MedicamentInfoDto
                    {
                        IdMedicament = pm.IdMedicament,
                        Name         = pm.Medicament.Name,
                        Dose         = pm.Dose,
                        Details      = pm.Details
                    }).ToList()
            };
        } 
    public async Task<PatientDetailsDto> GetPatientDetailsAsync(int idPatient)
    {
        var patientData = await data.Patients.Where(p => p.IdPatient == idPatient).Select(p => new PatientDetailsDto
            {
                IdPatient  = p.IdPatient, 
                FirstName  = p.FirstName,
                LastName   = p.LastName,
                Birthdate  = p.Birthdate,
                Prescriptions = p.Prescriptions.OrderBy(pr => pr.DueDate).Select(pr => new PrescriptionGetDto
                    {
                        IdPrescription = pr.IdPrescription,
                        Date           = pr.Date,
                        DueDate        = pr.DueDate,
                        Doctor = new DoctorInfoDto
                        {
                            IdDoctor  = pr.Doctor.IdDoctor,
                            FirstName = pr.Doctor.FirstName,
                            LastName  = pr.Doctor.LastName
                        },
                        Medicaments = pr.PrescriptionMedicaments.Select(pm => new MedicamentInfoDto
                            {
                                IdMedicament = pm.IdMedicament,
                                Name         = pm.Medicament.Name,
                                Dose         = pm.Dose,
                                Details      = pm.Details
                            }).ToList()
                    }).ToList()
            }).FirstOrDefaultAsync();
        return patientData ?? throw new NotFoundException($"Patient {idPatient} not found");
    }
}

