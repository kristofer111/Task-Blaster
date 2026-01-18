This project is a task management backend implemented in ASP.NET Core with RESTful APIs and PostgreSql databases. The backend represents the background processes that might occur when working with systems such as Trello, JIRA or equivalent software. The backend has internal communication between APIs which are protected using M2M token-based authentication associated with Auth0. The backend is also set up to notify users with an external notification service using Mailjet. The entire backend is containerized with Docker, including with multi-service orchestration with Docker Compose.

## Dependencies
This project uses the following key technologies:
- ASP.NET Core
- Entity Framework Core
- Hangfire for background job processing
- PostgreSQL as the database
- Swashbuckle for API documentation
- Docker for containerization

For a full list of dependencies, see the `.csproj` files in the respective projects.

## Initialization

First, to create the databases and start the applications, run ``docker compose up -d`` in the root folder.
If the 'notifications' container inexplicably stops running immediately after initialization, just press play again in Docker Desktop, or run ``docker compose up -d`` again. 
Next, to migrate the necessary tables to the database, navigate to the TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.API/ folder and run \
``dotnet tool install --global dotnet-ef`` (if necessary) \
``dotnet restore`` (if necessary) \
``dotnet ef migrations add <name>`` (if necessary) \
``dotnet ef database update``

The TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.DAL/Scripts/ folder contains a population script for inserting dummy data into the tables.
Next, to start the front end web-application, navigate to taskblaster-web/ and run \
``npm install`` \
``npm run dev``

The databases are set up with volumes to allow for persistent data. If for some reason the task-blaster-db is skipping initialization and you are not able to connect to it, it might be useful to delete the volumes and run the docker containers again. To delete the volumes, run \
``docker volume rm task-blaster_task-blaster-data task-blaster_hangfire-data``

To run the applications locally, navigate to TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.API/ and run \
``dotnet run``

Then navigate to TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.Notifications/ and run \

``dotnet run``
