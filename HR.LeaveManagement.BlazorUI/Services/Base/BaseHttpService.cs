using Blazored.LocalStorage;
using System.Net;
using System.Net.Http.Headers;

namespace HR.LeaveManagement.BlazorUI.Services.Base;

public class BaseHttpService
{
    protected IClient _client;
    protected readonly ILocalStorageService _localStorage;

    public BaseHttpService(IClient client, ILocalStorageService localStorage)
    {
        this._client = client;
        this._localStorage = localStorage;
    }

    protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
    {
        if(ex.StatusCode == 400)
        {
            return new Response<Guid>()
            {
                Message = "Invalid data was submitted.",
                ValidationErrors = ex.Response,
                Success = false
            };
        }

        if (ex.StatusCode == 404)
        {
            return new Response<Guid>()
            {
                Message = "The record was not found.",
                Success = false
            };
        }

        return new Response<Guid>()
        {
            Message = "Something went wrong, try again later.",
            Success = false
        };
    }

    protected async Task AddBearerToken()
    {
        var isTokenPresent = await _localStorage.ContainKeyAsync("token");
        if (isTokenPresent)
        {
            var token = await _localStorage.GetItemAsync<string>("token");
            _client.HttpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }
    }
}