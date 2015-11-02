using UnityEngine;
using System.Collections;

public class BlockMovement : MonoBehaviour {

	public GameObject BLOCK;
	GameObject activeBlock;

	float moveSpeed = 10f;
	float defaultFallSpeed = 1f;
    float fallSpeed = 1f;

	float currentDepth = 1f;
	float nextDepth = 2f;

    bool isFalling = true;
    bool isMoving = false;
    bool isRotating = false;
    Vector3 blockStartPos, blockEndPos;
    Quaternion blockStartRot, blockEndRot;

    float tElapsed;

	// Use this for initialization
	void Start () {
        // get the active block
        //activeBlock = GameObject.FindGameObjectWithTag("Player");
		activeBlock = (GameObject)GameObject.Instantiate(BLOCK, new Vector3(-2, -2, 1), Quaternion.identity);
		activeBlock.transform.position = new Vector3(-2, -2, 1);

	}

    // Update is called once per frame
    void Update()
    {
        if (!isMoving && !isRotating)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SmoothMove(activeBlock.transform.position, activeBlock.transform.position + new Vector3(-1, 0, 0));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SmoothMove(activeBlock.transform.position, activeBlock.transform.position + new Vector3(1, 0, 0));
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                SmoothMove(activeBlock.transform.position, activeBlock.transform.position + new Vector3(0, 1, 0));
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                SmoothMove(activeBlock.transform.position, activeBlock.transform.position + new Vector3(0, -1, 0));
            }
			else if (Input.GetKeyDown(KeyCode.Q))
			{
				SmoothRotate(activeBlock.transform.rotation, Quaternion.Euler(0, 0, 90) * activeBlock.transform.rotation);
			}
            else if (Input.GetKeyDown(KeyCode.W))
            {
				SmoothRotate(activeBlock.transform.rotation, Quaternion.Euler(90, 0, 0) * activeBlock.transform.rotation);
			}
			else if (Input.GetKeyDown(KeyCode.E))
			{
				SmoothRotate(activeBlock.transform.rotation, Quaternion.Euler(0, 0, -90) * activeBlock.transform.rotation);
			}
            else if (Input.GetKeyDown(KeyCode.A))
            {
				SmoothRotate(activeBlock.transform.rotation, Quaternion.Euler(0, 90, 0) * activeBlock.transform.rotation);
			}
            else if (Input.GetKeyDown(KeyCode.S))
            {
				SmoothRotate(activeBlock.transform.rotation, Quaternion.Euler(-90, 0, 0) * activeBlock.transform.rotation);
			}
            else if (Input.GetKeyDown(KeyCode.D))
            {
				SmoothRotate(activeBlock.transform.rotation, Quaternion.Euler(0, -90, 0) * activeBlock.transform.rotation);
			}

			fallSpeed = defaultFallSpeed;
			if (Input.GetKey(KeyCode.Space)) 
			{
				fallSpeed = 20f;
			}
			Debug.Log (fallSpeed);
        }
        else
        {
            if (isMoving)
            {
                SmoothMove();
            }
            else if (isRotating)
            {
                SmoothRotate();
            }
        }
        if (isFalling)
        {
            activeBlock.transform.position += new Vector3(0, 0, fallSpeed * Time.deltaTime);
			if (activeBlock.transform.position.z >= nextDepth) {
				currentDepth = nextDepth;
				nextDepth++;

				if (currentDepth == 10)
				{
					activeBlock.transform.position = new Vector3(activeBlock.transform.position.x, activeBlock.transform.position.y, 10);
					activeBlock = (GameObject)GameObject.Instantiate(BLOCK, new Vector3(-2, -2, 1), Quaternion.identity);
					currentDepth = 1;
					nextDepth = 2;
				}
			}
        }

    }

    private void SmoothMove(Vector3 start, Vector3 end)
    {
        isMoving = true;
        blockStartPos = start;
        blockEndPos = end;
        tElapsed = 0;
        SmoothMove();
    }

    private void SmoothMove()
    {
        tElapsed += Time.deltaTime * moveSpeed;
        activeBlock.transform.position = Vector3.Lerp(blockStartPos, blockEndPos, tElapsed);
        if (tElapsed >= 1f) isMoving = false;
    }

    private void SmoothRotate(Quaternion start, Quaternion end)
    {
        isRotating = true;
        blockStartRot = start;
        blockEndRot = end;
        tElapsed = 0;
        SmoothRotate();
    }

    private void SmoothRotate()
    {
        tElapsed += Time.deltaTime * moveSpeed;
        activeBlock.transform.rotation = Quaternion.Lerp(blockStartRot, blockEndRot, tElapsed);
        if (tElapsed >= 1f) isRotating = false;
    }
}
