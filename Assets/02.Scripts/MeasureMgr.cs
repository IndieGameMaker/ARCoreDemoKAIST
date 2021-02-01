using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleARCore;

public class MeasureMgr : MonoBehaviour
{
    public GameObject markerArrow;
    public Text lenText;

    private TrackableHit hit;
    private TrackableHitFlags flags = TrackableHitFlags.Default;

    private int tapCount = 0;
    private Vector3 firstPos = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
