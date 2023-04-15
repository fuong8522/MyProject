using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    public TouchFieldController touchpadField;
    public float speed;

    void Update()
    {
        float horizontalInput = touchpadField.direction.x;
        float verticalInput = touchpadField.direction.y;

        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput);
        if(Input.GetMouseButton(0))
        {
        transform.Translate(movement * speed * Time.deltaTime);

        }
    }
}
