create Table Users (
    Id int(11) PRIMARY KEY not null AUTO_INCREMENT ,
    FirstName varchar(45) NOT NULL,
    lastName varchar(45) NOT NULL,
    Email varchar(100) NOT NULL UNIQUE,
    BirthDate DATETIME NOT NULL
);

create Table Products  (
    Id int(11) PRIMARY KEY not null AUTO_INCREMENT ,
    Name varchar(45) NOT NULL,
    Description varchar(145) NOT NULL,
    Price DOUBLE NOT NULL,
    Stock int NOT NULL,
    ExpirationDate DATETIME NOT NULL,
    UserId int NOT NULL, 
    FOREIGN KEY (UserID) REFERENCES Users(Id)
);


INSERT INTO Users (FirstName, lastName, email, birthdate) VALUES 
('John', 'Doe', 'john.doe@example.com', '1990-05-15'),
('Jane', 'Smith', 'jane.smith@example.com', '1988-08-20'),
('Michael', 'Johnson', 'michael.johnson@example.com', '1975-03-10'),
('Emily', 'Williams', 'emily.williams@example.com', '1995-11-25'),
('Christopher', 'Brown', 'christopher.brown@example.com', '1982-07-03'),
('Sarah', 'Taylor', 'sarah.taylor@example.com', '1998-02-18'),
('Daniel', 'Martinez', 'daniel.martinez@example.com', '1989-09-30'),
('Jessica', 'Anderson', 'jessica.anderson@example.com', '1980-06-12'),
('Matthew', 'Garcia', 'matthew.garcia@example.com', '1972-12-05'),
('Lauren', 'Rodriguez', 'lauren.rodriguez@example.com', '1993-04-28'),
('David', 'Lopez', 'david.lopez@example.com', '1987-01-22'),
('Ashley', 'Lee', 'ashley.lee@example.com', '1991-10-07'),
('James', 'Perez', 'james.perez@example.com', '1984-09-15'),
('Jennifer', 'Gonzalez', 'jennifer.gonzalez@example.com', '1978-08-09'),
('Robert', 'Hernandez', 'robert.hernandez@example.com', '1990-06-30');
INSERT INTO Products (Name, Description, Price, Stock, ExpirationDate, UserId)
VALUES 
('Cheese', 'Good, pretty, and cheap', 10.99, 100, '2024-06-01 12:00:00', 1),
('Ducks', 'The best of life', 99.99, 1250, '2024-07-15 10:30:00', 2),
('bread', 'Food for ducks', 1.75, 2250, '2024-08-20 08:45:00', 3),
('jam', 'to put on breads', 30.00, 200, '2024-09-10 14:20:00', 1),
('spoon', 'to spread the jam', 25.99, 80, '2024-10-05 16:10:00', 2);



DROP Table Users;
TRUNCATE Users;
 
DROP Table Products;
TRUNCATE Products;