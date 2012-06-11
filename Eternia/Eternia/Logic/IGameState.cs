using System;
namespace Eternia
{
    public interface IGameState
    {
        string getState();
        void setState(String state);
    }
}
