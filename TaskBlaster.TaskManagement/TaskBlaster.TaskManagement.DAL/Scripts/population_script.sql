-- Priorities initial population
INSERT INTO "public"."Priorities" ("Name", "Description")
VALUES ('Critical', 'Tasks that must be addressed immediately');

INSERT INTO "public"."Priorities" ("Name", "Description")
VALUES ('High', 'Important tasks that should be prioritized after critical ones');

INSERT INTO "public"."Priorities" ("Name", "Description")
VALUES ('Medium', 'Tasks that are important but not urgent');

INSERT INTO "public"."Priorities" ("Name", "Description")
VALUES ('Low', 'Tasks of lesser importance that can be attended to last');

-- Statuses initial population
INSERT INTO "public"."Statuses" ("Name", "Description")
VALUES ('Backlog', 'Task is not started yet');

INSERT INTO "public"."Statuses" ("Name", "Description")
VALUES ('In Progress', 'Task is currently being worked on');

INSERT INTO "public"."Statuses" ("Name", "Description")
VALUES ('Completed', 'Task has been finished');

INSERT INTO "public"."Statuses" ("Name", "Description")
VALUES ('On Hold', 'Task is temporarily paused');

INSERT INTO "public"."Statuses" ("Name", "Description")
VALUES ('In Review', 'Task is being reviewed before completion');

INSERT INTO "public"."Users" ("FullName" , "EmailAddress", "ProfileImageUrl", "CreatedAt") VALUES
('Alice Johnson', 'alice.johnson@example.com', 'https://example.com/images/alice.jpg', '2023-10-01 08:30:00'),
('Bob Smith', 'bob.smith@example.com', 'https://example.com/images/bob.jpg', '2023-10-01 09:15:00'),
('Catherine Lee', 'catherine.lee@example.com', 'https://example.com/images/catherine.jpg', '2023-10-02 10:00:00'),
('David Kim', 'david.kim@example.com', 'https://example.com/images/david.jpg', '2023-10-03 11:45:00'),
('Eva Green', 'eva.green@example.com', 'https://example.com/images/eva.jpg', '2023-10-04 12:30:00'),
('Frank Brown', 'frank.brown@example.com', 'https://example.com/images/frank.jpg', '2023-10-05 13:15:00'),
('Grace Taylor', 'grace.taylor@example.com', 'https://example.com/images/grace.jpg', '2023-10-06 14:00:00'),
('Henry Wilson', 'henry.wilson@example.com', 'https://example.com/images/henry.jpg', '2023-10-07 15:45:00'),
('Isabella Moore', 'isabella.moore@example.com', 'https://example.com/images/isabella.jpg', '2023-10-08 16:30:00'),
('Kristófer Svavarsson', 'kristoferorri1@gmail.com', 'https://example.com/images/jack.jpg', '2023-10-09 17:15:00');

INSERT INTO "public"."Tasks" ("Title", "Description", "CreatedAt", "DueDate", "PriorityId", "StatusId", "AssignedToId", "CreatedById", "IsArchived") VALUES
('Setup Project Repository', 'Initialize and set up the project repository with appropriate branches and permissions.', '2023-10-01 08:00:00', '2023-10-05 18:00:00', 1, 2, 1, 1, false),
('UI Design Draft', 'Create the initial UI mockups for the project dashboard and main pages.', '2023-10-02 09:30:00', '2023-10-08 18:00:00', 2, 1, 2, 1, false),
('Database Schema Design', 'Design the database schema including tables and relationships for core entities.', '2023-10-03 10:00:00', '2023-10-10 17:00:00', 1, 1, 3, 2, false),
('Implement Authentication', 'Develop user login and authentication module using JWT.', '2023-10-04 11:00:00', '2023-10-12 18:00:00', 1, 3, 4, 2, false),
('API Development', 'Create RESTful API endpoints for core functionalities like user management.', '2023-10-05 12:15:00', '2023-10-15 18:00:00', 2, 2, 5, 3, false),
('Write Test Cases', 'Write unit tests for the authentication and user management modules.', '2023-10-06 13:30:00', '2023-10-16 17:00:00', 3, 1, 6, 3, false),
('Frontend Integration', 'Integrate API endpoints with the frontend using Axios.', '2023-10-07 14:45:00', '2023-10-18 18:00:00', 2, 2, 7, 4, false),
('Performance Optimization', 'Identify and optimize performance bottlenecks in the application.', '2023-10-08 15:00:00', '2023-10-20 17:00:00', 3, 1, 8, 4, false),
('Deployment Setup', 'Set up deployment scripts and CI/CD pipeline for automatic deployment.', '2023-10-09 16:00:00', '2023-10-22 18:00:00', 1, 2, 9, 5, false),
('Project Documentation', 'Complete and submit final project documentation.', '2023-10-10 17:15:00', '2023-10-25 17:00:00', 3, 1, 10, 5, false);

