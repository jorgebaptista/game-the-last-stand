using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteScript : MonoBehaviour
{
    [SerializeField]
    private string levelName;

    private void Awake()
    {
        if (!PlayerPrefs.HasKey(levelName))
        {
            gameObject.SetActive(false);
        }
    }
}
