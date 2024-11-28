using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI; //This refrences a class for unity called AI Navigation

public class PlayerMovement : MonoBehaviour
{
    //NavMeshAgent is what makes point and click possible without hardcoding it in unity.
    NavMeshAgent agent;
    //Refrence to gamemanager
    private GameManager gameManager;
     

    //Refrence to layers so player can choose which variable represents which layer.
    [Header("Movement")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask resourceLayer;

    //Speed at which player rotates towards something, the speed is stored in a variable
    float lookRotationSpeed = 70f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        gameManager = GameManager.Instance;
    }

    

    // Update is called once per frame
    void Update()
    {
        //When player presses right click ontheir mouse, this part gets executed.
        if (Input.GetMouseButtonDown(1))
        {
            //Ray is taking a ray fired in the 3d space of the game from the position where mouse pointer was on the game
            //screen at the time of clicking
            Ray myRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            //RaycastHit gets the info on where the fired ray hit on the gameobject on the gamescene
            
            //If hitting on resourcelayer, execute the following code and got to collect the resource by standing in a safe location
            if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, resourceLayer))
            {
                //Getting position of the resource which the ray hit
                Vector3 resourcePosition = hitInfo.collider.transform.position;
                Vector3 playerPosition = gameObject.transform.position;
                //getting position of player on the game scene board

                Vector3 destinationPosition = new Vector3();

                //Need to check if the player is moving in from up down left or right
                if(resourcePosition.x < playerPosition.x && resourcePosition.z == 0)
                {
                    destinationPosition = new Vector3(resourcePosition.x + 1.25f, playerPosition.y, 0);
                    agent.SetDestination(destinationPosition);
                    //setting destination for player, agent is player as it refers to a component the player gameobject is carrying
                }
                else if(resourcePosition.x > playerPosition.x && resourcePosition.z == 0)
                {
                    destinationPosition = new Vector3(resourcePosition.x - 1.25f, playerPosition.y, 0);
                    agent.SetDestination(destinationPosition);
                }
                else if (resourcePosition.z < playerPosition.z && resourcePosition.x == 0) 
                {
                    destinationPosition = new Vector3(0, playerPosition.y, resourcePosition.z + 1);
                    agent.SetDestination(destinationPosition);
                }
                else if(resourcePosition.z > playerPosition.z && resourcePosition.x == 0)
                {
                    destinationPosition = new Vector3(0, playerPosition.y, resourcePosition.z - 1);
                    agent.SetDestination(destinationPosition);
                }

                //face target is a fucntion which makes the player face the destination target.
                FaceTarget();
            }
            //If ray hit on ground layer, execure following code
            else if (Physics.Raycast(myRay, out hitInfo, Mathf.Infinity, groundLayer))
            {
                //made a gameobject to refrence to the pooled objects in the ClickParticlesPool singleton
                GameObject clickEffect = ClickParticlesPool.Instance.GetPooledObject();

                //changing state of the player to walking from available states
                gameManager.SwitchPlayerState(PlayerState.Walking);

                //checking if pooledobject refrence is null or not to refrence and enable it on desired point
                if (clickEffect != null)
                {
                    clickEffect.transform.position = hitInfo.point += new Vector3(0, 0.1f, 0);
                    clickEffect.SetActive(true);
                    StartCoroutine(DisableClickEffect(clickEffect));
                }
                //refer to comment above
                agent.SetDestination(hitInfo.point);

                FaceTarget();

                //this coroutine checks if player reached destination or not
                StartCoroutine(ReachedDestination(hitInfo.point));

            }
        }

    }

    IEnumerator ReachedDestination(Vector3 point)
    {
        bool walking = true;
        float tolerance = 0.1f;

        while (walking)
        {
            if (Mathf.Abs(agent.transform.position.x - point.x) < tolerance &&
                Mathf.Abs(agent.transform.position.z - point.z) < tolerance)
            {
                //Switch the state of the player to idle if player reached destination as they stopped walking
                walking = false;
                gameManager.SwitchPlayerState(PlayerState.Idle);
                yield return null;
            }

            yield return new WaitForSeconds(1f);
        }
        
    }


    //this function makes the target face our required direction
    void FaceTarget()
    {
        //it takes the destination and current position and normalizes them to get the direction to face
        Vector3 direction = (agent.destination - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));//lookrotation makes the object with this script look towards specified direction
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * lookRotationSpeed);//slerp makes the rotation smooth and not sudden so the object transitions from facing one direction to another in a more natural manner
    }

    //disable pooledobject clickeffects to reuse them later when needed
    IEnumerator DisableClickEffect(GameObject clickEffect)
    {
        yield return new WaitForSeconds(.5f);
        clickEffect.SetActive(false);
    }
}
