using Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Updown7.Utility;
using Updown7.Gameplay;
using Updown7.UI;

public class _7updown_InputHandler : MonoBehaviour
{
    [SerializeField] _7updown_ChipController chipController;
    public Camera camera;

    // private void Update()
    // {
    //     // if (isTimeUp) return;
    //     if (Input.GetMouseButtonDown(0))
    //     {
    //         // Debug.LogError("Pressed the mouse btn  " + this.gameObject.name);
    //         ProjectRay();
    //     }
    // }

    private void OnMouseDown()
    {
        ProjectRay();
    }

    void ProjectRay()
    {
        Vector3 origin = camera.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(origin, Vector3.forward * 100);
        if (hit.collider != null)
        {
            // Debug.LogError("handle  " + uiHandler.currentChip);
            chipController.OnUserInput(hit.transform, hit.point);
        }

        // RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector3.forward);
        // if( hit.collider != null )
        // {
        //     // Debug.LogError("hit collider ");
        //     chipController.OnUserInput(hit.transform, hit.point);
        // }

    }
}
