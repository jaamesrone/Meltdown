using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{

    public int powerOutput;
    public int income;
    public int availMoney;
    public int volatility;

    // Start is called before the first frame update
    void Start()
    {
        powerOutput = 1;
        income = 1;
        availMoney = 10;
        volatility = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
