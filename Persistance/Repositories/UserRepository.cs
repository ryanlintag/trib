using Application;
using Application.Repositories;
using Domain.Users;
using Domain.Users.Events;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Persistance.DbContexts;
using System.Linq;

namespace Persistance.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private AppDbContext _dbContext { get; set; }
        public UserRepository(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        public async Task CreateNewUser(User user)
        {
            await this._dbContext.Users.AddAsync(user);

            var userCreatedEvent = new UserCreatedEvent(user.Id.id,
                                                    user.Email.Address,
                                                    user.Name.FirstName,
                                                    user.Name.LastName,
                                                    user.Name.MiddleName,
                                                    user.Role.Name,
                                                    user.UpdatedByUser.Email.Address,
                                                    user.DateLastUpdated);

            var userEvent = new UserDomainEvent(new UserDomainId(Guid.NewGuid()), user.Id, 1, userCreatedEvent);
            await this._dbContext.UserEvents.AddAsync(userEvent);
        }

        public async Task SaveChanges()
        {
            await this._dbContext.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            this._dbContext.Entry<User>(user).State = EntityState.Modified;

            var userUpdatedEvent = new UserDetailsUpdatedEvent(
                                                    user.Email.Address,
                                                    user.Name.FirstName,
                                                    user.Name.LastName,
                                                    user.Name.MiddleName,
                                                    user.Role.Name,
                                                    user.IsActive,
                                                    user.UpdatedByUser.Email.Address,
                                                    user.DateLastUpdated);

            var lastVersion = await this._dbContext.UserEvents.Where(p => p.StreamId == user.Id).MaxAsync(p => p.Version);

            var userEvent = new UserDomainEvent(new UserDomainId(Guid.NewGuid()), user.Id, lastVersion + 1, userUpdatedEvent);
            await this._dbContext.UserEvents.AddAsync(userEvent);
        }

        public bool UserExists(string address)
        {
            return this._dbContext.Users.Any(p => p.Email.Address == address);
        }

        public async Task<User> GetUserById(UserId userId)
        {
            return await this._dbContext.Users.FirstOrDefaultAsync(p => p.Id == userId);
        }

        public async Task<User> GetUserByEmail(Email email)
        {
            return await this._dbContext.Users.FirstOrDefaultAsync(p => p.Email == email);
        }

        public async Task ActivateUser(User user)
        {
            this._dbContext.Entry<User>(user).State = EntityState.Modified;

            var userActivatedEvent = new UserActivatedEvent(user.UpdatedByUser.Email.Address, user.DateLastUpdated);

            var lastVersion = await this._dbContext.UserEvents.Where(p => p.StreamId == user.Id).MaxAsync(p => p.Version);

            var userEvent = new UserDomainEvent(new UserDomainId(Guid.NewGuid()), user.Id, lastVersion + 1, userActivatedEvent);
            await this._dbContext.UserEvents.AddAsync(userEvent);
        }

        public async Task DeactivateUser(User user)
        {
            this._dbContext.Entry<User>(user).State = EntityState.Modified;

            var userDeactivatedEvent = new UserDeactivatedEvent(user.UpdatedByUser.Email.Address, user.DateLastUpdated);

            var lastVersion = await this._dbContext.UserEvents.Where(p => p.StreamId == user.Id).MaxAsync(p => p.Version);

            var userEvent = new UserDomainEvent(new UserDomainId(Guid.NewGuid()), user.Id, lastVersion + 1, userDeactivatedEvent);
            await this._dbContext.UserEvents.AddAsync(userEvent);

        }

        public async Task<PagedList<UserRecord>> GetUsers(string searchValue, int currentPage, int itemsPerPage, string orderByProperty, SortOrder sortOrder, CancellationToken cancellationToken)
        {
            IQueryable<User> usersQuery = this._dbContext.Users;
            var totalCount = usersQuery.Count();
            if (!string.IsNullOrEmpty(searchValue))
            {
                usersQuery = this._dbContext.Users.Where(p =>
                                p.Email.Address.ToLower().Contains(searchValue)
                                || p.Name.FirstName.ToLower().Contains(searchValue)
                                || p.Name.LastName.ToLower().Contains(searchValue)
                                || p.Name.MiddleName.ToLower().Contains(searchValue)
                                );
            }

            switch (orderByProperty)
            {
                case "Name":
                    if (sortOrder.Value == sortOrder.OrderByAscending)
                    {
                        usersQuery.OrderBy(p => p.Name.FirstName)
                                    .ThenBy(p => p.Name.MiddleName)
                                    .ThenBy(p => p.Name.LastName);
                    }
                    else
                    {
                        usersQuery.OrderByDescending(p => p.Name.FirstName)
                                    .ThenByDescending(p => p.Name.MiddleName)
                                    .ThenByDescending(p => p.Name.LastName);
                    }
                    break;
                default:                     
                    if(sortOrder.Value == sortOrder.OrderByAscending)
                    {
                        usersQuery.OrderBy(p => p.Email.Address);
                    }
                    else
                    {
                        usersQuery.OrderByDescending(p => p.Email.Address);
                    }
                    break;
            }

            usersQuery = usersQuery.Skip(currentPage - 1)
                    .Take(itemsPerPage);

            var list = await usersQuery
                        .Select(p => new UserRecord(
                            p.Email.Address,
                            p.Name.FirstName,
                            p.Name.MiddleName,
                            p.Name.LastName,
                            p.Role.Name ,
                            p.IsActive,
                            p.UpdatedByUser.Email.Address,
                            p.DateLastUpdated))
                        .ToListAsync(cancellationToken);
            return new PagedList<UserRecord>(list, currentPage, itemsPerPage, totalCount, searchValue, orderByProperty, sortOrder);
        }
    }
}
