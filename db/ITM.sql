create database ITM_College;

use ITM_College;


create table Admin(id int primary key identity(1,1),adminName varchar(55),adminEmail varchar(55),password varchar(55), role int);

create table Facilities(id int primary key identity(1,1),facilityName varchar(55),facilityDesc text,facilityImg varchar(max));

create table Contact(id int primary key identity(1,1),userName varchar(55),userEmail varchar(55),message text);

create table Department(departmentID int primary key identity(1,1),departmentName varchar(255),departmentDesc text);

create table faculties(facultyID int primary key identity(1,1),
facultyName varchar(55),
facultyEmail varchar(255),
facultyPassword varchar(55),
facultyDepartment int,
facultyImg varchar(max),
gender int,
Role int,
foreign key (facultyDepartment) references Department(departmentID)
);



create table Courses(
courseID int primary key identity(1,1),
courseName varchar(55),
courseDesc varchar(55),
courseImg varchar(max),
courseDuration int,
facultyID int,
foreign key (facultyID) references Faculties(facultyID)
);

create table Students(studentID int primary key identity(1,1),studentName varchar(55),studentEmail varchar(55),password varchar(55),role int);

create table StudentCourseRegistration(
id int primary key identity(1,1),
studentID int,
studentName varchar(55),
fatherName varchar(55),
motherName varchar(55),
DOB date,
gender varchar(55),
residentalAddress varchar(255),
permanentAddress varchar(255),
addmissionFor int,
trackingID varchar(55),
status int,
foreign key (studentID) references Students(studentID),
foreign key (addmissionFor) references Courses(courseID)
)



create table PreviousExam(
examID int primary key identity(1,1),
studentDataID int,
instituteName varchar(255),
enrollmentNumber int,
center varchar(255),
stream varchar(255),
field varchar(255),
marks int,
outOf int,
classObtained int,
sports varchar(255),
foreign key (studentDataID) references StudentCourseRegistration(id),
)

create table Assignment(
id int primary key identity(1,1),
facultyId int,
courseId int,
title varchar(255),
description text,
media varchar(max),
uploadDate date,
maxDate date,
totalMarks int,
foreign key (facultyId) references faculties(facultyId),
foreign key (courseId) references Courses(courseId),
);
drop table Assignment
ALTER TABLE Assignment
ADD totalMarks INT NOT NULL,
 securedMarks INT DEFAULT 0

select * from Assignment

INSERT INTO Students (studentName, studentEmail,password)
VALUES
('Alice Smith', 'alice.smith@example.com','123123'),
('Bob Johnson', 'bob.johnson@example.com','123123'),
('Charlie Brown', 'charlie.brown@example.com','123123'),
('Diana Garcia', 'diana.garcia@example.com','123123'),
('Elizabeth Miller', 'elizabeth.miller@example.com','123123');

INSERT INTO StudentCourseRegistration (studentID, studentName, fatherName, motherName, DOB, gender,residentalAddress, permanentAddress, addmissionFor, trackingID, status)
VALUES
(1, 'Alice Smith', 'John Smith', 'Mary Smith', '1999-10-25', 'Female', '123 Main Street, Anytown', '456 Elm Street, Anytown', 1, 'ABCD1234', 1),
(1, 'Alice Smith', 'John Smith', 'Mary Smith', '1999-10-25', 'Female', '123 Main Street, Anytown', '456 Elm Street, Anytown', 2, 'ABCD1234', 1),
(1, 'Alice Smith', 'John Smith', 'Mary Smith', '1999-10-25', 'Female', '123 Main Street, Anytown', '456 Elm Street, Anytown', 3, 'ABCD1234', 1),
(2, 'Bob Johnson', 'David Johnson', 'Sarah Johnson', '2000-05-12', 'Male', '789 Oak Street, Anytown', '456 Elm Street, Anytown', 2, 'EFGH5678', 1),
(2, 'Bob Johnson', 'David Johnson', 'Sarah Johnson', '2000-05-12', 'Male', '789 Oak Street, Anytown', '456 Elm Street, Anytown', 1, 'EFGH5678', 1),
(3, 'Charlie Brown', 'Peter Brown', 'Susan Brown', '2001-03-07', 'Male', '321 Maple Street, Anytown', '456 Elm Street, Anytown', 3, 'HIJK9012', 1),
(3, 'Charlie Brown', 'Peter Brown', 'Susan Brown', '2001-03-07', 'Male', '321 Maple Street, Anytown', '456 Elm Street, Anytown', 2, 'HIJK9012', 1),
(4, 'Diana Garcia', 'Carlos Garcia', 'Elena Garcia', '2002-11-19', 'Female', '555 Pine Street, Anytown', '456 Elm Street, Anytown', 1, 'LMNOP3456', 1),
(5, 'Elizabeth Miller', 'Michael Miller', 'Jennifer Miller', '2003-08-14', 'Female', '888 Hickory Street, Anytown', '456 Elm Street, Anytown', 2, 'QRST7890', 1);


INSERT INTO Assignment (facultyId, courseId, title, description, uploadDate, maxDate,totalMarks)
VALUES
(1, 1, 'Introduction to Programming Project', 'Develop a simple Python program', '2024-02-20', '2024-03-05',100),
(1, 1, 'Web Design Challenge', 'Create a basic website using HTML and CSS', '2024-02-22', '2024-03-10',100),
(1, 1, 'Data Analysis Exercise', 'Analyze a provided dataset and write a report', '2024-02-23', '2024-03-15',100),
(1, 2, 'Marketing Plan Proposal', 'Develop a marketing plan for a fictitious product', '2024-02-24', '2024-03-20',100),
(1, 2, 'Business Communication Case Study', 'Analyze a business communication case study', '2024-02-25', '2024-03-25',100);


INSERT INTO Assignment (facultyId, courseId, title, description,media, uploadDate, maxDate,totalMarks)
VALUES
(1, 1, 'I Programming Project', 'Develop a simple Python program','admincss/assignment\abc.png', '2024-02-20', '2024-03-05',100),
(1, 1, 'Challenge', 'Create a basic website using HTML and CSS','admincss/assignment\abjhgc.txt', '2024-02-22', '2024-03-10',100),
(1, 1, 'Analysis Exercise', 'Analyze a provided dataset and write a report','admincss/assignment\abc.word', '2024-02-23', '2024-03-15',100),
(1, 2, 'Plan Proposal', 'Develop a marketing plan for a fictitious product','admincss/assignment\abc.png', '2024-02-24', '2024-03-20',100),
(1, 2, 'Communication Case Study', 'Analyze a business communication case study','admincss/assignment\abc.png', '2024-02-25', '2024-03-25',100);