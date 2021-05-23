using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class Keyboard : MonoBehaviour
{
    private List<Transform> keys = new List<Transform>();
    private float offset = 0.2f;
    private bool animIsPlaying;
    public bool animateKeyboard = false;

    [SerializeField] private TextMeshProUGUI textField;
    [SerializeField] public AudioClip[] sounds;
    private AudioSource speaker;
    private string word = "#Вездекод";
    private int wordLen;
    private int iter = 0;
    public void Start()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            keys.Add(transform.GetChild(i));
        }

        wordLen = word.Length;
        speaker = gameObject.GetComponent<AudioSource>();
    }


    public void StartAnim()
    {
        animateKeyboard = true;
    }

    public void StopAnim()
    {
        animateKeyboard = false;
    }
    
    public IEnumerator Animate()
    {
        animIsPlaying = true;
        int keyNumber = Random.Range(0, keys.Count);
        Vector3 startPos = keys[keyNumber].transform.position;
        Vector3 pressedKeyPos = new Vector3(startPos.x, startPos.y - offset, startPos.z);
        keys[keyNumber].transform.position = pressedKeyPos;
        textField.text += word[iter];
        speaker.PlayOneShot(sounds[Random.Range(0, sounds.Length)]);
        yield return new WaitForSeconds(0.2f);
        keys[keyNumber].transform.position = startPos;
        iter++;
        if (iter > wordLen - 1)
        {
            iter = 0;
        }
        animIsPlaying = false;
    }

    private void Update()
    {
        
        if (animateKeyboard)
        {
            if (!animIsPlaying)
            {
                StartCoroutine(Animate());
            }
        }

    }
}