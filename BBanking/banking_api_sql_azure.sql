
-- SQL Database Scripts
-- Create Database
CREATE DATABASE BankingDB;
GO

USE BankingDB;
GO

-- Create Users table
CREATE TABLE Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(100) NOT NULL,
    LastName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) UNIQUE NOT NULL,
    PasswordHash NVARCHAR(MAX) NOT NULL,
    PhoneNumber NVARCHAR(20),
    Address NVARCHAR(200),
    TaxIdentifier NVARCHAR(50) UNIQUE,
    CreatedDate DATETIME2 DEFAULT GETUTCDATE(),
    LastModified DATETIME2 DEFAULT GETUTCDATE(),
    IsActive BIT DEFAULT 1
);

-- Create Roles table
CREATE TABLE Roles (
    RoleId INT IDENTITY(1,1) PRIMARY KEY,
    RoleName NVARCHAR(50) UNIQUE NOT NULL,
    Description NVARCHAR(200)
);

-- Create UserRoles table
CREATE TABLE UserRoles (
    UserRoleId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NOT NULL,
    RoleId INT NOT NULL,
    AssignedDate DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    FOREIGN KEY (RoleId) REFERENCES Roles(RoleId) ON DELETE CASCADE,
    UNIQUE(UserId, RoleId)
);

-- Create Accounts table
CREATE TABLE Accounts (
    AccountId INT IDENTITY(1,1) PRIMARY KEY,
    AccountNumber NVARCHAR(20) UNIQUE NOT NULL,
    AccountType NVARCHAR(20) NOT NULL,
    Balance DECIMAL(18,2) NOT NULL DEFAULT 0,
    AvailableBalance DECIMAL(18,2) NOT NULL DEFAULT 0,
    UserId INT NOT NULL,
    Status NVARCHAR(20) DEFAULT 'Active',
    CreatedDate DATETIME2 DEFAULT GETUTCDATE(),
    LastModified DATETIME2 DEFAULT GETUTCDATE(),
    FOREIGN KEY (UserId) REFERENCES Users(UserId) ON DELETE CASCADE,
    CHECK (AccountType IN ('Savings', 'Checking')),
    CHECK (Status IN ('Active', 'Suspended', 'Closed'))
);

-- Create Transactions table
CREATE TABLE Transactions (
    TransactionId INT IDENTITY(1,1) PRIMARY KEY,
    TransactionType NVARCHAR(50) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    FromAccountId INT NOT NULL,
    ToAccountId INT NULL,
    Description NVARCHAR(200),
    TransactionDate DATETIME2 DEFAULT GETUTCDATE(),
    TransactionReference NVARCHAR(50),
    Status NVARCHAR(20) DEFAULT 'Pending',
    FOREIGN KEY (FromAccountId) REFERENCES Accounts(AccountId),
    FOREIGN KEY (ToAccountId) REFERENCES Accounts(AccountId),
    CHECK (TransactionType IN ('Deposit', 'Withdrawal', 'Transfer')),
    CHECK (Status IN ('Pending', 'Completed', 'Failed', 'Cancelled'))
);

-- Insert default roles
INSERT INTO Roles (RoleName, Description) VALUES 
('Customer', 'Regular customer'),
('Admin', 'Administrator'),
('Manager', 'Bank manager');

-- Create indexes for performance
CREATE INDEX IX_Users_Email ON Users(Email);
CREATE INDEX IX_Users_TaxIdentifier ON Users(TaxIdentifier);
CREATE INDEX IX_Accounts_UserId ON Accounts(UserId);
CREATE INDEX IX_Accounts_AccountNumber ON Accounts(AccountNumber);
CREATE INDEX IX_Transactions_FromAccountId ON Transactions(FromAccountId);
CREATE INDEX IX_Transactions_ToAccountId ON Transactions(ToAccountId);
CREATE INDEX IX_Transactions_TransactionDate ON Transactions(TransactionDate);
CREATE INDEX IX_UserRoles_UserId ON UserRoles(UserId);

