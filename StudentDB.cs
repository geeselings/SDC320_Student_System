/************
Name: Samantha Mowery
Date: 5-2-2026
Course: SDC320
Assignment: Course Project
************/
using System.Data.SQLite;

public class StudentDB {
    public static void CreateTable(SQLiteConnection conn) {
        string sql =
            "CREATE TABLE IF NOT EXISTS Students (\n"
            + " ID integer PRIMARY KEY\n"
            + " ,FirstName varchar(20)\n"
            + " ,LastName varchar(40)\n"
            + " ,Phone varchar(15)\n"
            + " ,Email varchar(30)\n"
            + " ,Year varchar(15)\n"
            + " ,GPA real);";

            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
    }

    public static void AddStudent(SQLiteConnection conn, Student s) {
        string sql = string.Format(
            "INSERT INTO Students(FirstName, LastName, Phone, Email, Year, GPA) "
            + "VALUES('{0}', '{1}', '{2}', '{3}', '{4}', {5})",
            s.FirstName, s.LastName, s.Phone, s.Email, s.Year, s.GPA);

            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
    }

    public static void UpdateStudent(SQLiteConnection conn, Student s) {
        string sql = string.Format(
            "UPDATE Students SET FirstName='{0}', LastName='{1}', Phone='{2}', Email='{3}', Year='{4}', GPA={5} WHERE ID = {6}",
            s.FirstName, s.LastName, s.Phone, s.Email, s.Year, s.GPA, s.ID);
        
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static List<Student> GetAllStudents(SQLiteConnection conn) {
        List<Student> students = new List<Student>();
        string sql = "SELECT * FROM Students";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read()) {
            students.Add(new Student(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetString(3),
                rdr.GetString(4),
                rdr.GetString(5),
                rdr.GetDouble(6)
            ));
        }

        return students;
    }

    public static Student GetStudent(SQLiteConnection conn, int id) {
        string sql = string.Format("SELECT * FROM Students WHERE ID = {0}", id);

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        if(rdr.Read()) {
            return new Student(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetString(3),
                rdr.GetString(4),
                rdr.GetString(5),
                rdr.GetDouble(6)
            );
        } else {
            return new Student(-1, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, -1);
        }
    }
}