use travelease;


-----------------USER TABLE
CREATE TABLE USERS (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Email VARCHAR(100) UNIQUE NOT NULL CHECK (Email LIKE '_%@_%._%'),
    Password VARCHAR(255) NOT NULL,
    ContactNo VARCHAR(20),
    DOB DATE,
	 Gender VARCHAR(10),
    City VARCHAR(100),
    Region VARCHAR(100),
    Country VARCHAR(100),
    FirstName VARCHAR(50),
    MiddleName VARCHAR(50),
    LastName VARCHAR(50),
	RegistrationDate DATE DEFAULT GETDATE(),
    Role VARCHAR(20) CHECK (Role IN ('Traveler', 'TourOperator', 'Admin','serviceprovider'))
);

-- TRIP CATEGORY
CREATE TABLE TripCategory (
    CategoryID INT PRIMARY KEY IDENTITY(1,1),
    CategoryName VARCHAR(50) NOT NULL UNIQUE
);

-- DESTINATION
CREATE TABLE Destination (
    DestinationID INT PRIMARY KEY IDENTITY(1,1),
    City VARCHAR(100) NOT NULL,
    Country VARCHAR(100),
    BestSeason VARCHAR(50),
);


--Insertions
INSERT INTO USERS (Email, Password, ContactNo, DOB, Gender, City, Region, Country, FirstName, MiddleName, LastName, Role)
VALUES 
('traveler1@gmail.com', 'Password123!', '+123456789001', '1990-01-01', 'Male', 'Lahore', 'Punjab', 'Pakistan', 'Ali', 'A.', 'Khan', 'Traveler'),
('traveler2@gmail.com', 'Password123!', '+123456789002', '1991-02-02', 'Female', 'Karachi', 'Sindh', 'Pakistan', 'Fatima', 'B.', 'Ahmed', 'Traveler'),
('traveler3@gmail.com', 'Password123!', '+123456789003', '1992-03-03', 'Male', 'Islamabad', 'Capital', 'Pakistan', 'Usman', 'C.', 'Riaz', 'Traveler'),
('traveler4@gmail.com', 'Password123!', '+123456789004', '1993-04-04', 'Female', 'Multan', 'Punjab', 'Pakistan', 'Sara', 'D.', 'Malik', 'Traveler'),
('traveler5@gmail.com', 'Password123!', '+123456789005', '1994-05-05', 'Male', 'Faisalabad', 'Punjab', 'Pakistan', 'Hassan', 'E.', 'Niazi', 'Traveler'),
('traveler6@gmail.com', 'Password123!', '+123456789019', '1995-07-19', 'Male', 'Peshawar', 'KPK', 'Pakistan', 'Bilal', 'S.', 'Jan', 'Traveler'),
('traveler7@gmail.com', 'Password123!', '+123456789020', '1996-08-20', 'Female', 'Quetta', 'Balochistan', 'Pakistan', 'Zara', 'T.', 'Shah', 'Traveler'),
('touroperator1@gmail.com', 'Password123!', '+123456789006', '1980-06-06', 'Female', 'London', 'England', 'UK', 'Emma', 'F.', 'Stone', 'TourOperator'),
('touroperator2@gmail.com', 'Password123!', '+123456789007', '1981-07-07', 'Male', 'Paris', 'Île-de-France', 'France', 'Luc', 'G.', 'Dubois', 'TourOperator'),
('touroperator3@gmail.com', 'Password123!', '+123456789008', '1982-08-08', 'Female', 'Berlin', 'Berlin', 'Germany', 'Anna', 'H.', 'Schmidt', 'TourOperator'),
('touroperator4@gmail.com', 'Password123!', '+123456789009', '1983-09-09', 'Male', 'Rome', 'Lazio', 'Italy', 'Marco', 'I.', 'Rossi', 'TourOperator'),
('touroperator5@gmail.com', 'Password123!', '+123456789010', '1984-10-10', 'Female', 'Madrid', 'Community of Madrid', 'Spain', 'Maria', 'J.', 'Lopez', 'TourOperator'),
('serviceprovider1@gmail.com', 'Password123!', '+123456789011', '1985-11-11', 'Male', 'New York', 'NY', 'USA', 'John', 'K.', 'Smith', 'serviceprovider'),
('serviceprovider2@gmail.com', 'Password123!', '+123456789012', '1986-12-12', 'Female', 'Los Angeles', 'CA', 'USA', 'Jessica', 'L.', 'Taylor', 'serviceprovider'),
('serviceprovider3@gmail.com', 'Password123!', '+123456789013', '1987-01-13', 'Male', 'Toronto', 'Ontario', 'Canada', 'Ryan', 'M.', 'Brown', 'serviceprovider'),
('serviceprovider4@gmail.com', 'Password123!', '+123456789014', '1988-02-14', 'Female', 'Vancouver', 'BC', 'Canada', 'Emily', 'N.', 'Wilson', 'serviceprovider'),
('serviceprovider5@gmail.com', 'Password123!', '+123456789015', '1989-03-15', 'Male', 'Sydney', 'NSW', 'Australia', 'Liam', 'O.', 'Jones', 'serviceprovider'),
('serviceprovider6@gmail.com', 'Password123!', '+123456789016', '1970-04-16', 'Female', 'Dubai', 'Dubai', 'UAE', 'Aisha', 'P.', 'Hassan', 'serviceprovider'),
('serviceprovider7@gmail.com', 'Password123!', '+123456789017', '1971-05-17', 'Male', 'Doha', 'Doha', 'Qatar', 'Ahmed', 'Q.', 'Saeed', 'serviceprovider'),
('onlyadmin@gmail.com', 'Password123!', '+123456789018', '1972-06-18', 'Female', 'Riyadh', 'Riyadh', 'Saudi Arabia', 'Noor', 'R.', 'Ali', 'Admin');


