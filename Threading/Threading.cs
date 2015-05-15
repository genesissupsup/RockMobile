using System;
using System.Threading;

#if __IOS__
using Foundation;

namespace Rock.Mobile.Threading
{
    public class Util
    {
        public delegate void ThreadTask( );

        public static void PerformOnUIThread( ThreadTask task )
        {
            new NSObject().InvokeOnMainThread( new Action( task ) );
        }

        public static void PerformOnWorkerThread( System.Threading.ThreadStart loadTask )
        {
            Thread workerThread = new Thread( loadTask );
            workerThread.Start( );
        }
    }
}
#endif

#if __ANDROID__
using Android.App;
using Droid;

namespace Rock.Mobile.Threading
{
    public class Util
    {
        public delegate void ThreadTask( );

        public static void PerformOnUIThread( ThreadTask task )
        {
            ((Activity)Rock.Mobile.PlatformSpecific.Android.Core.Context).RunOnUiThread( new System.Action( task ) );
        }

        public static void PerformOnWorkerThread( System.Threading.ThreadStart loadTask )
        {
            Thread workerThread = new Thread( loadTask );
            workerThread.Start( );
        }
    }
}
#endif
