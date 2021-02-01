using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMgr : MonoBehaviour
{
    private Transform camTr;
    private Transform tr;
    private Animator anim;

    private Ray ray;
    private RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
        camTr = Camera.main.GetComponent<Transform>();
        tr = GetComponent<Transform>();
        anim  = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(camTr.position, tr.position);

        if (distance <= 0.25f)
        {
            anim.SetBool("IsAttack", true);
        }

        if (distance > 0.2f)
        {
            anim.SetBool("IsTrace", true);
            //회전 
            Quaternion rot = Quaternion.LookRotation(camTr.position - tr.position);
            tr.rotation = Quaternion.Slerp(tr.rotation, rot, Time.deltaTime * 10.0f);
            //이동(전진)
            tr.Translate(Vector3.forward * Time.deltaTime * 0.2f);
        }

        //스크린좌표(x,y) -> Ray
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 2.0f, Color.green);

        if (Physics.Raycast(ray, out hit, 2.0f, 1<<8))
        {
            anim.SetTrigger("Hit");
        }

    }
}
