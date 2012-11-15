﻿// ====================================================================================================================
//   Copyright (c) 2012 IdeaBlade
// ====================================================================================================================
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
//   WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
//   OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
//   OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
// ====================================================================================================================
//   USE OF THIS SOFTWARE IS GOVERENED BY THE LICENSING TERMS WHICH CAN BE FOUND AT
//   http://cocktail.ideablade.com/licensing
// ====================================================================================================================

using System;
using Caliburn.Micro;

namespace Cocktail
{
    /// <summary>
    ///   A set of static methods and properties to inquire about and configure design time mode.
    /// </summary>
    public class DesignTime
    {
        private static readonly Func<bool> InDesignModeDefault = () => Execute.InDesignMode;

        /// <summary>
        /// Function to determine if in DesignMode. Can be replaced for testing.
        /// </summary>
        public static Func<bool> InDesignMode = InDesignModeDefault;

        /// <summary>
        /// Restore <see cref="InDesignMode"/> to default method. For testing.
        /// </summary>
        public static void ResetInDesignModeToDefault()
        {
            InDesignMode = InDesignModeDefault;
        }
    }
}