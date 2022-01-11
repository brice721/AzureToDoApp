using Data;
using Domain;
using Domain.Interfaces;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    /// <summary>
    /// CRUD functions for a stored to do list.
    /// </summary>
    public class ToDoService : IToDoService
    {
        private readonly ToDoDbContext _toDoDbContext;

        /// <summary>
        /// CTOR
        /// </summary>
        /// <param name="toDoDbContext" cref="ToDoDbContext"></param>
        public ToDoService(ToDoDbContext toDoDbContext) 
        {
            _toDoDbContext = toDoDbContext;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ToDo>> GetAll() =>
            await _toDoDbContext.ToDos.ToListAsync();

        /// <inheritdoc />
        public async Task<ToDo> GetById(string id) =>
            await _toDoDbContext.ToDos.FirstOrDefaultAsync(row => row.Id == id);

        /// <inheritdoc />
        public async Task<ToDo> Create(ToDoDto dto)
        {
            dto.IsComplete = false;

            var enity = await dto.AsToDoObjectAsync();

            try
            {
                await _toDoDbContext.ToDos.AddAsync(enity);
                await _toDoDbContext.SaveChangesAsync();

                var newToDo = await _toDoDbContext.ToDos.ToListAsync();

                return newToDo.OrderByDescending(x => x.CreatedOn).FirstOrDefault();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<bool> Update(ToDoDto dto)
        {
            var toDoToUpdate = 
                await _toDoDbContext.ToDos.SingleOrDefaultAsync(x => x.Id == dto.Id);

            if (toDoToUpdate == null)
            {
                return false;
            }

            try
            {
                var enity = await dto.AsUpdatedToDoAsync(toDoToUpdate);

                _toDoDbContext.ToDos.Attach(enity);
                _toDoDbContext.Entry(enity).State = EntityState.Modified;

                await _toDoDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);

                throw;
            }
        }

        /// <inheritdoc />
        public async Task<bool> Delete(string id)
        {
            var toDoToDelete =
                await _toDoDbContext.ToDos.SingleOrDefaultAsync(x => x.Id == id);

            if (toDoToDelete == null)
            {
                return false;
            }

            try
            {
                _toDoDbContext.ToDos.Remove(toDoToDelete);
                
                await _toDoDbContext.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);

                throw;
            }
        }
    }
}