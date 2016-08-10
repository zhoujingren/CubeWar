#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Excel;
using System;
using System.IO;
using System.Data;


public class DebugWindow : EditorWindow
{
    private Vector2 mScrollPosition;
    [UnityEditor.MenuItem("ZJRTools/Debug/DebugWindow")]
    static void Init()
    {
        EditorWindow.GetWindow(typeof(DebugWindow)).Show();
    }
    void OnGUI()
    {
        mScrollPosition = GUILayout.BeginScrollView(mScrollPosition, false, true);

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("导出ScriptableObject", GUILayout.Width(300f)))
        {
            ExportScriptableObject();
        }
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        if (GUILayout.Button("生成AssetBundle", GUILayout.Width(300f)))
        {
            CreateExcelAssetBundle.CreatExcelAssetBuncle();
        }
        GUILayout.EndHorizontal();

        GUILayout.EndScrollView();
    }
    private void ExportScriptableObject()
    {
        DirectoryInfo dir = new DirectoryInfo(Application.dataPath + "/../Designers/ExcelData");
        List<string> classList = new List<string>();
        foreach(FileInfo file in dir.GetFiles())
        {
            string filePath = Application.dataPath + "/../Designers/ExcelData/" + file.Name;
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            foreach (DataTable table in result.Tables)
            {
                classList.Add(table.TableName);
                WriteWorkSheetClass(table, table.TableName);
            }
            excelReader.Close();
            stream.Close();
        }
        WriteTrunkExcelExportClass(classList);
    }
    private void WriteWorkSheetClass(DataTable table, string className)
    {
        string path = Application.dataPath + "/Scripts/ExcelExports/" + className + ".cs";
        try
        {
            int columnCount = table.Columns.Count;
            int rowCount = table.Rows.Count;
            FileStream aFile = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(aFile);
            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using UnityEngine;");
            sw.WriteLine();
            sw.WriteLine("[System.Serializable]");
            sw.WriteLine("public class " + className + "Row");
            sw.WriteLine("{");
            for (int i = 0; i < columnCount; i++)
            {
                sw.WriteLine("    public " + table.Rows[1][i].ToString() + " " + table.Rows[0][i] + ";");
            }
            sw.WriteLine("}");
            sw.WriteLine();
            sw.WriteLine("[System.Serializable]");
            sw.WriteLine("public class " + className);
            sw.WriteLine("{");
            sw.WriteLine("    public List<" + className + "Row> rowList = new List<" + className + "Row>();");
            sw.WriteLine();
            sw.WriteLine("    public void AddRow(" + className + "Row row)");
            sw.WriteLine("    {");
            sw.WriteLine("        rowList.Add(row);");
            sw.WriteLine("    }");
            sw.WriteLine("}");
            sw.Close();
            aFile.Close();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return;
        }
    }
    public void WriteTrunkExcelExportClass(List<string> classNames)
    {
        string path = Application.dataPath + "/Scripts/ExcelExports/" + "TrunkExcelClass" + ".cs";
        try
        {
            FileStream aFile = new FileStream(path, FileMode.Create);
            StreamWriter sw = new StreamWriter(aFile);
            sw.WriteLine("using System.Collections.Generic;");
            sw.WriteLine("using UnityEngine;");
            sw.WriteLine();
            sw.WriteLine("public class " + "TrunkExcelClass" + " : ScriptableObject");
            sw.WriteLine("{");
            foreach (string name in classNames)
            {
                sw.WriteLine("    public " + name + " " + name + "Data;");
            }
            sw.WriteLine("}");
            sw.Close();
            aFile.Close();
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex);
            return;
        }
    }
}
#endif