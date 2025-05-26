using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace student_records_app_cs
{
    /// <summary>
    /// Interaction logic for the main application window.
    /// Handles UI events and coordinates between view and data layer.
    /// </summary>
    public partial class MainWindow : Window
    {
        // Service for data persistence operations
        private readonly StudentDataService _dataService;

        // Current list of students in memory
        private List<Student> _students;

        // Currently selected student in the UI
        private Student _currentStudent;

        /// <summary>
        /// Initializes the main window and loads student data.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            // Initialize data service
            _dataService = new StudentDataService();

            // Load student data from file
            LoadStudents();

            // Configure initial UI state
            ConfigureUI();
        }

        /// <summary>
        /// Loads student data from the data service and binds to UI.
        /// </summary>
        private void LoadStudents()
        {
            try
            {
                // Load students from data service
                _students = _dataService.LoadStudents();

                // Bind student list to ListBox
                StudentsListBox.ItemsSource = _students;
            }
            catch (Exception ex)
            {
                // Show error message if loading fails
                MessageBox.Show($"Error loading student data: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                // Initialize empty list if loading fails
                _students = new List<Student>();
            }
        }

        /// <summary>
        /// Configures initial UI settings and state.
        /// </summary>
        private void ConfigureUI()
        {
            // Make ID field editable with white background
            IdTextBox.IsReadOnly = false;
            IdTextBox.Background = SystemColors.WindowBrush;

            // Disable save button initially (no selection)
            SaveButton.IsEnabled = false;
        }

        /// <summary>
        /// Handles selection changes in the student list.
        /// </summary>
        private void StudentsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Get currently selected student
            _currentStudent = StudentsListBox.SelectedItem as Student;

            // Enable/disable save button based on selection
            SaveButton.IsEnabled = _currentStudent != null;

            if (_currentStudent != null)
            {
                // Display selected student's details in form
                IdTextBox.Text = _currentStudent.Id.ToString();
                NameTextBox.Text = _currentStudent.Name;
                AgeTextBox.Text = _currentStudent.Age.ToString();
                GradeTextBox.Text = _currentStudent.Grade;
            }
        }

        /// <summary>
        /// Handles click event for adding a new student.
        /// </summary>
        private void AddStudentButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Create new student with generated ID and default values
                var newStudent = new Student(
                    _dataService.GetNextId(_students),
                    "New Student",
                    18,
                    "A");

                // Add to in-memory list
                _students.Add(newStudent);

                // Refresh UI list
                RefreshList();

                // Select new student in list
                StudentsListBox.SelectedItem = newStudent;

                // Focus name field for editing
                NameTextBox.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to add new student: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Handles click event for saving student changes.
        /// </summary>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Return if no student selected
                if (_currentStudent == null) return;

                // Validate ID field
                if (!int.TryParse(IdTextBox.Text, out int newId) || newId <= 0)
                {
                    MessageBox.Show("Please enter a valid positive number for ID",
                        "Invalid ID", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Check for duplicate ID (if changed)
                if (_currentStudent.Id != newId && _students.Any(s => s.Id == newId))
                {
                    MessageBox.Show($"Student ID {newId} already exists. Please choose a different ID.",
                        "Duplicate ID", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Update student properties from form fields
                _currentStudent.Id = newId;
                _currentStudent.Name = NameTextBox.Text;
                _currentStudent.Age = int.Parse(AgeTextBox.Text);
                _currentStudent.Grade = GradeTextBox.Text;

                // Save changes to file
                _dataService.SaveStudents(_students);

                // Refresh UI list
                RefreshList();

                // Show success message
                MessageBox.Show("Student saved successfully!",
                    "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (FormatException)
            {
                MessageBox.Show("Please enter valid values for all fields",
                    "Invalid Data", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving student: {ex.Message}",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Refreshes the student list in the UI.
        /// </summary>
        private void RefreshList()
        {
            // Force ListBox to refresh by resetting ItemsSource
            StudentsListBox.ItemsSource = null;
            StudentsListBox.ItemsSource = _students;
        }
    }
}