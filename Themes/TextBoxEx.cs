// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-13-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-13-2024
// ******************************************************************************************
// <copyright file="TextBoxEx.cs" company="Terry D. Eppler">
//     A tiny .NET WPF tool that can be used to establish TCP (raw) or WebSocket connections
//     and exchange text messages for testing/debugging purposes.
// 
//     Copyright ©  2020 Terry D. Eppler
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
	using System.Globalization;
	using System.Windows;
	using System.Windows.Controls;
	using System.Windows.Controls.Primitives;
	using System.Windows.Data;
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
		public static readonly DependencyProperty IsClearBtnVisibleProperty =
			DependencyProperty.RegisterAttached( "IsClearBtnVisible", typeof( bool ),
				typeof( TextBoxEx ), new PropertyMetadata( TextBoxEx.OnTextBoxHookChanged ) );

		// Using a DependencyProperty as the backing store for MyProperty.
		// This enables animation, styling, binding, etc...
		/// <summary>
		/// The is add BTN visible property
		/// </summary>
		public static readonly DependencyProperty IsAddBtnVisibleProperty =
			DependencyProperty.RegisterAttached( "IsAddBtnVisible", typeof( bool ),
				typeof( TextBoxEx ), new PropertyMetadata( TextBoxEx.OnTextChanged ) );

		/// <summary>
		/// The is remove BTN visible property
		/// </summary>
		public static readonly DependencyProperty IsRemoveBtnVisibleProperty =
			DependencyProperty.RegisterAttached( "IsRemoveBtnVisible", typeof( bool ),
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
		public static bool GetIsClearBtnVisible( DependencyObject obj )
		{
			return ( bool )obj.GetValue( IsClearBtnVisibleProperty );
		}

		/// <summary>
		/// Sets the is clear BTN visible.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <param name="value">if set to <c>true</c> [value].</param>
		public static void SetIsClearBtnVisible( DependencyObject obj, bool value )
		{
			obj.SetValue( IsClearBtnVisibleProperty, value );
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
					new RoutedEventHandler( TextBoxEx.ClearBtnClicked ) );

				_textbox.AddHandler( ButtonBase.ClickEvent,
					new RoutedEventHandler( TextBoxEx.ClearBtnClicked ) );
			}
		}

		/// <summary>
		/// Clears the BTN clicked.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RoutedEventArgs"/>
		/// instance containing the event data.</param>
		private static void ClearBtnClicked( object sender, RoutedEventArgs e )
		{
			var _button = e.OriginalSource as Button;
			if( _button == null
				|| _button.Name != "PART_BtnClear" )
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
		/// Gets the is add BTN visible.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns></returns>
		public static bool GetIsAddBtnVisible( DependencyObject obj )
		{
			return ( bool )obj.GetValue( IsAddBtnVisibleProperty );
		}

		/// <summary>
		/// Sets the is add BTN visible.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <param name="value">if set to <c>true</c> [value].</param>
		public static void SetIsAddBtnVisible( DependencyObject obj, bool value )
		{
			obj.SetValue( IsAddBtnVisibleProperty, value );
		}

		/// <summary>
		/// Gets the is remove BTN visible.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <returns></returns>
		public static bool GetIsRemoveBtnVisible( DependencyObject obj )
		{
			return ( bool )obj.GetValue( IsRemoveBtnVisibleProperty );
		}

		/// <summary>
		/// Sets the is remove BTN visible.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <param name="value">if set to <c>true</c> [value].</param>
		public static void SetIsRemoveBtnVisible( DependencyObject obj, bool value )
		{
			obj.SetValue( IsRemoveBtnVisibleProperty, value );
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
				_textbox.MouseWheel += TextBoxEx.Textbox_MouseWheel;
			}
			else
			{
				_textbox.MouseWheel -= TextBoxEx.Textbox_MouseWheel;
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
		private static void Textbox_MouseWheel( object sender, MouseWheelEventArgs e )
		{
			var _textboxex = e.Source as TextBoxEx;
			double _step = 1;
			var _min = 0;
			if( _textboxex != null )
			{
				_step = _textboxex.Step;
				_min = _textboxex.Minimum;
			}

			var _textbox = sender as TextBox;
			var _result = double.TryParse( _textbox.Text, out var _temp );
			if( _result )
			{
				if( e.Delta > 0 )
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

				_textbox.Select( _textbox.Text.Length, 0 );
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
				if( _button.Name == "PART_BtnAdd" )
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