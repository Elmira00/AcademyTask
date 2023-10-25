using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace ConsoleApp31
{
    /*
 * Academy=>Groups
 * Group=>Teacher,Students
 * Teacher=name,surname,email,salary
 * Student=name,surname,email,Exams,GetAvgScore
 * Exam=>LessonName,Score,ExamDate
 */
    

    

    class Academy
    {
        public Academy(string name, string location)
        {
            Name = name;
            Location = location;
        }
        public string Name { get; set; }
        public string Location { get; set; }
        public Group[] Groups { get; set; }
        public int groupCount { get; set; }

        public void AddGroup(Group newGroup)
        {
            Group[] temp = new Group[++groupCount];
            if (Groups != null)
            {
                Groups.CopyTo(temp, 0);
            }
            temp[temp.Length - 1] = newGroup;
            Groups = temp;
        }


        public void Show()
        {
            Console.WriteLine($"\nA C A D E M Y   : {Name}");
            Console.WriteLine($"L o c a t i o n : {Location}");
            foreach (var group in Groups)
            {
                group.Show();
            }
        }
    }
    class Group
    {
        public Group(string name,Teacher teacher)
        {
            Id = Guid.NewGuid();
            Name = name;
            Teacher = teacher;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }
        public Student[] Students { get; set; }
        private int studentCount { get; set; }

        public void AddStudent(Student newStudent)
        {
            Student[] temp = new Student[++studentCount];
            if (Students != null)
            {
                Students.CopyTo(temp, 0);
            }
            temp[temp.Length - 1] = newStudent;
            Students = temp;
        }

        public void Show()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n_____________G R O U P    I N F O_____________\n");
            Console.WriteLine($"ID : {Id}");
            Console.WriteLine($"Name : {Name}");
            Teacher.Show();
            foreach (var student in Students)
            {
                student.Show();
            }

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    abstract class Human
    {
        protected Human(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }

        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public void Show()
        {
            Console.WriteLine($"Name : {Name}\n");
            Console.WriteLine($"Surname : {Surname}\n");
            Console.WriteLine($"Email : {Email}\n");
        }
    }
    class Teacher : Human
    {
        public double Salary { get; set; }
        public Teacher(string name, string surname, string email, double salary)
            : base(name, surname, email)
        {
            Salary = salary;
        }
        public new void Show()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("<<<<<<<<TEACHER INFO<<<<<<<<");
            base.Show();
            Console.WriteLine($"Salary : {Salary}");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }
    class Student : Human
    {
        public Exam[] Exams { get; set; }
        private int examCount { get; set; }
        public Student(string name, string surname, string email)
            : base(name, surname, email)
        {

        }
        public void AddExam(Exam newExam)
        {
            Exam[] temp = new Exam[++examCount];
            if (Exams != null)
            {
                Exams.CopyTo(temp, 0);
            }
            temp[temp.Length - 1] = newExam;
            Exams = temp;
        }
        public double GetAvgScore()
        {
            double sum = 0;
            for (int i = 0; i < examCount; i++)
            {
                sum += Exams[i].Score;
            }
            return sum / examCount;
        }
        public new void Show()
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("^^^^^^ STUDENT INFO^^^^^^");
            base.Show();
            Console.WriteLine($"Avarage score : {this.GetAvgScore()}");
            foreach (var exam in Exams)
            {
                exam.ShowExam();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    class Exam
    {
        public Exam(string lessonName, double score, DateTime examDate)
        {
            LessonName = lessonName;
            Score = score;
            ExamDate = examDate;
        }

        public string LessonName { get; set; }
        public double Score { get; set; }
        public DateTime ExamDate { get; set; }

        public void ShowExam()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("++++++++++Exam Info++++++++++");
            Console.WriteLine($"Lesson Name : {LessonName}");
            Console.WriteLine($"Score : {Score}");
            Console.WriteLine($"Exam Date: {ExamDate}");
            Console.ForegroundColor = ConsoleColor.White;
        }

    }

    public class Program
    {

        public static void SendEmail( string toAddress, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.Host = "smtp.gmail.com";
            smtpClient.Port = 587;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential("ahmadovaelmira67@gmail.com", "uqcpfobwuidtvcif");         
           Console.WriteLine("Sending email . . .");
           var toEmail = $"{toAddress}@gmail.com";
           smtpClient.Send("ahmadovaelmira67@gmail.com",toEmail, subject, body);
                 
        }

        static void Main(string[] args)
        {
     
            Exam e1 = new Exam("Logic", 67.3, DateTime.Today);
            Exam e2 = new Exam("Math", 90.8, DateTime.Now);
            Exam e3 = new Exam("Programming", 79, DateTime.Now);
            Exam e4 = new Exam("Graphic Design", 56.7, DateTime.Today);
            Exam e5 = new Exam("Pyhsics", 24.5, DateTime.Today);
            Exam e6 = new Exam("Geography", 100, DateTime.Today);
            Exam e7 = new Exam("English", 89.2, DateTime.Today);
            Exam e8 = new Exam("IT", 98 / 6, DateTime.Today);

            Student s1 = new Student("Shafaq", "Aliyeva", "liyevasfq6");
            Student s2 = new Student("Shalala", "Farzaliyeva", "selaleferzeliyeva6");
            Student s3 = new Student("Melek", "Qemberova", "qenberlimelek29");
            Student s4 = new Student("Fuad", "Aliyev", "eliyevf465");

            s1.AddExam(e1);
            s1.AddExam(e2);
            s1.AddExam(e3);
            s2.AddExam(e4);
            s2.AddExam(e5);
            s3.AddExam(e6);
            s4.AddExam(e7);
            s4.AddExam(e8);

            Teacher t1 = new Teacher("Sevil", "Sariyeva", "Sariyeva00@gmail.com", 700);
            Teacher t2 = new Teacher("Ali", "Murselli", "AliMursel@gmail.com", 1500);

            Group g1 = new Group("3212",t1);
            Group g2 = new Group("1225",t2);

            g1.AddStudent(s1);
            g1.AddStudent(s2);
            g2.AddStudent(s3);
            g2.AddStudent(s4);

            Academy academy = new Academy("StepIt", "Nizami,CityPoint");
            academy.AddGroup(g1);
            academy.AddGroup(g2);
            //academy.Show();

            try
            {
                while (true)
                {
                Menu:
                    Console.Clear();
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("\nEnter  group name : ");
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.WriteLine("\nEXIT -> [e] : ");
                    int i = 0;
                    foreach (var group in academy.Groups)
                    {
                        Console.WriteLine($"Group [{group.Name}]: ");
                    }
                    var selectedGroupName = Console.ReadLine();
                    if (selectedGroupName == "e")
                    {
                        break;
                    }
                    Group selectedGroup = null;
                    foreach (var group in academy.Groups)
                    {
                        if (group.Name.Equals(selectedGroupName) )
                        {
                            selectedGroup = group;
                        }                      
                    }
                    if (selectedGroup != null)
                    {
                        selectedGroup.Show();
                        Console.BackgroundColor = ConsoleColor.DarkMagenta;
                        Console.WriteLine("\n\nEnter student name : ");
                        Console.BackgroundColor = ConsoleColor.Black;
                        var selectedStudentName = Console.ReadLine();
                        Student selectedStudent = null;
                        foreach (var student in selectedGroup.Students)
                        {
                            if (student.Name.ToLower().Equals(selectedStudentName.ToLower()))
                            {
                                selectedStudent = student;
                            }                      
                        }
                        if (selectedStudent != null)
                        {
                            Console.Clear();
                            Console.BackgroundColor = ConsoleColor.DarkMagenta;
                            Console.WriteLine("\nDo you want to add exam : ");
                            Console.BackgroundColor = ConsoleColor.Black;
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nYes=>[1] No=>[2] : ");
                            Console.ForegroundColor = ConsoleColor.White;
                            var choice = Console.ReadLine();
                            if (choice == "1")
                            {
                                Console.WriteLine("Enter exam name : ");
                                var name = Console.ReadLine();
                                Console.WriteLine("Enter score : ");
                                double  score = Convert.ToDouble(Console.ReadLine());
                                Console.WriteLine("Enter exam date (month) : ");
                                int month = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter exam date (year) : ");
                                int year = Convert.ToInt32(Console.ReadLine());
                                Console.WriteLine("Enter exam date (day) : ");
                                int day = Convert.ToInt32(Console.ReadLine());
                                DateTime examDate = new DateTime(year,month,day);
                                Exam newExam = new Exam(name,score,examDate);
                                selectedStudent.AddExam(newExam);
                                string Email = $"Exam name : {name}\nScore : {score}\nExam date : {examDate.ToString()}";
                                SendEmail(selectedStudent.Email, "Exam", Email);                             
                                Console.ForegroundColor = ConsoleColor.DarkGreen;
                                Console.WriteLine("SENT SUCCESSFULLY!!");                            
                                Console.ForegroundColor = ConsoleColor.White;
                            }
                            else if (choice == "2")
                            {
                                goto Menu;
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                throw new Exception("\nW R O N G    I N P U T !!!\n");
                            }
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            throw new Exception("\nU N K N O W N      S T U D E N T     N A M E !!!\n");
                        }
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("\nU N K N O W N      G R O U P     N A M E !!!\n");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine(ex.Message);
            }
        }
    }
}