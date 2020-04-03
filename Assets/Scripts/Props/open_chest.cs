using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class open_chest : MonoBehaviour
{
    public Sprite chestClosed;
    public Sprite chestOpen;
    private SpriteRenderer chestSR;
    public bool inside;
    public int cont = 0;
    void Start()
    {
        chestSR = GetComponent<SpriteRenderer>(); //Accesing to the Gameobject's Spriterender.

        if(chestSR.sprite == null) //If the GameObject dont load the image at the beggining we want to load the closed chest image.
        {
            chestSR.sprite = chestClosed;
        }
    }


    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("PLayer"))
        {
            inside = true;
        }
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        
       
        if (other.tag.Equals("Player"))
        {
            inside = true;
            if (Input.GetKey(KeyCode.E) && cont <= 0 && inside)
            {
                chestSR.sprite = chestOpen;
                Vector3 endPoint = new Vector3(0.0f, 0.07f, 0f);
                this.transform.position = this.transform.position + endPoint;
                cont++;
            }
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            inside = false;
        }
    }
}
