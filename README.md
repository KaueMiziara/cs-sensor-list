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


## Introduction
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
