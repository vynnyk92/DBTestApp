use BlogDB

create table Users
(
Id int identity(1,1) not null primary key,
FirstName nvarchar(20) not null,
LastName nvarchar(20) not null,
UerName nvarchar(20) not null,
[Password] nvarchar(20) not null,
[Address] nvarchar(50) null,
Phone char(12) null check (phone like'([0-9][0-9][0-9])[0-9][0-9][0-9][0-9][0-9][0-9][0-9]'),
BirthDate datetime null
)

create table Blogs
(
BlogId int primary key identity(1,1) not null,
UserId int unique not null
FOREIGN KEY REFERENCES Users(ID) on delete cascade,
BlogName nvarchar(20) not null,
DateStamp datetime DEFAULT GetDate(), 
)

create table Categories
(
CategoryID int identity not null primary key,
BlogID int not null
FOREIGN KEY REFERENCES Blogs(BlogID) on delete cascade,
CategoryName nvarchar(20) not null,
DateStamp datetime DEFAULT GetDate(),
)

create table Posts
(
PostId int identity not null primary key,
UserId int not null unique
foreign key References Users(Id),
PostName nvarchar(20) not null,
)

create table CategoriesPosts
(
CategoryID int not null
Foreign key references Categories(CategoryID),
PostID int not null
Foreign key references Posts(PostID)
)

insert into Users
(FirstName, LastName, UerName, [Password], [Address], Phone, BirthDate)
values
('Jimmy','Page', 'LZ','LZ',null, '(099)9898981', '03/10/1967'),
('Bruce', 'Dickinson', 'IM','IM', 'London UK', null, '10/03/1955'),
('Ozzy', 'Osbourne', 'BS', 'BS', null, '(095)9191911', '11/02/1955'),
('James', 'Hetfield', 'ML','ML', null, null, '05/12/1977'),
('Dave', 'Murray', 'IM2','IM2', null, null, null),
('Steve', 'Harris', 'IM3', 'IM3', null, '(091)9999999', null),
('Nicko', 'Mcbrain', 'IM4', 'IM4', null, null, null),
('David', 'Gilmour','PF','PF', null, null, null),
('Robert', 'Plant','LZ2','LZ2', null, null, null),
('Brian','Jonson','ACDC', 'ACDC',null, null, null),
('Freddie','Mercury','Q','Q',null, null, null)


insert into Blogs
(UserId, BlogName)
values
(1, 'Led Zeppelin'),
(2, 'Iron Maiden'),
(3, 'Black Sabbath'),
(4, 'Metallica'),
(5, 'IM Side2'),
(6, 'IM Side3'),
(7, 'IM Side 4'),
(8, 'Pink Floyd'),
(9, 'Led Zeppelin'),
(10, 'ACDC'),
(11,'Queen')

insert into Categories
(BlogID, CategoryName)
values
(1, 'A'),
(1, 'B'),
(1, 'C'),
(2, 'D'),
(2, 'E'),
(3, 'F'),
(4, 'G'),
(6,'H'),
(7, 'J'),
(8, 'K'),
(9, 'L'),
(10, 'M'),
(11, 'N')

select * from Users

insert into Posts
(UserId, PostName)
values
(1, 'Post1_1'),
(1, 'Post1_2'),
(1, 'Post1_3'),
(1, 'Post1_4'),
(2, 'Post2_1'),
(2, 'Post2_2'),
(2, 'Post2_3'),
(3, 'Post3_1'),
(3, 'Post3_2'),
(4, 'Post4_1'),
(5, 'Post5_1'),
(6, 'Post6_1'),
(6, 'Post6_1'),
(7, 'Post7_1'),
(8, 'Post8_1'),
(9, 'Post9_1'),
(10, 'Post10_1')

select * from Posts
select * from Categories

insert into CategoriesPosts
(PostID, CategoryID)
values
(3,1),
(3,2),
(3,3),
(4,1),
(4,2),
(5,3),
(5,4),
(6, 1),
(6,14),
(7,5),
(7,7),
(8, 1),
(8, 5),
(8, 4),
(9, 6),
(10,10),
(11,11),
(11,12),
(12,12),
(13,9),
(13,10),
(14,14),
(15,8),
(16, 1),
(16,2),
(17,6),
(18, 8),
(19,1)

create table MostActiveUsers
(
UserId int,
FirstName nvarchar(20),
LastName nvarchar(20),
MessagesCount int
)

create proc ActiveUsers
@TopRows int =null
as 
truncate table MostActiveUsers
insert into MostActiveUsers
select u.Id, u.FirstName, u.LastName, Count(p.PostName) from Users as u
inner join Posts as p
on u.Id=p.UserId
group by u.Id, u.FirstName, u.LastName order by Count(p.PostName) desc
if @TopRows is null
select top 10  * from MostActiveUsers
order by MessagesCount desc
else
select top (@TopRows)  * from MostActiveUsers
order by MessagesCount desc

exec ActiveUsers 5

Create proc ExcepCategories
@AUserId int,
@BUserId int
as
select C.CategoryName from Posts as P
join CategoriesPosts as CP
on P.PostId=CP.PostID
join Categories as C
on CP.CategoryID=C.CategoryID
where P.UserId=@AUserId
except
select C.CategoryName from Posts as P
join CategoriesPosts as CP
on P.PostId=CP.PostID
join Categories as C
on CP.CategoryID=C.CategoryID
where P.UserId=@BUserId

exec ExcepCategories 1,2