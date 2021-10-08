using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntranceTrigger : MonoBehaviour
{
    public string entrySceneName;
    private bool allowEntrance = false;
    public GameObject message;
    public AudioClip audioClip;

    // Start is called before the first frame update
    void Start()
    {
        allowEntrance = false;
        message.SetActive(false);
        GetComponent<AudioSource>().clip = audioClip;

    }

    // Update is called once per frame
    void Update()
    {
        if(allowEntrance == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(entrySceneName);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            allowEntrance = true;
            message.SetActive(true);
            GetComponent<AudioSource>().Play();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            allowEntrance = false;
            message.SetActive(false);
        }
    }
}
