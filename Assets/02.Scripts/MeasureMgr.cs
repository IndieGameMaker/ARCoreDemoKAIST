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
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount < 1 || touch.phase != TouchPhase.Began) return;

        if (Frame.Raycast(touch.position.x, touch.position.y, flags, out hit))
        {
            ++tapCount;
            //마커를 생성 - 앵커 하위에 생성
            Anchor anchor = hit.Trackable.CreateAnchor(hit.Pose);

            GameObject marker = Instantiate(markerArrow,
                                            hit.Pose.position,
                                            Quaternion.LookRotation(hit.Pose.up) ,
                                            anchor.transform);     
            //두번째 터치했을 경우              
            if (firstPos != Vector3.zero && tapCount == 2)
            {
                //거리 계산 & 출력
                lenText.text = Vector3.Distance(firstPos, hit.Pose.position).ToString("000.00") + " m";
            }
            firstPos = hit.Pose.position;                       
        }
    }

    public void OnInitLenght()
    {
        GameObject[] markers = GameObject.FindGameObjectsWithTag("MARKER");
        foreach(var _marker in markers)
        {
            Destroy(_marker);
        }
        lenText.text = "0.0 m";
        firstPos = Vector3.zero;
        tapCount = 0;
    }
}
