using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMap : MonoBehaviour
{
    public Transform player;
    [SerializeField] private Camera minimapCamera;
    private Light[] coinLights;

    void Start()
    {
        // Find all light components on coins
        coinLights = FindObjectsOfType<Light>();
    }

    void OnPreRender()
    {
        if (Camera.current == minimapCamera)
        {
            foreach (var light in coinLights)
            {
                light.enabled = false; // Disable lights for the minimap
            }
        }
    }

    void OnPostRender()
    {
        if (Camera.current == minimapCamera)
        {
            foreach (var light in coinLights)
            {
                light.enabled = true; // Re-enable lights after minimap rendering
            }
        }
    }

    void LateUpdate ()
    {
        Vector3 newPosition = player.position;
        newPosition.y = transform.position.y;
        transform.position = newPosition;

        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
    }


}
