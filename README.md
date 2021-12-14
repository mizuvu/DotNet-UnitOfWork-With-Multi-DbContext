# UnitOfWork &amp; Repository pattern with multi DbContext
<br/>

## Frameworks / Platforms
* .NET 6
* ASP.NET Core Web Api 6
* Entity Framework Core 6
* Microsoft.EntityFrameworkCore.Sqlite 6
* Microsoft.EntityFrameworkCore.SqlServer 6

## How to use
- Create an DbContext or Interface inherits from IDbFactory
- Inject UnitOfWork as `IUnitOfWork<IApplicationDbContext>` or `IUnitOfWork<ApplicationDbContext>`

