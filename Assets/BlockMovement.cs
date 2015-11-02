using UnityEngine;
using System.Collections;

public class BlockMovement : MonoBehaviour {

	public GameObject BLOCK;
	GameObject activeBlock;

	float moveSpeed = 10f;
	float defaultFallSpeed = 1f;
    float fallSpeed = 1f;

	float zNextCheckPoint = 1f;

    bool isFalling = true;
    bool isMoving = false;
    bool isRotating = false;
    Vector3 blockStartPos, blockEndPos;
    Quaternion blockStartRot, blockEndRot;

    float tElapsed;

	// Use this for initialization
	void Start () {

		initGrid();
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
				Vector3 futurePos = activeBlock.transform.position + new Vector3(-1, 0, 0);
				Quaternion futureRot = activeBlock.transform.rotation;
				if (!IsPositionBlocked(futurePos, futureRot)) {
					SmoothMove(activeBlock.transform.position, futurePos);
				}
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
				Vector3 futurePos = activeBlock.transform.position + new Vector3(1, 0, 0);
				Quaternion futureRot = activeBlock.transform.rotation;
				if (!IsPositionBlocked(futurePos, futureRot)) {
					SmoothMove(activeBlock.transform.position, futurePos);
				}
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
				Vector3 futurePos = activeBlock.transform.position + new Vector3(0, 1, 0);
				Quaternion futureRot = activeBlock.transform.rotation;
				if (!IsPositionBlocked(futurePos, futureRot)) {
					SmoothMove(activeBlock.transform.position, futurePos);
				}
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
				Vector3 futurePos = activeBlock.transform.position + new Vector3(0, -1, 0);
				Quaternion futureRot = activeBlock.transform.rotation;
				if (!IsPositionBlocked(futurePos, futureRot)) {
					SmoothMove(activeBlock.transform.position, futurePos);
				}
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
			if (activeBlock.transform.position.z >= zNextCheckPoint) {
				zNextCheckPoint++;

				Vector3 futurePos = activeBlock.transform.position + new Vector3(0, 0, 1);
				Quaternion futureRot = activeBlock.transform.rotation;
				if (IsPositionBlocked(futurePos, futureRot))
				{
					activeBlock.transform.position = new Vector3(
						(int)Mathf.Round(activeBlock.transform.position.x), 
						(int)Mathf.Round(activeBlock.transform.position.y), 
						(int)Mathf.Round(activeBlock.transform.position.z));
					SetPositionBlocked();

					activeBlock = (GameObject)GameObject.Instantiate(BLOCK, new Vector3(-2, -2, 1), Quaternion.identity);
					zNextCheckPoint = 1;
				}
			}
        }

    }

	bool[,,] blocked = new bool[7,7,12];

	private void initGrid() {
		for (int i = 0; i < 7; i++) {
			for (int j = 0; j < 7; j++) {
				for (int k = 0; k < 12; k++) {
					blocked[i,j,k] = false;
					if (i == 0 || j == 0 || k == 0 || i == 6 || j == 6 || k == 11)
					{
						blocked[i,j,k] = true;
					}
				}
			}
		}
	}

	private bool IsPositionBlocked(Vector3 futurePos, Quaternion futureRot) {

		if (blocked[
			(int)Mathf.Round (futurePos.x) + 3, (int)Mathf.Round (futurePos.y) + 3, (int)Mathf.Round (futurePos.z)])
			return true;
		return false;
	}

	private void SetPositionBlocked() // uses activeBlock
	{
		int i = (int)Mathf.Round (activeBlock.transform.position.x) + 3;
		int j = (int)Mathf.Round (activeBlock.transform.position.y) + 3;
		int k = (int)Mathf.Round (activeBlock.transform.position.z);
		blocked[i,j,k] = true;

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
