# VoyadoTest

The application integrates with Google and Bing search engines to ensure that its content is easily discoverable by users. This code is designed to comply with search engine guidelines and standards, ensuring that it follows the recommended practices set by search engines such as Google, Bing, and Microsoft.

## Features
### Backend:
The backend is built with .NET C# technology, offering a robust and scalable foundation for the application. It utilizes the powerful features of .NET and provides efficient data processing, server-side logic, third-party service utilization for word ranking.

### Frontend:
The frontend of the application is developed using React, a popular JavaScript library for building user interfaces. Redux, a state management library, is also integrated to ensure a predictable and centralized state management system. This combination enables the creation of interactive and dynamic user interfaces.

### Seamless Communication:
The backend and frontend communicate seamlessly using APIs.
This enables efficient data transfer and ensures a smooth flow of information between the two sides of the application.

### Deployment:
The project is designed with Infrastructure as code (IaC) approach, using GitHub actions for building and publishing docker container into cloud.

### Hosting:
The application utilizes the robust infrastructure and services provided by Google Cloud Platform (GCP). GCP offers a wide range of services that enable seamless deployment and management of applications in the cloud.

### Secrets Management:
The application follows best practices for secrets management, ensuring that sensitive data is stored securely.
Secrets, such as API keys, database credentials, and Search Engine identifiers, are stored separately from the application code and configuration files. They are encrypted and protected using industry-standard encryption algorithms.

### Testing
Testing plays a crucial role in ensuring the reliability and stability of this application.
The business layer, which forms the core functionality, is covered with tests to validate its behavior and maintain the highest quality standards. 

## How to run
There are 2 main ways how to run this service. 
1. Open project in VisualStusio or Rider, provide secrets in the appsettings file, press F5 and wait for it.
2. Build and run the container with the next commands:

For docker build:
`docker build -f ./VoyadoTest/Dockerfile -t voyado-test .`

For docker run:
`docker run -e Search__Bing__subscriptionKey=######################## -e Search__Google__apiKey=######################## -e Search__Google__cx==######################## -p 8001:80 -d voyado-test`

where the `########################` should be replaced with the appropriate values.