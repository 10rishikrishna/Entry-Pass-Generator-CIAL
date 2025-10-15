CREATE TABLE LaborRenewal (
    LaborID INT PRIMARY KEY,                
    ContractorName NVARCHAR(100),          
    GateAccess NVARCHAR(100),              
    EntryDate DATE,                         
    ExitDate DATE,                          
    FullName NVARCHAR(200),                 
    DOB DATE,                               
    Area NVARCHAR(100),                     
    EntryTime TIME,                         
    CheckoutTime TIME                       
);
