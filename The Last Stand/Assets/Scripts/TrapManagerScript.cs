using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManagerScript : MonoBehaviour
{
    [SerializeField]
    private int cost = 250;

    private bool isBought = false;

    [SerializeField]
    private GameObject spikes;
    

    private void OnMouseDown()
    {
        if (isBought == false)
        {
            isBought = true;

            GameManagerScript.instance.UpdateMoney(cost);

            spikes.SetActive(true);

            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }

    public void Deactivate(bool enabled)
    {
        if (isBought == false)
        {
            gameObject.SetActive(enabled);
        }
    }

}
