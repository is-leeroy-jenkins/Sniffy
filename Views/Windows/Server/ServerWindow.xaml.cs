// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-13-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-13-2024
// ******************************************************************************************
// <copyright file="ServerWindow.xaml.cs" company="Terry D. Eppler">
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
//   ServerWindow.xaml.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Threading.Tasks;
	using System.Windows;
	using Microsoft.Win32;
	using System.Windows.Controls;
	using Syncfusion.SfSkinManager;

	/// <summary>
	/// ServerWindow.xaml
	/// </summary>
	[ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
	[ SuppressMessage( "ReSharper", "InheritdocConsiderUsage" ) ]
	[ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
	[ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedVariable" ) ]
	[ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
	public partial class ServerWindow : UserControl
	{
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="ServerWindow"/> class.
		/// </summary>
		public ServerWindow( )
		{
			// Theme Properties
			SfSkinManager.ApplyStylesOnApplication = true;
			SfSkinManager.SetTheme( this, new Theme( "FluentDark", App.Controls ) );

			// Window Properties
			InitializeComponent( );
		}

		/// <summary>
		/// Fades the in asynchronous.
		/// </summary>
		/// <param name="form">The o.</param>
		/// <param name="interval">The interval.</param>
		private async void FadeInAsync( UserControl form, int interval = 80 )
		{
			try
			{
				ThrowIf.Null( form, nameof( form ) );
				while( form.Opacity < 1.0 )
				{
					await Task.Delay( interval );
					form.Opacity += 0.05;
				}

				form.Opacity = 1;
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Fades the out asynchronous.
		/// </summary>
		/// <param name="form">The o.</param>
		/// <param name="interval">The interval.</param>
		private async void FadeOutAsync( UserControl form, int interval = 80 )
		{
			try
			{
				ThrowIf.Null( form, nameof( form ) );
				while( form.Opacity > 0.0 )
				{
					await Task.Delay( interval );
					form.Opacity -= 0.05;
				}

				form.Opacity = 0;
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Called when [calculator menu option click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/>
		/// instance containing the event data.</param>Cal
		private void OnCalculatorMenuOptionClick( object sender, EventArgs e )
		{
			try
			{
				var _calculator = new CalculatorWindow( );
				_calculator.ShowDialog( );
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Called when [file menu option click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/>
		/// instance containing the event data.</param>
		private void OnFileMenuOptionClick( object sender, EventArgs e )
		{
			try
			{
				var _dialog = new SaveFileDialog( );
				_dialog.ShowDialog( );
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Called when [folder menu option click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/>
		/// instance containing the event data.</param>
		private void OnFolderMenuOptionClick( object sender, EventArgs e )
		{
			try
			{
				var _dialog = new FolderBrowser( );
				_dialog.ShowDialog( );
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Called when [control panel option click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/>
		/// instance containing the event data.</param>
		private void OnControlPanelOptionClick( object sender, EventArgs e )
		{
			try
			{
				WinMinion.LaunchControlPanel( );
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Called when [task manager option click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/>
		/// instance containing the event data.</param>
		private void OnTaskManagerOptionClick( object sender, EventArgs e )
		{
			try
			{
				WinMinion.LaunchTaskManager( );
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Called when [close option click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/>
		/// instance containing the event data.</param>
		private void OnCloseOptionClick( object sender, EventArgs e )
		{
			try
			{
				Opacity = 0;
				FadeOutAsync( this );
				Application.Current.Shutdown( );
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Called when [chrome option click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/>
		/// containing the event data.</param>
		private void OnChromeOptionClick( object sender, EventArgs e )
		{
			try
			{
				WebMinion.RunChrome( );
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Called when [edge option click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/>
		/// instance containing the event data.</param>
		private void OnEdgeOptionClick( object sender, EventArgs e )
		{
			try
			{
				WebMinion.RunEdge( );
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Called when [firefox option click].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/>
		/// containing the event data.</param>
		private void OnFirefoxOptionClick( object sender, EventArgs e )
		{
			try
			{
				WebMinion.RunFirefox( );
			}
			catch( Exception ex )
			{
				Fail( ex );
			}
		}

		/// <summary>
		/// Fails the specified ex.
		/// </summary>
		/// <param name="ex">The ex.</param>
		private protected void Fail( Exception ex )
		{
			var _error = new ErrorWindow( ex );
			_error?.SetText( );
			_error?.ShowDialog( );
		}
	}
}