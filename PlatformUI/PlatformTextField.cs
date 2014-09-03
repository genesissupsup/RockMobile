﻿using System;
using System.Drawing;

namespace Rock.Mobile
{
    namespace PlatformUI
    {
        // put common utility things here (enums, etc)
        public enum TextAlignment
        {
            Left,
            Center,
            Right,
            Justified,
            Natural
        }

        /// <summary>
        /// The base text field that provides an interface to platform specific text fields.
        /// </summary>
        public abstract class PlatformTextField : PlatformBaseLabelUI
        {
            public static PlatformTextField Create( )
            {
                #if __IOS__
                return new iOSTextField( );
                #endif

                #if __ANDROID__
                return new DroidTextField( );
                #endif
            }

            public string Placeholder
            {
                get { return getPlaceholder( ); }
                set { setPlaceholder( value ); }
            }
            protected abstract string getPlaceholder( );
            protected abstract void setPlaceholder( string placeholder );

            public uint PlaceholderTextColor
            {
                set { setPlaceholderTextColor( value ); }
            }
            protected abstract void setPlaceholderTextColor( uint color );

            public bool ScaleHeightForText
            {
                get { return getScaleHeightForText( ); }
                set { setScaleHeightForText( value ); }
            }
            protected abstract bool getScaleHeightForText( );
            protected abstract void setScaleHeightForText( bool scale );

            public abstract void ResignFirstResponder( );
        }
    }
}

