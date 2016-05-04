namespace NShogi
{
    public interface IEngine
    {
        Move WaitMove(Position position);
    }
}