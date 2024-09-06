using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GISCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        newPos = transform.position;
    }

    private Vector3 dragStartPos, dragCurrentPos;
    private Vector3 newPos;
    private float moveTime = 10f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                dragStartPos = ray.GetPoint(distance);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                //获取到这条射线到XZ平面上具体点的位子信息
                dragCurrentPos = ray.GetPoint(distance);
                Vector3 difference = dragStartPos - dragCurrentPos;
                newPos = transform.position + difference;
            }
        }
        transform.position = Vector3.Lerp(transform.position, newPos, moveTime * Time.deltaTime);
    }
}
