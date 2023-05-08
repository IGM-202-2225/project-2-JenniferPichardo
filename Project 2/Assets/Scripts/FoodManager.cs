using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FoodManager : MonoBehaviour
{
    public static FoodManager Instance;

    //food prefab
    public GameObject foodPrefab;

    //list of foods currently spawned
    private List<GameObject> foods = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> Foods => foods;

    //mouse position
    private Vector2 currentMousePos;

    [HideInInspector]
    public Vector2 maxPosition = Vector2.one;
    [HideInInspector]
    public Vector2 minPosition = -Vector2.one;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        //set up camera dimensions
        Camera cam = Camera.main;

        if(cam != null)
        {
            Vector3 camPosition = cam.transform.position;
            float halfHeight = cam.orthographicSize;
            float halfWidth = halfHeight * cam.aspect;

            maxPosition.x = camPosition.x + halfWidth;
            maxPosition.y = camPosition.y + halfHeight;


            minPosition.x = camPosition.x - halfWidth;
            minPosition.y = camPosition.y - halfHeight;

        }
    }

    // Update is called once per frame
    void Update()
    {
        //update current mouse position
        currentMousePos = Mouse.current.position.ReadValue();
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(currentMousePos);
        objectPos.z = 0;

        //spawn food on mouse click
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            foods.Add(Instantiate(foodPrefab, objectPos, Quaternion.identity));
        }
    }

}
