Steps to run the project 
1. Open in Visual Studio 2022 (you should have dot net 8 sdk)
2. Open Package Manager Console: Run below commands one by one
Add-Migration Init
Update-Database

3. Run app → Swagger UI appears




# Username Microservice (.NET 8 + SQLite)

This microservice validates and stores usernames uniquely per account.

## Features

- Validate username format
- Register username + accountId
- Prevent duplicate usernames
- One username per account

## Tech Stack

- ASP.NET Core 8
- Entity Framework Core
- SQLite

## API Endpoints

###Validate Username

**GET /api/usernames/validate?username=yourName**

Returns:
- `200 OK` if valid
- `400 Bad Request` if invalid

### Register Username

**POST /api/usernames**

```json
{
  "accountId": "GUID",
  "username": "AlphanumericString"
}