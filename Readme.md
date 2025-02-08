# calc-cal

**Calorie Calculator App Powered by Google Gemini**

calc-cal is a user-friendly calorie calculator app that helps users track their daily calorie intake. It features an AI-powered calorie estimation function, allowing users to quickly determine the calorie content of dishes or ingredients—even when the exact nutritional information isn’t known. Additionally, once a dish’s calorie count is calculated, it’s stored in the database to benefit all users, enhancing both performance and cost efficiency.

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
