CREATE DATABASE CozyHavenStay;
USE CozyHavenStay;

-- User Table
CREATE TABLE [User] (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    Gender NVARCHAR(10) NOT NULL,
    ContactNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    AccountType NVARCHAR(50) DEFAULT('Guest'),
    ProfileImage NVARCHAR(255)
);

-- Hotel Table
CREATE TABLE Hotel (
    HotelID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Location NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NOT NULL,
    Amenities NVARCHAR(MAX) NOT NULL,
);

-- HotelImage Table
CREATE TABLE HotelImage (
    ImageID INT IDENTITY(1,1) PRIMARY KEY,
    HotelID INT FOREIGN KEY REFERENCES Hotel(HotelID) ON DELETE CASCADE,
    ImageURL NVARCHAR(255) -- URL to hotel image (Cloudinary URL)
);

-- Room Table
CREATE TABLE Room (
    RoomID INT IDENTITY(1,1) PRIMARY KEY,
    HotelID INT FOREIGN KEY REFERENCES Hotel(HotelID) ON DELETE CASCADE,
    RoomType NVARCHAR(50) NOT NULL,
    MaxOccupancy INT NOT NULL,
    BedType NVARCHAR(50) NOT NULL,
    BaseFare DECIMAL(10, 2) NOT NULL,
    RoomSize NVARCHAR(50) NOT NULL,
    ACStatus NVARCHAR(3) NOT NULL,
    CONSTRAINT CHK_AC_Status CHECK (ACStatus IN ('AC', 'Non-AC')),
);

-- RoomImage Table
CREATE TABLE RoomImage (
    ImageID INT IDENTITY(1,1) PRIMARY KEY,
    RoomID INT FOREIGN KEY REFERENCES Room(RoomID) ON DELETE CASCADE,
    ImageURL NVARCHAR(255) -- URL to room image (Cloudinary URL)
);

-- Booking Table
CREATE TABLE Booking (
    BookingID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT FOREIGN KEY REFERENCES [User](UserID) ON DELETE CASCADE,
    RoomID INT FOREIGN KEY REFERENCES Room(RoomID) ON DELETE CASCADE,
    NumberOfGuests INT NOT NULL,
    CheckInDate DATETIME NOT NULL,
    CheckOutDate DATETIME NOT NULL,
    TotalFare DECIMAL(10, 2) NOT NULL,
    Status NVARCHAR(20) NOT NULL,
    RefundAmount DECIMAL(10, 2)
);

-- Review Table
CREATE TABLE Review (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT FOREIGN KEY REFERENCES [User](UserID),
    HotelID INT FOREIGN KEY REFERENCES Hotel(HotelID) ON DELETE CASCADE,
    Rating INT NOT NULL,
    Comments NVARCHAR(MAX) NOT NULL
);

-- Admin Table
CREATE TABLE Admin (
    AdminID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    AccountType NVARCHAR(50) DEFAULT('Admin')
);

-- HotelOwner Table
CREATE TABLE HotelOwner (
    OwnerID INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Password NVARCHAR(255) NOT NULL,
    HotelID INT FOREIGN KEY REFERENCES Hotel(HotelID) ON DELETE CASCADE,
    AccountType NVARCHAR(50) DEFAULT('Owner')
);

-- Refund Table
CREATE TABLE Refund (
    RefundID INT IDENTITY(1,1) PRIMARY KEY,
    BookingID INT FOREIGN KEY REFERENCES Booking(BookingID) ON DELETE CASCADE,
    RefundAmount DECIMAL(10, 2) NOT NULL,
    Reason NVARCHAR(MAX),
    RefundDate DATETIME DEFAULT GETDATE()
	RefundStatus NVARCHAR(100)
);

-- Log Table
CREATE TABLE Log (
    LogID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT FOREIGN KEY REFERENCES [User](UserID),
    Action NVARCHAR(100),
    Timestamp DATETIME DEFAULT GETDATE()
);



-- Insert data into [User] table
INSERT INTO [User] (Name, Email, Password, Gender, ContactNumber, Address,AccountType)
VALUES ('John Doe', 'johndoe@example.com', 'password123', 'Male', '123-456-7890', '123 Main St, City, Country', 'Guest');

-- Insert data into Hotel table
INSERT INTO Hotel (Name, Location, Description, Amenities)
VALUES ('Cozy Inn', '1234 Oak Street, City, Country', 'A cozy hotel located in the heart of the city.', 'Free Wi-Fi, Parking, Breakfast included');

-- Insert data into Room table
INSERT INTO Room (HotelID, RoomType, MaxOccupancy, BedType, BaseFare, RoomSize, ACStatus)
VALUES (1, 'Standard', 2, 'Queen', 100.00, '300 sqft', 'AC');

-- Insert data into Booking table
INSERT INTO Booking (UserID, RoomID, NumberOfGuests, CheckInDate, CheckOutDate, TotalFare, Status)
VALUES (1, 1, 2, '2024-02-10', '2024-02-14', 400.00, 'Confirmed');

-- Insert data into Review table
INSERT INTO Review (UserID, HotelID, Rating, Comments)
VALUES (1, 1, 5, 'Great place to stay! Friendly staff and clean rooms.');

-- Insert data into Admin table
INSERT INTO Admin (Name, Email, Password)
VALUES ('Admin User', 'admin@example.com', 'adminpassword');

-- Insert data into HotelOwner table
INSERT INTO HotelOwner (Name, Email, Password, HotelID)
VALUES ('Hotel Owner', 'owner@example.com', 'ownerpassword', 1);

-- Insert data into Log table
INSERT INTO Log (UserID, Action)
VALUES (1, 'User login');

SELECT * FROM [User];