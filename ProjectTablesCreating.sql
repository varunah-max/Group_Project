use Project;
go
create table Roles
(
	id int primary key,
	name varchar(15) unique not null
);
go
create table Users
(
	id int primary key,
	username varchar(200) not null unique,
	password varchar(200) not null,
	roleId int not null,
	isOnline bit not null,
	created_at datetime2 not null,
	CONSTRAINT FK_Users_Roles FOREIGN KEY (roleId) REFERENCES Roles(id)

);
go
create table Messages
(
	id int primary key,
	senderId int not null,
	recieverId int not null,
	content varchar(max) not null,
	sentAt datetime2 not null,
	isDelivered bit not null,
	CONSTRAINT FK_Messages_Sender FOREIGN KEY (senderId) REFERENCES Users(id),
    CONSTRAINT FK_Messages_Receiver FOREIGN KEY (recieverId) REFERENCES Users(id)
);
