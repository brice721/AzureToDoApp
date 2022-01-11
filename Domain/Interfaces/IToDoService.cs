using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    /// <summary>
    /// Contract for the ToDoListService
    /// </summary>
    public interface IToDoService
    {
        /// <summary>
        /// Gets a list of all to do items.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<ToDo>> GetAll();

        /// <summary>
        /// Gets a single to do item by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ToDo> GetById(string id);

        /// <summary>
        /// Creates a new to do item.
        /// </summary>
        /// <param name="toDo" cref="ToDo"></param>
        /// <returns></returns>
        Task<ToDo> Create(ToDoDto dto);

        /// <summary>
        /// Updates a to do item after finding the item in the database by its id.
        /// </summary>
        /// <param name="toDo" cref="ToDo"></param>
        /// <returns></returns>
        Task<bool> Update(ToDoDto dto);

        /// <summary>
        /// Deletes a to do item associated with the supplied id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> Delete(string id);
    }
}
