using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TipsRow
{
    public int id;
    public string content;
}

[System.Serializable]
public class Tips
{
    public List<TipsRow> rowList = new List<TipsRow>();

    public void AddRow(TipsRow row)
    {
        rowList.Add(row);
    }
}
