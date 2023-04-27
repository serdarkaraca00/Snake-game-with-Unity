using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;//bunu ekledik, butonu codeMonkeyden aldık diye sanırım.
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    private static GameOverWindow instance;//bunu show fonksiyonu için yaptık diğer türlü ona ulaşamaz mışız.

    private void Awake(){
        instance = this;

        transform.Find("retryBtn").GetComponent<Button_UI>().ClickFunc = () =>{//retrryı bulduk sonra ordan button_UI component ini bulduk, ce tıkla dediğimizde aşağıdakini yap dedik.
            Loader.Load(Loader.Scene.GameScene);//retry a basınca oyun şeyleri baştan yükleniyo. oyun baştan başlıyo yani.
        };

        Hide();//bastık diye butonu görünmez yaptık.
    }


    private void Show(bool isNewHighscore){
        gameObject.SetActive(true);//butonu görünür yaptık

        transform.Find("newHighscoreText").gameObject.SetActive(isNewHighscore);//yeni high score ise ekranda göster dedik.

        transform.Find("scoreText").GetComponent<Text>().text = Score.GetScore().ToString();
        transform.Find("highscoreText").GetComponent<Text>().text = "HIGHSCORE " + Score.GetHighScore();
    }



    private void Hide(){//butonu deaktif eden fonksiyon
        gameObject.SetActive(false);
    }


    public static void ShowStatic(bool isNewHighscore){//yeni highscore ise highscore ekranı gözüktürcek fonskiyonu çağırdık.
        instance.Show(isNewHighscore);
    }
}