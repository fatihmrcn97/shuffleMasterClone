using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class CardCollector : MonoBehaviour
{

    public static CardCollector instance;

    public List<GameObject> collection=new List<GameObject>();

    public GameObject createCard;

    private bool isLeft = true;

    public Transform cardHolder;

    private Sequence seq;

    private bool gamePause=false;
     

    [SerializeField] private TextMeshProUGUI leftText; 
    [SerializeField] private TextMeshProUGUI rightText;

    [SerializeField]
    private float timeForShuffle;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    private void Start()
    {
      
        int childCount =transform.childCount;
        for (int i = 1; i < childCount; i++)
        {
            collection.Add(transform.GetChild(i).gameObject);
        }
        leftText.text = "" + collection.Count;
     
    }
    public void StackCards(GameObject cards,int index)
    {
        cards.transform.parent = transform;
        Vector3 newPos = collection[index].transform.localPosition;
        newPos.y += 0.1f;
        cards.transform.localPosition = newPos;
        collection.Add(cards);
    }
 

    public void AddCard(int cardNum)
    {
        for (int i = 0; i < cardNum; i++)
        {
            GameObject card = Instantiate(createCard, collection[collection.Count - 1].transform.position, collection[0].transform.rotation);
            card.transform.SetParent(transform,true); 
            card.transform.localScale = collection[0].transform.localScale;
            Vector3 newAddedCardPos = card.transform.localPosition; 
            newAddedCardPos.y += 0.02f;
            card.transform.localPosition = newAddedCardPos; 
            collection.Add(card);
            
        }
        if (!isLeft)
        {
            leftText.text = "0";
            rightText.text = "" + collection.Count;
        }
        else
        {
            leftText.text = "" + collection.Count;
            rightText.text = "0";
        }
        
    }

    public void RemoveCard(int cardNum)
    {
        if(cardNum > collection.Count)
        {
            // Game over , user dead!
            PlayerController.gamePause = true;
            return;
        }

        for (int i = 0; i < cardNum; i++)
        { 
            Destroy(transform.GetChild(collection.Count - 1).gameObject);
            collection.Remove(collection[collection.Count-1]);
            
        }
        if (!isLeft)
        {
            leftText.text = "0";
            rightText.text = "" + collection.Count;
        }
        else
        {
            leftText.text = "" + collection.Count;
            rightText.text = "0";
        }
    }



    private void shuffleRight()
    {
        float yPos = 0.02f;
        Vector3 newCardRot = new Vector3(0, 90,90);
        timeForShuffle = 0.021f;
        seq = DOTween.Sequence();
        for (int i = 0; i < collection.Count; i++)
        {
            seq.Append(collection[i].transform.DOLocalMoveY(1.65f, timeForShuffle, false)).
                Join(collection[i].transform.DOLocalMoveZ(-1.175f, timeForShuffle)).
                Join(collection[i].transform.DOLocalRotate(newCardRot, timeForShuffle)).
                    Append(collection[i].transform.DOLocalMoveY(yPos, timeForShuffle, false)).
                    Join(collection[i].transform.DOLocalMoveZ(-2.35f, timeForShuffle)).
                    Join(collection[i].transform.DOLocalRotate(new Vector3(0, 90, 180), timeForShuffle));
            yPos += 0.02f;

        }
        Vector3 dene = new Vector3(0,0,-2.3f);
        GetComponent<BoxCollider>().center = dene;
    }
     
    private void shuffleLeft()
    {
        float yPos = 0.02f;
        Vector3 newCardRot = new Vector3(0, 90, 90);
         timeForShuffle = 0.021f;
        seq = DOTween.Sequence();
        for (int i = 0; i < collection.Count; i++)
        {
            seq.Append(collection[i].transform.DOLocalMoveY(1.65f, timeForShuffle, false)).
                Join(collection[i].transform.DOLocalMoveZ(-1.175f, timeForShuffle)).
                Join(collection[i].transform.DOLocalRotate(newCardRot, timeForShuffle)).
                    Append(collection[i].transform.DOLocalMoveY(yPos, timeForShuffle, false)).
                    Join(collection[i].transform.DOLocalMoveZ(0f, timeForShuffle)).
                    Join(collection[i].transform.DOLocalRotate(new Vector3(0, 90, 0), timeForShuffle));
            yPos += 0.02f;

        }
        Vector3 dene = new Vector3(0, 0,0);
        GetComponent<BoxCollider>().center = dene;
    }
   





    public void LeftBtn()
    {
         
        if (!isLeft)
        {
            leftText.text = "" + collection.Count;
            rightText.text = "0";
             shuffleLeft();         
            isLeft = true;
      
        }

    }
    public void RightBtn()
    { 
        if (isLeft)
        {
            leftText.text = "0";
            rightText.text = "" + collection.Count;
            shuffleRight();
            isLeft = false;
        }

    }
 
 




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("finish"))
        {
            PlayerController.gamePause = true;
            shuffleForward();
        }
    }





    private void shuffleForward()
    {
        // first positions =  x= 5 , y=0,z= -0.9    --->   x+=5 , y+=0.5f  z sabit.
        // rotation x =0 to 180
        float x=5, y=0;
        int a = 50;
        float yPos = 0.02f;
        Vector3 newCardRot = new Vector3(180, 90, 0);
        timeForShuffle = 0.07f;
        seq = DOTween.Sequence();
        for (int i = collection.Count-1; i >= 0 ; i--)
        {
            seq.Append(collection[i].transform.DOLocalMoveY(y, timeForShuffle, false)).
                Join(collection[i].transform.DOLocalMoveX(x, timeForShuffle)).
                Join(collection[i].transform.DOLocalMoveZ(-0.9f, timeForShuffle)).
                Join(collection[i].transform.DOLocalRotate(newCardRot, timeForShuffle));
            if (i == a)
            {
                x += 5;
                y += 0.5f;
                a += 10;
            }

        } 


    }





}













