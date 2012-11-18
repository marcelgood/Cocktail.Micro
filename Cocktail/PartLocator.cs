//====================================================================================================================
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

using System;
using System.Diagnostics;

namespace Cocktail
{
    internal class PartLocator<T> where T : class
    {
        private T _instance;
        private readonly bool _useFactory;

        private bool _probed;
        private Func<T> _defaultGenerator;

        internal PartLocator(bool useFactory = false)
        {
            _useFactory = useFactory;
            _defaultGenerator = () => null;
            Composition.ProviderChanged +=
                new EventHandler<EventArgs>(OnCompositionProviderChanged).MakeWeak(eh => Composition.ProviderChanged -= eh);
        }

        internal PartLocator(PartLocator<T> partLocator)
        {
            _useFactory = partLocator._useFactory;
            _defaultGenerator = partLocator._defaultGenerator;
            Composition.ProviderChanged +=
                new EventHandler<EventArgs>(OnCompositionProviderChanged).MakeWeak(eh => Composition.ProviderChanged -= eh);
        }

        internal void OnCompositionProviderChanged(object sender, EventArgs e)
        {
            _instance = null;
            _probed = false;
        }

        internal bool Probed { get { return _probed || _instance != null; } }

        internal bool IsAvailable { get { return GetPart() != null; } }

        internal T GetPart()
        {
            if (Probed) return _instance;

            // Look for the part in the IoC container.
            if (_useFactory)
                _instance = TryGetPartFromFactory() ?? DefaultGenerator();
            else
                _instance = Composition.TryGetInstance<T>() ?? DefaultGenerator();

            _probed = true;
            WriteTrace();

            return _instance;
        }

        internal PartLocator<T> WithDefaultGenerator(Func<T> generator)
        {
            if (generator == null)
                throw new ArgumentNullException();

            var partLocator = new PartLocator<T>(this) {_defaultGenerator = generator};
            return partLocator;
        }

        protected Func<T> DefaultGenerator { get { return _defaultGenerator; } }

        private void WriteTrace()
        {
            if (_instance != null)
                LogFns.TraceWriteLine(String.Format(StringResources.ProbedForServiceAndFoundMatch, typeof(T).Name,
                                                 _instance.GetType().FullName));
            else
                LogFns.TraceWriteLine(String.Format(StringResources.ProbedForServiceFoundNoMatch, typeof(T).Name));
        }

        private T TryGetPartFromFactory()
        {
            var factory = Composition.TryGetInstanceFactory<T>();
            if (factory == null)
                return null;

            return factory.NewInstance();
        }
    }
}
