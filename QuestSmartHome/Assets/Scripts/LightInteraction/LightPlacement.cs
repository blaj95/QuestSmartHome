using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPlacement : MonoBehaviour
{
    public GameObject lightPrefab, hitLight;
    
    public Transform laser;

    public OVRSkeleton rightHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray wristRay = new Ray(laser.position, -laser.right);
        RaycastHit hit;
        if (Physics.Raycast(wristRay, out hit) && hit.transform.CompareTag("Light"))
        {
            hitLight = hit.transform.gameObject;
        }
        else
            hitLight = null;


        if (OVRInput.GetDown(OVRInput.Button.Two))
        {
            //Spawn Light Box
            Instantiate(lightPrefab,OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch), Quaternion.identity);
        }
        
        // bones = new List<OVRBone>(rightHand.Bones);
        //
        //
        // if (bones.Count > 0)
        // {
        //     for (int i = 0; i < bones.Count; i++)
        //     {
        //         points[i] = bones[i].Transform.position;
        //     }
        //     // laser.position = transform.TransformPoint(bones[3].Transform.position);
        //     //laser.rotation = bones[3].Transform.rotation;
        // }
    }
}
