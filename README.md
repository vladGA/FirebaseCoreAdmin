## What is FirebaseCoreAdmin

FirebaseCoreAdmin is .net core library for interacting with firebase database and storage. Library is supposed to use in server side. 
The interface of library is similar to offical google Node, Java (Admin) sdks.

## Initialization

Supports both json and p12 config files.
In order to give permissions to FirebaseCoreSdk to use your firebase database and storage you need first in your firebase app create [service account](https://firebase.google.com/docs/admin/setup) with corresponding permissions. After creating service account you will be propmted to download either json file or p12 file, recommended is json file. Download that file and attach to your project.

* Json file
``` C#
var credentials = new JSONServiceAccountCredentials("your-file.json");
var firebaseClient = new FirebaseAdmin(credentials);
```
* P12 file
``` C#
var credentials = new P12ServiceAccountCredentials("your-file.p12", "your-secret", "your-service-account", "your-database");
var firebaseClient = new FirebaseAdmin(credentials);
```

## Auth
Create token for some `userId` which should be used by client to authenticate against firebase database, that token could be used in client sdks by calling `firebase.auth().signInWithCustomToken(token)`

```C#
 var token = firebaseClient.Auth.CreateCustomToken(userId);
```

## Database
Getting reference on some node of database use `firebaseClient.Database.Ref("endpoint")` for example `firebaseClient.Database.Ref("users/12/details")`

### Query database
Following reference query methods are available
* LimitToLast
* OrderBy

For getting data
* Get
* GetWithKeyInjected

with their corresponding async methods

Examples:
Let's say you have this structure in firebase 
`-users/{userId}/events`

                      --EventKey1
                      ---- CodeId: 1
                      ---- IsRead: true,
                      ---- Timestamp: 1502047422150
                      --EventKey2
                      ---- CodeId: 2
                      ---- IsRead: false,
                      ---- Timestamp: 1502047422279

Let's assume we have UserHistory class
```C#
class UserHistory {
            public int CodeId { get; set; }
            public bool IsRead { get; set; }
            public long Timestamp { get; set; }
}
```

and can query via

```C#

var result = firebaseClient.Database.Ref("users/330/events")
                .OrderBy("isRead").LimitToLast(1)
                .Get<UserHistory>();

```
We can inject key into model by inheriting `UserHistory: KeyEntity`
and instead of calling `.Get<UserHistory>()` call `.GetWithKeyInjected<UserHistory>()`
like:

```C#
var result = firebaseClient.Database.Ref("users/330/events")
                .OrderBy("isRead").LimitToLast(1)
                .GetWithKeyInjected<UserHistory>();
```


### Update database

* Push
* Set
* Update

With corresponding async methods.
Methods are functioning exactly like their counterparts in NodeJs or Java sdks.

Examples:

`Push`
```C#
var result = firebaseClient.Database.Ref("/users/30/details").Push(new Detail())

```

`Bulk update`
```C#
var result = firebaseClient.Database.Ref("/users/30/details").Update(new Dictionary<string, object>() {
                { "codeId", 20 } ,
                { "info","info"} ,
                { "sub/info","subinfo"} ,
             });
```

`Set`

```C#
var result = firebaseClient.Database.Ref("/test").Set(new Test1());
```


## Storage
Following storage methods are supported

* GetPublicUrl
* GetSignedUrl
* RemoveObjectAsync
* GetObjectMetaDataAsync
* MoveObjectAsync

Examples:

```C#
var result = await firebaseClient.Storage.GetObjectMetaDataAsync("test/my-image");

var publicUrl = firebaseClient.Storage.GetPublicUrl("my-image");

var signedUrl = firebaseClient.Storage.GetSignedUrl(new Firebase.Storage.SigningOption()
             {
                 Action = Firebase.Storage.SigningAction.Write,
                 Path = "my-image",
                 ContentType = "image/jpeg",
                 ExpireDate = DateTime.Now + new TimeSpan(0, 0, 0, 0, 60000000)
             });
```





