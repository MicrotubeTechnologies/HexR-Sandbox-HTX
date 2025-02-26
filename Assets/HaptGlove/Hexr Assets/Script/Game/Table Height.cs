using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableHeight : MonoBehaviour
{
    public float heightIncrement = 0.03f; // Amount to increase the height

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // Change to any trigger you prefer
        {
            TableHeightUp();
        }
        if (Input.GetKeyDown(KeyCode.M)) // Change to any trigger you prefer
        {
            TableHeightDown();
        }
    }
    public void TableHeightUp()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += heightIncrement;
        transform.position = newPosition;

    }
    public void TableHeightDown()
    {
        Vector3 newPosition = transform.position;
        newPosition.y -= heightIncrement;
        transform.position = newPosition;

    }
}
