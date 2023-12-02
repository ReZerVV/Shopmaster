# API

## Authorization

### Register

#### Register request

```js
POST {{host}}/api/v1/auth/register
```

```json
{
    "firstName": "Cyril",
    "lastName": "Morozov",
    "email": "cyril@morozov.com",
    "password": "Cyril123"
}
```

#### Register response

```js
200 OK
```

### Login

#### Login request

```js
POST {{host}}/api/v1/auth/login
```

```json
{
    "email": "cyril@morozov.com",
    "password": "Cyril123"
}
```

#### Login response

```js
200 OK
```

```json
{
    "accessToken": "Ts3K...2dIws",
    "refreshToken": "LacK2...g3I",
}
```

### Recovery

#### Recovery request

```js
POST {{host}}/api/v1/auth/recovery
```

```json
{
    "email": "cyril@morozov.com"
}
```

#### Recovery response

```js
200 OK
```

### Recovery

#### Recovery request

```js
POST {{host}}/api/v1/auth/recovery/{link}
```

```json
{
    "password": "Cyril123"
}
```

#### Recovery response

```js
200 OK
```

### Refresh

#### Refresh request

```js
POST {{host}}/api/v1/auth/refresh
```

#### Refresh response

```js
200 OK
```

```json
{
    "accessToken": "Ts3K...2dIws",
    "refreshToken": "LacK2...g3I",
}
```

### Confirm

#### Confirm request

```js
POST {{host}}/api/v1/auth/confirm/{link}
```

#### Confirm response

```js
200 OK
```

### Logout

#### Logout request

```js
POST {{host}}/api/v1/auth/logout
```

#### Logout response

```js
200 OK
```

## Accounts

### Me

#### Me request

```js
GET {{host}}/api/v1/accounts/me
```

#### Me response

```js
200 OK
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "firstName": "Cyril",
    "lastName": "Morozov",
    "email": "cyril@morozov.com"
}
```

### Get by id

#### Get by id request

```js
GET {{host}}/api/v1/accounts/{userId}
```

#### Get by id response

```js
200 OK
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "firstName": "Cyril",
    "lastName": "Morozov",
    "email": "cyril@morozov.com"
}
```

### Edit password

#### Edit password request

```js
PUT {{host}}/api/v1/accounts/password
```

```json
{
    "oldPassword": "Cyril123",
    "newPassword": "Cyril321"
}
```

#### Edit password response

```js
200 OK
```

### Delete

#### Delete request

```js
DELETE {{host}}/api/v1/accounts/
```

#### Delete response

```js
200 OK
```

### Edit

#### Edit request

```js
PUT {{host}}/api/v1/accounts/
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "firstName": "Cyril",
    "lastName": "Morozov",
    "email": "cyril@morozov.com"
}
```

#### Edit response

```js
200 OK
```

```json
{
    "id": "00000000-0000-0000-0000-000000000000",
    "firstName": "Cyril",
    "lastName": "Morozov",
    "email": "cyril@morozov.com"
}
```

## Categories

### Create

#### Create request

```js
POST {{host}}/api/v1/categories/
```

```json
{
    "name": "Electronics"
}
```

#### Create response

```js
200 OK
```

```json
{
    "id": 1,
    "name": "Electronics"
}
```

### Edit

#### Edit request

```js
PUT {{host}}/api/v1/categories/
```

```json
{
    "id": 1,
    "name": "Electronics"
}
```

#### Edit response

```js
200 OK
```

```json
{
    "id": 1,
    "name": "Electronics"
}
```

### Delete

#### Delete request

```js
DELETE {{host}}/api/v1/categories/{categoryId}
```

#### Delete response

```js
200 OK
```

### Get all

#### Get all request

```js
GET {{host}}/api/v1/categories/
```

#### Get all response

```js
200 OK
```

```json
[
    {
        "id": 1,
        "name": "Electronics"
    }
]
```

### Get by id

#### Get by id request

```js
GET {{host}}/api/v1/categories/{categoryId}
```

#### Get by id response

```js
200 OK
```

```json
{
    "id": 1,
    "name": "Electronics"
}
```