# Static-Site-Builder-Dev

This repository contains the public version of the *Static-Site-Builder* project. The goal of this project is to create a static site management platform that allows end users to easily create websites or online stores through an intuitive web application. The site was originally deployed on Heroku, but the deployment is currently down due to changes in payment arrangements with Heroku.

## Project Overview

The *Static-Site-Builder* project allows users to build and manage static websites without needing advanced technical knowledge. The project automates the process of generating static websites, setting up repositories, and deploying them. The application streamlines the entire flow, from user input to deployment, with no additional action needed from the user.

### User Process

1. **Web Application Interface**: The user accesses the web application to create a website or online store.
2. **Template Selection**: The user selects an HTML template from a curated list. The backend uses a static site generator, such as Jekyll, to generate the site based on the selected template.
3. **Automatic Repository Creation**: After site generation, the application automatically creates a new Git repository for the user.
4. **Code Push**: The generated site code is then pushed to the newly created repository.
5. **Netlify Configuration**: The application configures a new project in Netlify, linking it to the user's repository.
6. **Deployment**: Once the repository and project are set up, the application automatically deploys the website without any user intervention.

This streamlined process eliminates the need for manual setup, making website creation and deployment as simple as possible for users.

## Features

- **Static Site Creation**: Users can create and manage their own static websites or online stores with ease.
- **Template-based**: The user selects a template, and the backend generates the site based on it.
- **Automatic Git Integration**: A new Git repository is created, and the generated code is pushed to it automatically.
- **Netlify Deployment**: The application configures and deploys the site on Netlify without requiring the user to perform any setup.
- **Data Gathering**: The application collects necessary data from the user to customize the site based on their needs, ensuring the final product matches their expectations.

## Current Status

- **Deployment**: The site was previously deployed on Heroku, but it is currently down due to payment changes on Heroku.
- **Development**: The development of the platform is frozen , and improvements and fixes are frozen.
