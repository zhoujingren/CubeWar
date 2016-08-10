using UnityEngine;
using System.Collections;

public class CubeManager : MonoBehaviour
{
    public PlayerCustomData CustomData;
    public GameObject Head;
    public GameObject Body;
    public GameObject Hand_L;
    public GameObject Hand_R;
    public GameObject Leg_L;
    public GameObject Leg_R;

    void Update()
    {
        float legW = CustomData.Agility * Global.LegWidthAgilityFactor;
        float legH = CustomData.Agility * Global.LegWidthAgilityFactor * Global.LegHeightWidthFactor;
        float legX = legW / 2f + Global.LegInterval / 2f;
        float legY = legH / 2f;
        Leg_L.transform.localPosition = new Vector3(-legX, legY, 0);
        Leg_L.transform.localScale = new Vector3(legW, legH, legW);
        Leg_R.transform.localPosition = new Vector3(legX, legY, 0);
        Leg_R.transform.localScale = new Vector3(legW, legH, legW);

        float bodyW = CustomData.Defence * Global.BodyWidthDefenceFactor;
        float bodyY = legH + Global.LegBodyInterval + bodyW / 2f;
        Body.transform.localPosition = new Vector3(0, bodyY, 0);
        Body.transform.localScale = Vector3.one * bodyW;

        float handW = CustomData.Strength * Global.HandWidthStengthFactor;
        float handX = bodyW / 2f + Global.HandBodyInterval + handW / 2f;
        float handY = bodyY;
        Hand_L.transform.localPosition = new Vector3(-handX, handY, 0);
        Hand_L.transform.localScale = Vector3.one * handW;
        Hand_R.transform.localPosition = new Vector3(handX, handY, 0);
        Hand_R.transform.localScale = Vector3.one * handW;

        float headW = CustomData.Intelligence * Global.HeadWidthIntelligenceFactor;
        float headY = bodyY + bodyW / 2f + Global.HeadBodyInterval + headW / 2f;
        Head.transform.localPosition = new Vector3(0, headY, 0);
        Head.transform.localScale = Vector3.one * headW;
    }
}