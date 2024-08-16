// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-14-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-14-2024
// ******************************************************************************************
// <copyright file="App.xaml.cs" company="Terry D. Eppler">
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
//   App.xaml.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Windows;
    using System.Windows.Media;
    using OfficeOpenXml;
    using Syncfusion.Licensing;
    using Syncfusion.SfSkinManager;
    using Syncfusion.Themes.FluentDark.WPF;

    /// <inheritdoc />
    /// <summary>
    /// App.xaml  
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    [ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
    [ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
    [ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
    [ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
    [ SuppressMessage( "ReSharper", "SuggestVarOrType_SimpleTypes" ) ]
    public partial class App : Application
    {
        /// <summary>
        /// The key
        /// </summary>
        private static readonly string _key = ConfigurationManager.AppSettings[ "UI" ];

        /// <summary>
        /// The controls
        /// </summary>
        public static string[ ] Controls =
        {
            "ComboBoxAdv",
            "MetroComboBox",
            "MetroDatagrid",
            "ToolBarAdv",
            "ToolStrip",
            "MetroCalendar",
            "CalendarEdit",
            "PivotGridControl",
            "MetroPivotGrid",
            "MetroMap",
            "EditControl",
            "CheckListBox",
            "MetroEditor",
            "DropDownButtonAdv",
            "MetroDropDown",
            "GridControl",
            "MetroGridControl",
            "TabControlExt",
            "MetroTabControl",
            "MetroTextInput",
            "MenuItemAdv",
            "ButtonAdv",
            "Carousel",
            "ColorEdit",
            "SfChart",
            "SfChart3D",
            "SfHeatMap",
            "SfMap",
            "SfDataGrid",
            "SfTextBoxExt",
            "SfCircularProgressBar",
            "SfLinearProgressBar",
            "SfTextInputLayout",
            "SfSpreadsheet",
            "SfSpreadsheetRibbon",
            "SfCalculator",
            "SfMultiColumnDropDownControl"
        };

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="T:Sniffy.App" /> class.
        /// </summary>
        public App( )
            : base( )
        {
            SyncfusionLicenseProvider.RegisterLicense( _key );
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            RegisterTheme( );
        }

        /// <summary>
        /// Registers the theme.
        /// </summary>
        private void RegisterTheme( )
        {
            var _theme = new FluentDarkThemeSettings
            {
                PrimaryBackground = new SolidColorBrush( Color.FromRgb( 0, 120, 212 ) ),
                PrimaryForeground = new SolidColorBrush( Color.FromRgb( 222, 222, 222 ) ),
                BodyFontSize = 11,
                HeaderFontSize = 16,
                SubHeaderFontSize = 12,
                TitleFontSize = 14,
                SubTitleFontSize = 10,
                BodyAltFontSize = 9,
                FontFamily = new FontFamily( "Segoe UI" )
            };

            SfSkinManager.RegisterThemeSettings( "FluentDark", _theme );
            SfSkinManager.ApplyStylesOnApplication = true;
        }

        /// <summary>
        /// Handles the UnhandledException event of the CurrentDomain control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="UnhandledExceptionEventArgs"/>
        /// instance containing the event data.</param>
        private static void OnUnhandledException( object sender, UnhandledExceptionEventArgs e )
        {
            var _exception = ( Exception )e.ExceptionObject;
            App.Fail( _exception );
        }

        /// <summary>
        /// Fails the specified _ex.
        /// </summary>
        /// <param name="ex">The _ex.</param>
        private static void Fail( Exception ex )
        {
            var _error = new ErrorWindow( ex );
            _error?.SetText( );
            _error?.ShowDialog( );
        }
    }
}