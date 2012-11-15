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

using System.Collections.Generic;
using System.ComponentModel.Composition;
using Caliburn.Micro;

namespace Cocktail
{
    [Export]
    public partial class HarnessViewModel : Conductor<object>
    {
        /// <summary>
        ///   Initializes a new instance.
        /// </summary>
        /// <param name="viewModels"> The list of discovered ViewModels injected through MEF. </param>
        [ImportingConstructor]
        public HarnessViewModel([ImportMany] IEnumerable<IDiscoverableViewModel> viewModels)
        {
            _viewModels = new Dictionary<string, object>();
            viewModels.ForEach(vm => _viewModels.Add(vm.GetType().Name, vm));
        }

        /// <summary>
        ///   Activates the ViewModel with the given name.
        /// </summary>
        public void ActivateViewModel(string name)
        {
            object viewModel;
            if (!_viewModels.TryGetValue(name, out viewModel)) 
                return;

            var harnessAware = viewModel as IHarnessAware;
            if (harnessAware != null)
                harnessAware.Setup();

            ActivateItem(viewModel);
            ActiveName = name;
        }
    }
}