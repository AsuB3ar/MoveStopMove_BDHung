using System.Collections.Generic;

namespace Utilitys.AI
{
    using MoveStopMove.Core.Character.LogicSystem;
    using MoveStopMove.Core.Character;
    public enum State
    {
        Idle = 0,
        Move = 1,
        Jump = 2,
        Die = 3,
        Attack = 4,
        Wandering = 100,
        Combat = 101,
    }

    public class BasicStateInsts<P,D>
        where P : AbstractParameterSystem
        where D : AbstractDataSystem<D>
    {
        Dictionary<State,BaseState<P,D>> states = new Dictionary<State,BaseState<P,D>>();
        //DEVELOP:Change condition to "If Name = null -> State = null"
        public BasicStateInsts()
        {
        }
        public BaseState<P, D> GetState(State name)
        {
            return states[name];
        }

        public void PushState(State state,BaseState<P,D> stateScript)
        {
            if (states.ContainsKey(state))
            {
                return;
            }
            else
            {
                states.Add(state, stateScript);
            }
        }
    }
}
