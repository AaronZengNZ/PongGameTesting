
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ball : MonoBehaviour
{
    public float speed = 1f;
    public Rigidbody rb;
    bool accelerate = true;
    public float scorePlayer = 0;
    public float scoreAi = 0;
    public TextMeshPro scoreText;
    public TextMeshPro shadowText;
    public float boundY = 4.25f;
    public float speedUp = 1f;
    float bouncesSinceLast = 0f;
    float timeSinceScore = 0f;
    public DialogText dt;
    public string[] smugLoseTexts = new string[3];
    float smugness = 0f;
    public string[] loseTexts = new string[3];
    public string[] winTexts = new string[3];
    public string[] winstreakTexts = new string[3];
    float winstreak = 0f;
    public string[] miscTexts;
    public GameObject settings;
    // Start is called before the first frame update
    void Start()
    {
        ApplyforceToRb();
    }

    void Update()
    {
        scoreText.text = scorePlayer.ToString() + " : " + scoreAi.ToString();
        shadowText.text = scorePlayer.ToString() + " : " + scoreAi.ToString();
        Bound();
        //if settings is active, set velocity to 0 and do a apply force to rb
        if (settings.activeSelf == true)
        {
            rb.velocity = new Vector3(0, 0, 0);
            ApplyforceToRb();
        }
    }

    void ApplyforceToRb()
    {
        StartCoroutine(ApplyForceCheck());
    }

    IEnumerator ApplyForceCheck(){
        //wait until settings is inactive
        while(settings.activeSelf == true){
            yield return new WaitForSeconds(0.1f);
        }
        //send the ball a random direction with 3 speed
        float yRand = Random.Range(-3f, 3f);
        //make xRand be the 3 - absolute of yrand
        float xRand = 6 - Mathf.Abs(yRand);
        //apply velocity
        rb.velocity = new Vector3(xRand, yRand, 0);
    }

    //on collision, alter y velocity to make ball bounce away from the object
    private void OnCollisionEnter(Collision other)
    {
        //if other has tag paddle, bounce from other
        if (other.gameObject.CompareTag("Paddle"))
        {
            bouncesSinceLast = 0f;
            BounceFromOther(other.transform);
        }
        //if other has tag playergoal or aigoal
        else if (other.gameObject.CompareTag("PlayerGoal"))
        {
            Score("ai");
        }
        else if (other.gameObject.CompareTag("AiGoal"))
        {
            Score("player");
        }
    }

    private void Score(string target)
    {
        if (target == "player")
        {
            scorePlayer++;
            winstreak++;
            if (smugness >= 3f)
            {
                dt.TypeText(miscTexts[2]);
            }
            else if(winstreak >= 2){
                if (winstreak > winstreakTexts.Length)
                {
                    dt.TypeText(winstreakTexts[winstreakTexts.Length - 1]);
                }
                else
                {
                    GameObject.Find("AiPaddle").GetComponent<AiPaddle>().SpeedIncrease();
                    dt.TypeText(winstreakTexts[(int)winstreak - 2]);
                    if (winstreak >= winstreakTexts.Length)
                    {
                        GameObject.Find("AiPaddle").GetComponent<AiPaddle>().AI = true;
                    }
                }
            }
            else if (Mathf.Abs(rb.velocity.y) > 12f)
            {
                dt.TypeText(miscTexts[3]);
            }
            else
            {
                int rand = UnityEngine.Random.Range(0, winTexts.Length);
                dt.TypeText(winTexts[rand]);
            }
            smugness = 0;
        }
        else if (target == "ai")
        {
            scoreAi++;
            smugness++;
            if (smugness >= 2f)
            {
                if (smugness > smugLoseTexts.Length)
                {
                    dt.TypeText(smugLoseTexts[smugLoseTexts.Length - 1]);
                }
                else
                {
                    dt.TypeText(smugLoseTexts[(int)smugness - 2]);
                }
            }
            else if(winstreak >= 5){
                GameObject.Find("AiPaddle").GetComponent<AiPaddle>().AI = false;
                dt.TypeText(miscTexts[5]);
            }
            else if(winstreak >= 3){
                dt.TypeText(miscTexts[4]);
            }
            else
            {
                int rand = UnityEngine.Random.Range(0, loseTexts.Length);
                dt.TypeText(loseTexts[rand]);
            }
             winstreak = 0f;
        }
        transform.position = new Vector3(0, 0, 0);
        //revert velocity
        rb.velocity = new Vector3(0, 0, 0);
        //waitandaddforce
        StartCoroutine(WaitAndAddForce());
    }

    IEnumerator WaitAndAddForce()
    {
        yield return new WaitForSeconds(1f);
        ApplyforceToRb();
        speed = 3f;
    }

    private void BounceFromOther(Transform other)
    {
        //get the direction of the ball from the paddle
        Vector3 prevDir = transform.position - other.position;
        //absolute the direction
        Vector3 dir = new Vector3(Mathf.Abs(prevDir.x), Mathf.Abs(prevDir.y), 0);
        //normalise the direction
        dir = dir.normalized;
        float up = -1;
        if (this.transform.position.y > other.transform.position.y)
        {
            up = 1;
        }
        //add force to the ball
        if (accelerate == true)
        {
            speed = speed * 1.2f;
            //increase x velocity based on paddle hit
            if (other.name == "PlayerPaddle")
            {
                rb.velocity = new Vector3(rb.velocity.x + speedUp, rb.velocity.y + dir.y * 6f * up, 0);
            }
            else if (other.name == "AiPaddle")
            {
                rb.velocity = new Vector3(rb.velocity.x - speedUp, rb.velocity.y + dir.y * 6f * up, 0);
            }
            StartCoroutine(AccelerationDelay());
        }
        //else{
        //   rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y + dir.y*speed*up, 0);
        //
        //if other name is aipaddle
        if (other.name == "AiPaddle")
        {
            other.GetComponent<AiPaddle>().Collison();
        }
    }

    private void Bound()
    {
        if (transform.position.y > boundY || transform.position.y < -boundY)
        {
            bouncesSinceLast++;
            if (bouncesSinceLast == 5)
            {
                //if x velocity is less than 1.5, type text
                if (Mathf.Abs(rb.velocity.x) < 4f)
                {
                    dt.TypeText(miscTexts[0]);
                }

            }
            if (bouncesSinceLast == 10)
            {
                if (Mathf.Abs(rb.velocity.x) < 1.5f)
                {
                    dt.TypeText(miscTexts[1]);
                }
            }
            if (bouncesSinceLast == 13)
            {
                Score("nobody");
            }
        }
        if (transform.position.y > boundY)
        {
            transform.position = new Vector3(transform.position.x, boundY, 0);
            rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, 0);
        }
        else if (transform.position.y < -boundY)
        {
            transform.position = new Vector3(transform.position.x, -boundY, 0);
            rb.velocity = new Vector3(rb.velocity.x, -rb.velocity.y, 0);
        }
    }

    IEnumerator AccelerationDelay()
    {
        accelerate = false;
        yield return new WaitForSeconds(0.5f);
        accelerate = true;
    }
}
