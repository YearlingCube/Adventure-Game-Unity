using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Saving : MonoBehaviour
{
    public static int SavedLevel;
    static string file = "dataLevel.txt";
    public static void Save()
    {
        if (File.Exists(file))
        {
            File.Delete(file);
        }
        FileStream fs = File.Create("dataLevel.txt");
        var sw = new StreamWriter(fs);
        sw.Write(GameManager.Level);
        sw.Close();
    }
    public static void Load()
    {
        FileStream fs = File.OpenRead("dataLevel.txt");
        var sw = new StreamReader(fs);
        SavedLevel = int.Parse(sw.ReadLine());
        Debug.Log("Saved Level" + SavedLevel);
        sw.Close();
    }
    public static void ResetLevel()
    {
        if (File.Exists(file))
        {
            File.Delete(file);
        }
        FileStream fs = File.Create("dataLevel.txt");
        var sw = new StreamWriter(fs);
        sw.Write("3");
        sw.Close();
        SavedLevel = 3;
    }
}