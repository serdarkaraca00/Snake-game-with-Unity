using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;//bunu ekledik.

public class MainMenuWindow : MonoBehaviour
{
    private enum Sub{//ana menu ve nasıl oynanır menusu için bunu açtık.
        Main,
        HowToPlay,
        HowDYWTP,//nasıl oynamak isstersin
    }

    public static bool gameOptionIsArrow;//true ise arrow, değil ise swipe

    private void Awake(){//awake galiba bir tepki gelince çaılışıyo, çünkü sürekli çalışsaydı, ben mesela howtopplay e basınca tama o fonksiyonla birlikte ekran geldi ama altına yine main menu yu göster fonksiyonu var. onunda hemen arkassından çalışması gerekirdi. awake hazırolda bekliyo, doğru input gelince hemen tepki veriyo sanırım.
        transform.Find("howToPlaySub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;//howtoplay ekranını oyun ekranımızın üstüne getirdik(tasarlarken başka tarafta tasarlamıştık ama burda sıfırladığımız sürece sorun olmuyo.)
        transform.Find("mainSub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;//ana ekranın konumu sıfrıladık.
        transform.Find("HowDYWTPSub").GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        

        

        //buna basınca direk oyun başlıyo
        transform.Find("mainSub").Find("playBtn").GetComponent<Button_UI>().ClickFunc = () => ShowSub(Sub.HowDYWTP);//playBtn a tıklayınaca oyunu yükle dedik.
        transform.Find("mainSub").Find("playBtn").GetComponent<Button_UI>().AddButtonSounds();


        //oynayış olarak arrow la oynamayı seçme
        transform.Find("HowDYWTPSub").Find("ArrowOption").Find("ArrowKeysButton").GetComponent<Button_UI>().ClickFunc = () => ArrowChosen();
        transform.Find("HowDYWTPSub").Find("ArrowOption").Find("ArrowKeysButton").GetComponent<Button_UI>().AddButtonSounds();


        //kaydırarak oynamayı seçme
        transform.Find("HowDYWTPSub").Find("SwipeOption").Find("SwipeButton").GetComponent<Button_UI>().ClickFunc = () => SwipeChosen();
        transform.Find("HowDYWTPSub").Find("SwipeOption").Find("SwipeButton").GetComponent<Button_UI>().AddButtonSounds();


        transform.Find("mainSub").Find("quitBtn").GetComponent<Button_UI>().ClickFunc = () => Application.Quit();//buna tıklayınca oyun masaüstüne gidiyo. unityde tabi bu olmuyo.
        transform.Find("mainSub").Find("quitBtn").GetComponent<Button_UI>().AddButtonSounds();

        transform.Find("mainSub").Find("HowToPlayBtn").GetComponent<Button_UI>().ClickFunc = () => ShowSub(Sub.HowToPlay);//howtoplay tuşuna basarsam nasıl oynanır ekranı görünür olsun dedik.
        
        transform.Find("mainSub").Find("HowToPlayBtn").GetComponent<Button_UI>().AddButtonSounds();

        transform.Find("howToPlaySub").Find("backBtn").GetComponent<Button_UI>().ClickFunc = () => ShowSub(Sub.Main);//nasıl oynanır ekranında back e basarsam ana ekran göster dedik, zaten fonskiyonu çağırınca howtoplay i kapat demiştik.
        transform.Find("howToPlaySub").Find("backBtn").GetComponent<Button_UI>().AddButtonSounds();

        ShowSub(Sub.Main);//ilk başta ana ekran ekrana geliyor.
    }
    


    private void ShowSub(Sub sub){//bu fonksiyon istenilen menuyu ekranda göstericek.
        transform.Find("mainSub").gameObject.SetActive(false);//başta ikisinide kapıcam, aşağıda istenileni göstercem. çünkü asla ikisinide aynı anda göster demicem.
        transform.Find("howToPlaySub").gameObject.SetActive(false);
        transform.Find("HowDYWTPSub").gameObject.SetActive(false);

        switch(sub) {
        case Sub.Main:
            transform.Find("mainSub").gameObject.SetActive(true);
            break;
        case Sub.HowToPlay:
            transform.Find("howToPlaySub").gameObject.SetActive(true);
            break;    
        case Sub.HowDYWTP:
            transform.Find("HowDYWTPSub").gameObject.SetActive(true);
            break;     
        }
        

    }






    private void ArrowChosen(){//eğer arrow la oynamayı seçerse bu fonk çağrılıyo.
        Loader.Load(Loader.Scene.GameScene);
        gameOptionIsArrow=true;

    }


    private void SwipeChosen(){//eğer kaydırarak oynamayı seçerse bu fonk çağrılıyo.
        Loader.Load(Loader.Scene.GameScene);
        gameOptionIsArrow=false;

    }

    

}
