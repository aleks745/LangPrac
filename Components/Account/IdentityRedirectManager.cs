using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace LangPrac.Components.Account
{
    internal sealed class IdentityRedirectManager
    {
        private readonly NavigationManager _navigationManager;
        private readonly IJSRuntime _jsRuntime;

        public IdentityRedirectManager(NavigationManager navigationManager, IJSRuntime jsRuntime)
        {
            _navigationManager = navigationManager;
            _jsRuntime = jsRuntime;
        }

        public const string StatusCookieName = "Identity.StatusMessage";

        [DoesNotReturn]
        public void RedirectTo(string? uri)
        {
            uri ??= "";

            if (!Uri.IsWellFormedUriString(uri, UriKind.Relative))
            {
                uri = _navigationManager.ToBaseRelativePath(uri);
            }

            _navigationManager.NavigateTo(uri);
            throw new InvalidOperationException($"{nameof(IdentityRedirectManager)} can only be used during static rendering.");
        }

        [DoesNotReturn]
        public void RedirectTo(string uri, Dictionary<string, object?> queryParameters)
        {
            var uriWithoutQuery = _navigationManager.ToAbsoluteUri(uri).GetLeftPart(UriPartial.Path);
            var newUri = _navigationManager.GetUriWithQueryParameters(uriWithoutQuery, queryParameters);
            RedirectTo(newUri);
        }

        [DoesNotReturn]
        public async Task RedirectToWithStatusAsync(string uri, string message)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StatusCookieName, message);
            RedirectTo(uri);
        }

        private string CurrentPath => _navigationManager.ToAbsoluteUri(_navigationManager.Uri).GetLeftPart(UriPartial.Path);

        [DoesNotReturn]
        public async Task RedirectToCurrentPageWithStatusAsync(string message)
        {
            await RedirectToWithStatusAsync(CurrentPath, message);
        }
    }
}
