using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using ThunderBank.Models;
using ThunderBank.Services.Interfaces;

namespace ThunderBank.Main.Auth
{
    public class UsuarioStore : IUserStore<Usuario>, IUserPasswordStore<Usuario>,IUserRoleStore<Usuario>
    {
        private readonly IRepositorioUsuario _repositorioUsuario;

        public UsuarioStore(IRepositorioUsuario repositorioUsuario)
        {
            this._repositorioUsuario = repositorioUsuario;
        }

        public Task AddToRoleAsync(Usuario user, string roleName, CancellationToken cancellationToken)
        {
            user.Rol = roleName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> CreateAsync(Usuario user, CancellationToken cancellationToken)
        {
            user.Id = await _repositorioUsuario.CrearUsuario(user);
            return IdentityResult.Success;
        }

        public Task<IdentityResult> DeleteAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
        }

        public Task<Usuario> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return await _repositorioUsuario.BuscarUsuarioPorNombre(normalizedUserName);
        }

        public Task<string> GetNormalizedUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Nombre);
        }

        public Task<string> GetPasswordHashAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Pwd);
        }

        public Task<IList<string>> GetRolesAsync(Usuario user, CancellationToken cancellationToken)
        {
            var roles = new List<string>
            {
                user.Rol
            };
            return Task.FromResult<IList<string>>(roles);
        }

        public Task<string> GetUserIdAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(Usuario user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.Nombre);
        }

        public Task<IList<Usuario>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(Usuario user, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFromRoleAsync(Usuario user, string roleName, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task SetNormalizedUserNameAsync(Usuario user, string normalizedName, CancellationToken cancellationToken)
        {
            user.Nombre = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetPasswordHashAsync(Usuario user, string passwordHash, CancellationToken cancellationToken)
        {
            user.Pwd = passwordHash;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(Usuario user, string userName, CancellationToken cancellationToken)
        {
            user.Nombre = userName;
            return Task.CompletedTask;
        }

        public Task<IdentityResult> UpdateAsync(Usuario user, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
