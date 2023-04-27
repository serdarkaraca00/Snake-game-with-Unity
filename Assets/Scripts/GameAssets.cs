using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;//bunu ekledik. bunu serializable için ekledik.

public class GameAssets : MonoBehaviour//bu classda tüm sprite ları burdan yöneticcez. tüm sprite lar buraya atılcak. diğer sınıflardan burdaki sprite ları çağırcaz sadece.
{
    public static GameAssets instance;//diğer class lardan buna ulaşabilmek içinmiş.

    //bu kısmın herşeyden önce çalışması için ayarlardan bişeyler yaptık. niye ilk illa burası çalışcak tam anlamadım.
    private void Awake(){//bu static olayı ile artık tüm public lere ulaşabildiğimizi söylüyo.
        instance = this;
    }

    public Sprite snakeHeadSprite;//buraya unity de sinek kafasını attık. bunu GameHandler dan çağırdık.
    public Sprite snakeBodySprite;
    public Sprite foodSprite;//elmamız için. LevelGrid de kullandık. 
    public Sprite pauseButton;
    public Sprite resumeButton;


    
    
    public SoundAudioClip[] soundAudioClipArray;//böyle bir array yaptık ve bu arrayi editörde görebiliyoruz. ordan boyutunu da 5 seçtim.

    [Serializable]
    public class SoundAudioClip{//burda da arra5i 5 seçince elementlerin Sound ve auidoClip diye özellikleri belirdi.
        public SoundManager.Sound sound;//editörde, soundManager daki enum başlıkları çıktı seçenek olarak.
        public AudioClip audioClip;//bu kısıma da ben sesleri sürükledim.
    }
}
