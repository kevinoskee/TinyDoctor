using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLocManager : MonoBehaviour
{

    #region Singleton

    public static TempLocManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject[] tempLoc;
}
