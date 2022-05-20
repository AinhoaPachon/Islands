using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System.Net.Sockets;
using System.Net;

public class PlayerDetection : MonoBehaviour
{
    public bool live;
    public float manualSpeed;
    public float[] positions;
    DeepTrackingReceiver tracking;

    int currentManualMarkId = 0;
    Vector2 worldLimits = new Vector2(1, 1);

    // Start is called before the first frame update
    void Start()
    {
        positions = new float[4];
        positions[0] = 0.3f;
        positions[1] = 0.7f;
        positions[2] = 0.6f;
        positions[3] = 0.3f;
 
        if (live)
        {            
            tracking = new DeepTrackingReceiver();
            tracking.init();
            tracking.listen();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (live)
        {
            positions = tracking.positions;
        }
        else {

            if (Input.GetKeyUp(KeyCode.R)) currentManualMarkId = 0;
            if (Input.GetKeyUp(KeyCode.G)) currentManualMarkId = 1;
            //Manual movements
            MapMovement(currentManualMarkId);
        }
    }

    private void OnApplicationQuit()
    {
        if (live) tracking.close();
    }


    void MapMovement(int markerId) {

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            positions[markerId*2] += manualSpeed;
            if (positions[markerId * 2] > worldLimits.x) positions[markerId * 2] = worldLimits.x;
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            positions[markerId * 2] -= manualSpeed;
            if (positions[markerId * 2] < 0) positions[markerId * 2] = 0;
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            positions[markerId * 2+1] += manualSpeed;
            if (positions[markerId * 2+1] > worldLimits.y) positions[markerId * 2+1] = worldLimits.y;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            positions[markerId * 2+1] -= manualSpeed;
            if (positions[markerId * 2+1] < 0) positions[markerId * 2+1] = 0;
        }

    }
}