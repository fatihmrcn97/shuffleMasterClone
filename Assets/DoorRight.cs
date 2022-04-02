using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRight : MonoBehaviour
{
    [SerializeField] int number;
    [SerializeField] bool isMultiplier;

    [SerializeField] bool isLastDoor;
    [SerializeField] GameObject buttonLeft, buttonRight;
    private int cardNum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Collector"))
        {
            if (isMultiplier)
            { 
                cardNum = number * CardCollector.instance.collection.Count;
                cardNum = cardNum - CardCollector.instance.collection.Count;
                CardCollector.instance.AddCard(cardNum);
        
            }
            else
            {
                CardCollector.instance.AddCard(number);
            }
        }
        if (isLastDoor)
        {
            buttonLeft.SetActive(false);
            buttonRight.SetActive(false);
        }
    }
}
