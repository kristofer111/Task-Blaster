First, to create the databases and start the applications, run ``docker compose up -d`` in the root folder. If the 'notifications' inexplicably stops running immediately after initialization, just press play again in Docker Desltop or run ``docker compose up -d`` once more. 

Next, to migrate the necessary tables to the database, navigate to the TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.API/ folder and run \
``dotnet tool install --global dotnet-ef`` (if necessary)
``dotnet restore`` (if necessary)
``dotnet ef migrations add <name>`` (if necessary)
``dotnet ef database update``

A population script that can be used to insert dummy data into the tables can be found in the TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.DAL/Scripts/ folder.

Next, to start the front end web-application, navigate to taskblaster-web/ and run \
``npm install``
``npm run dev``

The databases are set up with volumes to allow for persistent data. If for some reason the task-blaster-db is skipping initialization and you are not able to connect to it, it might be useful to delete the volumes and run the docker containers again. To delete the volumes, run ``docker volume rm task-blaster_task-blaster-data task-blaster_hangfire-data``

To run the applications locally, navigate to TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.API/ and run
``dotnet run``

Then navigate to TaskBlaster.TaskManagement/TaskBlaster.TaskManagement.Notifications/ and run
``dotnet run``
