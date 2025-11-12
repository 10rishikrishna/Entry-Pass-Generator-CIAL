using System;
using System.Text.Json.Serialization;

namespace Entry_Pass_Generator_CIAL
{
    public class PassModel
    {
        [JsonPropertyName("laborID")]
        public string LaborID { get; set; }

        [JsonPropertyName("fullName")]
        public string FullName { get; set; }

        [JsonPropertyName("dob")]
        public string DOB { get; set; }

        [JsonPropertyName("contractorName")]
        public string ContractorName { get; set; }

        [JsonPropertyName("area")]
        public string Area { get; set; }

        [JsonPropertyName("gateAccess")]
        public string GateAccess { get; set; }

        [JsonPropertyName("entryDate")]
        public string EntryDate { get; set; }

        [JsonPropertyName("exitDate")]
        public string ExitDate { get; set; }

        [JsonPropertyName("entryTime")]
        public string EntryTime { get; set; }

        [JsonPropertyName("checkoutTime")]
        public string CheckoutTime { get; set; }

        [JsonPropertyName("labourImageBase64")]
        public string LabourImageBase64 { get; set; }

        [JsonPropertyName("status")]
        public string Status { get; set; } = "Pending";

        [JsonPropertyName("submittedAt")]
        public DateTime SubmittedAt { get; set; } = DateTime.Now;

        [JsonPropertyName("approvedBy")]
        public string ApprovedBy { get; set; } = "";

        [JsonPropertyName("approvedAt")]
        public DateTime? ApprovedAt { get; set; }

        [JsonPropertyName("rejectionReason")]
        public string RejectionReason { get; set; } = "";

        [JsonPropertyName("isDownloaded")]
        public bool IsDownloaded { get; set; } = false;

        [JsonPropertyName("downloadedAt")]
        public DateTime? DownloadedAt { get; set; }

        [JsonPropertyName("digitalSignature")]
        public DigitalSignatureData DigitalSignature { get; set; }
    }

    public class DigitalSignatureData
    {
        [JsonPropertyName("signatureId")]
        public string SignatureId { get; set; }

        [JsonPropertyName("signerName")]
        public string SignerName { get; set; }

        [JsonPropertyName("signerTitle")]
        public string SignerTitle { get; set; }

        [JsonPropertyName("signerOrganization")]
        public string SignerOrganization { get; set; }

        [JsonPropertyName("signedDate")]
        public DateTime SignedDate { get; set; }

        [JsonPropertyName("documentHash")]
        public string DocumentHash { get; set; }

        [JsonPropertyName("signatureValue")]
        public string SignatureValue { get; set; }

        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set; }
    }
}