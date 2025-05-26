using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace student_records_app_cs
{
    /// <summary>
    /// Handles all data persistence operations for student records.
    /// Manages loading and saving student data to an XML file.
    /// </summary>
    public class StudentDataService
    {
        // Path to the XML data file
        private const string XmlFilePath = "stds.xml";

        // XML serializer configured for List<Student>
        private readonly XmlSerializer _serializer;

        /// <summary>
        /// Initializes a new instance of the StudentDataService.
        /// Configures the XML serializer with proper root attribute.
        /// </summary>
        public StudentDataService()
        {
            _serializer = new XmlSerializer(typeof(List<Student>),
                new XmlRootAttribute("Students"));
        }

        /// <summary>
        /// Loads all students from the XML data file.
        /// </summary>
        /// <returns>List of Student objects</returns>
        /// <exception cref="ApplicationException">Thrown when loading fails</exception>
        public List<Student> LoadStudents()
        {
            // Return empty list if file doesn't exist
            if (!File.Exists(XmlFilePath))
                return new List<Student>();

            try
            {
                // Open file stream and deserialize XML data
                using (var stream = File.OpenRead(XmlFilePath))
                {
                    return (List<Student>)_serializer.Deserialize(stream);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to load student data", ex);
            }
        }

        /// <summary>
        /// Saves all students to the XML data file.
        /// </summary>
        /// <param name="students">List of Student objects to save</param>
        /// <exception cref="InvalidDataException">Thrown for invalid student data</exception>
        /// <exception cref="ApplicationException">Thrown when saving fails</exception>
        public void SaveStudents(List<Student> students)
        {
            // Validate student data before saving
            ValidateStudents(students);

            try
            {
                // Create file stream and serialize data to XML
                using (var stream = File.Create(XmlFilePath))
                {
                    _serializer.Serialize(stream, students);
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Failed to save student data", ex);
            }
        }

        /// <summary>
        /// Validates student data for integrity before saving.
        /// </summary>
        /// <param name="students">List of Student objects to validate</param>
        /// <exception cref="InvalidDataException">Thrown for invalid data</exception>
        private void ValidateStudents(List<Student> students)
        {
            // Check for duplicate student IDs
            var duplicateIds = students
                .GroupBy(s => s.Id)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateIds.Any())
            {
                throw new InvalidDataException(
                    $"Duplicate student IDs found: {string.Join(", ", duplicateIds)}");
            }
        }

        /// <summary>
        /// Generates the next available student ID.
        /// </summary>
        /// <param name="students">Current list of students</param>
        /// <returns>Next available ID number</returns>
        public int GetNextId(List<Student> students)
        {
            return students.Count > 0 ? students.Max(s => s.Id) + 1 : 1;
        }
    }
}