using System;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask counterLayerMask;
    private bool _isWalking;
    private Vector3 lastInteractDir;

    private void Start()
    {
        gameInput.OnInteractAction += GameInputOnOnInteractAction;
    }

    private void GameInputOnOnInteractAction(object sender, EventArgs e)
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        var interactDistance = 2f;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (!Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask)) return;
        if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
        {
            clearCounter.Interact();
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }


    public bool IsWalking()
    {
        return _isWalking;
    }

    private void HandleInteractions()
    {
        var inputVector = gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        var interactDistance = 2f;

        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if (!Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, counterLayerMask)) return;
        if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
        {
            clearCounter.Interact();
        }
    }

    private void HandleMovement()   
    {
        var inputVector = gameInput.GetMovementVectorNormalized();

        var moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        var playerTransform = transform;

        var moveDistance = speed * Time.deltaTime;
        var playerRadius = 0.7f;
        var playerHeight = 2;
        var playerTransformPosition = playerTransform.position;
        var canMove = !Physics.CapsuleCast(playerTransformPosition, playerTransformPosition + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);
        
        if (!canMove)
        {
            var moveDirX = new Vector3(moveDir.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(playerTransformPosition, playerTransformPosition + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                var moveDirZ = new Vector3(0, 0, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(playerTransformPosition, playerTransformPosition + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }
        
        _isWalking = moveDir != Vector3.zero;
        playerTransform.forward = Vector3.Slerp(playerTransform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }
    
}
