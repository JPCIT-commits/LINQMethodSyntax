using System;
using System.Linq;

public class Student{
    public int StudentID { get; set; }
    public string StudentName { get; set; }
    public int Age { get; set; }
    public string Major { get; set; }
	public double Tuition {get;set;}
}
public class StudentClubs
{
    public int StudentID { get; set; }
    public string ClubName { get; set; }
}
public class StudentGPA
{
    public int StudentID { get; set;}
    public double GPA    { get; set;}
}

class Program
{
    static void Main(string[] args)
    {
        // Student collection
		IList < Student > studentList = new List < Student >() { 
                new Student() { StudentID = 1, StudentName = "Frank Furter", Age = 55, Major="Hospitality", Tuition=3500.00} ,
                new Student() { StudentID = 2, StudentName = "Gina Host", Age = 21, Major="Hospitality", Tuition=4500.00 } ,
                new Student() { StudentID = 3, StudentName = "Cookie Crumb",  Age = 21, Major="CIT", Tuition=2500.00 } ,
                new Student() { StudentID = 4, StudentName = "Ima Script",  Age = 48, Major="CIT", Tuition=5500.00 } ,
                new Student() { StudentID = 5, StudentName = "Cora Coder",  Age = 35, Major="CIT", Tuition=1500.00 } ,
                new Student() { StudentID = 6, StudentName = "Ura Goodchild" , Age = 40, Major="Marketing", Tuition=500.00} ,
                new Student() { StudentID = 7, StudentName = "Take Mewith" , Age = 29, Major="Aerospace Engineering", Tuition=5500.00 }
		};
        // Student GPA Collection
        IList < StudentGPA > studentGPAList = new List < StudentGPA > () {
                new StudentGPA() { StudentID = 1,  GPA=4.0} ,
                new StudentGPA() { StudentID = 2,  GPA=3.5} ,
                new StudentGPA() { StudentID = 3,  GPA=2.0 } ,
                new StudentGPA() { StudentID = 4,  GPA=1.5 } ,
                new StudentGPA() { StudentID = 5,  GPA=4.0 } ,
                new StudentGPA() { StudentID = 6,  GPA=2.5} ,
                new StudentGPA() { StudentID = 7,  GPA=1.0 }
            };
        // Club collection
        IList < StudentClubs > studentClubList = new List < StudentClubs >() {
            new StudentClubs() {StudentID=1, ClubName="Photography" },
            new StudentClubs() {StudentID=1, ClubName="Game" },
            new StudentClubs() {StudentID=2, ClubName="Game" },
            new StudentClubs() {StudentID=5, ClubName="Photography" },
            new StudentClubs() {StudentID=6, ClubName="Game" },
            new StudentClubs() {StudentID=7, ClubName="Photography" },
            new StudentClubs() {StudentID=3, ClubName="PTK" },
        };

        // Student id's grouped by GPA
        var studentIDGPA = studentGPAList.GroupBy (
            s => s.GPA).Select (
            g => new
            {
                GPA = g.Key,
                StudentIDs = g.Select (s => s.StudentID)
            });
        foreach (var g in studentIDGPA)
        {
            Console.WriteLine ($"Student ID: {g.StudentIDs}");
        }

        // Sorted by club, and then grouped by club -- displaying student's ID's
        var studentIDClub = studentClubList.OrderBy (s => s.ClubName).GroupBy (
            s => s.ClubName).Select (
            g => new 
            {
                ClubName = g.Key,
                StudentIDs = g.Select (s => s.StudentID)
            });
        foreach (var g in studentIDClub)
        {
            Console.WriteLine ($"Student ID: {g.StudentIDs}");
        }

        // Counting the number of students witha a GPA between 2.5 and 4.0
        var studentCount = studentGPAList.Count (s => s.GPA >= 2.5 && s.GPA <= 4.0);
        Console.WriteLine ($"Students with a GPA between 2.5 and 4.0: {studentCount}");

        // Averaging all student's tuition
        var studentTuition = studentList.Average (s => s.Tuition);
        Console.WriteLine ($"\nAverage Tuition: {studentTuition}");

        // Finding the student paying the highest tuition; displays their name, major, and tuition
        var highestTuition = studentList.OrderByDescending (s => s.Tuition).FirstOrDefault ();
        Console.WriteLine ($"\nStudent with the highest tuition: {highestTuition.StudentName}, {highestTuition.Major}, {highestTuition.Tuition}" );

        // Joining the student list and the student GPA list on student ID to display the student's name, major, and GPA
        var studentGPA = from s in studentList
                         join g in studentGPAList on s.StudentID equals g.StudentID
                         select new
                         {
                             StudentName = s.StudentName,
                             Major = s.Major,
                             GPA = g.GPA
                         };
        foreach (var g in studentGPA)
        {
            Console.WriteLine ($"\nStudent Name: {g.StudentName}, Major: {g.Major}, GPA: {g.GPA}");
        }

        // Joining the student list and student club list, exclusively displaying names of students in the Game club
        var studentGameClub = from s in studentList
                              join c in studentClubList on s.StudentID equals c.StudentID
                              where c.ClubName == "Game"
                              select new
                              {
                                  StudentName = s.StudentName,
                                  Major = s.Major,
                                  ClubName = c.ClubName
                              };
        foreach (var g in studentGameClub)
        {
            Console.WriteLine ($"\nStudent Name: {g.StudentName}, Major: {g.Major}, Club: {g.ClubName}");
        }
    }
}