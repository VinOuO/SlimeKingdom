using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundLibary : MonoBehaviour {

    public SoundGroup[] soundGroups;
    public int a;

    Dictionary<string, AudioClip[]> groupDicitionary = new Dictionary<string, AudioClip[]>();

    void Awake()
    {
        foreach(SoundGroup soundGroup in soundGroups)
        {
            groupDicitionary.Add(soundGroup.groupID, soundGroup.group);
        }
    } 

    public AudioClip GetClipFromName(string name)
    {
        if(groupDicitionary.ContainsKey(name))
        {
            AudioClip[] sounds = groupDicitionary[name];
            return sounds[Random.Range(0, sounds.Length)];
        }
        return null;
    }

    public AudioClip GetClipFromName(string name, int num)
    {
        if (groupDicitionary.ContainsKey(name))
        {
            AudioClip[] sounds = groupDicitionary[name];
            return sounds[num];
        }
        return null;
    }

    [System.Serializable]
    public class SoundGroup
    {
        public string groupID;
        public AudioClip[] group;
    }
}
