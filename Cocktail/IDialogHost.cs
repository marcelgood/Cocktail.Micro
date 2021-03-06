﻿//====================================================================================================================
// Copyright (c) 2012 IdeaBlade
//====================================================================================================================
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE 
// WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS 
// OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR 
// OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE. 
//====================================================================================================================
// USE OF THIS SOFTWARE IS GOVERENED BY THE LICENSING TERMS WHICH CAN BE FOUND AT
// http://cocktail.ideablade.com/licensing
//====================================================================================================================

using Caliburn.Micro;

namespace Cocktail
{
    /// <summary>Public interface to interact with the dialog host.</summary>
    public interface IDialogHost : IHideObjectMembers
    {
        /// <summary>
        /// Returns the command invoked by the user.
        /// </summary>
        IUICommand InvokedCommand { get; }

        /// <summary>Returns the user's response to a dialog or message box.</summary>
        object DialogResult { get; }

        /// <summary>Returns the logical button for the provided button value.</summary>
        /// <param name="value">The user response value associated with this button.</param>
        /// <returns>A logical object representing the dialog or message box button.</returns>
        DialogButton GetButton(object value);

        /// <summary>
        /// Instructs the dialog host to try closing the dialog window with the provided user response.
        /// </summary>
        /// <param name="dialogResult">The simulated user response.</param>
        void TryClose(object dialogResult);
    }

    /// <summary>
    /// A static class that provides access to the current DialogHost from within a hosted ViewModel.
    /// </summary>
    public static class DialogHost
    {
        /// <summary>Returns a reference to the dialog host if the provided ViewModel is currently hosted in the dialog host.</summary>
        /// <param name="source">The hosted ViewModel.</param>
        /// <returns>Null if the ViewModel is not currently hosted in a dialog host, otherwise a reference to the current dialog host.</returns>
        public static IDialogHost GetCurrent(IChild source)
        {
            return source.Parent as IDialogHost;
        }
    }
}