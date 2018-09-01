using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapScript : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    public TrapMode trapMode;

    [HideInInspector]
    public bool isAvaiable;

    private void Awake()
    {
        isAvaiable = true;
    }
}