using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace MoveStopMove.Core.Character.WorldInterfaceSystem
{
    public class DetectCharacterSensor : BaseSensor
    {
        private readonly Vector3 unit = new Vector3(1, 0.05f, 1);

        [SerializeField]
        Transform checkPoint;
        float checkRadius;
        [SerializeField]
        Collider parentCollider;
        //[SerializeField]
        //SensorType type;

        Collider[] temp = new Collider[10];
        private Queue<Collider> oldCharacters = new Queue<Collider>();
        public override void UpdateData()
        {
            checkRadius = Parameter.CharacterData.AttackRange;
            Array.Clear(temp, 0, temp.Length);
            Physics.OverlapBoxNonAlloc(checkPoint.position, unit * checkRadius, temp, Quaternion.identity, layer);
            EnterCheck(temp);
            StayCheck(temp);
            //Debug.Log(Data.CharacterPositions.Count);
        }

        private void StayCheck(Collider[] characters)
        {
            Data.CharacterPositions.Clear();
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i] == null || characters[i] == parentCollider)
                    continue;
                //Debug.Log((characters[i].transform.position - checkPoint.position).sqrMagnitude);

                if ((characters[i].transform.position - checkPoint.position).sqrMagnitude < checkRadius * checkRadius)
                {
                    Data.CharacterPositions.Add(characters[i].transform.position);
                }
                
            }
        }

        private void EnterCheck(Collider[] characters)
        {
            Data.CharacterPositions.Clear();
            int oldCount = oldCharacters.Count;
            for (int i = 0; i < characters.Length; i++)
            {
                if (characters[i] == null || characters[i] == parentCollider)
                    continue;
                if (!oldCharacters.Contains(characters[i]))
                {
                    if((characters[i].transform.position - checkPoint.position).sqrMagnitude < checkRadius * checkRadius)
                    {
                        Data.CharacterPositions.Add(characters[i].transform.position);
                    }                    
                }
                oldCharacters.Enqueue(characters[i]);
            }

            for (int i = 0; i < oldCount; i++)
            {
                oldCharacters.Dequeue();
            }
            
        }

        private void OnDrawGizmos()
        {
            if (checkPoint != null)
            {
                Gizmos.DrawCube(checkPoint.position, unit * checkRadius * 2);
            }
        }
    }
}