using System;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    [Range(0f, 1f)] [SerializeField] private float level = 0;
    [SerializeField] private AudioSource good;
    [SerializeField] private AudioSource bad;
    [SerializeField] private AudioSource ugly;

    public float CurrentPolution
    {
        set => level = value;
    }

    private void Update()
    {
        if (level < 0.3f)
        {
            good.mute = false;
            bad.mute = true;
            ugly.mute = true;
        }
        else if (level < 0.6f)
        {
            good.mute = true;
            bad.mute = false;
            ugly.mute = true;
        }
        else
        {
            good.mute = true;
            bad.mute = true;
            ugly.mute = false;
        }
    }
}