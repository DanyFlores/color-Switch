using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour {

    #region Property    
    public string currentColor;
    public float jumpForce = 9.5f;
    public Rigidbody2D circle;
    public SpriteRenderer sprite;
    public Rigidbody2D rb;
    public Color blue, yellow, pink, purple;

    public static int score = 0;
    public Text scoreText;
    public GameObject[] obsticle;
    public GameObject colorchanger;
    #endregion

    // Use this for initialization
    void Start () {

        setRandomColor();
        StarPlayer(0);       
    }
	
	// Update is called once per frame
	void Update () {
        try
        {
            //Salta el player cuando se preciona space,flechaaariba o clic izquierdo
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.Mouse0))
            {
                StarPlayer(3);           
                circle.velocity = Vector2.up * jumpForce;              
            }

            scoreText.text = score.ToString();
        }
        catch (System.Exception ex)
        {
            throw ex;
        }        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Sumar puntos cunado colisiona con el diamante
        if (collision.tag == "Scored")
        {
            score++;
            Destroy(collision.gameObject);
            int r = Random.Range(0,3);

            switch (r)
            {
                case 0:
                    Instantiate(obsticle[0], new Vector2(transform.position.x, transform.position.y + 7f), transform.rotation);
                    break;
                case 1:
                    Instantiate(obsticle[1], new Vector2(transform.position.x, transform.position.y + 6f), transform.rotation);
                    break;
                case 2:                    
                    if(score > 10)
                    {
                        Instantiate(obsticle[2], new Vector2(transform.position.x, transform.position.y + 6f), transform.rotation);
                    }
                    else
                    {
                        Instantiate(obsticle[0], new Vector2(transform.position.x, transform.position.y + 7f), transform.rotation);
                    }
                    break;
                default:
                    break;
            }
            return;
        }
        //Cambiar de color cuando colisiona con el tag "colorChanger"
        if (collision.tag == "colorChanger")
        {
            setRandomColor();
            Destroy(collision.gameObject);
            Instantiate(colorchanger, new Vector2(transform.position.x, transform.position.y + 7f), transform.rotation);
            return;
        }

        //Regresar a la pantalla Principal cuando la pelota baja al limbo
        if (collision.tag == "Base")
        {
            setRandomColor();
            Destroy(collision.gameObject);
            SceneManager.LoadScene(0);
            return;
        }
     
        //Terminar el juego y resetear puntos
        if (collision.tag != currentColor.ToString())
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            score = 0;
        }             
    }

    //Genera 4 numeros random y cada numeor se asigna a un color
    public void setRandomColor()
    {
        try
        {
            int rand = Random.Range(0, 4);        
            switch (rand)
            {
                case 0:
                    currentColor = "Blue"; //se asigna un color a la propiedad currentColor para comparar al momento de terminar el juego
                    sprite.color = blue; // variable que almacena el color del player al pasar por colorchanger
                    break;
                case 1:
                    currentColor = "Yellow";
                    sprite.color = yellow;
                    break;
                case 2:
                    currentColor = "Pink";
                    sprite.color = pink;
                    break;
                case 3:
                    currentColor = "Purple";
                    sprite.color = purple;
                    break;               
            }
        }
        catch (System.Exception ex)
        {
            throw ex;
        }
    }

    public void StarPlayer(float x)
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = x;
    }
}
