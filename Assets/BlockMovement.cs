using UnityEngine;
using System.Collections;

public class BlockMovement : MonoBehaviour {

    public float moveSpeed = 15f;
    public float fallSpeed = 1f;

    GameObject activeBlock;

    bool isFalling = true;
    bool isMoving = false;
    bool isRotating = false;
    Vector3 blockStartPos, blockEndPos;
    Quaternion blockStartRot, blockEndRot;

    float tElapsed;

	// Use this for initialization
	void Start () {
        // get the active block
        activeBlock = GameObject.FindGameObjectWithTag("Player");
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
            else if (Input.GetKeyDown(KeyCode.W))
            {
                SmoothRotate(activeBlock.transform.rotation, Quaternion.Euler(activeBlock.transform.rotation.x, activeBlock.transform.rotation.y + 90, activeBlock.transform.rotation.z));
            }
            else if (Input.GetKeyDown(KeyCode.A))
            {
                SmoothRotate(activeBlock.transform.rotation, Quaternion.Euler(activeBlock.transform.rotation.x, 90, activeBlock.transform.rotation.z));
            }
            else if (Input.GetKeyDown(KeyCode.S))
            {
                SmoothRotate(activeBlock.transform.rotation, Quaternion.Euler(activeBlock.transform.rotation.x, 90, activeBlock.transform.rotation.z));
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                SmoothRotate(activeBlock.transform.rotation, Quaternion.Euler(activeBlock.transform.rotation.x, 90, activeBlock.transform.rotation.z));
            }
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
