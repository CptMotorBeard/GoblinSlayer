using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public int value;
    public Vector2 destination;

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, destination, Time.deltaTime);
    }
}
