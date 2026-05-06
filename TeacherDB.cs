/************
Name: Samantha Mowery
Date: 5-2-2026
Course: SDC320
Assignment: Course Project
************/
using System.Data.SQLite;

public class TeacherDB {
    public static void CreateTable(SQLiteConnection conn) {
        string sql =
            "CREATE TABLE IF NOT EXISTS Teachers (\n"
            + " ID integer PRIMARY KEY\n"
            + " ,FirstName varchar(20)\n"
            + " ,LastName varchar(40)\n"
            + " ,Phone varchar(15)\n"
            + " ,Email varchar(30)\n"
            + " ,Honorific varchar(10));";

            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
    }

    public static void AddTeacher(SQLiteConnection conn, Teacher t) {
        string sql = string.Format(
            "INSERT INTO Teachers(FirstName, LastName, Phone, Email, Honorific) "
            + "VALUES('{0}', '{1}', '{2}', '{3}', '{4}')",
            t.FirstName, t.LastName, t.Phone, t.Email, t.Honorific);

            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = sql;
            cmd.ExecuteNonQuery();
    }

    public static void UpdateTeacher(SQLiteConnection conn, Teacher t) {
        string sql = string.Format(
            "UPDATE Teachers SET FirstName='{0}', LastName='{1}', Phone='{2}', Email='{3}', Honorific='{4}' WHERE ID = {5}",
            t.FirstName, t.LastName, t.Phone, t.Email, t.Honorific, t.ID);
        
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;
        cmd.ExecuteNonQuery();
    }

    public static List<Teacher> GetAllTeachers(SQLiteConnection conn) {
        List<Teacher> teachers = new List<Teacher>();
        string sql = "SELECT * FROM Teachers";
        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        while(rdr.Read()) {
            teachers.Add(new Teacher(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetString(3),
                rdr.GetString(4),
                rdr.GetString(5)
            ));
        }

        return teachers;
    }

    public static Teacher GetTeacher(SQLiteConnection conn, int id) {
        string sql = string.Format("SELECT * FROM Teachers WHERE ID = {0}", id);

        SQLiteCommand cmd = conn.CreateCommand();
        cmd.CommandText = sql;

        SQLiteDataReader rdr = cmd.ExecuteReader();

        if(rdr.Read()) {
            return new Teacher(
                rdr.GetInt32(0),
                rdr.GetString(1),
                rdr.GetString(2),
                rdr.GetString(3),
                rdr.GetString(4),
                rdr.GetString(5)
            );
        } else {
            return new Teacher(-1, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
        }
    }
}