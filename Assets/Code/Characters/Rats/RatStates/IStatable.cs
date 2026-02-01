namespace Code.Characters.Rats.RatStates
{
    public interface IStatable
    {
        enum MovementAxis
        {
            Horizontal,
            Vertical,
            Falling
        }

        public void SetState(MovementAxis state);
    }
}