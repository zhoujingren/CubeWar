using UnityEngine;
using System.Collections;

public class GameApp
{
    private static GameApp mInstance;
    
    public static GameApp GetInstance()
    {
        if (mInstance == null)
        {
            mInstance = new GameApp();
        }
        return mInstance;
    }
}