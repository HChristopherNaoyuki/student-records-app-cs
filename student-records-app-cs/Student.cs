using System.Xml.Serialization;

namespace student_records_app_cs
{
    /// <summary>
    /// Represents a student entity with properties for ID, name, age, and grade.
    /// Uses XML serialization attributes for persistence.
    /// </summary>
    [XmlRoot("Student")]
    public class Student
    {
        // Backing fields for properties
        private int _id;
        private string _name;
        private int _age;
        private string _grade;

        /// <summary>
        /// Gets or sets the student's unique identifier.
        /// Validates that ID is a positive number.
        /// </summary>
        [XmlAttribute("Id")]
        public int Id
        {
            get { return _id; }
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Student ID must be a positive number");
                _id = value;
            }
        }

        /// <summary>
        /// Gets or sets the student's full name.
        /// Validates that name is not null or whitespace.
        /// </summary>
        [XmlElement("Name")]
        public string Name
        {
            get { return _name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Student name cannot be empty");
                _name = value.Trim();
            }
        }

        /// <summary>
        /// Gets or sets the student's age.
        /// Validates age is between 5 and 120 years.
        /// </summary>
        [XmlElement("Age")]
        public int Age
        {
            get { return _age; }
            set
            {
                if (value < 5 || value > 120)
                    throw new ArgumentException("Age must be between 5 and 120");
                _age = value;
            }
        }

        /// <summary>
        /// Gets or sets the student's grade.
        /// Validates that grade is not null or whitespace.
        /// </summary>
        [XmlElement("Grade")]
        public string Grade
        {
            get { return _grade; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Grade cannot be empty");
                _grade = value.Trim().ToUpper();
            }
        }

        /// <summary>
        /// Default constructor required for XML serialization.
        /// Initializes string properties to empty strings.
        /// </summary>
        public Student()
        {
            _name = string.Empty;
            _grade = string.Empty;
        }

        /// <summary>
        /// Parameterized constructor for creating a new student.
        /// </summary>
        /// <param name="id">Unique student identifier</param>
        /// <param name="name">Full name of student</param>
        /// <param name="age">Age of student</param>
        /// <param name="grade">Current grade of student</param>
        public Student(int id, string name, int age, string grade)
        {
            Id = id;
            Name = name;
            Age = age;
            Grade = grade;
        }

        /// <summary>
        /// Returns a string representation of the student.
        /// </summary>
        /// <returns>Formatted string with student details</returns>
        public override string ToString()
        {
            return $"{Name} (ID: {Id})";
        }
    }
}