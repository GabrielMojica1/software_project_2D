using UnityEngine;

    public interface IMovementState
    {
        void Move();
        void HandleLeft();
        void HandleRight();
        void HandleDown();
        void HandleUp();
        void HandleBoost();
        void UpdateState();
    }