INSERT INTO Destination (City, Country, BestSeason)
VALUES 
('Paris', 'France', 'Spring'),
('Tokyo', 'Japan', 'Autumn'),
('New York', 'USA', 'Fall'),
('Rome', 'Italy', 'Spring'),
('Istanbul', 'Turkey', 'Spring'),
('Barcelona', 'Spain', 'Summer'),
('London', 'UK', 'Spring'),
('Dubai', 'UAE', 'Winter'),
('Cairo', 'Egypt', 'Winter'),
('Sydney', 'Australia', 'Summer'),
('Cape Town', 'South Africa', 'Spring'),
('Bangkok', 'Thailand', 'Winter'),
('Bali', 'Indonesia', 'Summer'),
('Moscow', 'Russia', 'Summer'),
('Toronto', 'Canada', 'Autumn'),
('Amsterdam', 'Netherlands', 'Spring'),
('Prague', 'Czech Republic', 'Spring'),
('Vienna', 'Austria', 'Summer'),
('Lisbon', 'Portugal', 'Spring'),
('Seoul', 'South Korea', 'Autumn'),
('Beijing', 'China', 'Autumn'),
('Athens', 'Greece', 'Spring'),
('Hanoi', 'Vietnam', 'Spring'),
('Lima', 'Peru', 'Winter'),
('Rio de Janeiro', 'Brazil', 'Summer'),
('Buenos Aires', 'Argentina', 'Fall'),
('San Francisco', 'USA', 'Fall'),
('Los Angeles', 'USA', 'Spring'),
('Karachi', 'Pakistan', 'Winter'),
('Lahore', 'Pakistan', 'Winter'),
('Islamabad', 'Pakistan', 'Spring'),
('Colombo', 'Sri Lanka', 'Winter'),
('Kathmandu', 'Nepal', 'Autumn'),
('Tehran', 'Iran', 'Spring'),
('Doha', 'Qatar', 'Winter'),
('Riyadh', 'Saudi Arabia', 'Winter'),
('Jakarta', 'Indonesia', 'Summer'),
('Zurich', 'Switzerland', 'Summer'),
('Oslo', 'Norway', 'Spring'),
('Reykjavik', 'Iceland', 'Winter');


INSERT INTO TripCategory (CategoryName)
VALUES 
('Adventure'),
('Cultural'),
('Historical'),
('Nature'),
('Wildlife'),
('Luxury'),
('Beach'),
('Mountain'),
('Religious'),
('Honeymoon'),
('Family'),
('Solo'),
('Food'),
('Photography'),
('Sports'),
('Cruise'),
('Festival'),
('Backpacking'),
('Road Trip'),
('Wellness');



CREATE TABLE Traveler (
    TravelerID INT PRIMARY KEY identity(1,1),
	userid int unique not null,
	FOREIGN KEY (userid) REFERENCES USERS(UserID)
    ON UPDATE CASCADE,
);

-- TOUR OPERATOR
CREATE TABLE TourOperator (
    OperatorID INT PRIMARY KEY IDENTITY(1,1),
    CompanyName VARCHAR(100),
	userid int unique not null,
    FOREIGN KEY (userid) REFERENCES Users(UserID)
    ON UPDATE CASCADE,
		profit decimal(10,2) default 0.00

);
-- ADMIN
CREATE TABLE Admins (
    AdminID INT PRIMARY KEY IDENTITY(1,1),
	userid int unique not null,
	FOREIGN KEY (userid) REFERENCES Users(UserID)  ON UPDATE CASCADE
);



-- SERVICE PROVIDER
CREATE TABLE ServiceProvider (
    ProviderID INT PRIMARY KEY IDENTITY(1,1),
	userid int unique not null,
	FOREIGN KEY (userid) REFERENCES Users(UserID)
	ON UPDATE CASCADE,
);

-- Insert Travelers (UserIDs 1 to 6 and 7)
INSERT INTO Traveler (userid) VALUES
(1),
(2),
(3),
(4),
(5),
(6),
(7);

-- Insert Tour Operators (UserIDs 8 to 12)
INSERT INTO TourOperator (CompanyName, userid) VALUES
('Global Adventures', 8),
('Wanderlust Tours', 9),
('Heritage Voyages', 10),
('Bella Italia Travel', 11),
('Madrid Express', 12);


-- Insert Admin (UserID 20)
INSERT INTO Admins (userid) VALUES (20);


-- Insert into ServiceProvider table
INSERT INTO ServiceProvider (userid)
SELECT UserID
FROM Users
WHERE Role = 'serviceprovider';


CREATE TABLE ProviderServiceTypes (
    ProviderID INT,
    ServiceType VARCHAR(30) CHECK (ServiceType IN ('Transport', 'PersonalGuide', 'Food', 'Hotel')),
    PRIMARY KEY (ProviderID, ServiceType),
    FOREIGN KEY (ProviderID) REFERENCES ServiceProvider(ProviderID) 
        ON DELETE CASCADE ON UPDATE CASCADE
);


-- TRANSPORT SERVICE
CREATE TABLE Transport (
    TransportID INT PRIMARY KEY IDENTITY(1,1),
    ProviderID INT NOT NULL,
    TransportMode VARCHAR(50) CHECK (TransportMode IN ('Bus', 'Train', 'Plane', 'Car','Van')),
    Capacity INT CHECK (Capacity > 0),
    ACAvailable BIT,
    FOREIGN KEY (ProviderID) REFERENCES ServiceProvider(ProviderID)
    ON UPDATE CASCADE

);

-- PERSONAL GUIDE SERVICE
CREATE TABLE PersonalGuide (
    GuideID INT PRIMARY KEY IDENTITY(1,1),
    ProviderID INT NOT NULL,
    Language VARCHAR(50),
    ExperienceYears INT CHECK (ExperienceYears >= 0),
    Licensed BIT,
    FOREIGN KEY (ProviderID) REFERENCES ServiceProvider(ProviderID)
     ON UPDATE CASCADE
);


-- FOOD SERVICE
CREATE TABLE Food (
    FoodID INT PRIMARY KEY IDENTITY(1,1),
    ProviderID INT NOT NULL,
    CuisineType VARCHAR(100),
    HalalAvailable BIT,
    VegAvailable BIT,
    DeliveryAvailable BIT,
    FOREIGN KEY (ProviderID) REFERENCES ServiceProvider(ProviderID)
       ON UPDATE CASCADE
);

-- HOTEL SERVICE
CREATE TABLE Hotel (
    HotelID INT PRIMARY KEY IDENTITY(1,1),
    ProviderID INT NOT NULL,
    Stars INT CHECK (Stars BETWEEN 1 AND 5),
    RoomsAvailable INT CHECK (RoomsAvailable >= 0),
    Location VARCHAR(200),
    WifiAvailable BIT,
    FOREIGN KEY (ProviderID) REFERENCES ServiceProvider(ProviderID)
        ON UPDATE CASCADE
);


