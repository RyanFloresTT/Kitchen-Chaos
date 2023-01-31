using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private GameInput gameInput;
    private bool _isWalking;
    private void Update()
    {

        var inputVector = gameInput.GetMovementVectorNormalized();
        
        inputVector = inputVector.normalized;

        var moveDir = new Vector3(inputVector.x, 0, inputVector.y);
        var playerTransform = transform;

        _isWalking = moveDir != Vector3.zero;
        
        playerTransform.position += moveDir * (Time.deltaTime * speed);
        playerTransform.forward = Vector3.Slerp(playerTransform.forward, moveDir, Time.deltaTime * rotationSpeed);
    }

    public bool IsWalking()
    {
        return _isWalking;
    }
}
