// ====================================================================================================================
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

using System.Threading.Tasks;

namespace Cocktail
{
    /// <summary>
    ///   Interface to be implemented by a unit of work.
    /// </summary>
    public interface IUnitOfWork : IHideObjectMembers
    {
        /// <summary>
        /// Resets the UnitOfWork to its initial state.
        /// </summary>
        void Clear();

        /// <summary>
        ///   Returns true if the unit of work contains pending changes.
        /// </summary>
        bool HasChanges();

        /// <summary>
        ///   Commits all pending changes to the underlying data source.
        /// </summary>
        Task CommitAsync();

        /// <summary>
        ///   Rolls back all pending changes.
        /// </summary>
        void Rollback();
    }

    /// <summary>
    ///   Interface to be implemented by a simple unit of work with a single entity.
    /// </summary>
    /// <typeparam name="T"> The type of entity used with this unit of work. </typeparam>
    public interface IUnitOfWork<T> : IUnitOfWork where T : class
    {
        /// <summary>
        ///   The factory to create new entity instances.
        /// </summary>
        IFactory<T> Factory { get; }

        /// <summary>
        ///   The repository to retrieve entities.
        /// </summary>
        IRepository<T> Entities { get; }
    }
}