// See https://aka.ms/new-console-template for more information

using Microsoft.EntityFrameworkCore;
using MySqlConnector;
using shiyan1.DatabaseModels;
using System;
using System.Linq;
using System.Threading;

namespace shiyan1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isContinue = true;
            bool isAdmin = false;
            bool backToMainMenu = false;
            string loggedInStudentId = null;

            while (isContinue)
            {
                if (!isAdmin && loggedInStudentId == null)
                {
                    Console.Clear();
                    HighlightText("Select role: 1. Admin 2. Student 3. Exit", ConsoleColor.Cyan);
                    var roleChoice = Console.ReadLine();

                    if (roleChoice == "1")
                    {
                        int attempts = 0;
                        while (attempts < 3)
                        {
                            Console.Write("Enter Admin Account: ");
                            var aAccount = Console.ReadLine();
                            Console.Write("Enter Admin Password: ");
                            var aPassword = Console.ReadLine();

                            using (var context = new _2109060123DbContext())
                            {
                                var admin = context.Admins.FirstOrDefault(a => a.Aaccount == aAccount && a.Apassword == aPassword);
                                if (admin != null)
                                {
                                    isAdmin = true;
                                    break;
                                }
                                else
                                {
                                    HighlightText("Invalid Admin credentials.", ConsoleColor.Red);
                                    attempts++;
                                }
                            }
                            if (attempts == 3)
                            {
                                HighlightText("Too many failed attempts. Please wait 1 minute before trying again.", ConsoleColor.Red);
                                Thread.Sleep(60000); // 等待1分钟
                                attempts = 0;
                            }
                        }
                    }
                    else if (roleChoice == "2")
                    {
                        int attempts = 0;
                        while (attempts < 3)
                        {
                            Console.Write("Enter Student Account: ");
                            var sid = Console.ReadLine();
                            Console.Write("Enter Student Password: ");
                            var sPassword = Console.ReadLine();

                            using (var context = new _2109060123DbContext())
                            {
                                var student = context.Students.FirstOrDefault(a => a.Sid == sid && a.Spassword == sPassword);
                                if (student != null)
                                {
                                    isAdmin = false;
                                    loggedInStudentId = sid; // 存储学生 ID
                                    break;
                                }
                                else
                                {
                                    HighlightText("Invalid Student credentials.", ConsoleColor.Red);
                                    attempts++;
                                }
                            }
                            if (attempts == 3)
                            {
                                HighlightText("Too many failed attempts. Please wait 1 minute before trying again.", ConsoleColor.Red);
                                Thread.Sleep(60000); // 等待1分钟
                                attempts = 0;
                            }
                        }
                    }
                    else if (roleChoice == "3")
                    {
                        isContinue = false;
                        continue;
                    }
                    else
                    {
                        HighlightText("Invalid choice.", ConsoleColor.Red);
                    }
                }
                else
                {
                    backToMainMenu = false;
                    while (!backToMainMenu)
                    {
                        Console.Clear();
                        if (isAdmin)
                        {
                            HighlightText("1. Show all students", ConsoleColor.Green);
                            HighlightText("2. Add new student", ConsoleColor.Green);
                            HighlightText("3. Delete student", ConsoleColor.Green);
                            HighlightText("4. Update student information", ConsoleColor.Green);
                            HighlightText("5. Find student by ID", ConsoleColor.Green);
                            HighlightText("6. Show all courses", ConsoleColor.Green);
                            HighlightText("7. Add new course", ConsoleColor.Green);
                            HighlightText("8. Delete course", ConsoleColor.Green);
                            HighlightText("9. Update course information", ConsoleColor.Green);
                            HighlightText("10. Find course by ID", ConsoleColor.Green);
                            HighlightText("11. Enroll in courses", ConsoleColor.Green);
                            HighlightText("12. Show enrolled courses", ConsoleColor.Green);
                            HighlightText("13. Back to main menu", ConsoleColor.Green);
                        }
                        else
                        {
                            HighlightText("1. Enroll in courses", ConsoleColor.Green);
                            HighlightText("2. Show my enrolled courses", ConsoleColor.Green);
                            HighlightText("3. Back to main menu", ConsoleColor.Green);
                        }

                        Console.Write("Enter your choice: ");
                        var choice = Console.ReadLine();

                        if (isAdmin)
                        {
                            switch (choice)
                            {
                                case "1":
                                    ShowAllStudents();
                                    break;
                                case "2":
                                    AddNewStudent();
                                    break;
                                case "3":
                                    DeleteStudent();
                                    break;
                                case "4":
                                    UpdateStudentInfo();
                                    break;
                                case "5":
                                    FindStudentById();
                                    break;
                                case "6":
                                    ShowAllCourses();
                                    break;
                                case "7":
                                    AddNewCourse();
                                    break;
                                case "8":
                                    DeleteCourse();
                                    break;
                                case "9":
                                    UpdateCourseInfo();
                                    break;
                                case "10":
                                    FindCourseById();
                                    break;
                                case "11":
                                    EnrollInCourse();
                                    break;
                                case "12":
                                    ShowEnrolledCourses();
                                    break;
                                case "13":
                                    backToMainMenu = true;
                                    isAdmin = false;
                                    break;
                                default:
                                    HighlightText("Invalid choice", ConsoleColor.Red);
                                    break;
                            }
                        }
                        else
                        {
                            switch (choice)
                            {
                                case "1":
                                    EnrollInCourse();
                                    break;
                                case "2":
                                    ShowMyEnrolledCourses(loggedInStudentId);
                                    break;
                                case "3":
                                    backToMainMenu = true;
                                    loggedInStudentId = null;
                                    break;
                                default:
                                    HighlightText("Invalid choice", ConsoleColor.Red);
                                    break;
                            }
                        }
                    }
                }
            }
        }

        static void HighlightText(string text, ConsoleColor color)
        {
            var originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }

        static void AddNewStudent()
        {
            Console.WriteLine("Adding new student id:");
            var studentId = Console.ReadLine();
            Console.WriteLine("Enter student name: ");
            var studentName = Console.ReadLine();
            Console.WriteLine("Enter student class: ");
            var studentClass = Console.ReadLine();
            Console.WriteLine("Enter initial password: ");
            var studentPassword = Console.ReadLine();

            using (var context = new _2109060123DbContext())
            {
                var checkStu = context.Students.Find(studentId);
                if (checkStu != null)
                {
                    HighlightText($"Student ID: {studentId} is already taken by {checkStu.Sname}", ConsoleColor.Red);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
                var student = new Student
                {
                    Sid = studentId,
                    Sname = studentName,
                    Sclass = studentClass,
                    Spassword = studentPassword
                };
                context.Students.Add(student);
                context.SaveChanges();
            }
            HighlightText("Student added successfully, press any key to return", ConsoleColor.Green);
            Console.ReadKey();
        }

        static void ShowAllStudents()
        {
            using (var context = new _2109060123DbContext())
            {
                var students = context.Students.ToList();
                foreach (var student in students)
                {
                    HighlightText($"ID: {student.Sid}, Name: {student.Sname}, Class: {student.Sclass}", ConsoleColor.Yellow);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void UpdateStudentInfo()
        {
            Console.WriteLine("Enter student id to update:");
            var studentId = Console.ReadLine();
            using (var context = new _2109060123DbContext())
            {
                var student = context.Students.Find(studentId);
                if (student == null)
                {
                    HighlightText($"Student ID: {studentId} does not exist.", ConsoleColor.Red);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Enter new student name (or press Enter to keep current): ");
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    student.Sname = input;
                }

                Console.WriteLine("Enter new student class (or press Enter to keep current): ");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    student.Sclass = input;
                }

                Console.WriteLine("Enter new password (or press Enter to keep current): ");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    student.Spassword = input;
                }

                context.SaveChanges();
            }
            HighlightText("Student information updated successfully, press any key to return", ConsoleColor.Green);
            Console.ReadKey();
        }

        static void DeleteStudent()
        {
            Console.WriteLine("Enter student id to delete:");
            var studentId = Console.ReadLine();
            using (var context = new _2109060123DbContext())
            {
                var student = context.Students.Find(studentId);
                if (student == null)
                {
                    HighlightText($"Student ID: {studentId} does not exist.", ConsoleColor.Red);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                // 删除 selected_course 表中所有引用该学生的记录
                var selectedCourses = context.SelectedCourses.Where(sc => sc.Sid == studentId).ToList();
                if (selectedCourses.Count > 0)
                {
                    context.SelectedCourses.RemoveRange(selectedCourses);
                    context.SaveChanges();
                }

                // 删除学生
                context.Students.Remove(student);
                context.SaveChanges();
            }
            HighlightText("Student deleted successfully, press any key to return", ConsoleColor.Green);
            Console.ReadKey();
        }


        static void FindStudentById()
        {
            Console.WriteLine("Enter student id to find:");
            var studentId = Console.ReadLine();

            using (var context = new _2109060123DbContext())
            {
                var student = context.Students.Find(studentId);
                if (student == null)
                {
                    HighlightText($"Student ID: {studentId} does not exist.", ConsoleColor.Red);
                }
                else
                {
                    HighlightText($"ID: {student.Sid}, Name: {student.Sname}, Class: {student.Sclass}", ConsoleColor.Yellow);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void AddNewCourse()
        {
            Console.WriteLine("Adding new course id:");
            var courseId = Console.ReadLine();
            Console.WriteLine("Enter course name: ");
            var courseName = Console.ReadLine();
            Console.WriteLine("Enter course score: ");
            var courseScore = Console.ReadLine();
            Console.WriteLine("Enter teacher name: ");
            var courseTeacher = Console.ReadLine();
            Console.WriteLine("Enter semester: ");
            var semester = Console.ReadLine();
            Console.WriteLine("Enter class time: ");
            var classTime = Console.ReadLine();//上课时间
            Console.WriteLine("Enter classroom: ");
            var classroom = Console.ReadLine();

            using (var context = new _2109060123DbContext())
            {
                var checkCourse = context.Courses.Find(courseId);
                if (checkCourse != null)
                {
                    HighlightText($"Course ID: {courseId} is already taken by {checkCourse.Cname}", ConsoleColor.Red);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }
                var course = new Course
                {
                    Cid = courseId,
                    Cname = courseName,
                    Cscore = courseScore,
                    Cteacher = courseTeacher,
                    Csem = semester,
                    Ctime = classTime,
                    Cclassroom = classroom
                };
                context.Courses.Add(course);
                context.SaveChanges();
            }
            HighlightText("Course added successfully, press any key to return", ConsoleColor.Green);
            Console.ReadKey();
        }

        static void ShowAllCourses()
        {
            using (var context = new _2109060123DbContext())
            {
                var courses = context.Courses.ToList();
                foreach (var course in courses)
                {
                    HighlightText($"ID: {course.Cid}, Name: {course.Cname}, Score: {course.Cscore}, Teacher: {course.Cteacher}, " +
                        $"Semester: {course.Csem}, Time: {course.Ctime}, Classroom: {course.Cclassroom}", ConsoleColor.Yellow);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void UpdateCourseInfo()
        {
            Console.WriteLine("Enter course id to update:");
            var courseId = Console.ReadLine();
            using (var context = new _2109060123DbContext())
            {
                var course = context.Courses.Find(courseId);
                if (course == null)
                {
                    HighlightText($"Course ID: {courseId} does not exist.", ConsoleColor.Red);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Enter new course name (or press Enter to keep current): ");
                var input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    course.Cname = input;
                }

                Console.WriteLine("Enter new course score (or press Enter to keep current): ");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    course.Cscore = input;
                }

                Console.WriteLine("Enter new teacher name (or press Enter to keep current): ");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    course.Cteacher = input;
                }

                Console.WriteLine("Enter new semester name (or press Enter to keep current): ");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    course.Csem = input;
                }

                Console.WriteLine("Enter new time (or press Enter to keep current): ");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    course.Ctime = input;
                }

                Console.WriteLine("Enter new classroom name (or press Enter to keep current): ");
                input = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(input))
                {
                    course.Cclassroom = input;
                }

                context.SaveChanges();
            }
            HighlightText("Course information updated successfully, press any key to return", ConsoleColor.Green);
            Console.ReadKey();
        }

        static void DeleteCourse()
        {
            Console.WriteLine("Enter course id to delete:");
            var courseId = Console.ReadLine();
            using (var context = new _2109060123DbContext())
            {
                var course = context.Courses.Find(courseId);
                if (course == null)
                {
                    HighlightText($"Course ID: {courseId} does not exist.", ConsoleColor.Red);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                // 删除 `selected_course` 表中所有引用该课程的记录
                var selectedCourses = context.SelectedCourses.Where(sc => sc.Cid == courseId).ToList();
                if (selectedCourses.Count > 0)
                {
                    context.SelectedCourses.RemoveRange(selectedCourses);
                    context.SaveChanges();
                }

                // 删除课程
                context.Courses.Remove(course);
                context.SaveChanges();
            }
            HighlightText("Course deleted successfully, press any key to return", ConsoleColor.Green);
            Console.ReadKey();
        }

        static void FindCourseById()
        {
            Console.WriteLine("Enter course id to find:");
            var courseId = Console.ReadLine();

            using (var context = new _2109060123DbContext())
            {
                var course = context.Courses.Find(courseId);
                if (course == null)
                {
                    HighlightText($"Course ID: {courseId} does not exist.", ConsoleColor.Red);
                }
                else
                {
                    HighlightText($"ID: {course.Cid}, Name: {course.Cname}, Score: {course.Cscore}, Teacher: {course.Cteacher}, " +
                        $"Semester: {course.Csem}, Time: {course.Ctime}, Classroom: {course.Cclassroom}", ConsoleColor.Yellow);
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        static void EnrollInCourse()
        {
            Console.WriteLine("Enter student id:");
            var studentId = Console.ReadLine();
            Console.WriteLine("Enter course ids (comma-separated): ");
            var courseIdsInput = Console.ReadLine();

            var currentDate = DateOnly.FromDateTime(DateTime.Now); //选课的日期
            var courseIds = courseIdsInput.Split(',');

            using (var context = new _2109060123DbContext())
            {
                var student = context.Students.Find(studentId);

                if (student == null)
                {
                    HighlightText($"Student ID: {studentId} does not exist.", ConsoleColor.Red);
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                    return;
                }

                foreach (var courseId in courseIds)
                {
                    var trimmedCourseId = courseId.Trim();
                    var course = context.Courses.Find(trimmedCourseId);

                    if (course == null)
                    {
                        HighlightText($"Course ID: {trimmedCourseId} does not exist.", ConsoleColor.Red);
                        continue; // Skip this course ID and continue with the next
                    }

                    var selectedCourse = new SelectedCourse
                    {
                        Sid = studentId,
                        Cid = trimmedCourseId,
                        ScDate = currentDate,
                    };

                    try
                    {
                        context.SelectedCourses.Add(selectedCourse);
                        context.SaveChanges();
                        HighlightText($"Enrolled in course {trimmedCourseId} successfully.", ConsoleColor.Green);
                    }
                    catch (DbUpdateException ex) when (ex.InnerException is MySqlException mysqlEx && mysqlEx.Number == 1062)
                    {
                        HighlightText($"Already enrolled in course {trimmedCourseId}. Cannot enroll again.", ConsoleColor.Red);
                        context.Entry(selectedCourse).State = EntityState.Detached; // Detach the entity to avoid tracking issues
                    }
                    catch (Exception ex)
                    {
                        HighlightText($"Error enrolling in course {trimmedCourseId}: {ex.Message}", ConsoleColor.Red);
                        context.Entry(selectedCourse).State = EntityState.Detached; // Detach the entity to avoid tracking issues
                    }
                }
            }

            HighlightText("Course enrollment process completed, press any key to return.", ConsoleColor.Green);
            Console.ReadKey();
        }

        static void ShowEnrolledCourses()
        {
            Console.WriteLine("Enter student id:");
            var studentId = Console.ReadLine();

            using (var context = new _2109060123DbContext())
            {
                var selectedCourses = context.SelectedCourses
                    .Where(sc => sc.Sid == studentId)
                    .ToList();

                if (selectedCourses.Count == 0)
                {
                    HighlightText("No courses found for the given student ID.", ConsoleColor.Red);
                }
                else
                {
                    var enrolledCourses = selectedCourses
                        .Join(context.Courses, sc => sc.Cid, c => c.Cid, (sc, c) => new
                        {
                            sc.Cid,
                            c.Cname,
                            c.Csem,
                            sc.ScDate,
                            c.Ctime,
                            c.Cclassroom,
                            c.Cscore,
                            c.Cteacher
                        })
                        .ToList();

                    foreach (var course in enrolledCourses)
                    {
                        HighlightText($"Course ID: {course.Cid}, Course Name: {course.Cname}, Semester: {course.Csem}, Date: {course.ScDate}, Time: {course.Ctime}, Classroom: {course.Cclassroom}, Score: {course.Cscore}, Teacher: {course.Cteacher}", ConsoleColor.Yellow);
                    }
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
        static void ShowMyEnrolledCourses(string studentId)
        {
            using (var context = new _2109060123DbContext())
            {
                var selectedCourses = context.SelectedCourses
                    .Where(sc => sc.Sid == studentId)
                    .ToList();

                if (selectedCourses.Count == 0)
                {
                    HighlightText("No courses found for the given student ID.", ConsoleColor.Red);
                }
                else
                {
                    var enrolledCourses = selectedCourses
                        .Join(context.Courses, sc => sc.Cid, c => c.Cid, (sc, c) => new
                        {
                            sc.Cid,
                            c.Cname,
                            c.Csem,
                            sc.ScDate,
                            c.Ctime,
                            c.Cclassroom,
                            c.Cscore,
                            c.Cteacher
                        })
                        .ToList();

                    foreach (var course in enrolledCourses)
                    {
                        HighlightText($"Course ID: {course.Cid}, Course Name: {course.Cname}, Semester: {course.Csem}, Date: {course.ScDate}, Time: {course.Ctime}, Classroom: {course.Cclassroom}, Score: {course.Cscore}, Teacher: {course.Cteacher}", ConsoleColor.Yellow);
                    }
                }
            }
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}



