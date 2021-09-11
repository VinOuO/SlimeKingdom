using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_face : MonoBehaviour {

    int i = 0;
    Material face_m;
    public Texture[] face_t = new Texture[2];
    float time_change_face = 0, time_change_face_random = 0;

    void Start () {
        face_m = GetComponent<Renderer>().material;
    }
	
	void Update () {
        if (Time.time - time_change_face >= time_change_face_random)
        {
            Debug.Log("change_face");
            time_change_face = Time.time;
            time_change_face_random = Random.Range(1, 3);
            face_m.mainTexture = face_t[i];
            i++;
            i = i % 2;
        }
	}
}
