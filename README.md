## Welcome to TicketsBasket (First Free Full Cloud Project Course)
TicketsBasket is the name of the project that we are building within the course, I prefer to give such a projects a name and branding instead of calling them demos, because in this way it looks like more realistic in addition to teaching the learners how to deal with the branding section in the solution.
The idea of the TicketsBasket project, is a software solution that combines the Events Organizers and the public in the same place, so the organizers can create and manage events (Conferences, Exhibitions, Sport Events .... etc.) and the public can view these events, shortlist, like them and of course book a tickets to attend them. 

## What you will learn  

 Through out the project we will build the app using C#, ASP.NET Core API, Blazor WebAssembly and Microsoft Azure.
 So you will learn how to combine all the .NET technologies to deliver a full product from A to Z following the best practices and the using the most cutting edge technologies in the industry. 
  
  ![Azure Services to be used](https://ahmadmozaffarstorage.blob.core.windows.net/blogs/Tickets%20Basket%20Technologes%20to%20use.png)

The previous picture shows the Azure services that will be used:

 - **App Service**: To host and run the application on the Cloud
 - **Azure Functions**: Modern service that allows us to develop single function that runs on the cloud and could be trigged by a specific triggers (Will use it to resize the images and create thumps from them and also to send emails to the users regularly)
 - **SQL**: We will use Azure SQL as the data store for the TicketsBasket
 - **Azure Redis for Cache**: Azure service to cache data in the Redis in memory database (Will use it to store temporarily the connection Ids of the Signal R clients for real-time functionality)
 - **Azure Active Directory B2C**: Active Directory B2C is a full managed Identity Service provides you with all the security requirements and services for authenticate and authorize users with very advanced features to manage the users and less time to implement an authentication system  
 - **Azure Storage**: Storage as a Service we will use the Azure Blob Storage to store the pictures of the events and the CV attachments sent by the users.
 - **Azure Signal R**: Signal R managed service to implement the real-time functionality in our solution.

For the API it will be built with ASP.NET Core API and the front-end we will use the awesome Blazor WebAssembly. 

 ## Technical requirement: 
 To get started with this course you need the following tools
 If you are on Windows, I highly prefer [Visual Studio 2019 Community Edition](https://visualstudio.microsoft.com/downloads/) if you are on Mac you can install it from here [Visual Studio 2019 for Mac OSX](https://visualstudio.microsoft.com/vs/mac/)
 You can follow up with [Visual Studio Code](https://code.visualstudio.com/) either on Windows, Mac OSX or Linux.

You  need the .NET Core 3.1 SDK [From Here](https://visualstudio.microsoft.com/vs/mac/) 

Free Azure Account, you can register [From Here](https://azure.microsoft.com/en-us/free/), if you are a student you can also get a free Azure account [By Verifying your student status from here](https://azure.microsoft.com/en-us/free/students/).

Azure Storage Explorer: [From this link](https://azure.microsoft.com/en-us/features/storage-explorer/)

Azure Key-Vault Explorer: [From this link](https://azure.microsoft.com/en-us/features/storage-explorer/)

Azure Storage Emulator (Windows only): [From this link](https://go.microsoft.com/fwlink/?LinkId=717179&clcid=0x409)

### Know more here 
[Tickets Basket Offical Blog](https://ahmadmozaffar.net/Blog/Details/TicketsBasket%20-%20Full%20Cloud%20Project%20Practical%20Course%20with%20Azure%20and%20.NET%20Core)
