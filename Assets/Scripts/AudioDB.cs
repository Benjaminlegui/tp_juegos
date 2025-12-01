using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "AudioDB")]
public class AudioDB : ScriptableObject
{
    public List<AudioClipData> player;
    public List<AudioClipData> ui;

    [Header("Music List")]
    public List<AudioClipData> music;

    private Dictionary<string, AudioClipData> clipCollection;

    private void OnEnable()
    {
        clipCollection = new Dictionary<string, AudioClipData>();

        AddToCollection(player);
        AddToCollection(ui);
        AddToCollection(music);
    }

    public AudioClipData Get(string groupName)
    {
        return clipCollection.TryGetValue(groupName, out var data) ? data : null;
    }

    private void AddToCollection(List<AudioClipData> listToAdd)
    {
        foreach (var data in listToAdd)
        {
            if (data != null && clipCollection.ContainsKey(data.audioName) == false)
            {
                clipCollection.Add(data.audioName, data);
            }
        }
    }
}

[System.Serializable]
public class AudioClipData
{
    public string audioName;
    public List<AudioClip> clips = new List<AudioClip>();
    [Range(0f, 1f)] public float volume = 1f;

    public AudioClip GetRandomClip()
    {
        if (clips == null || clips.Count == 0)
        {
            return null;
        }

        return clips[Random.Range(0, clips.Count)];
    }
}