-- Create stored procedures for security
CREATE PROCEDURE sp_GetUserAccounts
    @UserId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT 
        AccountId,
        AccountNumber,
        AccountType,
        Balance,
        AvailableBalance,
        Status,
        CreatedDate
    FROM Accounts 
    WHERE UserId = @UserId AND Status = 'Active';
END
GO

CREATE PROCEDURE sp_GetTransactionHistory
    @AccountId INT,
    @UserId INT,
    @PageNumber INT = 1,
    @PageSize INT = 10
AS
BEGIN
    SET NOCOUNT ON;

    -- Verify account ownership
    IF NOT EXISTS (SELECT 1 FROM Accounts WHERE AccountId = @AccountId AND UserId = @UserId)
    BEGIN
        RAISERROR('Account not found or access denied', 16, 1);
        RETURN;
    END

    SELECT 
        t.TransactionId,
        t.TransactionType,
        t.Amount,
        t.FromAccountId,
        t.ToAccountId,
        t.Description,
        t.TransactionDate,
        t.TransactionReference,
        t.Status,
        fa.AccountNumber AS FromAccountNumber,
        ta.AccountNumber AS ToAccountNumber
    FROM Transactions t
    INNER JOIN Accounts fa ON t.FromAccountId = fa.AccountId
    LEFT JOIN Accounts ta ON t.ToAccountId = ta.AccountId
    WHERE t.FromAccountId = @AccountId OR t.ToAccountId = @AccountId
    ORDER BY t.TransactionDate DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END
GO

-- Azure App Service Configuration Files

-- appsettings.json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=tcp:your-server.database.windows.net,1433;Initial Catalog=BankingDB;Persist Security Info=False;User ID=your-username;Password=your-password;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  },
  "Jwt": {
    "Key": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!@#$%^&*()",
    "Issuer": "BankingAPI",
    "Audience": "BankingAPIUsers"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning",
      "Microsoft.EntityFrameworkCore.Database.Command": "Warning"
    }
  },
  "AllowedHosts": "*",
  "AzureKeyVault": {
    "KeyVaultUrl": "https://your-keyvault.vault.azure.net/"
  }
}

-- appsettings.Production.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning",
      "Microsoft.AspNetCore": "Warning"
    }
  }
}

-- Azure Bicep Deployment Template
param location string = resourceGroup().location
param appServicePlanName string = 'bankingapi-plan'
param webAppName string = 'bankingapi-${uniqueString(resourceGroup().id)}'
param sqlServerName string = 'bankingsql-${uniqueString(resourceGroup().id)}'
param sqlDatabaseName string = 'BankingDB'
param keyVaultName string = 'bankingkv-${uniqueString(resourceGroup().id)}'
param sqlAdminLogin string = 'sqladmin'
@secure()
param sqlAdminPassword string

// App Service Plan
resource appServicePlan 'Microsoft.Web/serverfarms@2021-02-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'B1'
    tier: 'Basic'
  }
  kind: 'app'
  properties: {
    reserved: false
  }
}

// Web App
resource webApp 'Microsoft.Web/sites@2021-02-01' = {
  name: webAppName
  location: location
  identity: {
    type: 'SystemAssigned'
  }
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
    siteConfig: {
      netFrameworkVersion: 'v8.0'
      alwaysOn: true
      ftpsState: 'Disabled'
      minTlsVersion: '1.2'
      appSettings: [
        {
          name: 'ASPNETCORE_ENVIRONMENT'
          value: 'Production'
        }
        {
          name: 'KeyVaultUrl'
          value: keyVault.properties.vaultUri
        }
      ]
      connectionStrings: [
        {
          name: 'DefaultConnection'
          connectionString: 'Server=tcp:${sqlServer.properties.fullyQualifiedDomainName},1433;Initial Catalog=${sqlDatabaseName};Authentication=Active Directory Default;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;'
          type: 'SQLAzure'
        }
      ]
    }
  }
}

