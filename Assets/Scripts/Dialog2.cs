using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Dialog2 : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;
    public string[] sentences;
    private int index;

    public GameObject trophy;
    public Animator anim;

    void Awake()
    {
       trophy.SetActive(false);
    }


    void start()
    {
      
      anim.SetBool("Won", false);
      anim.GetComponentInChildren<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Type());
            Invoke("LoadMainMenu", 6f);
            Invoke("SpawnTrophy", 0.8f);             
        }
    }
  
     private void SpawnTrophy()
     {
        trophy.SetActive(true);
        anim.SetBool("Won", true);   
     }

    private void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    IEnumerator Type()
    {
        
        foreach(char letter in sentences[index].ToCharArray())
        {
            textDisplay.text += letter;
            yield return new WaitForSeconds(0.02f);        
            gameObject.GetComponent<BoxCollider2D>().enabled = false;          
        }
        
    }

}