using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia
{
    public interface IScreenManager
    {
        void attachObserver(IObserver observer);

        void detachObserver(IObserver observer);

        void notify();

        Screen currentScreen();
    }
}
