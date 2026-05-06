/************
Name: Samantha Mowery
Date: 5-2-2026
Course: SDC320
Assignment: Course Project
************/
using System.Data.SQLite;

public class CourseDB {
    public static void CreateTable(SQLiteConnection conn) {
        string sql = 
        "CREATE TABLE IF NOT EXISTS Courses (\n"
        + " Code varchar(6) PRIMARY KEY\n"
        + " ,Category varchar(30)\n"
        + " ,Name varchar(50)\n"
        + " ,MaxCapacity integer\n"
        + " ,TeacherID integer\n)";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void AddCourse(SQLiteConnection conn, Course c) {
        string sql = string.Format(
            "INSERT OR IGNORE INTO Courses(Code, Category, Name, MaxCapacity, TeacherID) "
            + "VALUES('{0}', '{1}', '{2}', {3}, {4})",
            c.Code, c.Category, c.Name, c.MaxCapacity, c.InstructorID);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void UpdateCourse(SQLiteConnection conn, Course c) {
        string sql = string.Format(
            "UPDATE Courses SET Category='{0}', Name='{1}', MaxCapacity={2}, TeacherID={3} "
            + "WHERE Code='{4}'", c.Category, c.Name, c.MaxCapacity, c.InstructorID, c.Code);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void DeleteCourse(SQLiteConnection conn, string code) {
        string sql = string.Format("DELETE from Courses WHERE Code = '{0}'", code);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static List<Course> GetAllCourses(SQLiteConnection conn) {
        List<Course> courses = new List<Course>();
        string sql = "SELECT * FROM Courses";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read()) {
            courses.Add(new Course(
                rdr.GetString(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetInt32(3),
                rdr.GetInt32(4)
            ));
        }

        return courses;
    }

    public static Course GetCourse(SQLiteConnection conn, string code) {
        string sql = string.Format("select * from Courses WHERE Code = '{0}'", code);

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        if (rdr.Read()) {
            return new Course(
                rdr.GetString(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetInt32(3),
                rdr.GetInt32(4)
            );
        } else {
            return new Course(string.Empty, string.Empty, string.Empty, -1, -1);
        }
    }
}