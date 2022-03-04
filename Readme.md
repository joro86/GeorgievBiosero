I had fun creating this project. I tried to follow the requirements as closed to as I can.

Here is somehting to know if you try to run this.

Please do not run the [Authorize] endpoint without without passing JWT token to the endpoint.
If you do, expect an exception since the app will not be able to get the user ID from the JWT token.
Here is how to generate one..

POST: https://localhost:44302/api/authentication/login
BODY: 
	{
		"username": "test",
		"password": "password"
	}

All users are hardcoded in UserRepository. I registered it as Singleto to use the same instance.

Login endpoint will return JWT toke that you can use to call the Delete(unpublish) endpoint;
https://localhost:44302/api/book/3

Let's make sure the book was unpublished:
https://localhost:44302/api/book/3


Create/Publish a book
POST: https://localhost:44302/api/book/create
BODY: 
{
    "title": "title",
    "description": "Description",
    "coverImage": "",
    "price": 44,
    "isPublished": true
}







