namespace Sniffy
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Globalization;
	using System.Windows;
	using System.Windows.Data;

	[ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
	[ SuppressMessage( "ReSharper", "InheritdocConsiderUsage" ) ]
	[ SuppressMessage( "ReSharper", "RedundantExtendsListEntry" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
	[ SuppressMessage( "ReSharper", "FieldCanBeMadeReadOnly.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedVariable" ) ]
	[ SuppressMessage( "ReSharper", "ClassCanBeSealed.Global" ) ]
	public class TextBoxConverter : IValueConverter
	{
		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value produced by the binding source.</param>
		/// <param name="targetType">The type of the binding target property.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// A converted value. If the method returns <see langword="null" />, the valid null value is used.
		/// </returns>
		public object Convert( object value, Type targetType, object parameter,
			CultureInfo culture )
		{
			return ( bool )value
				? Visibility.Visible
				: Visibility.Collapsed;
		}

		/// <summary>
		/// Converts a value.
		/// </summary>
		/// <param name="value">The value that is produced by the binding target.</param>
		/// <param name="targetType">The type to convert to.</param>
		/// <param name="parameter">The converter parameter to use.</param>
		/// <param name="culture">The culture to use in the converter.</param>
		/// <returns>
		/// A converted value. If the method returns <see langword="null" />, the valid null value is used.
		/// </returns>
		public object ConvertBack( object value, Type targetType, object parameter,
			CultureInfo culture )
		{
			return DependencyProperty.UnsetValue;
		}
	}
}
