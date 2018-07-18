using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTestScript : MonoBehaviour
{
    [SerializeField]
    private float cooldown = 1f;

    private float timePassed;

    [SerializeField]
    private GameObject spikes;

    private bool touchingEnemy;

    private void FixedUpdate()
    {
        if (touchingEnemy)
        {
            if (Time.time > timePassed)
            {
                spikes.SetActive(true);

                timePassed = Time.time + cooldown;
            }
        }
    }

}
