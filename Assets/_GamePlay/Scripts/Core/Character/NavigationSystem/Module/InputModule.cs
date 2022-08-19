using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MoveStopMove.Core.Character.NavigationSystem
{
    using Utilitys.Input;
    using Utilitys;
    public class InputModule : AbstractNavigationModule
    {
        JoyStick joyStick;
        Vector2 moveDirection = Vector2.zero;
        private void Start()
        {
            CanvasGameplay gameplay = (CanvasGameplay)UIManager.Inst.GetUI(UIID.UICGamePlay);
            joyStick = gameplay.joyStick;
            joyStick.OnMove += UpdateMoveDirection;
            gameplay.Close();
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