INSERT INTO "public"."Comments" ("Author", "ContentAsMarkdown", "CreatedDate", "TaskId") VALUES
('Alice Johnson', 'Initial task analysis complete. Moving to planning phase.', '2023-10-10 08:30:00', 1),
('Bob Smith', 'Reviewed the documentation, and it looks good. Minor adjustments suggested.', '2023-10-11 09:00:00', 1),
('Catherine Lee', 'Started working on the initial draft of the implementation.', '2023-10-12 10:15:00', 2),
('David Kim', 'Encountered a minor bug in the login module, will debug.', '2023-10-13 11:45:00', 2),
('Eva Green', 'UI design has been updated per the new guidelines.', '2023-10-14 12:30:00', 3),
('Frank Brown', 'Test cases for the login module are ready for review.', '2023-10-15 13:00:00', 3),
('Grace Taylor', 'Code review scheduled for tomorrow at 10 AM.', '2023-10-16 14:20:00', 4),
('Henry Wilson', 'Documentation draft submitted for approval.', '2023-10-17 15:10:00', 4),
('Isabella Moore', 'Planning to add error handling for all possible API failures.', '2023-10-18 16:25:00', 5),
('Jack Anderson', 'Deployed to the test environment successfully.', '2023-10-19 17:35:00', 5);

INSERT INTO "public"."Tags" ("Name", "Description") VALUES
('UI/UX', 'Tasks related to user interface and user experience design'),
('Backend', 'Tasks involving backend development and server-side logic'),
('Database', 'Tasks focused on database design, optimization, and management'),
('API', 'Tasks related to creating and managing API endpoints'),
('Testing', 'Tasks that involve writing, running, and reviewing tests'),
('Documentation', 'Tasks related to creating or updating project documentation'),
('Deployment', 'Tasks related to deployment processes and CI/CD pipelines'),
('Performance', 'Tasks focused on improving the application’s performance'),
('Security', 'Tasks dedicated to securing the application and its data'),
('Research', 'Tasks that involve research or investigation into new tools and techniques');

INSERT INTO "public"."TagTask" ("TagsId", "TasksId") VALUES
(1, 2), -- UI/UX for 'UI Design Draft'
(2, 4), -- Backend for 'Implement Authentication'
(3, 3), -- Database for 'Database Schema Design'
(4, 5), -- API for 'API Development'
(5, 6), -- Testing for 'Write Test Cases'
(6, 10), -- Documentation for 'Project Documentation'
(7, 9), -- Deployment for 'Deployment Setup'
(8, 8), -- Performance for 'Performance Optimization'
(9, 4), -- Security for 'Implement Authentication'
(10, 1); -- Research for 'Setup Project Repository'

INSERT INTO "public"."TaskNotifications" ("TaskId", "DueDateNotificationSent", "DayAfterNotificationSent", "LastNotificationDate") VALUES
(1, TRUE, TRUE, '2023-10-05 18:00:00'),   -- Task 1: No notifications sent
(2, TRUE, TRUE, '2023-10-09 18:00:00'),    -- Task 2: Notifications sent on due date and day after
(3, TRUE, TRUE, '2023-10-11 17:00:00'),   -- Task 3: No notifications sent
(4, TRUE, TRUE, '2023-10-12 18:00:00'),   -- Task 4: Notification sent only on due date
(5, TRUE, TRUE, '2023-10-16 18:00:00'),    -- Task 5: Notifications sent on both due date and day after
(6, TRUE, TRUE, '2023-10-16 17:00:00'),   -- Task 6: Notification sent on due date only
(7, TRUE, TRUE, '2023-10-19 18:00:00'),    -- Task 7: Notifications sent on both due date and day after
(8, TRUE, TRUE, '2023-10-21 17:00:00'),   -- Task 8: No notifications sent
(9, TRUE, TRUE, '2023-10-22 18:00:00'),   -- Task 9: Notification sent only on due date
(10, FALSE, FALSE, NULL);   -- Task 10: Notifications sent on both due date and day after
