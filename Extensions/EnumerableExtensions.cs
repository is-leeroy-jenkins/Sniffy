// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-11-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-11-2024
// ******************************************************************************************
// <copyright file="EnumerableExtensions.cs" company="Terry D. Eppler">
//    Sniffy is a tiny .NET WPF tool for network interaction written C sharp.
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
//   EnumerableExtensions.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Diagnostics.CodeAnalysis;
	using System.IO;
	using System.Linq;

	/// <summary>
	/// 
	/// </summary>
	[ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
	[ SuppressMessage( "ReSharper", "LoopCanBePartlyConvertedToQuery" ) ]
	[ SuppressMessage( "ReSharper", "UseObjectOrCollectionInitializer" ) ]
	[ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
	[ SuppressMessage( "ReSharper", "UnusedType.Global" ) ]
	[ SuppressMessage( "ReSharper", "CompareNonConstrainedGenericWithNull" ) ]
	public static class EnumerableExtensions
	{
		/// <summary>
		/// Converts to bindinglist.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="enumerable">The enumerable.</param>
		/// <returns></returns>
		public static BindingList<T> ToBindingList<T>( this IEnumerable<T> enumerable )
		{
			try
			{
				var _bindingList = new BindingList<T>( );
				foreach( var _item in enumerable )
				{
					if( _item != null )
					{
						_bindingList.Add( _item );
					}
				}

				return _bindingList?.Any( ) == true
					? _bindingList
					: default( BindingList<T> );
			}
			catch( Exception ex )
			{
				EnumerableExtensions.Fail( ex );
				return default( BindingList<T> );
			}
		}

		/// <summary>
		/// Filters a sequence of values based on a given predicate
		/// and returns those values that don't match
		/// the predicate.
		/// </summary>
		/// <typeparam name="T">The type of the elements of
		/// <paramref name="source" />
		/// .</typeparam>
		/// <param name="source">An
		/// <see cref="IEnumerable{T}" />
		/// to filter.</param>
		/// <param name="predicate">A function to test each element for a condition.</param>
		/// <returns>
		/// Those values that don't match the given predicate.
		/// </returns>
		public static IEnumerable<T> WhereNot<T>( this IEnumerable<T> source,
			Func<T, bool> predicate )
		{
			try
			{
				return source.Where( element => !predicate( element ) );
			}
			catch( Exception ex )
			{
				EnumerableExtensions.Fail( ex );
				return default( IEnumerable<T> );
			}
		}

		/// <summary>
		/// Filters a sequence of values based on a predicate and returns those values that don't match the
		/// given predicate. Each element's index is used in the logic of predicate function.
		/// </summary>
		/// <typeparam name="T">The type of the elements of
		/// <paramref name="source" />
		/// .</typeparam>
		/// <param name="source">An
		/// <see cref="IEnumerable{T}" />
		/// to filter.</param>
		/// <param name="predicate">A function to test each element for a condition; the second parameter of the functions represents
		/// the index of the source element.</param>
		/// <returns>
		/// Those values that don't match the given predicate.
		/// </returns>
		public static IEnumerable<T> WhereNot<T>( this IEnumerable<T> source,
			Func<T, int, bool> predicate )
		{
			try
			{
				return source.Where( ( element, index ) => !predicate( element, index ) );
			}
			catch( Exception ex )
			{
				EnumerableExtensions.Fail( ex );
				return default( IEnumerable<T> );
			}
		}

		/// <summary>
		/// Slices the specified start.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="type">The dataRow.</param>
		/// <param name="start">The start.</param>
		/// <param name="end">The end.</param>
		/// <returns></returns>
		public static IEnumerable<T> LazySlice<T>( this IEnumerable<T> type, int start, int end )
		{
			ThrowIf.NegativeOrZero( start, nameof( start ) );
			ThrowIf.NegativeOrZero( end, nameof( end ) );
			var _index = 0;
			foreach( var _item in type )
			{
				if( _index >= end )
				{
					yield break;
				}

				if( _index >= start )
				{
					yield return _item;
				}

				++_index;
			}
		}

		/// <summary>
		/// Fails the specified ex.
		/// </summary>
		/// <param name="ex">The ex.</param>
		private static void Fail( Exception ex )
		{
			var _error = new ErrorWindow( ex );
			_error?.SetText( );
			_error?.ShowDialog( );
		}
	}
}