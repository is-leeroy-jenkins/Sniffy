

namespace Sniffy
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Threading.Tasks;
	using System.Windows;
	using System.Windows.Controls;
	using Microsoft.Win32;

	/// <summary>
    /// SnifferWindow.xaml
    /// </summary>
    [SuppressMessage("ReSharper", "MemberCanBeInternal")]
    [SuppressMessage("ReSharper", "InheritdocConsiderUsage")]
    [SuppressMessage("ReSharper", "RedundantExtendsListEntry")]
    [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
    [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
    [SuppressMessage("ReSharper", "UnusedParameter.Global")]
    [SuppressMessage("ReSharper", "UnusedVariable")]
    [SuppressMessage("ReSharper", "ClassCanBeSealed.Global")]
    public partial class SnifferWindow : UserControl
    {
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="SnifferWindow"/> class.
		/// </summary>
		public SnifferWindow( )
		{
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
