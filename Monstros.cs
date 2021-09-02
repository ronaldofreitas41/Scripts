using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monstros : MonoBehaviour
{
    private bool colide = false;
    private float move = -2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        SpriteRenderer spriterenderer = GetComponent<SpriteRenderer>();
        rigidbody.velocity = new Vector2(move,rigidbody.velocity.y);


        if(colide){
        	Flip();
        }
        if(move < 0 ){
        	spriterenderer.flipX = false;
        }else if(move > 0){
        	spriterenderer.flipX = true;
        }
    }
    private void Flip(){
    	move *= -1;
    	GetComponent<SpriteRenderer>().flipX = GetComponent<SpriteRenderer>().flipX;
    	colide = false;
    }
    void OnCollisionEnter2D (Collision2D colision2D){
		
		if( colision2D.gameObject.CompareTag("Plataformas")){
			colide = true;
		}

	}
	void OnCollisionExit2D(Collision2D colision2D){
 	 	if( colision2D.gameObject.CompareTag("Plataformas")){
		
			colide = false;
		}
	}
}
