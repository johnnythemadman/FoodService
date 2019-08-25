using System;

namespace FoodService.Controllers
{

    internal class MyAction : IDisposable
    {
        private bool disposed = false;

        public MyAction(object p)
        {
                
        }
        public void Invoke(Action action)
        {
            action();
            Dispose();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                // Call the overridden Dispose method that contains common cleanup code

                // Pass true to indicate that it is called from Dispose

                Dispose(true);

                // Prevent subsequent finalization of this object. This is not needed

                // because managed and unmanaged resources have been explicitly released

                GC.SuppressFinalize(this);
            }

            ;
        }

        ~MyAction()
        {

            // Call the overridden Dispose method that contains common cleanup code

            // Pass false to indicate the it is not called from Dispose

            Dispose(false);

        }

        // Implement the override Dispose method that will contain common

        // cleanup functionality

        protected virtual void Dispose(bool disposing)
        {

        }

    }

}