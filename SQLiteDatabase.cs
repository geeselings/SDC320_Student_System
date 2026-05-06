/************
Name: Samantha Mowery
Date: 5-2-2026
Course: SDC320
Assignment: Course Project
************/

using System.Data.SQLite;

public class SQLiteDatabase {
    public static SQLiteConnection Connect(string database) {
        string cs = @"Data Source=" + database;
        SQLiteConnection conn = new SQLiteConnection(cs);

        try {
            conn.Open();
        } catch (Exception e) {
            Console.WriteLine(e.Message);
        }

        return conn;
    }
}