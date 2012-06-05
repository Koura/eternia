using System;
namespace Eternia
{
    public interface IGameState
    {
        string getState();

        int getArrowOnOptionState();

        int setArrowOnOptionState(int arrowOnOption);


    }
}