// SQL Server
resource sqlServer 'Microsoft.Sql/servers@2021-11-01' = {
  name: sqlServerName
  location: location
  properties: {
    administratorLogin: sqlAdminLogin
    administratorLoginPassword: sqlAdminPassword
    minimalTlsVersion: '1.2'
    publicNetworkAccess: 'Enabled'
  }
}

// SQL Database
resource sqlDatabase 'Microsoft.Sql/servers/databases@2021-11-01' = {
  parent: sqlServer
  name: sqlDatabaseName
  location: location
  sku: {
    name: 'S1'
    tier: 'Standard'
  }
  properties: {
    collation: 'SQL_Latin1_General_CP1_CI_AS'
    maxSizeBytes: 268435456000 // 250 GB
  }
}

// Key Vault
resource keyVault 'Microsoft.KeyVault/vaults@2021-11-01-preview' = {
  name: keyVaultName
  location: location
  properties: {
    sku: {
      family: 'A'
      name: 'standard'
    }
    tenantId: tenant().tenantId
    networkAcls: {
      defaultAction: 'Allow'
      bypass: 'AzureServices'
    }
    accessPolicies: [
      {
        tenantId: tenant().tenantId
        objectId: webApp.identity.principalId
        permissions: {
          secrets: [
            'get'
            'list'
          ]
        }
      }
    ]
  }
}

// Store JWT secret in Key Vault
resource jwtSecret 'Microsoft.KeyVault/vaults/secrets@2021-11-01-preview' = {
  parent: keyVault
  name: 'JwtKey'
  properties: {
    value: 'YourSuperSecretKeyThatIsAtLeast32CharactersLong!@#$%^&*()'
  }
}

// SQL Firewall rule to allow Azure services
resource sqlFirewallRule 'Microsoft.Sql/servers/firewallRules@2021-11-01' = {
  parent: sqlServer
  name: 'AllowAzureServices'
  properties: {
    startIpAddress: '0.0.0.0'
    endIpAddress: '0.0.0.0'
  }
}

// Output important values
output webAppUrl string = 'https://${webApp.properties.defaultHostName}'
output sqlServerName string = sqlServer.name
output keyVaultUrl string = keyVault.properties.vaultUri

-- PowerShell Deployment Script
# Deploy Banking API to Azure

param(
    [Parameter(Mandatory=$true)]
    [string]$ResourceGroupName,

    [Parameter(Mandatory=$true)]
    [string]$Location,

    [Parameter(Mandatory=$true)]
    [string]$SqlAdminPassword
)

# Variables
$templateFile = "main.bicep"
$deploymentName = "BankingAPI-$(Get-Date -Format 'yyyyMMddHHmmss')"

# Create resource group if it doesn't exist
Write-Host "Creating resource group..." -ForegroundColor Green
az group create --name $ResourceGroupName --location $Location

# Deploy Bicep template
Write-Host "Deploying infrastructure..." -ForegroundColor Green
az deployment group create `
    --resource-group $ResourceGroupName `
    --template-file $templateFile `
    --parameters sqlAdminPassword=$SqlAdminPassword `
    --name $deploymentName

# Get deployment outputs
$outputs = az deployment group show --resource-group $ResourceGroupName --name $deploymentName --query properties.outputs --output json | ConvertFrom-Json

Write-Host "Deployment completed!" -ForegroundColor Green
Write-Host "Web App URL: $($outputs.webAppUrl.value)" -ForegroundColor Yellow
Write-Host "SQL Server: $($outputs.sqlServerName.value)" -ForegroundColor Yellow
Write-Host "Key Vault: $($outputs.keyVaultUrl.value)" -ForegroundColor Yellow

# Deploy database schema
Write-Host "Deploying database schema..." -ForegroundColor Green
$connectionString = "Server=tcp:$($outputs.sqlServerName.value).database.windows.net,1433;Initial Catalog=BankingDB;User ID=sqladmin;Password=$SqlAdminPassword;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"

# Run SQL scripts (you can use sqlcmd or Entity Framework migrations)
# dotnet ef database update --connection $connectionString

Write-Host "Banking API deployment completed successfully!" -ForegroundColor Green
