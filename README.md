## AspNet Identity Without EF

对 `Microsoft.AspNet.Identity.Core` 程序集中的接口IRole、IUser、IQueryableRoleStore、IUserLoginStore的实现，自定义数据库表来实现用户的登录授权，不依赖于EF框架。

数据处理方面使用了[Dapper][dapper]（一个小型快速的ORM）。

代码主要是对[Asp.Net Codeplex][codeplex]例子中AspNet.Identity.MySQL的重写（***临摹***）。

纯属个人业余玩耍学习的代码，如有不对之处，请及时指出。


[dapper]:https://github.com/StackExchange/Dapper
[codeplex]:https://aspnet.codeplex.com/SourceControl/latest