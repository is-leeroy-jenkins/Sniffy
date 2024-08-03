﻿// ******************************************************************************************
//     Assembly:             Bitsy
//     Author:                  Terry D. Eppler
//     Created:                 08-02-2024
// 
//     Last Modified By:        Terry D. Eppler
//     Last Modified On:        08-02-2024
// ******************************************************************************************
// <copyright file="SocketEventArgs.cs" company="Terry D. Eppler">
//    Sniffy is a tiny web browser used is a budget, finance, and accounting tool for analysts with
//    the US Environmental Protection Agency (US EPA).
//    Copyright ©  2024  Terry Eppler
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
//    You can contact me at:   terryeppler@gmail.com or eppler.terry@epa.gov
// </copyright>
// <summary>
//   SocketEventArgs.cs
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
    public struct SocketEventArgs
    {
        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <value>
        /// The text.
        /// </value>
        public string Text { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is meta text.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is meta text;
        /// otherwise, <c>false</c>.
        /// </value>
        public bool IsMetaText { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is send text.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is send text;
        /// otherwise, <c>false</c>.
        /// </value>
        public bool IsSendText { get; }

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="SocketEventArgs"/> struct.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="isMetaText">if set to <c>true</c>
        /// [is meta text].</param>
        /// <param name="isSendText">if set to <c>true</c> </param>
        public SocketEventArgs( string text, bool isMetaText = false, bool isSendText = false )
        {
            Text = text;
            IsMetaText = isMetaText;
            IsSendText = isSendText;
        }
    }
}