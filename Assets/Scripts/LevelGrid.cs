using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    private Vector2Int foodGridPosition;
    private GameObject foodGameObject;//yemek objesi
    private int width;
    private int height;
    private Snake snake;

    public LevelGrid(int width, int height){ //GameHandler da bunu kullanıcaz.
        this.width= width;
        this.height = height;

       //SpawnFood();  ilk spawnfood burdaydı ama referans oalyını seup da yapıyoruz ilk olarak. o yüzden spawn derken snake classına ilk başta ulaşamayız o yüzden bunu aşağı taşıdık.
    }

    
    public void Setup(Snake snake){
        this.snake=snake;

        SpawnFood();
    }





    private void SpawnFood(){
        do{
            foodGridPosition = new Vector2Int(Random.Range(0, width), Random.Range(0 , height));//elma için verilen aralıklarda bir konum belirliyoruz.
        } while(snake.GetFullSnakeGridPositionList().IndexOf(foodGridPosition) != -1);//üstüste gelme durumunu kontorl ediyoruz. vücuda denk gelmeyene kadar döncek. üstteki do kısmında ilk rastgele konum belirledi, sonra burda baktık şartı sağlıyomuyuz, eğer üst üste çıkarsa tekrar  do ya gidicez ve sonra yine buraya gelicez.
        

        foodGameObject = new GameObject("Food", typeof(SpriteRenderer));//elma için yeni objeyi oluşturduk. ve adını ve tipini belirttik.
        foodGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.foodSprite;//objemizin sprite ı için bu kısım. instance sayesinde gameAssets kısmından yararlandık.
        foodGameObject.transform.position = new Vector3(foodGridPosition.x, foodGridPosition.y);//burda objemizin yerini değiştiriyoruz.
    }


    public bool TrySnakeEatFood(Vector2Int snakeGridPosition){//(eski adı SnakeMoved ve void den bool a çevirdik.)bunu Snake de çağırdık. snake in pozisyonu orda tutuluyo zaten.
        if(snakeGridPosition == foodGridPosition){// üstüste gelirse yedik demekttir.
            Object.Destroy(foodGameObject);//yersek elmayı yokettik.
            SpawnFood();//ve hemen yenisini spawnladık.
            Score.AddScore();//score a ekleme yaptık
            return true;//yedik yemeği o yüzden true
        }else{
            return false;
        }
    }


    public Vector2Int ValidateGridPosition(Vector2Int gridPosition){
        if(gridPosition.x < 0){//ekranın soluna çıkarsak.
            gridPosition.x = width - 1;//width yani tam ekran -1 ile tekrar en sağ olarak konumu güncellemiş olduk. bu tüm gövdeler için vs. otomatik olucak.
        }
        if(gridPosition.x > width -1){
            gridPosition.x = 0;
        }
        if(gridPosition.y < 0){
            gridPosition.y = height - 1;
        }
        if(gridPosition.y > height -1){
            gridPosition.y = 0;
        }
        return gridPosition;
    }
    
}
