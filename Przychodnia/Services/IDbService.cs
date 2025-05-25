using System.Threading.Tasks;
using Przychodnia.DTOs;

namespace Przychodnia.Services
{
    public interface IDbService
    {
        Task<PrescriptionGetDto> CreatePrescriptionAsync(PrescriptionCreateDto prescriptionData);
        Task<PatientDetailsDto> GetPatientDetailsAsync(int idPatient);
    }
}