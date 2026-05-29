
-- ============================================
-- HRForce Database Seeder
-- Microsoft SQL Server
-- ============================================

-- =========================
-- Seed Departments
-- =========================
IF NOT EXISTS (SELECT 1 FROM Departments)
BEGIN

INSERT INTO Departments
(
    DepartmentCode,
    DepartmentName,
    Status,
    CreatedAt,
    UpdatedAt
)
VALUES
('HR',  'Human Resources',        'Active',   GETDATE(), NULL),
('IT',  'Information Technology', 'Active',   GETDATE(), NULL),
('FIN', 'Finance',                'Active',   GETDATE(), NULL),
('MKT', 'Marketing',              'Active',   GETDATE(), NULL),
('OPS', 'Operations',             'Inactive', GETDATE(), NULL);

END


-- =========================
-- Seed Employees
-- =========================
IF NOT EXISTS (SELECT 1 FROM Employees)
BEGIN

DECLARE @HRDepartmentId INT;
DECLARE @ITDepartmentId INT;
DECLARE @FINDepartmentId INT;
DECLARE @MKTDepartmentId INT;

SELECT @HRDepartmentId = Id
FROM Departments
WHERE DepartmentCode = 'HR';

SELECT @ITDepartmentId = Id
FROM Departments
WHERE DepartmentCode = 'IT';

SELECT @FINDepartmentId = Id
FROM Departments
WHERE DepartmentCode = 'FIN';

SELECT @MKTDepartmentId = Id
FROM Departments
WHERE DepartmentCode = 'MKT';


INSERT INTO Employees
(
    EmployeeCode,
    FullName,
    Email,
    PhoneNumber,
    DepartmentId,
    Status,
    CreatedAt,
    UpdatedAt
)
VALUES
(
    'EMP001',
    'John Doe',
    'john.doe@hrforce.com',
    '0123456789',
    @HRDepartmentId,
    'Active',
    GETDATE(),
    NULL
),
(
    'EMP002',
    'Sarah Lim',
    'sarah.lim@hrforce.com',
    '0139876543',
    @ITDepartmentId,
    'Active',
    GETDATE(),
    NULL
),
(
    'EMP003',
    'Michael Tan',
    'michael.tan@hrforce.com',
    '0142223344',
    @FINDepartmentId,
    'Inactive',
    GETDATE(),
    NULL
),
(
    'EMP004',
    'Alicia Wong',
    'alicia.wong@hrforce.com',
    '0167788990',
    @MKTDepartmentId,
    'Active',
    GETDATE(),
    NULL
),
(
    'EMP005',
    'David Kumar',
    'david.kumar@hrforce.com',
    '0178899001',
    @ITDepartmentId,
    'Active',
    GETDATE(),
    NULL
);

END


-- =========================
-- Verify Seeded Data
-- =========================

SELECT * FROM Departments;
SELECT * FROM Employees;
