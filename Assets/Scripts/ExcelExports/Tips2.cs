using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tips2Row
{
    public int id;
    public string content;
}

[System.Serializable]
public class Tips2
{
    public List<Tips2Row> rowList = new List<Tips2Row>();

    public void AddRow(Tips2Row row)
    {
        rowList.Add(row);
    }
}
