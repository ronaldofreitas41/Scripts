using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Testte : MonoBehaviour
{
	//Variaveis Globais
    public float forcaPulo;
    public float velocidade;   
	public int vidas;
	public int moedas;
	public int municao;
	public bool nochao;
	public bool tiro;
	public bool pegouMu;
	public bool pegouMo;
	public Text TextoMoedas;
	public Text TextoMunicao;
	public Text TextoVidas;
	public AudioClip moedasSom;
	public AudioClip municaoSom;
	public AudioClip puloSom;
	public AudioClip tiroSom;
	

    void Start()
    {
    	/*
    	Nesta funçao o que esta sendo executado ao iniciar o Jogo
    	*/
		TextoMoedas.text = moedas.ToString();
		TextoMunicao.text = municao.ToString();
		TextoVidas.text = vidas.ToString();
    }
		
    
    void Update()
    {	
    	/*
		Nesta funçao o que esta inserido é verificado a todo momento apos a execução do jogo
		O 1º Tipo de variavel é relacionado a rederizaçao de um Objeto
		O 2º Tipo de variavel é relacionado a Física aplicada sobre o personagem 
		O 3º Tipo de variavel é relacionado as animações do personagens
		O 4º Tipo de variavel é relacionado aos sons inclusos na aplicaçao
		O 5º Tipo de variavel é relacionado ao movimento no eixo X do personagem
    	*/
    	SpriteRenderer spriterenderer = GetComponent<SpriteRenderer>();
    	Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
		Animator animacao = GetComponent<Animator>();
		AudioSource som = GetComponent<AudioSource>();
    	float movimento = Input.GetAxis("Horizontal");
    	
        rigidbody.velocity = new Vector2(movimento*velocidade,rigidbody.velocity.y);

        // nesta parte verifica em qual direçao o personagen esta para rotacionar ele no eixo X
        if(movimento < 0)
        {
        	spriterenderer.flipX = true;
        }else if(movimento > 0){
        	spriterenderer.flipX = false;
        }
        /* 
        Nesta parte verificamos se o personagem esta em contato com o chao para ele somente poder dar
        1 pulo alem disso há a bind da tecla Space para que ele pule ao essa tecla ser pressionada
        Além de controlar a animaçao de pulo
         */
		if(nochao){
		animacao.SetBool("Pulando" , false);

		if(Input.GetKeyDown(KeyCode.Space))
        {
			rigidbody.AddForce(new Vector2(0,forcaPulo));
			 som.PlayOneShot(puloSom);
        }

		}else{
		animacao.SetBool("Pulando" , true);

		}
		/*
		O if abaixo verifica se há muniçao além de controlar os tiros e a HitBox da arma do personagem
		fazendo que ao se apretar o LeftControl a animação de tiro é executada, também remove uma munição,
		e destroi os monstros que estão dentro da HitBox
		*/
		if(municao > 0){
			animacao.SetBool("Muniçao",true);
			if(Input.GetKeyDown(KeyCode.LeftControl)){
				animacao.SetTrigger("Atrirar");
				som.PlayOneShot(tiroSom);
				municao--;
				TextoMunicao.text= municao.ToString();
				Collider2D[] colliders = new Collider2D[3];
				transform.Find("HitBox").gameObject.GetComponent<Collider2D>()
				.OverlapCollider(new ContactFilter2D(),colliders);
				for(int i = 0 ;i<=3;i++)
				{
					if(colliders[i] != null && colliders[i].gameObject.CompareTag("Monstro")){
						Destroy(colliders[i].gameObject);
					}
				}		
        	}
      
		}else{
			animacao.SetBool("Muniçao",false);
		}
		/*
		o If abaixo verifica e executa a animação do personagem andando caso ele esteja andando
		*/
        if(movimento > 0 || movimento < 0){
			animacao.SetBool("Andando" , true);
		}else{
			animacao.SetBool("Andando" , false);
		}
		/*
		Os dois if a seguir verifica se o personagem coletou uma moeda ou uma munição assim também executando os 
		sons de coleta de tais itens 
		*/
        if(pegouMo){
        	som.PlayOneShot(moedasSom);
        	pegouMo = false;
        }
        if(pegouMu){
        	som.PlayOneShot(municaoSom);
        	pegouMu = false;
        }
        
		
    }
    /*
	A funçao a seguir verifica se o personagem entrou em contato atraves de um Trigger que é um tipo de colisor que não
	afeta o movimento do personagem ai quando ele entra em contato com cada um dos itens de munição ou moedas ele atualiza
	o indicador na parte superior da tela e remove aquele objeto da tela de play
    */
    private void OnTriggerEnter2D (Collider2D colision2D)  {
    	if( colision2D.gameObject.CompareTag("Moeda1")){
			Destroy(colision2D.gameObject);
			moedas++;
			TextoMoedas.text = moedas.ToString();
			pegouMo = true;
		}
		if( colision2D.gameObject.CompareTag("Moeda2")){
			Destroy(colision2D.gameObject);
			moedas += 2;
			TextoMoedas.text = moedas.ToString();
			pegouMo = true;
		}
		if( colision2D.gameObject.CompareTag("Moeda5")){
			Destroy(colision2D.gameObject);
		    moedas += 5;
		    TextoMoedas.text = moedas.ToString();
		    pegouMo = true;
		}
		if( colision2D.gameObject.CompareTag("Moeda7")){
			Destroy(colision2D.gameObject);
			moedas += 7;
			TextoMoedas.text = moedas.ToString();
			pegouMo = true;
		}
		if( colision2D.gameObject.CompareTag("Municao5")){
			Destroy(colision2D.gameObject);
			municao += 5;
		    TextoMunicao.text = municao.ToString();
		    pegouMu = true;
		}
		if( colision2D.gameObject.CompareTag("Municao10")){
			Destroy(colision2D.gameObject);
			municao += 10;
		    TextoMunicao.text = municao.ToString();
		    pegouMu = true;
		}
		if( colision2D.gameObject.CompareTag("Municao15")){
			Destroy(colision2D.gameObject);
			municao += 15;
		    TextoMunicao.text = municao.ToString();
		    pegouMu = true;
		}
    }
   /*
	Essa função verifica se o personagem colidiu com um monstro ou com o chã atraves de um Collider que é uma colisao
	que afeta o movimento e barra o movimento seja ele em X ou em Y  no caso dos mostros ele verifica se houve o contato
	e se houve um contato diminui em 1 as vidas do personagem e caso essas vidas atingam o 0 ele volta para o começo 
	novamente
   */
	void OnCollisionEnter2D (Collision2D colision2D){
		
		if( colision2D.gameObject.CompareTag("Monstro")){
			vidas--;
			TextoVidas.text = vidas.ToString();
			if(vidas == 0){
				transform.position = new Vector3(1,0,0);
				vidas = 5;
				TextoVidas.text = vidas.ToString();
			}
		}
		
		
		if( colision2D.gameObject.CompareTag("Plataformas")){
			nochao = true;
		}

	}
	// essa funçao abaixo é uma variante apenas para saber se o personagem finalizou o contato com algum dos objetos
	void OnCollisionExit2D(Collision2D colision2D){
 	 	if( colision2D.gameObject.CompareTag("Plataformas")){
		
			nochao = false;
		}
	}
 
}