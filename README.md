# WookieBooks.Api

## Objective
Implement a bookstore REST API using C# and .Net Core.

## Tasks
* A basic book model with title, description, author, cover image and price.
* A REST endpoint for the /books resource with CRUD operations (returns JSON)
* In-memory book repository and have two default items (books)
* REST endpoints are below

## Endpoints
  | Method  | End Point | Parameters |
| ------------- | ------------- | -------------|
| GET  | /books/getBook  | id - long |
| GET  | /books/getAllBooks |  |
| PUT  | /books/saveBook  | book - Book Object |
| PATCH  | /books/updateBook  | book - Book Object |
| DELETE  | /books/deleteBook  | id - long |

## Features
 * Unit Tests
 * Swagger Support
 
