using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Character.NavigationSystem
{
    using Utilitys.Input;
    public class InputModule : AbstractNavigationModule
    {
        [SerializeField]
        JoyStick joyStick;
        Vector2 moveDirection = Vector2.zero;
        private void Awake()
        {
            joyStick.OnMove += UpdateMoveDirection;
        }
        public override void UpdateData()
        {
            Vector3 move = (Vector3.right * moveDirection.x + Vector3.forward * moveDirection.y).normalized;
            Data.MoveDirection = move;
        }    

        private void UpdateMoveDirection(Vector2 moveDirection)
        {
            this.moveDirection = moveDirection;
        }

        private void OnDisable()
        {
            joyStick.OnMove -= UpdateMoveDirection;
        }
    }
}