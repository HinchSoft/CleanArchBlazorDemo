var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("Postgresql")
    .WithPgWeb();

var booksDb = postgres.AddDatabase("BooksDb");

var booksApi = builder.AddProject<Projects.BookStore_Api>("bookstore-api")
    .WithReference(booksDb);

builder.AddProject<Projects.Bookstore_UI>("bookstore-ui")
    .WithReference(booksApi);
   

builder.Build().Run();
