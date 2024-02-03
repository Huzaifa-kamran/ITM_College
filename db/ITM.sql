create database ITM_College;

use ITM_College;


create table Admin(id int primary key identity(1,1),adminName varchar(55),adminEmail varchar(55),password varchar(55));

create table Facilities(id int primary key identity(1,1),facilityName varchar(55),facilityDesc text,facilityImg varchar(max));

create table Contact(id int primary key identity(1,1),userName varchar(55),userEmail varchar(55),message text);

create table Department(departmentID int primary key identity(1,1),departmentName varchar(255),departmentDesc text);

create table faculties(facultyID int primary key identity(1,1),
facultyName varchar(55),
facultyEmail varchar(255),
facultyPassword varchar(55),
facultyDepartment int,
facultyImg varchar(max),
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

create table Students(studentID int primary key identity(1,1),studentName varchar(55),studentEmail varchar(55),password varchar(55));

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
select * from Courses

