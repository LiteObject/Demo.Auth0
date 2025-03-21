# Dotnet Auth0 Demo

### Auth0: Applications vs. APIs

| Feature                  | **Applications (Clients)**         | **APIs**                       |
|--------------------------|-------------------------------------|----------------------------------|
| **Purpose**               | Authenticate users                  | Protect and expose backend resources |
| **Entity**                | Front-end/back-end client           | Backend service/API              |
| **Token Handling**         | Requests access/ID tokens           | Validates access tokens          |
| **Authentication**         | Manages user login and identity     | Does not authenticate users      |
| **Authorization**         | Requests tokens with specific scopes | Validates token and checks scopes |
| **Token Type**             | Access/ID tokens                   | Access tokens                    |
| **Examples**               | Web app, mobile app, M2M client    | REST API, GraphQL API            |
| **Common Flows/Grants**     | Authorization Code, PKCE, Implicit, Client Credentials | Validates client and user permissions |
| **Scopes Assignment**      | Requests specific scopes            | Defines allowed scopes           |
| **Audience (audience)**    | Does not define audience            | Defines audience for token requests |
| **Token Usage**            | Uses token to access APIs           | Validates token to allow/deny requests |
| **Allowed Grant Types**     | Configured in Application Settings  | Checked while validating token   |
| **Communication Flow**     | Sends token to API                  | Validates token and responds     |
| **Client Credentials**     | Used for machine-to-machine (M2M)  | Verifies scopes and permissions  |
| **Domain/Endpoint**         | Typically `https://your-app.com`   | Typically `https://your-api.com` |
| **When to Use**            | For web, mobile, or backend apps    | To protect and expose APIs       |



### Scope vs Permission
There is a subtle but important difference between **scopes** and 
**permissions** in Auth0, although they are closely related and often 
used together. Here's a breakdown:

| Feature       | Permissions                              | Scopes                                      |
|---------------|------------------------------------------|---------------------------------------------|
| Definition    | Defined by the API (Resource Server)     | Defined by the Authorization Server (Auth0) |
| Representation| Specific actions (e.g., read:profile)    | Collections of permissions or access levels |
| Requestor     | Granted to users/applications            | Requested by the client application         |
| Purpose       | Authorization (what the user/app can do) | Requesting access (what the app wants)      |
| Token Claim   | permissions                              | scope                                       |

Scopes are what the client asks for, and permissions are what the client is ultimately granted (and what the 
API uses for authorization). The mapping between scopes and permissions is configured in Auth0. Using permissions 
directly in your application is generally considered best practice. Scopes are often used as a more abstract 
way to request access, with the actual permissions being determined by the authorization server.

### Since I can create policies in the `Program.cs` file, why would I need to implement IAuthorizationService?

Creating policies in the `Program.cs` file is a straightforward way to define and apply authorization rules based 
on claims, roles, or custom requirements. However, there are scenarios where using the `IAuthorizationService` 
interface can provide additional flexibility and control over the authorization process.

#### When to Use IAuthorizationService
- Complex Authorization Logic
- Dynamic Authorization
- Custom Authorization Handlers
- Granular Control