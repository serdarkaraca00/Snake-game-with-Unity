using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;//bunu ekledik.
using CodeMonkey.Utils;//bunu ekledik, butonu codeMonkeyden aldık diye

public class ScoreWindow : MonoBehaviour
{
    private static ScoreWindow instance;

    public GameObject pauseButton;

    public GameObject resumeButton;

    

    private Text scoreText;//scoreText için başlangıç olarak 0 hiçbiryerde demedik ama unity kendi yapıyo herhalde.

    private void Awake(){
        instance = this;
        scoreText = transform.Find("scoreText").GetComponent<Text>();//scoreText değişkenine dedik ki scoreText deki Text kısmından sen sorumlusun.       

        Score.OnHighscoreChanged += Score_OnHighscoreChanged;
        UpdateHighScore();
    }


    public void pause(){//oyunda pause tuşuna basınca bu fonksiyon çalışıyo
        
        GameHandler.PauseGame();//durdur
        pauseButton.SetActive(false);//pause butonunu ortadan kaldır.
        resumeButton.SetActive(true);//resume button u göster dedik.
    }


    public void resume(){//oyunda resume tuşuna basınca bu fonksiyon çalışıyo

        GameHandler.ResumeGame();
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);
        
    }



    public static void Showresume(){
        instance.resume();
    }




    private void Score_OnHighscoreChanged(object sender, System.EventArgs e){
        UpdateHighScore();
    }


    private void Update(){
        scoreText.text = Score.GetScore().ToString();//burda ekrandaki score u güncelliyoruz. levelgrid de ekleme yapıyoruz.
    }


    private void UpdateHighScore(){
        int highscore = Score.GetHighScore();//high score u atadık.
        transform.Find("highscoreText").GetComponent<Text>().text = "HIGHSCORE\n"+ highscore.ToString();//highscore textini güncelledik.
    }


    public static void HideStatic(){
        instance.gameObject.SetActive(false);
    }
}
