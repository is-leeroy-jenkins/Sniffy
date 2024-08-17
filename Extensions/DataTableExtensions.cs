// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-11-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-11-2024
// ******************************************************************************************
// <copyright file="DataTableExtensions.cs" company="Terry D. Eppler">
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
//   DataTableExtensions.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Data;
	using System.Diagnostics.CodeAnalysis;
	using System.Linq;
	using System.Xml.Linq;

	/// <summary>
	///
	/// </summary>
	[ SuppressMessage( "ReSharper", "AssignNullToNotNullAttribute" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" ) ]
	[ SuppressMessage( "ReSharper", "ArrangeRedundantParentheses" ) ]
	[ SuppressMessage( "ReSharper", "ArrangeDefaultValueWhenTypeNotEvident" ) ]
	[ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
	[ SuppressMessage( "ReSharper", "UseObjectOrCollectionInitializer" ) ]
	[ SuppressMessage( "ReSharper", "ReturnTypeCanBeEnumerable.Global" ) ]
	[ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
	public static class DataTableExtensions
	{
		/// <summary>
		/// Converts to xml.
		/// </summary>
		/// <param name="dataTable">The dataTable.</param>
		/// <param name="rootName">The rootName.</param>
		/// <returns></returns>
		public static XDocument ToXml( this DataTable dataTable, string rootName )
		{
			try
			{
				ThrowIf.Null( rootName, nameof( rootName ) );
				var _xml = new XDocument
				{
					Declaration = new XDeclaration( "1.0", "utf-8", "" )
				};

				_xml.Add( new XElement( rootName ) );
				foreach( DataRow _dataRow in dataTable.Rows )
				{
					var _element = new XElement( dataTable.TableName );
					for( var _i = 0; _i < dataTable.Columns.Count; _i++ )
					{
						var _col = dataTable.Columns[ _i ];
						var _row = _dataRow?[ _col ]?.ToString( )?.Trim( ' ' );
						var _node = new XElement( _col.ColumnName, _row );
						_element.Add( new XElement( _node ) );
					}

					_xml.Root?.Add( _element );
				}

				return _xml;
			}
			catch( Exception ex )
			{
				DataTableExtensions.Fail( ex );
				return default( XDocument );
			}
		}

		/// <summary>
		/// Gets the primary key values.
		/// </summary>
		/// <param name="dataTable">The dataTable.</param>
		/// <returns>
		/// IEnumerable
		/// </returns>
		public static IEnumerable<int> GetIndexValues( this DataTable dataTable )
		{
			try
			{
				if( dataTable?.Rows?.Count > 0 )
				{
					var _list = new List<int>( );
					foreach( DataColumn _col in dataTable.Columns )
					{
						for( var _i = 0; _i < dataTable.Rows.Count; _i++ )
						{
							if( ( _col.DataType == typeof( int ) )
								&& ( _col.Ordinal == 0 ) )
							{
								var _row = dataTable.Rows[ _i ];
								var _value = _row[ _col.ColumnName ]?.ToString( );
								if( !string.IsNullOrEmpty( _value ) )
								{
									var _index = int.Parse( _value );
									_list.Add( _index );
								}
							}
						}
					}

					return _list?.Any( ) == true
						? _list
						: default( IEnumerable<int> );
				}

				return default( IEnumerable<int> );
			}
			catch( Exception ex )
			{
				DataTableExtensions.Fail( ex );
				return default( IEnumerable<int> );
			}
		}

		/// <summary>
		/// Filters the specified dictionary.
		/// </summary>
		/// <param name="dataTable">The data table.</param>
		/// <param name="dict">The dictionary.</param>
		/// <returns>
		/// IEnumerable
		/// </returns>
		public static IEnumerable<DataRow> Filter( this DataTable dataTable,
			IDictionary<string, object> dict )
		{
			try
			{
				ThrowIf.Null( dict, nameof( dict ) );
				var _query = dataTable?.Select( dict.ToCriteria( ) )?.ToList( );
				return ( _query?.Any( ) == true )
					? _query
					: default( IEnumerable<DataRow> );
			}
			catch( Exception ex )
			{
				DataTableExtensions.Fail( ex );
				return default( IEnumerable<DataRow> );
			}
		}

		/// <summary>
		/// Gets the column names.
		/// </summary>
		/// <param name="dataTable">The data table.</param>
		/// <returns></returns>
		public static string[ ] GetColumnNames( this DataTable dataTable )
		{
			try
			{
				var _fields = new string[ dataTable.Columns.Count ];
				for( var _i = 0; _i < dataTable.Columns.Count; _i++ )
				{
					_fields[ _i ] = dataTable.Columns[ _i ].ColumnName;
				}

				var _names = _fields?.OrderBy( f => f.IndexOf( f ) )?.Select( f => f )?.ToArray( );
				return ( _names?.Any( ) == true )
					? _names
					: default( string[ ] );
			}
			catch( Exception ex )
			{
				DataTableExtensions.Fail( ex );
				return default( string[ ] );
			}
		}

		/// <summary>
		/// Gets the numeric columns.
		/// </summary>
		/// <param name="dataTable">The data table.</param>
		/// <returns>
		/// IList{DataColumn}
		/// </returns>
		public static IList<DataColumn> GetNumericColumns( this DataTable dataTable )
		{
			try
			{
				var _columns = new List<DataColumn>( );
				foreach( DataColumn _col in dataTable.Columns )
				{
					if( !_col.ColumnName.EndsWith( "Id" )
						&& ( _col.Ordinal > 0 )
						&& ( ( _col.DataType == typeof( decimal ) )
							| ( _col.DataType == typeof( float ) )
							| ( _col.DataType == typeof( double ) )
							| ( _col.DataType == typeof( int ) )
							| ( _col.DataType == typeof( uint ) )
							| ( _col.DataType == typeof( ushort ) )
							| ( _col.DataType == typeof( short ) ) ) )
					{
						_columns.Add( _col );
					}
				}

				return _columns?.Any( ) == true
					? _columns
					: default( IList<DataColumn> );
			}
			catch( Exception ex )
			{
				DataTableExtensions.Fail( ex );
				return default( IList<DataColumn> );
			}
		}

		/// <summary>
		/// Gets the date columns.
		/// </summary>
		/// <param name="dataTable">The data table.</param>
		/// <returns>
		/// IList{DataColumn}
		/// </returns>
		public static IList<DataColumn> GetDateColumns( this DataTable dataTable )
		{
			try
			{
				var _columns = new List<DataColumn>( );
				foreach( DataColumn _col in dataTable.Columns )
				{
					if( _col.ColumnName.EndsWith( "Date" )
						|| _col.ColumnName.EndsWith( "Day" )
						|| ( ( _col.DataType == typeof( DateTime ) )
							| ( _col.DataType == typeof( DateTimeOffset ) ) ) )
					{
						_columns.Add( _col );
					}
				}

				return _columns?.Any( ) == true
					? _columns
					: default( IList<DataColumn> );
			}
			catch( Exception ex )
			{
				DataTableExtensions.Fail( ex );
				return default( IList<DataColumn> );
			}
		}

		/// <summary>
		/// Removes the column.
		/// </summary>
		/// <param name="dataTable">The data table.</param>
		/// <param name="columnName">Name of the column.</param>
		public static void RemoveColumn( this DataTable dataTable, string columnName )
		{
			try
			{
				ThrowIf.Null( columnName, nameof( columnName ) );
				var _index = dataTable.Columns.IndexOf( columnName );
				dataTable.Columns.RemoveAt( _index );
				dataTable.AcceptChanges( );
			}
			catch( Exception ex )
			{
				DataTableExtensions.Fail( ex );
			}
		}

		/// <summary>
		/// Converts to bindinglist.
		/// </summary>
		/// <param name="dataTable">The data table.</param>
		/// <returns></returns>
		public static BindingList<DataRow> ToBindingList( this DataTable dataTable )
		{
			try
			{
				var _bindingList = new BindingList<DataRow>( );
				foreach( DataRow _row in dataTable.Rows )
				{
					_bindingList.Add( _row );
				}

				return ( _bindingList?.Any( ) == true )
					? _bindingList
					: default( BindingList<DataRow> );
			}
			catch( Exception ex )
			{
				DataTableExtensions.Fail( ex );
				return default( BindingList<DataRow> );
			}
		}

		/// <summary>
		/// Converts to sortedlist.
		/// </summary>
		/// <param name="dataTable">The data table.</param>
		/// <returns>
		/// SortedList(int, DataRow)
		/// </returns>
		public static SortedList<int, DataRow> ToSortedList( this DataTable dataTable )
		{
			try
			{
				if( dataTable?.Rows.Count > 0 )
				{
					var _sortedList = new SortedList<int, DataRow>( );
					var _columns = dataTable?.Columns;
					var _items = dataTable?.Rows;
					for( var _i = 0; _i < _columns?.Count; _i++ )
					{
						_sortedList?.Add( _i, _items[ _i ] );
					}

					return _sortedList?.Count > 0
						? _sortedList
						: default( SortedList<int, DataRow> );
				}

				return default( SortedList<int, DataRow> );
			}
			catch( Exception ex )
			{
				DataTableExtensions.Fail( ex );
				return default( SortedList<int, DataRow> );
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