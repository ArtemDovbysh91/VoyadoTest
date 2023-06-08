import * as React from 'react';
import { connect } from 'react-redux';

const About = () => (
  <div>
    <h1>Ranking service.</h1>
    <p>The application integrates with Google and Bing search engines to ensure that its content is easily discoverable by users. This code is designed to comply with search engine guidelines and standards, ensuring that it follows the recommended practices set by search engines such as Google, Bing, and Microsoft.</p>
    The github repo can be found here: <a href={"https://github.com/ArtemDovbysh91/VoyadoTest"}> https://github.com/VoyadoTest</a>

    <h3>Features</h3>
    <h5>Backend:</h5>
    <p>The backend is built with .NET C# technology, offering a robust and scalable foundation for the application. It utilizes the powerful features of .NET and provides efficient data processing, server-side logic, third-party service utilization for word ranking.</p>
    <h5>Frontend:</h5>
    <p>The frontend of the application is developed using React, a popular JavaScript library for building user interfaces. Redux, a state management library, is also integrated to ensure a predictable and centralized state management system. This combination enables the creation of interactive and dynamic user interfaces.</p>
    <h5>Seamless Communication:</h5>
    <p>The backend and frontend communicate seamlessly using APIs. This enables efficient data transfer and ensures a smooth flow of information between the two sides of the application.</p>
    <h5>Deployment: </h5>
    <p>The project is designed with Infrastructure as code (IaC) approach, using GitHub actions for building and publishing docker container into cloud.</p>
    <h5>Hosting: </h5>
    <p>The application utilizes the robust infrastructure and services provided by Google Cloud Platform (GCP). GCP offers a wide range of services that enable seamless deployment and management of applications in the cloud.</p>
    <h5>Secrets Management: </h5>
    <p>The application follows best practices for secrets management, ensuring that sensitive data is stored securely.
        Secrets, such as API keys, database credentials, and Search Engine identifiers, are stored separately from the application code and configuration files. They are encrypted and protected using industry-standard encryption algorithms.</p>
    <h5>Testing: </h5>
    <p>Testing plays a crucial role in ensuring the reliability and stability of this application.
        The business layer, which forms the core functionality, is covered with tests to validate its behavior and maintain the highest quality standards. </p>

  </div>
);

export default connect()(About);
