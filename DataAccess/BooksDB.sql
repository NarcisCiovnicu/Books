CREATE TABLE Books (
    Id          INTEGER PRIMARY KEY
                        UNIQUE
                        NOT NULL,
    Title       TEXT    NOT NULL,
    Description TEXT    NOT NULL,
    CoverPhoto  BLOB    NOT NULL
);

CREATE TABLE Authors (
    Id   INTEGER PRIMARY KEY
                 UNIQUE
                 NOT NULL,
    Name TEXT    NOT NULL
                 UNIQUE
);

CREATE TABLE Books_Authors (
    BookId   INTEGER REFERENCES Books (Id) 
                     NOT NULL,
    AuthorId INTEGER REFERENCES Authors (Id) 
                     NOT NULL
);

insert into Authors(Name) values ("Stephen King");
insert into Authors(Name) values ("William Shakespeare");
insert into Authors(Name) values ("Agatha Christie");
insert into Authors(Name) values ("Barbara Cartland");
insert into Authors(Name) values ("Danielle Steel");
insert into Authors(Name) values ("Harold Robbins");
insert into Authors(Name) values ("Georges Simenon");
insert into Authors(Name) values ("J. K. Rowling");
insert into Authors(Name) values ("Enid Blyton");
insert into Authors(Name) values ("Sidney Sheldon");
insert into Authors(Name) values ("Eiichiro Oda");
insert into Authors(Name) values ("Tom Clancy");
insert into Authors(Name) values ("Akira Toriyama");
insert into Authors(Name) values ("Leo Tolstoy");
insert into Authors(Name) values ("Dean Koontz");
insert into Authors(Name) values ("Jackie Collins");
insert into Authors(Name) values ("Nora Roberts");
insert into Authors(Name) values ("R. L. Stine");



select * from Books as B
 Join Books_Authors as BA ON B.id = BA.BookID
 JOIN Authors as A ON A.Id = BA.AuthorId;


SELECT Id, Name FROM Books_Authors BA
JOIN Authors A ON A.Id = BA.AuthorId
WHERE BA.BookId = 1;

insert into Books_Authors(BookId, AuthorId) values(1, 2);

select * from Books;
select * from Books_Authors;
select * from Authors;