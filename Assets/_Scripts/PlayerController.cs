using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.PlayerLoop;

public class PlayerController : MonoBehaviour
{
  Vector2 moveDirection;
  Vector2 LookDirection;
  public float moveSpeed = 2;
  public float maxForwardSpeed = 8;
  public float turnSpeed = 100;
  float desiredSpeed;
  float forwardSpeed;
  
  const float groundAccel = 5;
  const float groundDecel = 5;

  private float rotationSpeed = .8f;

  Animator anim;
  

  bool IsMoveInput{
    get { return !Mathf.Approximately(moveDirection.sqrMagnitude,0f); }
  }
  
 public void OnMove(InputAction.CallbackContext context){
     moveDirection = context.ReadValue<Vector2>();
  }

  // Use mouse to look around the world
   public void OnLook(InputAction.CallbackContext context){
     LookDirection = context.ReadValue<Vector2>();
  }

void Move(Vector2 direction){
    float turnAmount = direction.x;
    float fDirection = direction.y;
    if(direction.sqrMagnitude > 1f) direction.Normalize();

    desiredSpeed = direction.magnitude * maxForwardSpeed * Mathf.Sign(fDirection);
    float acceleration = IsMoveInput ? groundAccel : groundDecel;

    forwardSpeed = Mathf.MoveTowards(forwardSpeed, desiredSpeed, acceleration * Time.deltaTime);
    anim.SetFloat("ForwardSpeed", forwardSpeed);
    transform.Rotate(0,turnAmount * turnSpeed * Time.deltaTime,0);
    //transform.Translate(direction.x * moveSpeed * Time.deltaTime, 0, direction.y * moveSpeed * Time.deltaTime);

}



void Start(){
    anim = this.GetComponent<Animator>();
}


public Transform spine;
Vector2 lastLookDirection;

float xSensitivity = 0.5f;
float ySensitivity = 0.5f;

void LateUpdate() {
   lastLookDirection += new Vector2(-LookDirection.y * ySensitivity, LookDirection.x * xSensitivity);
   // Clamp the values
   lastLookDirection.x = Mathf.Clamp(lastLookDirection.x,-30,60);
   lastLookDirection.y = Mathf.Clamp(lastLookDirection.y,-30,30);

   spine.localEulerAngles = lastLookDirection;
}

void Update(){
    Move(moveDirection);
}

}
