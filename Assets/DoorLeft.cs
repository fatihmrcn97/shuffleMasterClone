using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLeft : MonoBehaviour
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
                cardNum = CardCollector.instance.collection.Count/number;  
                CardCollector.instance.RemoveCard(cardNum);
            }
            else
            {
            CardCollector.instance.RemoveCard(number);
            }
            if (isLastDoor)
            {
                buttonLeft.SetActive(false);
                buttonRight.SetActive(false);
            }
        }
    }
}
