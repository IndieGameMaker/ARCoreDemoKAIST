using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleARCore;

public class TouchMgr : MonoBehaviour
{
    private Camera arCamera;
    public GameObject placeObject;

    void Start()
    {
        arCamera = Camera.main;
    }

    void Update()
    {
        Touch touch = Input.GetTouch(0);

        if (Input.touchCount < 1 || touch.phase != TouchPhase.Began)
        {
            return;
        }

        TrackableHit hit;  //RaycastHit
        TrackableHitFlags flags = TrackableHitFlags.Default;

        //스크린 터치 지점에서 Ray 발사
        if (Frame.Raycast(touch.position.x, touch.position.y , flags, out hit))
        {
            //앵커 포인트 : 증강시킬 객체를 고정하는 역할
            var anchor = hit.Trackable.CreateAnchor(hit.Pose);

            //객체를 앵커 포인트에 생성
            GameObject obj = Instantiate(placeObject,
                                         hit.Pose.position,
                                         hit.Pose.rotation,
                                         anchor.transform);

            //생성한 객체를 사용자쪽으로 바라보도록 회전
            // (카메라의위치 - 객체의위치)
            var rot = Quaternion.LookRotation(arCamera.transform.position - hit.Pose.position);
            obj.transform.rotation = Quaternion.Euler(arCamera.transform.position.x,
                                                      rot.eulerAngles.y,
                                                      arCamera.transform.position.z );
        }
    }
}
