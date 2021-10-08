using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMessage : MonoBehaviour
{
    public GameObject message;
    public float distanceToMessage;
    private float distance;
    public AudioClip audioClip;

    public GameObject player;
    private bool PlaySound;
    // Start is called before the first frame update
    void Start()
    {
        message.SetActive(false);
        PlaySound = false;
        GetComponent<AudioSource>().clip = audioClip;
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= distanceToMessage)
        {
            message.SetActive(true);
            if (PlaySound == false)
            {
                GetComponent<AudioSource>().Play();
                PlaySound = true;
            }
        }
        else
        {
            message.SetActive(false);
            PlaySound = true;

        }
    }
}
