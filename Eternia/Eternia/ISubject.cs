using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Eternia

    /*
     * used with Iobserver interface. Subject knows it's Observers. Implement in your ConcreteSubject and in notify() method send a
     * notification.
     * for(Observer o: observers)
     *  notify()
     */
{
    public interface ISubject
    {

        void attachObserver(IObserver observer);

        void detachObserver(IObserver observer);
        
        void notify();
    }
}
