using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text gameOverText;
    [SerializeField] Button repeatButton;
    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        gameOverText.text = "Game Over";
        gameOverText.color = Color.red;
        repeatButton.gameObject.SetActive(true);

    }
    public void Repeat()
    {
        SceneManager.LoadScene(0);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
