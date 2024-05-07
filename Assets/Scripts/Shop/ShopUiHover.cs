using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopUiHover : MonoBehaviour
{
    public void PointerEnter()
    {
        transform.localScale = new Vector2(1.77f, 9.4f);
    }

    public void PointerExit()
    {
        transform.localScale = new Vector2(1.57f, 8.2f);
    }
    
    public void PointerEnterEB()
    {
        transform.localScale = new Vector2(2f, 4.5f);
    }
    
    public void PointerExitEB()
    {
        transform.localScale = new Vector2(1.8f, 4f);
    }
}
