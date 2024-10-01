using System;
using System.Collections.Generic;
using College_Website_Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace College_Website_Admin.Data;

public partial class CollegeWebdbContext : DbContext
{
    public CollegeWebdbContext()
    {
    }

    public CollegeWebdbContext(DbContextOptions<CollegeWebdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdmissionForm> AdmissionForms { get; set; }

    public virtual DbSet<AdmissionStatus> AdmissionStatuses { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseSelection> CourseSelections { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Facility> Facilities { get; set; }

    public virtual DbSet<Faculty> Faculties { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<PreviousEducation> PreviousEducations { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserRole> UserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Initial Catalog=CollegeWebdb;Persist Security Info=False;User ID=shehriyar;Password=asdf1234;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AdmissionForm>(entity =>
        {
            entity.HasKey(e => e.FormId).HasName("PK__ADMISSIO__85052F687FC78AA7");

            entity.ToTable("ADMISSION_FORMS");

            entity.Property(e => e.FormId).HasColumnName("FORM_ID");
            entity.Property(e => e.AdmissionField)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADMISSION_FIELD");
            entity.Property(e => e.AdmissionStream)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("ADMISSION_STREAM");
            entity.Property(e => e.AdmissionUserId).HasColumnName("ADMISSION_USER_ID");
            entity.Property(e => e.ApplicationStatus)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("Pending")
                .HasColumnName("APPLICATION_STATUS");
            entity.Property(e => e.ClassObtained)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CLASS_OBTAINED");
            entity.Property(e => e.CourseId).HasColumnName("COURSE_ID");
            entity.Property(e => e.MarksObtained).HasColumnName("MARKS_OBTAINED");
            entity.Property(e => e.PreviousCenter)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PREVIOUS_CENTER");
            entity.Property(e => e.PreviousEnrollmentNo)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("PREVIOUS_ENROLLMENT_NO");
            entity.Property(e => e.PreviousField)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PREVIOUS_FIELD");
            entity.Property(e => e.PreviousStream)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PREVIOUS_STREAM");
            entity.Property(e => e.PreviousUniversity)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("PREVIOUS_UNIVERSITY");
            entity.Property(e => e.SportsDetails)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("SPORTS_DETAILS");
            entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");
            entity.Property(e => e.SubmissionDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("SUBMISSION_DATE");
            entity.Property(e => e.TotalMarks).HasColumnName("TOTAL_MARKS");

            entity.HasOne(d => d.AdmissionUser).WithMany(p => p.AdmissionForms)
                .HasForeignKey(d => d.AdmissionUserId)
                .HasConstraintName("FK__ADMISSION__ADMIS__02FC7413");

            entity.HasOne(d => d.Course).WithMany(p => p.AdmissionForms)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__ADMISSION__COURS__00200768");

            entity.HasOne(d => d.Student).WithMany(p => p.AdmissionForms)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__ADMISSION__STUDE__7F2BE32F");
        });

        modelBuilder.Entity<AdmissionStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId).HasName("PK__ADMISSIO__D8827E7113A70213");

            entity.ToTable("ADMISSION_STATUS");

            entity.Property(e => e.StatusId).HasColumnName("STATUS_ID");
            entity.Property(e => e.AdmissionUserId).HasColumnName("ADMISSION_USER_ID");
            entity.Property(e => e.FormId).HasColumnName("FORM_ID");
            entity.Property(e => e.StatusDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("STATUS_DATE");
            entity.Property(e => e.StatusMessage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("STATUS_MESSAGE");
            entity.Property(e => e.StatusUpdate)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("STATUS_UPDATE");

            entity.HasOne(d => d.AdmissionUser).WithMany(p => p.AdmissionStatuses)
                .HasForeignKey(d => d.AdmissionUserId)
                .HasConstraintName("FK__ADMISSION__ADMIS__07C12930");

            entity.HasOne(d => d.Form).WithMany(p => p.AdmissionStatuses)
                .HasForeignKey(d => d.FormId)
                .HasConstraintName("FK__ADMISSION__FORM___05D8E0BE");
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.HasKey(e => e.ContId).HasName("PK__CONTACT__33DC4C7946154CBC");

            entity.ToTable("CONTACT");

            entity.Property(e => e.ContId).HasColumnName("CONT_ID");
            entity.Property(e => e.ContAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CONT_ADDRESS");
            entity.Property(e => e.ContEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CONT_EMAIL");
            entity.Property(e => e.ContMessage)
                .HasColumnType("text")
                .HasColumnName("CONT_MESSAGE");
            entity.Property(e => e.ContName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("CONT_NAME");
            entity.Property(e => e.ContPhone)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("CONT_PHONE");
            entity.Property(e => e.ContUserId).HasColumnName("CONT_USER_ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");

            entity.HasOne(d => d.ContUser).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.ContUserId)
                .HasConstraintName("FK__CONTACT__CONT_US__44FF419A");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__COURSES__71CB31DBB9AB0665");

            entity.ToTable("COURSES");

            entity.Property(e => e.CourseId).HasColumnName("COURSE_ID");
            entity.Property(e => e.CourseCredits).HasColumnName("COURSE_CREDITS");
            entity.Property(e => e.CourseDescription)
                .IsUnicode(false)
                .HasColumnName("COURSE_DESCRIPTION");
            entity.Property(e => e.CourseDuration)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("COURSE_DURATION");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .HasColumnName("COURSE_NAME");
            entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");

            entity.HasOne(d => d.Department).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__COURSES__DEPARTM__66603565");
        });

        modelBuilder.Entity<CourseSelection>(entity =>
        {
            entity.HasKey(e => e.SelectionId).HasName("PK__COURSE_S__FECF050E2C0EF63D");

            entity.ToTable("COURSE_SELECTION");

            entity.Property(e => e.SelectionId).HasColumnName("SELECTION_ID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CREATED_AT");
            entity.Property(e => e.FormId).HasColumnName("FORM_ID");
            entity.Property(e => e.OptionalSubject)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("OPTIONAL_SUBJECT");
            entity.Property(e => e.SelectedSubject)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("SELECTED_SUBJECT");

            entity.HasOne(d => d.Form).WithMany(p => p.CourseSelections)
                .HasForeignKey(d => d.FormId)
                .HasConstraintName("FK__COURSE_SE__FORM___160F4887");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Departmentid).HasName("PK__DEPARTME__EC3B6F4FA79B2A3D");

            entity.ToTable("DEPARTMENTS");

            entity.Property(e => e.Departmentid).HasColumnName("DEPARTMENTID");
            entity.Property(e => e.Datesubmitted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATESUBMITTED");
            entity.Property(e => e.Departmenthead)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DEPARTMENTHEAD");
            entity.Property(e => e.Departmentname)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("DEPARTMENTNAME");
        });

        modelBuilder.Entity<Facility>(entity =>
        {
            entity.HasKey(e => e.FacilityId).HasName("PK__FACILITI__A9B6EEB94BD8507B");

            entity.ToTable("FACILITIES");

            entity.Property(e => e.FacilityId).HasColumnName("FACILITY_ID");
            entity.Property(e => e.FacilityAvailablefrom).HasColumnName("FACILITY_AVAILABLEFROM");
            entity.Property(e => e.FacilityDepartId).HasColumnName("FACILITY_DEPART_ID");
            entity.Property(e => e.FacilityDescription)
                .IsUnicode(false)
                .HasColumnName("FACILITY_DESCRIPTION");
            entity.Property(e => e.FacilityName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FACILITY_NAME");

            entity.HasOne(d => d.FacilityDepart).WithMany(p => p.Facilities)
                .HasForeignKey(d => d.FacilityDepartId)
                .HasConstraintName("FK__FACILITIE__FACIL__5441852A");
        });

        modelBuilder.Entity<Faculty>(entity =>
        {
            entity.HasKey(e => e.FacultyId).HasName("PK__FACULTY__6755FF5BBAE56D2A");

            entity.ToTable("FACULTY");

            entity.Property(e => e.FacultyId).HasColumnName("FACULTY_ID");
            entity.Property(e => e.Datesubmitted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATESUBMITTED");
            entity.Property(e => e.DepartmentId).HasColumnName("DEPARTMENT_ID");
            entity.Property(e => e.FacultyImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("FACULTY_IMAGE");
            entity.Property(e => e.FacultyName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FACULTY_NAME");

            entity.HasOne(d => d.Department).WithMany(p => p.Faculties)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__FACULTY__DEPARTM__628FA481");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Feedbackid).HasName("PK__FEEDBACK__7074B31E7328D231");

            entity.ToTable("FEEDBACK");

            entity.Property(e => e.Feedbackid).HasColumnName("FEEDBACKID");
            entity.Property(e => e.Datesubmitted)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("DATESUBMITTED");
            entity.Property(e => e.FeedbackUserId).HasColumnName("FEEDBACK_USER_ID");
            entity.Property(e => e.Feedbacktext)
                .IsUnicode(false)
                .HasColumnName("FEEDBACKTEXT");

            entity.HasOne(d => d.FeedbackUser).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.FeedbackUserId)
                .HasConstraintName("FK__FEEDBACK__FEEDBA__48CFD27E");
        });

        modelBuilder.Entity<PreviousEducation>(entity =>
        {
            entity.HasKey(e => e.EducationId).HasName("PK__PREVIOUS__DA1AD9B1F08455B8");

            entity.ToTable("PREVIOUS_EDUCATION");

            entity.Property(e => e.EducationId).HasColumnName("EDUCATION_ID");
            entity.Property(e => e.AdmissionUserId).HasColumnName("ADMISSION_USER_ID");
            entity.Property(e => e.ClassObtained)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("CLASS_OBTAINED");
            entity.Property(e => e.ExamName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("EXAM_NAME");
            entity.Property(e => e.InstituteName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("INSTITUTE_NAME");
            entity.Property(e => e.MarksObtained).HasColumnName("MARKS_OBTAINED");
            entity.Property(e => e.PassingYear).HasColumnName("PASSING_YEAR");
            entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");
            entity.Property(e => e.TotalMarks).HasColumnName("TOTAL_MARKS");
            entity.Property(e => e.UniversityName)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("UNIVERSITY_NAME");

            entity.HasOne(d => d.AdmissionUser).WithMany(p => p.PreviousEducations)
                .HasForeignKey(d => d.AdmissionUserId)
                .HasConstraintName("FK__PREVIOUS___ADMIS__0B91BA14");

            entity.HasOne(d => d.Student).WithMany(p => p.PreviousEducations)
                .HasForeignKey(d => d.StudentId)
                .HasConstraintName("FK__PREVIOUS___STUDE__0A9D95DB");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__STUDENTS__E69FE77B6EE993B3");

            entity.ToTable("STUDENTS");

            entity.HasIndex(e => e.StudentsEmail, "UQ__STUDENTS__976E3A953E99EE8E").IsUnique();

            entity.Property(e => e.StudentId).HasColumnName("STUDENT_ID");
            entity.Property(e => e.DateOfBirth).HasColumnName("DATE_OF_BIRTH");
            entity.Property(e => e.FatherName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("FATHER_NAME");
            entity.Property(e => e.MotherName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("MOTHER_NAME");
            entity.Property(e => e.PermanentAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("PERMANENT_ADDRESS");
            entity.Property(e => e.RegistrationDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("REGISTRATION_DATE");
            entity.Property(e => e.ResidentialAddress)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("RESIDENTIAL_ADDRESS");
            entity.Property(e => e.StudentUserId).HasColumnName("STUDENT_USER_ID");
            entity.Property(e => e.StudentsContactNumber)
                .HasMaxLength(15)
                .IsUnicode(false)
                .HasColumnName("STUDENTS_CONTACT_NUMBER");
            entity.Property(e => e.StudentsEmail)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("STUDENTS_EMAIL");
            entity.Property(e => e.StudentsGender)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("STUDENTS_GENDER");
            entity.Property(e => e.StudentsName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("STUDENTS_NAME");

            entity.HasOne(d => d.StudentUser).WithMany(p => p.Students)
                .HasForeignKey(d => d.StudentUserId)
                .HasConstraintName("FK__STUDENTS__STUDEN__7C4F7684");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__USERS__F3BEEBFFD5BE1863");

            entity.ToTable("USERS");

            entity.HasIndex(e => e.UserEmail, "UQ__USERS__43CA3168C8E7EE54").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("USER_ID");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("USER_EMAIL");
            entity.Property(e => e.UserImage)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("USER_IMAGE");
            entity.Property(e => e.UserName)
                .HasMaxLength(75)
                .IsUnicode(false)
                .HasColumnName("USER_NAME");
            entity.Property(e => e.UserPassword)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("USER_PASSWORD");
            entity.Property(e => e.UserRoleId).HasColumnName("USER_ROLE_ID");

            entity.HasOne(d => d.UserRole).WithMany(p => p.Users)
                .HasForeignKey(d => d.UserRoleId)
                .HasConstraintName("FK__USERS__USER_ROLE__3A81B327");
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__USER_ROL__5AC4D222A3B2033F");

            entity.ToTable("USER_ROLE");

            entity.Property(e => e.RoleId).HasColumnName("ROLE_ID");
            entity.Property(e => e.RoleName)
                .HasMaxLength(55)
                .IsUnicode(false)
                .HasColumnName("ROLE_NAME");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
