using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAds : MonoBehaviour
{
    public GameObject superPowerButton;

    public void AddShowAd()
    {
        if (superPowerButton)
        {
            superPowerButton.SetActive(true);
        }
    }

}
