using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingCallback : MonoBehaviour
{
    private bool firstUpdate = true;

    private void Update(){//bu kısım LoaderCallback() fonskiyonunu 1 kez çağırılmasını sağladı.
        if(firstUpdate){
            firstUpdate = false;
            Loader.LoaderCallback();
        }
    }
}
