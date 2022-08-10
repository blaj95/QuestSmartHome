using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightControl : MonoBehaviour
{
    public string lightName;

    private LightTest lightTest;
    
    // Start is called before the first frame update
    void Start()
    {
        lightTest = FindObjectOfType<LightTest>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn()
    {
        lightTest.TurnOn(lightName);
    }
    
    public void TurnOff()
    {
        lightTest.TurnOff(lightName);
    }
}
