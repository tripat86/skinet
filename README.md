# skinet
E-commerce application with .Net Core using Generic Repository with Specification Pattern / Angular10 / Redis cache / Stripe payments

![alt-text](https://github.com/tripat86/skinet/blob/master/API/wwwroot/images/products/Shopping_Cart.gif)

This application is a proof of concept E-Commerce store using .net core web api and client side front-end Angular UI for the store using the Angular CLI. Some highlights of the project are following:

- use the Repository, Unit of Work and specification pattern in .net core
- Automapper in ASP.NET Core
- Paging, Sorting, Searching and Filtering
- Created custom middleware to handle 500 Internal server errors
- Created mechanism to return all sort of errors like 401, 404, 400, 500 into a common object which has status and response properties.
- Best OOPS practices used.
- Added Generic Pagination class that can be used with any Entity to get paged results.
