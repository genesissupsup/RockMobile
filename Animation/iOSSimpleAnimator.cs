﻿#if __IOS__
using System;
using Foundation;

namespace Rock.Mobile.Animation
{
    public abstract class SimpleAnimator
    {
        // This defines the rate we wish to update animations at, which happens to be 60fps.
        static float ANIMATION_TICK_RATE = (1.0f / 60.0f); 

        // This is the frequency at which we'll get an animation callback. It's basically 60fps, but needs to be in hundredth-nanoseconds
        // Tick Rate - 60fps
        // Tick Rate in ms - 0.016ms
        // Tick Rate in hundredth nanoseconds = 166666
        static float MILLISECONDS_TO_HUNDREDTH_NANOSECONDS = 10000.0f;
        static long ANIMATION_TICK_FREQUENCY = (long) ((ANIMATION_TICK_RATE * 1000) * MILLISECONDS_TO_HUNDREDTH_NANOSECONDS);

        float DurationTicksPerSec { get; set; }
        float CurrentTime { get; set; }

        NSTimer AnimTimer = null;

        public delegate void AnimationUpdate( float percent, object value );
        public delegate void AnimationComplete( );

        protected abstract void AnimTick( float percent, AnimationUpdate updateDelegate );

        protected void Init( float durationSeconds, AnimationUpdate updateDelegate, AnimationComplete completeDelegate )
        {
            // create our timer at 60hz
            AnimTimer = NSTimer.CreateRepeatingTimer( new TimeSpan( ANIMATION_TICK_FREQUENCY ), new Action<NSTimer>( 
                delegate
                {
                    // update our timer
                    CurrentTime += ANIMATION_TICK_RATE;

                    // let the animation implementation do what it needs to
                    AnimTick( System.Math.Min( CurrentTime / durationSeconds, 1.00f ), updateDelegate );

                    // see if we're finished.
                    if( CurrentTime >= durationSeconds )
                    {
                        // we are, so notify the completion delegate
                        if( completeDelegate != null )
                        {
                            completeDelegate( );
                        }

                        // and kill the timer
                        AnimTimer.Invalidate( );
                    }
                } )
            );
        }

        public void Start( )
        {
            // launch the timer
            NSRunLoop.Current.AddTimer( AnimTimer, NSRunLoop.NSDefaultRunLoopMode );
        }
    }
}
#endif
