using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey;

public class GameHandler : MonoBehaviour
{

    private static GameHandler instance;//bunu diğer sınıflardan ulaşabilmek için yaptık galiba. mesela ScoreWindow dan burdaki fonksiyonlara ulaşdık. yada pausewindow. 

    [SerializeField] private Snake snake;

    private LevelGrid levelGrid;//LevelGrid de yaptığımız classı kullanıcaz burda o yüzden.
    
    private void Awake(){
        instance=this;
        Score.InitalizeStatic();
        GameHandler.ResumeGame();//oyunu durdurup menu diyip tekrar başla dediğimde oyun hareket etmiyodu, bunu ekleyince etmeye başladı.

        //bu aşağıdakinden dolayı high score her başladığında 100 olarak yeniden save oluyodu.
        //PlayerPrefs.SetInt("highscore",100);//key ve value olaıy, highscore keyimiz, 100 value  muz. PlayerPrefs bu değeri kaydediyor, bir kez kaydettikten sonra bu satırı yorum saıtırına alsan bile bana high score u göster dediğimde yine 100ü gösterebilcek.
        PlayerPrefs.Save();
    }

    private void Start(){
        levelGrid = new LevelGrid(20,20);//alanımızı burda girdik

        snake.Setup(levelGrid);
        levelGrid.Setup(snake);//bunları her iki classda da yaptık amaç ne tam anlamaıdm. referans oldu galiba. birbirlerini görüyolar artık sanırım.

    }


    private void Update(){
        if(Input.GetKeyDown(KeyCode.Escape)){//escye basarsam oyun dursun dedik.
            if(IsGamePaused()){//oyun zaten durduysa
                GameHandler.ResumeGame();
            }else{//oyun durmadıysa şimdi dursun dedik
                GameHandler.PauseGame();//niye gamehandlerda olmamıza rağme gamehandler. dedik acaba?
            }
            
        }
    }


    public static void SnakeDied(){//bu fonksiyonu snake clasında state i dead yapınca çağırıyoruz.
        bool isNewHighscore = Score.TrySetNewHighScore();//öldüğümüzdea aldığımız score highscore mu diye kontrol ediyoruz.
        GameOverWindow.ShowStatic(isNewHighscore);//isNewHighscore parametresiyle birlikte yapıyoruz çünkü eğer high score ise ona göre ekran gelicek, değilse ona göre.
        ScoreWindow.HideStatic();
    }

    public static void ResumeGame(){//resume tuşuna basarsam donma ekranını kapa dedik.
        PauseWindow.HideStatic();
        Time.timeScale = 1f;//bununla birlikte oyunun eski halinde devam etmesini sağladık.
    }

    public static void PauseGame(){//durdur dersem onu ekranı gelsin dedik.
        PauseWindow.ShowStatic();
        Time.timeScale = 0f;//bununla birlikte oyun duruyo. zamanı sıfır yaptık diye.
    }


    public static bool IsGamePaused(){//eğer timescale = 0 ise ture döndürüyo
        return Time.timeScale == 0f;
    }

}
