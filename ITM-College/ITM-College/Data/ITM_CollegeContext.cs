using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ITM_College.Models;

namespace ITM_College.Data
{
    public partial class ITM_CollegeContext : DbContext
    {
        public ITM_CollegeContext()
        {
        }

        public ITM_CollegeContext(DbContextOptions<ITM_CollegeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Admin> Admins { get; set; } = null!;
        public virtual DbSet<Contact> Contacts { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<Department> Departments { get; set; } = null!;
        public virtual DbSet<Facility> Facilities { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<PreviousExam> PreviousExams { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentCourseRegistration> StudentCourseRegistrations { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Admin>(entity =>
            {
                entity.ToTable("Admin");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AdminEmail)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("adminEmail");

                entity.Property(e => e.AdminImg).IsUnicode(false);

                entity.Property(e => e.AdminName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("adminName");

                entity.Property(e => e.Password)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role).HasColumnName("role");
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.ToTable("Contact");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Message)
                    .HasColumnType("text")
                    .HasColumnName("message");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("userEmail");

                entity.Property(e => e.UserName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("userName");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.Property(e => e.CourseId).HasColumnName("courseID");

                entity.Property(e => e.CourseDesc)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("courseDesc");

                entity.Property(e => e.CourseDuration).HasColumnName("courseDuration");

                entity.Property(e => e.CourseImg)
                    .IsUnicode(false)
                    .HasColumnName("courseImg");

                entity.Property(e => e.CourseName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("courseName");

                entity.Property(e => e.FacultyId).HasColumnName("facultyID");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Courses)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK__Courses__faculty__4222D4EF");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("Department");

                entity.Property(e => e.DepartmentId).HasColumnName("departmentID");

                entity.Property(e => e.DepartmentDesc)
                    .HasColumnType("text")
                    .HasColumnName("departmentDesc");

                entity.Property(e => e.DepartmentName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("departmentName");
            });

            modelBuilder.Entity<Facility>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.FacilityDesc)
                    .HasColumnType("text")
                    .HasColumnName("facilityDesc");

                entity.Property(e => e.FacilityImg)
                    .IsUnicode(false)
                    .HasColumnName("facilityImg");

                entity.Property(e => e.FacilityName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("facilityName");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("faculties");

                entity.Property(e => e.FacultyId).HasColumnName("facultyID");

                entity.Property(e => e.FacultyDepartment).HasColumnName("facultyDepartment");

                entity.Property(e => e.FacultyEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("facultyEmail");

                entity.Property(e => e.FacultyImg)
                    .IsUnicode(false)
                    .HasColumnName("facultyImg");

                entity.Property(e => e.FacultyName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("facultyName");

                entity.Property(e => e.FacultyPassword)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("facultyPassword");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.HasOne(d => d.FacultyDepartmentNavigation)
                    .WithMany(p => p.Faculties)
                    .HasForeignKey(d => d.FacultyDepartment)
                    .HasConstraintName("FK__faculties__facul__3F466844");
            });

            modelBuilder.Entity<PreviousExam>(entity =>
            {
                entity.HasKey(e => e.ExamId)
                    .HasName("PK__Previous__A56D123FDE5C2BE6");

                entity.ToTable("PreviousExam");

                entity.Property(e => e.ExamId).HasColumnName("examID");

                entity.Property(e => e.Center)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("center");

                entity.Property(e => e.ClassObtained).HasColumnName("classObtained");

                entity.Property(e => e.EnrollmentNumber).HasColumnName("enrollmentNumber");

                entity.Property(e => e.Field)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("field");

                entity.Property(e => e.InstituteName)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("instituteName");

                entity.Property(e => e.Marks).HasColumnName("marks");

                entity.Property(e => e.OutOf).HasColumnName("outOf");

                entity.Property(e => e.Sports)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("sports");

                entity.Property(e => e.Stream)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("stream");

                entity.Property(e => e.StudentDataId).HasColumnName("studentDataID");

                entity.HasOne(d => d.StudentData)
                    .WithMany(p => p.PreviousExams)
                    .HasForeignKey(d => d.StudentDataId)
                    .HasConstraintName("FK__PreviousE__stude__4AB81AF0");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.StudentId).HasColumnName("studentID");

                entity.Property(e => e.Password)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role).HasColumnName("role");

                entity.Property(e => e.StdImg).IsUnicode(false);

                entity.Property(e => e.StudentEmail)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("studentEmail");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("studentName");
            });

            modelBuilder.Entity<StudentCourseRegistration>(entity =>
            {
                entity.ToTable("StudentCourseRegistration");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.AddmissionFor).HasColumnName("addmissionFor");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.FatherName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("fatherName");

                entity.Property(e => e.Gender)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.MotherName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("motherName");

                entity.Property(e => e.PermanentAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("permanentAddress");

                entity.Property(e => e.ResidentalAddress)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("residentalAddress");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.StudentId).HasColumnName("studentID");

                entity.Property(e => e.StudentName)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("studentName");

                entity.Property(e => e.TrackingId)
                    .HasMaxLength(55)
                    .IsUnicode(false)
                    .HasColumnName("trackingID");

                entity.HasOne(d => d.AddmissionForNavigation)
                    .WithMany(p => p.StudentCourseRegistrations)
                    .HasForeignKey(d => d.AddmissionFor)
                    .HasConstraintName("FK__StudentCo__addmi__47DBAE45");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.StudentCourseRegistrations)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__StudentCo__stude__46E78A0C");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
