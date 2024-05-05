using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keybinds : MonoBehaviour
{
    private Dictionary<string, KeyCode> keys = new Dictionary<string, KeyCode>();
    private GameObject currentKey;
    
    
    // Start is called before the first frame update
    void Start()
    {
        keys.Add("Horizontal", KeyCode.D);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
