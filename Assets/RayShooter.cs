using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    private Camera cam;
    private Vector3? hitCoordinates; // Nullable Vector3 to store hit coordinates

    void Start()
    {
        cam = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(2))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    Debug.Log("Target hit");
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                    hitCoordinates = hit.point; // Store hit coordinates
                }
            }
        }
    }

    IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }

    void OnGUI()
    {
        int size = 24; 
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "+");

        if (hitCoordinates != null)
        {
            DisplayHitCoordinates(hitCoordinates.Value);
            hitCoordinates = null; // Reset hit coordinates after displaying
        }
    }

    void DisplayHitCoordinates(Vector3 hitPoint)
    {
        int posX = 20; 
        int posY = 20; 
        GUI.Label(new Rect(posX, posY, 200, 20), "Hit at: " + hitPoint.ToString());
    }
}
