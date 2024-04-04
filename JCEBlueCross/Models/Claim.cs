using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace JCEBlueCross.Models
{
    public class Claim
    {
        [Required]
        [Key]
        public int ClaimId { get; set; }

        [Required]
        public string ClaimNumber { get; set; } = string.Empty;

        [Required]
        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        [Required]
        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DischargeDate { get; set; } = DateTime.Now;

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        [Column(TypeName = "TIME")]
        public TimeSpan EntryHour { get; set; } = DateTime.Now.TimeOfDay;

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm:ss}")]
        [Column(TypeName = "TIME")]
        public TimeSpan DischargeHour { get; set; } = DateTime.Now.TimeOfDay;

        public string InstitutionalClaimCode { get; set; } = string.Empty;

        public string ProfessionalClaimCode { get; set; } = string.Empty;

        [Required]
        public string TypeBill { get; set; } = string.Empty;

        [Required]
        public string ReferalNum { get; set; } = string.Empty;

        [Required]
        public string ServiceCode { get; set; } = string.Empty;

        [Required]
        public string AuthCode { get; set; } = string.Empty;

        [Required]
        public string MedicalRecordNumber { get; set; } = string.Empty;

        [Required]
        public string PayerClaimControlNumber { get; set; } = string.Empty;

        public string AutoAccidentState { get; set; } = string.Empty;

        [Required]
        public string FileInf { get; set; } = string.Empty;

        public string ClaimNote { get; set; } = string.Empty;

        public string BillingNote { get; set; } = string.Empty;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime OnsetSymptomDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime InitialTreatmentDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastSeenDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AcuteManifestationDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AccidentDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastMenstrualDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastXRayDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime HearingVisionPrescriptionDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DisabilityDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime LastWorkedDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AuthorizedReturnWorkDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime AssumedCareDate { get; set; } = DateTime.Now;

        [Column(TypeName = "DATETIME")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime RepricerReceivedDate { get; set; } = DateTime.Now;

        public string PrincipalDiagnosisCode { get; set; } = string.Empty;

        public string AdmitingDiagnosisCode { get; set; } = string.Empty;

        public string PatientReasonCode { get; set; } = string.Empty;

        public string ExternalCausesCode { get; set; } = string.Empty;

        public string DiagnosisRelatedCode { get; set; } = string.Empty;

        public string OtherDiagnosisCode { get; set; } = string.Empty;

        public string PrincipalProcedureCode { get; set; } = string.Empty;

        public string OtherProcedureCode { get; set; } = string.Empty;

        public string OccurrenceSpamCode { get; set; } = string.Empty;

        public string OccurrenceInformationCode { get; set; } = string.Empty;

        public string ValueInformationCode { get; set; } = string.Empty;

        public string ConditionInformationCode { get; set; } = string.Empty;

        public string TreatmentCodeCode { get; set; } = string.Empty;

        public string ClaimPricingCode { get; set; } = string.Empty;

        public double CostService { get; set; } = 0.0;

        public double CostMaterial { get; set; } = 0.0;

        public double CostMedicine { get; set; } = 0.0;

        public double ProviderCost { get; set; } = 0.0;

        [Required]
        public double TotalAmount { get; set; } = 0.0;

        [Required]
        public int MemberUserId { get; set; }

        [Required]
        public int ProviderId { get; set; }

        [Required]
        public int PayorId { get; set; }

        [Required]
        public int Status { get; set; } = 1; // 1 - Active, 0 - Inactive
    }
}
