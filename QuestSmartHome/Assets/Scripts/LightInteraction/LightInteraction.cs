using System.Collections;
using System.Collections.Generic;
using com.pison;
using UnityEngine;

public class LightInteraction : MonoBehaviour
{
    public LightPlacement lightPlacement;
    public GameObject selectedLight;
    
    private PisonEvents pisonEvents;
    
    
    private void OnExtension(string gesture)
    {
        Debug.Log(gesture);
        if (gesture == "INDEX")
        {
            selectedLight = lightPlacement.hitLight;
        }

        if (gesture == "HAND")
        {
            if (selectedLight != null)
            {
                selectedLight.GetComponent<LightControl>().TurnOff();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        pisonEvents = FindObjectOfType<PisonEvents>();
        pisonEvents.OnExtension += OnExtension;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
