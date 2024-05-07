using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private Transform tr;

    private bool rotate_door;

    [SerializeField] private Animator anim;

    public int dir;

    [SerializeField] private GameObject fade_ob;
    private bool fade;
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

    public void LoadScene(string sceneName)
    {
        if (fade_ob != null)
        {
            Material m = fade_ob.GetComponent<MeshRenderer>().material;
            StartCoroutine(ChangeColor(m));
        }
        SceneManager.LoadScene(sceneName);
    }
    private IEnumerator ChangeColor(Material m)
    {
        m.color = new Color(m.color.r, m.color.g, m.color.b, m.color.a + Time.deltaTime);
        yield return new WaitForSeconds(3);
    }
}