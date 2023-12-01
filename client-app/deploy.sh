#!/bin/bash

# Navigate to the React project directory
cd /Users/flo/Desktop/ynov/C#/Ynov-Board/client-app

# Build the React project
npm run build

# Copy the build output to the ASP.NET Core wwwroot directory
cp -R build/* /Users/flo/Desktop/ynov/C#/Ynov-Board/Ynov.API/wwwroot

echo "Deployment successful."