-- Insert all 4 service types for each provider
INSERT INTO ProviderServiceTypes (ProviderID, ServiceType)
SELECT sp.ProviderID, st.ServiceType
FROM ServiceProvider sp
CROSS JOIN (VALUES 
    ('Transport'), 
    ('PersonalGuide'), 
    ('Food'), 
    ('Hotel')
) AS st(ServiceType);



-- Insert into Hotel
INSERT INTO Hotel ( ProviderID, Stars, RoomsAvailable, Location, WifiAvailable)
VALUES 
( 1, 3, 25, 'Location 1', 1),
( 2, 4, 28, 'Location 2', 0),
( 3, 5, 30, 'Location 3', 1),
( 4, 3, 22, 'Location 4', 0),
( 5, 4, 24, 'Location 5', 1),
( 6, 5, 26, 'Location 6', 0),
( 7, 3, 27, 'Location 7', 1);

-- Insert into Transport
INSERT INTO Transport ( ProviderID, TransportMode, Capacity, ACAvailable)
VALUES 
( 1, 'Car', 12, 1),
( 2, 'Bus', 30, 0),
( 3, 'Train', 40, 1),
( 4, 'Van', 15, 0),
( 5, 'Plane', 50, 1),
( 6, 'Car', 10, 0),
( 7, 'Bus', 20, 1);

-- Insert into Food
INSERT INTO Food ( ProviderID, CuisineType, HalalAvailable, VegAvailable, DeliveryAvailable)
VALUES 
( 1, 'Pakistani', 1, 1, 1),
( 2, 'Italian', 1, 0, 0),
( 3, 'Chinese', 1, 1, 1),
( 4, 'Pakistani', 1, 0, 0),
( 5, 'Italian', 1, 1, 1),
( 6, 'Chinese', 1, 0, 1),
( 7, 'Pakistani', 1, 1, 0);

-- Insert into PersonalGuide
INSERT INTO PersonalGuide ( ProviderID, Language, ExperienceYears, Licensed)
VALUES 
( 1, 'English', 5, 1),
( 2, 'Urdu', 4, 0),
( 3, 'Arabic', 6, 1),
( 4, 'French', 3, 0),
( 5, 'English', 7, 1),
( 6, 'Urdu', 2, 0),
( 7, 'Arabic', 8, 1);


-- TRIP
CREATE TABLE Trip ( -- operator added 
    TripID INT PRIMARY KEY  IDENTITY(1,1),
    DestinationID INT NOT NULL,
	categoryid int not null,
	FOREIGN KEY (categoryid) REFERENCES TripCategory(CategoryID)
        ON DELETE CASCADE ON UPDATE CASCADE,
	operatorID int not null ,
	FOREIGN KEY (operatorID) REFERENCES TourOperator(OperatorID)
        ON DELETE CASCADE ON UPDATE CASCADE,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    PricePerPerson DECIMAL(10,2),
    TotalSeats INT CHECK (TotalSeats > 0),
	duration int,
	totalamount decimal(10,2),
	description text,
    FOREIGN KEY (DestinationID) REFERENCES Destination(DestinationID)
         ON UPDATE CASCADE,
);


-- Very Soon Trip (starts in 1 day)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (13, 19, 2, '2025-05-04', '2025-05-12', 1326.27, 32, 8, 42440.64, 'Trip starting very soon.');

-- Very Soon Trip (starts in 2 days)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (28, 2, 3, '2025-05-05', '2025-05-11', 1254.38, 21, 6, 26342.00, 'Trip starting very soon.');

-- Past Trip
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (20, 12, 1, '2025-03-26', '2025-04-03', 807.46, 14, 8, 11304.44, 'Past trip.');

-- Past Trip
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (5, 3, 5, '2024-11-10', '2024-11-25', 998.50, 40, 15, 39940.00, 'Cultural and historical exploration trip.');

-- Upcoming Trip (mid-May)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (10, 7, 4, '2025-05-15', '2025-05-20', 1450.00, 18, 5, 26100.00, 'Luxury beach vacation in Sydney.');

-- Upcoming Trip (late-May)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (8, 6, 3, '2025-05-25', '2025-06-01', 2300.00, 25, 7, 57500.00, 'Luxurious Dubai experience with desert safari.');

-- Ongoing Trip (started 2 days ago)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (1, 1, 1, '2025-05-01', '2025-05-08', 1100.00, 20, 7, 22000.00, 'Adventure trip in Paris, currently ongoing.');

-- Ongoing Trip (started yesterday)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (7, 14, 2, '2025-05-02', '2025-05-09', 875.00, 30, 7, 26250.00, 'Photography trip to London.');

-- Past Trip (March)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (3, 15, 4, '2025-03-01', '2025-03-10', 950.00, 16, 9, 15200.00, 'Sports-themed trip in New York.');

-- Upcoming Trip (early June)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (16, 10, 5, '2025-06-03', '2025-06-10', 1349.75, 24, 7, 32394.00, 'Romantic honeymoon trip to Amsterdam.');

-- Past Trip (Feb)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (33, 11, 1, '2025-02-10', '2025-02-20', 699.99, 12, 10, 8399.88, 'Family trip to Kathmandu.');

-- Upcoming Trip (mid-June)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (2, 18, 2, '2025-06-14', '2025-06-22', 1550.00, 10, 8, 15500.00, 'Backpacking journey through Tokyo.');

-- Upcoming Trip (late June)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (19, 9, 3, '2025-06-25', '2025-07-05', 1600.00, 8, 10, 12800.00, 'Religious heritage trip in Lisbon.');

-- Very Soon Trip (starts tomorrow)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (26, 13, 4, '2025-05-04', '2025-05-10', 725.50, 22, 6, 15961.00, 'Food and culture tour in Buenos Aires.');

-- Upcoming (starts next week)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (6, 5, 5, '2025-05-09', '2025-05-16', 990.00, 15, 7, 14850.00, 'Wildlife adventure in Barcelona.');

-- Ongoing
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (15, 4, 1, '2025-04-30', '2025-05-07', 899.00, 10, 7, 8990.00, 'Nature and sightseeing trip to Toronto.');

-- Past
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (22, 8, 2, '2024-12-20', '2025-01-02', 1199.99, 12, 13, 14399.88, 'Mountain trekking trip in Hanoi.');

-- Upcoming (July)
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (12, 17, 3, '2025-07-10', '2025-07-18', 1700.00, 28, 8, 47600.00, 'Festival fun in Bangkok.');

