#if __ANDROID__
using System;
using System.Drawing;
using Android.Widget;
using Android.Graphics;
using Android.Views;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Java.IO;
using Droid;
using Rock.Mobile.PlatformUI.DroidNative;
using Rock.Mobile.PlatformSpecific.Android.Graphics;

namespace Rock.Mobile
{
    namespace PlatformUI
    {
        /// <summary>
        /// Android implementation of a circleView
        /// </summary>
        public class DroidCircleView : PlatformCircleView
        {
            protected CircleView View { get; set; }

            public DroidCircleView( )
            {
                View = new CircleView( Rock.Mobile.PlatformSpecific.Android.Core.Context );
                View.LayoutParameters = new ViewGroup.LayoutParams( ViewGroup.LayoutParams.WrapContent, ViewGroup.LayoutParams.WrapContent );
                View.Style = Android.Graphics.Paint.Style.FillAndStroke;
            }

            // Properties
            protected override void setBackgroundColor( uint backgroundColor )
            {
                //View.SetBackgroundColor( Rock.Mobile.PlatformUI.Util.GetUIColor( 0xFFFF00FF ) );
                View.Color = Rock.Mobile.PlatformUI.Util.GetUIColor( backgroundColor );
                View.Invalidate( );
            }

            protected override void setBorderColor( uint borderColor )
            {
                // Not supported by circles
            }

            protected override float getBorderWidth()
            {
                return 0;
            }
            protected override void setBorderWidth( float width )
            {
                // not supported by circles
            }

            protected override float getOpacity( )
            {
                return View.Alpha;
            }

            protected override void setOpacity( float opacity )
            {
                View.Alpha = opacity;
            }

            protected override float getZPosition( )
            {
                //Android doesn't use/need a Z position for its layers. (It goes based on order added)
                return 0.0f;
            }

            protected override void setZPosition( float zPosition )
            {
                //Android doesn't use/need a Z position for its layers. (It goes based on order added)
            }

            protected override RectangleF getBounds( )
            {
                //Bounds is simply the localSpace coordinates of the edges.
                // NOTE: On android we're not supporting a non-0 left/top. I don't know why you'd EVER
                // want this, but it's possible to set on iOS.
                return new RectangleF( 0, 0, View.LayoutParameters.Width, View.LayoutParameters.Height );
            }

            protected override void setBounds( RectangleF bounds )
            {
                //Bounds is simply the localSpace coordinates of the edges.
                // NOTE: On android we're not supporting a non-0 left/top. I don't know why you'd EVER
                // want this, but it's possible to set on iOS.
                View.LayoutParameters.Width = ( int )bounds.Width;
                View.LayoutParameters.Height = ( int )bounds.Height;
            }

            protected override RectangleF getFrame( )
            {
                //Frame is the transformed bounds to include position, so the Right/Bottom will be absolute.
                RectangleF frame = new RectangleF( View.GetX( ), View.GetY( ), View.LayoutParameters.Width, View.LayoutParameters.Height );
                return frame;
            }

            protected override void setFrame( RectangleF frame )
            {
                //Frame is the transformed bounds to include position, so the Right/Bottom will be absolute.
                setPosition( new System.Drawing.PointF( frame.X, frame.Y ) );

                RectangleF bounds = new RectangleF( frame.Left, frame.Top, frame.Width, frame.Height );
                setBounds( bounds );
            }

            protected override System.Drawing.PointF getPosition( )
            {
                return new System.Drawing.PointF( View.GetX( ), View.GetY( ) );
            }

            protected override void setPosition( System.Drawing.PointF position )
            {
                View.SetX( position.X );
                View.SetY( position.Y );
            }

            protected override bool getHidden( )
            {
                return View.Visibility == ViewStates.Gone ? true : false;
            }

            protected override void setHidden( bool hidden )
            {
                View.Visibility = hidden == true ? ViewStates.Gone : ViewStates.Visible;
            }

            protected override bool getUserInteractionEnabled( )
            {
                // doesn't matter if we return this or regular Focusable,
                // because we set them both, guaranteeing the same value.
                return View.FocusableInTouchMode;
            }

            protected override void setUserInteractionEnabled( bool enabled )
            {
                View.FocusableInTouchMode = enabled;
                View.Focusable = enabled;
            }

            public override void AddAsSubview( object masterView )
            {
                // we know that masterView will be an iOS View.
                RelativeLayout view = masterView as RelativeLayout;
                if( view == null )
                {
                    throw new Exception( "Object passed to Android AddAsSubview must be a RelativeLayout." );
                }

                view.AddView( View );
            }

            public override void RemoveAsSubview( object masterView )
            {
                // we know that masterView will be an iOS View.
                RelativeLayout view = masterView as RelativeLayout;
                if( view == null )
                {
                    throw new Exception( "Object passed to Android RemoveAsSubview must be a RelativeLayout." );
                }

                view.RemoveView( View );
            }

            protected override object getPlatformNativeObject( )
            {
                return View;
            }
        }
    }
}
#endif
