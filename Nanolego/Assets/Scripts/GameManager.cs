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

    private AudioSource audioSource;
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

    private void Start()
    {
        if (GameObject.Find("Player").TryGetComponent<AudioSource>(out AudioSource a))
        {
            audioSource = a;
        }
    }

    public void SetAnim(Animator an)
    {
        anim = an;
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
            StartCoroutine(ChangeColor(m, sceneName));
        }
        else
            SceneManager.LoadScene(sceneName);
    }
    private IEnumerator ChangeColor(Material m, string sceneName)
    {
        m.color = new Color(m.color.r, m.color.g, m.color.b, m.color.a + Time.deltaTime);
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName);
    }

    public void PlayClip(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}