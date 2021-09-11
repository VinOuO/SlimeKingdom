using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {
    public int type = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
