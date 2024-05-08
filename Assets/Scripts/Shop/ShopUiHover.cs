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
    
    public void PointerEnterReset()
    {
        transform.localScale = new Vector2(2.7f, 1.7f);
    }
    
    public void PointerExitReset()
    {
        transform.localScale = new Vector2(2.5f, 1.5f);
    }

    public void PointerTestEnter()
    {
        transform.localScale = new Vector2(1.2f, 1.2f);
    }

    public void PointerTestExit()
    {
        transform.localScale = new Vector2(1f, 1f);
    }
}
