using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager1 : MonoBehaviour
{
    public UnityEvent<Vector3> OnMouseClickInteractable;
    public float rayDistance = 200;
    public LayerMask mask;

    public Texture2D arrow;
    public Texture2D target;
    public Texture2D DoorWay;
    public Texture2D DoorWayBack;

    Ray ray;
    RaycastHit hit;

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * rayDistance, Color.red);

        if (Physics.Raycast(ray, out hit, rayDistance, mask))
        {
            switch (hit.collider.tag)
            {
                case "Rock":
                    Cursor.SetCursor(arrow, Vector3.zero, CursorMode.Auto);
                    break;
                case "DoorWay":
                    Cursor.SetCursor(DoorWay, Vector3.zero, CursorMode.Auto);
                    break;
                case "DoorWayBack":
                    Cursor.SetCursor(DoorWayBack, Vector3.zero, CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }
            if (Input.GetMouseButtonDown(0))
            {
                if (!hit.collider.CompareTag("Rock"))
                {
                    if (hit.collider.CompareTag("DoorWay") || hit.collider.CompareTag("DoorWayBack"))
                    {

                        OnMouseClickInteractable.Invoke(hit.collider.GetComponent<DoorWay>().destination);
                    }
                    else
                    {
                        OnMouseClickInteractable.Invoke(hit.point);
                    }
                }
            }
        }
        else
        {
            Cursor.SetCursor(arrow, Vector3.zero, CursorMode.Auto);
        }
    }
}