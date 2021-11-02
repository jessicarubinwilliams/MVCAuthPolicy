# MVC Builder

### About.

This application creates a fully built, CRUD functional, MVC AspNet Core Web Server w/ EF Migration, User Authentication, and Authorization. 

### Setup.

- Create a new MySQL Schema.
- Open your terminal to your desired location, and run this command:

```bash
git clone https://github.com/littlestbrother/MVCAuthBuilder && cd MVCAuthBuilder && rm -rf .git && chmod +x build.sh && ./build.sh
```

- Follow the builder instructions, remember to capitalize the first letter of your `Project Name`,&nbsp; `Author`,&nbsp;and `Book`. Make sure the `SQL database name` matches the schema name previously created.

```
🏠 Project Name: Library

👤 Author (Category): Genre

👥 Book (Item): Book

🗂  SQL database name: db_name
```

### That's it.

The builder should automatically create the project and run the server. Navigate to: [http://localhost:5000/](http://localhost:5000/) to use it.