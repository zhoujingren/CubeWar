#if UNITY_EDITOR
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Excel;
using System;
using System.IO;
using System.Data;
using System.Reflection;
using UnityEditor;

public class CreateExcelAssetBundle
{
    public static void CreatExcelAssetBuncle()
    {
        string path = "Assets/Resources";
        string name = "DesignerData";

        DirectoryInfo dirInfo = new DirectoryInfo(path);
        if (!dirInfo.Exists)
        {
            Debug.LogError(string.Format("can't found path={0}", path));
            return;
        }

        TrunkExcelClass ddata = ScriptableObject.CreateInstance<TrunkExcelClass>();
        Type trunkType = ddata.GetType();

        DirectoryInfo designDir = new DirectoryInfo(Application.dataPath + "/../Designers/ExcelData");
        foreach (FileInfo file in designDir.GetFiles())
        {
            string filePath = Application.dataPath + "/../Designers/ExcelData/" + file.Name;
            FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet result = excelReader.AsDataSet();
            foreach (DataTable table in result.Tables)
            {
                int rowCount = table.Rows.Count;
                int columnCount = table.Columns.Count;

                Type tableType = Type.GetType(table.TableName);
                object tableObj = Activator.CreateInstance(tableType);

                Type rowType = Type.GetType(table.TableName + "Row");

                MethodInfo rowListMethod = tableType.GetMethod("AddRow");
                for (int row = 2; row < rowCount; row++)
                {
                    object rowObj = Activator.CreateInstance(rowType);
                    for(int column = 0; column < columnCount; column++)
                    {
                        string value = table.Rows[row][column].ToString();
                        string propertyName = table.Rows[0][column].ToString();
                        string typeName = table.Rows[1][column].ToString();
                        rowType.GetField(propertyName).SetValue(rowObj, ParseObject(typeName, value));
                    }
                    rowListMethod.Invoke(tableObj, new object[] { rowObj });
                }

                trunkType.GetField(table.TableName + "Data").SetValue(ddata, tableObj);
            }
            excelReader.Close();
            stream.Close();
        }

        string assetPath = string.Format("{0}/{1}.asset", path, name);
        AssetDatabase.CreateAsset(ddata, assetPath);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
    public static object ParseObject(string type, string value)
    {
        switch(type)
        {
            case "int":
                return int.Parse(value);
            case "string":
                return value;
            case "float":
                return float.Parse(value);
            default:
                return null;
        }
    }
}
#endif