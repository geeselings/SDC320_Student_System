/************
Name: Samantha Mowery
Date: 5-2-2026
Course: SDC320
Assignment: Course Project
************/

using System.Data.SQLite;

public class CourseStudentDB {
    public static void CreateTable(SQLiteConnection conn) {
        string sql =
        "CREATE TABLE IF NOT EXISTS CourseStudent (\n"
        + " Course varchar(6)\n"
        + " ,Student integer);";

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void AddCourseStudent(SQLiteConnection conn, string code, int id) {
        string sql = string.Format(
            "INSERT INTO CourseStudent(Course, Student) "
            + "VALUES('{0}', {1})", code, id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void DeleteByStudent(SQLiteConnection conn, int id) {
        string sql = string.Format("DELETE from CourseStudent WHERE Student = {0}", id);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void DeleteByCourse(SQLiteConnection conn, string code) {
        string sql = string.Format("DELETE from CourseStudent WHERE Course = '{0}'", code);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static void DeleteCourseStudent(SQLiteConnection conn, string code, int id) {
        string sql = string.Format("DELETE from CourseStudent WHERE Student = {0} AND Course = '{1}'", id, code);
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static List<int> GetStudentsByCourse(SQLiteConnection conn, string code) {
        string sql = string.Format("SELECT * FROM CourseStudent WHERE Course = '{0}'", code);
        List<int> students = new List<int>();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        while (rdr.Read()) {
            students.Add(rdr.GetInt32(1));
        }

        return students;
    }

    public static List<string> GetCoursesByStudent(SQLiteConnection conn, int id) {
        string sql = string.Format("SELECT * FROM CourseStudent WHERE Student = {0}", id);
        List<string> courses = new List<string>();
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read()) {
            courses.Add(rdr.GetString(0));
        }

        return courses;
    }
}