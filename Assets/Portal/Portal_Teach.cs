using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AI;

public class Portal_T : MonoBehaviour
{
    public GameObject[] slimes;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            switch (gameObject.name)
            {
                case "Farm_portal":
                    changeToFarm();
                    break;
                case "Main_world_portal":
                    changeToMainWorld();
                    break;
                case "Teach_portal":
                    changeToTeach();
                    break;
            }
        }
    }

    void changeToFarm()
    {
        GameObject.Find("Player").GetComponent<Player>().ExitMainWorld_pos = GameObject.Find("Player").transform.position;
        if (!GameObject.Find("Player").GetComponent<Player>().Slime_farm_loaded)
        {
            SceneManager.LoadScene("Slime_form", LoadSceneMode.Additive);
            GameObject.Find("Player").GetComponent<Player>().Slime_farm_loaded = true;
        }
        slimes = GameObject.FindGameObjectsWithTag("Slime");
        Debug.Log(slimes);
        GameObject.Find("Player").GetComponent<NavMeshAgent>().Warp(Vector3.zero);
        GameObject.Find("Player").transform.eulerAngles = Vector3.zero;
        foreach (GameObject _slime in slimes)
        {
            if (_slime.name != "Slime_throw")
            {
                Debug.Log(_slime.name);
                _slime.GetComponent<NavMeshAgent>().Warp(Vector3.zero);
            }
        }
    }

    void changeToTeach()
    {
        GameObject.Find("Player").GetComponent<Player>().ExitMainWorld_pos = GameObject.Find("Player").transform.position;
        SceneManager.LoadScene("Teach", LoadSceneMode.Additive);

        slimes = GameObject.FindGameObjectsWithTag("Slime");
        Debug.Log(slimes);
        GameObject.Find("Player").GetComponent<NavMeshAgent>().Warp(Vector3.zero);
        GameObject.Find("Player").transform.eulerAngles = Vector3.zero;
        foreach (GameObject _slime in slimes)
        {
            if (_slime.name != "Slime_throw")
            {
                Debug.Log(_slime.name);
                _slime.GetComponent<NavMeshAgent>().Warp(Vector3.zero);
            }
        }
    }

    void changeToMainWorld()
    {
        //SceneManager.LoadScene("Main", LoadSceneMode.Additive);
        slimes = GameObject.FindGameObjectsWithTag("Slime");
        //Debug.Log(slimes);
        GameObject.Find("Player").GetComponent<NavMeshAgent>().Warp(GameObject.Find("Player").GetComponent<Player>().ExitMainWorld_pos + Vector3.forward * 20);
        GameObject.Find("Player").transform.eulerAngles = Vector3.zero;
        foreach (GameObject _slime in slimes)
        {
            if (_slime.name != "Slime_throw")
            {
                Debug.Log(_slime.name);
                _slime.GetComponent<NavMeshAgent>().Warp(GameObject.Find("Player").GetComponent<Player>().ExitMainWorld_pos);
            }
        }
    }
}
