create database MusicSchool;

go
use MusicSchool;

go
create table Users
(
	Id nvarchar(50) primary key,
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	Name nvarchar(50) not null,
	Surname nvarchar(50) not null,
	Password nvarchar(50) not null,
	Email nvarchar(100) unique not null
);

create table Rooms
(
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	Name nvarchar(50) not null,
);

create table Lectures
(
	Id int primary key identity(1,1) not null,
	isActive bit not null DEFAULT 1,
	Name nvarchar(50) not null,
	Description nvarchar(50) not null
);

create table Parents
(
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	Name nvarchar(50) not null,
	Surname nvarchar(50) not null,
	Occupation nvarchar(50),
	Phone nvarchar(10) not null,
	Email nvarchar(100) not null,
	Address nvarchar(50),
	UserId nvarchar(50) foreign key references Users(Id) not null
);

create table Students
(
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	Name nvarchar(50) not null,
	Surname nvarchar(50) not null,
	isFemale bit not null,
	FatherId int foreign key references Parents(Id),
	MotherId int foreign key references Parents(Id),
	Birthday datetime2,
	School nvarchar(50),
	Job nvarchar(50),
	Phone nvarchar(10),
	Email nvarchar(100),
	Referance nvarchar(50),
	UserId nvarchar(50) foreign key references Users(Id) not null
);

create table Teachers
(
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	Name nvarchar(50) not null,
	Surname nvarchar(50) not null,
	Birthday datetime2,
	UserId nvarchar(50) foreign key references Users(Id) not null
);

create table WeekDays (
    Id int PRIMARY KEY,
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
    Name nvarchar(10)
);

create table WeeklyProgram
(	
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	Name nvarchar(50),
	TeacherId int foreign key references Teachers(Id) not null,
	LectureId int foreign key references Lectures(Id) not null,
	StudentId int foreign key references Students(Id) not null,
	RoomId int foreign key references Rooms(Id) not null,
	Day int foreign key references WeekDays(Id) not null, -- sriptin ilk halinde nullable olarak seçiliydi. tabloyu drop etmemek için düzeltmedim
	Hour int not null,
	StartDate datetime2 not null,
	EndDate datetime2,
	Price money,
	Note nvarchar(500),
	UserId nvarchar(50) foreign key references Users(Id) not null,
	Type int not null default 0, -- normal ders, telafi dersi, tanışma dersi
);

create table CanceledLessons
(
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	Name nvarchar(50) not null,
	WeeklyProgramId int foreign key references WeeklyProgram(Id) not null,
	Status int default 0 --yapılmadı, yapıldı, yandı, ertelendi
);

create table PostponedLessons
(
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	Name nvarchar(50) not null,
	OriginalLessonId int foreign key references WeeklyProgram(Id),
	RescheduledLessonId int foreign key references WeeklyProgram(Id) not null
);

create table ReceivedPayments
(
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	Name nvarchar(50) not null,
	LessonId int foreign key references WeeklyProgram(Id) not null,
	Payment money not null,
	UserId nvarchar(50) foreign key references Users(Id) not null
);

create table TeacherPayments
(
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	Name nvarchar(50) not null,
	TeacherId int foreign key references Teachers(Id) not null,
	Payment money not null
);

create table RoomLectures
(
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	RoomId int foreign key references Rooms(Id),
	LectureId int foreign key references Lectures(Id),

	CONSTRAINT RoomLecture unique (RoomId, LectureId)
);

create table TeacherLectures
(
	Id int primary key identity(1,1),
	isActive bit not null DEFAULT 1,
	AddedDate datetime2 not null default GETDATE(),
	TeacherId int foreign key references Teachers(Id),
	LectureId int foreign key references Lectures(Id),

	CONSTRAINT TeacherLecture UNIQUE (TeacherId, LectureId)
);

create table UpdatedWeeklyProgram
(
	Id int,
	OldName nvarchar(50) not null,
	NewName nvarchar(50) not null,
	AddedDate datetime2,
	ChangeDate datetime2,
	OldRoomId int,
	NewRoomId int,
	OldDay int,
	NewDay int,
	OldHour int,
	NewHour int,
	OldEndDate datetime2,
	NewEndDate datetime2,
	OldPrice money,
	NewPrice money,
	OldUserId nvarchar(50)
);

-- functions


-- NonClustered Index
go
create index ix_program_teacher
on WeeklyProgram(TeacherId);

go
create index ix_program_student
on WeeklyProgram(StudentId);

go
create index ix_program_lecture
on WeeklyProgram(LectureId);

--tirggers
go
create trigger trg_SaveWeeklyProgramChanges
	on WeeklyProgram
	for update
as
	begin
		declare @Id int;
		declare @OldName NVARCHAR(50);
		declare @NewName nvarchar(50);
		declare @OldRoomId int;
		declare @NewRoomId int;
		declare @OldDay int;
		declare @NewDay int;
		declare @OldHour int;
		declare @NewHour int;
		declare @OldEndDate datetime2;
		declare @NewEndDate datetime2;
		declare @OldPrice money;
		declare @NewPrice money;
		declare @OldUserId nvarchar(50);

		select @Id = Id from inserted;
		select @OldName = Name from deleted;
		select @NewName = Name from inserted;
		select @OldRoomId = RoomId from deleted;
		select @NewRoomId = RoomId from inserted;
		select @OldDay = Day from deleted;
		select @NewDay = Day from inserted;
		select @OldHour = Hour from deleted;
		select @NewHour = Hour from inserted;
		select @OldEndDate = EndDate from deleted;
		select @NewEndDate = EndDate from inserted;
		select @OldPrice = Price from deleted;
		select @NewPrice = Price from inserted;
		select @OldUserId = UserId from deleted;

		insert into UpdatedWeeklyProgram (Id, OldName, NewName, ChangeDate, OldRoomId, NewRoomId, OldDay, NewDay, OldHour, NewHour, OldEndDate, NewEndDate, OldPrice, NewPrice, OldUserId)
		values (@Id, @OldName, @NewName, GETDATE(), @OldRoomId, @NewRoomId, @OldDay, @NewDay, @OldHour, @NewHour, @OldEndDate, @NewEndDate, @OldPrice, @NewPrice, @OldUserId)
	end


--pivot table example
select RoomId, [9], [10], [11], [12], [13], [14], [15], [16], [17], [18], [19], [20], [21]
from 
(select RoomId, Hour, Id from WeeklyProgram) as tbl
pivot
(
	sum(Id)
	for Hour
	in ([9], [10], [11], [12], [13], [14], [15], [16], [17], [18], [19], [20], [21])
) as pivottable



--populate tables
insert into WeekDays (Id, Name) values
(1, 'Pazartesi'),
(2, 'Salı'),
(3, 'Çarşamba'),
(4, 'Perşembe'),
(5, 'Cuma'),
(6, 'Cumartesi'),
(7, 'Pazar')