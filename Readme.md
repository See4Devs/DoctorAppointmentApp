# Basic Doctor Appointments Management

This project is a sample Appointments booking for doctors. In this project I used the DDD pattern based on best practices when developping with .NetCore applications.

Database diagram of this project 

![Diagram](https://i.imgur.com/FcDinhO.jpg)

## Prerequisites
 * Docker insalled on you machine
 * DotNet installed

## Run your application
 * Set docker-compose as a startup project, and run your project using Visual Studio. Make sure you run docker before.
 * You can delete the Migrations folder, and the doctorly.db database. We are using an In Memorey database.


## To run test
  * Go the Application.Test directory
  * Then run the following command

  ```
  	dotnet test
  ```

