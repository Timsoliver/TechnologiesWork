using System;
using UnityEngine;

public class Movement : MonoBehaviour
{
   [SerializeField] CharacterController controller;
   [SerializeField] float speed = 11f;
   private Vector2 horizontalInput;

   [SerializeField] private float jumpHeight = 3.5f;
   private bool jump;
   
   [SerializeField] private float gravity = -30f;
   Vector3 verticalVelocity = Vector3.zero;
   [SerializeField] private LayerMask groundMask;
   bool isGrounded;
   
   [SerializeField] private MouseLook mouseLook;

   private void Update()
   {
      isGrounded = Physics.CheckSphere(transform.position, 0.1f, groundMask);
      if (isGrounded)
      {
         verticalVelocity.y = 0f;
      }
      Vector3 horizontalVelocity = (transform.right * horizontalInput.x + transform.forward * horizontalInput.y) * speed;
      controller.Move(horizontalVelocity * Time.deltaTime);

      if (jump)
      {
         if (isGrounded)
         {
            verticalVelocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
         }
         jump = false;
      }
      
      verticalVelocity.y += gravity * Time.deltaTime;
      controller.Move(verticalVelocity * Time.deltaTime);
   }

   public void ReceiveInput(Vector2 _horizontalInput)
   {
      horizontalInput = _horizontalInput;
   }

   public void OnJumpPressed()
   {
      jump = true;
   }

   public void Teleport(Vector3 position, Quaternion rotation)
   {
      controller.enabled = false;
      transform.position = position;
      controller.enabled = true;
      
      verticalVelocity = Vector3.zero;
      jump = false;

      if (mouseLook != null)
      {
         Vector3 euler = rotation.eulerAngles;
         mouseLook.SetRotation(euler);
      }

      Physics.SyncTransforms();
   }
}
