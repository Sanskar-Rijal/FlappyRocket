using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    Vector3 StartingPosition;
    [SerializeField]Vector3 MovementVector;
    [SerializeField][Range(0,1)] float MovementFactor; // 0 for not moved, 1 for fully moved
    [SerializeField] float period = 2f;

     private void Start() 
     {
        StartingPosition = transform.position;
     }
    


    private void Update() 
    {
        //If period is zero, then our app will creash 
        if (period <= Mathf.Epsilon)
        {
            return; 
        }
        float cycles = Time.time / period ; // time.time means how much time has elapsed
        const float tau = Mathf.PI * 2; //Constant value of 6.283
        float rawSinWave = Mathf.Sin(cycles * tau); // going from -1 to 1 

        //we want our movement factor to remain from 0 to 1 so we add 1 to the rawSinWave and divide it by 2
        //-1+1 = 0 and 0/2 = 0 
        MovementFactor = (rawSinWave + 1f) / 2f;


        Vector3 offset = MovementVector * MovementFactor;
        //all the object which has this script will have their position value changed.
        transform.position = StartingPosition + offset;
    }


}
