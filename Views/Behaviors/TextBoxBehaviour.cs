// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-13-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-13-2024
// ******************************************************************************************
// <copyright file="TextBoxBehaviour.cs" company="Terry D. Eppler">
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
//   TextBoxBehaviour.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Collections.Generic;
	using System.Diagnostics.CodeAnalysis;
	using System.Windows;
	using System.Windows.Controls;

	/// <summary>
	/// 
	/// </summary>
	[ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
	[ SuppressMessage( "ReSharper", "InheritdocConsiderUsage" ) ]
	[ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
	[ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedVariable" ) ]
	[ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
	public class TextBoxBehaviour
	{
		/// <summary>
		/// The associations
		/// </summary>
		private protected static readonly Dictionary<TextBox, Capture> _associations =
			new Dictionary<TextBox, Capture>( );

		/// <summary>
		/// The scroll on text changed property
		/// </summary>
		public static readonly DependencyProperty ScrollOnTextChangedProperty =
			DependencyProperty.RegisterAttached( "ScrollOnTextChanged", typeof( bool ),
				typeof( TextBoxBehaviour ),
				new UIPropertyMetadata( false, TextBoxBehaviour.OnScrollOnTextChanged ) );

		/// <summary>
		/// Gets the scroll on text changed.
		/// </summary>
		/// <param name="dependencyObject">The dependency object.</param>
		/// <returns></returns>
		public static bool GetScrollOnTextChanged( DependencyObject dependencyObject )
		{
			return ( bool )dependencyObject.GetValue( ScrollOnTextChangedProperty );
		}

		/// <summary>
		/// Sets the scroll on text changed.
		/// </summary>
		/// <param name="dependencyObject">The dependency object.</param>
		/// <param name="value">if set to <c>true</c> [value].</param>
		public static void SetScrollOnTextChanged( DependencyObject dependencyObject, bool value )
		{
			dependencyObject.SetValue( ScrollOnTextChangedProperty, value );
		}

		/// <summary>
		/// Called when [scroll on text changed].
		/// </summary>
		/// <param name="dependencyObject">The dependency object.</param>
		/// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/>
		/// instance containing the event data.</param>
		public static void OnScrollOnTextChanged( DependencyObject dependencyObject,
			DependencyPropertyChangedEventArgs e )
		{
			var _textBox = dependencyObject as TextBox;
			if( _textBox == null )
			{
				return;
			}

			bool _oldValue = ( bool )e.OldValue, _newValue = ( bool )e.NewValue;
			if( _newValue == _oldValue )
			{
				return;
			}

			if( _newValue )
			{
				_textBox.Loaded += TextBoxBehaviour.TextBoxLoaded;
				_textBox.Unloaded += TextBoxBehaviour.TextBoxUnloaded;
			}
			else
			{
				_textBox.Loaded -= TextBoxBehaviour.TextBoxLoaded;
				_textBox.Unloaded -= TextBoxBehaviour.TextBoxUnloaded;
				if( _associations.ContainsKey( _textBox ) )
				{
					_associations[ _textBox ].Dispose( );
				}
			}
		}

		/// <summary>
		/// Texts the box unloaded.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="routedEventArgs">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		public static void TextBoxUnloaded( object sender, RoutedEventArgs routedEventArgs )
		{
			var _textBox = ( TextBox )sender;
			_associations[ _textBox ].Dispose( );
			_textBox.Unloaded -= TextBoxBehaviour.TextBoxUnloaded;
		}

		/// <summary>
		/// Texts the box loaded.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="routedEventArgs">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
		public static void TextBoxLoaded( object sender, RoutedEventArgs routedEventArgs )
		{
			var _textBox = ( TextBox )sender;
			_textBox.Loaded -= TextBoxBehaviour.TextBoxLoaded;
			_associations[ _textBox ] = new Capture( _textBox );
		}
	}
}