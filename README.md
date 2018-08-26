# **About:** 
A simple phone book application.
- Use Cases:
   -   Add entry to my phone book
   -   View phone book where I can search for entries (text box to search and list view)

# **Technologies:**
- Angular 5
- ASP.NET Core 2.1
- EF core 2.1
- Bootstrap 3.0
- Automapper
- SQLite

# **Setup:**
1. Clone the repo and open the command line
2. cd ContactManager\
3. Run `dotnet build`
4. cd ContactManager.Web
5. Run `dotnet ef database update --project ..\ContactManager.Core\ContactManager.Core.csproj`
   
   After running migrations, the database is placed in ContactManager.Web\bin. You can use db browser from https://sqlitebrowser.org/ to view the tables.
6. Run `dotnet run`
7. Browse to https://localhost:5001

# **TO DO:**
There is still a lot that can be improved on this app, could not do much due to time constraints.

I have put //TODO comments inside the projects for some of the things that still need to be done.

Other areas of improvement include:
1. Tests have not been added on the client and server side.
2. There is a lot of duplicated code in angular components that can benefit from services and more granular components.
3. N+1 queries in some of the EF requests as I have enabled lazy loading.
4. Paging of data retrieved from the server so that we do not load it all at once.This will also allow us to implement serve side searching functionality.



