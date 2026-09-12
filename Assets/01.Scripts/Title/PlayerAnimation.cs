using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;

  
    public float animationSpeed = 0.5f;

    void Start()
    {
        anim = GetComponent<Animator>();

        anim.speed = animationSpeed;

        anim.Play("player idle");
    }
}