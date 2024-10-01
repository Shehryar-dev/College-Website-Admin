namespace College_Website_Admin.Models
{
    public class IndexRecords
    {
        public IEnumerable<Student> students { get; set; }
        public IEnumerable<User> users { get; set; }
        public IEnumerable<Course> courses { get; set; }
        public IEnumerable<Department> departments { get; set; }
        public IEnumerable<Contact> contacts { get; set; }


        public IEnumerable<Feedback> feedbacks { get; set; }

        public IEnumerable<Facility> facilities { get; set; }


        public IEnumerable<Faculty> faculties { get; set; }
        public IEnumerable<AdmissionForm> admissions { get; set; }


    }
}
