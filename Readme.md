# calc-cal
**Calorie Calculator App Powered by Google Gemini**

calc-cal is a user-friendly calorie calculator app that helps users track their daily calorie intake. It features an AI-powered calorie estimation function, allowing users to quickly determine the calorie content of dishes or ingredients—even when the exact nutritional information isn't known. Additionally, once a dish's calorie count is calculated, it's stored in the database to benefit all users, enhancing both performance and cost efficiency.

---
## Table of Contents
- [Features](#features)
- [Tech Stack](#tech-stack)
- [Installation and Setup](#installation-and-setup)
  - [Prerequisites](#prerequisites)
  - [Running Locally](#running-locally)
  - [Frontend Setup](#frontend-setup)
- [Usage](#usage)
- [Configuration](#configuration)
- [Contributing](#contributing)
- [License](#license)
- [Acknowledgments](#acknowledgments)

---
## Features
- **Daily Calorie Tracking:**  
  Users can easily record and compare their daily calorie intake.
  
- **AI-Powered Estimation:**  
  Utilize Google Gemini's AI to estimate the calorie content of a dish or ingredient when exact nutritional details are unavailable.

- **Community-Driven Database:**  
  Once a dish is entered by any user, its calorie information is saved and shared, reducing redundant API calls and enhancing user experience.

- **User Authentication:**  
  Although the app can be used without logging in, users are encouraged to create an account for saving daily progress. SMS verification (via Blower.io) ensures secure login.

---
## Tech Stack
- **Frontend:**  
  - React  
  - Typescript  
  - Material UI

- **Backend:**  
  - .NET 8  
  - Seq for logging

- **Database:**  
  - MongoDB

- **Additional Services:**  
  - SMS Gateway: Blower.io (for sending verification codes)
  - AI Integration: Google Gemini

- **Deployment and Development:**  
  - Heroku (for production deployment – note: currently in a frozen state)
  - Docker (for local development)

---
## Installation and Setup
### Prerequisites
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- [Docker Compose](https://docs.docker.com/compose/install/)

### Running Locally
1. **Clone the Repository:**
   ```bash
   git clone https://github.com/yourusername/calc-cal.git
   cd calc-cal
   ```

2. **Start the Local Environment:**  
   In the root folder of the project, run:
   ```bash
   docker-compose up
   ```
   This command will set up and run the entire backend environment using Docker.

### Frontend Setup
1. **Navigate to the Frontend Directory:**
   ```bash
   cd src/CalcCal.Web
   ```

2. **Start the Frontend Development Server:**
   ```bash
   npm start
   ```

---
## Usage
- **Without Login:**  
  Users can calculate calories without an account, but progress won't be saved.

- **With Login:**  
  For full functionality, including saving daily calorie data, users are recommended to log in. During login, a verification code is sent via SMS (powered by Blower.io).

- **AI-Powered Calorie Estimation:**  
  If unsure about the calorie content of a dish or ingredient, simply enter its name. The AI will automatically estimate the calories based on its internal logic and stored data.

---
## Configuration
The application uses several environment variables for proper configuration. Below are the details:

### Backend Configuration
Create a configuration file (e.g., appsettings.json) with the following structure:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Authentication": {
    "Issuer": "",
    "Audience": "",
    "SecretKey": "",
    "ExpireInMinutes": 60
  },
  "DatabaseSettings": {
    "ConnectionString": ""
  },
  "Gemini": {
    "ApiKey": "",
    "Url": "https://generativelanguage.googleapis.com/v1beta/models/gemini-pro:generateContent"
  },
  "MONGO_URL": "",
  "Blowerio": {
    "Url": ""
  }
}
```
Note: Replace the placeholder values (e.g., "") with your actual configuration values.

### Frontend Configuration
For the React frontend, create a `.env` file in the `/src/CalcCal.Web` directory with the following variable:
```env
REACT_APP_API_URL=""
```
Note: Replace the placeholder with the actual API URL for your backend.

---
## Contributing
This project is a hobby project, and contributions are welcome! Feel free to copy, contribute, or enhance the app as you see fit. If you have suggestions or improvements, please open an issue or submit a pull request.

---
## License
This project is open for public use. (You can add more specific licensing details if necessary.)
