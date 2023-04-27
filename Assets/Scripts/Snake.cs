using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{

    private enum Direction{//enum ne için tam anlamadım ama sabit değişkenler için sanırım. aşağıda SnakeMovePosition classında bunu çağırdık. 
        Left,
        Right,
        Up,
        Down
    }


    private enum State{//bu enum olayı etiket gibi bişey olarak kullanılıyo herhalde. 
        Alive,
        Dead
    }


    private State state;//üstteki enumu tanımladık.
    private Vector2Int gridPosition;// int olanı kullandık çünkü kordinat şekline tam sayı olarak gidicez, float a gerek yok.
    private Direction gridMoveDirection;//yılanın gittiği yön. Direction türünde yaptık.
    
    private float gridMoveTimerMax;//bu ne kadar sürede bir hareket ediceğimizi belirliyo.
    private float gridMoveTimer;//bu iise üstteki değişkene geldiğimizi anlamamızı sağlıcak.(yani mesela 1 saniye olana kadar artcak bu, 1i geçtiğinde dicezki 1 saniye olmuş hareket edelim.)

    private LevelGrid levelGrid;

    private int snakeBodySize;//sineğin vücut uzunluğu. 0 ise sadece kafası var demektir.
    private List<SnakeMovePosition> snakeMovePositionList;//sineğimize ekleme yapcağımız zaman yerini bilmemiz lazım. bu sineğin yerini tutucak
    private List<SnakeBodyPart> snakeBodyPartList;//yılan gövde sprite ları için sanırım

    private int moveInputCounter=-1;//ardarda hareket tuşuna basıpda yılanın kendisini yemesine engel olmak için.

    public GameObject rightButton;//sol alttaki yön tuşları
    public GameObject leftButton;
    public GameObject upButton;
    public GameObject downButton;
    private static int DirectionFOrArrow = 3;


    private void hideArrows(){//swipe seçinnce bunu çağırcam ki butonlar gözükmesin
        rightButton.SetActive(false);
        leftButton.SetActive(false);
        upButton.SetActive(false);
        downButton.SetActive(false);
    }




    public void rightMove(){
        DirectionFOrArrow=3;
    }
    public void leftMove(){
        DirectionFOrArrow=9;
    }
    public void upMove(){
        DirectionFOrArrow=12;
    }
    public void downMove(){
        DirectionFOrArrow=6;
    }


    
    public void Setup(LevelGrid levelGrid){//bunu GameHandler da çağırdık.
        this.levelGrid = levelGrid;
    }



    private void Awake(){
        gridPosition = new Vector2Int(10,10);//oyun başında yılan ortada başlasın diye.

        gridMoveTimerMax = .2f;//0.2 saniyede bir hareket edicez.
        gridMoveTimer = 0f;
        gridMoveDirection = Direction.Right;//oyun başında sola doğru giderek başlıyo.

        snakeMovePositionList = new List<SnakeMovePosition>();//listemizi başlatıyoruz.
        snakeBodySize = 0;//0 gövde ile başlıyoruz.

        snakeBodyPartList = new List<SnakeBodyPart>();

        state = State.Alive;//oyun başlarken alive dicez tabi ki.
    }



    private void Update(){
        switch (state){
            case State.Alive://bu durumda oyun devam eder.
                HandleInput();//inputu alıp değişkeni değiştiriyoruz.

                HandleGridMovement();//değişen değişkene göre hareketi güncelliyoruz.
                break;
            case State.Dead://bu durumda update de işimiz kalmadığından oyun donar
                break;    
        }
        
    }




    private void HandleInput(){//burda yöne göre gridMoveDirection a değer attık. 

        if(Input.GetKeyDown(KeyCode.UpArrow) && moveInputCounter==-1){//yukarı basınca değişkende değişiklik yapıyoruz. ama yılan hareketini gridPosition ile sağlıyoruz.
            if(gridMoveDirection != Direction.Down){//yılan eğer aşağı gitmiyosa yukarı gidebilir. hiç bir yön için yılan 180 derece dönemez.
                gridMoveDirection = Direction.Up;
                moveInputCounter*=-1;//yön tayin ettik, şimdi hareket olana kadar başka yön tayin edemeyiz.
            } 
        }

        if(Input.GetKeyDown(KeyCode.DownArrow)  && moveInputCounter==-1){
            if(gridMoveDirection != Direction.Up){
                gridMoveDirection = Direction.Down;
                moveInputCounter*=-1;
            }
        }

        if(Input.GetKeyDown(KeyCode.LeftArrow)  && moveInputCounter==-1){
            if(gridMoveDirection != Direction.Right){
                gridMoveDirection = Direction.Left;
                moveInputCounter*=-1;
            }
        }

        if(Input.GetKeyDown(KeyCode.RightArrow)  && moveInputCounter==-1){
            if(gridMoveDirection != Direction.Left){
                gridMoveDirection = Direction.Right;
                moveInputCounter*=-1;
            }
        }



        if(MainMenuWindow.gameOptionIsArrow==false){//kaydırmalıyı seçerse
            hideArrows();//swipe seçtik. o zaman butonlara gerek yok.
            if(Input.touchCount > 0){//0dan başka ekrana input gelirse
                Touch finger = Input.GetTouch(0);
                if(finger.deltaPosition.x > 50.0f && moveInputCounter==-1){//sağa doğru belli miktar piksel kaydırırsam.
                    if(gridMoveDirection != Direction.Left){
                        gridMoveDirection = Direction.Right;
                        moveInputCounter*=-1;
                    }
                }
                if(finger.deltaPosition.x < -50.0f && moveInputCounter==-1){//sola doğru belli miktar piksel kaydırırsam.
                    if(gridMoveDirection != Direction.Right){
                        gridMoveDirection = Direction.Left;
                        moveInputCounter*=-1;
                    }
                }
                if(finger.deltaPosition.y > 50.0f && moveInputCounter==-1){//yukarı doğru belli miktar piksel kaydırırsam.
                    if(gridMoveDirection != Direction.Down){
                        gridMoveDirection = Direction.Up;
                        moveInputCounter*=-1;
                    }
                }
                if(finger.deltaPosition.y < -50.0f && moveInputCounter==-1){//aşağı doğru belli miktar piksel kaydırırsam.
                    if(gridMoveDirection != Direction.Up){
                        gridMoveDirection = Direction.Down;
                        moveInputCounter*=-1;
                    }
                }
            }
        }
        




        if(MainMenuWindow.gameOptionIsArrow==true){//Arrow u seçerse
            if(DirectionFOrArrow== 3 /*&& moveInputCounter==-1*/){
                if(gridMoveDirection != Direction.Left){
                    gridMoveDirection = Direction.Right;
                    //moveInputCounter*=-1;
                }
            }
            if(DirectionFOrArrow== 9 /*&& moveInputCounter==-1*/){
                if(gridMoveDirection != Direction.Right){
                    gridMoveDirection = Direction.Left;
                    //moveInputCounter*=-1;
                }
            }
            if(DirectionFOrArrow== 12 /*&& moveInputCounter==-1*/){
                if(gridMoveDirection != Direction.Down){
                    gridMoveDirection = Direction.Up;
                    //moveInputCounter*=-1;
                } 
            }
            if(DirectionFOrArrow== 6 /*&& moveInputCounter==-1*/){
                if(gridMoveDirection != Direction.Up){
                    gridMoveDirection = Direction.Down;
                    //moveInputCounter*=-1;
                }
            }
        }
        






    }




    private void HandleGridMovement(){
        gridMoveTimer += Time.deltaTime;
        if(gridMoveTimer >= gridMoveTimerMax ){//1 saniyeyi geçtiğimiz anda if e girip hareketi yapıcaz.
            gridMoveTimer -= gridMoveTimerMax;//hareketten sonra sıfılıyoruzki if in içinde tıkılı kalmayalım.


            
            SoundManager.PlaySound(SoundManager.Sound.SnakeMove);//hareket ettik dye onun sesi çalsın dedik.

            SnakeMovePosition previousSnakeMovePosition = null;
            if(snakeMovePositionList.Count > 0){//oyun başında bull olarak başlıyo o yüzden hata olmasın diye sanırım.
                previousSnakeMovePosition = snakeMovePositionList[0];//aşağıda bu 0.indexde olan 1 diye güncellencek  ya o yüzden güncellemeden önce bunu previous diye tuttuk.
            }
            SnakeMovePosition snakeMovePosition = new SnakeMovePosition(previousSnakeMovePosition, gridPosition, gridMoveDirection);//burda her bir gövde parçası için mi obje oluşturuyoruz sanırım
            snakeMovePositionList.Insert(0,snakeMovePosition);//hareketten önce konumumuzu listeye ekledik. burda 0. indexe ekledik ama list mantığu farklı galiba. mesela a var 0da, biz b eklediğimizde b 0da olcak a 1 e itilcek. bunu list otomatik yapıyo galiba. böylece yılanın tüm hareketlerini tutuyoruz, mesela 2 hamle önce nerdeydi filan onu da görebiliriz.

            Vector2Int gridMoveDirectionVector;
            switch(gridMoveDirection){
                default:
                case Direction.Right: gridMoveDirectionVector = new Vector2Int(+1,0);break;//direction.right dersek yılanın sağa gitmesi demek olduğunu söylüyoruz. yani enumda yazdığımız right lefti burda anlamlandırdık, yoksa unity right dediğimizde sağ olduğunu anlamaz tabii ki.
                case Direction.Left: gridMoveDirectionVector = new Vector2Int(-1,0);break;
                case Direction.Up: gridMoveDirectionVector = new Vector2Int(0,+1);break;
                case Direction.Down: gridMoveDirectionVector = new Vector2Int(0,-1);break;
            }

            gridPosition += gridMoveDirectionVector;//gridPosition bizim yılanın konumu, burda konumumuzu güncelledik. ama hareket burda olmadı tabi.

            gridPosition = levelGrid.ValidateGridPosition(gridPosition);//levelGrid deki fonksiyonu çağırdık. gridPosition ekrandan dışardaysa fonksiyona göre yeniden ayarlancak, eğer ekran dışında değlse bişey değişmicek. tüm gövdeler bu süreçden geçicek.

            bool snakeAteFood = levelGrid.TrySnakeEatFood(gridPosition);//levelgrid deki fonksiyonu kullanıyoruz. yediysek true döndürcek. bu setup olayları sayesinde ordaki fonksiyonu kullanabildik sanırım.
            if(snakeAteFood){//eğer yediysek
                snakeBodySize++;
                CreateSnakeBody();//yediğimiz için yeni gövdeyi oluşturduk.
                SoundManager.PlaySound(SoundManager.Sound.SnakeEat);//yedik diye oun sesi çalıyo
            }

            if(snakeMovePositionList.Count >= snakeBodySize +1){//vücut boyumuzdan fazla tuttuğumuz geçtiğimiz yerleri siliyoruz. boşa list de yer tutmasına gerek yok.
                snakeMovePositionList.RemoveAt(snakeMovePositionList.Count -1);
            }

            UpdateSnakeBodyPart();

            foreach(SnakeBodyPart snakeBodyPart in snakeBodyPartList){
                Vector2Int snakeBodyPartGridPosition = snakeBodyPart.GetGridPosition();//gövdenin pozisyonunu atadık.
                if(gridPosition == snakeBodyPartGridPosition){//yeni pozsyonumuz yani kafamız gövdemizin üstüne gelirse
                    state = State.Dead;//öl dedik ve oyun durdu
                    GameHandler.SnakeDied();//ölünce RETRY butonunu ekrana getiriyoruz
                    SoundManager.PlaySound(SoundManager.Sound.SnakeDie);//öldü diye sesi ölüm sesi çaldı
                }
            }

            transform.position = new Vector3(gridPosition.x, gridPosition.y);//hareket burda oluyo.
            transform.eulerAngles = new Vector3(0, 0, GetAngleFromVector(gridMoveDirectionVector) - 90);//sprite ın yönünü değiştiriyoruz. gittiğimiz yere göre kafa da oraya bakcak. unity de rotation kısmında x y ve z var. orda z değişince sprite ın bakcağı yer değişiyo o yüzden burda da x ve y ye gerek yok direk 0 ver, ama z yi özel formülle bulduk.

            UpdateSnakeBodyPart();
            moveInputCounter=-1;//hareket olayı bittiği için -1 yaptık. şimdi yeni yön inputu alabilirim.
            
        }
    }

    
    private void CreateSnakeBody(){
        snakeBodyPartList.Add(new SnakeBodyPart(snakeBodyPartList.Count));//snakebodpart diye obje oluşturduk onu listeye attık.
    }

    private void UpdateSnakeBodyPart(){//gövdelerin konumunu güncelliyoruz.
        for(int i=0; i<snakeBodyPartList.Count; i++){
                snakeBodyPartList[i].SetSnakeMovePosition(snakeMovePositionList[i]);
        }
    }

    private float GetAngleFromVector(Vector2Int dir){//sprite ın yönünü değiştirmek için z rotation unu değiştirmek gerek.  bu formülümsü şeyi açıklamadı adam detaylıca.
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if ( n < 0) n += 360;
        return n;
        
    }


    public Vector2Int GetGridPosition(){//LevelGrid de çağırıyoruz.
        return gridPosition; 
    }


    public List<Vector2Int> GetFullSnakeGridPositionList(){//bu yılanın tüm vücudunun konumlarını liste olarak döndürüyor. bunu levelgrid de çağırdık.
        List<Vector2Int> gridPositionList = new List<Vector2Int>() { gridPosition };//döndürceğimiz listeyi oluşturduk.
        foreach (SnakeMovePosition snakeMovePosition in snakeMovePositionList){//snakeMovePositionList listesini döndük ve onları SnakeMovePosition tipinde snakeMovePosition a attık ve onun sadece konum bilgisini gridPositionList e attık. bunu döngü sayesinde tüm gövdeler için yapıyoruz.
            gridPositionList.Add(snakeMovePosition.GetGridPosition());
        }
        return gridPositionList;    
    }



    private class SnakeBodyPart{//oop mantığında, yılanın her vücudu için obje oluşturmak için class açtık. hem kod  daha düzenli olsun diye.

        private SnakeMovePosition snakeMovePosition;//aşağıda oluşturduğumuz classı tanımladık.
        private Transform transform;

        public SnakeBodyPart(int bodyIndex){
            GameObject snakeBodyGameObject = new GameObject("SnakeBody", typeof(SpriteRenderer));//body için obje oluşturduk.
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.instance.snakeBodySprite;//oluşan objeye sprite ı atadık. 
            
            snakeBodyGameObject.GetComponent<SpriteRenderer>().sortingOrder = -bodyIndex;//burda body le ilgili bir sıralama yaptı ama olay ne anlamadım.(bunsuzda çalışıyo gibi aslında)
            transform = snakeBodyGameObject.transform;
        }

        public void SetSnakeMovePosition(SnakeMovePosition snakeMovePosition){//vücut parçasının konumunu güncellemek için.
            this.snakeMovePosition = snakeMovePosition;
            transform.position = new Vector3(snakeMovePosition.GetGridPosition().x, snakeMovePosition.GetGridPosition().y);//burda konumu değiştiriyoruz. aşağıda ise yönünü.

            float angle;
            switch(snakeMovePosition.GetDirection()){//snakeMovePosition tüm gövdeleri için yöne göre sprite ı döndürüyoruz.
            default: 
                case Direction.Up:
                    switch (snakeMovePosition.GetPreviousDirection()){
                    default: 
                        angle = 0; break;
                    case Direction.Left:
                        angle = 0 + 45; break;
                    case Direction.Right:
                        angle = 0 - 45; break;     
                    }
                    break;   
                case Direction.Down:
                    switch (snakeMovePosition.GetPreviousDirection()){
                    default: 
                        angle = 180; break;
                    case Direction.Left:
                        angle = -45; break;  
                    case Direction.Right:
                        angle = 45; break;    
                    }
                    break;
                case Direction.Left:
                    switch (snakeMovePosition.GetPreviousDirection()){
                    default: 
                        angle = -90; break;
                    case Direction.Down:
                        angle = -45; break;  
                    case Direction.Up:
                        angle = +45; break;      
                    }
                    break;
                case Direction.Right:
                    switch (snakeMovePosition.GetPreviousDirection()){
                    default: 
                        angle = 90; break;
                    case Direction.Down:
                        angle = 45; break;  
                    case Direction.Up:
                        angle = 90 + 45; break;      
                    }
                    break;            
            }
            transform.eulerAngles = new Vector3(0,0,angle);//burda gövdenin yönünü değiştiriyoruz.
        }

        public Vector2Int GetGridPosition(){//bununla bir gövdenin konumunu döndürüyoruz.
            return snakeMovePosition.GetGridPosition();
        }
    }





    private class SnakeMovePosition{//burda yılan gövdesinin hem konumunu hem de yönünü tutuyoruz. ona göre sprite ı döndürücez. her vücut bölgesinin kendisine ait konumu ve yönü olcak o yüzden o mantıkla böyle bir classa gerek oldu sanırım.

        private SnakeMovePosition previousSnakeMovePosition;//bu köşeler için bir değişken. gövdemiz mesela sağa dönücek ama yukardan gelirken mi yoksa aşağıdan gelirken mi sağa döndük, buna göre sprite ın yönüne karar vericez.
        private Vector2Int gridPosition;//yılanın konumu için
        private Direction direction;


        public SnakeMovePosition(SnakeMovePosition previousSnakeMovePosition, Vector2Int gridPosition, Direction direction){
            this.previousSnakeMovePosition=previousSnakeMovePosition;
            this.gridPosition= gridPosition;
            this.direction= direction;
        }


        public Vector2Int GetGridPosition(){
            return gridPosition;
        }

        public Direction GetDirection(){
            return direction;
        }

        public Direction GetPreviousDirection(){
            if(previousSnakeMovePosition == null){//ilk başta meyve yenince gövdenin önceki bir hamlesi yok o yüzden başta null diye hata vermesin diye.
                return Direction.Right;
            }else{
                return previousSnakeMovePosition.direction;
            }
            
        }

    }




    

}
