CREATE TABLE BlacklistedAadhaar (
    Id INT PRIMARY KEY IDENTITY(1,1),
    AadhaarNumber VARCHAR(12) NOT NULL UNIQUE,
    PersonName NVARCHAR(100) NOT NULL,
    ContractorName NVARCHAR(100) NOT NULL,
    BanReason NVARCHAR(500) NOT NULL,
    BlacklistedDate DATETIME DEFAULT GETDATE(),
    BlacklistedBy NVARCHAR(100)
);