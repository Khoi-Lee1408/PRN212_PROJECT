-- ============================================================
-- ContactDb - SQL Server Database Creation Script
-- Generated from EF Core models and migrations
-- ============================================================

USE master;
GO

-- Drop database if it already exists (for fresh setup)
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'ContactDb')
BEGIN
    ALTER DATABASE ContactDb SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE ContactDb;
END
GO

-- Create database
CREATE DATABASE ContactDb;
GO

USE ContactDb;
GO

-- ============================================================
-- Table: Contacts
-- ============================================================
CREATE TABLE Contacts (
    Id          INT            IDENTITY(1,1) NOT NULL,
    FullName    NVARCHAR(100)  NOT NULL,
    PhoneNumber NVARCHAR(20)   NOT NULL,
    Email       NVARCHAR(150)  NOT NULL,
    Address     NVARCHAR(200)  NULL,
    Category    NVARCHAR(200)  NULL,
    CONSTRAINT PK_Contacts PRIMARY KEY (Id)
);
GO

-- ============================================================
-- Table: CallHistory
-- ============================================================
CREATE TABLE CallHistory (
    Id        INT           IDENTITY(1,1) NOT NULL,
    ContactId INT           NOT NULL,
    CallDate  DATE          NOT NULL,
    CallTime  TIME(0)       NOT NULL,
    Duration  INT           NOT NULL,
    CallType  NVARCHAR(20)  NOT NULL,
    CONSTRAINT PK_CallHistory PRIMARY KEY (Id),
    CONSTRAINT FK_CallHistory_Contacts FOREIGN KEY (ContactId)
        REFERENCES Contacts (Id)
        ON DELETE CASCADE
        ON UPDATE CASCADE
);
GO

-- ============================================================
-- EF Core migration history table
-- ============================================================
CREATE TABLE __EFMigrationsHistory (
    MigrationId    NVARCHAR(150) NOT NULL,
    ProductVersion NVARCHAR(32)  NOT NULL,
    CONSTRAINT PK___EFMigrationsHistory PRIMARY KEY (MigrationId)
);
GO

INSERT INTO __EFMigrationsHistory (MigrationId, ProductVersion) VALUES
    ('20260327083854_DataLayerInit',        '10.0.5'),
    ('20260328154642_AddCategoryToContact', '10.0.5'),
    ('20260330152943_AddCallHistory',       '10.0.5');
GO

-- ============================================================
-- Seed Data: Contacts
-- ============================================================
SET IDENTITY_INSERT Contacts ON;

INSERT INTO Contacts (Id, FullName, PhoneNumber, Email, Address, Category) VALUES
    (1, N'Nguyen Van An',   N'0912345678', N'nguyenvanan@example.com', N'Ha Noi',      N'Ban be'),
    (2, N'Tran Thi Binh',   N'0912345678', N'tranthibinh@example.com', N'Ho Chi Minh', N'Nguoi than'),
    (3, N'Le Van Cuong',    N'0912345678', N'levancuong@example.com',  N'Đa Nang',     N'Shipper Shopee'),
    (4, N'Pham Thi Dung',   N'0912345678', N'phamthidung@example.com', N'Hai Phong',   N'Dong nghiep'),
    (5, N'Hoang Van Đuc',   N'0912345678', N'hoangvanduc@example.com', N'Can Tho',     N'Nguoi than'),
    (6, N'Đo Thi Hanh',     N'0912345678', N'dothihanh@example.com',   N'Hue',         NULL),
    (7, N'Bui Van Giang',   N'0912345678', N'buivangiang@example.com', N'Nha Trang',   NULL);

SET IDENTITY_INSERT Contacts OFF;
GO

-- ============================================================
-- Seed Data: CallHistory
-- ============================================================
SET IDENTITY_INSERT CallHistory ON;

INSERT INTO CallHistory (Id, ContactId, CallDate, CallTime, Duration, CallType) VALUES
    (1, 1, '2026-03-25', '09:30:00', 120, N'Incoming'),
    (2, 2, '2026-03-25', '10:15:00', 300, N'Outgoing'),
    (3, 1, '2026-03-26', '14:45:00',   0, N'Missed'),
    (4, 3, '2026-03-27', '08:20:00', 180, N'Incoming'),
    (5, 2, '2026-03-27', '19:05:00', 240, N'Outgoing'),
    (6, 4, '2026-03-28', '11:00:00',  60, N'Incoming'),
    (7, 1, '2026-03-28', '21:15:00',   0, N'Missed');

SET IDENTITY_INSERT CallHistory OFF;
GO

-- ============================================================
-- Verify
-- ============================================================
SELECT 'Contacts'   AS TableName, COUNT(*) AS RowCount FROM Contacts
UNION ALL
SELECT 'CallHistory',              COUNT(*)             FROM CallHistory;
GO
