using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeManager : MonoBehaviour {

    public int maxCount = 10;

    private static ChargeManager mInstance;
    private int num = 0;

    public static ChargeManager Instance 
    {
        get {
            if( mInstance == null ) {
                GameObject obj = new GameObject("ChargeManager");
                mInstance = obj.AddComponent<ChargeManager>();
            }
            return mInstance;
        }
    }

    public void setCount( int n ) 
    {
        if(n < 0){
            n = 0;
        }
        else if(n > maxCount){
            n = maxCount;
        }
        this.num += n;
    }

    public int getCount() 
    {
        return this.num;
    }

}
