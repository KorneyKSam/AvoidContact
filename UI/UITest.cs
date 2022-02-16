using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITest : MonoBehaviour
{
    public Transform UIInventory;
    public Transform UIShop;

    private void Update()
    {
        if (Input.GetMouseButton(0))
        {
            ShowPopup(UIInventory);
        }

        if (Input.GetMouseButton(1))
        {
            ShowPopup(UIShop);
        }
    }

    private void ShowPopup(Transform popup)
    {
        popup.gameObject.SetActive(true);
        popup.SetAsLastSibling();
    }
}
