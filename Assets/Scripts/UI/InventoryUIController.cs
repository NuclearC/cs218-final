using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUIController : MonoBehaviour
{
    [SerializeField] Image[] inventoryImages;
    // Start is called before the first frame update
    private int lastItem = -1;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetCurrentItem(int index)
    {
        if (lastItem != index)
        {
            print(index);
            for (int i = 0; i < inventoryImages.Length; i++)
            {
                if (inventoryImages[i])
                    if (i == index)
                    {
                        inventoryImages[i].color = Color.yellow;
                    }
                    else
                    {
                        inventoryImages[i].color = Color.white;
                    }
            }
            lastItem = index;
        }
    }
}