-- Ongoing
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (35, 16, 4, '2025-04-29', '2025-05-06', 1399.00, 18, 7, 25182.00, 'Luxury cruise from Doha.');

-- Very Soon
INSERT INTO Trip (DestinationID, CategoryID, OperatorID, StartDate, EndDate, PricePerPerson, TotalSeats, Duration, TotalAmount, Description)
VALUES (9, 20, 5, '2025-05-05', '2025-05-10', 800.00, 20, 5, 16000.00, 'Wellness retreat in Cairo.');


CREATE TABLE Bookings (
    BookingID INT PRIMARY KEY IDENTITY(1,1),
    TripID INT NOT NULL,
    TravelerID INT NOT NULL,
    BookingDate DATETIME NOT NULL DEFAULT GETDATE(),
    OperatorResponseDate DATETIME,
    TourOperatorResponse VARCHAR(20) CHECK (TourOperatorResponse IN ('Pending', 'Reserved', 'Cancelled')) NOT NULL DEFAULT 'Pending',
    PaymentStatus VARCHAR(20) CHECK (PaymentStatus IN ('Unpaid', 'Paid', 'refund')) NOT NULL DEFAULT 'Unpaid',
    TicketType VARCHAR(20) CHECK (TicketType IN ('Digital', 'Physical', 'not paid')) DEFAULT 'not paid'
);
ALTER TABLE Bookings
ADD FOREIGN KEY (TripID) REFERENCES Trip(TripID) ON UPDATE CASCADE;
ALTER TABLE Bookings
ADD FOREIGN KEY (TravelerID) REFERENCES Traveler(TravelerID);


-- 1. Pending response, no OperatorResponseDate, unpaid, no ticket
INSERT INTO Bookings (TripID, TravelerID)
VALUES (1, 7);

-- 2. Reserved, response date present, paid, digital ticket
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType)
VALUES (2, 7, '2025-04-25', 'Reserved', 'Paid', 'Digital');

-- 3. Cancelled, response date present, refund, no ticket (refund = no ticket)
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType)
VALUES (3, 7, '2025-04-26', 'Cancelled', 'refund', 'Digital');

-- 4. Reserved, response date present, paid, physical ticket
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType)
VALUES (4, 7, '2025-04-27', 'Reserved', 'Paid', 'Physical');

-- 5. Reserved, response date present, unpaid, no ticket
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse)
VALUES (5, 7, '2025-04-28', 'Reserved');

-- 6. Cancelled, response date present, unpaid, no ticket
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus)
VALUES (6, 7, '2025-04-29', 'Cancelled', 'Unpaid');

-- 7. Pending response, no OperatorResponseDate, unpaid, no ticket
INSERT INTO Bookings (TripID, TravelerID)
VALUES (7, 7);

-- 8. Reserved, response date present, paid, digital ticket
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType)
VALUES (8, 7, '2025-04-30', 'Reserved', 'Paid', 'Digital');

-- 9. Cancelled, response date present, refund, no ticket
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType)
VALUES (9, 7, '2025-04-30', 'Cancelled', 'refund', 'Digital');

-- 10. Reserved, response date present, paid, physical ticket
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType)
VALUES (10, 7, '2025-05-01', 'Reserved', 'Paid', 'Physical');


