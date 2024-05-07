using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Transform tr;

    private bool rotate_door;

    [SerializeField] private Animator anim;

    public int dir;
    public static GameManager Instance { private set; get; }
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.LogWarning(this.gameObject.name + " had a GameManager script.");
            Destroy(this);
        }
    }

    public void ChangeRotation(bool open)
    {
        anim.SetBool("open", open);
    }
}