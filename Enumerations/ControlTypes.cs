﻿// ******************************************************************************************
//     Assembly:                Sniffy
//     Author:                  Terry D. Eppler
//     Created:                 08-15-2021
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-15-2024
// ******************************************************************************************
// <copyright file="ControlTypes.cs" company="Terry D. Eppler">
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
//   ControlTypes.cs
// </summary>
// ******************************************************************************************

namespace Sniffy
{
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// 
    /// </summary>
    [ SuppressMessage( "ReSharper", "MemberCanBeInternal" ) ]
    public enum ControlTypes : uint
    {
        /// <summary>
        /// The control c event
        /// </summary>
        ControlCopyEvent = 0,

        /// <summary>
        /// The control break event
        /// </summary>
        ControlBreakEvent,

        /// <summary>
        /// The control close event
        /// </summary>
        ControlCloseEvent,

        /// <summary>
        /// The control logoff event
        /// </summary>
        ControlLogoffEvent = 5,

        /// <summary>
        /// The control shutdown event
        /// </summary>
        ControlShutdownEvent
    }
}