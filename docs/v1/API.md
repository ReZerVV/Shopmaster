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

```json
{
    "accessToken": "Ts3K...2dIws",
    "refreshToken": "LacK2...g3I",
}
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
