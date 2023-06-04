using UnityEngine;

public class AiPaddle : MonoBehaviour
{
    public float speed = 10f;
    public float boundY = 3f;
    public float ballBoundY = 5f;
    public Rigidbody rb;
    private Vector3 velocity;
    private float yOffset = 0f;
    private Vector3 ballPos;
    public bool AI = false;
    // Start is called before the first frame update
    void Start()
    {
        RandomiseOffset();
    }

    void RandomiseOffset()
    {
        yOffset = Random.Range(-0.5f, 0.5f);
    }

    public void SpeedIncrease()
    {
        speed += 1.5f;
    }

    public void Collison()
    {
        RandomiseOffset();
    }

    // Update is called once per frame
    void Update()
    {
        //get ball pos and set ballpos to ball's transform with yoffset
        ballPos = FindBall(GameObject.Find("Ball"));
        if (Mathf.Round(ballPos.y * speed * 2f) == Mathf.Round(transform.position.y * speed * 2f))
        {
            transform.position = new Vector3(transform.position.x, ballPos.y, 0);
        }
        else if (ballPos.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, 0);
        }
        else if (ballPos.y < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - speed * Time.deltaTime, 0);
        }
        rb.velocity = velocity;
        if (transform.position.y > boundY)
        {
            transform.position = new Vector3(transform.position.x, boundY, 0);
        }
        else if (transform.position.y < -boundY)
        {
            transform.position = new Vector3(transform.position.x, -boundY, 0);
        }
    }

    public Vector3 FindBall(GameObject pos)
    {
        Vector3 transform = pos.transform.position;
        if (AI)
        {
            //predict ball y based on ball distance and speed
            //if ball is moving away from ai, dont predict
            //if ball is moving towards ai, predict
            Rigidbody rb = pos.GetComponent<Rigidbody>();
            //get rb velocity x to see if it's positive (moving towards ai)
            if (rb.velocity.x > 0)
            {
                //[get distance from ai to ball
                float distance = Mathf.Abs(this.transform.position.x - pos.transform.position.x);
                //if too close, dont predict
                if (distance < 1f)
                {
                    return transform + new Vector3(0, yOffset, 0);
                }
                //get time to reach ai
                float time = distance / rb.velocity.x;
                //get ball y at time
                float ballY = pos.transform.position.y + rb.velocity.y * time;
                float newBallY = 0f;
                float dir = 1f;
                if (rb.velocity.y < 0)
                {
                    dir = -1f;
                }
                while (Mathf.Round(ballY) != 0f)
                {
                    newBallY += 0.001f * dir;
                    if (ballY > 0f)
                    {
                        ballY -= 0.001f;
                    }
                    else
                    {
                        ballY += 0.001f;
                    }
                    if (newBallY > ballBoundY || newBallY < -ballBoundY)
                    {
                        dir = dir * -1f;
                        if (ballY > 0f)
                        {
                            ballY -= Mathf.Abs(newBallY) - ballBoundY;
                        }
                        else
                        {
                            ballY += Mathf.Abs(newBallY) - ballBoundY;
                        }
                        if (newBallY > ballBoundY)
                        {
                            newBallY = ballBoundY;
                        }
                        else if (newBallY < -ballBoundY)
                        {
                            newBallY = -ballBoundY;
                        }
                    }
                }
                //return ball pos with y offset
                transform.y = newBallY + yOffset;
            }
            else
            {
                transform.y += yOffset;
            }
        }
        else
        {
            transform.y += yOffset;
        }
        return transform;
    }
}
