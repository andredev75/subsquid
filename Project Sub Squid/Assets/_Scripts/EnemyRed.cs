using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRed : MonoBehaviour
{
    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    //private float movetime = 0.8f;

    
    public float vidaInimigo = 10f;
    public float danoSofrido = 3f;
    
    private bool dirRight;

    //private float timer;
    public float velocity = 10.0f;
    public float eixoX;

    public GameObject enemy;

    public Rigidbody rb;

    public float rotationY = 250;

    public float rotationX;

    // TIRO
    public Transform bulletEnemy;

    public bool podeAtirar = false;

    //public GameObject shot;
	public Transform shotSpawnEnemy;

    public float fireRate;
	private float nextFire;

    //particulas explosion
    public ParticleSystem particulaExplosaoPrefab;
    public ParticleSystem particulaSanguePrefab;

    public GameObject[] powerUpDropavelPrefab;

    public int randomPW;

    //
    public Material[] materialsEnemy;

    public Color corDano;

    public Color originColor;

    public float timeCorDano = 0.5f;

    public bool inpulso = false;

    public float superImpulso = 50f;

    public bool megaImpulso = false;


    void Start () 
    {
        enemy.transform.Rotate(rotationX, rotationY, 0);
        rb = this.GetComponent<Rigidbody>();
        

    }

    // Update is called once per frame
    void Update()
    {

        if (!inpulso)
        {
            rb.velocity = new Vector3(-speed, 0, 0);
        }

        if (megaImpulso)
        {
            rb.velocity = new Vector3(-superImpulso, 0, 0);
        }
        
        /*
        if(dirRight)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }

        
        /*timer += Time.deltaTime;
        if(timer >= movetime)
        {
            dirRight = !dirRight;
            timer = 0f;
        }
        */
        //Tiro
        if(Time.time > nextFire && podeAtirar == true)
        {
            nextFire = Time.time + fireRate;
            Instantiate(bulletEnemy, shotSpawnEnemy.position, shotSpawnEnemy.rotation);
        }
        //

        if(vidaInimigo >= 1 && vidaInimigo <= 3 && inpulso == false)
        {   
            
           StartCoroutine(SuperInpulso());
        }

        if(vidaInimigo <= 0)
        {
            if (GameController.contadorEnemyPW >= 5)
            {
                GameController.contadorEnemyPW = 0;
                randomPW = Random.Range(0 ,powerUpDropavelPrefab.Length);
                GameObject powerUpDropavel = Instantiate(this.powerUpDropavelPrefab[randomPW], this.transform.position, Quaternion.identity);

            }

            GameController.instance.SetScore(10);
            GameController.instance.ContadorDeinimigos();
            Destroy(gameObject);
            ParticleSystem particulaExplosao = Instantiate(this.particulaExplosaoPrefab, this.transform.position, Quaternion.identity);
            Destroy(particulaExplosao.gameObject, 1f); // Destr�i a part�cula ap�s 1 segundo
            FindObjectOfType<Audio_menager>().Play("explosion");
        }
        
    }

    IEnumerator SuperInpulso()
    {
        rb.velocity = new Vector3(superImpulso, 0, 0);

        yield return new WaitForSeconds(0.2f);

        inpulso = true;

        megaImpulso = true;

    }

    IEnumerator DanoCor()
    {
        Debug.Log("CorDOTiro");
        GetComponent<Renderer>().materials[0].color = corDano;
        GetComponent<Renderer>().materials[1].color = corDano;
        GetComponent<Renderer>().materials[2].color = corDano;
        GetComponent<Renderer>().materials[3].color = corDano;
        GetComponent<Renderer>().materials[4].color = corDano;
        //GetComponent<Renderer>().materials[5].color = corDano;

        yield return new WaitForSeconds(timeCorDano);

        GetComponent<Renderer>().materials[0].color = materialsEnemy[0].color;
        GetComponent<Renderer>().materials[1].color = materialsEnemy[1].color;
        GetComponent<Renderer>().materials[2].color = materialsEnemy[2].color;
        GetComponent<Renderer>().materials[3].color = materialsEnemy[3].color;
        GetComponent<Renderer>().materials[4].color = materialsEnemy[4].color;
        //GetComponent<Renderer>().materials[5].color = materialsEnemy[5].color;
    }

    void PararDanoCor()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Tiro")
        {
            //FindObjectOfType<Audio_menager>().Play("tiro");
            Debug.Log("levou um tiro do player");
            vidaInimigo = vidaInimigo - 3;
            StartCoroutine(DanoCor());
            ParticleSystem particulaSangue = Instantiate(this.particulaSanguePrefab, this.transform.position, Quaternion.identity);
            Destroy(particulaSangue.gameObject, 0.2f); // Destr�i a part�cula ap�s 1 segundo

        }

        if (other.gameObject.tag == "TiroDuplo")
        {
            Debug.Log("levou um tiro do player");
            vidaInimigo = vidaInimigo - 4;
            StartCoroutine(DanoCor());
            ParticleSystem particulaSangue = Instantiate(this.particulaSanguePrefab, this.transform.position, Quaternion.identity);
            Destroy(particulaSangue.gameObject, 0.2f); // Destr�i a part�cula ap�s 1 segundo
        }

        if (other.gameObject.tag == "TiroTriplo")
        {
            Debug.Log("levou um tiro do player");
            vidaInimigo = vidaInimigo - 3;
            StartCoroutine(DanoCor());
            ParticleSystem particulaSangue = Instantiate(this.particulaSanguePrefab, this.transform.position, Quaternion.identity);
            Destroy(particulaSangue.gameObject, 0.2f); // Destr�i a part�cula ap�s 1 segundo
        }

        if (other.gameObject.tag == "TiroPesado")
        {
            Debug.Log("levou um tiro do Inimigo");
            vidaInimigo = vidaInimigo - 6;
            StartCoroutine(DanoCor());
            ParticleSystem particulaSangue = Instantiate(this.particulaSanguePrefab, this.transform.position, Quaternion.identity);
            Destroy(particulaSangue.gameObject, 0.2f); // Destr�i a part�cula ap�s 1 segundo
            
        }

        if (other.gameObject.tag == "5Tiros")
        {
            Debug.Log("levou um tiro do Inimigo");
            vidaInimigo = vidaInimigo - 1f;
            StartCoroutine(DanoCor());
            ParticleSystem particulaSangue = Instantiate(this.particulaSanguePrefab, this.transform.position, Quaternion.identity);
            Destroy(particulaSangue.gameObject, 0.2f); // Destr�i a part�cula ap�s 1 segundo
            
        }

        if (other.gameObject.tag == "TiroZigZag")
        {
            Debug.Log("levou um tiro do Inimigo");
            vidaInimigo = vidaInimigo - 6f;
            StartCoroutine(DanoCor());
            ParticleSystem particulaSangue = Instantiate(this.particulaSanguePrefab, this.transform.position, Quaternion.identity);
            Destroy(particulaSangue.gameObject, 0.2f); // Destr�i a part�cula ap�s 1 segundo
            
        }
      

        if (other.gameObject.tag == "Player")
        {
            Debug.Log("chocou com o player");
            Destroy(this.gameObject, 0.1f);
            ParticleSystem particulaExplosao = Instantiate(this.particulaExplosaoPrefab, this.transform.position, Quaternion.identity);
            Destroy(particulaExplosao.gameObject, 1f); // Destr�i a part�cula ap�s 1 segundo
            FindObjectOfType<Audio_menager>().Play("explosion");
        }

        if (other.gameObject.tag == "PodeAtirar")
        {
           podeAtirar = true;
        }

    }
}
