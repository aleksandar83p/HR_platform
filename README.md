# HR_platform
.NET 5 REST API

We are creating a part of an HR platform for adding and monitoring job candidates and their skills in order to facilitate HR processes within a company.
The task is to implement C# .NET 5 project for adding, updating, deleting, and searching job candidates and their skills (basic CRUD operations).

Here is list of requirements that should be taken into consideration:
• Each job candidate has the following attributes: name (full name), date of birth, contact number, E-mail and the list of skills which he/she possesses; skills like Java programming, C# programming, Database design, English, Russian, German language …
• Each skill has its name

Task:
1) Create database model and appropriate tables for job candidates and skills (you can use any relational database you prefer)
2) Create appropriate model and service classes which represent these functionalities
3) Expose these functionalities through REST web services with following operations: add job candidate, add skills, update job candidate with skill, remove skill from job candidate, remove candidate, search candidate by name and/or given skill(s)
4) Extra points for unit tests for service layer
5) Extra points for unit tests for rest APIs
6) Extra points for implementing swagger
7) Extra points for implementing UI via React 16+"
