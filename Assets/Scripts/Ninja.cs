using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ninja : MonoBehaviour
{
    [SerializeField] Columns columns = null;
    [SerializeField] float jumpSpeed = 10;
    [SerializeField] float fallingMultiplier = 1.5f;
    [SerializeField] float positionOddX = 0.9f, positionOddY = 0.11f;
    [SerializeField] float recoveryTime = 1f;
    [SerializeField] float faceplantDistance = 1f;
    [Tooltip("Distance from the column")][SerializeField] float jumpChargeLowest = 0.2f;
    [SerializeField] float losePosition = -8;
    [SerializeField] UnityEvent loseEvent = null;

    public delegate void OnJump(bool move);
    public static OnJump onJump;

    public delegate void OnLand();
    public static OnLand onLand;

    public static bool isJumping = false;
    public static bool isRecovering = true;

    readonly float fallSpeed = 9.8f;
    
    float currentSpeed = 0;
    Vector3 landingPosition;
    public Transform currentColumn { get; private set; }
    int currentColumnIndex = 0;
    int nextColumnIndex = 1;

    private void Awake()
    {
        currentColumn = columns.columns[currentColumnIndex];
        landingPosition = transform.position;
    }

    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, (currentSpeed < 0 ? currentSpeed * fallingMultiplier : currentSpeed ) * Time.fixedDeltaTime, 0));     
        if (!isJumping) return;

        if (transform.position.y < losePosition)
        {
            onJump.Invoke(false);
            loseEvent.Invoke();
            return;
        }

        for (int i = 0; i < columns.columns.Count; i++)
        {
            Transform column = columns.columns[i];
            if (column.position.y <= transform.position.y && column.position.y + positionOddY >= transform.position.y)
            {
                if (column.position.x - positionOddX <= transform.position.x && column.position.x + positionOddX >= transform.position.x)
                {
                    currentSpeed = 0;
                    isRecovering = true;
                    isJumping = false;
                    landingPosition = transform.position;
                    nextColumnIndex = i;
                    onJump.Invoke(false);
                    onLand.Invoke();
                    currentColumn = column;
                    currentColumnIndex = i;
                    nextColumnIndex = nextColumnIndex + 1 >= columns.columns.Count ? 0 : nextColumnIndex + 1;
                    return;
                }
            }
        }

        if (columns.columns[nextColumnIndex].position.x - transform.position.x >= 0)
        {
            if (transform.position.y < columns.columns[nextColumnIndex].position.y)
            {
                if (columns.columns[nextColumnIndex].position.x - transform.position.x <= faceplantDistance)
                {
                    onJump.Invoke(false);

                }

            }
        }
        else
        {
            nextColumnIndex = nextColumnIndex + 1 >= columns.columns.Count ? 0 : nextColumnIndex + 1;
            print("current " + currentColumnIndex + ", next" + nextColumnIndex);
        }

        currentSpeed -= fallSpeed * Time.fixedDeltaTime;
    }

    float currentRecoverTime = 0;

    private void Update()
    {
        if (isRecovering)
        {
            currentRecoverTime += Time.deltaTime;
            if(currentRecoverTime > recoveryTime)
            {
                currentRecoverTime = 0;
                isRecovering = false;
                landingPosition = transform.position;
                return;
            }
            transform.position = Vector3.Lerp(landingPosition, landingPosition + new Vector3(0, 1, 0), currentRecoverTime / recoveryTime);
        }
    }

    public void Jump(float speed)
    {
        isJumping = true;
        currentSpeed = speed * jumpSpeed;
        onJump.Invoke(true);
    }

    public void ChargeJump(float time)
    {
        transform.position = Vector3.Lerp(landingPosition, new Vector3(landingPosition.x, currentColumn.position.y + jumpChargeLowest, landingPosition.z), time);
    }

    public int GetColumnIndexDifference()
    {
        if (currentColumnIndex < nextColumnIndex) return nextColumnIndex - currentColumnIndex; 
        else { return  columns.columns.Count - (currentColumnIndex - nextColumnIndex); }
    }
}
