# Sensor List in Avalonia

## Table of Contents

- [Introduction](#introduction)
- [Project Setup](#project-setup)
- [Backend Setup](#backend-setup)
  - [DBContext Implementation](#dbcontext-implementation) 
  - [Sensor Model](#sensor-model)
  - [Repository Implementation](#repository-implementation)
- [Frontend Setup](#frontend-setup)
  - [SensorItem](#sensoritem) 
  - [MainWindow](#mainwindow)
  - [CreateSensor](#createsensor)
- [CRUD Functionality](#crud-functionality)
    - [Repository Implementations](#repository-implementations)
    - [Binding and Validations](#bindings-and-validations)
- [Conclusion](#conclusion)
- [Next Steps](#next-steps)


## Introduction
I have been studying programming for some time now, and
although I have gained knowledge and experience, I still
consider myself relatively new to developing more complex
applications.
To challenge myself and further enhance my skills, I asked
ChatGPT to generate project ideas in various difficulty
levels for my main programming languages.

For the easy-level project in C#, the initial suggestion was 
to develop a generic TODO list. While this idea is widely
used and familiar to many beginner programmers, I wanted to
take on a more distinctive and personally meaningful project.

I have a collection of electronic sensors that I use for
various purposes, and was keeping track of them using a
simple text file. This project aims to provide a better
organization method by leveraging a database to store the
sensor items and creating a user-friendly frontend to display
them as a list.

By developing this project, I hope to showcase my ability to
work with databases, implement CRUD operations and design a
functional (although not visually appealing) user interface.
It serves as a practical solution to my own organizational
needs while also demonstrating my programming skills.

Throughout this project, I tried to follow best practices,
incorporate design patterns, and make use of the C# language
features and frameworks to create a clean and maintainable
codebase.

## Project Setup
To ensure the separation of concerns and maintain a modular
codebase, this project will adhere to the Model-View-ViewModel
(MVVM) architecture pattern. MVVM promotes a clear separation
between the user interface (View), the data and logic (Model),
and the intermediary layer that connects them (ViewModel).

For the frontend development, I chose to utilize the Avalonia
Framework, a WPF (Windows Presentation Foundation) inspired 
cross-platform UI framework that enables the creation of
responsive desktop applications. With Avalonia,
I can build the user interface components and easily integrate
them with the rest of the application.

On the backend side, I established the database connection
using Entity Framework Core. This ORM (Object-Relational
Mapping) framework simplifies the interaction with the
database by providing a higher-level abstraction.
EFCore allows me to define the data model,
perform database operations, and handle data persistence
in a convenient and efficient manner.

## Backend Setup
The backend of the project involves the creation of a
Model class, a DBContext class to handle the database
connection,and a Repository class responsible for executing
SQL queries.

### DBContext Implementation
The DBContext class serves as the bridge between the
application and the database. It inherits from EF Core's
DBContext class and overrides the OnConfiguring method,
which is called when the DBContext is being configured.

In the OnConfiguring method, an OptionsBuilder instance is
passed as a parameter. Using the OptionsBuilder, we define
the database to be used and provide the connection string.

Originally, the project utilized SQLite, which creates a
local database for the application. However, during testing,
it was observed that the application encountered issues
finding the table in the database when running from the IDE
or in release mode. To overcome this issue, the decision was
made to switch the database to MariaDB, which was already
installed on my local machine.

To protect information such as the database password,
the connection string was encapsulated within a hidden
Connection class. Make sure to replace the properties in the
Connection class with your actual connection string.
For MySQL or MariaDB, the connection string follows this
template:

```csharp
"server={serverAddress};port={serverPort};database={databaseName};user={username};password={userPassword}"
```

### Sensor Model
The Sensor model represents an item in the database table.
The class properties will be used by Entity Framework to
create the corresponding columns in the database table based
on the attributes specified.

A sensor should have the following properties:

- An auto-incrementing ID to serve as the primary key;
- A non-null name string to identify the sensor;
- A non-null category string to categorize the sensor;
- An amount property, which is a non-negative integer and
defaults to 0;

The attributes specified on the properties of the Sensor
model class inform Entity Framework how to map the properties
to the corresponding columns in the database table.

### Repository Implementation
The Repository class serves as the Data Access Layer,
responsible for handling database read and write operations
through C# code.

This class implements methods to perform Create, Read,
Update, and Delete (CRUD) functionalities. The Repository
class interacts with the DBContext to execute SQL queries
and manipulate the data stored in the database.

By implementing the Repository pattern, we can encapsulate
the data access logic and provide a clear separation between
data operations and other application layers, allowing a
more maintainable code.

The CRUD functionalities will be detailed in the upcoming
sections, demonstrating how the Repository class interacts
with the database and how data is retrieved and manipulated.

## Frontend Setup
The frontend of this project involves the Views and their
corresponding ViewModels, which define the user interface
and handle the binding with the backend.

### SensorItem
The SensorItemView is the visual representation of
a single sensor item in the application. It is an AXML file
that defines the layout, controls, and data bindings required
to display the sensor item information.

The SensorItemViewModel is associated with this view to 
provide the necessary data and logic from the backend
for interacting with the sensor item.

### MainWindow
The MainWindow is the main screen of the application that
displays a list of all the sensors. It is also an AXML file
that defines the overall layout and structure of the main
window.

The MainWindowViewModel is associated with this view and
serves as the intermediary between the UI and the backend
logic. It provides the necessary functionality to load the
sensors, perform search operations and handle user
interactions.

### CreateSensor
The CreateSensorView is responsible for capturing user input
to create a new sensor item. It allows users to enter the
name, category and amount for the new sensor.

This view is associated with the CreateSensorViewModel, wich
handles the creation of the sensor item and updates the
backend accordingly. It also includes input controls and
validation logic to ensure the entered data is valid before
creating the sensor. 

## CRUD Functionality
The CRUD operations are essential for managing the sensor 
items in the application. These operatoins involve both the
Repository implementation on the backend and the bindings
and validations made in the Views and ViewModels.

### Repository Implementations
It interacts with the DBContext to perform the necessary 
database operations.
This section will details each CRUD functionality
implementation.

#### Read
- The GetAllSensors method retrieves all the sensor items from 
the database. It uses the SensorDbContext to query the Sensors
table and returns the result as a list of Sensor objects.

- The GetSensorByName method takes a name parameter and 
retrieves the sensor items whose names contain the specified
value. It performs a LINQ query on the Sensors table using 
the SensorDbContext and returns the matching sensor items.

- The GetSensorByCategory method takes a category parameter and 
retrieves the sensor items that belong to the specified 
category. It filters the Sensors table using a LINQ query 
and returns the matching sensor items.

#### Create
The AddSensor method is responsible for creating a new sensor
item and adding it to the database. It takes a Sensor object
as a parameter and performs the following steps:

- Checks if the name or category of the sensor is null or 
empty. If either of them is, an ArgumentException is thrown.

- Validates that the amount of the sensor is not negative.
If it is negative, an ArgumentOutOfRangeException is thrown.

- Checks if a sensor with the same name already exists in the
database. If a duplicate name is found, an ArgumentException 
is thrown.

If all validations pass, the sensor is added to the Sensors 
table using the SensorDbContext, and the changes are saved to
the database.

#### Update
The UpdateSensor method is responsible for updating an 
existing sensor item in the database. It takes a Sensor 
object as a parameter and performs the following steps:

- Checks if the name or category of the sensor is null or 
empty. If either of them is, an ArgumentException is thrown.

- Updates the corresponding sensor object in the Sensors
table using the SensorDbContext and saves the changes to 
the database.

#### Delete
The DeleteSensor method is responsible for deleting a sensor 
item from the database. It takes a Sensor object as a 
parameter and performs the following steps:

- Removes the sensor object from the Sensors table using the
SensorDbContext and saves the changes to the database.

### Bindings and Validations
- In the MainWindowView, the sensor items are displayed in 
a ListBox. The ItemsSource property of the ListBox is bound
to a collection of sensors in the MainWindowViewModel. This
binding ensures that any changes in the sensor collection are
reflected in the ListBox.

- The MainWindowView also contains a search TextBox and to
filter the sensor items. The Text property of the TextBox 
is bound to the SearchName property in the MainWindowViewModel,
allowing real-time updates as the user types.

- The CreateSensorView contains input fields for the name,
category, and amount of the sensor. The Text properties of 
the input fields are bound to corresponding properties in
the CreateSensorViewModel, enabling two-way data binding 
between the user input and the ViewModel. The CreateSensorView
also includes a Save button, which triggers the creation of 
a new sensor item by invoking a command in the 
CreateSensorViewModel.

- In the SensorItemView, the Delete button is bound to a
command in the SensorItemViewModel, which initiates the 
deletion of the corresponding sensor item from the database.

Furthermore, in the CreateSensorViewModel, there are
validation checks to ensure that the name and category
fields are not null or empty. If these fields are not valid,
appropriate error messages are displayed in the View
to provide feedback to the user.

By establishing these bindings and validations between
the frontend and backend components, we ensure integrity
and consistency of the data managed by the application.

## Conclusion
In this project, I have developed a desktop application
using C# and Avalonia to create a sensor cataloging
system. The application allows me to manage my sensors
by storing them in a database and providing a
user-friendly interface to view, search, create, update,
and delete items.

By following the MVVM architecture pattern, I separated
the concerns of the backend and frontend components, 
promoting code organization and maintainability. 

The backend utilizes Entity Framework Core and a repository
pattern to handle database operations, while the frontend
employs Avalonia Views and ViewModels to provide a user
interface and easy data binding.

Throughout the development process, I tried to adhere best
practices such as using appropriate design patterns, 
following coding conventions, and incorporating error 
handling and data validation techniques.

Although this project focused on a specific use case of 
cataloging sensors, the principles and techniques employed 
can be applied to other similar applications and serve as a
foundation for further learning and development. 

By embracing good coding practices and continuously 
enhancing our programming skills, we can strive towards
becoming proficient software developers capable of tackling 
more complex projects.

Overall, this project has provided me with hands-on
experience in C# programming, database integration,
UI design and application development using the MVVM
pattern.

# Next Steps
Having completed this sensor cataloging project, I will 
continue working on the project ideas generated by ChatGTP,
listed below:

## Languages

- [C#](#c-sharp)
- [Java](#java)
- [Python](#python)
- [C](#c)
- [C++](#cpp)
- [Rust](#rust)

___
## C Sharp
___
#### (+) Easy: Sensor Cataloging Application
- Description:
- - Create a sensor list application with a 
user-friendly interface that allows users to manage their
electronic sensors.
- - The program should provide features such
as sensor registration, searching and filtering by name 
or category, updating sensor information, and deleting 
sensors from the list.

- Milestones: Implement GUI using Avalonia Framework, 
sensor management functionality, search and filter 
capabilities, update and delete operations.

- Requirements: .NET framework, Avalonia Framework, 
SQLite or MariaDB for local database storage.
___
#### Intermediate: Weather Forecast Application
- Description: 
- - Build a desktop application that displays 
weather forecasts for different locations.
- - The application
should fetch weather data from an API (e.g., OpenWeatherMap)
based on user input and present it in a user-friendly manner.

- Milestones: Implement API integration, user input 
handling, weather data visualization, error handling.

- Requirements: .NET framework, JSON parsing library
(e.g., Newtonsoft.Json), GUI library (Windows Forms, WPF).

___
#### Hard: Stock Trading Platform
- Description: 
- - Develop a stock trading platform with a GUI
that allows users to buy and sell stocks. 
- - The program should 
provide real-time stock data, trading functionalities,
portfolio management, and transaction history.
- - It should 
integrate with a stock market API for fetching live data.

- Milestones: Implement GUI using Windows Forms or WPF,
stock data integration, trading functionalities, portfolio
management, transaction history.

- Requirements: .NET framework with Windows Forms or WPF,
stock market API integration.

___
## Java
___
#### Easy: Student Management System
- Description:
- - Develop a student management system with a graphical user 
interface (GUI) and database integration.

- - The program should allow users to add, view, update,
and delete student records.

- - Student information, such as name, ID, and grades, should
be stored in a database for persistence.

- Milestones:
- - Design the GUI layout for displaying student records and
capturing user input.

- - Set up a database (e.g., MySQL, PostgreSQL) to store student
information.

- - Implement database connectivity to perform CRUD operations 
on student records.

- - Integrate the GUI with the database to fetch and display 
student records.

- - Implement functionality to add new students, update 
existing records, and delete student information
from the database.

- Requirements:
- - Java libraries/frameworks for GUI development

- - Java Database Connectivity (JDBC) for database integration.
- - Database software (e.g., MySQL, PostgreSQL) and relevant
driver libraries.

___
#### Intermediate: Online Bookstore
- Description:
- - Build a web-based bookstore application that allows users
to browse and purchase books.

- - The application should have features like user registration,
book search, shopping cart, and order tracking.

- - Persistence can be achieved using a database like MySQL 
or PostgreSQL.

- Milestones: Implement user registration/authentication,
book catalog, shopping cart, database integration,
web interface.

- Requirements: Java libraries/frameworks like Spring Boot,
Hibernate, and a database.

___
#### Hard: Chat Application
- Description:
- - Develop a real-time chat application that enables users 
to exchange messages.
- - The application should support features like user 
registration, private/group messaging, online/offline status,
and message history.
- - Communication can be implemented using sockets or a
messaging protocol like MQTT.

- Milestones: Implement user registration/authentication, 
message exchange, online/offline status, message history, GUI.

- Requirements: Java libraries/frameworks like JavaFX 
and socket programming or a messaging protocol library.

___
## Python
___
#### Easy: URL Shortener
- Description: 
- - Build a URL shortening service that takes a long URL
as input and generates a short URL. 
- - The program should be able to redirect users from the
short URL to the original long URL.
- - Additionally, provide a basic web interface to input
long URLs and display the corresponding short URLs.

- Milestones: Implement URL shortening algorithm, database
storage, URL redirection, web interface (optional).

- Requirements: Python libraries like Flask or Django,
database (e.g., SQLite, PostgreSQL).

___
#### Intermediate: Movie Recommendation System
- Description: 
- - Develop a movie recommendation system with a GUI that
suggests movies to users based on their preferences.
- - The program should utilize machine learning techniques 
to analyze user ratings and recommend similar movies.

- - It should provide a user-friendly interface for inputting
preferences and displaying movie recommendations.

- Milestones: Implement GUI using a library like Tkinter or 
PyQt, movie database integration, user rating input, 
machine learning model training, movie recommendation 
algorithm, result display.

- Requirements: Python libraries for GUI, machine learning
libraries, movie database (e.g., IMDb dataset).


___
#### Hard: Social Media Analytics Dashboard
- Description:
- - Develop a social media analytics dashboard with a 
GUI that fetches and displays data from social media 
APIs (e.g., Twitter, Instagram). 
- - The program should provide features like post
analytics, user engagement metrics, sentiment analysis, 
and visualizations of social media data.

- Milestones: Implement GUI using a library like Tkinter
or PyQt, social media API integration, data fetching,
analytics algorithms, visualizations.

- Requirements: Python libraries for GUI, social media
API libraries (e.g., Tweepy, InstagramAPI), data 
visualization libraries (e.g., Matplotlib, Plotly).


___
## C
___
#### Easy: Simple Text Editor
- Description: 
- - Develop a basic text editor with a graphical user
interface.
- - The editor should allow users to create, open, edit,
and save text files. It should provide features like
text formatting (e.g., bold, italic), search/replace
functionality, and word count.

- Milestones: Implement GUI using a library like GTK or SDL, 
file handling, text editing features.

- Requirements: C libraries for GUI (e.g., GTK, SDL).


___
#### Intermediate: Image Viewer and Editor
- Description:
- - Create an image viewer and editor application.
- - Users should be able to open and display image files, 
apply basic image editing operations (e.g., crop, resize,
rotate), and save modified images.

- Milestones: Implement GUI using a library like SDL or GTK,
image file handling, image editing features.

- Requirements: C libraries for GUI, image processing 
libraries (optional).


___
#### Hard: 2D Platformer Game Development
- Description:
- - Develop a 2D platformer game with a graphical user
interface. 
- - The game should have features like player movement, 
collision detection, enemy AI, power-ups, and multiple levels.

- Milestones: Implement game logic, graphics rendering, 
user input handling, collision detection, level design.

- Requirements: C libraries for GUI (e.g., SDL), game 
development libraries (e.g., SDL, Allegro).


___
## CPP
___
#### Easy: Calculator Application
- Description:
- - Develop a calculator application with a GUI that allows
users to perform basic arithmetic operations 
(addition, subtraction, multiplication, division). 
- - The program should display the input/output fields and 
provide buttons for number input and operation selection.

- Milestones: Implement GUI using a library like Qt or 
wxWidgets, arithmetic operations, input/output handling.

- Requirements: C++ libraries for GUI (e.g., Qt, wxWidgets).

- Extra: Calculus operations, graphing calculator
functionalities.


___
#### Intermediate: Media Player
- Description: 
- - Build a media player application that can play audio
and video files. 
- - The player should have a GUI with playback controls
(play, pause, stop), volume adjustment, playlist management,
and support for common media formats.

- Milestones: Implement GUI using a library like SFML, Qt, 
or SDL, media playback, playlist management, volume control.

- Requirements: C++ libraries for GUI (e.g., SFML, Qt, SDL),
multimedia libraries (e.g., FFmpeg).


___
#### Hard: 3D Game Engine
- Description: 
- - Develop a 3D game engine with a GUI that supports
rendering 3D models, handling user input, and managing 
game states. 
- - The engine should provide a framework for creating 
interactive 3D games with features like physics simulation,
lighting, and sound.

- Milestones: Implement GUI using a library like OpenGL, 
DirectX, or Unity, 3D model rendering, input handling, 
physics simulation, sound integration.

- Requirements: C++ libraries for GUI (e.g., OpenGL, 
DirectX), 3D graphics libraries (e.g., OpenGL, DirectX).


___
## Rust
___
#### Easy: Password Generator
- Description: 
- - Create a command-line tool that generates random 
passwords based on user-defined criteria
(e.g., length, character set). 
- - The program should output the generated password to 
the console.

- Milestones: Implement password generation, user input
handling, output formatting.

- Requirements: Standard Rust libraries.


___
#### Intermediate: Password Manager
- Description: 
- - Build a password manager application that securely
stores and manages user passwords. 
- - The program should have a GUI for adding, retrieving, 
and updating passwords. 
- - It should also provide features like password generation 
and encryption.

- Milestones: Implement GUI using a library like Qt or Iced,
password storage, retrieval, encryption, password generation.

- Requirements: Rust libraries for GUI (e.g., Qt, Iced),
encryption libraries (e.g., libsodium).


___
#### Intermediate: Web Scraping Tool
- Description: 
- - Build a web scraping tool that extracts data from a
given website.
- - The program should fetch the HTML content, parse it,
and extract specific information based on user-defined 
rules (e.g., CSS selectors). 
- - The extracted data should be saved to a file or 
displayed on the console.

- Milestones: Implement HTTP requests, HTML parsing, 
data extraction, command-line interface.

- Requirements: Rust libraries for HTTP requests 
(e.g., reqwest) and HTML parsing (e.g., select.rs).


___
#### Hard: Game of Life Simulation
- Description: 
- - Develop a graphical simulation of Conway's Game 
of Life.
- - The program should display a grid of cells and animate
their evolution based on specific rules.
- - Users should be able to interact with the simulation, 
change the initial cell configuration, and control the
simulation speed.

- Milestones: Implement GUI using a library like Amethyst
or Piston, Game of Life rules implementation, 
user interaction, simulation controls.

- Requirements: Rust libraries for GUI.
