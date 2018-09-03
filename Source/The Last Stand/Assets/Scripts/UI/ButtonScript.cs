using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField]
    private string clickSound = "Button_Click";

    private Button myButton;

    private void Awake()
    {
        myButton = GetComponent<Button>();
    }

    private void Start()
    {
        myButton.onClick.AddListener(TaskOnClick);
    }

    public void TaskOnClick()
    {
        AudioManagerScript.instance.PlaySound(clickSound, name);
    }

}