-- TravelerID 1
INSERT INTO Bookings (TripID, TravelerID) VALUES (1, 1);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (2, 1, '2025-04-25', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (3, 1, '2025-04-26', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (4, 1, '2025-04-27', 'Reserved', 'Paid', 'Physical');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse) VALUES (5, 1, '2025-04-28', 'Reserved');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus) VALUES (6, 1, '2025-04-29', 'Cancelled', 'Unpaid');
INSERT INTO Bookings (TripID, TravelerID) VALUES (7, 1);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (8, 1, '2025-04-30', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (9, 1, '2025-04-30', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (10, 1, '2025-05-01', 'Reserved', 'Paid', 'Physical');

-- TravelerID 2
INSERT INTO Bookings (TripID, TravelerID) VALUES (1, 2);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (2, 2, '2025-04-25', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (3, 2, '2025-04-26', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (4, 2, '2025-04-27', 'Reserved', 'Paid', 'Physical');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse) VALUES (5, 2, '2025-04-28', 'Reserved');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus) VALUES (6, 2, '2025-04-29', 'Cancelled', 'Unpaid');
INSERT INTO Bookings (TripID, TravelerID) VALUES (7, 2);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (8, 2, '2025-04-30', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (9, 2, '2025-04-30', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (10, 2, '2025-05-01', 'Reserved', 'Paid', 'Physical');

-- TravelerID 3
INSERT INTO Bookings (TripID, TravelerID) VALUES (1, 3);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (2, 3, '2025-04-25', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (3, 3, '2025-04-26', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (4, 3, '2025-04-27', 'Reserved', 'Paid', 'Physical');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse) VALUES (5, 3, '2025-04-28', 'Reserved');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus) VALUES (6, 3, '2025-04-29', 'Cancelled', 'Unpaid');
INSERT INTO Bookings (TripID, TravelerID) VALUES (7, 3);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (8, 3, '2025-04-30', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (9, 3, '2025-04-30', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (10, 3, '2025-05-01', 'Reserved', 'Paid', 'Physical');

-- TravelerID 4
INSERT INTO Bookings (TripID, TravelerID) VALUES (1, 4);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (2, 4, '2025-04-25', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (3, 4, '2025-04-26', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (4, 4, '2025-04-27', 'Reserved', 'Paid', 'Physical');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse) VALUES (5, 4, '2025-04-28', 'Reserved');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus) VALUES (6, 4, '2025-04-29', 'Cancelled', 'Unpaid');
INSERT INTO Bookings (TripID, TravelerID) VALUES (7, 4);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (8, 4, '2025-04-30', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (9, 4, '2025-04-30', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (10, 4, '2025-05-01', 'Reserved', 'Paid', 'Physical');

-- TravelerID 5
INSERT INTO Bookings (TripID, TravelerID) VALUES (1, 5);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (2, 5, '2025-04-25', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (3, 5, '2025-04-26', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (4, 5, '2025-04-27', 'Reserved', 'Paid', 'Physical');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse) VALUES (5, 5, '2025-04-28', 'Reserved');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus) VALUES (6, 5, '2025-04-29', 'Cancelled', 'Unpaid');
INSERT INTO Bookings (TripID, TravelerID) VALUES (7, 5);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (8, 5, '2025-04-30', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (9, 5, '2025-04-30', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (10, 5, '2025-05-01', 'Reserved', 'Paid', 'Physical');

-- TravelerID 6
INSERT INTO Bookings (TripID, TravelerID) VALUES (1, 6);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (2, 6, '2025-04-25', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (3, 6, '2025-04-26', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (4, 6, '2025-04-27', 'Reserved', 'Paid', 'Physical');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse) VALUES (5, 6, '2025-04-28', 'Reserved');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus) VALUES (6, 6, '2025-04-29', 'Cancelled', 'Unpaid');
INSERT INTO Bookings (TripID, TravelerID) VALUES (7, 6);
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (8, 6, '2025-04-30', 'Reserved', 'Paid', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (9, 6, '2025-04-30', 'Cancelled', 'refund', 'Digital');
INSERT INTO Bookings (TripID, TravelerID, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType) VALUES (10, 6, '2025-05-01', 'Reserved', 'Paid', 'Physical');

update Bookings
set OperatorResponseDate = '2025-09-25'
where TripID = 2

update Bookings
set OperatorResponseDate = '2025-10-16'
where TripID = 3

update Bookings
set OperatorResponseDate = '2025-06-15'
where TripID = 4

update Bookings
set OperatorResponseDate = '2025-08-14'
where TripID = 5

update Bookings
set OperatorResponseDate = '2025-09-12'
where TripID = 6

update Bookings
set OperatorResponseDate = '2025-06-17'
where TripID = 8

update Bookings
set OperatorResponseDate = '2025-11-16'
where TripID = 9

update Bookings
set OperatorResponseDate = '2025-08-10'
where TripID = 10

-- 1. Abandoned booking (unpaid + older than 3 days)
INSERT INTO Bookings (TripID, TravelerID, BookingDate, PaymentStatus)
VALUES (1, 6, '2024-12-01', 'Unpaid');

-- 2. Paid (Recovered)
INSERT INTO Bookings (TripID, TravelerID, BookingDate, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType)
VALUES (2, 6, '2024-12-10', '2024-12-11', 'Reserved', 'Paid', 'Digital');

-- 3. Refunded booking
INSERT INTO Bookings (TripID, TravelerID, BookingDate, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType)
VALUES (3, 6, '2024-12-15', '2024-12-16', 'Cancelled', 'refund', 'Digital');

-- 4. Pending response, unpaid (older than 3 days)
INSERT INTO Bookings (TripID, TravelerID, BookingDate, PaymentStatus)
VALUES (4, 6, '2024-12-05', 'Unpaid');

-- 5. Reserved, paid, physical ticket (Recovered)
INSERT INTO Bookings (TripID, TravelerID, BookingDate, OperatorResponseDate, TourOperatorResponse, PaymentStatus, TicketType)
VALUES (5, 6, '2024-12-03', '2024-12-04', 'Reserved', 'Paid', 'Physical');

-- 6. Abandoned booking (unpaid, old)
INSERT INTO Bookings (TripID, TravelerID, BookingDate, PaymentStatus)
VALUES (6, 6, '2024-11-30', 'Unpaid');

-- 8. Cancelled, unpaid
INSERT INTO Bookings (TripID, TravelerID, BookingDate, OperatorResponseDate, TourOperatorResponse, PaymentStatus)
VALUES (8, 6, '2024-12-08', '2024-12-09', 'Cancelled', 'Unpaid');

CREATE TABLE AssignedServices (
    AssignedServiceID INT PRIMARY KEY IDENTITY(1,1),
	ServiceProviderID int not null,
	 FOREIGN KEY (ServiceProviderID) REFERENCES ServiceProvider(ProviderID),
    BookingID INT NOT NULL, 
    HotelProviderID INT NULL,
    TransportProviderID INT NULL,
    FoodProviderID INT NULL,
    GuideProviderID INT NULL,
	ServiceProviderStatus VARCHAR(30) CHECK (ServiceProviderStatus IN ('RequestedtoServiceProvider', 'NotAvailable', 'Available')) NOT NULL default 'RequestedtoServiceProvider',
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID)on delete cascade on update cascade ,
    FOREIGN KEY (HotelProviderID) REFERENCES Hotel(HotelID) ,
    FOREIGN KEY (TransportProviderID) REFERENCES Transport(TransportID) ,
    FOREIGN KEY (FoodProviderID) REFERENCES Food(FoodID) ,
    FOREIGN KEY (GuideProviderID) REFERENCES PersonalGuide(GuideID),
);


INSERT INTO AssignedServices (ServiceProviderID, BookingID, HotelProviderID, TransportProviderID, FoodProviderID, GuideProviderID, ServiceProviderStatus)
VALUES
(2, 2, 2, 2, 2, 2, 'Available'),
(3, 3, 3, 3, 3, 3, 'NotAvailable'),
(4, 4, 4, 4, 4, 4, 'Available'),
(5, 5, 5, 5, 5, 5, 'RequestedtoServiceProvider'),
(6, 6, 6, 6, 6, 6, 'Available'),

(1, 8, 1, 1, 1, 1, 'Available'),
(2, 9, 2, 2, 2, 2, 'NotAvailable'),
(3, 10, 3, 3, 3, 3, 'RequestedtoServiceProvider'),

(5, 12, 5, 5, 5, 5, 'NotAvailable'),
(6, 13, 6, 6, 6, 6, 'RequestedtoServiceProvider'),
(7, 14, 7, 7, 7, 7, 'Available'),
(1, 15, 1, 1, 1, 1, 'NotAvailable'),
(2, 16, 2, 2, 2, 2, 'RequestedtoServiceProvider'),

(4, 18, 4, 4, 4, 4, 'NotAvailable'),
(5, 19, 5, 5, 5, 5, 'RequestedtoServiceProvider'),
(6, 20, 6, 6, 6, 6, 'Available'),

(1, 22, 1, 1, 1, 1, 'NotAvailable'),
(2, 23, 2, 2, 2, 2, 'Available'),
(3, 24, 3, 3, 3, 3, 'Available'),
(4, 25, 4, 4, 4, 4, 'RequestedtoServiceProvider'),
(5, 26, 5, 5, 5, 5, 'NotAvailable'),

(7, 28, 7, 7, 7, 7, 'RequestedtoServiceProvider'),
(1, 29, 1, 1, 1, 1, 'Available'),
(2, 30, 2, 2, 2, 2, 'Available'),

(4, 32, 4, 4, 4, 4, 'RequestedtoServiceProvider'),
(5, 33, 5, 5, 5, 5, 'Available'),
(6, 34, 6, 6, 6, 6, 'RequestedtoServiceProvider'),
(7, 35, 7, 7, 7, 7, 'NotAvailable'),
(1, 36, 1, 1, 1, 1, 'Available'),

(3, 38, 3, 3, 3, 3, 'Available'),
(4, 39, 4, 4, 4, 4, 'NotAvailable'),
(5, 40, 5, 5, 5, 5, 'RequestedtoServiceProvider'),
(7, 42, 7, 7, 7, 7, 'Available'),
(1, 43, 1, 1, 1, 1, 'NotAvailable'),
(2, 44, 2, 2, 2, 2, 'Available'),
(3, 45, 3, 3, 3, 3, 'RequestedtoServiceProvider'),
(4, 46, 4, 4, 4, 4, 'Available'),
(6, 48, 6, 6, 6, 6, 'RequestedtoServiceProvider'),
(7, 49, 7, 7, 7, 7, 'NotAvailable'),
(1, 50, 1, 1, 1, 1, 'Available'),
(3, 52, 3, 3, 3, 3, 'NotAvailable'),
(4, 53, 4, 4, 4, 4, 'Available'),
(5, 54, 5, 5, 5, 5, 'NotAvailable'),
(6, 55, 6, 6, 6, 6, 'RequestedtoServiceProvider'),
(7, 56, 7, 7, 7, 7, 'Available'),
(2, 58, 2, 2, 2, 2, 'NotAvailable'),
(3, 59, 3, 3, 3, 3, 'RequestedtoServiceProvider'),
(4, 60, 4, 4, 4, 4, 'Available'),
(6, 62, 6, 6, 6, 6, 'NotAvailable'),
(7, 63, 7, 7, 7, 7, 'Available'),
(1, 64, 1, 1, 1, 1, 'RequestedtoServiceProvider'),
(2, 65, 2, 2, 2, 2, 'Available'),
(3, 66, 3, 3, 3, 3, 'Available'),
(5, 68, 5, 5, 5, 5, 'Available'),
(6, 69, 6, 6, 6, 6, 'RequestedtoServiceProvider'),
(7, 70, 7, 7, 7, 7, 'NotAvailable');




CREATE TABLE TripHistory (
    HistoryID INT PRIMARY KEY IDENTITY(1,1),
    TravelerID INT NOT NULL,
    TripID INT NOT NULL,
    BookingID INT NOT NULL,
    FOREIGN KEY (TravelerID) REFERENCES Traveler(TravelerID),
    FOREIGN KEY (TripID) REFERENCES Trip(TripID) ,
    FOREIGN KEY (BookingID) REFERENCES Bookings(BookingID),
);

CREATE TABLE Review (
    ReviewID INT PRIMARY KEY IDENTITY(1,1),
    
    TravelerID INT NOT NULL,
    
    TargetRole VARCHAR(30) CHECK (TargetRole IN ('TourOperator', 'ServiceProvider', 'Trip')) NOT NULL,
    TargetID INT NOT NULL,  -- TourOperatorID or ServiceProviderID depending on TargetRole
	ServiceType varchar(30)  CHECK (ServiceType IN ('Transport', 'Hotel','Food','PersonalGuide')) NULL,
	Transportperformance varchar(20) check (Transportperformance in ('On-Time','Delayed'))NULL,
    Rating INT CHECK (Rating BETWEEN 1 AND 5),
    ReviewText TEXT,
    ReviewDate DATE DEFAULT GETDATE(),

    AdminResponnse VARCHAR(30) CHECK (AdminResponnse IN ('Accepted', 'Rejected', 'Pending')) NOT NULL DEFAULT 'Pending',
    
    FOREIGN KEY (TravelerID) REFERENCES Traveler(TravelerID) ON DELETE CASCADE ON UPDATE CASCADE
);

SET IDENTITY_INSERT Review ON;

INSERT INTO Review (ReviewID, TravelerID, TargetRole, TargetID, ServiceType, Transportperformance, Rating, ReviewText, ReviewDate, AdminResponnse)
VALUES
(1, 1, 'Trip', 2, NULL, NULL, 1, 'Amazing', '2025-05-12', 'Pending'),
(2, 1, 'Trip', 4, NULL, NULL, 4, 'Amazing', '2025-05-12', 'Pending'),
(3, 1, 'Trip', 8, NULL, NULL, 3, 'Bravo', '2025-05-12', 'Pending'),
(4, 2, 'Trip', 2, NULL, NULL, 3, NULL, '2025-05-12', 'Pending'),
(5, 2, 'Trip', 4, NULL, NULL, 5, NULL, '2025-05-12', 'Pending'),
(6, 2, 'Trip', 8, NULL, NULL, 1, NULL, '2025-05-12', 'Pending');

SET IDENTITY_INSERT Review OFF;

SET IDENTITY_INSERT Review ON;

INSERT INTO Review (ReviewID, TravelerID, TargetRole, TargetID, ServiceType, Transportperformance, Rating, ReviewText, ReviewDate, AdminResponnse)
VALUES
(7, 3, 'Trip', 2, NULL, NULL, 1, 'Amazing', '2025-05-12', 'Pending'),
(8, 3, 'Trip', 4, NULL, NULL, 4, 'Amazing', '2025-05-12', 'Pending'),
(9, 3, 'Trip', 8, NULL, NULL, 3, 'Bravo', '2025-05-12', 'Pending'),
(10, 4, 'Trip', 2, NULL, NULL, 3, 'Bravo', '2025-05-12', 'Pending'),
(11, 4, 'Trip', 4, NULL, NULL, 5, 'Amazing', '2025-05-12', 'Pending'),
(12, 4, 'Trip', 8, NULL, NULL, 1, 'Amazing', '2025-05-12', 'Pending');

SET IDENTITY_INSERT Review OFF;

-- ReviewID 13
INSERT INTO Review (TravelerID, TargetRole, TargetID, ServiceType, Transportperformance, Rating, ReviewText, ReviewDate)
VALUES (1, 'ServiceProvider', 5, 'Transport', 'On-Time', 3, 'Comfortable', '2025-05-12');

-- ReviewID 14
INSERT INTO Review (TravelerID, TargetRole, TargetID, ServiceType, Transportperformance, Rating, ReviewText, ReviewDate)
VALUES (1, 'ServiceProvider', 7, 'Transport', 'Delayed', 3, 'Comfortable', '2025-05-12');

-- ReviewID 15
INSERT INTO Review (TravelerID, TargetRole, TargetID, ServiceType, Transportperformance, Rating, ReviewText, ReviewDate)
VALUES (1, 'ServiceProvider', 4, 'Transport', 'On-Time', 3, 'Comfortable', '2025-05-12');

-- ReviewID 16
INSERT INTO Review (TravelerID, TargetRole, TargetID, Rating, ReviewDate)
VALUES (1, 'TourOperator', 3, 3, '2025-05-12');

-- ReviewID 17
INSERT INTO Review (TravelerID, TargetRole, TargetID, Rating, ReviewDate)
VALUES (1, 'TourOperator', 5, 5, '2025-05-12');

-- ReviewID 18
INSERT INTO Review (TravelerID, TargetRole, TargetID, Rating, ReviewText, ReviewDate)
VALUES (1, 'TourOperator', 2, 5, 'Best trip', '2025-05-12');

-- ReviewID 19
INSERT INTO Review (TravelerID, TargetRole, TargetID, ServiceType, Rating, ReviewDate)
VALUES (1, 'ServiceProvider', 5, 'PersonalGuide', 4, '2025-05-12');

-- ReviewID 20
INSERT INTO Review (TravelerID, TargetRole, TargetID, ServiceType, Rating, ReviewDate)
VALUES (1, 'ServiceProvider', 7, 'PersonalGuide', 4, '2025-05-12');

-- ReviewID 21
INSERT INTO Review (TravelerID, TargetRole, TargetID, ServiceType, Rating, ReviewText, ReviewDate)
VALUES (1, 'ServiceProvider', 4, 'PersonalGuide', 4, 'very responsive', '2025-05-12');

-- ReviewID 22
INSERT INTO Review (TravelerID, TargetRole, TargetID, Rating, ReviewDate)
VALUES (6, 'TourOperator', 3, 3, '2025-05-12');

-- ReviewID 23
INSERT INTO Review (TravelerID, TargetRole, TargetID, Rating, ReviewDate)
VALUES (6, 'TourOperator', 5, 3, '2025-05-12');

-- ReviewID 24
INSERT INTO Review (TravelerID, TargetRole, TargetID, Rating, ReviewDate)
VALUES (6, 'TourOperator', 2, 3, '2025-05-12');



--//////////////////////////////////////////////////////////////////////////////////// 


CREATE VIEW vw_TravelerDemographics AS
SELECT 
    U.Gender,
    U.Country,
    DATEDIFF(YEAR, U.DOB, GETDATE()) AS Age,
    TC.CategoryName,
    DST.City AS DestinationCity,
    B.TravelerID,
	T.TripID,
    AVG(T.PricePerPerson) AS AvgSpending
FROM Bookings B
JOIN Traveler TLR ON B.TravelerID = TLR.TravelerID
JOIN USERS U ON TLR.UserID = U.UserID
JOIN Trip T ON B.TripID = T.TripID
JOIN TripCategory TC ON T.CategoryID = TC.CategoryID
JOIN Destination DST ON T.DestinationID = DST.DestinationID
WHERE B.PaymentStatus = 'Paid'
GROUP BY 
    U.Gender, U.Country, U.DOB, TC.CategoryName, DST.City, B.TravelerID, T.TripID;

CREATE VIEW vw_TripBookingRevenueReport AS
SELECT 
    t.TripID,
    c.CategoryName,
    COUNT(b.BookingID) AS TotalBookings,
    SUM(CASE WHEN b.TourOperatorResponse = 'Cancelled' THEN 1 ELSE 0 END) * 100.0 / COUNT(b.BookingID) AS CancellationRate,
    FORMAT(b.BookingDate, 'yyyy-MM') AS BookingMonth,
    SUM(CASE WHEN b.TourOperatorResponse = 'Reserved' THEN t.TotalAmount ELSE 0 END) AS Revenue,
    COUNT(DISTINCT FORMAT(b.BookingDate, 'yyyy-MM')) AS BookingMonths,
    AVG(CASE WHEN b.TourOperatorResponse = 'Reserved' THEN t.TotalAmount ELSE NULL END) AS AvgBookingValue,
    t.Duration,
    CASE 
        WHEN t.TotalSeats = 1 THEN 'Solo'
        ELSE 'Group'
    END AS Capacity
FROM Trip t
JOIN Bookings b ON t.TripID = b.TripID
JOIN TripCategory c ON t.CategoryID = c.CategoryID
WHERE 
    (b.TourOperatorResponse = 'Reserved' AND b.PaymentStatus = 'Paid')
    OR (b.TourOperatorResponse = 'Cancelled' AND b.PaymentStatus IN ('Paid', 'refund'))
GROUP BY 
    t.TripID, 
    c.CategoryName, 
    FORMAT(b.BookingDate, 'yyyy-MM'),
    t.TotalSeats,
    t.Duration;

----------------------------------------------------------//////////////////////////------------------------
-----------  AUDIT TABLE ----------------

CREATE TABLE AuditLog (
    AuditID INT PRIMARY KEY IDENTITY(1,1),
    TableName VARCHAR(100),
    Operation VARCHAR(10), -- INSERT, UPDATE, DELETE
    UserID INT NULL,
    RoleType VARCHAR(20)CHECK (RoleType IN ('Traveler', 'TourOperator', 'Admin','serviceprovider')),
    ChangeTime DATETIME DEFAULT GETDATE()
);



----------------  Trigger  --------------

----- Inserting in Booking

CREATE TRIGGER trg_Bookings_Insert
ON Bookings
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Bookings',
        'INSERT',
        t.UserID,
        u.Role
    FROM inserted i
    JOIN Traveler t ON i.TravelerID = t.TravelerID
    JOIN Users u ON t.UserID = u.UserID;
END



--------- deleting in booking
CREATE TRIGGER trg_Bookings_Delete
ON Bookings
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Bookings',
        'DELETE',
        t.UserID,
        u.Role
    FROM deleted d
    JOIN Traveler t ON d.TravelerID = t.TravelerID
    JOIN Users u ON t.UserID = u.UserID;
END


------------ inserting in review

CREATE TRIGGER trg_Review_Insert
ON Review
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Review',
        'INSERT',
        t.UserID,
        u.Role
    FROM inserted i
    JOIN Traveler t ON i.TravelerID = t.TravelerID
    JOIN Users u ON t.UserID = u.UserID;
END

-------------- INSERT for Trip
CREATE TRIGGER trg_Trip_Insert
ON Trip
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Trip',
        'INSERT',
        o.UserID,
        u.Role
    FROM inserted i
    JOIN TourOperator o ON i.OperatorID = o.OperatorID
    JOIN Users u ON o.UserID = u.UserID;
END

----- new user insert
CREATE TRIGGER trg_Users_Insert
ON Users
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Users',
        'INSERT',
        i.UserID,
        i.Role
    FROM inserted i;
END

------------- inserting tripcategory

CREATE TRIGGER trg_TripCategory_Insert
ON TripCategory
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'TripCategory',
        'INSERT',
        a.UserID,
        'Admin'
    FROM inserted i
    CROSS APPLY (
        SELECT TOP 1 AdminID, UserID FROM Admins ORDER BY AdminID DESC
    ) a
    JOIN Users u ON a.UserID = u.UserID;
END

-------- delete trip category
CREATE TRIGGER trg_TripCategory_Delete
ON TripCategory
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'TripCategory',
        'DELETE',
        a.UserID,
        'Admin'
    FROM deleted d
    CROSS APPLY (
        SELECT TOP 1 AdminID, UserID FROM Admins ORDER BY AdminID DESC
    ) a
    JOIN Users u ON a.UserID = u.UserID;
END


-------- inserting transport -----
CREATE TRIGGER trg_Transport_Insert
ON Transport
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Transport',
        'INSERT',
        u.UserID,
        u.Role
    FROM inserted i
    JOIN ServiceProvider sp ON i.ProviderID = sp.ProviderID
    JOIN Users u ON sp.UserID = u.UserID;
END


--------- deleting transport  -----
CREATE TRIGGER trg_Transport_Delete
ON Transport
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Transport',
        'DELETE',
        u.UserID,
        u.Role
    FROM deleted d
    JOIN ServiceProvider sp ON d.ProviderID = sp.ProviderID
    JOIN Users u ON sp.UserID = u.UserID;
END

-------- inserting hotel -----
CREATE TRIGGER trg_Hotel_Insert
ON Hotel
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Hotel',
        'INSERT',
        u.UserID,
        u.Role
    FROM inserted i
    JOIN ServiceProvider sp ON i.ProviderID = sp.ProviderID
    JOIN Users u ON sp.UserID = u.UserID;
END


--------- deleting hotel  -----
CREATE TRIGGER trg_Hotel_Delete
ON Hotel
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Hotel',
        'DELETE',
        u.UserID,
        u.Role
    FROM deleted d
    JOIN ServiceProvider sp ON d.ProviderID = sp.ProviderID
    JOIN Users u ON sp.UserID = u.UserID;
END

-------- inserting guide -----
CREATE TRIGGER trg_Guide_Insert
ON PersonalGuide
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'PersonalGuide',
        'INSERT',
        u.UserID,
        u.Role
    FROM inserted i
    JOIN ServiceProvider sp ON i.ProviderID = sp.ProviderID
    JOIN Users u ON sp.UserID = u.UserID;
END


--------- deleting guide  -----

CREATE TRIGGER trg_Guide_Delete
ON PersonalGuide
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'PersonalGuide',
        'DELETE',
        u.UserID,
        u.Role
    FROM deleted d
    JOIN ServiceProvider sp ON d.ProviderID = sp.ProviderID
    JOIN Users u ON sp.UserID = u.UserID;
END


-------- inserting food -----
CREATE TRIGGER trg_Food_Insert
ON Food
AFTER INSERT
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Food',
        'INSERT',
        u.UserID,
        u.Role
    FROM inserted i
    JOIN ServiceProvider sp ON i.ProviderID = sp.ProviderID
    JOIN Users u ON sp.UserID = u.UserID;
END


--------- deleting food  -----
CREATE TRIGGER trg_Food_Delete
ON Food
AFTER DELETE
AS
BEGIN
    INSERT INTO AuditLog (TableName, Operation, UserID, RoleType)
    SELECT 
        'Food',
        'DELETE',
        u.UserID,
        u.Role
    FROM deleted d
    JOIN ServiceProvider sp ON d.ProviderID = sp.ProviderID
    JOIN Users u ON sp.UserID = u.UserID;
END


------------/////////////////////////////////////////////////////////////////////----------------------------


SELECT * FROM Users;
SELECT * FROM TripCategory;
SELECT * FROM Destination;
SELECT * FROM Trip;
SELECT * FROM Traveler;
SELECT * FROM TourOperator;
SELECT * FROM Admins;
SELECT * FROM ServiceProvider;
SELECT * FROM ProviderServiceTypes;
SELECT * FROM Transport;
SELECT * FROM PersonalGuide;
SELECT * FROM Food;
SELECT * FROM Hotel;
SELECT * FROM Bookings;
SELECT * FROM AssignedServices;
SELECT * FROM TripHistory;
SELECT * FROM Review;
SELECT * FROM vw_TravelerDemographics;
SELECT * FROM vw_TripBookingRevenueReport;
SELECT * FROM AuditLog



DROP TABLE IF EXISTS Review;
DROP TABLE IF EXISTS ReviewTargetType;
DROP TABLE IF EXISTS ReviewStatus;
DROP TABLE IF EXISTS TripHistory;
DROP TABLE IF EXISTS AssignedServices;
DROP TABLE IF EXISTS Bookings;
DROP TABLE IF EXISTS Hotel;
DROP TABLE IF EXISTS Food;
DROP TABLE IF EXISTS PersonalGuide;
DROP TABLE IF EXISTS Transport;
DROP TABLE IF EXISTS ProviderServiceTypes;
DROP TABLE IF EXISTS ServiceProvider;
DROP TABLE IF EXISTS Admins;
DROP TABLE IF EXISTS TourOperator;
DROP TABLE IF EXISTS Traveler;
DROP TABLE IF EXISTS Trip;
DROP TABLE IF EXISTS Destination;
DROP TABLE IF EXISTS TripCategory;
DROP TABLE IF EXISTS Users;
DROP view IF EXISTS vw_TravelerDemographics;
DROP view IF EXISTS vw_TripBookingRevenueReport;
DROP TABLE IF EXISTS AuditLog

