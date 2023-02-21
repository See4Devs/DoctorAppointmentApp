# Basic Doctor Appointments Management

This project is a sample Appointments booking for doctors. In this project I used the DDD pattern based on best practices when developping with .NetCore applications.

Database diagram

![Diagram](https://i.imgur.com/FcDinhO.jpg)

## Prerequisites
 * [Docker](https://www.docker.com/products/docker-desktop/) insalled on you machine
 * [DotNet installed](https://dotnet.microsoft.com/en-us/download), this project was built using version 6.0.202

## Run your application
 * Set docker-compose as a startup project, and run your project using Visual Studio. Make sure you run docker before.
 * You can delete the Migrations folder, and the doctorly.db database. We are using an In Memorey database.


## To run your tests
  * Go the Application.Test directory
  * Then run the following command

  ```
  	dotnet test
  ```

