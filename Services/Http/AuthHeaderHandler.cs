using Phanteon.Services.Storage;
using Phanteon.Constants;

namespace Phanteon.Services.Http
{
    /// <summary>
    /// Handler que agrega automáticamente el token de autorización a las peticiones HTTP
    /// </summary>
    public class AuthHeaderHandler : DelegatingHandler
    {
        private readonly ISecureStorageService _storage;

        public AuthHeaderHandler(ISecureStorageService storage)
        {
            _storage = storage;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            // Obtener el token de autenticación del almacenamiento seguro
            var token = await _storage.GetAsync(AppConstants.StorageKeys.AuthToken);

            if (!string.IsNullOrEmpty(token))
            {
                // Agregar el header de autorización con el token Bearer
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
