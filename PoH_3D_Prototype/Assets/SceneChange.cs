using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneChange : MonoBehaviour
{
    public AudioClip sound;
    AudioSource audioSource;
    public string Scene_01;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = sound;
    }
    public void ChangeScene1Button()
    {
        audioSource.Play();
        StartCoroutine(LoadNextScene());
    }
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        SceneManager.LoadScene(Scene_01);
    }
    // Update is called once per frame
    void Update()
    {

    }
}