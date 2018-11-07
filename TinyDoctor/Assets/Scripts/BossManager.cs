using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    #region Singleton

    public static BossManager instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject boss;
}
