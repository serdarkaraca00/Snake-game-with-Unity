using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;//bunu da ekledik.
using System;//bunu da ekledik.

public static class Loader//monobehavior u kaldırdık. ve static yaptık.
{
    public enum Scene{
        GameScene,//rastgele olarak GameScene demedik, unityde en sol üstte olan şeye o ismi verdmiştik taa projenin en başında.
        Loading,
        MainMenu,//bunu da file kısmından build settings de sürükledik.
    }

    private static Action loaderCallbackAction;

    public static void Load(Scene scene){//bunu game handler da çağırdık. tuş basınca çalışcak.

        loaderCallbackAction = () =>{//bu çağırıldığı zaman içindeki kod çalışcak
            SceneManager.LoadScene(scene.ToString());//SceneManager.LoadScene hazır fonksiyon sanırım. 
        };

        SceneManager.LoadScene(Scene.Loading.ToString());//bu scene için File build settings e gittim, add open scenes e tıkladım. bu kısım tuşa basınca yükleniyor ekranının gelmesini sağlıyo.
    }


    public static void LoaderCallback(){//bunu LoaderCallback class ında çağırdık. 
        if(loaderCallbackAction != null){
            loaderCallbackAction();//bununlar birlikte artık loaderCallbackAction ın içindeki kod çalışıyo ve oyun tekrar başladı artık.
            loaderCallbackAction = null;
        }
    }
}
