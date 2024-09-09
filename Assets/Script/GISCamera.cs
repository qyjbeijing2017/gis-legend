using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class GISCamera : MonoBehaviour
{
    private Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        _camera = GetComponent<Camera>();
        transform.parent = Global.Instance.transform;
        newPos = transform.localPosition;
    }

    private Vector3 dragStartPos, dragCurrentPos;
    private Vector3 newPos;
    private float moveTime = 10f;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Plane plane = new Plane(Global.Instance.transform.up, Global.Instance.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                dragStartPos = ray.GetPoint(distance);
                dragStartPos = Global.Instance.transform.worldToLocalMatrix.MultiplyPoint(dragStartPos);
            }
        }
        if (Input.GetMouseButton(1))
        {
            Plane plane = new Plane(Global.Instance.transform.up, Global.Instance.transform.position);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                //获取到这条射线到XZ平面上具体点的位子信息
                dragCurrentPos = ray.GetPoint(distance);
                dragCurrentPos = Global.Instance.transform.worldToLocalMatrix.MultiplyPoint(dragCurrentPos);
                Vector3 difference = dragStartPos - dragCurrentPos;
                newPos = transform.localPosition + difference;
            }
        }
        float  mouseCenter = Input.GetAxis("Mouse ScrollWheel");
        if(mouseCenter != 0)
        {
            Plane plane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (plane.Raycast(ray, out distance))
            {
                Vector3 point = ray.GetPoint(distance);
                newPos = transform.position + (point - transform.position) * mouseCenter;
            }
        }

        if(transform.localPosition.y < 0.5 && Global.Instance.level < 18) {
            Global.Instance.level += 1;
            transform.localPosition *= 2;
            newPos *= 2;
            dragStartPos *= 2;
            dragCurrentPos *= 2;
        }
        if(transform.localPosition.y > 2 && Global.Instance.level > 1) {
            Global.Instance.level -= 1;
            transform.localPosition /= 2;
            newPos /= 2;
            dragStartPos /= 2;
            dragCurrentPos /= 2;
        }

        if(transform.localPosition.x > 1) {
            Global.Instance.RelativePosition.longitude += 180 / (float)Math.Pow(2, Global.Instance.level - 1);
            transform.localPosition = new Vector3(transform.localPosition.x - 1, transform.localPosition.y, transform.localPosition.z);
            newPos = new Vector3(newPos.x - 1, newPos.y, newPos.z);
            dragStartPos = new Vector3(dragStartPos.x - 1, dragStartPos.y, dragStartPos.z);
            dragCurrentPos = new Vector3(dragCurrentPos.x - 1, dragCurrentPos.y, dragCurrentPos.z);
        }
        if(transform.localPosition.x < -1) {
            Global.Instance.RelativePosition.longitude -= 180 / (float)Math.Pow(2, Global.Instance.level - 1);
            transform.localPosition = new Vector3(transform.localPosition.x + 1, transform.localPosition.y, transform.localPosition.z);
            newPos = new Vector3(newPos.x + 1, newPos.y, newPos.z);
            dragStartPos = new Vector3(dragStartPos.x + 1, dragStartPos.y, dragStartPos.z);
            dragCurrentPos = new Vector3(dragCurrentPos.x + 1, dragCurrentPos.y, dragCurrentPos.z);
        }
        if(transform.localPosition.z > 1) {
            Global.Instance.RelativePosition.latitude += 90 / (float)Math.Pow(2, Global.Instance.level - 1);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z - 1);
            newPos = new Vector3(newPos.x, newPos.y, newPos.z - 1);
            dragStartPos = new Vector3(dragStartPos.x, dragStartPos.y, dragStartPos.z - 1);
            dragCurrentPos = new Vector3(dragCurrentPos.x, dragCurrentPos.y, dragCurrentPos.z - 1);
        }
        if(transform.localPosition.z < -1) {
            Global.Instance.RelativePosition.latitude -= 90 / (float)Math.Pow(2, Global.Instance.level - 1);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z + 1);
            newPos = new Vector3(newPos.x, newPos.y, newPos.z + 1);
            dragStartPos = new Vector3(dragStartPos.x, dragStartPos.y, dragStartPos.z + 1);
            dragCurrentPos = new Vector3(dragCurrentPos.x, dragCurrentPos.y, dragCurrentPos.z + 1);
        }

        if(transform.position.y < _camera.nearClipPlane) {
            newPos.y = _camera.nearClipPlane;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, newPos, moveTime * Time.deltaTime);
    }
}
