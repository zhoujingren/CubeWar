using UnityEngine;
using System.Collections;

public class MainScript : MonoBehaviour
{
    void Start()
    {
        TrunkExcelClass trunkExcel = Resources.Load("DesignerData") as TrunkExcelClass;
        Debug.LogError(trunkExcel.TipsData.rowList[2].content);
    }
}