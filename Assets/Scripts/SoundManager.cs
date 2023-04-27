using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public static class SoundManager {//static yaptık ama neden bilmiyom

    public enum Sound {
        SnakeMove,
        SnakeDie,
        SnakeEat,
        ButtonClick,
        ButtonOver,
    }

    public static void PlaySound(Sound sound) {
        GameObject soundGameObject = new GameObject("Sound");//Sound isminde obje oluşturduk
        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();//oluşan objeye ses özelliği ekledik burda sanırım
        audioSource.PlayOneShot(GetAudioClip(sound));//istenilen sesi buluyo ve onu oynatıyo.
    }

    private static AudioClip GetAudioClip(Sound sound) {//stenilen sesi buluyoruz
        foreach (GameAssets.SoundAudioClip soundAudioClip in GameAssets.instance.soundAudioClipArray) {//gameAssets de arraydeki tüm sesleri döndük, enumlar eşleşirse döndürcez.
            if (soundAudioClip.sound == sound) {//eğer eşleşirse, sesi döndürdük.
                return soundAudioClip.audioClip;
            }
        }
        Debug.LogError("Sound " + sound + " not found!");//o ses yoksa nul döncek ve yazı çıkcak
        return null;
    }



    //bunu mainmenuWindowda ve PauseWindowda çağırdık.
    public static void AddButtonSounds(this Button_UI buttonUI) {//bu this kısmı olayı için adam extension method dedi. Button_UI için yeni bri fonksiyon yapmış olduk. mesla MainMenuClass kısmında AddButtonSounds fonksiyonunu kullanmış olduk. bu this ile bunu sağladık sanırım
        buttonUI.MouseOverOnceFunc += () => SoundManager.PlaySound(Sound.ButtonOver);//mouse ile tuşun üstüne gelirsem bu sesi çal dedik.
        buttonUI.ClickFunc += () => SoundManager.PlaySound(Sound.ButtonClick);//tıklarsamda bunu çal dedik.
    }

}
