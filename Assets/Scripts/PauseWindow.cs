using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PauseWindow : MonoBehaviour
{
    private static PauseWindow instance;//bununla birlike gamehandlerdan burayı kontol ediyoruz. mesela orda diyoruz bu pause windowu aç yada kapa. galiba bu bağlanma durumunda her 2 classda da instance olması gerek.
    

    private void Awake(){
        instance = this;

        transform.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;//bununla birlikte durma ekranı tam oyun ekranının üstüne gelicek.
        transform.GetComponent<RectTransform>().sizeDelta = Vector2.zero;//durma menusunun boyutunu düzenliyo sanırım.

        
        transform.Find("resumeBtn").GetComponent<Button_UI>().ClickFunc = () => ScoreWindow.Showresume();
        transform.Find("resumeBtn").GetComponent<Button_UI>().AddButtonSounds();

        transform.Find("mainMenuBtn").GetComponent<Button_UI>().ClickFunc = () => Loader.Load(Loader.Scene.MainMenu);
        transform.Find("mainMenuBtn").GetComponent<Button_UI>().AddButtonSounds();

        Hide();//başlangıçta gözükmememsi lazım
    }


    private void Show(){
        gameObject.SetActive(true);
    }
  
    private void Hide(){
        gameObject.SetActive(false);
    }


    public static void ShowStatic(){//gameHandlerdan kontrol ettik.
        instance.Show();
    }

    public static void HideStatic(){//gameHandlerdan kontrol ettik.
        instance.Hide();//burda instance dediği bu class, yani beni gösterme diyo
    }
}
