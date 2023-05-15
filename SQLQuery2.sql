Create Table ShiftMember
(
id int not null primary key,
MemberID int not null,
ShiftID int not null
foreign key(MemberID) references Member(MemberID)
foreign key(ShiftID) references Shift(ShiftID)
);

Create Table Shiftevent
(
id int identity(1,1) not null primary key,
ShiftID int not null,
EventID int not null
foreign key(EventID) references Event(eventid),
foreign key(MemberID) references Member(MemberID)
);