using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator[] animators = new Animator[3];
    private Vector2 dir;
    // Start is called before the first frame update
    void Start()
    {
        dir = GetComponent<playerMovement>().Dir;
    }

    // Update is called once per frame
    void Update()
    {
        dir = GetComponent<playerMovement>().Dir;

        foreach (Animator anim in animators)
        {
            if (dir.magnitude != 0)
                anim.SetBool("Move", true);
            else
                anim.SetBool("Move", false);

            if (dir.x == 1 && dir.y == 1 || dir.x == -1 && dir.y == -1 || dir.x == 1 && dir.y == -1 || dir.x == -1 && dir.y == 1)
            {
                anim.SetFloat("vertical", dir.y);
                anim.SetFloat("horizontal", 0);
            }
            else
            {
                anim.SetFloat("vertical", dir.y);
                anim.SetFloat("horizontal", dir.x);
            }
            // if(dir.y >= 1)
            // {

            // }else if(dir.y <=-1)
            // {

            // }else
            // {

            // }

            // if(dir.x >= 1)
            // {

            // }else if(dir.x <=-1)
            // {

            // }else
            // {

            // }
        }
    }
}
