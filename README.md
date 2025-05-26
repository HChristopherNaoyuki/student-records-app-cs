# Student Records App

## Overview

A Windows Presentation Foundation (WPF) application for managing student records with XML data persistence. This application allows users to view, add, and edit student information with proper data validation.

## Features

- **Student Management**:
  - View list of all students
  - Add new students with auto-generated IDs
  - Edit existing student details
  - Intuitive form-based interface

- **Data Persistence**:
  - Automatically saves to `stds.xml` file
  - Loads existing data on startup
  - XML-based storage for easy data portability

- **Validation**:
  - ID must be unique and positive
  - Name cannot be empty
  - Age must be between 5-120
  - Grade cannot be empty
  - Real-time error feedback

## System Requirements

- .NET Framework 4.7.2 or later
- Windows 7 SP1 or newer
- 50MB disk space

## Installation

1. **Download the Application**:
   - Clone the repository or download the release package
   ```
   git clone https://github.com/your-repo/student-records-app-cs.git
   ```

2. **Build the Solution**:
   - Open `student-records-app-cs.sln` in Visual Studio 2019 or later
   - Build the solution (Ctrl+Shift+B)

3. **Run the Application**:
   - Press F5 to run in debug mode
   - Or run the compiled executable from `bin/Release`

## Usage

### Basic Operations

1. **Viewing Students**:
   - All students appear in the left panel list
   - Select a student to view their details

2. **Adding a Student**:
   - Click "Add New Student" button
   - A new student with default values will be created
   - Edit the details and click "Save Changes"

3. **Editing a Student**:
   - Select a student from the list
   - Modify any field (including ID)
   - Click "Save Changes" to persist edits

4. **Data Persistence**:
   - Changes are automatically saved to `stds.xml`
   - File is created in the application directory

### Data File Location

The student records are stored in:
```
[Application Folder]\stds.xml
```

Sample XML structure:
```xml
<Students>
  <Student Id="1">
    <Name>John Smith</Name>
    <Age>20</Age>
    <Grade>A</Grade>
  </Student>
</Students>
```

## Troubleshooting

### Common Issues

1. **File Access Errors**:
   - Ensure the application has write permissions in its directory
   - Check if `stds.xml` isn't open in another program

2. **Validation Errors**:
   - ID must be unique and positive
   - Name cannot be blank
   - Age must be 5-120
   - Grade cannot be blank

3. **Corrupted Data**:
   - Delete `stds.xml` to reset the application
   - A new file will be created on next launch

### Debugging

- Check the application logs in:
  ```
  [Application Folder]\logs\
  ```
- For Visual Studio debugging:
  - Set breakpoints in `MainWindow.xaml.cs`
  - Watch the `_students` collection during runtime

## Technical Details

### Project Structure

```
student-records-app-cs/
├── MainWindow.xaml        # Main UI definition
├── MainWindow.xaml.cs     # UI logic and event handlers
├── Student.cs             # Student model class
├── StudentDataService.cs  # Data persistence logic
├── App.xaml               # Application resources
└── App.xaml.cs            # Application entry point
```

### Dependencies

- .NET Framework 4.7.2
- System.Xml.Serialization

### Data Flow

1. **Startup**:
   - Application loads `stds.xml` into memory
   - Binds student list to UI

2. **User Actions**:
   - Add/Edit operations modify in-memory collection
   - Changes are validated before saving

3. **Persistence**:
   - Entire collection is serialized to XML on save
   - File is overwritten with complete dataset

---
