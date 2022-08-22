# BlogApp

Minimal Blog Engine backend API, that allows to create, edit and publish text-based posts, with an approval flow where three different user types may interact.

The API should allow the following operations for the specified roles:

- Retrieve a list of all published posts (all roles)
- Add comments to a published post (all roles)
- Get own posts, create and edit posts (Writer)
- Submit posts (the post should move to a “pending approval” status where it’s locked and cannot be updated) (Writer)
- Get, Approve or Reject pending posts (Editor)

## Instructions

- Clone project
- Build project (VS recommended)
- Configure DefaultConnection in BlogApp.Backend/appsettings.json (MSSQL Server)
- Run

For testing purposes, a Postman collection has been added in the root with the possible authentications. The API security is based on JWT Tokens (10 min valid), so some manual replacement on the bearer token is required in Postman.

Time taken until initial API (with time taken to investigate): ~10-12 hours

## To-Do

- Implement UnitOfWork on Backend
- Implement Front-end for project
- Add ViewModels to send only required information in the API
