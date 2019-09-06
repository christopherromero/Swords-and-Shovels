using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
   public LayerMask clickableLayer;
    // Swap Cursors per object
    public Texture2D pointer;   // Normal Pointer
    public Texture2D target;    // cursor for clickable objects like the world
    public Texture2D doorway;   // Cursor for doorways
    public Texture2D combat;    // Cursor for combat actions
    public EventVector3 onClickEnvironment;
    
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 50, clickableLayer.value))
        {
            bool door = false;
            bool item = false;

            if(hit.collider.gameObject.tag == "Doorway")
            {
                Cursor.SetCursor(doorway, new Vector2(16,16), CursorMode.Auto);
                door = true;
            }
            else if (hit.collider.gameObject.tag == "Item")
            {
                Cursor.SetCursor(combat, new Vector2(16,16), CursorMode.Auto);
                item = true;
            }
            else
            {
                Cursor.SetCursor(target, new Vector2(16,16), CursorMode.Auto);
            }

            if(Input.GetMouseButtonDown(0))
            {
                if(door)
                {
                    // Opens door
                    Transform doorway = hit.collider.gameObject.transform;

                    onClickEnvironment.Invoke(doorway.position);
                    Debug.Log("DOOR");
                }
                else if(item)
                {
                    // Pick up Item
                    Transform itemPos = hit.collider.gameObject.transform;

                    onClickEnvironment.Invoke(itemPos.position);
                    Debug.Log("ITEM");
                }
                else
                {
                    onClickEnvironment.Invoke(hit.point);
                }
            }
        }
        else
        {
            Cursor.SetCursor(pointer, Vector2.zero, CursorMode.Auto);
        }
    }
}

[System.Serializable]
public class EventVector3 : UnityEvent<Vector3>
{

}