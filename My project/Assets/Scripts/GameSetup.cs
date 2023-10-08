using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class GameSetup : MonoBehaviour
{
    int redBallsRemaining = 7;
    int blueBallsRemaining = 7;
    float ballRadius;
    float ballDiamaeter;
    float ballDiamaeterWithBuffer;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;
    [SerializeField] Transform headBallPosition;

    // Start is called before the first frame update
    void Start()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 100f;
        ballDiamaeter = ballRadius * 2f;
        PlaceAllBalls();

    }

    void PlaceAllBalls()
    {
        PlaceCueBall();
        PlaceRandomBalls();

    }
    void PlaceCueBall()
    {
        GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeCueBall();
            
    }
    void PlaceEightBall(Vector3 position)
    {
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        ball.GetComponent <Ball>().MakeEightBall();

    }
    void PlaceRandomBalls()
    {
        int NumInThisRow = 1;
        int rand;
        Vector3 firstInRowPosition = headBallPosition.position;
        Vector3 currentPosition = firstInRowPosition;

        void PlaceRedBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab,position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemaining--;
            
        }
        void PlaceBlueBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(false);
            blueBallsRemaining--;
        }
        //outer loop is 5 rows
        for (int i = 0; i< 5; i++)
        {
            //inner loop is balls in each row
            for(int j = 0; j< NumInThisRow; j++)
            {
                //check to see if this is the middle spot where 8 ball goes
                if (i == 2 && j == 1)
                {
                    PlaceEightBall(currentPosition);
                }
                //if there are red and blue balls remaining choose it
                else if (redBallsRemaining > 0 && blueBallsRemaining > 0)
                {
                    rand = Random.Range(0, 2);
                    if (rand == 0)
                    {
                        PlaceRedBall(currentPosition);
                    }
                    else
                    {
                        PlaceBlueBall(currentPosition);
                    }

                }
                //if only red balls remaining then place 1
                else if (redBallsRemaining > 0)
                {
                    PlaceRedBall(currentPosition);
                }
                //other wise place a blue ball
                else
                {
                    PlaceBlueBall(currentPosition);
                }
                //move current position for the next bow to the right
                currentPosition += new Vector3(1, 0, 0).normalized * ballDiamaeter;
             
            }
            //once all balls have been placed move to the next row
            firstInRowPosition += Vector3.back * (Mathf.Sqrt(3) * ballRadius ) + Vector3.left * ballRadius;
            currentPosition = firstInRowPosition;
            NumInThisRow++;
        }
    }
}