using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoverTooltipScript : MonoBehaviour
{
    [Header("Settings")]
    [Space]
    [SerializeField]
    private GameObject tooltip;
    [SerializeField]
    private float tooltipTimer = 0.5f;

    private void OnEnable()
    {
        tooltip.SetActive(false);
    }

    public void ToggleTooltip(bool toggle)
    {
        if (toggle) Invoke("ShowTooltip", tooltipTimer);
        else
        {
            CancelInvoke();
            tooltip.SetActive(false);
        }
    }

    private void ShowTooltip()
    {
        tooltip.SetActive(true);
    }
}