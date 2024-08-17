// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-17-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-17-2024
// ******************************************************************************************
// <copyright file="TextBoxEx.cs" company="Terry D. Eppler">
//     A tiny .NET WPF tool that can be used to establish TCP (raw) or WebSocket connections
//     and exchange text messages for testing/debugging purposes.
// 
//     Copyright ©  2021 Terry D. Eppler
// 
//    Permission is hereby granted, free of charge, to any person obtaining a copy
//    of this software and associated documentation files (the “Software”),
//    to deal in the Software without restriction,
//    including without limitation the rights to use,
//    copy, modify, merge, publish, distribute, sublicense,
//    and/or sell copies of the Software,
//    and to permit persons to whom the Software is furnished to do so,
//    subject to the following conditions:
// 
//    The above copyright notice and this permission notice shall be included in all
//    copies or substantial portions of the Software.
// 
//    THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
//    INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//    FITNESS FOR A PARTICULAR PURPOSE AND NON-INFRINGEMENT.
//    IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
//    DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE,
//    ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
//    DEALINGS IN THE SOFTWARE.
// 
//    You can contact me at:  terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   TextBoxEx.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="System.Windows.Data.IValueConverter" />
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "InheritdocConsiderUsage" ) ]
    [ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedVariable" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "UsePatternMatching" ) ]
    [ SuppressMessage( "ReSharper", "UseNegatedPatternMatching" ) ]
    [ SuppressMessage( "ReSharper", "PossibleNullReferenceException" ) ]
    [ SuppressMessage( "ReSharper", "ConvertIfStatementToConditionalTernaryExpression" ) ]
    public class TextBoxEx : TextBox
    {
        /// <summary>
        /// The step property
        /// </summary>
        public static readonly DependencyProperty StepProperty =
            DependencyProperty.Register( nameof( Step ), typeof( double ), typeof( TextBoxEx ),
                new PropertyMetadata( 1.0 ) );

        /// <summary>
        /// The minimum property
        /// </summary>
        public static readonly DependencyProperty MinimumProperty =
            DependencyProperty.Register( nameof( Minimum ), typeof( int ), typeof( TextBoxEx ),
                new PropertyMetadata( 0 ) );

        /// <summary>
        /// The maximum property
        /// </summary>
        public static readonly DependencyProperty MaximumProperty =
            DependencyProperty.Register( nameof( Maximum ), typeof( int ), typeof( TextBoxEx ),
                new PropertyMetadata( 0 ) );

        /// <summary>
        /// The corner radius property
        /// </summary>
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.RegisterAttached( "CornerRadius", typeof( CornerRadius ),
                typeof( TextBoxEx ) );

        /// <summary>
        /// The is clear BTN visible property
        /// </summary>
        public static readonly DependencyProperty IsClearButtonVisibleProperty =
            DependencyProperty.RegisterAttached( "IsClearButtonVisible", typeof( bool ),
                typeof( TextBoxEx ), new PropertyMetadata( TextBoxEx.OnTextBoxHookChanged ) );

        // Using a DependencyProperty as the backing store for MyProperty.
        // This enables animation, styling, binding, etc...
        /// <summary>
        /// The is add BTN visible property
        /// </summary>
        public static readonly DependencyProperty IsAddButtonVisibleProperty =
            DependencyProperty.RegisterAttached( "IsAddButtonVisible", typeof( bool ),
                typeof( TextBoxEx ), new PropertyMetadata( TextBoxEx.OnTextChanged ) );

        /// <summary>
        /// The is remove BTN visible property
        /// </summary>
        public static readonly DependencyProperty IsRemoveButtonVisibleProperty =
            DependencyProperty.RegisterAttached( "IsRemoveButtonVisible", typeof( bool ),
                typeof( TextBoxEx ), new PropertyMetadata( TextBoxEx.OnTextChanged ) );

        /// <summary>
        /// Initializes the <see cref="TextBoxEx"/> class.
        /// </summary>
        static TextBoxEx( )
        {
            DefaultStyleKeyProperty.OverrideMetadata( typeof( TextBoxEx ),
                new FrameworkPropertyMetadata( typeof( TextBoxEx ) ) );
        }

        /// <summary>
        /// Gets or sets the step.
        /// </summary>
        /// <value>
        /// The step.
        /// </value>
        public double Step
        {
            get
            {
                return ( double )GetValue( StepProperty );
            }
            set
            {
                SetValue( StepProperty, value );
            }
        }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public int Minimum
        {
            get
            {
                return ( int )GetValue( MinimumProperty );
            }
            set
            {
                SetValue( MinimumProperty, value );
            }
        }

        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public int Maximum
        {
            get
            {
                return ( int )GetValue( MaximumProperty );
            }
            set
            {
                SetValue( MaximumProperty, value );
            }
        }

        /// <summary>
        /// Gets the corner radius.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public CornerRadius GetCornerRadius( DependencyObject obj )
        {
            return ( CornerRadius )obj.GetValue( CornerRadiusProperty );
        }

        /// <summary>
        /// Sets the corner radius.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">The value.</param>
        public static void SetCornerRadius( DependencyObject obj, CornerRadius value )
        {
            obj.SetValue( CornerRadiusProperty, value );
        }

        /// <summary>
        /// Gets the is clear BTN visible.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetIsClearButtonVisible( DependencyObject obj )
        {
            return ( bool )obj.GetValue( IsClearButtonVisibleProperty );
        }

        /// <summary>
        /// Sets the is clear BTN visible.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetIsClearButtonVisible( DependencyObject obj, bool value )
        {
            obj.SetValue( IsClearButtonVisibleProperty, value );
        }

        /// <summary>
        /// Gets the is add BTN visible.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetIsAddButtonVisible( DependencyObject obj )
        {
            return ( bool )obj.GetValue( IsAddButtonVisibleProperty );
        }

        /// <summary>
        /// Sets the is add BTN visible.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetIsAddButtonVisible( DependencyObject obj, bool value )
        {
            obj.SetValue( IsAddButtonVisibleProperty, value );
        }

        /// <summary>
        /// Gets the is remove BTN visible.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <returns></returns>
        public static bool GetIsRemoveButtonVisible( DependencyObject obj )
        {
            return ( bool )obj.GetValue( IsRemoveButtonVisibleProperty );
        }

        /// <summary>
        /// Sets the is remove BTN visible.
        /// </summary>
        /// <param name="obj">The object.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public static void SetIsRemoveButtonVisible( DependencyObject obj, bool value )
        {
            obj.SetValue( IsRemoveButtonVisibleProperty, value );
        }

        /// <summary>
        /// Called when [text box hook changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The
        /// <see cref="DependencyPropertyChangedEventArgs"/>
        /// instance containing the event data.</param>
        private static void OnTextBoxHookChanged( DependencyObject d,
            DependencyPropertyChangedEventArgs e )
        {
            var _textbox = d as TextBox;
            if( _textbox != null )
            {
                _textbox.RemoveHandler( ButtonBase.ClickEvent,
                    new RoutedEventHandler( TextBoxEx.OnClearButtonClick ) );

                _textbox.AddHandler( ButtonBase.ClickEvent,
                    new RoutedEventHandler( TextBoxEx.OnClearButtonClick ) );
            }
        }

        /// <summary>
        /// Clears the BTN clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private static void OnClearButtonClick( object sender, RoutedEventArgs e )
        {
            var _button = e.OriginalSource as Button;
            if( _button == null
                || _button.Name != "PART_ButtonClear" )
            {
                return;
            }

            var _textbox = sender as TextBox;
            if( _textbox == null )
            {
                return;
            }

            _textbox.Text = "";
        }

        /// <summary>
        /// Called when [text changed].
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The
        /// <see cref="DependencyPropertyChangedEventArgs"/>
        /// instance containing the event data.</param>
        private static void OnTextChanged( DependencyObject d,
            DependencyPropertyChangedEventArgs e )
        {
            var _textbox = d as TextBox;
            if( ( bool )e.NewValue )
            {
                _textbox.MouseWheel += TextBoxEx.OnTextboxMouseWheel;
            }
            else
            {
                _textbox.MouseWheel -= TextBoxEx.OnTextboxMouseWheel;
            }

            _textbox.RemoveHandler( ButtonBase.ClickEvent,
                new RoutedEventHandler( TextBoxEx.ButtonClicked ) );

            _textbox.AddHandler( ButtonBase.ClickEvent,
                new RoutedEventHandler( TextBoxEx.ButtonClicked ) );
        }

        /// <summary>
        /// 鼠标滚轮
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The
        /// <see cref="System.Windows.Input.MouseWheelEventArgs"/>
        /// instance containing the event data.</param>
        private static void OnTextboxMouseWheel( object sender, MouseWheelEventArgs e )
        {
            var _args = e.Source as TextBoxEx;
            double _step = 1;
            var _min = 0;
            if( _args != null )
            {
                _step = _args.Step;
                _min = _args.Minimum;
            }

            var _sender = sender as TextBox;
            var _result = double.TryParse( _sender.Text, out var _temp );
            if( _result )
            {
                if( e.Delta > 0 )
                {
                    _sender.Text = ( _temp + _step ).ToString( );
                }
                else
                {
                    _sender.Text = ( _temp - _step ).ToString( );
                }

                if( double.Parse( _sender.Text ) < _min )
                {
                    _sender.Text = _min.ToString( );
                }

                _sender.Select( _sender.Text.Length, 0 );
            }
        }

        /// <summary>
        /// Buttons the clicked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/>
        /// instance containing the event data.</param>
        private static void ButtonClicked( object sender, RoutedEventArgs e )
        {
            var _button = e.OriginalSource as RepeatButton;
            if( _button == null )
            {
                return;
            }

            var _textboxex = e.Source as TextBoxEx;
            double _step = 1;
            var _min = 0;
            if( _textboxex != null )
            {
                _step = _textboxex.Step;
                _min = _textboxex.Minimum;
            }

            var _textbox = sender as TextBox;
            if( _textbox == null )
            {
                return;
            }

            //int Step = step.Step;
            var _result = double.TryParse( _textbox.Text, out var _temp );
            if( _result )
            {
                if( _button.Name == "PART_ButtonAdd" )
                {
                    _textbox.Text = ( _temp + _step ).ToString( );
                }
                else
                {
                    _textbox.Text = ( _temp - _step ).ToString( );
                }

                if( double.Parse( _textbox.Text ) < _min )
                {
                    _textbox.Text = _min.ToString( );
                }

                _textbox.Focus( );
                _textbox.Select( _textbox.Text.Length, 0 );
            }
        }
    }
}