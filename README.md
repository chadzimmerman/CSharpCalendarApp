# Calendar Scheduling Application

## Overview

This Calendar Scheduling Application is a desktop software project developed in C# using WinForms. It was designed to meet real-world business requirements for a global consulting organization, simulating a contract development scenario. The application connects to a MySQL database (structure fixed by the client) to manage customer records and appointments with complex validation and reporting features.

This project showcases key software development skills including user authentication, multi-language support, time zone adjustments, and robust data handling — all essential for professional software engineering roles.

---

## Features

* **User Authentication**

  * Login with username and password (`test`/`test` by default).
  * Detects user location and supports English plus one additional language for UI messages and errors.

* **Customer Management**

  * Add, update, and delete customer records.
  * Validation ensures all fields are non-empty and phone numbers contain only digits and dashes.
  * Exception handling for database operations.

* **Appointment Scheduling**

  * Create, modify, and delete appointments linked to customers.
  * Enforces business hours (9:00 AM to 5:00 PM EST, Monday–Friday).
  * Prevents overlapping appointments.
  * Automatically adjusts appointment times for user time zones and daylight saving.

* **Calendar View**

  * Displays appointments for a selected day via a month calendar UI.

* **Alerts and Logging**

  * Alerts users of appointments occurring within 15 minutes after login.
  * Logs all login attempts with timestamps and usernames to a “Login\_History.txt” file.

* **Reporting**

  * Generates reports using collection classes and lambda expressions for:

    * Number of appointment types by month.
    * User schedules.
    * A custom additional report.

---

## Technical Details

* **Language & Framework:** C# (.NET Framework) with WinForms UI
* **Database:** MySQL (client-provided schema, unmodifiable)
* **Development Environment:** Visual Studio
* **Project Structure:** Full Visual Studio solution with all source files, database connectivity, and resources.

---

## How to Run
Set up the MySQL Database

Install MySQL Server locally if you don’t have it already.

Run the provided SQL scripts in the following order to create and populate the database:

C969_DB_Setup.sql (creates the database schema)

LoadData.sql (inserts sample data)

You can run these scripts using MySQL Workbench, the MySQL CLI, or any other MySQL client tool.

Configure the Application

Make sure your app’s database connection string matches your local MySQL setup (e.g., username, password, port).

This connection string is typically found in the app’s configuration files or code.

Run the Application

Open the solution in Visual Studio.

Build and run the application.

Login using the username and password: test / test.



---

## Purpose

This application was created as a performance assessment project for Western Governors University (WGU) to demonstrate proficiency in software design, database interaction, UI development, and professional coding practices. It is intended as a portfolio piece to showcase skills relevant to software engineering employment.

---

## Notes

* No external libraries or frameworks were used outside the .NET Framework.
* The database initially contains no data and must be populated through the application interface.
* All project files are included with preserved Visual Studio folder structure for ease of compilation and evaluation.

---

Feel free to customize or ask if you'd like me to help write a short summary or cover letter blurb to pair with this!
