I had fun creating this project. I tried to follow the requirements as closed to as I can.
I tried to show my stryle of coding and how I write unit tests. 

Here is somehting to know if you try to run this.

Please do not run the [Authorize] endpoint without without passing JWT token to the endpoint.
If you do, expect an exception since the app will not be able to get the user ID from the JWT token.
Here is how to generate one..

POST: https://localhost:44302/api/auth/login
BODY: 
	{
		"username": "test",
		"password": "password"
	}

    User name and password for darth vader
    	{
		"username": "darth",
		"password": "vader"
	}

All users are hardcoded in UserRepository. I registered it as Singleton to use the same instance.

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







