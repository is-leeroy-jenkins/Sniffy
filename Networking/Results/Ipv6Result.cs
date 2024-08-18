// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-11-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-11-2024
// ******************************************************************************************
// <copyright file="Ipv6Result.cs" company="Terry D. Eppler">
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
//   Ipv6Result.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;
	using System.Threading.Tasks;

	/// <summary>
	/// 
	/// </summary>
	public class Ipv6Result
	{
		/// <summary>
		/// Initializes a new instance of the
		/// <see cref="Ipv6Result"/> class.
		/// </summary>
		public Ipv6Result( )
		{
		}

		/// Gets or sets the interface.
		/// </summary>
		/// <value>
		/// The interface.
		/// </value>
		public string Interface { get; set; }

		/// <summary>
		/// Gets or sets the metric.
		/// </summary>
		/// <value>
		/// The metric.
		/// </value>
		public int Metric { get; set; }

		/// <summary>
		/// Gets or sets the dest address.
		/// </summary>
		/// <value>
		/// The dest address.
		/// </value>
		public string DestAddress { get; set; }

		/// <summary>
		/// Gets or sets the gateway.
		/// </summary>
		/// <value>
		/// The gateway.
		/// </value>
		public string Gateway { get; set; }
	}
}