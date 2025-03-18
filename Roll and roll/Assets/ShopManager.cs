using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel;

    public void OpenShop()
    {
        if (shopPanel.activeInHierarchy)
        {
            return;
        }

        //Setup stuff

        shopPanel.SetActive(true);
    }
}