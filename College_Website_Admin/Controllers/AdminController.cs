using College_Website_Admin.Data;
using College_Website_Admin.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Diagnostics;
using System.Security.Claims;

namespace College_Website_Admin.Controllers
{
    public class AdminController : Controller
    {

        private readonly CollegeWebdbContext db;

        public AdminController(CollegeWebdbContext db)
        {
            this.db = db;
        }



        //@@@@@@@@@@@@##@#@#@#@#@#@###########@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
        //Index
        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var allRecord = new IndexRecords
            {
               students = db.Students.Include(u => u.StudentUser).ToList(),
                contacts = db.Contacts.Include(c => c.ContUser).ToList(),
                feedbacks = db.Feedbacks.Include(c => c.FeedbackUser).ToList(),
                departments = db.Departments.ToList(),
                users = db.Users.Include(r => r.UserRole).ToList(),
                admissions = db.AdmissionForms.Include(a => a.AdmissionUser).Include(st => st.Student).Include(c => c.Course).ToList(),
                courses = db.Courses.Include(d => d.Department).ToList(),
                faculties = db.Faculties.Include(d => d.Department).ToList(),
                facilities = db.Facilities.Include(d => d.FacilityDepart).ToList(),


            };

            return View(allRecord);
        }

        //*********************************************************************************************************
        //Courses
        [Authorize(Roles = "Admin")]
        public IActionResult course_add()
        {
            ViewBag.Course = new SelectList(db.Departments, "Departmentid", "Departmentname");
            return View();
        }
        public IActionResult add_course(Course course) //insert
        {
            try
            {
                db.Add(course);
                db.SaveChanges();
                TempData["Success"] = "Course Record Insert SuccessFully";
                return RedirectToAction(nameof(course_show));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while inserting the record. Please try again later. " + ex.Message;
                ViewBag.Course = new SelectList(db.Departments, "Departmentid", "Departmentname");
                return View(course);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult course_show()
        {
            return View(db.Courses.Include("Department").ToList());
        }
        public IActionResult course_del(int? id) //delete
        {
            try
            {
                var delete = db.Courses.FirstOrDefault(d => d.CourseId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(course_show));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(course_show));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(course_show));
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult course_edit(int? id) //edit
        {
            try
            {
                var data = db.Courses.FirstOrDefault(c => c.CourseId == id);
                if (data == null)
                {
                    TempData["Error"] = "Course Record not found.";
                    return RedirectToAction(nameof(course_show));
                }
                ViewBag.Course = new SelectList(db.Departments, "Departmentid", "Departmentname");
                return View(data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the Course records for editing: " + ex.Message;
                return RedirectToAction(nameof(course_show));
            }

        }
        [Authorize(Roles = "Admin")]
        public IActionResult course_edit2(Course course) //update
        {
            try
            {
                var original = db.Courses.AsNoTracking().FirstOrDefault(c => c.CourseId == course.CourseId);
                if (original == null)
                {
                    TempData["Error"] = "Course Record not found.";
                    return RedirectToAction(nameof(course_show));
                }
                db.Update(course);
                db.SaveChanges();

                TempData["Success"] = "Course Records Updated Successfully!!";
                return RedirectToAction(nameof(course_show));
            }

            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the category: " + ex.Message;
                return RedirectToAction(nameof(course_show));
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //************************************************************************
        //Online Ragistration Records
        [Authorize(Roles = "Admin")]
        public IActionResult admission_records()
        {
            return View(db.AdmissionForms.Include(a => a.AdmissionUser).Include(st => st.Student).Include(c => c.Course).ToList()); ;
        }
        public IActionResult admission_del(int? id)
        {
            try
            {
                var delete = db.AdmissionForms.FirstOrDefault(d => d.FormId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(admission_records));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(admission_records));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(admission_records));
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult status_records()
        {
            return View(db.AdmissionStatuses.Include(f => f.Form).Include(au => au.AdmissionUser).Include(st => st.Form.Student).ToList()); ;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult status_del(int? id)
        {
            try
            {
                var delete = db.AdmissionStatuses.FirstOrDefault(d => d.StatusId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(status_records));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(status_records));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(status_records));
            }
        }




        //***********************************************************************
        //Departments
        [Authorize(Roles = "Admin")]

        public IActionResult department_add() { 
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult add_depart(Department department) //insert
        {
            try
            {
                db.Add(department);
                db.SaveChanges();
                TempData["Success"] = "Department Record Insert SuccessFully";
                return RedirectToAction(nameof(department_show));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while inserting the record. Please try again later. " + ex.Message;
                return View(department);
            }
        }
        //Read
        [Authorize(Roles = "Admin")]
        public IActionResult department_show()
        {
            return View(db.Departments.ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult depart_del(int? id) //delete
        {
            try
            {
                var delete = db.Departments.FirstOrDefault(d => d.Departmentid == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(department_show));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(department_show));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(department_show));
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult department_edit(int? id) //edit
        {
            try
            {
                var data = db.Departments.FirstOrDefault(dt => dt.Departmentid == id);
                if (data == null)
                {
                    TempData["Error"] = "Department Record not found.";
                    return RedirectToAction(nameof(department_show));
                }
                return View(data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the depart records for editing: " + ex.Message;
                return RedirectToAction(nameof(department_show));
            }

        }
        [Authorize(Roles = "Admin")]
        public IActionResult depart_edit2(Department department) //update
        {
            try
            {
                var originalDepart = db.Departments.AsNoTracking().FirstOrDefault(dt => dt.Departmentid == department.Departmentid);
                if (originalDepart == null)
                {
                    TempData["Error"] = "Department Record not found.";
                    return RedirectToAction(nameof(department_show));
                }

                department.Datesubmitted = originalDepart.Datesubmitted;

                db.Update(department);
                db.SaveChanges();

                TempData["Success"] = "Records Updated Successfully!!";
                return RedirectToAction(nameof(department_show));
            }

            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the category: " + ex.Message;
                return RedirectToAction(nameof(department_show));
            }
        }

        //**************************************************************************
        //Faculties 
        [Authorize(Roles = "Admin")]
        public IActionResult faculty_add()
        {
            ViewBag.FacultyDepart = new SelectList(db.Departments, "Departmentid", "Departmentname");
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult add_faculty(Faculty faculty, IFormFile f_img) // insert
        {
            try
            {
                if (faculty != null)
                {
                    if (f_img != null && f_img.Length > 0)
                    {
                        var filename = Path.GetFileName(f_img.FileName);
                        string folderPath = Path.Combine("wwwroot/Contentimages/faculty", filename);
                        var dbpath = Path.Combine("faculty", filename);
                        using (var stream = new FileStream(folderPath, FileMode.Create))
                        {
                            f_img.CopyTo(stream);
                        }
                        faculty.FacultyImage = dbpath;

                    }
                    db.Add(faculty);
                    db.SaveChanges();
                    TempData["Success"] = "Faculty Record Insert Successfully..";
                    return RedirectToAction(nameof(faculty_show));
                }
                else
                {
                    TempData["Error"] = "Invalid Faculty data. Please check your input and try again.";
                    return View(faculty);
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while adding the cause. Please try again later. " + ex.Message;
                ViewBag.FacultyDepart = new SelectList(db.Departments, "Departmentid", "Departmentname");
                return View(faculty);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult faculty_show()
        {
            return View(db.Faculties.Include("Department").ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult faculty_del(int? id) //delete
        {
            try
            {
                var delete = db.Faculties.FirstOrDefault(f => f.FacultyId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(faculty_show));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(faculty_show));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(faculty_show));
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult faculty_edit(int? id) //edit
        {
            try
            {
                var data = db.Faculties.FirstOrDefault(ft => ft.FacultyId == id);
                if (data == null)
                {
                    TempData["Error"] = "Faculty Record not found.";
                    return RedirectToAction(nameof(faculty_show));
                }
                ViewBag.FacultyDepart = new SelectList(db.Departments, "Departmentid", "Departmentname");
                return View(data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the Faculty records for editing: " + ex.Message;
                return RedirectToAction(nameof(faculty_show));
            }

        }
        [Authorize(Roles = "Admin")]
        public IActionResult faculty_edit2(Faculty faculty, IFormFile file) //update
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    Guid r = Guid.NewGuid();
                    var filename = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = file.ContentType.ToLower();
                    var exten_presize = extension.Substring(6);

                    var unique_name = filename + r + "." + exten_presize;
                    string folderPath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Contentimages/faculty");
                    var dbpath = Path.Combine(folderPath, unique_name);
                    using (var stream = new FileStream(dbpath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var dbAddress = Path.Combine(unique_name);

                    faculty.FacultyImage = dbAddress;
                    faculty.Datesubmitted = DateTime.Now;
                    db.Update(faculty);
                    db.SaveChanges();
                    TempData["Success"] = "Record Update SuccessFully";
                    return RedirectToAction(nameof(faculty_show));

                }

                else
                {
                    var existing = db.Faculties.FirstOrDefault(f => f.FacultyId == faculty.FacultyId);
                    if (existing != null)
                    {
                        existing.FacultyName = faculty.FacultyName;
                        existing.Datesubmitted = DateTime.Now;
                        existing.Department = faculty.Department;
                        existing.DepartmentId = faculty.DepartmentId;
                       

                        db.SaveChanges();
                        TempData["Success"] = "Record Updated Successfully with previous image";
                        return RedirectToAction(nameof(faculty_show));
                    }
                    else
                    {
                        TempData["Error"] = "Record Not Found";
                        return RedirectToAction(nameof(faculty_show));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the Faculty Record: " + ex.Message;
                return RedirectToAction(nameof(faculty_show));
            }
        }



        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //Faculties 
        //------------------------------------------------------------------------------------------
        //Facilities
        [Authorize(Roles = "Admin")]
        public IActionResult facility_add()
        {
            ViewBag.FacilityDepart = new SelectList(db.Departments, "Departmentid", "Departmentname");
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult add_facility(Facility facility) //insert
        {
            try
            {
                db.Add(facility);
                db.SaveChanges();
                TempData["Success"] = "Facility Record Insert SuccessFully";
                return RedirectToAction(nameof(facility_show));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while inserting the record. Please try again later. " + ex.Message;
                ViewBag.FacilityDepart = new SelectList(db.Departments, "Departmentid", "Departmentname");
                return View(facility);

            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult facility_show()
        {
            return View(db.Facilities.Include("FacilityDepart").ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult facility_del(int? id) //delete
        {
            try
            {
                var delete = db.Facilities.FirstOrDefault(f => f.FacilityId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(facility_show));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(facility_show));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(facility_show));
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult facility_edit(int? id) //edit
        {
            try
            {
                var data = db.Facilities.FirstOrDefault(ft => ft.FacilityId == id);
                if (data == null)
                {
                    TempData["Error"] = "Facility Record not found.";
                    return RedirectToAction(nameof(facility_show));
                }
                ViewBag.FacilityDepart = new SelectList(db.Departments, "Departmentid", "Departmentname");
                return View(data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the facility records for editing: " + ex.Message;
                return RedirectToAction(nameof(facility_show));
            }

        }
        [Authorize(Roles = "Admin")]
        public IActionResult facility_edit2(Facility facility) //update
        {
            try
            {
                var original = db.Facilities.AsNoTracking().FirstOrDefault(ft => ft.FacilityId == facility.FacilityId);
                if (original == null)
                {
                    TempData["Error"] = "Facility Record not found.";
                    return RedirectToAction(nameof(facility_show));
                }
/*
                facility.FacilityAvailablefrom = DateTime.Now;*/
                db.Update(facility);
                db.SaveChanges();

                TempData["Success"] = "Records Updated Successfully!!";
                return RedirectToAction(nameof(facility_show));
            }

            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the category: " + ex.Message;
                return RedirectToAction(nameof(facility_show));
            }
        }

        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

        // Student registered
        [Authorize(Roles = "Admin")]
        public IActionResult registered_student()
        {
            return View(db.Students.Include(s => s.StudentUser).ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult student_del(int? id)  //delete
        {
            try
            {
                var delete = db.Students.FirstOrDefault(s => s.StudentId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(registered_student));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(registered_student));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(registered_student));
            }
        }
        //********************************
        // Student Select Course
        [Authorize(Roles = "Admin")]
        public IActionResult select_course()
        {
            return View(db.CourseSelections.Include(f => f.Form).Include(f => f.Form.Student).ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult selectcourse_del(int? id)  //delete
        {
            try
            {
                var delete = db.CourseSelections.FirstOrDefault(s => s.SelectionId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(select_course));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(select_course));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(select_course));
            }
        }

        //-------------------------------
        //***Education Records 
        [Authorize(Roles = "Admin")]
        public IActionResult education_record()
        {
            return View(db.PreviousEducations.Include(p => p.AdmissionUser).Include(st => st.Student).ToList());
        }

        public IActionResult education_del(int? id)  //delete
        {
            try
            {
                var delete = db.PreviousEducations.FirstOrDefault(s => s.EducationId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(education_record));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(education_record));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(education_record));
            }
        }
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


        //========================================================
        //Role
        [Authorize(Roles = "Admin")]
        public IActionResult role_add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        public IActionResult addRole(UserRole role) //insert
        {
            try
            {
                db.Add(role);
                db.SaveChanges();
                TempData["Success"] = "Admin Role Insert SuccessFully";
                return RedirectToAction(nameof(role_add));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while inserting the record. Please try again later. " + ex.Message;
                return RedirectToAction(nameof(role_add));
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult role_show() //read
        {
            return View(db.UserRoles.ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult role_del(int? id) //delete
        {
            try
            {
                var delete = db.UserRoles.FirstOrDefault(x => x.RoleId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(role_show));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(role_show));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(role_show));
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult role_edit(int? id) //update
        {
            try
            {
                var data = db.UserRoles.FirstOrDefault(x => x.RoleId == id);
                if (data == null)
                {
                    TempData["Error"] = "Admin Role not found.";
                    return RedirectToAction(nameof(role_show));
                }
                return View(data);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while loading the category for editing: " + ex.Message;
                return RedirectToAction(nameof(role_show));
            }

        }
        [Authorize(Roles = "Admin")]
        public IActionResult Role_edit2(UserRole role) //update
        {
            try
            {
                db.Update(role);
                db.SaveChanges();
                TempData["Success"] = "Records Updated Successfully!!";
                return RedirectToAction(nameof(role_show));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the category: " + ex.Message;
                return RedirectToAction(nameof(role_show));
            }
        }

        //========================================================

        //**************************************************************************************
        //User

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult user_add() //insert
        {

            ViewBag.UserRoles = new SelectList(db.UserRoles, "RoleId", "RoleName");
            return View();
        }

        [HttpPost]
        public IActionResult user_add(User user, IFormFile uimg) //insert
        {
            try
            {
                if (user != null)
                {
                    if (uimg != null && uimg.Length > 0)
                    {
                        var filename = Path.GetFileName(uimg.FileName);
                        string folderPath = Path.Combine("wwwroot/Contentimages/profile", filename);
                        var dbpath = Path.Combine("profile", filename);
                        using (var stream = new FileStream(folderPath, FileMode.Create))
                        {
                            uimg.CopyTo(stream);
                        }
                        user.UserImage = dbpath;
                        user.UserRoleId = 2;
                    }
                    db.Add(user);
                    db.SaveChanges();
                    TempData["Success"] = "User Registered Successfully..";
                    return RedirectToAction(nameof(user_show));
                }
                else
                {
                    TempData["Error"] = "Invalid user data. Please check your input and try again.";
                    ViewBag.UserRoles = new SelectList(db.UserRoles, "RoleId", "RoleName");
                    return View(user);

                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while registering the user. Please try again later. " + ex.Message;
                ViewBag.UserRoles = new SelectList(db.UserRoles, "RoleId", "RoleName");
                return View(user);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult user_show() // read
        {
            return View(db.Users.Include("UserRole").ToList());

        }
/*
        [Authorize(Roles = "Admin")]*/
        public IActionResult user_del(int? id) // delete
        {
            try
            {
                var delete = db.Users.FirstOrDefault(u => u.UserId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(user_show));
                }

                if (delete.UserRoleId == 1)
                {
                    TempData["Error"] = "You cannot delete the admin user.";
                    return RedirectToAction(nameof(user_show));
                }
                else if (delete.UserRoleId > 1)
                {
                    TempData["Success"] = "User Record Deleted Successfully..";
                }

                db.Remove(delete);
                db.SaveChanges();

                return RedirectToAction(nameof(user_show));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later. " + ex.Message;
                return RedirectToAction(nameof(user_show));
            }
        }


/*
        [Authorize(Roles = "Admin")]*/
        public IActionResult user_edit(int? id) // update
        {
            var user_data = db.Users.FirstOrDefault(u => u.UserId == id);
            ViewBag.UserRoles = new SelectList(db.UserRoles, "RoleId", "RoleName");
            return View(user_data);
        }

        public IActionResult user_edit2(User user, IFormFile file) //update
        {
            try
            {
                if (file != null && file.Length > 0)
                {
                    Guid r = Guid.NewGuid();
                    var filename = Path.GetFileNameWithoutExtension(file.FileName);
                    var extension = file.ContentType.ToLower();
                    var exten_presize = extension.Substring(6);

                    var unique_name = filename + r + "." + exten_presize;
                    string folderPath = Path.Combine(HttpContext.Request.PathBase.Value, "wwwroot/Contentimages/profile");
                    var dbpath = Path.Combine(folderPath, unique_name);
                    using (var stream = new FileStream(dbpath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    var dbAddress = Path.Combine(unique_name);

                    user.UserImage = dbAddress;
                    db.Update(user);
                    db.SaveChanges();
                    TempData["Success"] = "Record Update SuccessFully";
                    return RedirectToAction(nameof(user_show));

                }
                else
                {
                    var existinguser = db.Users.FirstOrDefault(u => u.UserId == user.UserId);
                    if (existinguser != null)
                    {
                        existinguser.UserRoleId = user.UserRoleId;
                        existinguser.UserName = user.UserName;
                        existinguser.UserEmail = user.UserEmail;
                        existinguser.UserPassword = user.UserPassword;
                        existinguser.UserRole = user.UserRole;

                        db.SaveChanges();
                        TempData["Success"] = "Record Updated Successfully with previous image";
                        return RedirectToAction(nameof(user_show));
                    }
                    else
                    {
                        TempData["Error"] = "Record Not Found";
                        return RedirectToAction(nameof(user_show));
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while updating the User: " + ex.Message;
                return RedirectToAction(nameof(user_show));
            }
        }



        //***************************************************************************************
        //Contacts Record
        [Authorize(Roles = "Admin")]
        public IActionResult contact_record()
        {
            return View(db.Contacts.Include(c => c.ContUser).ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult cont_del(int? id)  //delete
        {
            try
            {
                var delete = db.Contacts.FirstOrDefault(x => x.ContId == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(contact_record));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(contact_record));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(contact_record));
            }
        }
        //$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$$
        //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        //Feedback Record
        [Authorize(Roles = "Admin")]
        public IActionResult feedback_record()
        {
            return View(db.Feedbacks.Include(f => f.FeedbackUser).ToList());
        }
        [Authorize(Roles = "Admin")]
        public IActionResult feedback_del(int? id)  //delete
        {
            try
            {
                var delete = db.Feedbacks.FirstOrDefault(f => f.Feedbackid == id);
                if (delete == null)
                {
                    TempData["Error"] = "Record not found.";
                    return RedirectToAction(nameof(feedback_record));
                }

                db.Remove(delete);
                db.SaveChanges();
                TempData["Success"] = "Record Deleted Successfully..";
                return RedirectToAction(nameof(feedback_record));
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while deleting the record. Please try again later." + ex.Message;
                return RedirectToAction(nameof(feedback_record));
            }
        }
        //****************************************************************************************
        //Login

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User user)
        {
            try
            {
                var data = db.Users.FirstOrDefault(x => x.UserEmail == user.UserEmail && x.UserPassword == user.UserPassword);
                if (data != null)
                {
                    var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, data.UserName),
                new Claim(ClaimTypes.Email, data.UserEmail),
                new Claim(ClaimTypes.Role, data.UserRoleId == 1 ? "Admin" : "User"),
                new Claim("UserImage", data.UserImage ?? "us-icon.png")
            };

                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    var login = HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                    TempData["Success"] = data.UserRoleId == 1 ? "Admin login successful!" : "User login successful!";

                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    TempData["Error"] = "Invalid Password.";
                    return View("Login");
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occurred while processing your request. Please try again later. " + ex;
                return View("Login");
            }
        }



        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            TempData["Success"] = "You have been logged out successfully.";
            return RedirectToAction(nameof(Index), "Admin");
        }





        //****************************************************************************************






        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