//private void TestShuffleRigh()
//{ 
//    Vector3 newCardRot = new Vector3(90, 0, 0);
//    float timeForShuffle = 0.1f;
//    seq = DOTween.Sequence();
//    seq.Append(transform.DOLocalMoveY(1.65f, timeForShuffle, false)).
//    Join(transform.DOLocalMoveZ(-1.175f, timeForShuffle)).
//    Join(transform.DOLocalRotate(newCardRot, timeForShuffle)).
//        Append(transform.DOLocalMoveY(transform.position.y, timeForShuffle, false)).
//        Join(transform.DOLocalMoveZ(-2.35f, timeForShuffle)).
//        Join(transform.DOLocalRotate(new Vector3(180, 0, 0), timeForShuffle));
//}
//private void TestShuffleLeft()
//{
//    Vector3 newCardRot = new Vector3(90, 0, 0);
//    float timeForShuffle = 0.1f;
//    seq = DOTween.Sequence();
//    seq.Append(transform.DOLocalMoveY(1.65f, timeForShuffle, false)).
//    Join(transform.DOLocalMoveZ(-1.175f, timeForShuffle)).
//    Join(transform.DOLocalRotate(newCardRot, timeForShuffle)).
//        Append(transform.DOLocalMoveY(transform.position.y, timeForShuffle, false)).
//        Join(transform.DOLocalMoveZ(0f, timeForShuffle)).
//        Join(transform.DOLocalRotate(new Vector3(0, 90, 0), timeForShuffle));
//}












//if (Input.touchCount > 0)
//{
//    var touch = Input.GetTouch(0);
//    if (touch.position.x < Screen.width / 2)
//    {
//        if (Input.GetTouch(0).phase == TouchPhase.Began)
//        {
//            Debug.Log("Left click");
//        }
//    }
//    else if (touch.position.x > Screen.width / 2)
//    {
//        if (Input.GetTouch(0).phase == TouchPhase.Began)
//        {
//            Debug.Log("Right click");
//        }
//    }
//}

//if (Input.mousePosition.x < Screen.width / 2f)
//{
//    // => left half
//    if(isLeft)
//    {
//        //don't move
//    }
//    else
//    {

//    }

//}
//else
//{
//    // => right half 

//}