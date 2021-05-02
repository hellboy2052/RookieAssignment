# Rookie Assignment Project - RookieAssignment
## Languages
- **TypeScript**.
- **C#**.
## Framework
- **API**: ASP.Net Core API, Identity, Entity Framework 5.0.
- **API.test**: Xunit Test.
- **CustomerSite**: ASP.Net Core MVC.
- **my-admin**: React, Mobx, Axios.
## Database
- **SQL**: Microsoft SQL server.
## Deploy
- Microsoft Azure.
## Service
- Cloudinary
## Main structure and functionality
### API
- Contains API queries and commands to provide and manipulate data from database.
- Handling authentication and authority by using Cookies and JWTs.
### API.test
- test controller: Brand.
### CustomerSite
- Login/register account.
- Show list of products by brand or category (or both).
- View product details.
- User can rate the product from 1 - 5 star.
- To rate or add product to cart user must login first.
### my-admin
- Only user with role of superadmin or member are allow to login.
- Manage product detail.
- View product detail.
- Manage all image of product and select which image to be the main image.
- Manage product brand.
- Show list of user.
## Notice
- If manage image functionality doesn't work, it probably because cloud name, API key and secret aren't provided.
## Contact
- **Author**: Pham Quang Cao Tri
- **Email**: caotribo123@gmail.com

