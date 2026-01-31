namespace Code.Characters.Rats.RatStates
{
    public interface IStatable
    {
        enum MovementAxis
        {
            Horizontal,
            Vertical
        }

        public void SetState(MovementAxis state);
    }
}