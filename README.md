# Introduction 

NOTE: This is still being developed and not ready for production and also come with no warranties at all.

This project is to provide an application (system) to help an organisaztion to keep track of what Nuget packages all their applications are using.  Know what your apps are relying on can help mitigate issue when it comes to breaking changes and which of your apps could be affected by this.

This also provides a sample of implementing the CQRS pattern in .Net Core with automatic registration of domain objects into the EF model.  For additional theory and info have a look at:
https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs

This sample will also need the following project which os available as a NuGet package.
https://github.com/BenVanZyl/SnowStorm
https://www.nuget.org/packages/SnowStorm/


# Getting Started
1. Clone the repo
2. Create a MS SQL database (any version, even local db)
3. Supply the connection strings and other applicable settings (appsettings.json):

       appsettings.json => Production settings

       appsettings.Development.json => development settings

- Reliance.Db.Scripts.MsSql
- Reliance.Web
4. Set Reliance.Db.Scripts.MsSql as start up project and run the code.  This should run the database scripts.
5. Set Reliance.Web as start up project and run the code.


# Build and Test
TODO: Describe and show how to build your code and run the tests. 

# Contribute
TODO: Explain how other users and developers can contribute to make your code better. 

If you want to learn more about creating good readme files then refer the following [guidelines](https://docs.microsoft.com/en-us/azure/devops/repos/git/create-a-readme?view=azure-devops). You can also seek inspiration from the below readme files:
- [ASP.NET Core](https://github.com/aspnet/Home)
- [Visual Studio Code](https://github.com/Microsoft/vscode)
- [Chakra Core](https://github.com/Microsoft/ChakraCore)


# Note

Deployment command in Powershell for db up:
dotnet.exe "$(System.DefaultWorkingDirectory)/_BenVanZyl.Reliance/drop/DbUpdates/Reliance.Db.Scripts.MsSql.dll" "$(DbUpConnectionString)"