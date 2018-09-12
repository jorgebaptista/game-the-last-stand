using UnityEngine;

public class CreditsScript : MonoBehaviour
{
    public bool winGame;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && winGame) transform.parent.gameObject.SetActive(false);
    }
}