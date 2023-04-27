using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class Score
{
    public static event EventHandler OnHighscoreChanged;

    private static int score;

    public static void InitalizeStatic(){//bunu oyunu yeniden başlat dediğimizde score u sıfırlasın diye yaptık. score u static tanımladığımızdan doalyı tekrar sıfırlamak içiin böyle bişey gerek.
        OnHighscoreChanged = null;
        score=0;
    }


    public static int GetScore(){
        return score;
    }

    public static void AddScore(){
        score += 100;
    }



    public static int GetHighScore(){ 
        return PlayerPrefs.GetInt("highscore",0);//highscore keyinin value sunu döndür dedik, sıfır kısmı ise bizim durumda yok o yüzden 0 dedi, ama nasıl bir durumlarda gerekli bilmiyorum.
    }


    public static bool TrySetNewHighScore(){//diğer classlarda bu şekilde bu fonskiyonu kullanmak için bunu yaptık sanırım, çünkü zaten altta bunu yapan fonskiyon var zaten.
        return TrySetNewHighScore(score);
    }


    public static bool TrySetNewHighScore(int score){//scoreu giricez, eğer highscore dan yüksekse true döndürcek ve highscore u güncellicek.
        int highscore = GetHighScore();
        if(score > highscore){//bizim skordan daha yüksek mi diye baktık.
            PlayerPrefs.SetInt("highscore",score);
            PlayerPrefs.Save();
            if(OnHighscoreChanged != null) OnHighscoreChanged(null, EventArgs.Empty);
            return true;
        } else {
            return false;
        }
    }
}
